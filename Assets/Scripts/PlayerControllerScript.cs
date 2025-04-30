using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] // Hans charecter parent Gameobject
    public GameObject HansGameObject;
    public bool HansFinish = false;
    [SerializeField] // Grete character parent Gameobject
    public GameObject GreteGameObject;
    public bool GreteFinish = false;
    [SerializeField] // Current character object.
    public GameObject CurrentCharObj;
    [SerializeField] // Camera Object
    GameObject Camera;


    [SerializeField] float PlayerSpeed = 0.8f;

    float StepTimer = 0f;

    float stepSize = 1f;

    float SwapCooldown = 0.5f;

    void PlaySound(string sound)
    {
        Debug.Log("[" + Time.time + "] Play Sound: " + sound);
        //SoundsObject.GetComponent<CamSounds>().PlaySound(sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<LevelManager>().ResetLevel();
        }
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
        HansGameObject.GetComponent<SpriteRenderer>().enabled = true;
        GreteGameObject.GetComponent<SpriteRenderer>().enabled = true;

        if (HansFinish)
        {
            Debug.Log("Hans is finish");
            CurrentCharObj = GreteGameObject;
            HansGameObject.GetComponent<SpriteRenderer>().enabled = false;
            HansGameObject.transform.position = new Vector3(10000, 0, 0);
        }
        if (GreteFinish)
        {
            Debug.Log("Grete is finish");
            CurrentCharObj = HansGameObject;
            GreteGameObject.GetComponent<SpriteRenderer>().enabled = false;
            GreteGameObject.transform.position = new Vector3(10000, 0, 0);
        }
        if (HansFinish && GreteFinish)
        {
            GetComponent<LevelManager>().ChangeLevel(GetComponent<LevelManager>().GetLevel()+1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<LevelManager>().ChangeLevel(GetComponent<LevelManager>().GetLevel() + 1);
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
        if (dir.magnitude > 0.2f)
        {
            CurrentCharObj.transform.localRotation = Quaternion.identity;
            CurrentCharObj.transform.Rotate(new Vector3(0, 0, GetAngleFromVector(dir) + 90));
        }


        //Step sound
        if (dir == Vector3.zero)
        {
            StepTimer = 0f;
            stepSize = 1f;
        }
        StepTimer += Time.deltaTime;
        stepSize += Time.deltaTime / 5;
        if (StepTimer > 0.45f)
        {
            PlaySound("step");
            StepTimer = 0f;
            stepSize = 1f;
        }
        CurrentCharObj.transform.localScale = new Vector2(stepSize, stepSize);

    }
    float GetAngleFromVector(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return 0f;

        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public void ResetPlayers()
    {
        HansGameObject.transform.position = new Vector3(0, 0.5f, 0);
        GreteGameObject.transform.position = new Vector3(0, -0.5f, 0);
        HansGameObject.GetComponent<SpriteRenderer>().enabled = true;
        GreteGameObject.GetComponent<SpriteRenderer>().enabled = true;
        HansFinish = false;
        GreteFinish = false;
    }
}
