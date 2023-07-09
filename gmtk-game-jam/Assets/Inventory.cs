using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;
    // Start is called before the first frame update
    public UnityEvent<GameObject> onDrop;
    private ItemDetails[] droppedItems = Array.Empty<ItemDetails>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropAll()
    {
        StartCoroutine(DropSequence());
    }

    private IEnumerator DropSequence()
    {
        droppedItems = new ItemDetails[items.Length];
        for (var i = 0; i < items.Length; i++)
        {
            var item = items[i];
            var instance = GameObject.Instantiate(item);
            instance.transform.position = transform.position;
            droppedItems[i] = instance.GetComponent<ItemDetails>();
            onDrop.Invoke(instance);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    public ItemSelection[] GetInventory()
    {
        var itemSelections = new ItemSelection[droppedItems.Length];
        for (var i = 0; i < droppedItems.Length; i++)
        {
            itemSelections[i] = droppedItems[i].ToItemSelection();
        }
        return itemSelections;
    }
}
