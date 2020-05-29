using UnityEngine;
using Assets.Scripts.Types;

namespace Assets.Scripts
{
    public class PickableItem : MonoBehaviour
    {
        public ItemType KindOf;
        public float Value;

        public void DestroyItem()
        {
            Destroy(this.gameObject);
        }
    }
}
