using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
	[SerializeField] public GameObject attackObject;

	public void Attack(Vector3 position, Quaternion rotation, int layer)
	{
		var attackInstance = Instantiate(attackObject);
		attackInstance.transform.position = position;
		attackInstance.transform.rotation = rotation;
		attackInstance.layer = layer;
	}
}