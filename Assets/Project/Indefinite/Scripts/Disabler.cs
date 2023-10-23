using Project.EmergingObjectsAndPlayer.Scripts;
using UnityEngine;

namespace Project.Indefinite.Scripts
{
    public class Disabler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.TryGetComponent(out AbstractObject abstractObject))
                abstractObject.gameObject.SetActive(false);
        }
    }
}
