using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialFallSpeed = 0.2f;  // 初始下坠速度
    public float maxFallSpeed = 10.0f; // 最大下坠速度
    public float acceleration = 0.1f;      // 下坠加速度
    private Rigidbody2D rb;                // 玩家的Rigidbody组件
    private float currentSpeed;  // 当前下坠速度
    private float speedMultiplier;  // 速度乘数，基于持续按压时间增加

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
        spawner = FindObjectOfType<ObstacleSpawner>(); // 获取障碍物生成器的引用
    }

    void Update()
    {
        // 逐渐增加下坠速度，但不超过最大速度
        if (currentSpeed < maxFallSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        // 应用下坠速度到玩家的Rigidbody
        rb.velocity = new Vector2(rb.velocity.x, -currentSpeed);

        // 键盘控制
        float horizontalInput = Input.GetAxis("Horizontal");
        ManageAcceleration(horizontalInput != 0);
        transform.Translate(Vector2.right * horizontalInput * currentSpeed * speedMultiplier * Time.deltaTime);

        // 触摸控制
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
            // 增加速度乘数，直到最大限度
            speedMultiplier += acceleration * Time.deltaTime;
        }
        else
        {
            // 重置速度乘数
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
