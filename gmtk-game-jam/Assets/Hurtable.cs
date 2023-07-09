
using UnityEngine;
using UnityEngine.Events;

public class Hurtable : MonoBehaviour
{
	[SerializeField] public float hp = 1.0f;
	// Start is called before the first frame update

	[SerializeField] public UnityEvent<GameObject> onZero;

	public AudioSource AudioSource;

	public void Hurt(float damage)
	{
		hp -= damage;
		HurtNoise();
		if (hp <= 0)
		{
			onZero.Invoke(gameObject);
			Destroy(gameObject);
		}
	}

	public void HurtNoise()
	{
		AudioSource.Play();
	}
}
