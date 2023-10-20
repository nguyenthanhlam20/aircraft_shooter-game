using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private GameObject gridCellPrefab; // Assign your content prefab in the Inspector.
    [SerializeField] private float flySpeed = 2.0f; // Adjust the speed at which enemies fly in.
    [SerializeField] private int gridRows = 5; // Adjust the speed at which enemies fly in.
    [SerializeField] private int gridColumns = 9; // Adjust the speed at which enemies fly in.
    [SerializeField] private int enemyNumber = 16; // Adjust the speed at which enemies fly in.

    private List<Enemy> enemies = new List<Enemy>();
    private GameObject[,] grid;

    private float cellWidth = 2.0f; // Adjust the width of each cell.
    private float cellHeight = 2.0f; // Adjust the height of each cell.

    private void Awake()
    {
        Vector2 spriteSize = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size;

        cellWidth = spriteSize.x;
        cellHeight = spriteSize.y;
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
        transform.position = new Vector2(-1.95f, 3.5f);
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MakeASquare();

            yield return new WaitForSeconds(5f);
            MakeAnEllipse();

            yield return new WaitForSeconds(5f);
            MakeATriangle();

            yield return new WaitForSeconds(5f);
            MakeARectangle();

            yield return new WaitForSeconds(1f);

            DisableEnemyInvincible();
        }

    }

    private void DisableEnemyInvincible()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.IsInvincible = false;
        }
    }

    private void MakeASquare()
    {
        int rows = 4;
        int columns = 4;
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                MoveEnemyToTarget(enemies[index++], grid[row, col + rows - 1].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));

            }
        }
    }

    private void MoveEnemyToTarget(Enemy enemy, Vector2 targetPosition, float delayTime)
    {
        LeanTween.moveLocal(enemy.gameObject, targetPosition, flySpeed).setDelay(delayTime).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {

        });
    }

    private void MakeARectangle()
    {
        int rows = 3;
        int cols = 7;
        int index = 0;

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int colIndex = 0; colIndex < cols; colIndex++)
            {
                if (rowIndex != 1)
                {
                    MoveEnemyToTarget(enemies[index++], grid[rowIndex, colIndex + 1].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));
                }
                else
                {
                    if (colIndex == 0 || colIndex == cols - 1)
                    {
                        MoveEnemyToTarget(enemies[index++], grid[rowIndex, colIndex + 1].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));

                    }
                }
            }
        }

    }

    private void MakeATriangle()
    {
        int rows = 5;
        int cols = 9;
        int index = 0;
        int center = cols / 2;

        MoveEnemyToTarget(enemies[index++], grid[0, center].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));

        for (int rowIndex = 1; rowIndex < rows - 1; rowIndex++)
        {
            int left = center - rowIndex;
            int right = center + rowIndex;

            if (index <= enemies.Count)
            {
                MoveEnemyToTarget(enemies[index++], grid[rowIndex, left].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));
                MoveEnemyToTarget(enemies[index++], grid[rowIndex, right].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));

            }
        }

        for (int colIndex = 0; colIndex < cols; colIndex++)
        {
            MoveEnemyToTarget(enemies[index++], grid[rows - 1, colIndex].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));
        }

    }



    private void MakeAnEllipse()
    {
        int rows = 5;
        int cols = 7;
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (IsAsterisk(i, j, rows, cols))
                {
                    if (index == 0 || index == enemyNumber - 1)
                    {
                        MoveEnemyToTarget(enemies[index++], grid[i, j].transform.position + (new Vector3(cellWidth / 2, 0f, 0f)), UnityEngine.Random.Range(-0.5f, 0.5f));

                    }
                    else
                    {
                        MoveEnemyToTarget(enemies[index++], grid[i, j].transform.position, UnityEngine.Random.Range(-0.5f, 0.5f));

                    }

                }
            }
        }
    }
    static bool IsAsterisk(int row, int col, int rows, int cols)
    {

        int[,] asteriskPositions = {
            {0, 3},
            {1, 2},
            {1, 3},
            {1, 4},
            {1, 5},
            {2, 1},
            {2, 2},
            {2, 3},
            {2, 4},
            {2, 5},
            {2, 6},
            {3, 2},
            {3, 3},
            {3, 4},
            {3, 5},
            {4, 3}
        };
        for (int i = 0; i < asteriskPositions.GetLength(0); i++)
        {
            if (row == asteriskPositions[i, 0] && col == asteriskPositions[i, 1])
            {
                return true;
            }
        }

        return false;
    }

}
