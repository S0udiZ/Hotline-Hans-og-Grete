using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] // Hans charecter parent Gameobject
    public GameObject HansGameObject;
    [SerializeField] // Grete character parent Gameobject
    public GameObject GreteGameObject;
    [SerializeField] // Current character object.
    public GameObject CurrentCharObj;
    [SerializeField] // Camera Object
    GameObject Camera;

    float PlayerSpeed = 1.2f;

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
        HansGameObject.GetComponent<Animator>().SetInteger("value", 1);
        //Swap mechanic
        SwapCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SwapCooldown < 0)
            {
                if (CurrentCharObj == HansGameObject)
                {
                    CurrentCharObj = GreteGameObject;
                }
                else if (CurrentCharObj == GreteGameObject)
                {
                    CurrentCharObj = HansGameObject;
                }
                PlaySound("swap");
                SwapCooldown = 0.5f;
            }
        }

        //Camera follow

        Vector3 dir;
        dir = CurrentCharObj.transform.localPosition-Camera.transform.localPosition;
        Camera.transform.localPosition = Camera.transform.localPosition+(dir*(Time.deltaTime*2));
        Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -10);

        //Movement
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

        dir = dir.normalized * PlayerSpeed;
        dir += (Vector3)CurrentCharObj.GetComponent<Rigidbody2D>().linearVelocity;
        CurrentCharObj.GetComponent<Rigidbody2D>().linearVelocity = dir;
        //(dir).normalized;

        //Step sound
        if (dir == Vector3.zero)
        {
            StepTimer = 0f;
        }
        StepTimer += Time.deltaTime;
        if (StepTimer > 0.45f)
        {
            PlaySound("step");
            StepTimer = 0;
        }

    }
}
