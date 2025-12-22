using System;
using UnityEngine;

namespace MiniGameB
{
    public class Player : MonoBehaviour
    {
        public Rigidbody rb;
        public AudioManager music;
        public bool move = false;
        public float speed = 5f;
        public bool canControl = true;

        //事件
        public event Action OnFoodCollected;

        void Start()
        {
            music = AudioManager.instance;
        }

        //只有小球有水平速度时，才开始计时
        //move会在levelManager里被使用
        void Update()
        {
            //从rb获取小球速度信息(V3)
            Vector3 v = rb.velocity;
            //忽略竖直速度，得到二维速度信息(V2)
            Vector2 hv = new Vector2(v.x, v.z);
            //得到水平速度的绝对大小
            float hSpeed = hv.magnitude;

            if (hSpeed > 0.1f)
                move = true;
            else
                move = false;
        }

        //施加力实现小球移动
        void FixedUpdate()
        {
            if (!canControl)
                return;

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v) * speed;
            rb.AddForce(dir);
        }

        //碰撞食物检测
        void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Food"))
            {
                Destroy(collision.gameObject);
                music.PlayFood();
                OnFoodCollected?.Invoke(); // 通知 LevelManager
            }
        }
    }

}
