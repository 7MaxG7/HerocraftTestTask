using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulesPanel : MonoBehaviour {
    [SerializeField] RectTransform mainPanel, gunsPanel, armorPanel, toolsPanel
            , ballisticGunsPanel, plasmaGunsPanel, explosiveGunsPanel
            , lightArmorPanel, advancedArmorPanel
            , radarsPanel, enginesPanel;
    ScrollRect modulesPanel_SRcomp;
    void Start() {
        modulesPanel_SRcomp = GetComponent<ScrollRect>();
        modulesPanel_SRcomp.content = mainPanel;
    }

    void PanelButtonClick(RectTransform newPanel) {
        modulesPanel_SRcomp.content.gameObject.SetActive(false);
        modulesPanel_SRcomp.content = newPanel;
        modulesPanel_SRcomp.content.gameObject.SetActive(true);
    }
    public void OnClickMainPanelButton() {
        PanelButtonClick(mainPanel);
    }
    public void OnClickGunsPanelButton() {
        PanelButtonClick(gunsPanel);
    }
    public void OnClickArmorPanelButton() {
        PanelButtonClick(armorPanel);
    }
    public void OnClickToolsPanelButton() {
        PanelButtonClick(toolsPanel);
    }
    public void OnClickBallisticGunsPanelButton() {
        PanelButtonClick(ballisticGunsPanel);
    }
    public void OnClickPlasmaGunsPanelButton() {
        PanelButtonClick(plasmaGunsPanel);
    }
    public void OnClickExplosiveGunsPanelButton() {
        PanelButtonClick(explosiveGunsPanel);
    }
    public void OnClickLightArmorPanelButton() {
        PanelButtonClick(lightArmorPanel);
    }
    public void OnClickAdvancedArmorPanelButton() {
        PanelButtonClick(advancedArmorPanel);
    }
    public void OnClickRadarToolsPanelButton() {
        PanelButtonClick(radarsPanel);
    }
    public void OnClickEngineToolsPanelButton() {
        PanelButtonClick(enginesPanel);
    }
}
