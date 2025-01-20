using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour
{
    public Transform target = null;
    public float speed = 30.0f;
    
    [SerializeField]
    private MonsterData monsterData;


    void Update()
    {
        transform.RotateAround(target.position, Vector3.forward, monsterData.MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }
}

 