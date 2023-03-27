using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class mouseClickChar : MonoBehaviour
{
    public GameObject selectedObject;
    private List<RaycastHit2D> colliders = new List<RaycastHit2D>();
    public bool isSelected = false;  //��ǰ�Ƿ���ѡ��״̬
    //����ѡһ����ɫ���ٴε�����ɫ���λ���ƶ������Ҽ���ȡ��ѡ��
    //����ű�����һ����ɫ�ϣ�������ɫ�û�ȡ����ķ�������

    private float lastClickTime;
    private readonly float doubleClickTimeThreshold = 0.2f; // ���� 0.2s ��ʱ����������������˫���¼�

    Vector3 startPosition,endPosition;


    void Start()
    {
        //Collider2D test = this.GetComponent<BoxCollider2D>();
        //Debug.Log("center:" + test.bounds.center);
        //Debug.Log("size:" + test.bounds.size);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && (!isSelected))
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("mouseclick:��⵽�����:" + worldPosition);
            var ret = Physics2D.OverlapCircle(worldPosition, 0.01f, LayerMask.GetMask("character"));
            //Debug.Log(ret != null);
            if (ret != null)
            {
                Debug.Log("ret��" + ret.name);
                selectedObject = ret.gameObject;
                Debug.Log("mouseclick:��ѡ�У�"+selectedObject .name);
                startPosition =new Vector3 (selectedObject.transform.position.x, selectedObject.transform.position.y,0);
                isSelected = true;
                lastClickTime = Time.time;
            }

            /*Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(mouseWorldPoint.x, mouseWorldPoint.y);
            Collider2D collider = Physics2D.OverlapCircle(mousePosition, 0.2f);

            if (collider!=null)
            {
                //�ж��Ƿ��������                
                
                    //�������ò��������
                    selectedObject = collider .transform.gameObject;
                    Debug.Log("ѡ�����壺" + selectedObject.name);                       
                
            }*/
        }//δѡ��״̬������������Ϊѡ��

        if(Input.GetMouseButtonDown(0) && (isSelected) &&(Time .time -lastClickTime)>doubleClickTimeThreshold  ) //ѡ��״̬����������Ϊָ��λ�� ������Сʱ���϶
        {
            string dataName = selectedObject.name +"_data";
            //Debug.Log("dataname:"+dataName);

            endPosition =new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);

            System.Type type = System.Type.GetType(dataName);  //������ƣ������ű�ֻҪ�ǹ��������Ϳ������ַ���
            //Debug.Log("mouseclick:ѡ�е�Ϊ��"+selectedObject);
            Component component = selectedObject.GetComponent(type); //�ҵ�Ŀ����� ȡ����׼���� ��startGame��̫һ��
            Debug.Log("mouseclick:����ٴΰ���:"+component.name);

            FieldInfo field = type.GetField("moving");  //��ȡ����
            field.SetValue(component, true);
            object value = field.GetValue(component);

            field = type.GetField("action");
            field.SetValue(component, true);
            value = field.GetValue(component);

            field = type.GetField("startPosition");
            field.SetValue(component,startPosition);
            value = field.GetValue(component);

            field = type.GetField("endPosition");
            field.SetValue(component,endPosition); //����ֱ����mouseposition��ԭ��ͬ��
            value = field.GetValue(component);

            field = type.GetField("time");
            field.SetValue(component, (float)Time.time);
            value = field.GetValue(component);

            isSelected = false;//��Ϊ�㰴��ɣ��Զ�ȡ��ѡ��
        }

    } 

}
    
    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //��ȡ�����λ�õ���������
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(mouseWorldPoint.x, mouseWorldPoint.y);

            //ͨ�������λ�õ����߻�ȡ�����������
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.down);

            //�ж��Ƿ��������
            if (hit.collider != null)
            {
                //�������ò��������
                selectedObject = hit.transform.gameObject;
                Debug.Log("ѡ�����壺" + selectedObject.name);
            }
            else
            {
                //���û�л����κ����壬ȡ��ѡ�е�ǰ����
                selectedObject = null;
                Debug.Log("ȡ��/δѡ");
            }
        }
    }*/

   

