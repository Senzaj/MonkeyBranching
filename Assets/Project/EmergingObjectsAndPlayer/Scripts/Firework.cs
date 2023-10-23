using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Firework : AbstractObject
    {
        [SerializeField] private Canon _canon;

        public void SetCanon(Canon canon) => _canon = canon;

        private void OnMouseDown() => _canon.TryHook(_rb);
    }
}
