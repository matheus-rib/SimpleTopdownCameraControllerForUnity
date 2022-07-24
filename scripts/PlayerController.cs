using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Tooltip("Player walking speed (Running speed will multiply it for 2).")]
  [Range(0, Mathf.Infinity)]
  public float speed = 10.0f;
  [Tooltip("The gravity force that will be applied to you Player to prevent it goes off the ground when going down a ramp or change angles abruptly.")]
  [Range(0, Mathf.Infinity)]
  public float gravity = 100f;
  [Tooltip("Player's Gameobject rotation speed when changing directions.")]
  [Range(0, Mathf.Infinity)]
  public float rotationSpeed = 360f;

  // Components
  private CharacterController controller;
  private Animator animator;

  // Attributes
  private bool isWalking = false;
  private bool isRunning = false;
  private float gravityInfluence;
  private float effectiveSpeed;
  private Vector3 movement;

  void Start()
  {
    gravityInfluence = gravity * Time.deltaTime * -1;
    controller = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    // Set all values for movement and animation.
    UpdatePlayerValues();
    PlayerMovement();
    AnimationManagement();
  }

  private void UpdatePlayerValues()
  {
    isWalking = getIsWalking();
    isRunning = getIsRunning();
    effectiveSpeed = getEffectiveSpeed();

    float horizontalAxis = Input.GetAxis("Horizontal");
    float verticalAxis = Input.GetAxis("Vertical");

    movement = new Vector3(horizontalAxis, gravityInfluence, verticalAxis);
  }

  private void PlayerMovement()
  {
    controller.Move(movement * effectiveSpeed * Time.deltaTime);

    // Handle Gameobject smoothly rotation.
    if (isWalking)
    {
      movement.y = 0;
      Quaternion toRotation = Quaternion.LookRotation(movement);
      transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

  }

  private void AnimationManagement()
  {
    animator.SetBool("isWalking", isWalking);
    animator.SetBool("isRunning", isRunning);
  }

  private bool getIsWalking()
  {
    return controller.velocity.magnitude > 0;
  }

  private bool getIsRunning()
  {
    return isWalking && Input.GetKey("left shift");
  }

  private float getEffectiveSpeed()
  {
    return isRunning ? speed * 2 : speed;
  }
}
