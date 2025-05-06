using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Sprite inactive;
    [SerializeField] Sprite active;
    public bool isactive = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = inactive;
        isactive = false;
        if (Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, new Vector2(0, 0), 0)){
            GetComponent<SpriteRenderer>().sprite = active;
            isactive = true;
        }
    }
}
