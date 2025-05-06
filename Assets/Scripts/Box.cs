using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    void Update()
    {
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().CurrentCharObj.transform.position;
        if (Vector2.Distance(playerpos, transform.position) < 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>().SetHint("Du kan skubbe til den her kasse.");
        }
    }
    public void Smash()
    {
        ps.Emit(100);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
