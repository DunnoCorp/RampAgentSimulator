using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(MeshRenderer))]
public class StickController : MonoBehaviour {

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;

    public Tweener m_PositionTweener;
    public Tweener m_RotationTweener;

    public float m_ReturnToIdleTime;
    public Ease m_ReturnToIdleAnim;

    public float m_WaitTime;

    public Light m_Light;
    public float m_MaxIntensity;

    private MeshRenderer stickRenderer;
    public Material m_OnMaterial;
    public Material m_OffMaterial;

    private TrailRenderer trail;
    public Material m_TrailOnMaterial;
    public Material m_TrailOffMaterial;

    public ParticleSystem m_Particles;

    public void Start()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation.eulerAngles;

        stickRenderer = GetComponent<MeshRenderer>();
        trail = GetComponent<TrailRenderer>();
    }

    public void Update()
    {
        m_Light.intensity = Mathf.Lerp(0, m_MaxIntensity, Mathf.InverseLerp(0, GameManager.current.manaMax, GameManager.current.mana));

        if (GameManager.current.m_Empty)
        {
            stickRenderer.material = m_OffMaterial;
            trail.material = m_TrailOffMaterial;
            if (m_Particles.isPlaying)
            {
                m_Particles.Stop();
            }
        } else
        {
            stickRenderer.material = m_OnMaterial;
            trail.material = m_TrailOnMaterial;
            if (!m_Particles.isPlaying)
            {
                m_Particles.Play();
            }
        }
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
