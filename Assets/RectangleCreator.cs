using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleCreator : MonoBehaviour
{
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 7;
    private EnemySpawner enemySpawn;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawner>();
    }
    public void MakeARectangle()
    {
      
        int index = 0;

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int colIndex = 0; colIndex < columns; colIndex++)
            {
                if (rowIndex != 1)
                {
                    enemySpawn.MoveEnemyToTarget(index++, rowIndex, colIndex + 1);
                }
                else
                {
                    if (colIndex == 0 || colIndex == columns - 1)
                    {
                        enemySpawn.MoveEnemyToTarget(index++, rowIndex, colIndex + 1);

                    }
                }
            }
        }

    }
}
