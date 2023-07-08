using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class RepossessionScreen : MonoBehaviour
{

    [SerializeField] private UIDocument document;
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
        //todo: get hero inventory
        var inventory = hero.GetComponentInChildren<Inventory>();
        StartCoroutine(SlowDownTime(inventory));
    }

    [SerializeField] private AnimationCurve slowdownCurve;
    private IEnumerator SlowDownTime(Inventory inventory)
    {
        var time = 0f;
        var lastFrameTime = Time.realtimeSinceStartup;
        var slowdownDuration = slowdownCurve[slowdownCurve.length-1].time;
        while (time < slowdownDuration)
        {
            var delta = Time.realtimeSinceStartup - lastFrameTime;
            lastFrameTime = Time.realtimeSinceStartup;
            time += delta;
            var progress = time;
            Time.timeScale = slowdownCurve.Evaluate(progress);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        OpenPossesionScreen(inventory.GetInventory()); 
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
