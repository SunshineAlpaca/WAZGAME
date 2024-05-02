using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ��ҵ�Transform
    public float verticalOffset = -5.0f;  // �����������·��Ĵ�ֱƫ����
    public float smoothSpeed = 0.3f;  // ����������ƽ���ٶ�

    private Vector3 targetPosition;  // �������Ŀ��λ��

    void LateUpdate()
    {
        if (player != null)
        {
            // �����������Ŀ��λ�ã�ȷ�������ʼ��������·��̶��Ĵ�ֱ����
            targetPosition = new Vector3(transform.position.x, player.position.y + verticalOffset, transform.position.z);

            // ƽ�����ƶ��������Ŀ��λ��
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}