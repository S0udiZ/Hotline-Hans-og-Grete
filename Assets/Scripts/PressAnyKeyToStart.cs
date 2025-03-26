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

    // Update is called once per frame
    void Update()
    {
        end.SetActive(false);
        SelfAlivetime += Time.deltaTime;
        for (int i = 0; i < flashes.Length; i++)
        {
            flashes[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
            if (SelfAlivetime > ((i + 1) * 2) - 1)
            {
                flashes[i].GetComponent<Image>().color = new Color(1, 1, 1, SelfAlivetime - (((i + 1) * 2) - 1));
            }
            if (SelfAlivetime > (i + 1) * 2)
            {
                flashes[i].GetComponent<Image>().color = new Color(1, 1, 1, (((i + 1) * 2) + 1) - SelfAlivetime);
            }
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
