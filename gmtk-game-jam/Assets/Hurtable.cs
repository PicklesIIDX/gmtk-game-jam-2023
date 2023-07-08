
using UnityEngine;
using UnityEngine.Events;

public class Hurtable : MonoBehaviour
{
	[SerializeField] public float hp = 1.0f;
	// Start is called before the first frame update

	[SerializeField] public UnityEvent<GameObject> onZero;

	public void Hurt(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			onZero.Invoke(gameObject);
			Destroy(gameObject);
		}
	}
}
