using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Defines moving ship during selection
/// </summary>
public class ShipSelector : TEventInvoker<bool> {
    Rigidbody2D ship_RBcomp;
    List<GridBlock> childBlocks = new List<GridBlock>();
    List<SpriteRenderer> blocks_SRcomp = new List<SpriteRenderer>();
    int movingFramesNumber = 15;
    float chosenShipLiftDistance = 1.5f;
    float transparentBlocksAlfa = 0.15f;
    Vector3 shipChoosingScreenPosition = new Vector3(0, 0, 0);

    protected override void Awake() {
        base.Awake();
        ship_RBcomp = GetComponent<Rigidbody2D>();

        events.Add(EventNameEnum.SwitchModulesRigBodySimulation, new BoolEvent());
        events.Add(EventNameEnum.FullnessChecked, new BoolEvent());
        events.Add(EventNameEnum.ButtonsActivationSwitch, new BoolEvent());
        TEventManager<bool>.AddInvoker(EventNameEnum.SwitchModulesRigBodySimulation, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.FullnessChecked, this);
        TEventManager<bool>.AddInvoker(EventNameEnum.ButtonsActivationSwitch, this);

        TEventManager<bool>.AddListener(EventNameEnum.ChoosenShipIsEquipedClick, LiftShip);
        TEventManager<bool>.AddListener(EventNameEnum.CheckShipsFullness, CheckShipEquiping);
    }
    void Start() {
        foreach (Transform block in transform) {
            childBlocks.Add(block.GetComponent<GridBlock>());
            blocks_SRcomp.Add(block.GetComponent<SpriteRenderer>());
        }
    }

    public void ShipAppearance() {
        gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(shipChoosingScreenPosition));
    }
    public void ShipHiding(Vector3 targetPosition) {
        events[EventNameEnum.ButtonsActivationSwitch].Invoke(false);
        StartCoroutine(MoveToTarget(targetPosition));
    }
    /// <summary>
    /// Moves the ship up a little after choosing or down after aquipping
    /// </summary>
    /// <param name="liftDown">True if the ship should lift down and false for lifting up</param>
    public void LiftShip(bool liftDown) {
        if (gameObject.activeSelf) {
            if (liftDown) {
                events[EventNameEnum.SwitchModulesRigBodySimulation].Invoke(false);
            }
            Vector3 targetPosition = transform.position;
            if (liftDown) {
                targetPosition.y -= chosenShipLiftDistance;
            } else {
                targetPosition.y += chosenShipLiftDistance;
            }
            SetBlocksAlfa(liftDown);
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }
    IEnumerator MoveToTarget(Vector3 targetPosition) {
        Vector3 moveStepPosition = (targetPosition - transform.position) / movingFramesNumber;
        float stepDistance = Mathf.Sqrt(Mathf.Pow(moveStepPosition.x, 2) + Mathf.Pow(moveStepPosition.y, 2));
        while (transform.position != targetPosition) {
            if (stepDistance > Vector2.Distance(targetPosition, transform.position)) {
                moveStepPosition = targetPosition - transform.position;
            }
            ship_RBcomp.MovePosition(transform.position + moveStepPosition);
            yield return new WaitForEndOfFrame();
        }
        if (targetPosition.x > ScreenUtils.ScreenRight || targetPosition.x < ScreenUtils.ScreenLeft) {
            gameObject.SetActive(false);
        } else if (targetPosition != shipChoosingScreenPosition) {
            events[EventNameEnum.SwitchModulesRigBodySimulation].Invoke(true);
        }
        yield return new WaitForEndOfFrame();
        events[EventNameEnum.ButtonsActivationSwitch].Invoke(true);
    }
    /// <summary>
    /// Sets the block's color transparent
    /// </summary>
    /// <param name="makeTransparent">True is the blocks should be transparent and false if it shouldn't</param>
    void SetBlocksAlfa(bool makeTransparent) {
        if (gameObject.activeSelf) {
            float blocksAlfa;
            if (makeTransparent) {
                blocksAlfa = transparentBlocksAlfa;
            } else {
                blocksAlfa = 1;
            }
            StopCoroutine("SetBlocksAlfa");
            StartCoroutine(SmoothSetBlockAlfa(blocksAlfa));
        }
    }
    IEnumerator SmoothSetBlockAlfa(float targetAlfa) {
        float alfaChangingStep = (targetAlfa - blocks_SRcomp[0].color.a) / movingFramesNumber;
        while (blocks_SRcomp[0].color.a != targetAlfa) {
            if (Mathf.Abs(targetAlfa - blocks_SRcomp[0].color.a) < Mathf.Abs(alfaChangingStep)) {
                alfaChangingStep = targetAlfa - blocks_SRcomp[0].color.a;
            }
            Color color_tmp;
            foreach (var block_SRcomp in blocks_SRcomp) {
                color_tmp = block_SRcomp.color;
                color_tmp.a += alfaChangingStep;
                block_SRcomp.color = color_tmp;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// Returns true if there's no free blocks on the ship
    /// </summary>
    void CheckShipEquiping(bool noValue) {
        if (gameObject.activeSelf == true) {
            foreach (var block in childBlocks) {
                if (block.Empty == true) {
                    events[EventNameEnum.FullnessChecked].Invoke(false);
                    return;
                }
            }
            events[EventNameEnum.FullnessChecked].Invoke(true);
        }
    }
}
