using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class SlidingDoors : IDoor
{
    Vector3 defaultposition;
    [SerializeField] Vector2 MoveVector;
    private Coroutine doorCoroutine;

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
            }
            // transform.position += (defaultposition - transform.position) * Time.deltaTime * 10;
        }
    }

    private IEnumerator AnimationTweenCoroutine(float duration, Vector2 direction)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = open ? defaultposition + (Vector3)direction : defaultposition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }

    private void AnimationTween(float duration, Vector2 direction)
    {
        if (doorCoroutine != null)
        {
            StopCoroutine(doorCoroutine);
        }
        doorCoroutine = StartCoroutine(AnimationTweenCoroutine(duration, direction));
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
