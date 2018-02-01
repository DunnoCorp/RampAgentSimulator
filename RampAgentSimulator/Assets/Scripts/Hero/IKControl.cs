using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;

    public float m_PositionWeight;
    public float m_RotationWeight;

    public bool m_SpaceShipMode = false;

    public float m_LookWeight;
    public float m_BodyWeight;
    public float m_HeadWeight;
    public float m_EyesWeight;
    public float m_ClampWeight;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    //animator.SetLookAtWeight(1);
                    animator.SetLookAtWeight(m_LookWeight, m_BodyWeight, m_HeadWeight, m_EyesWeight, m_ClampWeight);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, m_PositionWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, m_RotationWeight);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                    if (m_SpaceShipMode)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_PositionWeight);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_RotationWeight);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightHandObj.rotation);
                    }
                }

                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, m_PositionWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, m_RotationWeight);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                    if (m_SpaceShipMode)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_PositionWeight);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_RotationWeight);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftHandObj.rotation);
                    }
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                if (m_SpaceShipMode)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                }
            }
        }
    }
}