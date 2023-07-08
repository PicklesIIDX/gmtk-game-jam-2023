
using UnityEngine;
using UnityEngine.Events;

public class Hurtable : MonoBehaviour
{
	[SerializeField] public float hp = 1.0f;
	// Start is called before the first frame update

	[SerializeField] public UnityEvent onZero;
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log($"{name} touched by {other.gameObject.name}");
	}

	public void Hurt(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			onZero.Invoke();
			Destroy(gameObject);
		}
	}
}
