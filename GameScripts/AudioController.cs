using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("------------- AUDIO SOURCE -------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------- AUDIO CLIP -------------")]
    public AudioClip background;
    public AudioClip dash;
    public AudioClip hitWall;
    public AudioClip openChest;
    public AudioClip usePotion;
    public AudioClip potionEnd;
    public AudioClip grabKeys;
    public AudioClip openDoor;
    public AudioClip takeDamage;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
