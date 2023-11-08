using LTA.Entity;
using LTA.NonEntity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    // Start is called before the first frame update
    void Start()
    {
        typeData = "players";
        NonEntityInfo info = new NonEntityInfo();
        info.name = "BlackKnight";
        info.level = 1;
        NonEntityInfo = info;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        GlobalInput.ChangeDirection("PlayerInput", new Vector3(horizontal,vertical));
    }
}
