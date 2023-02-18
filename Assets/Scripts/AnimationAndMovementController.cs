// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class AnimationAndMovementController : MonoBehaviour
// {
//     void Update()
//     {
        
//         HandleAnimation();
//         if(isRunPressed)
//         {
//             appliedMovement.x = currentRunMovement.x;
//             appliedMovement.z = currentRunMovement.z;
//         }
//         else
//         {
//             appliedMovement.x = currentMovement.x;
//             appliedMovement.z = currentMovement.z;
//         }

        

//         HandleGravity();
//         HandleJump();
//     }

    

//     void HandleJump()
//     {
//         if(!isJumping && characterController.isGrounded && isJumpPressed)
//         {
            
//         }
//         else if(!isJumpPressed && isJumping && characterController.isGrounded)
//         {
//             isJumping = false;
//         }
//     }

    

    

//     void HandleAnimation()
//     {
//         isWalking = animator.GetBool(isWalkingHash);
//         isRunning = animator.GetBool(isRunningHash);

//         if(isMovementPressed && !isWalking)
//         {
//             animator.SetBool(isWalkingHash, true);
//         }
//         else if (!isMovementPressed && isWalking)
//         {
//             animator.SetBool(isWalkingHash, false);
//         }

//         if((isMovementPressed && isRunPressed) && !isRunning)
//         {
//             animator.SetBool(isRunningHash, true);
//         }
//         else if(!isRunPressed && isRunning)
//         {
//             animator.SetBool(isRunningHash, false);
//         }
//     }

    

    

    
// }
