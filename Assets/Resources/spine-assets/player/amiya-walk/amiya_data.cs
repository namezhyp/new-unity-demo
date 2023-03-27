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
    public bool chooseable = true;//标记该物体可否被选中
    private bool choosing = false;//当前是否在被选中的状态
    public bool attacking = false; //是否处于攻击状态
    public bool moving = false; //移动状态
    public bool action = false; //为了处理移动时只改变一次spine

    public Vector3 startPosition;
    public Vector3 endPosition; //移动指令要用的坐标
    public float time;  //标记接到移动命令的开始时间

    private GameObject bullet;
    public Vector3 targetPosition; //子弹目标

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
    }   //反射  让其他脚本能同时访问多个名字不全相同的此类脚本的特定变量，目前用处不大


    // Start is called before the first frame update
    private void Awake()
    {
        bullet = Instantiate(Resources.Load("spine-assets/player/amiya-walk/amiya-bullet")) as GameObject;  //子弹预制件
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
                //Debug.Log("amiya:起始和结束位置:" + startPosition + "" + endPosition);
            }  //这部分每次发生动作时只能执行一次

            transform.position = Vector3.MoveTowards(transform.position,endPosition,moveSpeed);
            Debug.Log("amiya:起止坐标："+startPosition +" "+endPosition);
            Debug.Log("amiya:当前位置:" + transform.position);
            if (Vector3.Distance(transform.position, endPosition) < 0.002f || Vector3.Distance(transform.position, endPosition) > Vector3.Distance(startPosition, endPosition))
            {
                Debug.Log("amiya:角色移动完成");
                moving = false;
                GameObject scriptObject = GameObject.Find("scripts holder");
                string path = "spine-assets/player/amiya-walk/build_char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Relax");
            }
            
          

            /*float x, y;                  //移动代码1
        if (endPosition.x >= startPosition.x)
        { x = transform.position.x + moveSpeed * (Time.time-time); }
        else
        { x = transform.position.x - moveSpeed * (Time.time - time); }
        if ((endPosition.y >= startPosition.y))
        { y = transform.position.y + moveSpeed * (Time.time - time); }
        else
        { y = transform.position.y - moveSpeed * (Time.time - time); }
        transform.position = new Vector3(x,y,0);

        Debug.Log("amiya:当前位置:"+transform.position);
        if (Vector3.Distance(transform.position,endPosition) < 0.002f || Vector3.Distance(transform.position,endPosition) > Vector3.Distance(startPosition, endPosition))
        {
            Debug.Log("amiya:角色移动完成");
            moving = false;
            GameObject scriptObject = GameObject.Find("scripts holder");
            string path = "spine-assets/player/amiya-walk/build_char_002_amiya_SkeletonData";
            scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Relax");
        }
        */
            //移动的代码
        }  //移动的代码


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("amiya:碰撞对象: "+collision.name);
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        targetPosition = capsuleCollider.transform.position;
        if (capsuleCollider != null)
        {
            Debug.Log("amiya:进入攻击范围");
            GameObject scriptObject = GameObject.Find("scripts holder");
            if (capsuleCollider.gameObject.transform.position.x >= transform.position.x)
            {
                string path = "spine-assets/player/amiya-attack/char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Attack");
                moving = false;//攻击时暂停移动
            }
            else
            {
                string path = "spine-assets/player/amiya-back/char_002_amiya_SkeletonData";
                scriptObject.GetComponent<spine_change>().change_spine("amiya", path, "Attack");
                moving = false;
            }
            //InvokeRepeating("CreateBullet", 0f, 1.67f);  //发射子弹

        }
    }  //有人进入攻击范围

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("amiya:碰撞对象"+collision.name);
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider == null)
        {
            Debug.Log("amiya.exit:无敌人");
            //CancelInvoke("CreateBullet");  //停止发射子弹
            if (Vector3.Distance(transform.position, endPosition) > 0.02f) //  还没移动完就继续移动
            {
                Debug.Log("amiya.exit:继续移动");
                moving = true;
            }
        }
    }

    private void CreateBullet()
    {
        Debug.Log("amiya:发射子弹");
        //GameObject newbullet = Instantiate(bullet, transform.position, transform.rotation);
        //amiya_bullet script = newbullet.GetComponent<amiya_bullet>();
        //script.targetPosition = targetPosition;
        Debug.Log("amiya:子弹目标 "+targetPosition);
        //Destroy(newbullet, 3f); 
    }
}


