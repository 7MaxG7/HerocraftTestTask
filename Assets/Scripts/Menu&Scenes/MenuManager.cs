using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager {
    public static void GoTo(MenuEnum menu) {
        switch(menu) {
            case MenuEnum.ShipChoosingScene:
                SceneManager.LoadScene("01_ShipChoosing");
                break;
            case MenuEnum.EquippingScene:
                SceneManager.LoadScene("02_ShipEquiping");
                break;
        }
    }
}
