using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSwitching : MonoBehaviour
{
    [Header("ここにアニメーションさせたい画像を入れる")]
    [SerializeField]private Sprite[] Images;
    [Header("ここにアニメーションさせるスプライトレンダラーを入れる")]
    [SerializeField]private SpriteRenderer TargetSpriteRenderer;
    [Header("画像が切り替わる速さ")]
    [SerializeField]private float SwitchSpeed;
    [Header("アニメーションを止める/動かす")]
    [SerializeField]private bool EnableAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(SwitchCoroutine == null && EnableAnimation){
            SwitchCoroutine = StartCoroutine(ImageSwitch());
        }else if(SwitchCoroutine != null && !EnableAnimation){
            StopCoroutine(SwitchCoroutine);
        }
    }
    Coroutine SwitchCoroutine;
    IEnumerator ImageSwitch()
    {
        while(true)
            {
                for(int i = 0;i <= Images.Length-1;i++)
                {
                    TargetSpriteRenderer.sprite = Images[i];
                    yield return new WaitForSeconds (SwitchSpeed);
                }
                yield return null;
            }  
    }

}
