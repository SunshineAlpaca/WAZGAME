using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 玩家的Transform
    public float verticalOffset = -5.0f;  // 摄像机在玩家下方的垂直偏移量
    public float smoothSpeed = 0.3f;  // 摄像机跟随的平滑速度

    private Vector3 targetPosition;  // 摄像机的目标位置

    void LateUpdate()
    {
        if (player != null)
        {
            // 计算摄像机的目标位置，确保摄像机始终在玩家下方固定的垂直距离
            targetPosition = new Vector3(transform.position.x, player.position.y + verticalOffset, transform.position.z);

            // 平滑地移动摄像机到目标位置
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}