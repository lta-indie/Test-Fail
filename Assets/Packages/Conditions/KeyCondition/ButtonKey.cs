using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.UI;
public class ButtonKey : BaseButtonController
{
    [SerializeField]
    string key;
    protected override void OnClick()
    {
        KeyActionCondition.Action(key);
    }
}
