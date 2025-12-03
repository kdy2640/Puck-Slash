using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public PlayerController player;
    public EnemySpawner Spawner;
    public ArenaData Arena;
    // Start is called before the first frame update
    void Start()
    {
        LoadArena(Arena);
    }


    public void LoadArena(ArenaData data)
    {
        foreach (var entry in data.objects)
        {
            // Resources 폴더에서 프리팹 로드
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{entry.prefabName}");

            if (prefab == null)
            {
                Debug.LogError($"프리팹 '{entry.prefabName}' 을(를) 찾을 수 없음!");
                continue;
            }

            GameObject obj = Instantiate(prefab, entry.position, Quaternion.identity);
            obj.name = prefab.name; // 깔끔하게 이름 정리
        }
    }
}
