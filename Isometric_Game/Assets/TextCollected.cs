using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCollected : MonoBehaviour
{
    public TextMeshProUGUI textCollected;
    int mushroomCount;

    private void OnEnable()
    {
        PickupMushroom.OnMushroomCollected += IncrementMushroomCount;
    }

    private void OnDisable()
    {
        PickupMushroom.OnMushroomCollected -= IncrementMushroomCount;
    }

    public void IncrementMushroomCount()
    {
        mushroomCount++;
        textCollected.text = $"Mushrooms Collected: {mushroomCount}";
    }
}
