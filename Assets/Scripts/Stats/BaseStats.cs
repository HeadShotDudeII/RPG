using UnityEngine;

namespace RPG.Stats
{

    public class BaseStats : MonoBehaviour
    {
        [Range(1, 10)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public int GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }


    }
}
