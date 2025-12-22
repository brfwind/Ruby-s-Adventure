using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    private Vector2 startPos;
    private bool canMove = true;
    public float speed;
    private float BG_WIDTH = 7.4f;

    void Update()
    {
        if (BirdController.isDead)
        {
            canMove = false;
        }
    }

    //地面移动
    //利用两个一摸一样的地面，互相衔接
    void FixedUpdate()
    {
        if (canMove)
        {
            bg1.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
            bg2.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);

            if (bg1.transform.position.x < -BG_WIDTH - 0.1f)
            {
                bg1.transform.position = bg2.transform.position + new Vector3(BG_WIDTH, 0, 0);
            }
            if (bg2.transform.position.x < -BG_WIDTH - 0.1f)
            {
                bg2.transform.position = bg1.transform.position + new Vector3(BG_WIDTH, 0, 0);
            }
        }
    }
}
