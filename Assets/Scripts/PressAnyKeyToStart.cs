using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] List<Image> hagekors;

    void Update()
    {
        GetComponent<TextMeshProUGUI>().color = new Color(Time.time*0.6f, Time.time*0.6f, Time.time*0.6f);
        transform.localPosition = Vector3.zero;
        transform.position += new Vector3(0, Mathf.Cos(Time.time*2.5f)*15f, 0);
        if (Time.time > 0 && Time.time < 2.2f)
        {
            if (Time.time < 1)
            {
                hagekors.GetComponent<Image>().color = new Color(1, 1, 1, (Time.time));
            }
            else
            {
                hagekors.GetComponent<Image>().color = new Color(1, 1, 1, (Time.time));
            }
        }
        if (Input.anyKey)
        {
            GetComponent<TextMeshProUGUI>().text = "";
            SceneManager.LoadScene("LevelManager", LoadSceneMode.Single);
        }
    }
}
