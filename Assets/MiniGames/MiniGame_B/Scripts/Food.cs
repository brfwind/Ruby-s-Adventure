using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameB
{
    public class Food : MonoBehaviour
    {
        public float floatAmplitude;   // 上下浮动幅度
        public float floatSpeed;       // 上下浮动速度
        public float rotateSpeed;      // 旋转速度

        private Vector3 startPos;

        void Start()
        {
            startPos = transform.position; // 记录初始位置
        }

        //食物浮动动画
        void Update()
        {
            // 旋转方块
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);

            // 上下浮动
            //Time.time是游戏从开始到目前经过的时间
            //通过Time.time * floatSpeed实现sin值随时间变化
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

            //更新位置
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }

}

