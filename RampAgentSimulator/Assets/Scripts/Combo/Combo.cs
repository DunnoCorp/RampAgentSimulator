using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Combo {
    public KeyCode[] keys;
    public string onFinished;

    public bool Check(List<KeyCode> combo) {
        return keys.SequenceEqual(combo);
    }

}
