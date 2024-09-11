using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundSequence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private StartCountDown startCountDown;
    [SerializeField] private GameObject nextRoundButton;
    [SerializeField] private GameObject backToTitleButton;
    [SerializeField] private Image fadeInImage;
    [SerializeField] private float fadeInDelay = 1f;
    [SerializeField] private float gotoNextRoundDelay = 2f;
    private MainGameButtonManager buttonManager => MainGameButtonManager.Instance;

    private void Start()
    {


        roundText.text = $"Round {GameStateData.Instance.Round + 1}";
        FadeOut();

        scoreManager.OnScored += (isRight, score) =>
        {
            if (GameStateData.Instance.IsGameEnd)
            {
                SoundManager.Instance.PlaySE(SoundManager.SEType.Win);
                string winnerName = GameStateData.Instance.LPlayerHp == 0 ? "中野" : "厚木";
                roundText.text = $"{winnerName}の勝利！";
                buttonManager.IsLoad = false;
                roundText.gameObject.SetActive(true);
                nextRoundButton.gameObject.SetActive(true);
                backToTitleButton.gameObject.SetActive(true);

                GameStateData.Instance.Reset();
            }
            else
            {
                Debug.Log("fadin");
                GameStateData.Instance.IncrementRound();
                FadeIn().OnComplete(GoToNextRound).SetDelay(fadeInDelay);
            }
        };

        startCountDown.CountDownStart += () =>
        {
            roundText.gameObject.SetActive(false);
        };

        startCountDown.CountDownEnd += () =>
        {
            roundText.gameObject.SetActive(true);
        };
    }

    private TweenerCore<Color, Color, ColorOptions> FadeOut()
    {
        fadeInImage.gameObject.SetActive(true);
        var color = fadeInImage.color;
        color.a = 1f;
        fadeInImage.color = color;
        Debug.Log(2);
        return fadeInImage.DOFade(0f, gotoNextRoundDelay).OnComplete(() => fadeInImage.gameObject.SetActive(false));
    }

    private TweenerCore<Color, Color, ColorOptions> FadeIn()
    {
        fadeInImage.gameObject.SetActive(true);
        var color = fadeInImage.color;
        color.a = 0f;
        fadeInImage.color = color;
        return fadeInImage.DOFade(1f, gotoNextRoundDelay).OnComplete(() => fadeInImage.gameObject.SetActive(false));
    }

    private void GoToNextRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}