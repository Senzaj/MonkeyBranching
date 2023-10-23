using System;
using Project.GUI.Scripts.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Project.GUI.Scripts.Sheets
{
    public class TutorialSheet : Sheet
    {
        [SerializeField] private TutorialListSheet[] _lists;
        [SerializeField] private Button _continueButton;

        public event Action Explained;

        private const string Tutorialized = nameof(Tutorialized);
        private const string Yes = nameof(Yes);
        private const int SheetIndexDiff = 1;
        private bool _isTutorialized;
        
        private int _tempPanelIndex;
        
        public void Launch()
        {
            if (PlayerPrefs.GetString(Tutorialized) == Yes)
                _isTutorialized = true;

            CoverCanvasGroup();

            _continueButton.onClick.AddListener(TryViewList);

            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.AddListener(StartViewing);
            _hideButton.onClick.AddListener(StartHiding);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(TryViewList);
            
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.RemoveListener(StartViewing);
            _hideButton.onClick.RemoveListener(StartHiding);
        }

        public bool GetTutorialStatus()
        {
            return _isTutorialized;
        }
        
        public void Explain()
        {
            CoverLists();
            StartViewing();
            TryViewList();
        }
        
        private void CoverLists()
        {
            foreach (TutorialListSheet menu in _lists)
                menu.CoverCanvasGroup();
        }
        
        private void TryViewList()
        {
            if (_tempPanelIndex < _lists.Length)
            {
                if (_tempPanelIndex > 0)
                    _lists[_tempPanelIndex - SheetIndexDiff].StartHiding();

                _lists[_tempPanelIndex++].StartViewing();
            }
            else
            {
                _isTutorialized = true;
                PlayerPrefs.SetString(Tutorialized, Yes);
                StartHiding();
                Explained?.Invoke();
            }
        }
    }
}
