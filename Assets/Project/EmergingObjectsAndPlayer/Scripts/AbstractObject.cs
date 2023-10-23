using System.Collections;
using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public abstract class AbstractObject : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected Rigidbody2D _rb;

        private Coroutine _movementRoutine;

        public void StartMove()
        {
            _movementRoutine = StartCoroutine(Movement());
        }

        protected void StopMove()
        {
            if (_movementRoutine != null)
                StopCoroutine(_movementRoutine);
        }
        
        private IEnumerator Movement()
        {
            while (true)
            {
                _rb.velocity = Vector2.left * _moveSpeed;
                yield return null;
            }
        }
    }
}
