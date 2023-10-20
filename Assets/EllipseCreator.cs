using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseCreator : MonoBehaviour
{
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 7;
    private EnemySpawner enemySpawn;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawner>();
    }
    public void MakeAnEllipse()
    {
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (IsEllipsePoint(i, j))
                {
                    if (index == 0 || index == enemySpawn.EnemyNumber - 1)
                    {
                        enemySpawn.MoveEnemyToTargetCenter(index++, i, j);

                    }
                    else
                    {
                        enemySpawn.MoveEnemyToTarget(index++, i, j);

                    }

                }
            }
        }
    }
    static bool IsEllipsePoint(int row, int col)
    {

        int[,] ellipsePoints = {
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
        for (int i = 0; i < ellipsePoints.GetLength(0); i++)
        {
            if (row == ellipsePoints[i, 0] && col == ellipsePoints[i, 1])
            {
                return true;
            }
        }

        return false;
    }
}
