using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsExplosivePanel : ModulePanelParent {
    protected override void Start() {
        modulesNumber = 1;
        modulePrefs = new GameObject[modulesNumber];
        modulePrefs[0] = Resources.Load<GameObject>(@"Prefabs/Modules/33BombPref");

        base.Start();
    }
    protected override void BackButtonOnClick() {
        transform.parent.gameObject.GetComponent<ModulesPanel>().OnClickGunsPanelButton();
    }
}
