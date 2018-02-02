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

    private void Awake() {
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        mana += 1f * manaRegenFactor * Time.deltaTime;
        mana = Mathf.Clamp(mana, 0f, manaMax);
    }


}
