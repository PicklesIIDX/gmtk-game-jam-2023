using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    [SerializeField] private TimeSlowAnimator timeSlowAnimator;

    [SerializeField] private UnityEvent onVictory;
    // Start is called before the first frame update
    void Start()
    {
        document.rootVisualElement.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Show")]
    public void ShowVictory()
    {
        StartCoroutine(timeSlowAnimator.SlowDownTime(() =>
        {
            onVictory.Invoke();
            document.rootVisualElement.visible = true;
        }));
    }
}
