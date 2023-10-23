using Project.GUI.Scripts.Caretakers;
using Project.GUI.Scripts.Sheets;
using Project.Indefinite.Scripts;
using UnityEngine;

namespace Main
{
    public class LaunchManager : MonoBehaviour
    {
        [SerializeField] private bool _isMain;
        [SerializeField] private GameCaretaker _gameCaretaker;
        [SerializeField] private TutorialSheet _tutorialSheet;
        [SerializeField] private VolumeCaretaker _volumeCaretaker;
        [SerializeField] private RecordCaretaker _recordCaretaker;
        [SerializeField] private SettingsSheet _settingsSheet;

        private void Awake()
        {
            if (_isMain == false)
            {
                SetLandscape();
                _tutorialSheet.Launch();
                _recordCaretaker.Launch();
                _gameCaretaker.Launch();
                _volumeCaretaker.Launch();
            }
            else
            {
                SetPortrait();
                _recordCaretaker.Launch();
                _volumeCaretaker.Launch();
                _settingsSheet.SetMaxRecord(_recordCaretaker.MaxRecord);
            }
        }

        private static void SetLandscape()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
        }

        private static void SetPortrait()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
        }
    }
}
