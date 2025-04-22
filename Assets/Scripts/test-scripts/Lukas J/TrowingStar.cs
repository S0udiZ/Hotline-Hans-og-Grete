using UnityEngine;

public class Trowingstar : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed of the throwing star
    [SerializeField] private float lifetime = 5f; // Lifetime of the throwing star
    [SerializeField] private GameObject player; // Reference to the player object
    [SerializeField] private GameObject throwingStarPrefab; // Prefab for the throwing star
    private Vector2 spawnPoint;

    // Method to throw the star
    public void ThrowStar()
    {
        // Set the spawn point to the player's position
        spawnPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        // Instantiate the throwing star at the player's position
        GameObject throwingStar = Instantiate(throwingStarPrefab, spawnPoint, Quaternion.identity);
        Rigidbody2D rb = throwingStar.GetComponent<Rigidbody2D>();
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnPoint).normalized;
        rb.linearVelocity = direction * speed;

        // Destroy the throwing star after its lifetime
        Destroy(throwingStar, lifetime);
    }
}
