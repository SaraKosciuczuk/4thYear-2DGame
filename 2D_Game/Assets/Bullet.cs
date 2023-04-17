using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);

        EnemyOne enemy = hitInfo.GetComponent<EnemyOne>();
        //if (enemy != null)
        //{
            //enemy.TakeDamage(damage);
        //}
        Destroy(gameObject);
    }
}
