using System.Collections;
using UnityEngine;

public class ShootingEngine : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float fireDelay = 0.1f;
    private Player player;
    private PlayerAudio playerAudio;
    private WaitForSeconds fireWaiter;

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        player = GetComponent<Player>();
        fireWaiter = new WaitForSeconds(fireDelay);
    }

    void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            if (player.IsDead) break;

            yield return fireWaiter;
            playerAudio.PlayGunFireSound();

            Bullet bullet = bulletPool.GetPooledBullet();

            bullet.gameObject.transform.position = transform.position;

        }
    }
}
