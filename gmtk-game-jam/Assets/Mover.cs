
using UnityEngine;

public class Mover : MonoBehaviour
{

	[SerializeField] public GameObject target;
	[SerializeField] public float range = 2.0f;
	[SerializeField] private Rigidbody2D body2D;
	[SerializeField] private float speed = 1.0f;

	public void Move()
	{
		if (target)
		{
			if (body2D)
			{
				if (AtTarget())
				{
					body2D.velocity = Vector2.zero;
				}
				else
				{
					var targetVector = (target.transform.position - transform.position).normalized;
					body2D.velocity = (targetVector * speed);
					transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
				}
			}
		}
	}

	public bool AtTarget()
	{
		if (!target)
		{
			return false;
		}
		var distance = Vector2.Distance(target.transform.position, transform.position);
		return distance <= range;
	}
}