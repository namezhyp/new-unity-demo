using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;



public class test_button : MonoBehaviour
{
    //这是一个原始脚本，里面放了测试代码，现在只用于按钮按下并唤醒初始脚本
    //GameObject spine1 = GameObject.Find("test-amiya");
    // Start is called before the first frame update
    //Debug.log("yes");
    void Start()
    {
        //spine1.GetComponent<skeletonAnimation>();
        //Debug.Log("111");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void testhanshu()
    {
        //Debug.Log("test");
        SkeletonAnimation test1 = GameObject.Find("amiya").GetComponent<SkeletonAnimation>();
        
        SkeletonDataAsset newAsset= Resources.Load<SkeletonDataAsset>("spine-assets/player/amiya-attack/char_002_amiya_SkeletonData");
        //resoureces.load这个函数的前提是文件必须在resources这个文件夹下
        
        if (newAsset == null) Debug.Log("no asset"); //检查是否找到了新资源
        
        test1.skeletonDataAsset = newAsset;
        newAsset.GetSkeletonData(true);
        

        test1.Initialize(true);
        test1.AnimationName ="Skill_2";
        Debug.Log("test函数运行结束");       
    }  //测试 无用

    public void startGame()
    {
        //激活startGame脚本
        GameObject test = GameObject.Find("开局脚本挂载");
        test.GetComponent<startGame>().enabled = true ;
    }

   
    /*public void onClick()
    {
        Debug.Log("aaa");
        SkeletonAnimation test1 = GameObject.Find("test-amiya").GetComponent<SkeletonAnimation>();
        test1.AnimationName = "skill2";
        //Debug.Log("yes");
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
        SkeletonAnimation test1 = GameObject.Find("test-amiya").GetComponent<SkeletonAnimation>();
        test1.AnimationName = "skill2";
        Debug.Log("yes");
    }*/
}
