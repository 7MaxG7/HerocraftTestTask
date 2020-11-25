using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ModulePanelParent : MonoBehaviour {
    GameObject modulePanelPref, backButtonPref;
    protected int modulesNumber;
    protected GameObject[] modulePrefs;
    //GameObject[] modulePanels;

    protected virtual void Start() {
        modulePanelPref = Resources.Load<GameObject>(@"Prefabs/Panels/CellPanelPref");
        backButtonPref = Resources.Load<GameObject>(@"Prefabs/Panels/BackPanelPref");

        GameObject backButton = Instantiate(backButtonPref, transform);
        backButton.GetComponent<Button>().onClick.AddListener(BackButtonOnClick);

        GameObject module;
        //modulePanels = new GameObject[modulesNumber];
        GameObject canvas = transform.parent.parent.gameObject;
        float moduleScale = 1 / canvas.transform.localScale.x;
        for (int i = 0; i < modulesNumber; ++i) {
            GameObject modulePanel = Instantiate(modulePanelPref, transform);
            module = Instantiate(modulePrefs[i], modulePanel.transform);
            module.transform.localScale = new Vector3(moduleScale, moduleScale, moduleScale);
            module.GetComponent<ModulePositioner>().modulePanel = gameObject;
        }
    }

    protected abstract void BackButtonOnClick();
    public void MoveModuleToPanel(GameObject module) {
        GameObject newModulePanel = Instantiate(modulePanelPref, transform);
        module.transform.SetParent(newModulePanel.transform);
        module.transform.localPosition = new Vector3(0, 0, -1);
    }
}
