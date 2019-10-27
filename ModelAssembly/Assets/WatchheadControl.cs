using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchheadControl : MonoBehaviour
{
    public List<GameObject> WatchHeadModel = new List<GameObject>();
    private int index;

    void Start()
    {
        index = 0;
        WatchHeadShow();
    }

    private void WatchHeadShow()
    {
        if (index < WatchHeadModel.Capacity)
        {
            WatchHeadModel[index].SetActive(true);
            index++;
            Invoke("WatchHeadShow", 0.2f);
        }
        else
        {
            Debug.Log("hide");
            index = 0;
            Invoke("WatchHeadHide", 2f);
        }
    }

    private void WatchHeadHide()
    {
        for (int i = 0; i < WatchHeadModel.Capacity; i++)
        {
            WatchHeadModel[i].SetActive(false);
        }
        Invoke("WatchHeadShow", 0.5f);
    }
}
