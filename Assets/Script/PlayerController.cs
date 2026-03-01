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
            StartCoroutine(movement.MovePlayer(dir));
        }
    }

}
