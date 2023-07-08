using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepossesionScreen : MonoBehaviour
{

    [SerializeField] private UIDocument document;

    [SerializeField] private GameObject newHero;
    // Start is called before the first frame update
    void OnEnable()
    {
        var button = document.rootVisualElement.Q<Button>("item1");
        button.RegisterCallback<ClickEvent>(Close);
        button = document.rootVisualElement.Q<Button>("item2");
        button.RegisterCallback<ClickEvent>(Close);
    }

    private void Close(ClickEvent _)
    {
        Debug.Log("selected item 1");
        var newHeroInstance = GameObject.Instantiate(newHero);
        newHeroInstance.transform.position = new Vector3(0, 0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
