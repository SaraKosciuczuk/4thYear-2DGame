using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupMushroom : MonoBehaviour, Collectable
{
    public static event Action OnMushroomCollected;

    public void Collect()
    {
        OnMushroomCollected?.Invoke();
        Destroy(gameObject);
        Debug.Log("You collected a mushroom");
    }
}
