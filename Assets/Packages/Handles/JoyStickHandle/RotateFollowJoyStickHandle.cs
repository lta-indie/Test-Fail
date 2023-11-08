using UnityEngine;
using LTA.Handle;
using System;

[System.Serializable]
public class RotateFollowJoyStickHandleInfo
{
    public string typeJoyStick = "Move";
    public string typeMove = TypeMove.Move3D;
}

[ComponentType(typeof(RotateFollowJoyStickHandle),typeof(RotateFollowJoyStickHandleInfo),"RotateFollowJoyStickHandle","handles")]
public class RotateFollowJoyStickHandle : MonoBehaviour,IHandle,ISetInfo
{
    RotateFollowJoyStickHandleInfo info;

    public object Info
    {
        set
        {
            info = (RotateFollowJoyStickHandleInfo)value;
        }
    }

    Action<IHandle> endHandle;

    public Action<IHandle> EndHandle { 
        set => endHandle = value;
    }

    JoyStickContoller joyStick;

    JoyStickContoller JoyStick
    {
        get
        {
            if (joyStick == null)
                joyStick = JoyStickContoller.GetJoyStick(info.typeJoyStick);
            return joyStick;
        }
    }

    public void Handle()
    {
        Vector3 direction = JoyStick.Direction;
        if (info.typeMove == TypeMove.Move3D)
        {
            direction = new Vector3(direction.x, 0, direction.y);
        }
        transform.forward = Vector3.Lerp(transform.forward,direction,0.1f);
        OnEndHandle();
    }

    void OnEndHandle() {
        if (endHandle != null)
        {
            endHandle(this);
        }
    }
}
