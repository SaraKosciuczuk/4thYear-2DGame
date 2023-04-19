using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static manager instance;

    public Text mushroomPickedUp;
    public float mushroomScore = 0;

    public GameObject door;
    public GameObject doorTwo;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mushroomPickedUp.text = mushroomScore.ToString() + " Collected";
    }

    void Update()
    {
        if(mushroomScore >= 5)
        {
            DoorOne();
        }

        if(mushroomScore >= 10)
        {
            DoorTwo();
        }
    }

    public void AddMushroom()
    {
        mushroomScore += 1f;
        mushroomPickedUp.text = mushroomScore.ToString() + " Collected";


    } 

    public void DoorOne()
    {
        door.gameObject.SetActive(false);
    }

    public void DoorTwo()
    {
        door.gameObject.SetActive(false);
    }

}
