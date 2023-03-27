using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class startGame : MonoBehaviour
{
    //��ʼ�ű������°�ť����������֡���ʼ�ƶ����
    public bool mouseEnable = true;  //����Ƿ񼤻�
    public float waitingTime = 2f;   //��������������
    
    void Start()
    {
        startMusic();
        changeButton();   //�������
        moveCamera(new Vector3(-14.85f,4.17f,-17.32f), new Vector3(-10.17f,1.54f,-17.32f),0.5f);
        //yield return new WaitForSeconds(3f);
        Invoke("changeButton", waitingTime);  //�ӳ�3��  �������

        characterMove("amiya",new Vector3(-23.21f,5.14f,0) ,new Vector3 (-21.21f, 3.14f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMusic() //��������
    {
        GameObject musicObject = GameObject.Find("music");
        AudioSource clip = musicObject.GetComponent<AudioSource>();
        if (clip.isPlaying) clip.Stop();
        else clip.Play();
    }

    private void changeButton() //����/�������
    {
        if (mouseEnable)
        {
            Cursor.visible = false; // ���������
            Cursor.lockState = CursorLockMode.Locked; // ������겢����������Ļ����
            mouseEnable = false;
            Debug.Log("�������");
        }
        else
        {
            Cursor.visible = true;      //���
            Cursor.lockState = CursorLockMode.None;
            mouseEnable = true;
            Debug.Log("�������");
        }
    }

    public void moveCamera(Vector3 startPosition,Vector3 endPosition,float speed)  //����������ű���ƽ���ƶ�
    {
        Debug.Log("����moveCamera����");
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraMovement>().isMoving = true;
        camera.GetComponent<CameraMovement>().startPosition  = startPosition ;
        camera.GetComponent<CameraMovement>().endPosition = endPosition;
        //camera.GetComponent<CameraMovement>().speed=speed;
        camera.GetComponent<CameraMovement>().startMoveTime = Time .time;
        //Debug.Log("����startGame�ű��Ƿ���˲�����"+ camera.GetComponent<CameraMovement>().startPosition+"   "+ camera.GetComponent<CameraMovement>().lastPosition);
    }

    public void characterMove(string name,Vector3 startPosition,Vector3 endPosition)
    {
        string testname=name+"_data";
        Debug.Log("start:��ɫ�ƶ�:"+testname);

        System.Type type = System.Type.GetType(testname);  //������ƣ������ű�ֻҪ�ǹ��������Ϳ������ַ���
        GameObject gameobject = GameObject .Find(name);
        Component component = gameobject.GetComponent(type); //�ҵ�Ŀ����� ȡ����׼����
        //object instance = Activator.CreateInstance(type);//��䲻���ã�unity������newһ��monobehavior

        FieldInfo field = type.GetField("moving");  //��ȡ����
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
                // ��ȡGetPositionData()���ص�PositionData��ʵ��
                MethodInfo getPositionDataMethod = type.GetMethod("GetPositionData");
                object positionDataInstance = getPositionDataMethod.Invoke(component, null);

                //��ȡ�ڲ���ı�������ת��Ϊ�������͵ı���
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
    }  //���ö�Ӧ��ɫ�ű����ƶ�







}

