using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Combo/New Combo")]
public class Combo : ScriptableObject {
    public KeyCode[] keys;
    public string onFinished;

    public bool Check(List<KeyCode> combo) {
        return keys.SequenceEqual(combo);
    }
}