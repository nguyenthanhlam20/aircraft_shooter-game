using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCreator : MonoBehaviour
{

    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 9;

    private EnemySpawner enemySpawn;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawner>();
    }
    public void MakeATriangle()
    {
        int index = 0;
        int center = columns / 2;

        enemySpawn.MoveEnemyToTarget(index++, 0, center);

        for (int rowIndex = 1; rowIndex < rows - 1; rowIndex++)
        {
            int left = center - rowIndex;
            int right = center + rowIndex;

            if (index <= enemySpawn.EnemyNumber)
            {
                enemySpawn.MoveEnemyToTarget(index++, rowIndex, left);
                enemySpawn.MoveEnemyToTarget(index++, rowIndex, right);

            }
        }

        for (int colIndex = 0; colIndex < columns; colIndex++)
        {
            enemySpawn.MoveEnemyToTarget(index++, rows - 1, colIndex);
        }

    }
}
