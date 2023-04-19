using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponAnimatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] int weaponDamage = 10;
        [SerializeField] float weaponRange = 2f;


        public void Spawn
            (Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
                Debug.Log("Weapon Equiped");
            }


            if (weaponAnimatorOverride != null)
                animator.runtimeAnimatorController = weaponAnimatorOverride;

        }

        public int GetWeaponDamage()
        {
            return weaponDamage;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }

}