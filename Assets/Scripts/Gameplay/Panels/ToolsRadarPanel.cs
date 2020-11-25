using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsRadarPanel : ModulePanelParent {
    protected override void Start() {
        modulesNumber = 2;
        modulePrefs = new GameObject[modulesNumber];
        modulePrefs[0] = Resources.Load<GameObject>(@"Prefabs/Modules/22RadarPref");
        modulePrefs[1] = Resources.Load<GameObject>(@"Prefabs/Modules/22UpgradedRadarPref");

        base.Start();
    }
    protected override void BackButtonOnClick() {
        transform.parent.gameObject.GetComponent<ModulesPanel>().OnClickToolsPanelButton();
    }
}
