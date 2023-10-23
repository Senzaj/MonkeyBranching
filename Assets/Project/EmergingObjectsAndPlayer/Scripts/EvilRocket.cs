using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class EvilRocket : AbstractObject
    {
        [SerializeField] private Canon _canon;
        [SerializeField] private ParticleSystem _explosionParticles;

        public void SetCanon(Canon canon) => _canon = canon;
        
        public void OnExploded()
        {
            Instantiate(_explosionParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Firework firework))
                firework.gameObject.SetActive(false);
        }

        private void OnMouseDown() => _canon.TryHook(_rb);
    }
}
