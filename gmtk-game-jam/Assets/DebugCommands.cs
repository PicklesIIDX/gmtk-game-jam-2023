using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommands : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			GameObject.FindWithTag("Player").GetComponent<Hurtable>().Hurt(9999);
		}
	}
}
