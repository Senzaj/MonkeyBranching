using TMPro;
using UnityEngine;

namespace Project.GUI.Scripts.Caretakers
{
    public class RecordCaretaker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScoreText;
        
        private const int Summand = 1;
        private const string MaxSavedRecord = nameof(MaxSavedRecord);

        private int _currentScore;
        private int _maxRecord;

        public int MaxRecord => _maxRecord;

        public void Launch()
        {
            _currentScore = 0;

            _maxRecord = PlayerPrefs.HasKey(MaxSavedRecord) ? PlayerPrefs.GetInt(MaxSavedRecord) : 0;
            
            if (_currentScoreText != null)
                _currentScoreText.text = _currentScore.ToString();
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int TrySetRecord()
        {
            if (_currentScore > _maxRecord)
            {
                _maxRecord = _currentScore;
                PlayerPrefs.SetInt(MaxSavedRecord, _maxRecord);
            }

            return _maxRecord;
        }

        public void AddPoint()
        {
            _currentScore += Summand;
            _currentScoreText.text = _currentScore.ToString();
        }
    }
}
