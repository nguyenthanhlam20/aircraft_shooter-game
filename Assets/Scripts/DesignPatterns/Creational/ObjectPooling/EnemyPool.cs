using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private int initialPoolSize = 16;
    private Transform _t;

    private void Awake()
    {
        _t = transform;
        InitialObjectPool();
    }

    private void Start()
    {
        GoOut();
    }
    private void GoOut()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x - 1.5f, transform.position.y), 0.2f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => GoIn());
    }

    private void GoIn()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x + 0.5f, transform.position.y), 2f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => MoveRight());
    }


    private void MoveRight()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x + 1f, transform.position.y), 3f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => MoveDown());
    }

    private void MoveDown()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x - 1f, transform.position.y - 1f), 3f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => MoveUp());
    }

    private void MoveUp()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x + 1f, transform.position.y + 1f), 3f)
              .setEase(LeanTweenType.easeOutQuad)
              .setOnComplete(() => MoveLeft());
    }

    private void MoveLeft()
    {
        LeanTween.moveLocal(gameObject, new Vector2(transform.position.x - 1f, transform.position.y), 3f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => StandStill());
    }

 
    private void StandStill()
    {
        LeanTween.moveLocal(gameObject, new Vector2(0f, 0f), 3f)
         .setEase(LeanTweenType.easeOutQuad)
         .setOnComplete(() => MoveRight());
    }




    private void InitialObjectPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, _t);
            _enemies.Add(enemy);
        }
    }

    public Enemy GetPooledEnemy()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        return null;
    }

    public void PoolEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }


    public List<Enemy> GetEnemies()
    {
        return _enemies;
    }
}
