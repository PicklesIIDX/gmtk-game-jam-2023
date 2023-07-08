using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] public GameObject target;
    [SerializeField] public float range = 2.0f;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (_rigidbody2D)
            {
                var distance = Vector2.Distance(target.transform.position, transform.position);
                if (distance > range)
                {
                    _rigidbody2D.velocity = ((target.transform.position - transform.position).normalized);
                }
                else
                {
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
        }    
    }
}
