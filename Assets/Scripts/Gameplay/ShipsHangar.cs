using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsHangar : TEventInvoker<bool> {
    int shipsNum = 3;
    GameObject[] ships;
    ShipSelector[] shipSelectors;

    int currentShipNum = -1;
    float shipHidingAppearanceDelay = 0.2f;
    public Vector3 LeftOutsidePosition { get; private set; }
    public Vector3 RightOutsidePosition { get; private set; }

    protected override void Awake() {
        if (!ShipEquiperGameStatic.GameInitialized) {
            base.Awake();

            ships = new GameObject[shipsNum];
            shipSelectors = new ShipSelector[shipsNum];

            events.Add(EventNameEnum.PrevButtonActivationSwitch, new BoolEvent());
            events.Add(EventNameEnum.NextButtonActivationSwitch, new BoolEvent());
            TEventManager<bool>.AddInvoker(EventNameEnum.PrevButtonActivationSwitch, this);
            TEventManager<bool>.AddInvoker(EventNameEnum.NextButtonActivationSwitch, this);

            TEventManager<bool>.AddListener(EventNameEnum.PrevShipClick, ShowPreviousShip);
            TEventManager<bool>.AddListener(EventNameEnum.NextShipClick, ShowNextShip);
            TEventManager<bool>.AddListener(EventNameEnum.ChoosenShipIsEquipedClick, GoToShipEquippinig);
            TEventManager<bool>.AddListener(EventNameEnum.ButtonsNeedActivationCheck, CheckAndDeactivateArrows);

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void Start() {
        LeftOutsidePosition = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width * 0.5f, Screen.height * .5f, -Camera.main.transform.position.z));
        RightOutsidePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1.5f, Screen.height * .5f, -Camera.main.transform.position.z));

        ships[0] = Instantiate(Resources.Load<GameObject>(@"Prefabs/Ships/ShipTitanPref"), RightOutsidePosition, Quaternion.identity);
        ships[1] = Instantiate(Resources.Load<GameObject>(@"Prefabs/Ships/ShipBlazerPref"), RightOutsidePosition, Quaternion.identity);
        ships[2] = Instantiate(Resources.Load<GameObject>(@"Prefabs/Ships/ShipRitterPref"), RightOutsidePosition, Quaternion.identity);
        for (int i = 0; i < ships.Length; ++i) {
            DontDestroyOnLoad(ships[i]);
            shipSelectors[i] = ships[i].GetComponent<ShipSelector>();
            ships[i].SetActive(false);
        }
    }

    void ShowNextShip(bool noValue) {
        // Hide current ship if it's not level start - else disables PrevShip button
        if (currentShipNum >= 0) {
            shipSelectors[currentShipNum].ShipHiding(LeftOutsidePosition);
        }
        ++currentShipNum;
        StartCoroutine(ShipAppearanceWithDelay(shipHidingAppearanceDelay));
    }
    void ShowPreviousShip(bool noValue) {
        shipSelectors[currentShipNum].ShipHiding(RightOutsidePosition);
        --currentShipNum;
        StartCoroutine(ShipAppearanceWithDelay(shipHidingAppearanceDelay));
    }
    IEnumerator ShipAppearanceWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        shipSelectors[currentShipNum].ShipAppearance();
    }
    /// <summary>
    /// Finish choosing and start ship equiping
    /// </summary>
    void GoToShipEquippinig(bool noValue) {
        ShipEquiperGameStatic.InitializeGame();
        MenuManager.GoTo(MenuEnum.EquippingScene);
    }
    /// <summary>
    /// Checks if next and previous ship buttons are needed to be deactivated and realizes it
    /// </summary>
    void CheckAndDeactivateArrows(bool noValue) {
        if (currentShipNum == 0) {
            events[EventNameEnum.PrevButtonActivationSwitch].Invoke(false);
        } else if (currentShipNum == ships.Length - 1) {
            events[EventNameEnum.NextButtonActivationSwitch].Invoke(false);
        }
    }
}
