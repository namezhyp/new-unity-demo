using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class spine_change : MonoBehaviour
{
    //���������ű�Ҫ�����spine
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
        //resoureces.load���������ǰ�����ļ�������resources����ļ�����

        if (newAsset == null) Debug.Log("spine_change:no asset"); //����Ƿ��ҵ�������Դ

        test1.skeletonDataAsset = newAsset;
        newAsset.GetSkeletonData(true);

        test1.Initialize(true);
        test1.AnimationName = action;
        Debug.Log ("spine_change:�����������");

    }
}
