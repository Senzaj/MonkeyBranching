using Project.GUI.Scripts.Abstract;

namespace Project.GUI.Scripts.Sheets
{
    public class GameSheet : Sheet
    {
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
    }
}
