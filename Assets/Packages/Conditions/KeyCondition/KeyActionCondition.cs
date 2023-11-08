using UnityEngine;
using LTA.Condition;
using System;
using System.Collections.Generic;
[System.Serializable]
public class KeyConditionInfo
{
    public string key;
}

[ComponentType(typeof(KeyActionCondition),typeof(KeyConditionInfo),"KeyCondition","conditions")]
public class KeyActionCondition : MonoBehaviour,ICondition,ISetInfo,IResetCondition
{
    static Dictionary<string,Action> dic_Key_Bool = new Dictionary<string,Action>();

    public static void Action(string key)
    {
        if (!dic_Key_Bool.ContainsKey(key)) return;
        dic_Key_Bool[key]();
    }

    KeyConditionInfo info;

    public object Info
    {
        set
        {
            info = (KeyConditionInfo)value;
            if (!dic_Key_Bool.ContainsKey(info.key))
                dic_Key_Bool.Add(info.key, OnAction);
            else
                dic_Key_Bool[info.key] += OnAction;
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

    void OnAction()
    {
        IsSuitable = true;
        isSuitable = false;
    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }

    private void OnDestroy()
    {
        dic_Key_Bool[info.key] -= OnAction;
    }

    public void ResetCondition()
    {
        isSuitable = false;
    }
}
