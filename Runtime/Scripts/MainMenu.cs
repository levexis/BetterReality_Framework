using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterReality.Framework
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            // todo should this always be called? Or should it check the gameman component?
//            TestMan.DevPreload();
        }

        private void Start()
        {
//            Debug.Log("Main menu loaded for" + TestMan.Instance.SubjectName + ". Loading whackamole...");
            SceneManager.LoadScene("Whackamole");
        }
    }
}