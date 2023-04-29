using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField]TextMeshProUGUI TextBox;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Start()
        {
            UpdateHealthText();

            //Debug.Log("Health % is " + health.GetPercentage());

        }


        void Update()
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            TextBox.text = "Health:" + Mathf.RoundToInt(health.GetPercentage()) + "%";
            Debug.Log("Health is " + health.GetPercentage());

        }

    }
}