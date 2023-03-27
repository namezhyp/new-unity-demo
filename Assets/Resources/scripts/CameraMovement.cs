using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3  startPosition;
    public Vector3 endPosition;

    //public float speed;
    public float startMoveTime;//��¼�����ʼƽ���ƶ���ʱ�䣬��startGame�ű�����
    public bool isMoving = false;  //�Ƿ���ƽ���ƶ�����ֹ������ͻ


    public Vector3 lastPosition;  //��������Ҽ��ƶ�
    private float x, y;




    private void LateUpdate()  //��update��ͬ����ÿ֡���ִ��
    {
        if (Input.GetMouseButton(1))  // �������Ҽ��Ƿ񱻰���
        {
            if (lastPosition != Vector3.zero)  // ��������϶��Ŀ�ʼ
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                delta *= 0.1f;  // �����϶����ٶ�

                transform.Translate(delta.x, delta.y, 0);  //��Ļͬ��귽���ƶ�

            }
            lastPosition = Input.mousePosition;  // �������λ��
        }
        else
        {
            lastPosition = Vector3.zero;  // ������һ�����λ��
        }
    } //�Ҽ��϶�����ƶ�

    private void Update()  //ƽ���ƶ�
    {
        if(isMoving) //�������ű�Ҫ��ƽ���ƶ�
        { 
            //Debug.Log("ƽ���ƶ�������" + startPosition + "   " + endPosition +"  "+ startMoveTime) ;
            //Debug.Log("��ǰ���λ��:"+this .transform .position);

            x = (endPosition.x - startPosition.x)/3f * (Time.time -startMoveTime ); //����ƶ�ʱ��Ϊ3�룬���㵱ǰʱ���Ѿ��ƶ��˶���,�ٶ�û���õ�
            y = (endPosition.y - startPosition.y) / 3f * (Time.time - startMoveTime);

            transform.position = new Vector3(startPosition.x+x, startPosition.y + y,endPosition .z);
            /*length = Vector3.Distance(startPosition, endPosition);
            float distCovered = (Time.time - startTime * speed);
            float fracJourne = distCovered / length;
            this.transform.position=Vector3 .Lerp (startPosition, endPosition, speed);*/

            if (Vector3.Distance(transform.position, endPosition) < 0.01f)
            {
                Debug.Log("camera_move:ƽ�����");
                isMoving = false;  //�ƶ���ɣ�����ռ��
            }
        }
    }
}

