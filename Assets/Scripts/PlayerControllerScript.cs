using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] // Hans charecter parent Gameobject
    public GameObject HansGameObject;
    [SerializeField] // ParticleSystem
    public ParticleSystem HansPS;
    public bool HansFinish = false;

    [SerializeField] // Grete character parent Gameobject
    public GameObject GreteGameObject;
    [SerializeField] // ParticleSystem
    public ParticleSystem GretePS;
    public bool GreteFinish = false;

    [SerializeField] // Current character object.
    public GameObject CurrentCharObj;
    [SerializeField] // Camera Object
    GameObject Camera;

    // [SerializeField] TextMeshProUGUI hinttext;
    // [SerializeField] Image hintbg;
    // float hintdelay = 0f;

    [SerializeField] float PlayerSpeed = 0.8f;

    [SerializeField] GameObject SoundsObj;

    float StepTimer = 0f;

    float stepSize = 1f;

    float SwapCooldown = 0.5f;

    bool awaitreset = false;
    float resettimer = 0f;

    void PlaySound(string sound)
    {
        // Debug.Log("[" + Time.time + "] Play Sound: " + sound);
        SoundsObj.GetComponent<SoundScript>().PlaySound(sound);
    }

    // public void SetHint(string txt)
    // {
    //     hinttext.text = txt;
    //     hintdelay = 0.2f;
    // }

    public void KillHans()
    {
        if (HansGameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            return;
        }
        awaitreset = true;
        resettimer = 0.2f;
        HansPS.Emit(40);
        HansGameObject.GetComponent<SpriteRenderer>().enabled = false;
        HansGameObject.GetComponent<Collider2D>().enabled = false;
    }
    public void KillGrete()
    {
        if (GreteGameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            return;
        }
        awaitreset = true;
        resettimer = 0.2f;
        GretePS.Emit(40);
        GreteGameObject.GetComponent<SpriteRenderer>().enabled = false;
        GreteGameObject.GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Hint
        // hintdelay -= Time.deltaTime;
        // hintbg.GetComponent<Image>().enabled = true;
        // if (hintdelay < 0)
        // {
        //     hinttext.text = "";
        //     hintbg.GetComponent<Image>().enabled = false;
        // }
        //Reset
        if (awaitreset)
        {
            resettimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.R) || (resettimer<0&&awaitreset))
        {
            resettimer = 0;
            awaitreset = false;
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

        dir = dir.normalized * PlayerSpeed * Time.deltaTime;
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
        HansGameObject.GetComponent<Collider2D>().enabled = true;
        GreteGameObject.GetComponent<Collider2D>().enabled = true;
        HansFinish = false;
        GreteFinish = false;
        CurrentCharObj = HansGameObject;
    }
}
