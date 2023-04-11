using System.Collections;
using System.Collections.Generic;
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

    [Tooltip("How long the jump ray is. When this hits the ground the player will be able to jump again")]
    [SerializeField] private float jumpRayDistance = 1;
    
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
    }

    /// <summary>
    /// Move the player laterally
    /// TODO: Add a jump
    /// </summary>
    void Update()
    {
        // Get the W/S and A/D axis
        float inlineMove = Input.GetAxisRaw("Vertical");
        float strafeMove = Input.GetAxisRaw("Horizontal");
        bool jump = Input.GetButton("Jump");
        Vector3 moveVec = Vector3.zero;

        // Set the Z and X to the correct values based on the buttons pressed
        moveVec.z = inlineMove * (moveForce + moveForceMod) * Time.deltaTime;
        moveVec.x = strafeMove * (moveForce + moveForceMod) * Time.deltaTime;
        
        // Show the jump ray in the editor window
        Debug.DrawRay(transform.position, Vector3.down * jumpRayDistance, Color.green, 0.1f);
        // Jump if the player hit jump and they are on the ground
        if (jump && Physics.Raycast(transform.position, Vector3.down, jumpRayDistance, ~LayerMask.NameToLayer("Default")))
        {
            Debug.Log("Jumping!");
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
    }
}
