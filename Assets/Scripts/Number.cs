using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField]
    int degitValue;
    [SerializeField]
    TextMeshProUGUI text;
    List<int> values = new List<int>();
    [SerializeField]
    List<string> digites;
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        values.Add(1);
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (!Input.GetMouseButtonDown(0)) return;
            audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audioSource.Play();
            Double();
            TextView();
        }).AddTo(this);
    }

    public void Double()
    {
        for (int i = values.Count - 1; i >= 0; i--)
        {
            values[i] *= 2;
            CarryCheck(i);
        }
    }
    private void CarryCheck(int _i)
    {
        if (values[_i] >= degitValue)
        {
            values[_i] -= degitValue;
            if (_i == values.Count - 1) values.Add(0);
            Carry(_i + 1);
        }
    }
    public void Carry(int _i)
    {
        values[_i] += 1;
        CarryCheck(_i);
    }

    private void TextView()
    {
        text.text = "";
        for (int i = values.Count - 1; i >= 0; i--)
        {
            if (values[i] == 0) continue;
            if (i == 0) text.text += values[i].ToString();
            else text.text += values[i].ToString() + digites[(i - 1) % digites.Count];
        }
        text.text += "個";
    }
}