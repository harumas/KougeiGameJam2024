using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private ParticleSystem barrierParticle;
    [SerializeField] private SpriteRenderer barrierSprite;

    private bool stopNotice;
    private bool enableNotice;

    private void Start()
    {
        StopNotice();
        StartCoroutine(nameof(NoticeSequence));
    }

    public void Notice()
    {
        if (enableNotice)
        {
            return; 
        }
        
        enableNotice = true;
        barrierSprite.enabled = true;
    }

    public void StopNotice()
    {
        if (!enableNotice)
        {
            return; 
        }
        
        enableNotice = false;
        barrierSprite.enabled = false;
    }

    private IEnumerator NoticeSequence()
    {
        bool show = true;

        while (!stopNotice)
        {
            show = !show;
            barrierSprite.enabled = show;
            yield return new WaitForSeconds(0.3f);
            yield return new WaitUntil(() => enableNotice);
        }
    }

    public void Play()
    {
        stopNotice = true;
        barrierSprite.enabled = true;
        barrierParticle.Play();
    }
}