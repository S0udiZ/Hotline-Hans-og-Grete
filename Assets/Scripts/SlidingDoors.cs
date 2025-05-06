using Unity.Mathematics;
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
            transform.position += (defaultposition + (Vector3)MoveVector - transform.position) * Time.deltaTime * 10;
        }
        else
        {
            if ((defaultposition - transform.position).magnitude > 0.01f)
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.4f, 0.4f), 0f, MoveVector, 0.8f);
                if (hit.collider.gameObject.CompareTag("Box"))
                {
                    hit.collider.gameObject.GetComponent<Box>().Smash();
                }
                if (hit.collider.gameObject.CompareTag("Hans"))
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().KillHans();
                }
                if (hit.collider.gameObject.CompareTag("Grete"))
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().KillGrete();
                }
            }
            transform.position += (defaultposition - transform.position) * Time.deltaTime * 10;
        }
    }
}
