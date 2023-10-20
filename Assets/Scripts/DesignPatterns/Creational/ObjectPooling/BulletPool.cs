using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<Bullet> _bullets = new List<Bullet>();
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private int initialPoolSize = 3;
    private Transform _t;

    private void Awake()
    {
        _t = transform;
        InitialObjectPool();
    }


    private void InitialObjectPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, _t);
            _bullets.Add(bullet);
        }
    }

    public Bullet GetPooledBullet()
    {
        foreach (Bullet bullet in _bullets)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    public void PoolBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}