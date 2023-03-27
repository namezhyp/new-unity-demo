using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class mouseClickChar : MonoBehaviour
{
    public GameObject selectedObject;
    private List<RaycastHit2D> colliders = new List<RaycastHit2D>();
    public bool isSelected = false;  //当前是否处于选中状态
    //鼠标点选一个角色，再次点击则角色向此位置移动，点右键则取消选中
    //这个脚本挂在一个角色上，其他角色用获取组件的方法引用

    private float lastClickTime;
    private readonly float doubleClickTimeThreshold = 0.2f; // 允许 0.2s 的时间区分连续单击和双击事件

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
            Debug.Log("mouseclick:检测到鼠标点击:" + worldPosition);
            var ret = Physics2D.OverlapCircle(worldPosition, 0.01f, LayerMask.GetMask("character"));
            //Debug.Log(ret != null);
            if (ret != null)
            {
                Debug.Log("ret：" + ret.name);
                selectedObject = ret.gameObject;
                Debug.Log("mouseclick:已选中："+selectedObject .name);
                startPosition =new Vector3 (selectedObject.transform.position.x, selectedObject.transform.position.y,0);
                isSelected = true;
                lastClickTime = Time.time;
            }

            /*Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(mouseWorldPoint.x, mouseWorldPoint.y);
            Collider2D collider = Physics2D.OverlapCircle(mousePosition, 0.2f);

            if (collider!=null)
            {
                //判断是否击中物体                
                
                    //保存引用并输出名称
                    selectedObject = collider .transform.gameObject;
                    Debug.Log("选中物体：" + selectedObject.name);                       
                
            }*/
        }//未选中状态按下鼠标左键即为选中

        if(Input.GetMouseButtonDown(0) && (isSelected) &&(Time .time -lastClickTime)>doubleClickTimeThreshold  ) //选中状态按下鼠标左键为指定位置 增加最小时间间隙
        {
            string dataName = selectedObject.name +"_data";
            //Debug.Log("dataname:"+dataName);

            endPosition =new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);

            System.Type type = System.Type.GetType(dataName);  //反射机制，其他脚本只要是公共变量就可用这种方法
            //Debug.Log("mouseclick:选中的为："+selectedObject);
            Component component = selectedObject.GetComponent(type); //找到目标组件 取出来准备用 和startGame不太一样
            Debug.Log("mouseclick:鼠标再次按下:"+component.name);

            FieldInfo field = type.GetField("moving");  //获取变量
            field.SetValue(component, true);
            object value = field.GetValue(component);

            field = type.GetField("action");
            field.SetValue(component, true);
            value = field.GetValue(component);

            field = type.GetField("startPosition");
            field.SetValue(component,startPosition);
            value = field.GetValue(component);

            field = type.GetField("endPosition");
            field.SetValue(component,endPosition); //不能直接用mouseposition，原理同上
            value = field.GetValue(component);

            field = type.GetField("time");
            field.SetValue(component, (float)Time.time);
            value = field.GetValue(component);

            isSelected = false;//视为点按完成，自动取消选中
        }

    } 

}
    
    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //获取鼠标点击位置的世界坐标
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(mouseWorldPoint.x, mouseWorldPoint.y);

            //通过鼠标点击位置的射线获取点击到的物体
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.down);

            //判断是否击中物体
            if (hit.collider != null)
            {
                //保存引用并输出名称
                selectedObject = hit.transform.gameObject;
                Debug.Log("选中物体：" + selectedObject.name);
            }
            else
            {
                //如果没有击中任何物体，取消选中当前物体
                selectedObject = null;
                Debug.Log("取消/未选");
            }
        }
    }*/

   

