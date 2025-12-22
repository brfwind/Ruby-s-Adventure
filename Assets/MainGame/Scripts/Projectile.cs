using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //为子弹施加一个力
    public void Launch(Vector2 dir,float force)
    {   
        Debug.Log("施加力");
        rb.AddForce(dir*force);
    }

    void Update()
    {
        if(transform.position.magnitude > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemyController = other.gameObject.GetComponentInParent<EnemyController>();
        
        if(enemyController != null)
        {
            enemyController.Fix();
            Debug.Log("修复");
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
        
        if(enemyController != null)
        {
            enemyController.Fix();
            Debug.Log("修复");
        }

        Destroy(gameObject);
    }
}
