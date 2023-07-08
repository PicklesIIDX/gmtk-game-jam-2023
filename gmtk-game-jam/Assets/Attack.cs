using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public float lifetime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= 0)
        {
            Destroy(gameObject);
            return;
        }
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"I touch {other}");
    }
}
