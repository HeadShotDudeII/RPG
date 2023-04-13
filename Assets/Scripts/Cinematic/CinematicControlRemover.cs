using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
        player = GameObject.FindWithTag("Player");



    }

    void DisableControl(PlayableDirector playableDirector)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Disalbe Control");
    }


    void EnableControl(PlayableDirector playableDirector)
    {
        player.GetComponent<PlayerController>().enabled = true;
        Debug.Log("Enalbe Control");


    }


}
