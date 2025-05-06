using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class PressAnyKeyToStart : MonoBehaviour
{
    private float SelfAlivetime = 0;
    [SerializeField] private GameObject[] flashes;
    [SerializeField] private GameObject end;

    // Store the base colors of the images
    private Color[] baseColors;

    void Start()
    {
        // Initialize the base colors array
        baseColors = new Color[flashes.Length];
        for (int i = 0; i < flashes.Length; i++)
        {
            baseColors[i] = flashes[i].GetComponent<Image>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        end.SetActive(false);
        SelfAlivetime += Time.deltaTime;

        for (int i = 0; i < flashes.Length; i++)
        {
            Image image = flashes[i].GetComponent<Image>();
            Color tempColor = baseColors[i]; // Start with the base color

            if (SelfAlivetime > ((i + 1) * 2) - 1)
            {
                tempColor.a = Mathf.Clamp01(SelfAlivetime - (((i + 1) * 2) - 1));
            }
            else
            {
                tempColor.a = 0; // Ensure alpha is 0 before animation starts
            }

            if (SelfAlivetime > (i + 1) * 2)
            {
                tempColor.a = Mathf.Clamp01((((i + 1) * 2) + 1) - SelfAlivetime);
            }

            image.color = tempColor; // Apply the animated color
        }

        if (SelfAlivetime > (flashes.Length + 1) * 2)
        {
            end.SetActive(true);
            end.transform.localPosition = Vector3.zero;
            end.transform.position += new Vector3(0, Mathf.Cos(Time.time * 1.8f) * 0.2f, 0);
        }

        if (Input.anyKey)
        {
            SceneManager.LoadScene("Base");
        }
    }
}
