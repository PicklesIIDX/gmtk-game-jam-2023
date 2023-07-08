using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{

    [SerializeField] public GameObject target;
    [SerializeField] public float range = 2.0f;
    private Rigidbody2D _rigidbody2D;

    [FormerlySerializedAs("attackPrefab")] [SerializeField] public GameObject attackObject;
    [SerializeField] public float attackCooldown = 1.0f;
    [SerializeField] public float attackTimer = 0.0f;
    
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
                    var targetVector = (target.transform.position - transform.position).normalized;
                    _rigidbody2D.velocity = (targetVector);
                    transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
                }
                else
                {
                    _rigidbody2D.velocity = Vector2.zero;
                    if (attackTimer <= 0)
                    {
                        if (attackObject)
                        {
                            var attackInstance = GameObject.Instantiate(attackObject);
                            attackInstance.transform.position = transform.position;
                            attackInstance.transform.rotation = transform.rotation;
                        }
                        attackTimer = attackCooldown;
                    }
                    attackTimer -= Time.deltaTime;
                }
            }
        }    
    }
}
