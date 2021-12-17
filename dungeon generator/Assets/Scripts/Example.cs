 using UnityEngine;

 public class Example : MonoBehaviour {
     //Variables
     public float speed = 6.0F;
     public float jumpSpeed = 8.0F; 
     public float gravity = 20.0F;
     private Vector3 _moveDirection = Vector3.zero;

     [SerializeField] private Culling culling;

     private void Update() {
         var controller = GetComponent<CharacterController>();
         // is the controller on the ground?
         if (controller.isGrounded) {
             //Feed moveDirection with input.
             _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
             _moveDirection = transform.TransformDirection(_moveDirection);
             //Multiply it by speed.
             _moveDirection *= speed;
             //Jumping
             if (Input.GetButton("Jump"))
                 _moveDirection.y = jumpSpeed;
             
         }
         //Applying gravity to the controller
         _moveDirection.y -= gravity * Time.deltaTime;
         //Making the character move
         controller.Move(_moveDirection * Time.deltaTime);
         culling.HandleEnabling();
     }
 }