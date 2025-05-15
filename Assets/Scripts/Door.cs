using UnityEngine;

public class Door : IDoor
{
    [SerializeField] Sprite closedsprite;
    [SerializeField] Sprite opensprite;

    // void Update()
    // {
    //     //Set open/close sprite
    //     if (open)
    //     {
    //         GetComponent<SpriteRenderer>().sprite = opensprite;
    //     }
    //     else
    //     {
    //         GetComponent<SpriteRenderer>().sprite = closedsprite;
    //     }
    // }

    public override void SwitchOpen()
    {
        Debug.Log("SwitchOpen");
        open = !open;
        if (open)
        {
            GetComponent<SpriteRenderer>().sprite = opensprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = closedsprite;
        }
    }
}
