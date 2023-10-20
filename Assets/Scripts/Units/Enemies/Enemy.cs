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
    private WaitForSeconds _flashDelay;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _flashDelay = new WaitForSeconds(0.083f);
    }
    public int Health { get { return _health; } set { _health = value; } }
    public bool IsInvincible { get { return _isInvincible; } set { _isInvincible = value; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

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
            yield return _flashDelay;

            // Enable the sprite renderer
            _sr.enabled = true;
            yield return _flashDelay;
        }

        yield return new WaitForSeconds(0.2f);

        // After flashing is finished, disable invincibility mode
        IsInvincible = false;
    }

}
