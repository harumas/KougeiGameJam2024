using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownMesh;
    [SerializeField] private PlayerMovement lPlayerMovement;
    [SerializeField] private PlayerMovement rPlayerMovement;
    [SerializeField] private TextMeshProUGUI lPlayerHoldText;
    [SerializeField] private TextMeshProUGUI rPlayerHoldText;
    [SerializeField] private bool skipCountDown;
    private bool isStarting = false;

    public event Action CountDownStart;
    public event Action CountDownEnd;

    private void Update()
    {
        if (isStarting)
        {
            return;
        }

        UpdateKeyState();

        if (IsBothPushing())
        {
            isStarting = true;

            if (skipCountDown)
            {
                lPlayerMovement.Begin();
                rPlayerMovement.Begin();
                return;
            }

            StartCoroutine(nameof(StartSequence));
        }
    }

    private IEnumerator StartSequence()
    {
        CountDownStart?.Invoke();

        countDownMesh.text = "3";
        countDownMesh.enabled = true;
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            CancelCountDown();
            yield break;
        }

        countDownMesh.text = "2";
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            CancelCountDown();
            yield break;
        }

        countDownMesh.text = "1";
        yield return new WaitForSeconds(1f);

        if (!IsBothPushing())
        {
            CancelCountDown();
            yield break;
        }

        countDownMesh.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        countDownMesh.enabled = false;

        lPlayerMovement.Begin();
        rPlayerMovement.Begin();
    }

    private void CancelCountDown()
    {
        countDownMesh.enabled = false;
        isStarting = false;
        CountDownEnd?.Invoke();
    }

    private void UpdateKeyState()
    {
        lPlayerHoldText.enabled = !Input.GetKey(KeyCode.D);
        rPlayerHoldText.enabled = !Input.GetKey(KeyCode.LeftArrow);
    }

    private bool IsBothPushing()
    {
        return Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow);
    }
}