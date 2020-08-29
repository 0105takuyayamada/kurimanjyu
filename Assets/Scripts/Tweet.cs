using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TMPro;

public class Tweet : MonoBehaviour
{
    const int TWEET_MAX_LENGTH = 127;

    [SerializeField]
    GameObject TweetButton;
    [SerializeField]
    TextMeshProUGUI text;

    public void TweetProcess()
    {
        string _text = "";

        TweetButton.SetActive(false);
        Observable.Timer(TimeSpan.FromSeconds(0.8f)).Subscribe(_ => TweetButton.SetActive(true)).AddTo(this);

        if (text.text.Length > TWEET_MAX_LENGTH)
        {
            _text = "栗饅頭が "
                + text.text.Substring(0, Mathf.Min(TWEET_MAX_LENGTH, text.text.Length - 1))
                + "…個になったよ！";
        }
        else
        {
            _text = "栗饅頭が "
                + text.text.Substring(0, Mathf.Min(TWEET_MAX_LENGTH, text.text.Length - 1))
                + " 個になったよ！";
        }

        _text += "%0ahttps://t.co/6qAFhWx28G?amp=1%0a";

        StartCoroutine(TweetWithScreenShot.TweetManager.TweetWithScreenShot(_text));
    }
}