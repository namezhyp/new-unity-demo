using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amiya_bullet : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool moving = false;
    public float moveSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�����ӵ�");
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
            Debug.Log("amiya:��ǰλ��:" + transform.position+"  "+targetPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.)
        //Debug.Log("bullet:�Ӵ�������");
    }
}
