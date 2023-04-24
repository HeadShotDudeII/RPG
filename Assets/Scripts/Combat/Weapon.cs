using RPG.Core;
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
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile;


        public void Spawn
            (Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(weaponPrefab, handTransform);
            }


            if (weaponAnimatorOverride != null)
                animator.runtimeAnimatorController = weaponAnimatorOverride;

        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            //projectile is same hand as arrow ie left hand
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);

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