using System.IO;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    // --- Charging 변수 ---
    public float PushPower = 15.0f;
    public float MaxPower = 50.0f;
    public float MaxBonus = 1.5f;
    public float MaxChargeTime = 0.75f;   
    private float chargeTimer = 0f;      
    private float chargePercent = 0f;    

    // --- 이동 변수 --- 
    private bool isHolding = false;
    private Vector2 dragStartPos;
    private Vector2 currentDragPos;

    // --- 객체들 --- 
    [SerializeField]
    private ArrowController Arrow;
    private GameManager manager;
    private Rigidbody Rigid;
    private ProjectilePool pool;
    private CapsuleCollider CapsuleCol;
    private void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        if (Arrow == null)
        { 
            GameObject prefab=  Resources.Load<GameObject>("Prefabs/Arrow");
            GameObject obj = GameObject.Instantiate(prefab);
            Arrow = obj.GetComponent<ArrowController>();
        }
        Arrow.gameObject.SetActive(false);
        manager = GameManager.Instance;
        pool = manager.Stage.ObjectPooler.GetComponent<ProjectilePool>();
        CapsuleCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if(isHolding)
        {
            chargeTimer += Time.deltaTime;

            OnHold();

        }
        CapsuleCol.radius = (Rigid.velocity.magnitude < 1f) ? 1.5f : 3.0f;
    }

    void FixedUpdate()
    {
        Vector3 pos = Rigid.position;
        pos.y = Mathf.Clamp(pos.y, 0f, 5.5f);   
        Rigid.position = pos;
    }

    public void OnHold()
    {
        // 계산
        currentDragPos = Input.mousePosition;
        Vector2 dragDelta = currentDragPos - dragStartPos;
        dragDelta = Vector2.ClampMagnitude(dragDelta, MaxPower);
        // 화살표 설정
        Vector2 NowPlayerPos = new Vector2(transform.position.x, transform.position.z);
        Arrow.SetArrow(NowPlayerPos, NowPlayerPos - dragDelta, dragDelta.magnitude);
        // 차징 설정
        chargePercent = Mathf.Clamp01(chargeTimer / MaxChargeTime);
        Arrow.SetChargeArrow(chargePercent);
    }


    // 누를 때
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        dragStartPos = eventData.position;   
        currentDragPos = dragStartPos;
        Arrow.gameObject.SetActive(true);

        chargeTimer = 0f;
        chargePercent = 0f;
    }

    // 드래그 중 (홀드 상태)
    public void OnDrag(PointerEventData eventData)
    {
        if (!isHolding) return;
    }

    // 손 뗄 때
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHolding) return;
        isHolding = false;
        // 계산
        Vector2 releasePos = eventData.position;
        Vector2 dragDelta = releasePos - dragStartPos;
        // 
        float allowedPower = MaxPower * chargePercent;
        dragDelta = Vector2.ClampMagnitude(dragDelta, allowedPower);
        if(chargePercent > 0.98f)
        {
            dragDelta *= MaxBonus;
        }
        Vector3 PushVector = -new Vector3(dragDelta.x, 0, dragDelta.y) * PushPower;
        Rigid.AddForce(PushVector, ForceMode.Impulse);


        Arrow.gameObject.SetActive(false);
        // 충전 초기화
        chargeTimer = 0f;
        chargePercent = 0f;
    }


}