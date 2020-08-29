using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class SandBox : MonoBehaviour
{
    [SerializeField]
    int WIDTH, HEIGHT, loopCount, addCompleteCount;
    [SerializeField]
    new Renderer renderer;

    Texture2D texture2D;

    const int
        COLOR_TYPES = 6;

    readonly Color32[] color32s = new Color32[]{
        new Color32(0, 0, 0, 0),
        new Color32(233, 184, 103, 255),
        new Color32(224, 171, 92, 255),
        new Color32(219, 159, 83, 255),
        new Color32(104, 54, 32, 255),
        new Color32(146, 104, 80, 255),
        new Color32(107, 57, 34, 255)
        };

    bool tempBool;
    bool[,,] array;
    int n;
    Color32[] colorArray;
    Color32[] clearArray;

    void Start()
    {
        array = new bool[2, WIDTH, HEIGHT];
        colorArray = new Color32[WIDTH * HEIGHT];

        texture2D = (Texture2D)renderer.material.mainTexture;
        texture2D = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGBA32, false);
        texture2D.filterMode = FilterMode.Bilinear;
        clearArray = texture2D.GetPixels32();

        //栗饅頭生成
        for (int y = HEIGHT / 16 * 15; y <= HEIGHT - 1; y++)
        {
            for (int x = WIDTH / 16 * 11; x < WIDTH / 16 * 13; x++)
            {
                array[0, x, y] = true;
            }
        }

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            GravityProcess();
            Draw();

        }).AddTo(this);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                Double();
            }
        }).AddTo(this);

        void GravityProcess()
        {
            //計算
            colorArray.CopyTo(clearArray, 0);
            n = 1 - n;
            for (int y = HEIGHT - 1; y > 0; y--)
            {
                for (int x = 1; x < WIDTH - 1; x++)
                {   //落下を受け取る
                    array[n, x, y] =
                        array[1 - n, x, y] || array[1 - n, x, y - 1];
                    //落下させる
                    array[1 - n, x, y - 1] =
                        array[1 - n, x, y - 1] && array[1 - n, x, y];
                }
            }

            //横移動
            for (int y = HEIGHT - 1; y > 0; y--)
            {
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    SideMove(x, y);
                    OnePixelDraw(x, y);
                }
                for (int x = WIDTH - 2; x > 0; x--)
                {
                    SideMove(x, y);
                    OnePixelDraw(x, y);
                }
            }
            void SideMove(int x, int y)
            {
                if (array[n, x, y] && array[1 - n, x, Mathf.Min(HEIGHT - 1, y + 1)])
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (UnityEngine.Random.value <= 0.68255f - 0.31645f * x / WIDTH)
                        {
                            tempBool = array[n, x - 1, y];
                            array[n, x - 1, y] = array[n, x, y];
                            array[n, x, y] = tempBool;
                        }
                        else
                        {
                            tempBool = array[n, x + 1, y];
                            array[n, x + 1, y] = array[n, x, y];
                            array[n, x, y] = tempBool;
                        }
                    }
                }
            }
        }

        void OnePixelDraw(int x, int y)
        {
            colorArray[x + y * WIDTH] = color32s[
                UnityEngine.Random.Range(1, COLOR_TYPES + 1)
                * Convert.ToInt32(array[n, x, y])
                ];
        }

        void Draw()
        {
            //描画
            texture2D.SetPixels32(colorArray);
            texture2D.Apply();
            renderer.material.mainTexture = texture2D;
        }

        void Double()
        {
            int addCount = 0;

            for (int loopCounter = 0; loopCounter < loopCount; loopCounter++)
            {
                addCount = 0;

                for (int y = 1; y < HEIGHT - 1; y++)
                {
                    for (int x = 1; x < WIDTH - 1; x++)
                    {
                        if (!array[n, x, y]) continue;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (!array[n, x + i, y - j])
                                {
                                    array[n, x + i, y - j] = true;
                                    addCount++;
                                }
                            }
                        }
                        if (addCount >= addCompleteCount) break;
                    }
                    if (addCount >= addCompleteCount) break;
                }
                if (addCount >= addCompleteCount) break;
            }
            loopCount++;
            if (addCompleteCount * 2f < int.MaxValue) addCompleteCount *= 2;
            else addCompleteCount = int.MaxValue;
        }
    }
}