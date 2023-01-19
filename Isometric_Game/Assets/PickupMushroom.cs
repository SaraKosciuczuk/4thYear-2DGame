using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMushroom : MonoBehaviour, Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        Debug.Log("You collected a mushroom");
    }
}
