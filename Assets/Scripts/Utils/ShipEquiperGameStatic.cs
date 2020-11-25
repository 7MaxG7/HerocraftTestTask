using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipEquiperGameStatic {
    public static bool GameInitialized { get; private set; } = false;

    public static void InitializeGame() {
        if (!GameInitialized) {
            GameInitialized = true;
        }
    }
}
