using System;
using UnityEngine;

public class IDoor : MonoBehaviour, IDoorInterface
{
    public bool open = false;

    public virtual void SwitchOpen()
    {
        throw new NotImplementedException();
    }
}

public interface IDoorInterface
{
    void SwitchOpen();
}