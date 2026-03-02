using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (movement.isMoving) return;

        Vector3 dir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector3.right;      

        if (dir != Vector3.zero)
        {
            // 1. 여기서 이번 턴에 쓸 바구니(TurnData)를 미리 만듭니다.
            TurnData currentTurn = new TurnData();

            // 2. CanMove에 이 바구니를 넘깁니다. (블록들이 이 바구니에 자기 위치를 기록함)
            if (movement.CanMove(dir, currentTurn))
            {
                // 3. MoveTo에도 같은 바구니를 넘깁니다.
                StartCoroutine(movement.MoveTo(dir, currentTurn));
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            movement.MoveBack();
        }

    }

}
