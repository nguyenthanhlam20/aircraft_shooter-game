using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 8f;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("run here");
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!enemy.IsInvincible)
            {
                enemy.GetHit(_damage);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

}