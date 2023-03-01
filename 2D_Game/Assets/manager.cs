using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static manager instance;

    public Text mushroomPickedUp;
    float mushroomScore = 0;

    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mushroomPickedUp.text = mushroomScore.ToString() + " Collected";
    }

    public void AddMushroom()
    {
        mushroomScore += 0.5f;
        mushroomPickedUp.text = mushroomScore.ToString() + " Collected";
    } 
}
