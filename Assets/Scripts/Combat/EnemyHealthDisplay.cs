using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if (fighter.GetTargetHealth() == null)
            {
                GetComponent<TextMeshProUGUI>().text = "Enemy Health: N/A";
                return;
            }
            Health health = fighter.GetTargetHealth();
            GetComponent<TextMeshProUGUI>().text = "Enemy Health:" + Mathf.RoundToInt(health.GetPercentage()) +"%";

        }
    }
}
