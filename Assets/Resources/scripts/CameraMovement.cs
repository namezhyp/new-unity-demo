using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3  startPosition;
    public Vector3 endPosition;

    //public float speed;
    public float startMoveTime;//记录相机开始平滑移动的时间，由startGame脚本给出
    public bool isMoving = false;  //是否处于平滑移动，防止动作冲突


    public Vector3 lastPosition;  //用于相机右键移动
    private float x, y;




    private void LateUpdate()  //与update不同的是每帧最后执行
    {
        if (Input.GetMouseButton(1))  // 检测鼠标右键是否被按下
        {
            if (lastPosition != Vector3.zero)  // 如果不是拖动的开始
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                delta *= 0.1f;  // 调整拖动的速度

                transform.Translate(delta.x, delta.y, 0);  //屏幕同鼠标方向移动

            }
            lastPosition = Input.mousePosition;  // 更新鼠标位置
        }
        else
        {
            lastPosition = Vector3.zero;  // 重置上一次鼠标位置
        }
    } //右键拖动相机移动

    private void Update()  //平滑移动
    {
        if(isMoving) //被其他脚本要求平滑移动
        { 
            //Debug.Log("平滑移动被调用" + startPosition + "   " + endPosition +"  "+ startMoveTime) ;
            //Debug.Log("当前相机位置:"+this .transform .position);

            x = (endPosition.x - startPosition.x)/3f * (Time.time -startMoveTime ); //相机移动时长为3秒，计算当前时间已经移动了多少,速度没有用到
            y = (endPosition.y - startPosition.y) / 3f * (Time.time - startMoveTime);

            transform.position = new Vector3(startPosition.x+x, startPosition.y + y,endPosition .z);
            /*length = Vector3.Distance(startPosition, endPosition);
            float distCovered = (Time.time - startTime * speed);
            float fracJourne = distCovered / length;
            this.transform.position=Vector3 .Lerp (startPosition, endPosition, speed);*/

            if (Vector3.Distance(transform.position, endPosition) < 0.01f)
            {
                Debug.Log("camera_move:平移完成");
                isMoving = false;  //移动完成，结束占用
            }
        }
    }
}

