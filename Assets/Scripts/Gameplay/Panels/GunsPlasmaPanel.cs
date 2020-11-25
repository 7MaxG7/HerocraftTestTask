using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsPlasmaPanel : ModulePanelParent {
    protected override void Start() {
        modulesNumber = 3;
        modulePrefs = new GameObject[modulesNumber];
        modulePrefs[0] = Resources.Load<GameObject>(@"Prefabs/Modules/32Plasma1Pref");
        modulePrefs[1] = Resources.Load<GameObject>(@"Prefabs/Modules/32Plasma2Pref");
        modulePrefs[2] = Resources.Load<GameObject>(@"Prefabs/Modules/32Plasma3Pref");

        base.Start();
    }
    protected override void BackButtonOnClick() {
        transform.parent.gameObject.GetComponent<ModulesPanel>().OnClickGunsPanelButton();
    }
}
