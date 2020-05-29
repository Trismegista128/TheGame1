using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player playerScript;
        //Movements
        //Health
        //Items/weapons
        //Animation

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Item")
            {
                var item = collision.gameObject.GetComponent<PickableItem>();

                switch (item.KindOf)
                {
                    case Types.ItemType.HealingPotion:
                        playerScript.Heal(item.Value);
                        break;
                    case Types.ItemType.SubmachineGun:
                        playerScript.AddAmmo(item.Value);
                        break;
                }

                item.DestroyItem();
            }
        }
    }
}
