using System.Collections;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    public PlayerManager playerManager;

    [Header("Movement Stats")]
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float walkingSpeed = 1;
    [SerializeField] float sprintSpeed = 7;
    [SerializeField] float rotationSpeed = 10;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void HandleGroundedMovement()
    {
        if (playerManager.isInteracting)
        {
            return;
        }

        if (!playerManager.isGrounded)
        {
            return;
        }

        moveDirection = mainCamera.transform.forward * playerManager.playerInputHandler.vertical;

        moveDirection += mainCamera.transform.right * playerManager.playerInputHandler.horizontal;

        moveDirection.Normalize();
        moveDirection.y = 0;

        if (playerManager.playerInputHandler.moveAmount > 0.5f)
        {
            playerManager.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        }
        else if (playerManager.playerInputHandler.moveAmount <= 0.5f)
        {
            playerManager.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }

        playerManager.playerAnimatorManager.UpdateAnimatorValues(playerManager.playerInputHandler.moveAmount, 0, false);

    }

    public void HandleRotation()
    {
        if (!playerManager.canRotate)
        {
            return;
        }

        Vector3 targetDir = Vector3.zero;
        float moveOverride = playerManager.playerInputHandler.moveAmount;

        targetDir = mainCamera.transform.forward * playerManager.playerInputHandler.vertical;
        targetDir += mainCamera.transform.right * playerManager.playerInputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = playerManager.transform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, rs * Time.deltaTime);

        playerManager.transform.rotation = targetRotation;
    }

    public void HandleRollingAndSprinting()
    {
        if (!playerManager.playerInputHandler.rollFlag)
        {
            return;
        }

        if (playerManager.isInteracting)
        {
            return;
        }

        playerManager.playerInputHandler.rollFlag = false;


        playerManager.playerAnimatorManager.PlayTargetAnimation(
            playerManager.playerAnimatorManager.ANIMATOR_CLIP_BACKSTEP, true);
    }

}
