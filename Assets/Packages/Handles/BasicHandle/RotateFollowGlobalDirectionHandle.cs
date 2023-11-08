using UnityEngine;
using LTA.Handle;
using System;

[System.Serializable]
public class RotateFollowGlobalDirectionHandleInfo
{
    public string name = "PlayerInput";
    public string typeMove = TypeMove.Move3D;
}

[ComponentType(typeof(RotateFollowGlobalDirectionHandle),typeof(RotateFollowGlobalDirectionHandleInfo),"RotateFollowGlobalDirectionHandle","handles")]
public class RotateFollowGlobalDirectionHandle : MonoBehaviour,IHandle,ISetInfo
{
    RotateFollowGlobalDirectionHandleInfo info;

    public object Info
    {
        set
        {
            info = (RotateFollowGlobalDirectionHandleInfo)value;
        }
    }

    Action<IHandle> endHandle;

    public Action<IHandle> EndHandle { 
        set => endHandle = value;
    }

    public void Handle()
    {
        Vector3 direction = GlobalInput.GetDirection(info.name);
        if (info.typeMove == TypeMove.Move3D)
        {
            direction = new Vector3(direction.x, 0, direction.y);
        }
        transform.forward = Vector3.Lerp(transform.forward, direction, 0.1f);
        OnEndHandle();
    }

    void OnEndHandle() {
        if (endHandle != null)
        {
            endHandle(this);
        }
    }
}
