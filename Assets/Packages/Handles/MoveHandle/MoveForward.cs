using UnityEngine;
using LTA.Handle;
using System;

[ComponentType(typeof(MoveForward),"MoveForward","handles")]
public class MoveForward : MonoBehaviour,IHandle
{
    Action<IHandle> endHandle;

    public Action<IHandle> EndHandle { 
        set => endHandle = value;
    }
    IMove move;

    IMove Move
    {
        get
        {
            if (move == null)
                move = GetComponent<IMove>();
            return move;
        }
    }

    public void Handle()
    {
        Move.Move(transform.forward);
        OnEndHandle();
    }

    void OnEndHandle() {
        if (endHandle != null)
        {
            endHandle(this);
        }
    }
}
