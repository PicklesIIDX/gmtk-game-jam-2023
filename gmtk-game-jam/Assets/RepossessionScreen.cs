using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class RepossessionScreen : MonoBehaviour
{

    [SerializeField] private UIDocument document;
    [SerializeField] private TimeSlowAnimator timeSlowAnimator;
    [SerializeField] private UnityEvent<ItemSelection> onSelectPossession;

    [SerializeField] private string startingItem = "SWORD";
    // Start is called before the first frame update
    void OnEnable()
    {
        document.rootVisualElement.visible = false;
        onSelectPossession.Invoke(new ItemSelection(){Item = startingItem, Position = transform.position});
    }

    private void OpenPossesionScreen(ItemSelection[] inventory)
    {
        
        var buttonHolder = document.rootVisualElement.Q<VisualElement>("button-holder");
        buttonHolder.Clear();
        foreach (var selection in inventory)
        {
            var itemButton = new Button
            {
                text = selection.Item,
            };
            itemButton.AddToClassList("chat-text");
            itemButton.style.backgroundColor = new StyleColor(new Color(.1f, .1f, .1f));
            itemButton.RegisterCallback<ClickEvent, ItemSelection>(OnSelectItem, selection);
            buttonHolder.Add(itemButton);
        }
        document.rootVisualElement.visible = true;
    }

    private ItemSelection[] GetInventory()
    {
        return new[]
        {
            new ItemSelection() { Item = "SWORD", Position = Vector3.zero},
            new ItemSelection() { Item = "BOOTS", Position = Vector3.zero},
        };
    }

    private void OnSelectItem(ClickEvent clickEvent, ItemSelection evt)
    {
        Debug.Log($"selected {evt.Item}");
        document.rootVisualElement.visible = false;

        Time.timeScale = 1.0f;
        onSelectPossession.Invoke(evt);
    }

    public void ListenToHeroDeath(GameObject heroInstance)
    {
        heroInstance.GetComponent<Hurtable>().onZero.AddListener(OnHeroDeath);
    }

    private void OnHeroDeath(GameObject hero)
    {
        var inventory = hero.GetComponentInChildren<Inventory>();
        StartCoroutine(timeSlowAnimator.SlowDownTime(() =>
        {
            OpenPossesionScreen(inventory.GetInventory());
        }));
    }
    
}

public struct ItemSelection
{
    public string Item;
    public Vector3 Position;

    public bool IsSword()
    {
        return Item == "SWORD";
    }

    public bool IsBoots()
    {
        return Item == "BOOTS";
    }
}
