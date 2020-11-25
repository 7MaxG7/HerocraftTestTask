using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsBallisticPanel : ModulePanelParent {
    protected override void Start() {
        modulesNumber = 6;
        modulePrefs = new GameObject[modulesNumber];
        modulePrefs[0] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret1Pref");
        modulePrefs[1] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret2Pref");
        modulePrefs[2] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret3Pref");
        modulePrefs[3] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret4Pref");
        modulePrefs[4] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret5Pref");
        modulePrefs[5] = Resources.Load<GameObject>(@"Prefabs/Modules/31Turret6Pref");

        base.Start();
    }
    protected override void BackButtonOnClick() {
        transform.parent.gameObject.GetComponent<ModulesPanel>().OnClickGunsPanelButton();
    }
}
