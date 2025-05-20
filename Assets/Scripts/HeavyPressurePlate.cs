using System.Collections.Generic;
using UnityEngine;

public class HeavyPressurePlate : MonoBehaviour
{
    [SerializeField] private Sprite inactive;
    [SerializeField] private Sprite active;
    public bool isactive = false;
    [SerializeField] private GameObject bars;
    [SerializeField] private List<IDoor> doors;
    [SerializeField] private List<PressurePlate> plateConnection;
    private int triggerCount = 0;
    private Coroutine checkCoroutine;
    private void Start()
    {
        isactive = false;
        bars.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Golem")) return;
<<<<<<< ours
        if (triggerCount >= 1)
||||||| ancestor
        if (triggerCount >= activatibleCount)
=======
        triggerCount++;
        if (triggerCount >= activatibleCount)
>>>>>>> theirs
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Golem")) return;
        triggerCount = Mathf.Max(0, triggerCount - 1);
        if (triggerCount < 1 && isactive)
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
        bars.SetActive(false);
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
            checkCoroutine = null;
        }
        foreach (var door in doors)
        {
            // door.open = false;
            door.SwitchOpen();
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
                if (!plate.isactive)
                {
                    connectionsMet = false;
                    break;
                }
            }
            if (connectionsMet != lastConnectionsMet)
            {
                bars.SetActive(connectionsMet);
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
