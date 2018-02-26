using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour {
    public static ComboManager current;

    public KeyCode[] allowedKeys;

    private string display;

    public Combo[] combos;
    private List<KeyCode> currentCombo;

    public float m_AutoResetTime;
    private float currentAutoResetDuration;

    private float currentComboCost;

    public void Awake() {
        current = this;
        currentCombo = new List<KeyCode>();
    }

    private void Update() {
        currentAutoResetDuration += Time.deltaTime;

        if(currentAutoResetDuration >= m_AutoResetTime && currentCombo.Count > 0)
        {
            Debug.Log("COMBO LOST...");
            currentCombo.Clear();
            currentComboCost = 0;
        }

        foreach (var key in allowedKeys) {
            if(Input.GetKeyDown(key))
            {
                float cost = 1 * GameManager.current.manaCostFactor;
                GameManager.current.mana -= cost;
                if (GameManager.current.mana < 0) {
                    Debug.Log(GameManager.current.noManaText);
                    return;
                }
                currentComboCost += cost;
                currentCombo.Add(key);
                currentAutoResetDuration = 0f;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            currentComboCost = 0;
            display = "NOPE";
            foreach (var item in combos) {
                if (item.Check(currentCombo)) {
                    display = item.onFinished;
                    //FIRE DA COMBO MY BRUDA
                    break;
                }
            }
            Debug.Log(display);
            currentCombo.Clear();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentCombo.Clear();
            Debug.Log("CLEAR COMBO !");
            GameManager.current.RefillMana(currentComboCost);
            currentComboCost = 0;
        }
    }
}
