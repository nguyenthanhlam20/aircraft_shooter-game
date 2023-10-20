using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private float fireDelay = 0.1f;

    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private AudioClip _explosionEffect;
    [SerializeField] private AudioClip _gunFireEffect;

    private WaitForSeconds fireWaiter;
    private InputManager inputManager;

    private Rigidbody2D rb;
    public float speed = 2f;
    public Animator anim;

    private bool _isDead = false;

    public bool IsDead { get { return _isDead; } }

    Vector2 moveDir;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fireWaiter = new WaitForSeconds(fireDelay);

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (!_isDead)
        {
            moveDir = inputManager.MoveInput.normalized;

            Vector2 newPosition = rb.position + (moveDir * speed * Time.fixedDeltaTime);

            // Define the boundaries of the screen in world coordinates
            float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float screenHeight = Camera.main.orthographicSize;

            // Calculate the clamped position
            newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth, screenWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight, screenHeight);

            rb.MovePosition(newPosition);
        }
    }





    private IEnumerator Fire()
    {
        while (true)
        {
            if (_isDead) break;

            yield return fireWaiter;
            AudioManager.Instance.PlayEffect(_gunFireEffect);
            Bullet bullet = bulletPool.GetPooledBullet();

            bullet.gameObject.transform.position = transform.position;

        }
    }

    public void Explode()
    {
        ScreenShakeManager.Instance.StartShake(0.2f, 0.1f);
        AudioManager.Instance.PlayEffect(_explosionEffect);

        _isDead = true;
        anim.SetTrigger("Dead");
    }

    public void ShowGameOverPopup()
    {
        gameObject.SetActive(false);
        PopupManager.Instance.ShowGameOverPopup();
    }

}
