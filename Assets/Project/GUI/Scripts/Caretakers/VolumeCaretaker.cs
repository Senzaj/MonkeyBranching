using UnityEngine;
using UnityEngine.UI;

namespace Project.GUI.Scripts.Caretakers
{
    public class VolumeCaretaker : MonoBehaviour
    {
        [SerializeField] private Image _gramophoneIcon;
        [SerializeField] private Sprite _highVolumeIcon;
        [SerializeField] private Sprite _LowVolumeIcon;
        [SerializeField] private Button _theActualButton;

        private const int LowVolume = 0;
        private const int HighVolume = 1;
        private const string WasVolumeLow = nameof(WasVolumeLow);

        public void Launch()
        {
            _theActualButton.onClick.AddListener(IncreaseOrDecreaseVolume);

            if (PlayerPrefs.GetInt(WasVolumeLow) == 1)
                EstablishLowVolume();
            else
                EstablishHighVolume();
        }

        private void OnDisable() => _theActualButton.onClick.RemoveListener(IncreaseOrDecreaseVolume);

        private void IncreaseOrDecreaseVolume()
        {
            if (AudioListener.volume == HighVolume)
            {
                EstablishLowVolume();
            }
            else
            {
                EstablishHighVolume();
            }
        }

        private void EstablishHighVolume()
        {
            AudioListener.volume = HighVolume;
            PlayerPrefs.SetInt(WasVolumeLow, 0);
            _gramophoneIcon.sprite = _LowVolumeIcon;
        }
        
        private void EstablishLowVolume()
        {
            AudioListener.volume = LowVolume;
            PlayerPrefs.SetInt(WasVolumeLow, 1);
            _gramophoneIcon.sprite = _highVolumeIcon;
        }
    }
}
