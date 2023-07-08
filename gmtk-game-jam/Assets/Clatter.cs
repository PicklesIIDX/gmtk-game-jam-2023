using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clatter : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] public bool queueAnimate = false;
    [SerializeField] public float gravity = 1f;

    [SerializeField] public Vector2[] forces;
    [SerializeField] public float[] delays;
    [SerializeField] public float[] soundVolume;

    [SerializeField] public AudioClip[] clatterSounds;

    [SerializeField] public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (queueAnimate)
        {
            queueAnimate = false;
            StartCoroutine(Animate());
        }
    }

    IEnumerator Animate()
    {
        _rigidbody2D.gravityScale = gravity;
        for (int i = 0; i < forces.Length; i++)
        {
            _rigidbody2D.AddForce(forces[i]);
            PlayRandomSound(soundVolume[i]);
            yield return new WaitForSeconds(delays[i]);
        }
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0;
    }

    private void PlayRandomSound(float volume)
    {
        var soundIndex = Random.Range(0, clatterSounds.Length);
        AudioSource.clip = clatterSounds[soundIndex];
        AudioSource.volume = volume;
        AudioSource.Play();
    }
}
