using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float floatSpeed;
    public float floatA;
    private float gravity = -9.81f;
    public float JumpHeight;
    public float fallSpeed;
    public float fallRotateSpeed;
    private float RotationZ;
    public float MaxFallSpeed;
    public static bool isDead = false;
    public static int score = 0;
    private Vector2 TopV;
    private Vector2 startPos;
    private Vector3 Velocity = Vector3.zero;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!UIManager.isStart)
        {
            Idle();
        }
        else
        {
            Flying();
        }
    }

    private void Flying()
    {
        //如果按下左键
        //就让小鸟固定向上移动一段距离（经验公式）
        if(Input.GetMouseButtonDown(0) && !isDead)
        {
            Velocity.y = Mathf.Sqrt(-2 * gravity * JumpHeight);
            TopV = Velocity;
            RotationZ = 30;
        }
        
        //小鸟沿重力加速度下落
        Velocity.y += Time.deltaTime * gravity * fallSpeed;

        //最高下落速度限制
        if(Velocity.y < MaxFallSpeed)
            Velocity.y = MaxFallSpeed;

        transform.position += Velocity * Time.deltaTime;

        //合适时候，让小鸟头部下掉
        if(Velocity.y < TopV.y * 0.5f)
        {
            RotationZ -= Time.deltaTime * fallRotateSpeed * Mathf.Rad2Deg;
            RotationZ = Mathf.Max(-90,RotationZ);
        }

        transform.eulerAngles = new Vector3(0,0,RotationZ);
    }

    void Idle()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatA;
        transform.position = new Vector2(startPos.x, newY);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("pipe"))
        {
            Debug.Log("撞到了");
            isDead = true;
        }   
        if(other.CompareTag("score"))
        {
            Debug.Log("得分了");
            score++;
        }   
    }
}
