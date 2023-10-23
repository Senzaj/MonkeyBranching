using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Project.EmergingObjectsAndPlayer.Scripts;

namespace Project.Indefinite.Scripts
{
    public class ObjectsCreator : MonoBehaviour
    {
        [SerializeField] private AbstractObject _prefab;

        private List<AbstractObject> _prefabClones;

        public void Launch()
        {
            _prefabClones = new List<AbstractObject>();
            ClonePrefab();
        }

        public AbstractObject GetInactivePrefabClone()
        {
            AbstractObject prefabClone = _prefabClones.FirstOrDefault(obj => obj.gameObject.activeSelf == false);

            if (prefabClone == null)
                prefabClone = ClonePrefab();

            return prefabClone;
        }

        private AbstractObject ClonePrefab()
        {
            AbstractObject prefabClone = Instantiate(_prefab, transform);
            prefabClone.gameObject.SetActive(false);
            _prefabClones.Add(prefabClone);
            return prefabClone;
        }
    }
}
