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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentCharacter = !CurrentCharacter;
        }
        Vector3 dir;
        if (CurrentCharacter)
        {
            dir = HansGameObject.transform.localPosition-Camera.transform.localPosition;
        }
        else
        {
            dir = GreteGameObject.transform.localPosition - Camera.transform.localPosition;
        }
        Debug.Log(dir);
        dir.z = -10;
        Camera.transform.localPosition = Camera.transform.localPosition+(dir/5);
    }
}
