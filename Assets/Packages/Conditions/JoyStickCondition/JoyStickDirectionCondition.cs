using UnityEngine;
using LTA.Condition;
using System;



[System.Serializable]
public class JoyStickDirectionConditionInfo : CompareDirectionInfo
{
    public string typeJoyStick = "Move";
}

[ComponentType(typeof(JoyStickDirectionCondition),typeof(JoyStickDirectionConditionInfo),"JoyStickDirectionCondition","conditions")]
public class JoyStickDirectionCondition : MonoBehaviour,ICondition,ISetInfo, IOnJoyStickDrag, IOnJoyStickBeginDrag, IOnJoyStickEndDrag
{
    JoyStickDirectionConditionInfo info;

    public object Info
    {
        set
        {
            info = (JoyStickDirectionConditionInfo)value;
        }
    }
    
    Action<ICondition> suitableCondition;

    protected bool isSuitable = false;

    public bool IsSuitable {
        get
        {
            return isSuitable;
        }
        private set
        {
            if (isSuitable == value) return;
            isSuitable = value;
            if (suitableCondition != null)
            {
                suitableCondition(this);
            }
        }
    }
    void Start()
    {
        JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickBeginDrag(this);
        JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickDrag(this);
        JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickEndDrag(this);
    }

    private void OnDestroy()
    {
        JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickBeginDrag(this);
        JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickDrag(this);
        JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickEndDrag(this);
    }
    public void OnJoyStickBeginDrag(JoyStickContoller joyStick)
    {
        IsSuitable = CompareHelper.CompareDirection(joyStick.Direction,info);
    }

    public void OnJoyStickDrag(JoyStickContoller joyStick)
    {
        IsSuitable = CompareHelper.CompareDirection(joyStick.Direction, info);
    }

    public void OnJoyStickEndDrag(JoyStickContoller joyStick)
    {
        IsSuitable = CompareHelper.CompareDirection(joyStick.Direction, info);
    }


    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
