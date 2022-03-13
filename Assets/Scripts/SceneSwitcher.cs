using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Inctance;

    private void Start() => Inctance = this;
    public void SwitchScene(string sceneName) => SceneManager.LoadScene(sceneName);
    
}
