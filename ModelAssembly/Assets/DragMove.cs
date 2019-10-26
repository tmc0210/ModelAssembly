using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]

public class DragMove : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Transform mMark;
    private HeadLocationMark headLocationMark;
    private AssambleObject assambleObject;

    void Start()
    {
        assambleObject = GetComponent<AssambleObject>();
        if (assambleObject == null)
        {
            Debug.LogError("未找到AssambleObject");
        }

        mMark = this.transform.Find("LocationMark");
        if (mMark == null)
        {
            Debug.LogError("本地对象未找到位置标志");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        { 
}
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        headLocationMark = mMark.gameObject.GetComponent<HeadLocationMark>();

        if (headLocationMark.tMark != null)
        {
            headLocationMark.tMark.GetComponent<TailLocationMark>().tMark = null;
        }
        headLocationMark.tMark = null;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }

    private void OnMouseUp()
    {
        assambleObject.FindTargetAndMove();
    }
}

