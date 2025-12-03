using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject eatEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null)
        {
            if (ruby.CurrentHealth < ruby.maxHealth)
            {
                Instantiate(eatEffect,transform.position,quaternion.identity);
                ruby.ChangeHealth(1);
                AudioManager.PlayAudio("GetHealth",false);
                Destroy(gameObject);
            }
        }
    }
}
