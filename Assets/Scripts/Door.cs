using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite closedsprite;
    [SerializeField] Sprite opensprite;
    public bool open = false;

    void Update()
    {
        //Set open/close sprite
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
