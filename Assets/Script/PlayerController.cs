using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialFallSpeed = 0.2f;  // ��ʼ��׹�ٶ�
    public float maxFallSpeed = 10.0f; // �����׹�ٶ�
    public float acceleration = 0.1f;      // ��׹���ٶ�
    private Rigidbody2D rb;                // ��ҵ�Rigidbody���
    private float currentSpeed;  // ��ǰ��׹�ٶ�
    private float speedMultiplier;  // �ٶȳ��������ڳ�����ѹʱ������

    public int coins = 0;

    private ObstacleSpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            spawner.StopSpawning();
            GameOver();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialFallSpeed;
        speedMultiplier = 1.0f;
        spawner = FindObjectOfType<ObstacleSpawner>(); // ��ȡ�ϰ���������������
    }

    void Update()
    {
        // ��������׹�ٶȣ�������������ٶ�
        if (currentSpeed < maxFallSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        // Ӧ����׹�ٶȵ���ҵ�Rigidbody
        rb.velocity = new Vector2(rb.velocity.x, -currentSpeed);

        // ���̿���
        float horizontalInput = Input.GetAxis("Horizontal");
        ManageAcceleration(horizontalInput != 0);
        transform.Translate(Vector2.right * horizontalInput * currentSpeed * speedMultiplier * Time.deltaTime);

        // ��������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float direction = touch.deltaPosition.x > 0 ? 1 : -1;
                ManageAcceleration(true);
                transform.Translate(Vector2.right * direction * currentSpeed * speedMultiplier * Time.deltaTime);
            }
            else
            {
                ManageAcceleration(false);
            }
        }
        else
        {
            ManageAcceleration(false);
        }


    }

    void ManageAcceleration(bool isMoving)
    {
        if (isMoving)
        {
            // �����ٶȳ�����ֱ������޶�
            speedMultiplier += acceleration * Time.deltaTime;
        }
        else
        {
            // �����ٶȳ���
            speedMultiplier = 1.0f;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(collision.gameObject);
            UpdateCoinUI();
        }
    }

    void UpdateCoinUI()
    {
        Debug.Log("Coin:" + coins);
    }
}
