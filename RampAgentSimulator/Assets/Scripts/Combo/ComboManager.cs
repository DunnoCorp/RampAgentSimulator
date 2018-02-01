using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour {
    public static ComboManager current;

    public KeyCode[] allowedKeys;

    public string display;

    public Combo[] combos;
    public List<KeyCode> currentCombo;

    public void Awake() {
        current = this;
        currentCombo = new List<KeyCode>();
    }

    private void Update() {
        foreach (var key in allowedKeys) {
            if(Input.GetKeyDown(key)) {
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
