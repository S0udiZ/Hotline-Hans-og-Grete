using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    GameObject Player;
    GameObject Hans;
    GameObject Grete;
    [SerializeField] List<GameObject> Plates;
    [SerializeField] List<GameObject> SlidingDoors;
    GameObject Door;
    [SerializeField] int Level = 1;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hans = GameObject.FindGameObjectWithTag("Hans");
        Grete = GameObject.FindGameObjectWithTag("Grete");
        Door = GameObject.FindGameObjectWithTag("Door");
        Player.GetComponent<PlayerControllerScript>().HansFinish = false;
        Player.GetComponent<PlayerControllerScript>().GreteFinish = false;
    }

    // Update is called twice per frame
    void Update()
    {
        if (Door.GetComponent<Door>().open == true)
        {
            if (Vector2.Distance(Hans.transform.position, Door.transform.position) < 0.4f)
            {
                Player.GetComponent<PlayerControllerScript>().HansFinish = true;
            }
            if (Vector2.Distance(Grete.transform.position, Door.transform.position) < 0.4f)
            {
                Player.GetComponent<PlayerControllerScript>().GreteFinish = true;
            }
        }
        /*
        switch (Level)
        {
            case 1:
                {
                    Door.GetComponent<Door>().open = false;
                    SlidingDoors[0].GetComponent<SlidingDoors>().open = false;
                    SlidingDoors[1].GetComponent<SlidingDoors>().open = false;
                    if (Plates[0].GetComponent<PressurePlate>().isactive)
                    {
                        Door.GetComponent<Door>().open = true;
                        SlidingDoors[0].GetComponent<SlidingDoors>().open = true;
                        SlidingDoors[1].GetComponent<SlidingDoors>().open = true;
                    }
                    break;
                }
            case 2:
                {
                    Door.GetComponent<Door>().open = false;
                    if (Plates[0].GetComponent<PressurePlate>().isactive)
                    {
                        if (Plates[1].GetComponent<PressurePlate>().isactive)
                        {
                            Door.GetComponent<Door>().open = true;
                        }
                    }
                    break;
                }
            case 3:
                {
                    Door.GetComponent<Door>().open = false;
                    if (Plates[0].GetComponent<PressurePlate>().isactive)
                    {
                        if (Plates[1].GetComponent<PressurePlate>().isactive)
                        {
                            Door.GetComponent<Door>().open = true;
                        }
                    }
                    break;
                }
            case 4:
                {
                    Door.GetComponent<Door>().open = false;
                    if (Plates[0].GetComponent<PressurePlate>().isactive)
                    {
                        if (Plates[1].GetComponent<PressurePlate>().isactive)
                        {
                            Door.GetComponent<Door>().open = true;
                        }
                    }
                    break;
                }
            case 5:
                {
                    Door.GetComponent<Door>().open = false;
                    SlidingDoors[1].GetComponent<SlidingDoors>().open = false;
                    SlidingDoors[2].GetComponent<SlidingDoors>().open = false;
                    if (Plates[0].GetComponent<PressurePlate>().isactive)
                    {
                        Door.GetComponent<Door>().open = true;
                        SlidingDoors[1].GetComponent<SlidingDoors>().open = true;
                        SlidingDoors[2].GetComponent<SlidingDoors>().open = true;
                    }
                    SlidingDoors[0].GetComponent<SlidingDoors>().open = false;
                    if (Plates[1].GetComponent<PressurePlate>().isactive)
                    {
                        SlidingDoors[0].GetComponent<SlidingDoors>().open = true;
                    }
                    break;
                }
        }
*/
    }
}
