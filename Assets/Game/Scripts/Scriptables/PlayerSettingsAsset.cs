using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "Scriptable Objects/Player Settings", order = 1)]
public class PlayerSettingsAsset : ScriptableObject {
    public List<GameObject> models;
}