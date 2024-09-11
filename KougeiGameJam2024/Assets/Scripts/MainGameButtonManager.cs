using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainGameButtonManager : MonoBehaviour
{
    [SerializeField] private MainGameButtonController[] Buttons;
    private int Value = -1;
    [NonSerialized] public static MainGameButtonManager Instance;
    [NonSerialized] public bool IsLoad = true;
   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this.gameObject);

    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGMType.Title);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            UpButton(Buttons);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DownButton(Buttons);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickButton(Buttons);
        }
    }

    private void UpButton(MainGameButtonController[]buttons)
    {
        if (Value == -1)
            Value = 0;


        else
        {
            buttons[Value].ExitButton();
            Value = Value - 1;
        }


        if (Value < 0)
            Value = buttons.Length-1;

        buttons[Value].EnterButton();
    }

    private void DownButton(MainGameButtonController[] buttons)
    {
        if (Value == -1)
            Value = 0;


        else
        {
            buttons[Value].ExitButton();
            Value = Value + 1;
        }


        if (Value >= buttons.Length)
            Value = 0;

        buttons[Value].EnterButton();
    }

    private void ClickButton(MainGameButtonController[] buttons)
    {
        if (Value < 0 && Value >= buttons.Length)
            return;

        Debug.Log("Pushed");
        SoundManager.Instance.PlaySE(SoundManager.SEType.Win);
        buttons[Value].ClickButton();
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void SetValue(MainGameButtonController button)
    {
        if(Value>=0&&Value<=Buttons.Length)
            Buttons[Value].ExitButton();
        for (int i = 0; i <= Buttons.Length; i++)
        {
            if (button == Buttons[i])
            {
                Value = i;
                Buttons[Value].EnterButton();
                return;
            }
        }
    }
    
}
