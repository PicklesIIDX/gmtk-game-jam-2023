using UnityEngine;

public class Hurter : MonoBehaviour
{
    [SerializeField] public float damage = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Hurter {name} touched {other.gameObject.name}");
        var hurtable = other.gameObject.GetComponent<Hurtable>();
        if (hurtable)
        {
            hurtable.Hurt(damage);
        }
    }
}
