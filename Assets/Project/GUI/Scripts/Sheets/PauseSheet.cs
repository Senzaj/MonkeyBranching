using Project.GUI.Scripts.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.GUI.Scripts.Sheets
{
    public class PauseSheet : Sheet
    {
        [SerializeField] private Button _loadStartSceneButton;
        
        private void OnEnable()
        {
            if (_viewed)
                StartViewing();
            else
                CoverCanvasGroup();
            
            _loadStartSceneButton.onClick.AddListener(LoadStartScreen);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.AddListener(StartViewing);
            _hideButton.onClick.AddListener(StartHiding);
        }

        private void OnDisable()
        {
            _loadStartSceneButton.onClick.RemoveListener(LoadStartScreen);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.RemoveListener(StartViewing);
            _hideButton.onClick.RemoveListener(StartHiding);
        }
        
        private void LoadStartScreen()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
