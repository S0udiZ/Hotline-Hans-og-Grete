using UnityEngine;
using System.Collections;

public class Trowingstar : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed of the throwing star
    [SerializeField] private float lifetime = 5f; // Lifetime of the throwing star
    [SerializeField] private float rotationSpeed = 500f; // Rotation speed of the throwing star
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
        // Rotate the throwing star towards the direction of the throw
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        throwingStar.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust the rotation to match the direction of the throw
        // Rotate the throwing star while it is alive
        StartCoroutine(RotateThrowingStar(throwingStar));

        // Destroy the throwing star after its lifetime
        Destroy(throwingStar, lifetime);
    }

    // Coroutine to rotate the throwing star
    private IEnumerator RotateThrowingStar(GameObject throwingStar)
    {
        while (throwingStar != null)
        {
            throwingStar.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Method to set the player reference (can be called from another script)
    public void SetPlayer(GameObject playerObject)
    {
        player = playerObject;
    }
}
