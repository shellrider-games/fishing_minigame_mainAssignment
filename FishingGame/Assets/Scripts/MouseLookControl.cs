using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookControl : MonoBehaviour
{
    [SerializeField][Range(0.1f, 1f)] private float lookSpeed = 1f;
    [SerializeField] private float maxVertical = 15f;
    [SerializeField] private float maxHorizontal = 15f;

    public void OnLookAround(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Vector2 delta = ctx.ReadValue<Vector2>();
        Vector3 nextRotation = new Vector3(-delta.y, delta.x, 0) * lookSpeed + transform.localRotation.eulerAngles;
        if (!(nextRotation.x < maxVertical || nextRotation.x > 360 - maxVertical))
        {
            nextRotation.x = nextRotation.x < 180 ? maxVertical : 360-maxVertical;
        }

        if (!(nextRotation.y < maxHorizontal || nextRotation.y > 360 - maxHorizontal))
        {
            nextRotation.y = nextRotation.y < 180 ? maxHorizontal : 360 - maxHorizontal;
        }
        transform.localRotation = Quaternion.Euler(nextRotation);
    }
}
