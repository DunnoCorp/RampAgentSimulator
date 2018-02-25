using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour {
    public static ComboManager current;

    public KeyCode[] allowedKeys;

    private string display;

    public Combo[] combos;
    private List<KeyCode> currentCombo;

    public void Awake() {
        current = this;
        currentCombo = new List<KeyCode>();
    }

    private void Update() {
        foreach (var key in allowedKeys) {
            if(Input.GetKeyDown(key))
            {
                GameManager.current.mana -= 1 * GameManager.current.manaCostFactor;
                if (GameManager.current.mana < 0) {
                    Debug.Log(GameManager.current.noManaText);
                    return;
                }
                currentCombo.Add(key);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            display = "NOPE";
            foreach (var item in combos) {
                if (item.Check(currentCombo)) {
                    display = item.onFinished;
                    break;
                }
            }
            Debug.Log(display);
            currentCombo.Clear();
        }
    }
}
