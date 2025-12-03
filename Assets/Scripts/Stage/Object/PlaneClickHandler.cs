using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneClickHandler : MonoBehaviour, IPointerClickHandler
{

    private GameManager manager;
    private ProjectilePool pool;
    [SerializeField] private GameObject player;
    void Start()
    {
        manager = GameManager.Instance;
        pool = manager.Stage.ObjectPooler.GetComponent<ProjectilePool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 playerPos = player.transform.position;
        float camDist = Mathf.Abs(Camera.main.transform.position.y - playerPos.y);

        // 화면 클릭 → 월드 좌표 변환
        Vector3 worldClickPos = Camera.main.ScreenToWorldPoint(
            new Vector3(eventData.position.x, eventData.position.y, camDist)
        );

        // 방향 계산 (XZ 평면)
        Vector2 direction = new Vector2(
            worldClickPos.x - playerPos.x,
            worldClickPos.z - playerPos.z
        );

        LaserBehaviour temp = (LaserBehaviour)pool.Get();
        temp.transform.position = playerPos;
        temp.SetValue(direction.normalized, 10f);
    }

}
