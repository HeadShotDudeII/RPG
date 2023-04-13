using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    //this class is for attacking
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttack = 1f;
        [SerializeField] int damage = 10;
        [Range(0.7f, 1)]
        [SerializeField] float attackSpeedFraction = 1f;
        private float timeSinceLastAttack = 0f;


        Health target;

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
            return Vector3.Distance(transform.position, target.transform.position) > weaponRange;
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
            target.TakeDamage(damage);


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
    }
}