using UnityEngine;
using UnityEngine.SceneManagement;

namespace SortItems
{
    public class SceneChanger : MonoBehaviour
    {
        private void Start()
        {
            var level = PlayerPrefs.GetInt("Level", 0);
            var idx = SceneManager.GetActiveScene().buildIndex;
            if (level != idx)
            {
                LoadLevel(level);
            }
        }

        public void LoadLevel(int levelIdx)
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var nextLevel = (levelIdx) % sceneCount;
            SceneManager.LoadScene(nextLevel);
        }


        public void LoadNextLevel()
        {
            var idx = SceneManager.GetActiveScene().buildIndex;
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var nextLevel = (idx + 1) % sceneCount;
            PlayerPrefs.SetInt("Level", nextLevel);
            SceneManager.LoadScene(nextLevel);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                ReloadScene();
            }    
        }
    }
}
