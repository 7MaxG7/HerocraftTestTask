using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePositioner : MonoBehaviour {
    [SerializeField] ModuleCollider moduleCollider;
    SpriteRenderer SRcomp;
    Rigidbody2D RBcomp;
    float halfColliderWidth, halfColliderHeight;
    public GameObject modulePanel { get; set; }

    private void Start() {
        TEventManager<bool>.AddListener(EventNameEnum.SwitchModulesRigBodySimulation, SwitchRigidBodySimulation);

        SRcomp = GetComponent<SpriteRenderer>();
        RBcomp = GetComponent<Rigidbody2D>();
        halfColliderWidth = GetComponent<BoxCollider2D>().size.x / 2;
        halfColliderHeight = GetComponent<BoxCollider2D>().size.y / 2;
    }

    private void OnMouseDrag() {
        Vector3 position_tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position_tmp.z = ScreenUtils.CamZ;
        transform.position = position_tmp;
    }
    protected void OnMouseDown() {
        SRcomp.sortingOrder = 3;
        moduleCollider.beforeDruggingPosition = transform.position;
        StartCoroutine(moduleCollider.SetHoldedBlocksEmptiness(true));
        StartCoroutine(moduleCollider.ColorBlocks(Color.white));
    }
    protected void OnMouseUp() {
        SRcomp.sortingOrder = 0;
        transform.position = moduleCollider.SetAfterDruggingPosition();
        MoveCollidedModules();
        StartCoroutine(moduleCollider.SetHoldedBlocksEmptiness(false));
        StartCoroutine(moduleCollider.ColorBlocks(Color.green));
    }
    /// <summary>
    /// If there are other modules on the blocks this module is going to be putted on other modules just move to panel (or delete if the panel was updated)
    /// </summary>
    public void MoveCollidedModules() {
        Collider2D[] otherModules = Physics2D.OverlapAreaAll(
                new Vector2(transform.position.x - halfColliderWidth, transform.position.y - halfColliderHeight)
                , new Vector2(transform.position.x + halfColliderWidth, transform.position.y + halfColliderHeight)
                , LayerMask.GetMask("Equipment")
        );
        foreach (var moduleCol in otherModules) {
            if (moduleCol.gameObject != transform.gameObject) {
                moduleCol.GetComponent<ModulePositioner>().ReturnModuleToPool();
            }
        }
    }
    void ReturnModuleToPool() {
        moduleCollider.ClearBlocks();
        if (modulePanel != null) {
            modulePanel.GetComponent<ModulePanelParent>().MoveModuleToPanel(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void SwitchRigidBodySimulation(bool simulate) {
        RBcomp.simulated = simulate;
    }
}
