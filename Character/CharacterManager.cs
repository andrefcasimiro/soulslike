using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Components")]
    public CharacterController characterController;
    public Animator animator;

    public CharacterAnimatorManager characterAnimatorManager;

    [Header("Lock On Transform")]
    public Transform lockOnTransform;

    [Header("Interaction")]
    public bool isInteracting;

    [Header("Movement Flags")]
    public bool canRotate;
    public bool isGrounded;

    [Header("Status Flags")]
    public bool isDead;

    protected virtual void Update()
    {
        canRotate = animator.GetBool(characterAnimatorManager.ANIMATOR_PARAM_CAN_ROTATE);
        isInteracting = animator.GetBool(characterAnimatorManager.ANIMATOR_PARAM_IS_INTERACTING);
    }
}
