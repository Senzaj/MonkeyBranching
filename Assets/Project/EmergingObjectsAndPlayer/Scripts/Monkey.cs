using System;
using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Monkey : MonoBehaviour
    {
        [SerializeField] private Animator _monkeyAnimator;
        [SerializeField] private ParticleSystem _explodedFx;
        
        public event Action Exploded;
        public event Action TookCoin;
        
        private void OnTriggerEnter2D(Collider2D collisionObj)
        {
            if (collisionObj.TryGetComponent(out EvilRocket rocket))
            {
                OnTakenDown();
                rocket.OnExploded();
                Exploded?.Invoke();
            }

            if (collisionObj.TryGetComponent(out Coin coin))
            {
                _monkeyAnimator.Play("OnCoinTaken");
                coin.OnTaken();
                TookCoin?.Invoke();
            }

            if (collisionObj.TryGetComponent(out Nowhere _))
            {
                OnTakenDown();
                
                Exploded?.Invoke();
            }
        }

        public void OnShoot()
        {
            _monkeyAnimator.Play("OnShoot");
        }
        
        public void OnTakenDown()
        {
            Instantiate(_explodedFx, transform.position, Quaternion.identity);
            _monkeyAnimator.Play("OnTakenDown");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.gravityScale = 1.5f;
        }
    }
}
