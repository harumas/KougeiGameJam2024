using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]private PlayerMovement lPlayerMovement;
    [SerializeField]private PlayerMovement rPlayerMovement;
    [SerializeField]private ScoreManager scoreManager;
    [Header("キャラクターのImageコンポーネントが入ったオブジェクト")]
    [SerializeField]private GameObject lRendererObj;
    [SerializeField]private GameObject rRendererObj;
    private SpriteRenderer lPlayerImage;
    private SpriteRenderer rPlayerImage;
    [Header("負けた時の画像")]
    [SerializeField]private Sprite lDefeatCharacterSprite;
    [SerializeField]private Sprite rDefeatCharacterSprite;
    [Header("歩くアニメーションに使う画像")]
    [SerializeField]private Sprite[] lCharacterSprites;
    [SerializeField]private Sprite[] rCharacterSprites;

    [SerializeField]private GameObject PattinEffect;

    [SerializeField]private float ImageSwitchDuration;

    bool isAnimationEnabled = true;

    bool isAnimationEnabled_R = true,isAnimationEnabled_l = true;

    // Start is called before the first frame update
    void Start()
    {
        isAnimationEnabled = true;

        lPlayerImage = lRendererObj.GetComponent<SpriteRenderer>();
        rPlayerImage = rRendererObj.GetComponent<SpriteRenderer>();

        scoreManager.OnScored += (isRight, score) =>
        {
            OnDamaged(isRight);
        };
    }

    void Update()
    {
        if(lCoroutine == null && lPlayerMovement.isWalking_L)
        {
            lCoroutine = StartCoroutine(AnimationCoroutine(lPlayerImage,false));
        }else if(lCoroutine != null && !lPlayerMovement.isWalking_L)
        {
            StopCoroutine(lCoroutine);
            lCoroutine = null;
        }

        if(rCoroutine == null && rPlayerMovement.isWalking_R)
        {
            rCoroutine = StartCoroutine(AnimationCoroutine(rPlayerImage,true));
        }else if(rCoroutine != null && !rPlayerMovement.isWalking_R)
        {
            StopCoroutine(rCoroutine);
            rCoroutine = null;
        }
    }

    void OnDamaged(bool isRight)
    {
        isAnimationEnabled = false;
        SoundManager.Instance.PlaySE(SoundManager.SEType.HitByRubber);

        if(isRight)
        {
            rPlayerImage.sprite = rDefeatCharacterSprite;
            Vector3 AdditionalRotation = new Vector3(0,180,0);
            Destroy(Instantiate(PattinEffect,rRendererObj.transform.position,Quaternion.Euler(AdditionalRotation)),1);
            rRendererObj.transform.DOLocalRotate(new Vector3(0, 0, -720f), 1f,  
            RotateMode.FastBeyond360);
        }else{
            lPlayerImage.sprite = lDefeatCharacterSprite;
            Destroy(Instantiate(PattinEffect,lRendererObj.transform.position,Quaternion.identity),1);
            lRendererObj.transform.DOLocalRotate(new Vector3(0, 0, 720f), 1f,  
            RotateMode.FastBeyond360);
        }


          
    }

    Coroutine lCoroutine,rCoroutine;
    IEnumerator AnimationCoroutine(SpriteRenderer targetRenderer,bool isRight)
    {
        if(isRight)
        {
          while(true)
            {
                for(int i = 0;i <= rCharacterSprites.Length-1;i++)
                {
                    targetRenderer.sprite = rCharacterSprites[i];
                    yield return new WaitForSeconds (ImageSwitchDuration);
                }
                yield return null;
            }  
        }else{
            while(true)
            {
                
                for(int i = 0;i <= lCharacterSprites.Length-1;i++)
                {
                    targetRenderer.sprite = lCharacterSprites[i];
                    
                    yield return new WaitForSeconds (ImageSwitchDuration);
                }
                yield return null;
            }  
        }
    }

    
}
