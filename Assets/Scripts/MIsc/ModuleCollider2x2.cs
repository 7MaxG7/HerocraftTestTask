using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCollider2x2 : ModuleCollider {
    protected override void Start() {
        moduleSize = 4;
        base.Start();
    }
}
