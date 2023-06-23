using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterAnimatorManager : MonoBehaviour
{
    public CharacterManager characterManager;

    [HideInInspector] public string ANIMATOR_PARAM_CAN_ROTATE = "canRotate";
    [HideInInspector] public string ANIMATOR_PARAM_IS_INTERACTING = "isInteracting";
    public const float CROSS_FADE_VALUE = 0.05f;

    // Animation Clips
    [HideInInspector] public string ANIMATOR_CLIP_BACKSTEP = "Backstep";
    [HideInInspector] public string ANIMATOR_CLIP_ROLL = "Roll";


    protected virtual void Awake()
    {
    }
    
    public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnim = false)
    {
        characterManager.animator.applyRootMotion = isInteracting;
        characterManager.animator.SetBool(ANIMATOR_PARAM_CAN_ROTATE, canRotate);
        characterManager.animator.SetBool(ANIMATOR_PARAM_IS_INTERACTING, isInteracting);
        characterManager.animator.CrossFade(targetAnim, CROSS_FADE_VALUE);
    }

    public virtual void EnableCanRotate()
    {
        characterManager.animator.SetBool(ANIMATOR_PARAM_CAN_ROTATE, true);
    }

    public virtual void DisableCanRotate()
    {
        characterManager.animator.SetBool(ANIMATOR_PARAM_CAN_ROTATE, false);
    }

    public virtual void OnAnimatorMove()
    {
        Vector3 velocity = characterManager.animator.deltaPosition;
        characterManager.characterController.Move(velocity);
        characterManager.transform.rotation *= characterManager.animator.deltaRotation;
    }
}
