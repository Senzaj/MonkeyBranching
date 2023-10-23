using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class EvilRocket : AbstractObject
    {
        [SerializeField] private Canon _canon;

        public void SetCanon(Canon canon) => _canon = canon;
        
        public void OnExploded()
        {
            
            gameObject.SetActive(false);
        }
        
        private void OnMouseDown() => _canon.TryHook(_rb);
    }
}
