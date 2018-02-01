using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moves/New Move")]
public class MoveScriptable : ScriptableObject {

    public Vector3 m_LeftHandPos;
    public Vector3 m_RightHandPos;
    public Vector3 m_LeftHandRot;
    public Vector3 m_RightHandRot;

    public KeyCode m_Key;

}
