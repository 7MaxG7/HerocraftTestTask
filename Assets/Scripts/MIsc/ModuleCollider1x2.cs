using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCollider1x2 : ModuleCollider {
    protected override void Start() {
        moduleSize = 2;
        base.Start();
    }
}
