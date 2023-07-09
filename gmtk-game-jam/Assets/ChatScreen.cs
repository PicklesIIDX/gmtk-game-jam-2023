using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatScreen : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private VisualElement _textArea;
    [SerializeField] private bool flagSpeakSequence = false;
    [SerializeField] private Conversation _currentConversation;
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
        var textEntry = new Label();
        textEntry.AddToClassList("chat-text");
        _textArea.Insert(0, textEntry);
        int index = 0;
        while (index < line.Length)
        {
            index++;
            textEntry.text = line.Substring(0, index);
            yield return new WaitForSeconds(characterDelay);
        }
        
    }
}
