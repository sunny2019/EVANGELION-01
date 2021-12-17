using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Mode
{
    public class HotUpdate_Canvas : MonoBehaviour
    {
        [SerializeField] private Transform _modalWindow;
        [SerializeField] private Transform _modalConfimTitle;
        [SerializeField] private Transform _modalConfimContent;
        [SerializeField] private Transform _modalConfimButton;
        [SerializeField] private Transform _modalCancleButton;

        [SerializeField] private Transform _progressBar;
        [SerializeField] private Transform _progressBarProgress;
        [SerializeField] private Transform _progressBarTitle;
        [SerializeField] private Transform _tip;

        public void ResetUI()
        {
            _modalWindow.gameObject.SetActive(false);
            _progressBar.gameObject.SetActive(false);
            _tip.gameObject.SetActive(false);
        }

        public void ShowModal(string title, string content, Action confim = null, Action cancle = null)
        {
            _modalWindow.gameObject.SetActive(true);
            _modalConfimTitle.GetComponent<TMP_Text>().text = title;
            _modalConfimContent.GetComponent<TMP_Text>().text = content;

            _modalConfimButton.GetComponent<Button>().onClick.RemoveAllListeners();
            _modalCancleButton.GetComponent<Button>().onClick.RemoveAllListeners();
            _modalConfimButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                confim?.Invoke();
                HideModal();
            });
            _modalCancleButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                cancle?.Invoke();
                HideModal();
            });
        }

        public void HideModal()
        {
            _modalWindow.gameObject.SetActive(false);
        }

        public void ShowProgress(long cur, long all)
        {
            _progressBar.gameObject.SetActive(true);
            _progressBarTitle.GetComponent<TMP_Text>().text = FileSizeCovert.BytesToMb(cur) + "MB/" + FileSizeCovert.BytesToMb(all) + "MB";
            _progressBarProgress.GetComponent<Image>().fillAmount = cur / (float) all;
            LogCurrentProgress(cur, all);
        }
        
        private static void LogCurrentProgress(long cur, long all)
        {
            Debug.Log(cur + "/" + all
                      + "\t" + FileSizeCovert.BytesToMb(cur) + "MB/" + FileSizeCovert.BytesToMb(all) + "MB"
                      + "\t" + cur / (float) all);
        }

        public void HideProgress()
        {
            _progressBar.gameObject.SetActive(false);
        }


        public void ShowTip(string content)
        {
            _tip.gameObject.SetActive(true);
            _tip.GetComponent<TMP_Text>().text = content;
        }

        public void HideTip()
        {
            _tip.gameObject.SetActive(false);
        }
    }
}