using UnityEngine;
using LTA.Handle;
using System;

[System.Serializable]
public class MoveFollowJoyStickHandleInfo
{
    public string typeJoyStick = "Move";
    public string typeMove = TypeMove.Move3D;
}

[ComponentType(typeof(MoveFollowJoyStickHandle),typeof(MoveFollowJoyStickHandleInfo),"MoveFollowJoyStickHandle","handles")]
public class MoveFollowJoyStickHandle : MonoBehaviour,IHandle,ISetInfo
{
    MoveFollowJoyStickHandleInfo info;

    public object Info
    {
        set
        {
            info = (MoveFollowJoyStickHandleInfo)value;
        }
    }

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
            direction = new Vector3(direction.x,0,direction.y);
        }
        Move.Move(direction);
        OnEndHandle();
    }

    void OnEndHandle() {
        if (endHandle != null)
        {
            endHandle(this);
        }
    }
}
