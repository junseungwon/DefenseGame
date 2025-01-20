using UnityEngine;

public class Drag : MonoBehaviour
{
    private Camera mainCamera; // 메인 카메라
    private bool isDragging = false; // 드래그 상태
    private Vector3 offset; // 클릭 위치와 물체의 위치 차이

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라 저장
    }

    void OnMouseDown()
    {
        // 클릭한 위치와 물체의 위치 차이 저장
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true; // 드래그 시작
    }

    void OnMouseUp()
    {
        isDragging = false; // 드래그 종료
    }

    void Update()
    {
        if (isDragging)
        {
            // 드래그 중일 때 물체의 위치 업데이트
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 마우스의 현재 화면 위치를 월드 좌표로 변환
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.nearClipPlane; // 카메라의 근접 클립 평면 거리 설정
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}



