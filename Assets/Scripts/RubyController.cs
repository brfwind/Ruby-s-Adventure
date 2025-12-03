using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    [Header("动画相关")]
    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;

    public GameObject projectilePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }

        //检测是否与NPC对话
        if(Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position+Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("npc"));

            if(hit.collider != null)
            {
                NPCDialog npcDialog = hit.collider.GetComponent<NPCDialog>();

                if(npcDialog != null)
                {
                    npcDialog.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        //若当前玩家输入的某个轴向值不为0
        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //移动
        Vector2 position = transform.position;
        position += speed * move * Time.deltaTime;

        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        //如果是扣血指令
        if (amount < 0)
        {
            //如果在无敌状态
            //不再执行下面的所有代码
            if (isInvincible)
                return;

            //如果无敌状态过期了，且收到了伤害
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        //ps：以上的代码除非因为在无敌时间而进入return，下面的代码还是会正常执行
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }

    //发射子弹
    private void Launch()
    {
        //生成一个子弹预制体
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position, Quaternion.identity);
        //获得该预制体上的脚本
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        //让子弹只能朝上下左右四个方向发射
        //Mathf.Sign返回1或-1
        Vector2 pushDir;
        if (Mathf.Abs(lookDirection.x) > Mathf.Abs(lookDirection.y))
        {
            // 左右方向
            pushDir = new Vector2(Mathf.Sign(lookDirection.x), 0);
        }
        else
        {
            // 上下方向
            pushDir = new Vector2(0, Mathf.Sign(lookDirection.y));
        }

        //调用脚本里的Launch()方法
        projectile.Launch(pushDir, 300);
        //设置动画条件
        animator.SetTrigger("Launch");
    }
}
