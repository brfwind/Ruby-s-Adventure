using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null)
        {
            if (ruby.CurrentHealth < ruby.maxHealth)
            {
                ruby.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
