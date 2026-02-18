using UnityEngine;
using UnityEngine.InputSystem;
public class MoveTarget : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    private Vector2 dir;
    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
