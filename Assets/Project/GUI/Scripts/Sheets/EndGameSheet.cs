using Project.GUI.Scripts.Abstract;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Project.GUI.Scripts.Sheets
{
    public class EndGameSheet : Sheet
    {
        [SerializeField] private TMP_Text _sessionScoreText;
        [SerializeField] private TMP_Text _maxScoreText;
        [SerializeField] private Sheet _gameSheet;

        [SerializeField] private Button _reloadGameSceneButton;
        [SerializeField] private Button _loadStartScreenButton;
        
        private void OnEnable()
        {
            if (_viewed)
                StartViewing();
            else
                CoverCanvasGroup();
            
            _reloadGameSceneButton.onClick.AddListener(ReloadGame);
            _loadStartScreenButton.onClick.AddListener(LoadStartScreen);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.AddListener(StartViewing);
            _hideButton.onClick.AddListener(StartHiding);
        }

        private void OnDisable()
        {
            _reloadGameSceneButton.onClick.RemoveListener(ReloadGame);
            _loadStartScreenButton.onClick.RemoveListener(LoadStartScreen);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.RemoveListener(StartViewing);
            _hideButton.onClick.RemoveListener(StartHiding);
        }

        public void OnLose(int currentScore, int maxScore)
        {
            _gameSheet.StartHiding();
            SetResults(currentScore, maxScore);
        }
        
        public void SetResults(int currentScore, int maxScore)
        {
            _sessionScoreText.text = currentScore.ToString();
            _maxScoreText.text = maxScore.ToString();
        }
        
        private void ReloadGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        private void LoadStartScreen()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
