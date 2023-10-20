using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _explosionEffect;
    [SerializeField] private AudioClip _gunFireEffect;
    
    public void PlayExplosionSound()
    {
        AudioManager.Instance.PlayEffect(_explosionEffect);
    }

    public void PlayGunFireSound()
    {
        AudioManager.Instance.PlayEffect(_gunFireEffect);
    }

}
