using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShieldJudge : MonoBehaviour
    {
        [SerializeField] private PlayerMovement lPlayer;
        [SerializeField] private PlayerMovement rPlayer;
        [SerializeField] private Shield lShield;
        [SerializeField] private Shield rShield;
        [SerializeField] private ZoomCamera zoomCamera;
        [SerializeField] private GameObject barrierObject;
        [SerializeField] private float moveCameraDelay = 1f;

        public event Action<bool, bool> Damaged;

        private void Start()
        {
            lPlayer.OnReleased += OnPlayerReleased;
            rPlayer.OnReleased += OnPlayerReleased;
        }

        private void OnPlayerReleased(bool isRight)
        {
            if (isRight)
            {
                Debug.Log(lPlayer.IsUsingShield);
                if (lPlayer.IsUsingShield)
                {
                    zoomCamera.SetEaseDuration(1.3f);
                    lShield.Play();
                    Invoke(nameof(SwitchCameraToRight), moveCameraDelay);
                }
                else
                {
                    Damaged?.Invoke(true, false);
                }
            }
            else
            {
                if (rPlayer.IsUsingShield)
                {
                    zoomCamera.SetEaseDuration(1.3f);
                    rShield.Play();
                    Invoke(nameof(SwitchCameraToLeft), moveCameraDelay);
                }
                else
                {
                    Damaged?.Invoke(false, false);
                }
            }
        }

        private void DamageToRight()
        {
            Damaged?.Invoke(true, true);
        }

        private void DamageToLeft()
        {
            Damaged?.Invoke(false, true);
        }

        private void SwitchCameraToRight()
        {
            zoomCamera.SwitchCameraPriority(false);
            barrierObject.SetActive(true);
            barrierObject.transform.position += new Vector3(-10, 0, 0);
            barrierObject.transform.DOScaleX(100f, 0.7f).SetRelative();
            Invoke(nameof(DamageToRight), 1.2f);
        }

        private void SwitchCameraToLeft()
        {
            zoomCamera.SwitchCameraPriority(true);
            barrierObject.SetActive(true);
            barrierObject.transform.position += new Vector3(10, 0, 0);
            barrierObject.transform.DOScaleX(100f, 0.7f).SetRelative();
            Invoke(nameof(DamageToLeft), 1.2f);
        }
    }
}