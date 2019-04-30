using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/* SOME TUTORIALS USED!!!!!!
 * SEE README FOR DETAILS
 */

public class PlayerController : NetworkBehaviour
{
    // variables for running/walking
    private float walkSpeed = 5.0f;
    private float runSpeed = 11.0f;
    private float runSpeedBuildUp = 3.0f;
    Stamina myStamina;
    [SerializeField] private KeyCode runKey;

    // variable to hold current move speed
    private float currentSpeed;

    // used to stop jittering when moving down slopes
    private float slopeForce = 6.0f;
    private float slopeForceRayLength = 1.5f;

    private CharacterController control;
    public Animator anim;
    public Camera cam;
    public Light flashLight;

    private int soundWait = 10;
    private bool canPlay = true;

    // variables for jumping
    [SerializeField] private AnimationCurve jumpFallOff;
    private float jumpMulti = 6.0f;
    [SerializeField] private KeyCode jumpKey;
    private bool isJumping;

    // when game starts
    private void Awake()
    {
        // get player character controller
        control = GetComponent<CharacterController>();

        myStamina = GetComponent<Stamina>();
    }

    // executes every frame
    private void Update()
    {

        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }

        // check if player is currently running
        bool running = Input.GetKey(runKey);

        // call function that does movement
        PlayerMovement(running);

        // turn on/off flashlight
        if (Input.GetKeyDown(KeyCode.F))
            flashLight.enabled = FlashLightControl();
    }

    // handles movement
    private void PlayerMovement(bool running)
    {
        // store the buttons for movemement
        float ADInput = Input.GetAxis("Horizontal");
        float WSInput = Input.GetAxis("Vertical");

        // calculate positions of movement
        Vector3 forwardMovement = transform.forward * WSInput;
        Vector3 rightMovement = transform.right * ADInput;

        
        // actually move the player
        control.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * currentSpeed);

        // if on a slope, apply force to player to make it look like they really are on a slope
        if ((WSInput != 0 || ADInput != 0) && OnSlope())
            control.Move(Vector3.down * control.height / 2 * slopeForce * Time.deltaTime);

        // not pressing any movement buttons sets currentSpeed to 0
        if (WSInput == 0 && ADInput == 0)
            currentSpeed = 0;

        // if run key is pressed, currentSpeed is now runSpeed
        else if (running && myStamina.GetStamina() > 0)
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, Time.deltaTime * runSpeedBuildUp);

        // is walking
        else
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, Time.deltaTime * runSpeedBuildUp);

        // is running, take away stamina
        if (currentSpeed >= 6 && running)
            myStamina.deductStamina();
        else
        {
            myStamina.AddStamina();
        }

        // makes player jump
        JumpInput();

        // animate the player
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        anim.SetFloat("Speed", animationSpeedPercent, 0.1f, Time.deltaTime);
    }

    // when walking up or down slope
    private bool OnSlope()
    {
        // if jumping, not on slope
        if (isJumping)
            return false;

        RaycastHit hit;

        // shoot ray downwards from player to detect if currently on a slope
        if (Physics.Raycast(transform.position, Vector3.down, out hit, control.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        // if jump key gets pressed
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(jumpEvent());
        }
    }

    private IEnumerator jumpEvent()
    {
        control.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        // jump and stay in the air for a certain amount of time
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            control.Move(Vector3.up * jumpForce * jumpMulti * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;

            // while not grounded or not hitting the ceiling
        } while (!control.isGrounded && control.collisionFlags != CollisionFlags.Above);

        // jump has finished
        control.slopeLimit = 45.0f;
        isJumping = false;
    }

    private bool FlashLightControl()
    {
        return (flashLight.isActiveAndEnabled ? false: true);
    }
}
