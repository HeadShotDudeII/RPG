using UnityEngine;

namespace RPG.Core
{

    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 100;

        private bool isDead = false;
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

    }


}