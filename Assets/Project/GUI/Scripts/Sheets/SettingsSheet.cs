using Project.GUI.Scripts.Abstract;
using TMPro;
using UnityEngine;

namespace Project.GUI.Scripts.Sheets
{
    public class SettingsSheet : Sheet
    {
        [SerializeField] private TMP_Text _maxRecordText;
        
        private void OnEnable()
        {
            if (_viewed)
                StartViewing();
            else
                CoverCanvasGroup();

            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.AddListener(StartViewing);
            _hideButton.onClick.AddListener(StartHiding);
        }
        
        private void OnDisable()
        {
            if (_viewButton == null || _hideButton == null) return;
            _viewButton.onClick.RemoveListener(StartViewing);
            _hideButton.onClick.RemoveListener(StartHiding);
        }
        
        public void SetMaxRecord(int maxRecord)
        {
            _maxRecordText.text = maxRecord.ToString();
        }
    }
}
