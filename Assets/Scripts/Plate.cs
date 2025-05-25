using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Sprite inactive;
    [SerializeField] private Sprite active;
    public bool isactive = false;
    [SerializeField] private List<IDoor> doors;

    [SerializeField] private List<PlateConnection> plateConnection;
    private int triggerCount = 0;
    private Coroutine checkCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerCount++;
        if (triggerCount == 1)
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggerCount = Mathf.Max(0, triggerCount - 1);
        if (triggerCount == 0)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        GetComponent<SpriteRenderer>().sprite = active;
        isactive = true;
        if (checkCoroutine == null)
        {
            checkCoroutine = StartCoroutine(CheckConnectionsRoutine());
        }
    }

    private void Deactivate()
    {
        GetComponent<SpriteRenderer>().sprite = inactive;
        isactive = false;
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
            checkCoroutine = null;
            foreach (var door in doors)
            {
                if (door.open == true)
                {
                    door.SwitchOpen();
                }
            }
        }
    }

    private System.Collections.IEnumerator CheckConnectionsRoutine()
    {
        bool lastConnectionsMet = false;
        while (isactive)
        {
            bool connectionsMet = true;
            foreach (var plate in plateConnection)
            {
                if (!plate.plate.isactive != plate.inverse)
                {
                    connectionsMet = false;
                    break;
                }
            }
            if (connectionsMet != lastConnectionsMet)
            {
                foreach (var door in doors)
                {
                    door.SwitchOpen();
                }
                lastConnectionsMet = connectionsMet;
            }
            yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
        }
    }
}
