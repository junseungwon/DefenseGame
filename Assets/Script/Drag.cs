using UnityEngine;

public class Drag : MonoBehaviour
{
    private Camera mainCamera; // ���� ī�޶�
    private bool isDragging = false; // �巡�� ����
    private Vector3 offset; // Ŭ�� ��ġ�� ��ü�� ��ġ ����

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ����
    }

    void OnMouseDown()
    {
        // Ŭ���� ��ġ�� ��ü�� ��ġ ���� ����
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true; // �巡�� ����
    }

    void OnMouseUp()
    {
        isDragging = false; // �巡�� ����
    }

    void Update()
    {
        if (isDragging)
        {
            // �巡�� ���� �� ��ü�� ��ġ ������Ʈ
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // ���콺�� ���� ȭ�� ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.nearClipPlane; // ī�޶��� ���� Ŭ�� ��� �Ÿ� ����
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}



