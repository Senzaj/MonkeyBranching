using Project.GUI.Scripts.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.GUI.Scripts.Sheets
{
    public class MainSheet : Sheet
    {
        [SerializeField] private Button _loadGameSceneButton;
        [SerializeField] private Button _closeApplicationButton;

        private void OnEnable()
        {
            if (_viewed)
                StartViewing();
            else
                CoverCanvasGroup();

            _loadGameSceneButton.onClick.AddListener(LoadGame);
            _closeApplicationButton.onClick.AddListener(CloseGame);

            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.AddListener(StartViewing);
            _hideButton.onClick.AddListener(StartHiding);
        }

        private void OnDisable()
        {
            _loadGameSceneButton.onClick.RemoveListener(LoadGame);
            _closeApplicationButton.onClick.RemoveListener(CloseGame);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.RemoveListener(StartViewing);
            _hideButton.onClick.RemoveListener(StartHiding);
        }

        private void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        private void CloseGame()
        {
            Application.Quit();
        }
    }
}
