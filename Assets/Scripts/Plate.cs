using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Sprite inactive;
    [SerializeField] Sprite active;
    public bool isactive = false;
    [SerializeField] private List<Door> doors;
    [SerializeField] private List<PressurePlate> plateConnection;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = inactive;
        if (Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, new Vector2(0, 0), 0)){
            GetComponent<SpriteRenderer>().sprite = active;
            isactive = true;
            var connetionsMet = true;
            foreach (var plate in plateConnection)
            {
                if (!plate.isactive)
                {
                    connetionsMet = false;
                    break;
                }
            }
            if (connetionsMet)
            {
                foreach (var door in doors)
                {
                    door.open = true;
                }
            }
        }
        else
        {
            isactive = false;
            foreach (var door in doors)
            {
                door.open = false;
            }
        }
    }
}
