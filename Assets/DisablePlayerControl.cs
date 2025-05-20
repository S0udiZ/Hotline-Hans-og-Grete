using Cainos.PixelArtTopDown_Basic;
using System.ComponentModel;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DisablePlayerControl : MonoBehaviour
{

    TopDownCharacterController cc;

    bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<TopDownCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            cc.enabled = false;
            StartCoroutine(EnableBox(2.0F));
        }
        if (other.gameObject.CompareTag("Base"))
        {
            cc.enabled = true;
        }
        IEnumerator EnableBox(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            cc.enabled = true;
        }
    }
}
