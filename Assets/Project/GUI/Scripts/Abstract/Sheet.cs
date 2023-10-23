using Project.EmergingObjectsAndPlayer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Project.GUI.Scripts.Abstract
{
    public abstract class Sheet : MonoBehaviour
    {
        [SerializeField] protected bool _viewed;
        [SerializeField] private bool _stopTime;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Animator _animator;
        [SerializeField] protected Button _viewButton;
        [SerializeField] protected Button _hideButton;
        [SerializeField] private Canon _canon;

        private static string Hiding = nameof(Hiding);
        private static string Viewing = nameof(Viewing);
        private static string Idle = nameof(Idle);

        public void StartViewing()
        {
            ShowCanvasGroup();
            _animator.Play(Viewing);

            if (_stopTime)
            {
                Time.timeScale = 0;
                _canon.DisableHook();
            }
        }

        public void StartHiding() => _animator.Play(Hiding);

        private void View() => _animator.Play(Idle);
        

        private void Hide()
        {
            CoverCanvasGroup();
            _animator.Play(Idle);
            
            if (_stopTime)
            {
                Time.timeScale = 1;
                _canon.EnableHook();
            }
        }

        public void ShowCanvasGroup()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        
        public void CoverCanvasGroup()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
