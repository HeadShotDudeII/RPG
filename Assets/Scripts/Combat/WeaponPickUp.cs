using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}