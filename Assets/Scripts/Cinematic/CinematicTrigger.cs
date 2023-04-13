using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreayPlayed = false;
        private void OnTriggerEnter(Collider other)
        {

            if (!alreayPlayed && other.gameObject.tag == "Player")
            {
                alreayPlayed = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
