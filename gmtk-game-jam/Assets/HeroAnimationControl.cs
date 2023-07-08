using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationControl : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Animator Animator;

    public void UpdateAnimator(Vector2 movementDirection)
    {
        Animator.SetFloat("speed", movementDirection.magnitude);
        Sprite.flipX = movementDirection.x < 0f;
    }
}
