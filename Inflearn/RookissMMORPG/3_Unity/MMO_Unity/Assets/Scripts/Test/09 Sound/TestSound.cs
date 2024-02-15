using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip clip2;

    private void Start()
    {
        Managers.sound.Play("Bgm/Skyscraper_110 BPM", Define.Sound.Bgm);
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        //AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
        /*
        audio.PlayOneShot(clip);
        audio.PlayOneShot(clip2);
        float lifeTime = Mathf.Max(clip.length, clip2.length);
        GameObject.Destroy(gameObject, lifeTime);
         */
        Managers.sound.Play("UnityChan/univ1046");
        Managers.sound.Play(clip);
    }
}
