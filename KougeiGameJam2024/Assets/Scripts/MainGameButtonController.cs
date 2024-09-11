using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;

public class MainGameButtonController : MonoBehaviour
{
    [SerializeField] private Image ButtonImage;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color EnterColor;
    [SerializeField] private Color ClickColor;
    [SerializeField] private float SetTime;
    private Vector3 NormalSize;
    [SerializeField] private float EnterSize;
    [SerializeField] private float ClickSize;
    private MainGameButtonManager buttonManager => MainGameButtonManager.Instance;

    private enum PUSHTYPE
    { 
        GameStart,
        Title,
    }

    [SerializeField] private PUSHTYPE PushType; 

  

    private void Start()
    {
        NormalSize = transform.localScale;
    }
    public void EnterButton()
    {
        if (buttonManager.IsLoad)
            return;
        ButtonImage.DOColor(EnterColor, SetTime);
        transform.DOScale(NormalSize*EnterSize, SetTime);
        SoundManager.Instance.PlaySE(SoundManager.SEType.ButtonMove);
    }

    

    public void ExitButton()
    {
        if (buttonManager.IsLoad)
            return;
        ButtonImage.DOColor(NormalColor, SetTime);
        transform.DOScale(NormalSize, SetTime);
    }

    public void ClickButton()
    {
        if (buttonManager.IsLoad )
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
                case PUSHTYPE.GameStart: buttonManager.LoadScene("MainGameScene"); break;
                case PUSHTYPE.Title: buttonManager.LoadScene("Title"); break;

            }

        });

      
    }
    public void SetValue()
    {
        if (buttonManager.IsLoad)
            return;

        buttonManager.SetValue(this);
    }
}
