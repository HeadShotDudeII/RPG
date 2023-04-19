using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField] bool alreayPlayed = false;

        private void OnTriggerEnter(Collider other)
        {

            if (!alreayPlayed && other.gameObject.tag == "Player")
            {
                alreayPlayed = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            return alreayPlayed;
        }

        public void RestoreState(object state)
        {
            alreayPlayed = (bool)state;
        }

    }
}
