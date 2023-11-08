using UnityEngine;
using LTA.Condition;
using System;

[System.Serializable]
public class CountDownConditionInfo
{
    public float timeCountDown;

    public bool fistValue = false;

}

[ComponentType(typeof(CountDownCondition),typeof(CountDownConditionInfo),"CountDownCondition","conditions")]
public class CountDownCondition : MonoBehaviour,ICondition,ISetInfo,IResetCondition
{
    CountDownConditionInfo info;

    public float countDown = 0;

    public object Info
    {
        set
        {
            info = (CountDownConditionInfo)value;
            countDown = info.timeCountDown;
            Debug.Log("countDown" + countDown);
            IsSuitable = info.fistValue;
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

    void Update()
    {
        if (info == null) return;
        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;
            return;
        }
        IsSuitable = true;
    }

    public void ResetCondition()
    {
        countDown = info.timeCountDown;
        IsSuitable = false;

    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
