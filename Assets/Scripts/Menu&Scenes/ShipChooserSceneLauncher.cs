using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipChooserSceneLauncher : TEventInvoker<bool> {
    void Start() {
        if (!ShipEquiperGameStatic.GameInitialized) {
            events.Add(EventNameEnum.NextShipClick, new BoolEvent());
            TEventManager<bool>.AddInvoker(EventNameEnum.NextShipClick, this);
            events[EventNameEnum.NextShipClick].Invoke(false);
        } else {
            events.Add(EventNameEnum.ButtonsNeedActivationCheck, new BoolEvent());
            TEventManager<bool>.AddInvoker(EventNameEnum.ButtonsNeedActivationCheck, this);
            events[EventNameEnum.ButtonsNeedActivationCheck].Invoke(false);
        }
    }
}
