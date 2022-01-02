using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Scriptable Objects/Game Settings", order = 0)]
public class GameSettingsAsset : ScriptableObject {
    public List<GameObject> levels;
}