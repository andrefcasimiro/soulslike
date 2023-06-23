using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    public CharacterManager characterManager;

    public Vector3 moveDirection;
    public LayerMask groundLayer;

    public const string ANIMATOR_PARAM_IS_GROUNDED = "isGrounded";
    public const string ANIMATOR_PARAM_IN_AIR_TIMER = "inAirTimer";

    [Header("Gravity Settings")]
    public float inAirTimer;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float groundedYVelocity = -20; // The force applied to you whilst grounded
    [SerializeField] protected float fallStartVelocity = -7; // The force applied when you begin to fall (increases over time)
    [SerializeField] protected float gravityForce = -25;
    [SerializeField] float groundCheckSphereRadius = 1f;
    protected bool fallingVelocitySet = false;

    protected virtual void Update()
    {
        characterManager.isGrounded = Physics.CheckSphere(characterManager.transform.position,
            groundCheckSphereRadius, groundLayer);

        characterManager.animator.SetBool(ANIMATOR_PARAM_IS_GROUNDED, characterManager.isGrounded);

        HandleGroundCheck();
    }

    public virtual void HandleGroundCheck()
    {
        if (characterManager.isGrounded)
        {
            if (yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocitySet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if (!fallingVelocitySet)
            {
                fallingVelocitySet = true;
                yVelocity.y = fallStartVelocity;
            }

            inAirTimer += Time.deltaTime;
            yVelocity.y += gravityForce * Time.deltaTime;
        }

        characterManager.characterController.Move(yVelocity * Time.deltaTime);

        characterManager.animator.SetFloat(ANIMATOR_PARAM_IN_AIR_TIMER, inAirTimer);
    }


}
