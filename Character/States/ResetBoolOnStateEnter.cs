using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    CharacterAnimatorManager characterAnimatorManager;

    public bool isInteractingStatus = false;
    public bool canRotateStatus = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterAnimatorManager == null)
        {
            characterAnimatorManager = animator.GetComponent<CharacterAnimatorManager>();
        }

        animator.SetBool(characterAnimatorManager.ANIMATOR_PARAM_IS_INTERACTING, isInteractingStatus);
        animator.SetBool(characterAnimatorManager.ANIMATOR_PARAM_CAN_ROTATE, canRotateStatus);
    }
}
