using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    ulong count;

    void Start()
    {
        count = 0;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (!Input.GetMouseButtonDown(0)) return;
            count++;
            text.text = "2^" + count.ToString();
        }).AddTo(this);
    }
}
