using System;
using System.Collections;
using UnityEngine;

public class TimeSlowAnimator : MonoBehaviour
{
	[SerializeField] private AnimationCurve slowdownCurve;

	public IEnumerator SlowDownTime(Action onComplete)
	{
		var time = 0f;
		var lastFrameTime = Time.realtimeSinceStartup;
		var slowdownDuration = slowdownCurve[slowdownCurve.length-1].time;
		while (time < slowdownDuration)
		{
			var delta = Time.realtimeSinceStartup - lastFrameTime;
			lastFrameTime = Time.realtimeSinceStartup;
			time += delta;
			var progress = time;
			Time.timeScale = slowdownCurve.Evaluate(progress);
			yield return null;
		}
		yield return new WaitForSecondsRealtime(0.5f);
		onComplete.Invoke(); 
	}
}