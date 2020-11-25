using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipChooserSceneUI : TEventInvoker<bool> {
    [SerializeField] Button prevShipButton, nextShipButton, chooseShipButton;
    [SerializeField] Text message;
    Timer messageTimer;
    float messageDisappearingFramesSpeed = 60;

    protected override void  Awake() {
        base.Awake();
        events.Add(EventNameEnum.PrevShipClick, new BoolEvent());
        events.Add(EventNameEnum.NextShipClick, new BoolEvent());
        events.Add(EventNameEnum.ChoosenShipIsEquipedClick, new BoolEvent());
        events.Add(EventNameEnum.ButtonsNeedActivationCheck, new BoolEvent());
        TEventManager<bool>.AddInvoker(EventNameEnum.PrevShipClick, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.NextShipClick, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.ChoosenShipIsEquipedClick, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.ButtonsNeedActivationCheck, this);

        TEventManager<bool>.AddListener(EventNameEnum.PrevButtonActivationSwitch, PrevShipButtonActivation);
        TEventManager<bool>.AddListener(EventNameEnum.NextButtonActivationSwitch, NextShipButtonActivation);
        TEventManager<bool>.AddListener(EventNameEnum.ButtonsActivationSwitch, SwitchButtonsActivation);
    }
    void Start() {
        message.text = "Choose your ship";
        messageTimer = gameObject.AddComponent<Timer>();
        messageTimer.Duration = 2;
        messageTimer.Run();
        messageTimer.AddTimerFinishedListener(HideMessage);
    }

    public void OnClickPrevShipButton() {
        events[EventNameEnum.PrevShipClick].Invoke(false);
    }
    public void OnClickNextShipButton() {
        events[EventNameEnum.NextShipClick].Invoke(false);
    }
    public void OnClickEquipShipButton() {
        events[EventNameEnum.ChoosenShipIsEquipedClick].Invoke(false);
    }
    void PrevShipButtonActivation(bool enableButton) {
        prevShipButton.interactable = enableButton;
    }
    void NextShipButtonActivation(bool enableButton) {
        nextShipButton.interactable = enableButton;
    }
    void HideMessage() {
        StartCoroutine(MessageDisappearing());
        messageTimer.Stop();
    }
    IEnumerator MessageDisappearing() {
        float disappearingAlfaStep = message.color.a / messageDisappearingFramesSpeed;
        Color color_tmp = message.color;
        while (message.color.a > 0) {
            color_tmp = message.color;
            color_tmp.a -= disappearingAlfaStep;
            message.color = color_tmp;
            yield return new WaitForEndOfFrame();
        }
        message.text = "";
        color_tmp.a = 1;
        message.color = color_tmp;
    }
    void SwitchButtonsActivation(bool active) {
        PrevShipButtonActivation(active);
        NextShipButtonActivation(active);
        chooseShipButton.interactable = active;
        if (active) {
            events[EventNameEnum.ButtonsNeedActivationCheck].Invoke(false);
        }
    }
}
