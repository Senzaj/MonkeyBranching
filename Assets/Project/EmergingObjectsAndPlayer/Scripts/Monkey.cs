using System;
using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Monkey : MonoBehaviour
    {
        public event Action Exploded;
        public event Action TookCoin;
        
        private void OnTriggerEnter2D(Collider2D collisionObj)
        {
            if (collisionObj.TryGetComponent(out EvilRocket rocket))
            {
                //
                rocket.OnExploded();
                
                Exploded?.Invoke();
            }

            if (collisionObj.TryGetComponent(out Coin coin))
            {
                //
                
                coin.OnTaken();
                
                TookCoin?.Invoke();
            }

            if (collisionObj.TryGetComponent(out Nowhere _))
            {
                //
                
                TookCoin?.Invoke();
            }
        }
    }
}
