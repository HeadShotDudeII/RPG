using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{


    public class SavingWrapper : MonoBehaviour
    {

        const string defaultSaveFile = "save";
        [SerializeField] float fadInTime = 0.25f;

        //IEnumerator Start()
        //{
        //    Fader fader = FindObjectOfType<Fader>();
        //    fader.FadeOutImmediate();
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        //    yield return fader.FadeIn(fadInTime);
        //}




        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }



        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);

        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}