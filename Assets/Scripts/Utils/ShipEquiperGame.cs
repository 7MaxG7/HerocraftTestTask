using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEquiperGame : MonoBehaviour {
    void Awake() {
        if (!ShipEquiperGameStatic.GameInitialized) {
            TEventManager<bool>.Initialize();
            ScreenUtils.Initialize();
        }
    }
}
