using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEquipingSceneUI : TEventInvoker<bool> {
    [SerializeField] Text message;
    [SerializeField] Button finishEquipingButton;
    Timer messageTimer;
    float messageDisappearingFramesSpeed = 60;
    private void Start() {
        events.Add(EventNameEnum.ChoosenShipIsEquipedClick, new BoolEvent());
        events.Add(EventNameEnum.CheckShipsFullness, new BoolEvent());
        TEventManager<bool>.AddInvoker(EventNameEnum.ChoosenShipIsEquipedClick, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.CheckShipsFullness, this);

        TEventManager<bool>.AddListener(EventNameEnum.FullnessChecked, FullnessCheckComplete);
        TEventManager<bool>.AddListener(EventNameEnum.ButtonsActivationSwitch, FinishButtonSwitchActivation);

        message.text = "Equip your ship";
        messageTimer = gameObject.AddComponent<Timer>();
        messageTimer.Duration = 2;
        messageTimer.AddTimerFinishedListener(HideMessage);
        messageTimer.Run();
    }

    public void OnClickFinishEquipButton() {
        events[EventNameEnum.CheckShipsFullness].Invoke(false);
    }
    /// <summary>
    /// Goes from equiping to choosing screen button is the ship is fully equipped or just shiow the message if not
    /// </summary>
    void FullnessCheckComplete(bool shipIsFull) {
        if (shipIsFull) {
            events[EventNameEnum.ChoosenShipIsEquipedClick].Invoke(true);
            MenuManager.GoTo(MenuEnum.ShipChoosingScene);
        } else {
            if (message.color.a < 1) {
                StopCoroutine(MessageDisappearing());
                Color color_tmp = message.color;
                color_tmp.a = 1;
                message.color = color_tmp;
            }

            messageTimer.Stop();
            message.text = "Your ship still has free space...";
            messageTimer.Duration = 2;
            messageTimer.AddTimerFinishedListener(HideMessage);
            messageTimer.Run();
        }
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
    void FinishButtonSwitchActivation(bool active) {
        finishEquipingButton.interactable = active;
    }
}
