using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    public bool open = false;
    Vector3 defaultposition;
    [SerializeField] Vector2 MoveVector;

    void Start()
    {
        defaultposition = transform.position;
    }

    void Update()
    {
        //Set open/close sprite
        if (open)
        {
            transform.position += ((defaultposition + (Vector3)MoveVector) - transform.position) * Time.deltaTime*2;
        }
        else
        {
            transform.position += (defaultposition - transform.position) * Time.deltaTime*5;
        }
    }
}
