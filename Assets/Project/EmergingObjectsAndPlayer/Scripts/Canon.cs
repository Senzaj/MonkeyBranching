using System;
using System.Collections;
using UnityEngine;

namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Canon : MonoBehaviour
    {
        [SerializeField] private DistanceJoint2D _joint2D;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Transform _lineStartPoint;
        [SerializeField] private Rigidbody2D _currentTarget;

        public event Action Moved;
        public event Action TouchedEvilRocket;
        
        private const float MaxDistance = 5;
        private const float MinDistance = 1;
        private const float DistanceDelta = 1.4f;
        private const float DistanceDeltaMultiplier = 1.8f;
        private const float ShootTime = 0.13f;
        private const float UnhookTime = 0.13f;

        private Coroutine _hooking;
        private bool _isHooked;
        private bool _canHook;
        private bool _isFirstMove;

        private void Start()
        {
            _canHook = true;
            _isFirstMove = true;
            _line.SetPosition(0, _lineStartPoint.position);
            _line.SetPosition(1, _lineStartPoint.position);
            _isHooked = true;
            _hooking = StartCoroutine(Hooking());
        }

        private void Update()
        {
            _line.SetPosition(0, _lineStartPoint.position);

            if (_hooking == null)
                _line.SetPosition(1, _lineStartPoint.position);
        }

        public void DisableHook() => _canHook = false;
        
        public void EnableHook() => _canHook = true;
        
        public void TryHook(Rigidbody2D _targetRB)
        {
            if (_canHook == false)
                return;

            if (_targetRB.gameObject.TryGetComponent(out EvilRocket rocket))
            {
                //
                
                rocket.OnExploded();
                TouchedEvilRocket?.Invoke();
                return;
            }
            
            if ( _targetRB != _currentTarget)
            {
                //

                if (_isFirstMove)
                {
                    _isFirstMove = false;
                    Moved?.Invoke();
                }
                
                _currentTarget = _targetRB;
                
                if (_isHooked)
                {
                    _hooking = StartCoroutine(UnhookingBeforeHook());
                }
                else
                {
                    if (_hooking != null)
                        StopCoroutine(_hooking);
                
                    _hooking = StartCoroutine(Shooting());
                }
            }
            else
            {
                //
                
                if (_isFirstMove)
                {
                    _isFirstMove = false;
                    Moved?.Invoke();
                }
                
                if (_hooking != null)
                    StopCoroutine(_hooking);
                
                _hooking = StartCoroutine(Unhooking());
            }
        }

        private IEnumerator Hooking()
        {
            while (_isHooked)
            {
                /*
                Vector2 targetRotation = _currentTarget.transform.position - transform.position;
                transform.right = Vector2.MoveTowards(transform.right, targetRotation,
                    DistanceDelta *
                    Time.deltaTime);
                    */
                
                _line.SetPosition(0, _lineStartPoint.position);
                _line.SetPosition(1, _currentTarget.transform.position);

                if (_joint2D.distance > MaxDistance)
                {
                    _joint2D.distance = Mathf.MoveTowards(_joint2D.distance, MaxDistance,
                        DistanceDelta * DistanceDeltaMultiplier * Time.deltaTime);
                }
                else
                {
                    _joint2D.distance = Mathf.Lerp(_joint2D.distance, MinDistance, DistanceDelta * Time.deltaTime);
                }

                yield return null;
            }
        }

        private IEnumerator Shooting()
        {
            float currentHookingTime = 0;

            while (_isHooked == false)
            {
                Vector2 newTargetPos =
                    Vector2.MoveTowards(_line.GetPosition(1), _currentTarget.transform.position,
                        40 * Time.deltaTime);

                _line.SetPosition(1, newTargetPos);

                currentHookingTime += Time.deltaTime;

                if (currentHookingTime > ShootTime)
                {
                    _joint2D.enabled = true;
                    _joint2D.connectedBody = _currentTarget;
                    _joint2D.distance = Vector2.Distance(transform.position, _currentTarget.transform.position);
                    _isHooked = true;

                    if (_hooking != null)
                        StopCoroutine(_hooking);

                    _hooking = StartCoroutine(Hooking());
                }

                yield return null;
            }
        }

        private IEnumerator Unhooking()
        {
            _isHooked = false;
            _joint2D.enabled = false;
            _joint2D.connectedBody = null;
            _currentTarget = null;

            float currentUnhookingTime = 0;

            while (_isHooked == false)
            {
                Vector2 newTargetPos =
                    Vector2.MoveTowards(_line.GetPosition(1), _lineStartPoint.transform.position,
                        30 * Time.deltaTime);

                _line.SetPosition(1, newTargetPos);

                currentUnhookingTime += Time.deltaTime;

                if (currentUnhookingTime > UnhookTime)
                {
                    if (_hooking != null)
                        StopCoroutine(_hooking);

                    _hooking = null;
                    break;
                }

                yield return null;
            }
        }
        
        private IEnumerator UnhookingBeforeHook()
        {
            _isHooked = false;
            _joint2D.enabled = false;
            
            float currentUnhookingTime = 0;

            while (_isHooked == false)
            {
                Vector2 newTargetPos =
                    Vector2.MoveTowards(_line.GetPosition(1), _lineStartPoint.transform.position,
                        30 * Time.deltaTime);

                _line.SetPosition(1, newTargetPos);

                currentUnhookingTime += Time.deltaTime;

                if (currentUnhookingTime > UnhookTime)
                {
                    if (_hooking != null)
                        StopCoroutine(_hooking);

                    _hooking = StartCoroutine(Shooting());
                }

                yield return null;
            }
        }
    }
}
