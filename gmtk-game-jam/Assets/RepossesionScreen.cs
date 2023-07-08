using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepossesionScreen : MonoBehaviour
{

    [SerializeField] private UIDocument document;

    [SerializeField] private GameObject newHero;

    [SerializeField] private GameObject enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        OpenPossesionScreen(GetInventory());
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
            new ItemSelection() { Item = "SWORD" },
            new ItemSelection() { Item = "KEY" },
            new ItemSelection() { Item = "BOOTS" },
        };
    }

    private void OnSelectItem(ClickEvent clickEvent, ItemSelection evt)
    {
        Debug.Log($"selected {evt.Item}");
        var newHeroInstance = GameObject.Instantiate(newHero);
        newHeroInstance.transform.position = new Vector3(Random.Range(-1,1), Random.Range(-1,1));
        newHeroInstance.GetComponent<Hurtable>().onZero.AddListener(OnHeroDeath);
        var newEnemyInstance = GameObject.Instantiate(enemy);

        var randomPositionOnScreen = PositionGetter.RandomPositionOnScreen();
        newEnemyInstance.transform.position = randomPositionOnScreen;
        document.rootVisualElement.visible = false;
    }

    private void OnHeroDeath(GameObject hero)
    {
        //todo: get hero inventory
        OpenPossesionScreen(GetInventory());   
    }
}

internal struct ItemSelection
{
    public string Item;
}
