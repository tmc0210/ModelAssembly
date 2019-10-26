using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssambleObject : MonoBehaviour
{

    private Transform mMark;

    private Transform tMark;

    public Transform TAssambleObject;

    private Transform[] targetMark;

    public float minAssambleDistance = 10f;

    // Use this for initialization
    void Start()
    {
        mMark = this.transform.Find("LocationMark");
        if (mMark == null)
        {
            Debug.LogError("本地对象未找到位置标志");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tMark = null;
    }

    private void FindTarget()
    {
        targetMark = TAssambleObject.gameObject.GetComponentsInChildren<Transform>();

        float tmp1 = minAssambleDistance;

        foreach (Transform childTransform in targetMark)
        {
            if (childTransform.gameObject.GetComponent<TailLocationMark>())
            {
                if (childTransform.gameObject.GetComponent<TailLocationMark>().tMark == null)
                {
                    float tmp2 = (childTransform.position - mMark.transform.position).magnitude;
                    if (tmp2 < tmp1)
                    {

                        tmp1 = tmp2;
                        tMark = childTransform;
                    }
                }
                else
                {
                    Debug.Log(childTransform.gameObject.name + "is not free");
                }
            }
        }

        //tMark = TAssambleObject.Find("LocationMark");

        if (tMark == null)
        {
            Debug.Log("目标对象未找到位置标志,可能距离过远或tail的所有空位已满");
        }
    }


    int j = 0;

    private void MoveToTarget()
    {

        Vector3 RotateAix = Vector3.Cross(mMark.transform.forward, tMark.transform.forward);
        float angle = Vector3.Angle(mMark.transform.forward, tMark.transform.forward);
        mMark.transform.parent.Rotate(RotateAix, angle, Space.World);
        while (true)
        {
            //第一步
            float Angle = Vector3.Angle(tMark.transform.up, mMark.transform.up);
            mMark.transform.parent.Rotate(tMark.transform.forward, Angle, Space.World);
            //第二步
            Vector3 moveVector = tMark.transform.position - mMark.transform.position;
            mMark.transform.parent.transform.Translate(moveVector, Space.World);
            j++;
            if (Angle == 0 && moveVector == Vector3.zero)
            {
                tMark.gameObject.GetComponent<TailLocationMark>().tMark = mMark.gameObject;
                mMark.gameObject.GetComponent<HeadLocationMark>().tMark = tMark.gameObject;
                Debug.Log("进行了多少次数：" + j);//发现一次不是能达到目标，所以重复几次
                break;
            }
        }
    }

    public void FindTargetAndMove()
    {
        if (mMark.gameObject.GetComponent<HeadLocationMark>().tMark == null)
        {
            FindTarget();
        }
        else
        {
            tMark = null;
        }
        if (tMark != null)
        {
            MoveToTarget();
        }
    }
}
