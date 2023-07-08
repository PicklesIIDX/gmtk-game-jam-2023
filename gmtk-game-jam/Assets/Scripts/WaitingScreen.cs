namespace DefaultNamespace
{
	using System;
	using System.Collections;
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.Serialization;
	using UnityEngine.UI;
	using UnityEngine.UIElements;
	using Random = UnityEngine.Random;

	public class WaitingScreen : MonoBehaviour
	{
		[SerializeField] private UIDocument document;
		[SerializeField] private UnityEvent onWaitingComplete;

		[SerializeField] private Spawn _heroSpawn;

		private void Start()
		{
			_heroSpawn.onSpawn.AddListener(OnHeroSpawn);
			document.rootVisualElement.visible = false;
			_textElement = document.rootVisualElement.Q<TextElement>("waiting-for");
		}

		private void OnHeroSpawn(GameObject heroObject)
		{
			var movement = heroObject.GetComponentInChildren<Movement>();
			if (movement)
			{
				movement.isSword = itemSelection.IsSword();
				movement.legControl = itemSelection.IsBoots();
			}
			heroObject.transform.position = itemSelection.Position;
		}

		[SerializeField] private AnimationCurve animationCurve;
		private TextElement _textElement;
		private ItemSelection itemSelection;

		private IEnumerator WaitForPickup()
		{
			document.rootVisualElement.visible = true;
			yield return new WaitForSecondsRealtime(0.5f);
			int targetTime = Random.Range(1, 1000000);
			var time = 0f;
			var lastFrameTime = Time.realtimeSinceStartup;
			var slowdownDuration = animationCurve[animationCurve.length-1].time;
			while (time < slowdownDuration)
			{
				var delta = Time.realtimeSinceStartup - lastFrameTime;
				lastFrameTime = Time.realtimeSinceStartup;
				time += delta;
				var waitedDays = (int)(targetTime * animationCurve.Evaluate(time));
				if (waitedDays > 730)
				{
					_textElement.text = $"{waitedDays/365} YEARS";
				}
				else
				{
					_textElement.text = $"{waitedDays} DAYS";
				}
				yield return null;
			}
			yield return new WaitForSecondsRealtime(0.5f);
			document.rootVisualElement.visible = false;
			onWaitingComplete.Invoke();
		}

		public void Possession(ItemSelection itemSelection)
		{
			this.itemSelection = itemSelection;
			StartCoroutine(WaitForPickup());
		}
	}
}