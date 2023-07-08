using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Mover mover;
	[SerializeField] private Attacker attacker;

	// Update is called once per frame
	void Update()
	{ 
		mover.Move();
		if (mover.AtTarget())
		{
			attacker.Attack(transform.position, transform.rotation, gameObject.layer);
		}
	}
}
