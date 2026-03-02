using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class MoveRecord
{
    public Movement obj;      // 움직인 대상 (플레이어 혹은 블록)
    public Vector3 prevPos;   // 움직이기 전 위치
}

[System.Serializable]
public class TurnData
{
    public List<MoveRecord> records = new List<MoveRecord>();
}

public class Movement : MonoBehaviour
{
    public bool isMoving = false;
    private float timeToMove = 0.2f;

    public static Stack<TurnData> undoStack = new Stack<TurnData>();

    public LayerMask wallLayer;

    public LayerMask breakLayer;

    public bool CanMove(Vector3 direction, TurnData currentTurn)
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.2f;
        RaycastHit hit;

        if (Physics.Raycast(rayStart, direction, out hit, 1f, wallLayer)) return false;

        if (Physics.Raycast(rayStart, direction, out hit, 1f, breakLayer))
        {
            Movement breakblock = hit.collider.GetComponent<Movement>();
            if (breakblock != null)
            {
                // 블록이 움직일 수 있는지 '연쇄적으로' 확인 (블록 앞이 비었는지)
                if (breakblock.CanMove(direction, currentTurn))
                {
                    StartCoroutine(breakblock.MoveTo(direction, currentTurn));
                    return true;
                }
            }
            return false; // 블록 뒤가 막혀있으면 나도 못 감
        }
        return true;
    }

    public IEnumerator MoveTo(Vector3 direction, TurnData currentTurn = null)
    {
        currentTurn.records.Add(new MoveRecord { obj = this, prevPos = transform.position });

        isMoving = true;
        
        Vector3 startpos = transform.position;
        Vector3 targetpos = new Vector3(Mathf.Round(startpos.x + direction.x), Mathf.Round(startpos.y + direction.y), Mathf.Round(startpos.z + direction.z));
        float elapsedTime = 0;
        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, targetpos, elapsedTime / timeToMove);
            yield return null;
        }

        transform.position = targetpos;
        isMoving = false;

        undoStack.Push(currentTurn);

    }
    public void MoveBack()
    {
        if (undoStack.Count == 0) return;

        isMoving = true;
        TurnData lastTurn = undoStack.Pop();
        foreach (MoveRecord record in lastTurn.records)
        {
            record.obj.transform.position = record.prevPos;
            record.obj.isMoving = false;
            record.obj.StopAllCoroutines();
        }
        isMoving = false;
    }
}
