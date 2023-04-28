using RPG.Attributes;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        // responsible for doing raycasting

        void Update()
        {

            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;


                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;



                if (Input.GetMouseButton(0))
                {


                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                target = null;

                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {


            RaycastHit raycastHit;
            if (Physics.Raycast(GetMouseRay(), out raycastHit))
            {
                // GetMouseButton will return true as long as mouse button held down, there will be multiple logs.
                if (Input.GetMouseButton(0))
                //if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(raycastHit.point, 1f);
                }

                return true;
            }

            return false;


        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

