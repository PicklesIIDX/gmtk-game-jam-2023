using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBasedOnVector : MonoBehaviour
{
    public SpriteRenderer Sprite;
    
    public void SetFlip(Vector2 movementDirection)
    {
        Sprite.flipX = movementDirection.x < 0f;
    }
}
