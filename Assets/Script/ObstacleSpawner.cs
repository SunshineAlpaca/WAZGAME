using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // 障碍物预设数组
    public float initialDelay = 1.5f;  // 开局延迟生成障碍物的时间
    public float spawnInterval = 2.0f;  // 初始生成间隔
    public float minY = -5.0f;  // 障碍物生成的最小Y坐标
    public float maxY = 5.0f;   // 障碍物生成的最大Y坐标
    public float minGapX = 3.0f;  // 两个障碍物在X轴的最小间隔
    public float minX = -5.0f;  // 障碍物生成的最小X坐标
    public float maxX = 5.0f;   // 障碍物生成的最大X坐标
    public float decreaseInterval = 0.1f;  // 垂直间隔减少的量
    public float minSpawnInterval = 0.5f;  // 最小生成间隔
    public float initialVerticalGap = 10.0f;  // 初始垂直间隔
    public float minVerticalGap = 3.0f;  // 最小垂直间隔
    public List<Vector2>obstaclePositions = new List<Vector2>();

    private float currentVerticalGap;  // 当前垂直间隔
    private float nextSpawnY;  // 下一个障碍物生成的Y位置

    private Coroutine spawnCoroutine;


    void Start()
    {
        nextSpawnY = transform.position.y;
        currentVerticalGap = initialVerticalGap;
        StartCoroutine(SpawnObstacles());
    }


    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // 在Y轴的当前位置生成障碍物
            GenerateObstacles();

            // 更新Y位置，根据当前垂直间隔进行调整
            nextSpawnY -= currentVerticalGap;

            // 减少生成间隔和垂直间隔，以增加游戏难度
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - decreaseInterval);
            currentVerticalGap = Mathf.Max(minVerticalGap, currentVerticalGap - decreaseInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void GenerateObstacles()
    {
        float xPosition1 = Random.Range(minX, maxX);
        SpawnObstacle(xPosition1, nextSpawnY);

        // 随机决定是否在同一Y轴上生成第二个障碍物
        if (Random.value < 0.5f)
        {
            float xPosition2;
            // 确保第二个障碍物与第一个间有足够的间隔
            do
            {
                xPosition2 = Random.Range(minX, maxX);
            }
            while (Mathf.Abs(xPosition2 - xPosition1) < minGapX);

            SpawnObstacle(xPosition2, nextSpawnY);
        }
    }

    void SpawnObstacle(float x, float y)
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        Vector3 spawnPosition = new Vector3(x, y, 0);
        Instantiate(obstaclePrefabs[index], spawnPosition, Quaternion.identity);
    }


}