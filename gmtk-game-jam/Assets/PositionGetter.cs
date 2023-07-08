using UnityEngine;

public abstract class PositionGetter
{
	public static Vector3 RandomPositionOnScreen()
	{
		var low = Random.Range(0, 2) > 1;
		var x = low ? Random.Range(-6, -1) : Random.Range(1, 6);
		low = Random.Range(0, 2) > 1;
		var y = low ? Random.Range(-6, -1) : Random.Range(1, 6);
		var randomPositionOnScreen = new Vector3(x, y);
		return randomPositionOnScreen;
	}
}