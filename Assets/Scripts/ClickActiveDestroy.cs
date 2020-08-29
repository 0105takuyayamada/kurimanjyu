using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClickActiveDestroy : MonoBehaviour
{
    [SerializeField]
    int nextCount;
    int count;

    [SerializeField]
    GameObject nextObject;

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                count++;
                if (count >= nextCount)
                {
                    if (nextObject) nextObject.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }).AddTo(this);
    }
}