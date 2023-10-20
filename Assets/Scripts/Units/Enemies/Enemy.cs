using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private bool _isInvincible = true;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private AudioClip _explosionEffect;

    private SpriteRenderer _sr;
    private Animator _anim;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    public int Health { get { return _health; } set { _health = value; } }
    public bool IsInvincible { get { return _isInvincible; } set { _isInvincible = value; } }

    public void GetHit(int damage)
    {
        if (_isDead == false)
        {
            IsInvincible = true;
            if (_health - damage > 0)
            {
                _health--;
                StartCoroutine(FlashAfterHit());
            }
            else
            {
                _isDead = true;
                ScreenShakeManager.Instance.StartShake(0.1f, 0.05f);
                AudioManager.Instance.PlayEffect(_explosionEffect);
                ScoreManager.Instance.UpdateScore(1);
                Explode();
            }
        }
    }

    public void Explode()
    {
        _anim.SetTrigger("Dead");
    }

    public void HideEnemy()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator FlashAfterHit()
    {
        // Flash 10 times
        for (int i = 0; i < 5; i++)
        {
            // Disable the sprite renderer
            _sr.enabled = false;
            yield return new WaitForSeconds(0.0833f);

            // Enable the sprite renderer
            _sr.enabled = true;
            yield return new WaitForSeconds(0.0833f);
        }

        yield return new WaitForSeconds(0.2f);

        // After flashing is finished, disable invincibility mode
        IsInvincible = false;
    }

}
