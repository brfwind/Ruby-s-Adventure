using UnityEngine;

namespace MiniGameB
{
    public class SmoothCameraFollow : MonoBehaviour
    {
        public Transform target;      // 玩家或目标物体
        public float smoothTime = 0.3f;
        private Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        void Start()
        {
            offset = transform.position - target.transform.position;
        }

        void LateUpdate()
        {
            if (target == null) return;

            // 计算摄像机的新位置
            Vector3 targetPosition = target.position + offset; // 可以调节偏移
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // 可选：让摄像机始终看向玩家
            if (MenuManager.is3DCamera)
                transform.LookAt(target);
        }
    }

}

