﻿using RPG.Attributes;
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
        [SerializeField] string defaultWeaponName = "Unarmed";



        Weapon currentWeapon = null;
        private float timeSinceLastAttack = 0f;
        Health targetHealth;

        private void Start()
        {

            // Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            EquipWeapon(defaultWeapon);
        }



        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //move to target and to the attack.
            if (targetHealth == null) return;
            if (targetHealth.IsDead()) return;


            if (GetIsOutofRange())
            {
                GetComponent<Mover>().MoveTo(targetHealth.transform.position, attackSpeedFraction);
            }
            else
            {   //once within range isInRange == true will not move only with GetMouseButton(0) holdingdown but not with GetMouseButtonDown(0) clickonce
                AttackBeHaviour();

                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsOutofRange()
        {
            return Vector3.Distance(transform.position, targetHealth.transform.position) > currentWeapon.GetWeaponRange();
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
            transform.LookAt(targetHealth.transform);
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
            if (targetHealth == null) return;
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, targetHealth, gameObject);
            }
            else
            {
                targetHealth.TakeDamage(gameObject, currentWeapon.GetWeaponDamage());
            }


        }

        void Shoot()
        {
            Hit();
        }




        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetHealth = combatTarget.GetComponent<Health>();

        }

        public void Cancel()
        {
            // setting target to null to early will get the player stop fighter action too soon. 
            // fither action will not stop when moving to target

            targetHealth = null;
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


        public Health GetTargetHealth()
        {
            return targetHealth;
        }
    }
}