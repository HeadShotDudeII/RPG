using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes

{

    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] int health = 100;

        private bool isDead = false;
        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }
        public bool IsDead()
        {
            return isDead;

        }

        public void TakeDamage(int damage)
        {
            health = Mathf.Max(health - damage, 0);
            //Debug.Log("health is " + health);
            if (health <= 0)
            {
                Die();
            }
        }

        public int GetHealth()
        {
            return health;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            //Debug.Log(this + " dead is " + isDead);
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (int)state;
            if (health <= 0)
            {
                Die();
            }
        }
    }


}
