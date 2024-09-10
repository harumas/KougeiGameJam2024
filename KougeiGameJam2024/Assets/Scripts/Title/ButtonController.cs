using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Image ButtonImage;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color EnterColor;
    [SerializeField] private Color ClickColor;
    [SerializeField] private float SetTime;
    private Vector3 NormalSize;
    [SerializeField] private float EnterSize;
    [SerializeField] private float ClickSize;
    private ButtonManager buttonManager => ButtonManager.Instance;

    private enum TYPE
    { 
        GameStart,
        Finish
    }

    [SerializeField] private TYPE type; 

    private void Start()
    {
        NormalSize = transform.localScale;
    }
    public void EnterButton()
    {
        if (buttonManager.Load)
            return;
        ButtonImage.DOColor(EnterColor, SetTime);
        transform.DOScale(NormalSize*EnterSize, SetTime);
    }

    public void ExitButton()
    {
        if (buttonManager.Load)
            return;
        ButtonImage.DOColor(NormalColor, SetTime);
        transform.DOScale(NormalSize, SetTime);
    }

    public void ClickButton()
    {
        if (buttonManager.Load)
            return;
        buttonManager.Load = true;
        ButtonImage.DOColor(ClickColor, SetTime/2);
        transform.DOScale(NormalSize*ClickSize, SetTime/2).OnComplete(() =>
        {
            ButtonImage.DOColor(EnterColor, SetTime/2);
            transform.DOScale(NormalSize*EnterSize, SetTime/2);

            switch (type) 
            { 
                case TYPE.GameStart:buttonManager.LoadScene(""); break;
                case TYPE.Finish:
                    {
                        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                        #else
                        Application.Quit();
                        #endif
                        break;
                    }

            }

        });
       
       
    }
}
