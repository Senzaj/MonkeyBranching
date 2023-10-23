using System.Collections;
using Project.EmergingObjectsAndPlayer.Scripts;
using Project.GUI.Scripts.Caretakers;
using Project.GUI.Scripts.Sheets;
using UnityEngine;

namespace Project.Indefinite.Scripts
{
    public class GameCaretaker : MonoBehaviour
    {
        [SerializeField] private ObjectsDisposer _disposer;
        [SerializeField] private GameObject _startRocket;
        [SerializeField] private TutorialSheet _tutorialSheet;
        [SerializeField] private RecordCaretaker _recordCaretaker;
        [SerializeField] private EndGameSheet _endGameSheet;
        [SerializeField] private Canon _canon;
        [SerializeField] private Monkey _monkey;

        private bool _lost;
        
        public void Launch()
        {
            _lost = false;
            _canon.TouchedEvilRocket += OnLost;
            _monkey.Exploded += OnLost;
            _monkey.TookCoin += OnCoinTaken;
            _canon.Moved += StartGame;
            _tutorialSheet.Explained += StartGame;
            
            if (_tutorialSheet.GetTutorialStatus() == false)
                _tutorialSheet.Explain();
            
            _disposer.Launch();
        }

        private void OnDisable()
        {
            _canon.TouchedEvilRocket -= OnLost;
            _monkey.Exploded -= OnLost;
            _monkey.TookCoin -= OnCoinTaken;
            _canon.Moved -= StartGame;
            _tutorialSheet.Explained -= StartGame;
        }

        private void StartGame()
        {
            _startRocket.GetComponent<CapsuleCollider2D>().enabled = false;
            _disposer.StartDisposing();
        }

        private void OnCoinTaken()
        {
            _recordCaretaker.AddPoint();
        }
        
        private void OnLost()
        {
            if (_lost) return;
            _lost = true;
            _endGameSheet.OnLose(_recordCaretaker.GetCurrentScore(), _recordCaretaker.TrySetRecord());
            StartCoroutine(ViewingEndGameSheet());
        }
        
        private IEnumerator ViewingEndGameSheet()
        {
            yield return new WaitForSeconds(1f);
            _endGameSheet.StartViewing();
        }
    }
}
