namespace DefaultNamespace
{
	using UnityEngine;
	using UnityEngine.Events;

	public class Spawn : MonoBehaviour
	{
		[SerializeField] public GameObject objectToSpawn;
		[SerializeField] public UnityEvent<GameObject> onSpawn;
		[SerializeField] public float innerMin = 1;
		[SerializeField] public float outerMax = 6;

		public void InstantiateAt(Vector3 position)
		{
			var instance = GameObject.Instantiate(objectToSpawn);
			instance.transform.position = position;
			onSpawn.Invoke(instance);
		}
		public void Instantiate()
		{
			InstantiateAt(PositionGetter.RandomNearbyPosition(Vector3.zero, innerMin, outerMax));
		}
	}
}