using Project.EmergingObjectsAndPlayer.Scripts;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine;

namespace Project.Indefinite.Scripts
{
    public class ObjectsDisposer : MonoBehaviour
    {
        [SerializeField] private Canon _canon;
        
        [SerializeField] private ObjectsCreator _fireworkCreator;
        [SerializeField] private ObjectsCreator _coinsCreator;
        [SerializeField] private ObjectsCreator _rocketsCreator;

        [SerializeField] private Transform[] _fireworkPoints;
        [SerializeField] private Transform[] _otherPoints;

        [SerializeField]
        private  float _minFireworkDisposeDiff;
        [SerializeField]
        private  float _maxFireworkDisposeDiff;
        [SerializeField]
        private  float _minOtherDisposeDiff;
        [SerializeField]
        private  float _maxOtherDisposeDiff;

        private Coroutine _fireworkDisposing;
        private Coroutine _otherObjDisposing;

        public void Launch()
        {
            _fireworkCreator.Launch();
            _coinsCreator.Launch();
            _rocketsCreator.Launch();
            
            _fireworkDisposing = StartCoroutine(FireworkDisposing());
        }
        
        public void StartDisposing()
        {
            _otherObjDisposing = StartCoroutine(OtherDisposing());
        }

        private void OnDisable()
        {
            if (_fireworkDisposing != null)
                StopCoroutine(_fireworkDisposing);
            
            if (_otherObjDisposing != null)
                StopCoroutine(_otherObjDisposing);
        }

        private IEnumerator FireworkDisposing()
        {
            while (_fireworkCreator != null)
            {
                float randomTimeDiff = Random.Range(_minFireworkDisposeDiff, _maxFireworkDisposeDiff);
                WaitForSeconds timeDiff = new WaitForSeconds(randomTimeDiff);
                int randomPointIndex = Random.Range(0, _fireworkPoints.Length);

                AbstractObject newFirework = _fireworkCreator.GetInactivePrefabClone();
                newFirework.transform.position = _fireworkPoints[randomPointIndex].position;
                newFirework.gameObject.SetActive(true);
                newFirework.GetComponent<Firework>().SetCanon(_canon);
                newFirework.StartMove();

                yield return timeDiff;
            }
        }

        private IEnumerator OtherDisposing()
        {
            while (_rocketsCreator != null || _coinsCreator != null)
            {
                float randomTimeDiff = Random.Range(_minOtherDisposeDiff, _maxOtherDisposeDiff);
                WaitForSeconds timeDiff = new WaitForSeconds(randomTimeDiff);
                int randomPointIndex = Random.Range(0, _otherPoints.Length);
                int coinOrRocket = Random.Range(0, 2);
                AbstractObject newObj;

                if (coinOrRocket == 0)
                {
                    newObj = _rocketsCreator.GetInactivePrefabClone();
                    newObj.GetComponent<EvilRocket>().SetCanon(_canon);
                }
                else
                    newObj = _coinsCreator.GetInactivePrefabClone();

                newObj.transform.position = _otherPoints[randomPointIndex].position;
                newObj.gameObject.SetActive(true);
                newObj.StartMove();

                yield return timeDiff;
            }
        }
    }
}
