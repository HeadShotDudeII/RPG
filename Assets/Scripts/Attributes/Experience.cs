using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {

        [SerializeField] int experiencePoints = 0;

        public void GainExperience(int experience)
        {
            experiencePoints += experience;
        }

    }
}