using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("自动移动")]
    public bool horizontal; //移动轴向
    private int dir = 1; //移动方向
    public float changeTime = 3.0f; //方向改变时间间隔
    private float timer; //计时器
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;

        if (horizontal)
        {
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("Dir", dir);
        }
        else
        {
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("Dir", dir);
        }
    }

    //转向计时器
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            dir = -dir;
            animator.SetFloat("Dir", dir);
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;

        if (horizontal)
        {
            position.x = position.x + Time.deltaTime * speed * dir;
        }
        else
        {
            position.y = position.y + Time.deltaTime * speed * dir;
        }

        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.gameObject.GetComponent<RubyController>();

        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }
}
