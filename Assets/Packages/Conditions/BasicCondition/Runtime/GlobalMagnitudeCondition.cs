using UnityEngine;
using LTA.Condition;
using System;

[System.Serializable]
public class GlobalMagnitudeConditionInfo : CompareValue
{
    public string name;
}

[ComponentType(typeof(GlobalMagnitudeCondition),typeof(GlobalMagnitudeConditionInfo),"GlobalMagnitudeCondition","conditions")]
public class GlobalMagnitudeCondition : MonoBehaviour,ICondition,ISetInfo
{
    GlobalMagnitudeConditionInfo info;

    public object Info
    {
        set
        {
            info = (GlobalMagnitudeConditionInfo)value;
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

    bool isProcessing = false;

    void Update()
    {
        //if (isProcessing) return;
        //isProcessing = true;
        Vector3 direction = GlobalInput.GetDirection(info.name);
        IsSuitable = CompareHelper.Compare(info, direction.magnitude);
        //isProcessing = false;
    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
