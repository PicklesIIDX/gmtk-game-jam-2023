using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroScreen : MonoBehaviour
{
    [SerializeField] private UnityEvent onIntroComplete;
    // Start is called before the first frame update
    void Start()
    {
        onIntroComplete.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
