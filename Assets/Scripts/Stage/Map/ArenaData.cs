using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArenaObjectEntry
{
    public string prefabName;   // Resources 폴더 기준 프리팹 이름
    public Vector3 position;    // 배치할 위치
}

[CreateAssetMenu(fileName = "ArenaData", menuName = "PuckSlash/ArenaData")]
public class ArenaData : ScriptableObject
{
    public List<ArenaObjectEntry> objects = new();
}
