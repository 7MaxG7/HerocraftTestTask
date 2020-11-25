using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCollider2x1 : ModuleCollider {
    protected override void Start() {
        moduleSize = 2;
        base.Start();
    }
}
