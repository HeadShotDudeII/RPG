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
        private void Awake()
        {
            //Change to Awake HealthDisplay can execute before Health
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
            //Debug.Log(gameObject.name + "Start health is " + health);
        }
        public bool IsDead()
        {
            return isDead;

        }

        public void TakeDamage(GameObject instigator, int damage)
        {
            health = Mathf.Max(health - damage, 0);
            //Debug.Log("health is " + health);
            if (health <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;


            //experience.GainExperience tells who get the experience
            //GetComponent<BaseStats>().GetExperienceReward()  how much exp is rewarded.
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));

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

        public float GetPercentage()
        {
            //Debug.Log("Health is " + health);
            //Debug.Log("BaseStatsHealth is " + GetComponent<BaseStats>().GetHealth());

            //return 10000*(health/ GetComponent<BaseStats>().GetHealth());
            //return health;
            return 100 * ((float)health / GetComponent<BaseStats>().GetStat(Stat.Health));

        }
    }


}
