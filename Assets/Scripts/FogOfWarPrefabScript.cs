using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FogOfWarPrefabScript : MonoBehaviour
{
    // Update is called once per frame

    bool HasSeen = false;

    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.localScale = new Vector3(3.9f, 3.9f, 1);

        Transform ptrans;
        RaycastHit2D hits;

        ptrans = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().HansGameObject.transform;
        hits = Physics2D.Linecast(transform.position, ptrans.position);
        if (!(hits.transform.tag == "MapCollider"))
        {
            if (HasSeen)
            {
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            HasSeen = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }

        ptrans = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().GreteGameObject.transform;
        hits = Physics2D.Linecast(transform.position, ptrans.position);
        if (!(hits.transform.tag == "MapCollider"))
        {
            if (HasSeen)
            {
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            HasSeen = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
