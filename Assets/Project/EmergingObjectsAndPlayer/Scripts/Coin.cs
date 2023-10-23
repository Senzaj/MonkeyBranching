using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Coin : AbstractObject
    {
        [SerializeField] private ParticleSystem _onTakenParticles; 
            
        public void OnTaken()
        {
            Instantiate(_onTakenParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
