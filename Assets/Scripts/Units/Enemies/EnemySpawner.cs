using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private GameObject gridCellPrefab; 
    [SerializeField] private Shield enemyShield; 
    [SerializeField] private float flySpeed = 2.0f;
    [SerializeField] private int gridRows = 5; 
    [SerializeField] private int gridColumns = 9; 
    [SerializeField] private int _enemyNumber = 16;

    private List<Enemy> enemies = new List<Enemy>();
    private GameObject[,] grid;

    private float cellWidth = 2.0f; 
    private float cellHeight = 2.0f; 


    private SquareCreator squareCreator;
    private EllipseCreator ellipseCreator;
    private RectangleCreator rectangleCreator;
    private TriangleCreator triangleCreator;


    public int EnemyNumber { get { return _enemyNumber; } }

    private void Awake()
    {
        Vector2 spriteSize = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size;
        cellWidth = spriteSize.x;
        cellHeight = spriteSize.y;

        squareCreator = GetComponent<SquareCreator>();
        ellipseCreator = GetComponent<EllipseCreator>();
        rectangleCreator = GetComponent<RectangleCreator>();
        triangleCreator = GetComponent<TriangleCreator>();
    }

    private void Start()
    {
        enemies = enemyPool.GetEnemies();
        RenderCells();

        StartCoroutine(SpawnEnemies());
    }

    private void RenderCells()
    {
        grid = new GameObject[gridRows, gridColumns];

        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                // Instantiate the content prefab and set its position within the grid.
                Vector2 position = new Vector2(col * cellWidth, -row * cellHeight);
                GameObject cell = Instantiate(gridCellPrefab, transform);
                cell.transform.localPosition = position;

                grid[row, col] = cell;
            }
        }
        transform.position = new Vector2(-1.95f, 3f);
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            squareCreator.MakeASquare();

            yield return new WaitForSeconds(5f);
            ellipseCreator.MakeAnEllipse();

            yield return new WaitForSeconds(5f);
            triangleCreator.MakeATriangle();

            yield return new WaitForSeconds(5f);
            rectangleCreator.MakeARectangle();

            yield return new WaitForSeconds(1f);

            HideEnemyShield();
            yield return new WaitForSeconds(5f);

        }

    }

    private void HideEnemyShield()
    {
        if(enemyShield.gameObject.activeSelf)
        {
            StartCoroutine(enemyShield.HideShield());
            foreach (Enemy enemy in enemies)
            {
                enemy.IsInvincible = false;
            }
        }
    }

    public void MoveEnemyToTarget(int enemyIndex, int rowIndex, int colIndex)
    {
        float delayTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        LeanTween.moveLocal(enemies[enemyIndex].gameObject, grid[rowIndex, colIndex].transform.position, flySpeed)
            .setDelay(delayTime)
            .setEase(LeanTweenType.easeInOutExpo);
    }

    public void MoveEnemyToTargetCenter(int enemyIndex, int rowIndex, int colIndex)
    {
        float delayTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        LeanTween.moveLocal(enemies[enemyIndex].gameObject,
            grid[rowIndex, colIndex].transform.position + (new Vector3(cellWidth / 2, 0f, 0f))
            , flySpeed)
            .setDelay(delayTime)
            .setEase(LeanTweenType.easeInOutExpo);
    }

}
