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
    [SerializeField] private ButtonController[] buttons;
    private int Value = -1;
    [SerializeField] private Image Fade;
    [NonSerialized]public static ButtonManager Instance;
    [NonSerialized] public bool Load = false;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        else
            Destroy(this.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(Value == -1)
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Value == -1)
            {
                Value = 0;
            }

            else
            {
                buttons[Value].ExitButton();
                Value = Value - 1;
            }

            if (Value < 0)
                Value = buttons.Length - 1;

            buttons[Value].EnterButton();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            buttons[Value].ClickButton();
        }
    }

    public void LoadScene(string SceneName)
    {
        Fade.DOFade(1,0.5f).OnComplete(() =>
        {
           // SceneManager.LoadScene(SceneName);
        });
    }
}
