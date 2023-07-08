namespace DefaultNamespace
{
	using UnityEngine;

	public class ItemDetails : MonoBehaviour
	{
		public string ItemName;

		public ItemSelection ToItemSelection()
		{
			return new ItemSelection()
			{
				Item = ItemName,
				Position = transform.position
			};
		}
	}
}