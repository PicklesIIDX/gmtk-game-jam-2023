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
            AddLine(chatEntry.Speaker, chatEntry.Line);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void AddLine(string speaker, string line)
    {
        var textEntry = new Label(line);
        textEntry.style.whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.Normal);
        _textArea.Insert(0, textEntry);
    }
}
