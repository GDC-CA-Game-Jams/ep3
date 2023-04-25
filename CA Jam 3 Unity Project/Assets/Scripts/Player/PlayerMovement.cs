using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("How much force to apply to the player for them to move")]
    [SerializeField] private float moveForce;

    [Tooltip("Fastest the player will be able to go")]
    [SerializeField] private float maxSpeed = 10;
    
    [Tooltip("How much force will be applied to the player when they jump")]
    [SerializeField] private float jumpForce = 10;

    [Tooltip("How much beyond the player the jump ray extends")]
    [SerializeField] private float jumpRayDistance = 1;
    
    /// <summary>
    /// The attached Animator. Used for the stretch and squish walking animation
    /// </summary>
    private Animator anim;
    
    /// <summary>
    /// Modification applied to the move force
    /// </summary>
    private float moveForceMod;

    /// <summary>
    /// Modification applied to the maximum speed
    /// </summary>
    private float maxSpeedMod;

    /// <summary>
    /// The attached rigidbody
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// The combined distance of the player's Y scale and the extension beyond that
    /// </summary>
    private float combinedRayLength;
    
    /// <summary>
    /// Modification applied additivly to the move force
    /// </summary>
    public float MoveForceMod
    {
        get => moveForceMod;
        set => moveForceMod = value;
    }

    /// <summary>
    /// Modification applied additivly to the maximum speed
    /// </summary>
    public float MaxSpeedMod
    {
        get => maxSpeedMod;
        set => maxSpeedMod = value;
    }
    
    /// <summary>
    /// Cache the rigidbody at start to use it later
    /// </summary>
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponentInChildren<Animator>();
        MoveForceMod = 10;
    }

    /// <summary>
    /// Move the player laterally
    /// </summary>
    void Update()
    {
        // Get the W/S and A/D axis
        float inlineMove = Input.GetAxisRaw("Vertical");
        float strafeMove = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Vert: " + inlineMove + "\nStrafe: " + strafeMove + "\nmoveForce: " + moveForce+ "\nmoveForceMod: " + moveForceMod);

        bool jump = Input.GetButton("Jump");
        Vector3 moveVec = Vector3.zero;

        // Set the Z and X to the correct values based on the buttons pressed
        moveVec.z = inlineMove * (moveForce + moveForceMod) * Time.deltaTime;
        moveVec.x = strafeMove * (moveForce + moveForceMod) * Time.deltaTime;

        combinedRayLength = jumpRayDistance + transform.localScale.y;
        
        // Show the jump ray in the editor window
        Debug.DrawRay(transform.position, Vector3.down * combinedRayLength, Color.green, 0.1f);

        // Jump if the player hit jump and they are on the ground
        if (jump && Physics.Raycast(transform.position, Vector3.down, combinedRayLength, ~LayerMask.NameToLayer("Default")))
        {
            moveVec.y = jumpForce;
        }
        
        // Clamp the speed to the maximum allowed
        moveVec = Vector3.ClampMagnitude(moveVec, (maxSpeed + maxSpeedMod));

        if (moveVec.magnitude != 0)
        {
            // Apply the force
            rb.AddRelativeForce(moveVec);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); 
        }
        
        anim.SetBool("Move", (rb.velocity.x != 0 || rb.velocity.z != 0));
    }
}
