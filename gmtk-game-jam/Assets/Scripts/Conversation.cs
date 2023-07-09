
	using System;
	using UnityEngine;

	[Serializable]
	public struct ChatEntry
	{
		public string Speaker;
		public string Line;
	}
	
	[CreateAssetMenu(fileName = "conversation-01", menuName = "Conversation", order = 0)]
	public class Conversation : ScriptableObject
	{
		[SerializeField] public ChatEntry[] ChatEntries;
	}