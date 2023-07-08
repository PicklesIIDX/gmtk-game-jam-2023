
using UnityEngine;

public class Hurtable : MonoBehaviour
{
	[SerializeField] public float hp = 1.0f;
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
		Debug.Log($"touched by {other}");
	}

	public void Hurt(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}
