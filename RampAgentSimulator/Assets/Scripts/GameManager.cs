using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float m_EmptyRefillAmount = 4f;

    public Slider m_ManaBar;
    public Image m_ManaBarFill;
    public Color m_ManaBarColorOn;
    public Color m_ManaBarColorOff;

    private void Awake() {
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {

        //EMPTY CHECK
        if (mana <= 0 && !m_Empty)
        {
            m_Empty = true;
            m_ManaBarFill.color = m_ManaBarColorOff;
            emptyDuration = 0f;
        }

        //EMPTY RELOAD
        if (m_Empty)
        {
            emptyDuration += Time.deltaTime;

            float lerper = emptyDuration / m_EmptyReloadTime;

            m_ManaBar.value = Mathf.Lerp(0, m_EmptyRefillAmount / manaMax, lerper);

            if (emptyDuration >= m_EmptyReloadTime)
            {
                m_Empty = false;
                m_ManaBarFill.color = m_ManaBarColorOn;
                mana = m_EmptyRefillAmount;
            }
        }

        //REGEN
        if (!m_Empty)
        {
            mana += 1f * manaRegenFactor * Time.deltaTime;
            m_ManaBar.value = mana / manaMax;
        }


        mana = Mathf.Clamp(mana, 0f, manaMax);
    }

    public void RefillMana(float amount)
    {
        mana += amount;
        mana = Mathf.Clamp(mana, 0f, manaMax);

        if(amount > 0)
        {
            m_Empty = false;
            m_ManaBarFill.color = m_ManaBarColorOn;
        }
    }


}
