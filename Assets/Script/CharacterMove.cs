using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private SelectManager selectManager = null;

    [SerializeField]
    private Camera selectCam = null;
    private List<SelectableCharacter> characters = new List<SelectableCharacter>();
    private Vector3 movePos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //selecting�� �Ǿ��ִ� ���¿��� �ȿ� ������ ������ �������
            if (!selectManager.selecting && selectManager.selectedArmy.Count > 0)
            {
               // Debug.Log("������");
                RaycastHit hit;
                if (Physics.Raycast(selectCam.ScreenPointToRay(Input.mousePosition), out hit, int.MaxValue))
                {
                    movePos = hit.point;
                    characters = selectManager.selectedArmy;
                    //�ش�Ǵ� ��ġ�� �̵���Ų��.
                    foreach (SelectableCharacter selectableCharacter in selectManager.selectedArmy)
                    {
                        selectableCharacter.gameObject.transform.parent.parent.GetComponent<Hero>().MoveArea(movePos);
                    }
                    //selectManager.ReSelect();
                }
            }
        }
    }

    //���� ĳ���Ϳ��� ����� ������ �ش� ��ҷ� �̵��϶��

    //���õ� ���¿��� ����Ʈ ��Ͽ� ĳ���͵��� ������ �� ���͵��� �ش�� �������� �̵���Ų��.
}
