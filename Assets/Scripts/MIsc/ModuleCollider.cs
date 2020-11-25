using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCollider : MonoBehaviour {
    protected int moduleSize;
    List<GameObject> holdedBlocks;
    public Vector3 beforeDruggingPosition;

    protected virtual void Start() {
        holdedBlocks = new List<GameObject>(moduleSize);
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Block" && !holdedBlocks.Contains(collision.gameObject)) {
            holdedBlocks.Add(collision.gameObject);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision) {
        holdedBlocks.Remove(collision.gameObject);
    }
    /// <summary>
    /// After finishing drugging counts new position or returns previous object's position if it's not putted to the ship's blocks
    /// </summary>
    public Vector3 SetAfterDruggingPosition() {
        if (holdedBlocks.Count == moduleSize) {
            Vector3 newPosition = new Vector3(0, 0, -1);

            GameObject parentPanel = transform.parent.parent.gameObject;
            //Check if module is on panel but at the ship and make the ship parent
            if (parentPanel != holdedBlocks[0].transform.parent.gameObject) {
                transform.parent.SetParent(holdedBlocks[0].transform.parent, false);
                transform.parent.localScale = new Vector3(1, 1, 1);
                Destroy(parentPanel);
            }
            foreach (var block in holdedBlocks) {
                newPosition.x += block.transform.position.x;
                newPosition.y += block.transform.position.y;
            }
            newPosition.x /= holdedBlocks.Count;
            newPosition.y /= holdedBlocks.Count;
            return newPosition;
        } else {
            return beforeDruggingPosition;
        }
    }
    public void ClearBlocks() {
        StartCoroutine(ColorBlocks(Color.white));
        StartCoroutine(SetHoldedBlocksEmptiness(true));
        holdedBlocks.Clear();
    }
    public IEnumerator ColorBlocks(Color newColor) {
        int framesLatencyBuffer = 4;
        for (int i = 0; i < framesLatencyBuffer; i++) {
            yield return new WaitForEndOfFrame();
        }
        if (holdedBlocks.Count != 0) {
            while (holdedBlocks.Count != moduleSize) {
                yield return new WaitForEndOfFrame();
            }
            foreach (var block in holdedBlocks) {
                block.GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }
    public IEnumerator SetHoldedBlocksEmptiness(bool isEmpty) {
        int framesLatencyBuffer = 4;
        for (int i = 0; i < framesLatencyBuffer; i++) {
            yield return new WaitForEndOfFrame();
        }
        foreach (var block in holdedBlocks) {
            block.GetComponent<GridBlock>().Empty = isEmpty;
        }
    }
}
