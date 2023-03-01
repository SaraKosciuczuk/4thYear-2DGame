using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public int enemyHealth = 100;
    Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight())
        {
            enemyBody.velocity = new Vector2(moveSpeed, 0f);
        }else
        {
            enemyBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)*3), transform.localScale.y);
    }

    public void TakeDamage (int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
