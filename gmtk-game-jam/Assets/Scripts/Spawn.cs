namespace DefaultNamespace
{
	using UnityEngine;

	public class Spawn : MonoBehaviour
	{
		[SerializeField] public GameObject objectToSpawn;

		public void Instantiate()
		{
			GameObject.Instantiate(objectToSpawn);
		}
	}
}