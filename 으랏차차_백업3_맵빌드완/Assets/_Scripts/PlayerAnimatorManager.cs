using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Urachacha
{
	public class PlayerAnimatorManager : MonoBehaviourPun 
	{
        #region Private Fields

        [SerializeField]
	    private float directionDampTime = 0.25f;
        Animator animator;

		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
	    void Start () 
	    {
	        animator = GetComponent<Animator>();
	    }
	        
		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
	    void Update () 
	    {

			// Prevent control is connected to Photon and represent the localPlayer
	        if( photonView.IsMine == false && PhotonNetwork.IsConnected == true )
	        {
	            return;
	        }

			// failSafe is missing Animator component on GameObject
	        if (!animator)
	        {
				return;
			}

			// deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);			

			// only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))
            {
				// When using trigger parameter
                if (Input.GetButtonDown("Jump")) animator.SetTrigger("Jump");
			}
           
			// deal with movement
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

			// prevent negative Speed.
            if( v < 0 )
            {
                v = 0;
            }

			// set the Animator Parameters
            animator.SetFloat( "Speed", h*h+v*v );
            animator.SetFloat( "Direction", h, directionDampTime, Time.deltaTime );
	    }

		#endregion

	}
}