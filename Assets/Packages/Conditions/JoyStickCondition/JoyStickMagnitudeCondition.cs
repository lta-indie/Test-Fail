using UnityEngine;
using LTA.Condition;
using System;

[System.Serializable]
public class JoyStickMagnitudeConditionInfo : CompareValue
{
    public string typeJoyStick = "Move";
}

[ComponentType(typeof(JoyStickMagnitudeCondition),typeof(JoyStickMagnitudeConditionInfo),"JoyStickMagnitudeCondition","conditions")]
public class JoyStickMagnitudeCondition : MonoBehaviour,ICondition,ISetInfo,IResetCondition
{
    JoyStickMagnitudeConditionInfo info;

    public object Info
    {
        set
        {
            info = (JoyStickMagnitudeConditionInfo)value;
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

    private void Update()
    {
        IsSuitable = CompareHelper.Compare(info, JoyStick.Direction.magnitude);
    }

    //void Start()
    //{
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickBeginDrag(this);
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickDrag(this);
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).AddOnJoyStickEndDrag(this);
    //}

    //private void OnDestroy()
    //{
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickBeginDrag(this);
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickDrag(this);
    //    JoyStickContoller.GetJoyStick(info.typeJoyStick).RemoveOnJoyStickEndDrag(this);
    //}
    //public void OnJoyStickBeginDrag(JoyStickContoller joyStick)
    //{
    //    IsSuitable = CompareHelper.Compare(info,joyStick.Direction.magnitude);
    //}

    //public void OnJoyStickDrag(JoyStickContoller joyStick)
    //{
    //    IsSuitable = CompareHelper.Compare(info, joyStick.Direction.magnitude);
    //}

    //public void OnJoyStickEndDrag(JoyStickContoller joyStick)
    //{
    //    IsSuitable = CompareHelper.Compare(info, joyStick.Direction.magnitude);
    //}

    public void ResetCondition()
    {
        isSuitable = false;
    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
