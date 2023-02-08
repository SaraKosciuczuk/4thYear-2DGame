using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public int enemyHealth = 4;

    Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyHealth = 4;
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

        if(enemyHealth <= 0)
		{
			this.gameObject.SetActive(false);
		}
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)), transform.localScale.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("collided, -1 health");
            enemyHealth -= 1;
        }
    }
}
