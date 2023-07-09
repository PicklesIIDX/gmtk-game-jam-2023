using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroScreen : MonoBehaviour
{
    [SerializeField] private UnityEvent onIntroComplete;
    public Animator animator;
    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimationPart1());
        //onIntroComplete.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopCoroutine(AnimationPart1());
            StopCoroutine(AnimationPart2());
            StopCoroutine(AnimationPart3());
            _audioSource.Stop();
            onIntroComplete.Invoke();
        }
    }
    IEnumerator AnimationPart1()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(8);
        animator.SetTrigger("next");
        StartCoroutine(AnimationPart2());
    }
    IEnumerator AnimationPart2()
    {
        yield return new WaitForSeconds(5);
        animator.SetTrigger("next");
        StartCoroutine(AnimationPart3());

    }
    IEnumerator AnimationPart3()
    {
        yield return new WaitForSeconds(3);
        onIntroComplete.Invoke();
    }

    IEnumerator AnimationWatch()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        onIntroComplete.Invoke();
    }
}
