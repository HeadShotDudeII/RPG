using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }


        [SerializeField] DestinationIdentifier destination;
        [SerializeField] int sceneNumberToLoad = 1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            if (sceneNumberToLoad < 0)
            {
                Debug.Log("Forgot to set sceneNumber to load");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneNumberToLoad);

            savingWrapper.Load();


            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            //savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;


            player.GetComponent<NavMeshAgent>().enabled = true;


        }

        private Portal GetOtherPortal()
        {
            foreach (Portal otherPortal in FindObjectsOfType<Portal>())
            {
                if (otherPortal == this) continue;

                // need to match destination
                if (otherPortal.destination != destination) continue;
                return otherPortal;
            }
            return null;

        }
    }

}

