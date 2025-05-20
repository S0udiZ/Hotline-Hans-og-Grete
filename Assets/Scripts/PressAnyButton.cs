using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class PressAnyButton : MonoBehaviour
{
    public GameObject ActivatedObject;
    public GameObject ActivatedTitle;
    public GameObject DeactivatedObject;
    void Update()
    {
        if (Input.anyKey)
        {
            ActivatedObject.SetActive(true);
            ActivatedTitle.SetActive(true);
            DeactivatedObject.SetActive(false);
        }
    }

}
