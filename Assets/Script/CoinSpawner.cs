using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // 金币预设
    public ObstacleSpawner obstacleSpawner; // 障碍物生成器引用
    public float spawnRate = 5.0f; // 金币生成的时间间隔，单位为秒
    private float nextSpawnTime = 0; // 下一次生成金币的时间点

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnRate; // 计算下次生成金币的时间
        }
    }

    void SpawnCoin()
    {
        foreach (var pos in obstacleSpawner.obstaclePositions)
        {
            float potentialX = pos.x + 1.5f; // 假设金币在障碍物右侧1.5单位距离处生成

            bool isFree = true;
            foreach (Vector2 obstaclePos in obstacleSpawner.obstaclePositions)
            {
                if (Mathf.Abs(obstaclePos.x - potentialX) < 1.5f) // 检查生成位置是否有足够空间
                {
                    isFree = false;
                    break;
                }
            }

            if (isFree)
            {
                float y = pos.y;
                Instantiate(coinPrefab, new Vector3(potentialX, y, 0), Quaternion.identity);
                break; // 生成一个金币后退出循环
            }
        }
    }
}
