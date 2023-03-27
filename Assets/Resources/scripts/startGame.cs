using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class startGame : MonoBehaviour
{
    //初始脚本，按下按钮激活，处理音乐、初始移动相关
    public bool mouseEnable = true;  //鼠标是否激活
    public float waitingTime = 2f;   //开局锁定多久鼠标
    
    void Start()
    {
        startMusic();
        changeButton();   //禁用鼠标
        moveCamera(new Vector3(-14.85f,4.17f,-17.32f), new Vector3(-10.17f,1.54f,-17.32f),0.5f);
        //yield return new WaitForSeconds(3f);
        Invoke("changeButton", waitingTime);  //延迟3秒  启用鼠标

        characterMove("amiya",new Vector3(-23.21f,5.14f,0) ,new Vector3 (-21.21f, 3.14f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMusic() //播放音乐
    {
        GameObject musicObject = GameObject.Find("music");
        AudioSource clip = musicObject.GetComponent<AudioSource>();
        if (clip.isPlaying) clip.Stop();
        else clip.Play();
    }

    private void changeButton() //禁用/启用鼠标
    {
        if (mouseEnable)
        {
            Cursor.visible = false; // 隐藏鼠标光标
            Cursor.lockState = CursorLockMode.Locked; // 隐藏鼠标并锁定它在屏幕中央
            mouseEnable = false;
            Debug.Log("禁用鼠标");
        }
        else
        {
            Cursor.visible = true;      //解除
            Cursor.lockState = CursorLockMode.None;
            mouseEnable = true;
            Debug.Log("启用鼠标");
        }
    }

    public void moveCamera(Vector3 startPosition,Vector3 endPosition,float speed)  //调用摄像机脚本的平滑移动
    {
        Debug.Log("进入moveCamera函数");
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraMovement>().isMoving = true;
        camera.GetComponent<CameraMovement>().startPosition  = startPosition ;
        camera.GetComponent<CameraMovement>().endPosition = endPosition;
        //camera.GetComponent<CameraMovement>().speed=speed;
        camera.GetComponent<CameraMovement>().startMoveTime = Time .time;
        //Debug.Log("测试startGame脚本是否改了参数："+ camera.GetComponent<CameraMovement>().startPosition+"   "+ camera.GetComponent<CameraMovement>().lastPosition);
    }

    public void characterMove(string name,Vector3 startPosition,Vector3 endPosition)
    {
        string testname=name+"_data";
        Debug.Log("start:角色移动:"+testname);

        System.Type type = System.Type.GetType(testname);  //反射机制，其他脚本只要是公共变量就可用这种方法
        GameObject gameobject = GameObject .Find(name);
        Component component = gameobject.GetComponent(type); //找到目标组件 取出来准备用
        //object instance = Activator.CreateInstance(type);//这句不能用，unity不允许new一个monobehavior

        FieldInfo field = type.GetField("moving");  //获取变量
        field.SetValue(component,true);
        object value = field.GetValue(component);

        field = type.GetField("action");
        field.SetValue(component, true);
        value = field.GetValue(component);

        field = type.GetField("startPosition");
        field.SetValue(component, startPosition);
        value = field.GetValue(component);  

        field = type.GetField("endPosition"); 
        field.SetValue(component, endPosition);
        value = field.GetValue(component);

        field = type.GetField("time");
        field.SetValue(component, (float)Time .time);
        value = field.GetValue(component);

        /*
        System.Type type = System.Type.GetType(testname);
        if (type != null)
        {
            Component component = objectChar.GetComponent(type);
            if (component != null)
            {
                // 获取GetPositionData()返回的PositionData类实例
                MethodInfo getPositionDataMethod = type.GetMethod("GetPositionData");
                object positionDataInstance = getPositionDataMethod.Invoke(component, null);

                //获取内部类的变量，并转换为公开类型的变量
                System.Type positionType = positionDataInstance.GetType();
                FieldInfo startField = positionType.GetField("startPosition");
                Vector3 startPosition = (Vector3)startField.GetValue(positionDataInstance);

                FieldInfo endField = positionType.GetField("endPosition");
                Vector3 endPosition = (Vector3)endField.GetValue(positionDataInstance);

                FieldInfo movingField = positionType.GetField("moving");
                bool moving = (bool)movingField.GetValue(positionDataInstance);

                Debug.Log("startPosition: " + startPosition);
                Debug.Log("endPosition: " + endPosition);
                Debug.Log("moving: " + moving);
            }
        }*/
    }  //调用对应角色脚本的移动







}

