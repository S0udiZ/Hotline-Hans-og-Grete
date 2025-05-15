using Unity.Mathematics;
using UnityEngine;

public class SlidingDoors : IDoor
{
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
            // transform.position += (defaultposition + (Vector3)MoveVector - transform.position) * Time.deltaTime * 10;
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
            // transform.position += (defaultposition - transform.position) * Time.deltaTime * 10;
        }
    }

    private void AnimationTween(float duration, Vector2 direction)
    {
        var time = duration;
        if (open)
        {
            transform.position += 10 * Time.deltaTime * (defaultposition + (Vector3)direction - transform.position);
        }
        else
        {
            transform.position += 10 * Time.deltaTime * (defaultposition - transform.position);
        }
        time -= Time.deltaTime;
        if (time >= 0f)
        {
            AnimationTween(time, direction);
        }
    }

    public override void SwitchOpen()
    {
        open = !open;
        if (open)
        {
            AnimationTween(0.5f, MoveVector);
        }
        else
        {
            AnimationTween(0.5f, -MoveVector);
        }

    }
}
