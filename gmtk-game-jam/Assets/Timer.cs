using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[SerializeField] public float cooldown = 1.0f;
	[SerializeField] public float timer = 0.0f;

	[SerializeField] public UnityEvent onComplete;
	public void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer = cooldown;
			onComplete.Invoke();
		}
	}
}