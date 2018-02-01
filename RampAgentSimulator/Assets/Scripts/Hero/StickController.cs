using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickController : MonoBehaviour {

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;

    public Tweener m_PositionTweener;
    public Tweener m_RotationTweener;

    public float m_ReturnToIdleTime;
    public Ease m_ReturnToIdleAnim;

    public float m_WaitTime;

    public void Start()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation.eulerAngles;
    }

    public void Move(Vector3 toPos, Vector3 toRot, float time, Ease animation)
    {
        if(m_PositionTweener != null)
        {
            m_PositionTweener.Kill();
        }
        if(m_RotationTweener != null)
        {
            m_RotationTweener.Kill();
        }

        m_PositionTweener = transform.DOLocalMove(toPos, time).SetEase(animation).OnComplete(delegate
        {
            m_PositionTweener = transform.DOLocalMove(toPos, m_WaitTime).OnComplete(delegate
            {
                m_PositionTweener = transform.DOLocalMove(defaultPosition, m_ReturnToIdleTime).SetEase(m_ReturnToIdleAnim);
            });
        });

        m_RotationTweener = transform.DOLocalRotate(toRot, time).SetEase(animation).OnComplete(delegate
        {
            m_RotationTweener = transform.DOLocalRotate(toRot, m_WaitTime).OnComplete(delegate
            {
                m_RotationTweener = transform.DOLocalRotate(defaultRotation, m_ReturnToIdleTime).SetEase(m_ReturnToIdleAnim);
            });
        });
    }

}
