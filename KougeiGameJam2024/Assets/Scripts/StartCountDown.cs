using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownMesh;
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;
    [SerializeField] private bool skipCountDown;
    private bool isStarting = false;

    private void Update()
    {
        if (isStarting)
        {
            return;
        }

        if (IsBothPushing())
        {
            if (skipCountDown)
            {
                lPlayerMovement.Begin();
                rPlayerMovement.Begin();
                return;
            }

            isStarting = true;
            StartCoroutine(nameof(StartSequence));
        }
    }

    private IEnumerator StartSequence()
    {
        countDownMesh.text = "3";
        countDownMesh.enabled = true;
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            countDownMesh.enabled = false;
            isStarting = false;
            yield break;
        }

        countDownMesh.text = "2";
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            countDownMesh.enabled = false;
            isStarting = false;
            yield break;
        }

        countDownMesh.text = "1";
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            countDownMesh.enabled = false;
            isStarting = false;
            yield break;
        }

        countDownMesh.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        countDownMesh.enabled = false;

        lPlayerMovement.Begin();
        rPlayerMovement.Begin();
    }

    private bool IsBothPushing()
    {
        return Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow);
    }
}