using UnityEngine;

public class PressurePlate_Button : MonoBehaviour
{
    [SerializeField] private Transform DoorLight;
    [SerializeField] private Collider2D ButtonCollider;

    // check if the player is on the pressure plate
    private void OnTriggerEnter2D(Collider2D collision) // Det dur fucking ikke
    {
        Debug.Log("Player is on the pressure plate");
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorLight.localScale = new Vector3(4, DoorLight.localScale.y, DoorLight.localScale.z);
        }
    }
}
