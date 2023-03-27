using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Spine.Unity;

/*public interface IMyinterface
{
    int MyPublicVariable { get; }
}*/

public class amiya_data : MonoBehaviour
{
    public int health = 1000;
    public int attackRange = 10;
    public int damage = 400;
    public float moveSpeed = 0.08f;
    public bool chooseable = true;//��Ǹ�����ɷ�ѡ��
    private bool choosing = false;//��ǰ�Ƿ��ڱ�ѡ�е�״̬
    public bool attacking = false; //�Ƿ��ڹ���״̬
    public bool moving = false; //�ƶ�״̬
    public bool action = false; //Ϊ�˴����ƶ�ʱֻ�ı�һ��spine

    public Vector3 startPosition;
    public Vector3 endPosition; //�ƶ�ָ��Ҫ�õ�����
    public float time;  //��ǽӵ��ƶ�����Ŀ�ʼʱ��

    private GameObject bullet;
    public Vector3 targetPosition; //�ӵ�Ŀ��

    public class PositionData
    {
        public Vector3 startPosition;
        public Vector3 endPosition;
        public bool moving = false;

        public PositionData(Vector3 start, Vector3 end, bool isMoving)
        {
            startPosition = start;
            endPosition = end;
            moving = isMoving;
        }
    }

    public PositionData GetPositionData()
    {
        return new PositionData(startPosition, endPosition, moving);
    }   //����  �������ű���ͬʱ���ʶ�����ֲ�ȫ��ͬ�Ĵ���ű����ض�������Ŀǰ�ô�����


    // Start is called before the first frame update
    private void Awake()
    {
        bullet = Instantiate(Resources.Load("spine-assets/player/amiya-walk/amiya-bullet")) as GameObject;  //�ӵ�Ԥ�Ƽ�
    }

    void Start()
    {
        
        //Debug.Log(transform .position);
    }                                                                                                                 


    void Update()
    {
        if (moving)
        {
            if (action)
            {
                GameObject scriptObject = GameObject.Find("scripts holder");
                string path = "spine-assets/player/amiya-walk/build_char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Move");
                if(endPosition .x<startPosition .x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                action = false;
                //Debug.Log("amiya:��ʼ�ͽ���λ��:" + startPosition + "" + endPosition);
            }  //�ⲿ��ÿ�η�������ʱֻ��ִ��һ��

            transform.position = Vector3.MoveTowards(transform.position,endPosition,moveSpeed);
            Debug.Log("amiya:��ֹ���꣺"+startPosition +" "+endPosition);
            Debug.Log("amiya:��ǰλ��:" + transform.position);
            if (Vector3.Distance(transform.position, endPosition) < 0.002f || Vector3.Distance(transform.position, endPosition) > Vector3.Distance(startPosition, endPosition))
            {
                Debug.Log("amiya:��ɫ�ƶ����");
                moving = false;
                GameObject scriptObject = GameObject.Find("scripts holder");
                string path = "spine-assets/player/amiya-walk/build_char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Relax");
            }
            
          

            /*float x, y;                  //�ƶ�����1
        if (endPosition.x >= startPosition.x)
        { x = transform.position.x + moveSpeed * (Time.time-time); }
        else
        { x = transform.position.x - moveSpeed * (Time.time - time); }
        if ((endPosition.y >= startPosition.y))
        { y = transform.position.y + moveSpeed * (Time.time - time); }
        else
        { y = transform.position.y - moveSpeed * (Time.time - time); }
        transform.position = new Vector3(x,y,0);

        Debug.Log("amiya:��ǰλ��:"+transform.position);
        if (Vector3.Distance(transform.position,endPosition) < 0.002f || Vector3.Distance(transform.position,endPosition) > Vector3.Distance(startPosition, endPosition))
        {
            Debug.Log("amiya:��ɫ�ƶ����");
            moving = false;
            GameObject scriptObject = GameObject.Find("scripts holder");
            string path = "spine-assets/player/amiya-walk/build_char_002_amiya_SkeletonData";
            scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Relax");
        }
        */
            //�ƶ��Ĵ���
        }  //�ƶ��Ĵ���


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("amiya:��ײ����: "+collision.name);
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        targetPosition = capsuleCollider.transform.position;
        if (capsuleCollider != null)
        {
            Debug.Log("amiya:���빥����Χ");
            GameObject scriptObject = GameObject.Find("scripts holder");
            if (capsuleCollider.gameObject.transform.position.x >= transform.position.x)
            {
                string path = "spine-assets/player/amiya-attack/char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Attack");
                moving = false;//����ʱ��ͣ�ƶ�
            }
            else
            {
                string path = "spine-assets/player/amiya-back/char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Attack");
                moving = false;
            }
            //InvokeRepeating("CreateBullet", 0f, 1.67f);  //�����ӵ�

        }
    }  //���˽��빥����Χ

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("amiya:��ײ����"+collision.name);
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.Log("amiya.exit:�޵���");
            //CancelInvoke("CreateBullet");  //ֹͣ�����ӵ�
            if (Vector3.Distance(transform.position, endPosition) > 0.02f) //  ��û�ƶ���ͼ����ƶ�
            {
                Debug.Log("amiya.exit:�����ƶ�");
                moving = true;
            }
        }
    }

    private void CreateBullet()
    {
        Debug.Log("amiya:�����ӵ�");
        //GameObject newbullet = Instantiate(bullet, transform.position, transform.rotation);
        //amiya_bullet script = newbullet.GetComponent<amiya_bullet>();
        //script.targetPosition = targetPosition;
        Debug.Log("amiya:�ӵ�Ŀ�� "+targetPosition);
        //Destroy(newbullet, 3f); 
    }
}


