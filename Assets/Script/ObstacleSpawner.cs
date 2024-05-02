using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // �ϰ���Ԥ������
    public float initialDelay = 1.5f;  // �����ӳ������ϰ����ʱ��
    public float spawnInterval = 2.0f;  // ��ʼ���ɼ��
    public float minY = -5.0f;  // �ϰ������ɵ���СY����
    public float maxY = 5.0f;   // �ϰ������ɵ����Y����
    public float minGapX = 3.0f;  // �����ϰ�����X�����С���
    public float minX = -5.0f;  // �ϰ������ɵ���СX����
    public float maxX = 5.0f;   // �ϰ������ɵ����X����
    public float decreaseInterval = 0.1f;  // ��ֱ������ٵ���
    public float minSpawnInterval = 0.5f;  // ��С���ɼ��
    public float initialVerticalGap = 10.0f;  // ��ʼ��ֱ���
    public float minVerticalGap = 3.0f;  // ��С��ֱ���
    public List<Vector2>obstaclePositions = new List<Vector2>();

    private float currentVerticalGap;  // ��ǰ��ֱ���
    private float nextSpawnY;  // ��һ���ϰ������ɵ�Yλ��

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
            // ��Y��ĵ�ǰλ�������ϰ���
            GenerateObstacles();

            // ����Yλ�ã����ݵ�ǰ��ֱ������е���
            nextSpawnY -= currentVerticalGap;

            // �������ɼ���ʹ�ֱ�������������Ϸ�Ѷ�
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - decreaseInterval);
            currentVerticalGap = Mathf.Max(minVerticalGap, currentVerticalGap - decreaseInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void GenerateObstacles()
    {
        float xPosition1 = Random.Range(minX, maxX);
        SpawnObstacle(xPosition1, nextSpawnY);

        // ��������Ƿ���ͬһY�������ɵڶ����ϰ���
        if (Random.value < 0.5f)
        {
            float xPosition2;
            // ȷ���ڶ����ϰ������һ�������㹻�ļ��
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