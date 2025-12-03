using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;
    private Animator animator;
    private ParticleSystem smokeEffect;
    private AudioSource walkAudio;
    public GameObject hitEffect;

    [Header("自动移动")]
    public bool horizontal; //移动轴向
    private int dir = 1; //移动方向
    public float changeTime = 3.0f; //方向改变时间间隔
    private float timer; //计时器
    private bool isBroken; //是否故障

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        smokeEffect = GetComponentInChildren<ParticleSystem>();

        walkAudio = GetComponent<AudioSource>();
        walkAudio.volume = 0.2f;
        walkAudio.Play();

        timer = changeTime;
        isBroken = true;

        PlayMoveAnimation();
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
        if (!isBroken)
        {
            return;
        }

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

    private void PlayMoveAnimation()
    {
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

    public void Fix()
    {
        Instantiate(hitEffect,transform.position,Quaternion.identity);

        isBroken = false;

        //不再发生碰撞监测和效果
        rb.simulated = false;
        //动画状态机的条件设置
        animator.SetTrigger("Fixed");
        //粒子特效关闭
        smokeEffect.Stop();

        walkAudio.Stop();

        //随机播放受击音效
        string[] clips = { "HitTile1", "HitTile2" };
        AudioManager.PlayAudio(clips[Random.Range(0, clips.Length)], false);

        //延时后播放修复音效
        StartCoroutine(DelayPlaySFX());

        UIHealthBar.instance.fixedNum++;
    }

    IEnumerator DelayPlaySFX()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.PlayAudio("Robot Fixed", true);
    }
}
