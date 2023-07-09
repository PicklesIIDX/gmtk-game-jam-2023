using UnityEngine;

public abstract class PositionGetter
{
	private static Vector2[] directions = new[]
	{
		new Vector2(0, 1),
		new Vector2(1, 1),
		new Vector2(1, 0),
		new Vector2(1, -1),
		new Vector2(0, -1),
		new Vector2(-1, -1),
		new Vector2(-1, 0),
		new Vector2(-1, 1),
	};
	public static Vector3 RandomNearbyPosition(Vector2 currentPosition, float innerMin, float outerMax)
	{
		var startingOffset = Random.Range(0, directions.Length);
		for (int i = 0; i < directions.Length; i++)
		{
			var randomDirection = directions[(i+startingOffset)%directions.Length];
			var randomNearbyPosition = currentPosition + (randomDirection * Random.Range(innerMin, outerMax));
			var distance = Vector3.Distance(currentPosition, randomNearbyPosition);
			var filter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.NameToLayer("enemy"),  };
			RaycastHit2D[] results = new RaycastHit2D[1];
			Physics2D.Raycast(currentPosition + randomDirection, randomDirection, filter, results, distance * 2);
			if(results[0].distance < distance && results[0].distance > 2)
			{
				var contactPoint = results[0].point - randomDirection * 0.5f;
				Debug.Log($"adjusted position to hit point {contactPoint}");
				return contactPoint;
			}
			else if(results[0].distance >= distance)
			{ 
				return randomNearbyPosition;
			}
		}
		Debug.LogError($"failed to find position after {directions.Length} attempts");
		return currentPosition;
	}
	
	
	public static Vector3 FindNewPositionNearHeroWithinRange(Vector3 currentPosition, float maxRange)
	{
		var player = FindPlayer();
		if (player)
		{
			if (Vector3.Distance(player.transform.position, currentPosition) > maxRange)
			{
				return RandomNearbyPosition(currentPosition, 1, 6);
			}
			return player.transform.position;
		}
		return RandomNearbyPosition(currentPosition, 1, 6);
	}

	public static Vector3 FindPositionAwayFromHero(Vector3 currentPosition, float distance, float maxRange)
	{
		var player = FindPlayer();
		if (player)
		{
			if (Vector3.Distance(player.transform.position, currentPosition) > maxRange)
			{
				return RandomNearbyPosition(currentPosition, 1, 6);
			}
			var vectorToPlayer = player.transform.position - currentPosition;
			var perpendicular = Vector3.Cross(vectorToPlayer, Vector3.forward).normalized;
			return player.transform.position + vectorToPlayer + perpendicular * Random.Range(-distance, distance);
		}
		return RandomNearbyPosition(currentPosition, 1, 6);
	}

	public static GameObject FindPlayer()
	{
		return GameObject.FindWithTag("Player");
	}
}