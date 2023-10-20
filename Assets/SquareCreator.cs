using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCreator : MonoBehaviour
{
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;

    private EnemySpawner enemySpawn;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawner>();  
    }
    public void MakeASquare()
    {
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                enemySpawn.MoveEnemyToTarget(index++, row, col + rows - 1);
            }
        }
    }
}
