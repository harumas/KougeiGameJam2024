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
    [SerializeField] private string URL;
    private ButtonManager buttonManager => ButtonManager.Instance;

    private enum PUSHTYPE
    { 
        GameStart,
        Credit,
        ResetCredit,
        Finish,
        URL,
    }

    [SerializeField] private PUSHTYPE PushType; 

    private enum DISPLAYTYPE
    {
        Title,
        Credit
    }

    [SerializeField] private DISPLAYTYPE DisplayType;

    private void Start()
    {
        NormalSize = transform.localScale;
    }
    public void EnterButton()
    {
        if (buttonManager.IsLoad||(int)DisplayType!=buttonManager.DisplayButton)
            return;
        ButtonImage.DOColor(EnterColor, SetTime);
        transform.DOScale(NormalSize*EnterSize, SetTime);
        SoundManager.Instance.PlaySE(SoundManager.SEType.ButtonMove);
    }

    public void SetValue()
    {
        if (buttonManager.IsLoad || (int)DisplayType != buttonManager.DisplayButton)
            return;

       switch (DisplayType)
        { 
            case DISPLAYTYPE.Title:buttonManager.SetTitleValue(this); break;
            case DISPLAYTYPE.Credit:buttonManager.SetCreditValue(this);break;
        }
    }

    public void ExitButton()
    {
        if (buttonManager.IsLoad || (int)DisplayType != buttonManager.DisplayButton)
            return;
        ButtonImage.DOColor(NormalColor, SetTime);
        transform.DOScale(NormalSize, SetTime);
    }

    public void ClickButton()
    {
        if (buttonManager.IsLoad || (int)DisplayType != buttonManager.DisplayButton)
            return;
        SoundManager.Instance.PlaySE(SoundManager.SEType.PushButton);
        buttonManager.IsLoad = true;
        ButtonImage.DOColor(ClickColor, SetTime);
        transform.DOScale(NormalSize*ClickSize, SetTime).OnComplete(() =>
        {
            ButtonImage.DOColor(NormalColor, SetTime);
            transform.DOScale(NormalSize, SetTime);

            Debug.Log("Pushed");

            switch (PushType) 
            { 
                case PUSHTYPE.GameStart: buttonManager.LoadScene("MainScene"); break;
                case PUSHTYPE.Credit: buttonManager.SetCredit(); break;
                case PUSHTYPE.ResetCredit: buttonManager.ResetCredit();  break;
                case PUSHTYPE.Finish:
                    {
                        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                        #else
                        Application.Quit();
                        #endif
                        break;
                    }
                case PUSHTYPE.URL:
                    {
                        Application.OpenURL(URL);
                        buttonManager.IsLoad = false;
                        break;
                    }

            }

        });
       
       
    }
}
