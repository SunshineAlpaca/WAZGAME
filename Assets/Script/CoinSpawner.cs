using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // ���Ԥ��
    public ObstacleSpawner obstacleSpawner; // �ϰ�������������
    public float spawnRate = 5.0f; // ������ɵ�ʱ��������λΪ��
    private float nextSpawnTime = 0; // ��һ�����ɽ�ҵ�ʱ���

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnRate; // �����´����ɽ�ҵ�ʱ��
        }
    }

    void SpawnCoin()
    {
        foreach (var pos in obstacleSpawner.obstaclePositions)
        {
            float potentialX = pos.x + 1.5f; // ���������ϰ����Ҳ�1.5��λ���봦����

            bool isFree = true;
            foreach (Vector2 obstaclePos in obstacleSpawner.obstaclePositions)
            {
                if (Mathf.Abs(obstaclePos.x - potentialX) < 1.5f) // �������λ���Ƿ����㹻�ռ�
                {
                    isFree = false;
                    break;
                }
            }

            if (isFree)
            {
                float y = pos.y;
                Instantiate(coinPrefab, new Vector3(potentialX, y, 0), Quaternion.identity);
                break; // ����һ����Һ��˳�ѭ��
            }
        }
    }
}
