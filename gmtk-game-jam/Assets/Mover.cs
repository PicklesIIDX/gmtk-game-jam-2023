
using System;
using UnityEngine;

public class Mover : MonoBehaviour
{

	[SerializeField] public GameObject target;
	[SerializeField] public float range = 2.0f;
	[SerializeField] private Rigidbody2D body2D;
	[SerializeField] private float speed = 1.0f;
	private Vector3 _targetPosition;
	[SerializeField] private Timer findNewPositionTimer;

	private void Start()
	{
		findNewPositionTimer.onComplete.AddListener(FindNewPosition);
	}

	public bool Move()
	{
		if (body2D)
		{
			if (AtTarget(_targetPosition))
			{
				body2D.velocity = Vector2.zero;
				return true;
			}
			else
			{
				var targetVector = (_targetPosition - transform.position).normalized;
				body2D.velocity = (targetVector * speed);
				transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
			}
		}
		return false;
	}

	private void FindNewPosition()
	{
		var player = GameObject.FindWithTag("Player");
		_targetPosition = player ? player.transform.position : PositionGetter.RandomPositionOnScreen();
	}

	public bool AtTarget(Vector3 targetPosition)
	{
		var distance = Vector2.Distance(targetPosition, transform.position);
		return distance <= range;
	}
}