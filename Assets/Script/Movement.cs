using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isMoving = false;
    private Vector3 curpos, targetpos;
    private float timeToMove = 0.2f;

    public IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        curpos = transform.position;
        targetpos = new Vector3(Mathf.Round(curpos.x + direction.x), Mathf.Round(curpos.y + direction.y), Mathf.Round(curpos.z + direction.z));

        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(curpos, targetpos, elapsedTime / timeToMove);
            yield return null;
        }

        transform.position = targetpos;
        isMoving = false;
    }
}
