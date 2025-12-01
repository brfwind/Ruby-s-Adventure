using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("基本属性")]
    public float speed;
    public int maxHealth;
    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    [Header("无敌时间")]
    public float timeInvincible; //无敌时间
    private bool isInvincible;
    private float invincibleTimer; //计时器

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        invincibleTimer = timeInvincible;
        isInvincible = true;
    }

    void Update()
    {
        //无敌时间计时器
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        Debug.Log(currentHealth);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        //如果是扣血指令
        if (amount < 0)
        {
            //如果在无敌状态
            //不再执行下面的所有代码
            if(isInvincible)
                return;
            
            //如果无敌状态过期了，且收到了伤害
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        //ps：以上的代码除非因为在无敌时间而进入return，下面的代码还是会正常执行
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
