using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    bool startChange;
    [SerializeField]
    string sceneName;

    void Start()
    {
        if (startChange)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(sceneName);
            }
        }).AddTo(this);
    }
}
