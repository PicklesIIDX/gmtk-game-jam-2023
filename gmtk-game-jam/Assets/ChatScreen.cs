using System;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ChatScreen : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private VisualElement _textArea;
    [SerializeField] private bool flagSpeakSequence = false;
    [SerializeField] private Conversation _currentConversation;
    [SerializeField] private Conversation[] _conversations = Array.Empty<Conversation>();
    [SerializeField] private float lineDelay = 1.0f;
    [SerializeField] private float characterDelay = 0.03f;
    
    // Start is called before the first frame update
    void Start()
    {
        _textArea = _document.rootVisualElement.Q<VisualElement>("text-area");
    }

    // Update is called once per frame
    void Update()
    {
        if (flagSpeakSequence)
        {
            flagSpeakSequence = false;
            StartCoroutine(TestChatSequence(_currentConversation));
        }
    }

    void SelectConversation(string condition)
    {
        var validConversations = _conversations.Where(x => x.ChatEntries.Any(x => x.Speaker == condition)).ToList();
        if (validConversations.Count == 0)
        {
            return;
        }
        _currentConversation = validConversations[Random.Range(0, validConversations.Count - 1)];
        flagSpeakSequence = true;
    }

    private IEnumerator TestChatSequence(Conversation conversation)
    {
        foreach (var chatEntry in conversation.ChatEntries)
        {
            yield return StartCoroutine(AddLine(chatEntry.Speaker, chatEntry.Line));
            yield return new WaitForSeconds(lineDelay);
        }
    }

    public IEnumerator AddLine(string speaker, string line)
    {
        SetSpeaker(speaker);
        var textEntry = new Label();
        textEntry.AddToClassList("chat-text");
        _textArea.Insert(0, textEntry);
        if (_textArea.childCount > 10)
        {
            _textArea.RemoveAt(_textArea.childCount-1);
        }
        int index = 0;
        while (index < line.Length)
        {
            index++;
            textEntry.text = line.Substring(0, index);
            yield return new WaitForSeconds(characterDelay);
        }
    }

    [SerializeField] private Transform _speakerHolder;
    [SerializeField] private Transform _nonSpeakerHolder;
    public void SetSpeaker(string speaker)
    {
        if (_speakerHolder.childCount > 0)
        {
            var lastSpeaker = _speakerHolder.GetChild(0);
            lastSpeaker.parent = _nonSpeakerHolder;
            lastSpeaker.transform.localPosition = Vector3.zero;
        }
        for (int i = 0; i < _nonSpeakerHolder.childCount; i ++)
        {
            var child = _nonSpeakerHolder.GetChild(i);
            if (child.name == speaker)
            {
                child.transform.parent = _speakerHolder;
                child.transform.localPosition = Vector3.zero;
                return;
            }
        }
        Debug.LogError($"no speaker '{speaker}'");
    }
}
