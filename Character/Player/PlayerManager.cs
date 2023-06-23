using System.Collections;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("Camera")]
    public PlayerCameraManager playerCameraManager;

    [Header("Input")]
    public PlayerInputHandler playerInputHandler;

    public PlayerLocomotionManager playerLocomotionManager;

    public PlayerAnimatorManager playerAnimatorManager;

    protected override void Update()
    {
        base.Update();

        playerInputHandler.TickInput();

        playerLocomotionManager.HandleGroundedMovement();
        playerLocomotionManager.HandleRotation();
        playerLocomotionManager.HandleRollingAndSprinting();
    }

}
