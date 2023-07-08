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
	
	
	public static Vector3 FindNewPosition()
	{
		var player = FindPlayer();
		return player ? player.transform.position : RandomPositionOnScreen();
	}

	public static GameObject FindPlayer()
	{
		return GameObject.FindWithTag("Player");
	}
}