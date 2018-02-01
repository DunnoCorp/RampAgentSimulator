using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public Movement[] m_Moves;

    public StickController m_LeftStick;
    public StickController m_RightStick;
    public Transform m_LookAt;

    public float m_MoveTime;
    public Ease m_MoveAnim;

    public bool m_MoveLookAt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Movement m in m_Moves)
        {
            if (Input.GetKeyDown(m.m_Key))
            {
                m_LeftStick.Move(m.m_LeftHandPos, m.m_LeftHandRot, m_MoveTime, m_MoveAnim);
                m_RightStick.Move(m.m_RightHandPos, m.m_RightHandRot, m_MoveTime, m_MoveAnim);
                break;
            }
        }
        if (m_MoveLookAt)
        {
            m_LookAt.transform.localPosition = Vector3.Lerp(m_LeftStick.transform.localPosition, m_RightStick.transform.localPosition, 0.5f);
        }
    }
}
