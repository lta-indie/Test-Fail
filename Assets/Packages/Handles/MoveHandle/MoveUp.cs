using UnityEngine;
using LTA.Handle;
using System;

[ComponentType(typeof(MoveUp), "MoveUp", "handles")]
public class MoveUp : MonoBehaviour,IHandle
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
        Debug.Log("Move Up");
        Move.Move(transform.up);
        OnEndHandle();
    }

    void OnEndHandle() {
        if (endHandle != null)
        {
            endHandle(this);
        }
    }
}
