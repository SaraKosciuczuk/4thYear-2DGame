using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public int enemyHealth = 50;
    Rigidbody2D rb;

    int walkin = 1;
    public GameObject walkingRightAnimation;
    public GameObject walkingLeftAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(walkin == 1)
        {
            StartCoroutine(walkingg());
        }

        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator walkingg()
    {
        walkin -= 1;
        walkingLeftAnimation.gameObject.SetActive(false);
        
        rb.velocity = new Vector2(speed, 0f);
        yield return new WaitForSeconds(3);

        walkingRightAnimation.gameObject.SetActive(false);

        walkingLeftAnimation.gameObject.SetActive(true);

        rb.velocity = new Vector2(-speed, 0f);
        yield return new WaitForSeconds(3);

        walkin += 1;
        walkingRightAnimation.gameObject.SetActive(true);
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            enemyHealth -= 1;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}