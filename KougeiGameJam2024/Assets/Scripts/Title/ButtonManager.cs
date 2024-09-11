using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private ButtonController[] TitleButtons;
    [SerializeField] private ButtonController[] CreditButtons;
    private int Value = -1;
    [SerializeField] private Image Fade;
    [SerializeField] private GameObject Credit;
    private Vector3 CreditScale;
    [NonSerialized] public static ButtonManager Instance;
    [NonSerialized] public bool IsLoad = false;
    [NonSerialized] public int DisplayButton = 0;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this.gameObject);

        SoundManager.Instance.PlayBGM(SoundManager.BGMType.Title);
    }

    private void Start()
    {
        CreditScale = Credit.transform.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           switch(DisplayButton) 
            {
                case 0:UpButton(TitleButtons);
                break;
                case 1:UpButton(CreditButtons); break;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            switch (DisplayButton)
            {
                case 0: DownButton(TitleButtons); break;
                case 1: DownButton(CreditButtons); break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (DisplayButton)
            {
                case 0: ClickButton(TitleButtons); break;
                case 1: ClickButton(CreditButtons); break;
            }
        }
    }

    private void UpButton(ButtonController[]buttons)
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

    private void DownButton(ButtonController[] buttons)
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

    private void ClickButton(ButtonController[] buttons)
    {
        if (Value < 0 && Value >= buttons.Length)
            return;

        Debug.Log("Pushed");
        SoundManager.Instance.PlaySE(SoundManager.SEType.Win);
        buttons[Value].ClickButton();
    }
    public void LoadScene(string SceneName)
    {
        Fade.DOFade(1, 0.5f).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }

    public void SetTitleValue(ButtonController button)
    {
        if(Value>=0&&Value<=TitleButtons.Length)
            TitleButtons[Value].ExitButton();
        for (int i = 0; i <= TitleButtons.Length; i++)
        {
            if (button == TitleButtons[i])
            {
                Value = i;
                TitleButtons[Value].EnterButton();
                return;
            }
        }
    }
    public void SetCreditValue(ButtonController button)
    {
        if (Value >= 0 && Value <= CreditButtons.Length)
            CreditButtons[Value].ExitButton();
        for(int i = 0;i <= CreditButtons.Length;i++)
        {
            if(button == CreditButtons[i])
            {
                Value = i;
                CreditButtons[Value].EnterButton();
                return;
            }
        }
    }
    public void SetCredit()
    {
        Credit.transform.localScale = Vector3.zero;
        Credit.SetActive(true);
        Credit.transform.DOScale(CreditScale, 0.5f).OnComplete(() =>
        {
            IsLoad = false;
            Value = -1;
            DisplayButton = 1;
        });
    }

    public void ResetCredit()
    {
        Credit.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            Credit.SetActive(false);
            IsLoad = false;
            Value = -1;
            DisplayButton = 0;
        });
    }
}
