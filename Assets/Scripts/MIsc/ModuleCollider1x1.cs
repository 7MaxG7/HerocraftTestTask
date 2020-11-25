using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCollider1x1 : ModuleCollider {
    protected override void Start() {
        moduleSize = 1;
        base.Start();
    }
}
