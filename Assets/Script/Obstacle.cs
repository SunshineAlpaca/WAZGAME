using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void Update()
    {
        // 确定摄像机视野的上边界
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;

        // 检查障碍物是否超出了摄像机视野的上边界
        if (transform.position.y > cameraTop)
        {
            Destroy(gameObject);  // 销毁障碍物
        }
    }
}
