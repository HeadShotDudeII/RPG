using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Combat
{

    //this class is for attacking
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttack = 1f;

        [Range(0.7f, 1)]
        [SerializeField] float attackSpeedFraction = 1f;
        [SerializeField] Weapon defaultWeapon = null;

        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;



        Weapon currentWeapon = null;
        private float timeSinceLastAttack = 0f;
        Health target;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }



        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //move to target and to the attack.
            if (target == null) return;
            if (target.IsDead()) return;


            if (GetIsOutofRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, attackSpeedFraction);
            }
            else
            {   //once within range isInRange == true will not move only with GetMouseButton(0) holdingdown but not with GetMouseButtonDown(0) clickonce
                AttackBeHaviour();

                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsOutofRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) > currentWeapon.GetWeaponRange();
        }


        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();

            //Debug.Log("targetToTest is " + targetToTest + " LiveState is " + !targetToTest.IsDead());

            return targetToTest != null && !targetToTest.IsDead();

        }

        private void AttackBeHaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //this will trigger the Hit() event
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;

            }
        }

        void Hit()
        {
            if (target == null) return;
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
            }


        }

        void Shoot()
        {
            Hit();
        }




        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public void Cancel()
        {
            // setting target to null to early will get the player stop fighter action too soon. 
            // fither action will not stop when moving to target

            target = null;
            GetComponent<Mover>().Cancel();
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");


        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            if (animator == null) { Debug.Log("animator is null"); }
            currentWeapon.Spawn(rightHandTransform, leftHandTransform, animator);

        }
    }
}