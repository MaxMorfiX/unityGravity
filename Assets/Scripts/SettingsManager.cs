using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {

    static public void ToggleLayerCollision(byte layerA, byte layerB) {
        SetLayerCollision(layerA, layerB, !GetLayerCollision(layerA, layerB));
    }
    static public void SetLayerCollision(byte layerA, byte layerB, bool val) {
        Debug.Log(val);
        Physics2D.IgnoreLayerCollision(layerA, layerB, !val);
    }
    static public void EnableLayerCollision(byte layerA, byte layerB) {
        SetLayerCollision(layerA, layerB, false);
    }
    static public void DisableLayerCollision(byte layerA, byte layerB) {
        SetLayerCollision(layerA, layerB, true);
    }
    static public bool GetLayerCollision(byte layerA, byte layerB) {
        return !Physics2D.GetIgnoreLayerCollision(layerA, layerB);
    }

}
