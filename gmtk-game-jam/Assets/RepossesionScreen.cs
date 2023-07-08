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

        Time.timeScale = 1.0f;
    }

    private void OnHeroDeath(GameObject hero)
    {
        //todo: get hero inventory
        StartCoroutine(SlowDownTime());
    }

    [SerializeField] private AnimationCurve slowdownCurve;
    private IEnumerator SlowDownTime()
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
        OpenPossesionScreen(GetInventory()); 
    }
}

internal struct ItemSelection
{
    public string Item;
}
