
using System;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
	[SerializeField] public float range = 2.0f;
	[SerializeField] private Rigidbody2D body2D;
	[SerializeField] private float speed = 1.0f;
	public Vector3 TargetPosition;
	public UnityEvent<Vector2> onMove;

	private void Awake()
	{
		body2D.freezeRotation = true;
	}

	public bool Move()
	{
		if (body2D)
		{
			if (AtTarget(TargetPosition))
			{
				body2D.velocity = Vector2.zero;
				onMove.Invoke(body2D.velocity);
				return true;
			}
			else
			{
				var targetVector = (TargetPosition - transform.position).normalized;
				body2D.velocity = (targetVector * speed);
				body2D.rotation = Vector2.Angle(TargetPosition, transform.position);
				//transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
				onMove.Invoke(body2D.velocity);
			}
		}
		return false;
	}

	
	public bool AtTarget(Vector3 targetPosition)
	{
		var distance = Vector2.Distance(targetPosition, transform.position);
		return distance <= range;
	}

	public void Stop()
	{
		body2D.velocity = Vector2.zero;
		onMove.Invoke(body2D.velocity);
	}
	
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(TargetPosition, .1f);
	}
}