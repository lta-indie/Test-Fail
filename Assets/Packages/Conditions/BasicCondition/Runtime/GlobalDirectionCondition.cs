using UnityEngine;
using LTA.Condition;
using System;

[System.Serializable]
public class GlobalDirectionConditionInfo : CompareDirectionInfo
{
    public string name;
}

[ComponentType(typeof(GlobalDirectionCondition),typeof(GlobalDirectionConditionInfo),"GlobalDirectionCondition","conditions")]
public class GlobalDirectionCondition : MonoBehaviour,ICondition,ISetInfo,IOnChangeDirection
{
    GlobalDirectionConditionInfo info;

    public object Info
    {
        set
        {
            info = (GlobalDirectionConditionInfo)value;
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
        GlobalInput.AddOnChangeDirection(info.name,this);
    }

    private void OnDestroy()
    {
        GlobalInput.RemoveOnChangeDirection(info.name, this);
    }

    public void OnChangeDirection(Vector3 direction)
    {
        IsSuitable = CompareHelper.CompareDirection(direction, info);
    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
