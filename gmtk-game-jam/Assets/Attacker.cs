using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
	[SerializeField] public GameObject attackObject;
	[SerializeField] private bool attackReady = false;
	[SerializeField] private Timer timer;

	private void Start()
	{
		timer.onComplete.AddListener(OnTimerComplete);
	}

	private void OnTimerComplete()
	{
		attackReady = true;
	}

	public void Attack(Vector3 position, Quaternion rotation)
	{
		if (!attackReady)
		{
			return;
		}
		if (attackObject)
		{
			attackReady = false;
			var attackInstance = Instantiate(attackObject);
			attackInstance.transform.position = position;
			attackInstance.transform.rotation = rotation;
		}
	}
}