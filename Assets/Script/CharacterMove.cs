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
            //selecting이 되어있는 상태에서 안에 움직일 몹들이 있을경우
            if (!selectManager.selecting && selectManager.selectedArmy.Count > 0)
            {
               // Debug.Log("움직임");
                RaycastHit hit;
                if (Physics.Raycast(selectCam.ScreenPointToRay(Input.mousePosition), out hit, int.MaxValue))
                {
                    movePos = hit.point;
                    characters = selectManager.selectedArmy;
                    //해당되는 위치로 이동시킨다.
                    foreach (SelectableCharacter selectableCharacter in selectManager.selectedArmy)
                    {
                        selectableCharacter.gameObject.transform.parent.parent.GetComponent<Hero>().MoveArea(movePos);
                    }
                    //selectManager.ReSelect();
                }
            }
        }
    }

    //각자 캐릭터에게 명령을 내리자 해당 장소로 이동하라고

    //선택된 상태에서 리스트 목록에 캐릭터들이 있으면 그 몬스터들을 해당된 지역으로 이동시킨다.
}
