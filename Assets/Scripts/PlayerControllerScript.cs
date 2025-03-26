using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] // Hans charecter parent Gameobject
    GameObject HansGameObject;
    [SerializeField] // Grete character parent Gameobject
    GameObject GreteGameObject;

    [SerializeField] // Camera Object
    GameObject Camera;

    bool CurrentCharacter = false; // False=Hans, True=Grete.

    float StepTimer = 0f;

    float SwapCooldown = 0.5f;

    void PlaySound(string sound)
    {
        Debug.Log("[" + Time.time + "] Play Sound: " + sound);
        //SoundsObject.GetComponent<CamSounds>().PlaySound(sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SwapCooldown < 0)
            {
                CurrentCharacter = !CurrentCharacter;
                PlaySound("swap");
                SwapCooldown = 0.5f;
            }
        }
        SwapCooldown -= Time.deltaTime;
        Vector3 dir;
        if (CurrentCharacter)
        {
            dir = HansGameObject.transform.localPosition-Camera.transform.localPosition;
        }
        else
        {
            dir = GreteGameObject.transform.localPosition - Camera.transform.localPosition;
        }
        dir.z = -10;
        Camera.transform.localPosition = Camera.transform.localPosition+(dir/5);
        Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -10);

        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir.y++;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir.y--;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x++;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x--;
        }
        if (dir == Vector3.zero)
        {
            StepTimer = 0f;
        }

        dir = dir.normalized * 3.4f;

        StepTimer += Time.deltaTime;
        if (StepTimer > 0.45f)
        {
            PlaySound("step");
            StepTimer = 0;
        }

        if (CurrentCharacter)
        {
            HansGameObject.transform.position += dir*Time.deltaTime;
        }
        else
        {
            GreteGameObject.transform.position += dir*Time.deltaTime;
        }
    }
}
