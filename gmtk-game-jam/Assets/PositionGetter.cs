using UnityEngine;

public abstract class PositionGetter
{
	public static Vector3 RandomNearbyPosition(Vector3 currentPosition, float innerMin, float outerMax)
	{
		for (int i = 0; i < 100; i++)
		{
			var low = Random.Range(0, 2) > 1;
			var x = low ? Random.Range(-outerMax, -innerMin) : Random.Range(innerMin, outerMax);
			low = Random.Range(0, 2) > 1;
			var y = low ? Random.Range(-outerMax, -innerMin) : Random.Range(innerMin, outerMax);
			var randomNearbyPosition = new Vector3(x, y) + currentPosition;
			var distance = Vector3.Distance(currentPosition, randomNearbyPosition);
			var filter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.NameToLayer("enemy") };
			RaycastHit2D[] results = new RaycastHit2D[1];
			Physics2D.Raycast(currentPosition, randomNearbyPosition, filter, results, distance);
			if(results[0].distance < distance)
			{
				var contactPoint = results[0].point;
				Debug.Log($"adjusted position to hit point {contactPoint}");
				return contactPoint;
			}
			return randomNearbyPosition;
		}
		Debug.LogError($"failed to find position after 100 attempts");
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

	public static Vector3 FindPositionAwayFromHero(Vector3 currentPosition, float distance)
	{
		var player = FindPlayer();
		if (player)
		{
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