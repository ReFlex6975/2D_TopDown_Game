using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceboll : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChaseBehavior enemy = collision.GetComponent<ChaseBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Враг получил урон от Iceboll");
            }

            Destroy(gameObject);
        }
    }
}
