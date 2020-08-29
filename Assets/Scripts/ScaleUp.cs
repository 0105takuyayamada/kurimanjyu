using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScaleUp : MonoBehaviour
{
    [SerializeField]
    float rate;
    [SerializeField]
    bool notRotate;

    float count;

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!
                notRotate) transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
                transform.localScale = Vector2.one * transform.localScale * rate;
            }
        }).AddTo(this);
    }
}
