using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager current;

    public float mana = 10f;
    public float manaMax = 10f;
    public float manaRegenFactor = 1f;
    public float manaCostFactor = 1f;

    public string noManaText = "NO MANA";

    public bool m_Empty = false;
    public float m_EmptyReloadTime = 2f;
    private float emptyDuration = 0f;

    private void Awake() {
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {

        //EMPTY CHECK
        if (mana <= 0 && !m_Empty)
        {
            m_Empty = true;
            emptyDuration = 0f;
        }

        //EMPTY RELOAD
        if (m_Empty)
        {
            emptyDuration += Time.deltaTime;
            if (emptyDuration >= m_EmptyReloadTime)
            {
                m_Empty = false;
                mana = 1f;
            }
        }

        //REGEN
        if (!m_Empty)
        {
            mana += 1f * manaRegenFactor * Time.deltaTime;
        }


        mana = Mathf.Clamp(mana, 0f, manaMax);
    }


}
