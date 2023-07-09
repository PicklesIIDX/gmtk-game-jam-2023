using UnityEngine;

public abstract class PositionGetter
{
	public static Vector3 RandomNearbyPosition(Vector2 currentPosition, float innerMin, float outerMax)
	{
		int attempts = 20;
		for (int i = 0; i < attempts; i++)
		{
			var low = Random.Range(0, 2) > 1;
			var x = low ? Random.Range(-outerMax, -innerMin) : Random.Range(innerMin, outerMax);
			low = Random.Range(0, 2) > 1;
			var y = low ? Random.Range(-outerMax, -innerMin) : Random.Range(innerMin, outerMax);
			var randomDirection = new Vector2(x, y).normalized;
			var randomNearbyPosition = currentPosition + randomDirection;
			var distance = Vector3.Distance(currentPosition, randomNearbyPosition);
			var filter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.NameToLayer("enemy") };
			RaycastHit2D[] results = new RaycastHit2D[1];
			Physics2D.Raycast(currentPosition, randomDirection, filter, results, distance);
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
		Debug.LogError($"failed to find position after {attempts} attempts");
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