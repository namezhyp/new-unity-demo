using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class spine_change : MonoBehaviour
{
    //接收其他脚本要求调整spine
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_spine(string name,string path,string action)
    {
        SkeletonAnimation test1 = GameObject.Find("amiya").GetComponent<SkeletonAnimation>();
        SkeletonDataAsset newAsset = Resources.Load<SkeletonDataAsset>(path);
        //resoureces.load这个函数的前提是文件必须在resources这个文件夹下

        if (newAsset == null) Debug.Log("spine_change:no asset"); //检查是否找到了新资源

        test1.skeletonDataAsset = newAsset;
        newAsset.GetSkeletonData(true);

        test1.Initialize(true);
        test1.AnimationName = action;
        Debug.Log ("spine_change:动作更换完成");

    }
}
