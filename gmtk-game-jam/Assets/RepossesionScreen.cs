using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepossesionScreen : MonoBehaviour
{

    [SerializeField] private UIDocument document;
    // Start is called before the first frame update
    void Start()
    {
        var button = document.rootVisualElement.Q<Button>("item1");
        button?.RegisterCallback<ClickEvent>(_ =>
        {
            Debug.Log("selected item 1");
            document.enabled = false;
        });
        document.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
