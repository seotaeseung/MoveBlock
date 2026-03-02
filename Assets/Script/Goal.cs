using UnityEngine;

public class Goal : MonoBehaviour
{
    public LayerMask blockLayer;
    private bool isCleared = false;
    void Update()
    {
        // 1. 골 지점 중심에서 위(Vector3.up) 방향으로 짧은 레이를 쏩니다.
        // 시작점: 내 위치에서 살짝 위(0.1f), 길이: 0.5f
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.up, out hit, 0.5f, blockLayer))
        {
            if (!isCleared)
            {
                isCleared = true;
                Debug.Log("★ Goal Reached! 블록이 위에 있습니다. ★");
            }
        }
        else
        {
            isCleared = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.up * 0.5f);
    }
}
