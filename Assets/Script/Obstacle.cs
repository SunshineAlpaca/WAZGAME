using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void Update()
    {
        // ȷ���������Ұ���ϱ߽�
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;

        // ����ϰ����Ƿ񳬳����������Ұ���ϱ߽�
        if (transform.position.y > cameraTop)
        {
            Destroy(gameObject);  // �����ϰ���
        }
    }
}
