using UnityEngine;
using LTA.Condition;
using System;

[System.Serializable]
public class RigibodyVelocityConditionInfo : CompareDirectionInfo
{
    
}

[ComponentType(typeof(RigibodyVelocityCondition),typeof(RigibodyVelocityConditionInfo),"RigibodyVelocityCondition","conditions")]
public class RigibodyVelocityCondition : MonoBehaviour,ICondition,ISetInfo
{
    RigibodyVelocityConditionInfo info;

    public object Info
    {
        set
        {
            info = (RigibodyVelocityConditionInfo)value;
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

    Rigidbody rigidbody3D;

    Rigidbody Rigidbody3D
    {
        get
        {
            if (rigidbody3D == null)
            {
                rigidbody3D = GetComponent<Rigidbody>();
                if (rigidbody3D == null)
                {
                    rigidbody3D = gameObject.AddComponent<Rigidbody>();
                }
                rigidbody3D.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            }
            return rigidbody3D;
        }
    }

    private void Update()
    {
        IsSuitable = CompareHelper.CompareDirection(Rigidbody3D.velocity, info);
    }

    public Action<ICondition> SuitableCondition { set => suitableCondition = value; }
}
