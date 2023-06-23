using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX = 0f;
    public float mouseY = 0f;

    Vector2 movementInput = Vector2.zero;
    [HideInInspector] public Vector2 cameraInput;

    [Header("Flags")]
    public bool rollFlag;

    public PlayerManager playerManager;
    public PlayerControl playerControl;

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControl();

            playerControl.PlayerMovement.Movement.performed += i =>
                movementInput = i.ReadValue<Vector2>();

            playerControl.PlayerMovement.Camera.performed += i =>
                cameraInput = i.ReadValue<Vector2>();

            playerControl.PlayerActions.Dodge.performed += i => rollFlag = true;
            playerControl.PlayerActions.Dodge.canceled += i => rollFlag = false;

        }

        playerControl.Enable();
    }

    private void OnDisable()
    {
        if (playerControl == null)
        {
            return;
        }

        playerControl.Disable();
    }

    public void TickInput()
    {
        if (playerManager.isDead)
        {
            return;
        }

        HandleMoveInput();

    }

    void HandleMoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }


}
