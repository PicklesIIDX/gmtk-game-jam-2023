using UnityEngine;

public abstract class PositionGetter
{
	public static Vector3 RandomNearbyPosition(Vector3 currentPosition)
	{
		var low = Random.Range(0, 2) > 1;
		var x = low ? Random.Range(-6, -1) : Random.Range(1, 6);
		low = Random.Range(0, 2) > 1;
		var y = low ? Random.Range(-6, -1) : Random.Range(1, 6);
		var randomNearbyPosition = new Vector3(x, y) + currentPosition;
		return randomNearbyPosition;
	}
	
	
	public static Vector3 FindNewPositionNearHeroWithinRange(Vector3 currentPosition, float maxRange)
	{
		var player = FindPlayer();
		if (player)
		{
			if (Vector3.Distance(player.transform.position, currentPosition) > maxRange)
			{
				return RandomNearbyPosition(currentPosition);
			}
			return player.transform.position;
		}
		return RandomNearbyPosition(currentPosition);
	}

	public static Vector3 FindPositionAwayFromHero(Vector3 currentPosition, float distance)
	{
		var player = FindPlayer();
		if (player)
		{
			var vectorToPlayer = player.transform.position - currentPosition;
			var perpendicular = Vector3.Cross(vectorToPlayer, Vector3.forward).normalized;
			return player.transform.position + vectorToPlayer + perpendicular * Random.Range(-distance, distance);
		}
		return RandomNearbyPosition(currentPosition);
	}

	public static GameObject FindPlayer()
	{
		return GameObject.FindWithTag("Player");
	}
}