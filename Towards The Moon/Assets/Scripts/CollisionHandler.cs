
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=1f;
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Friendly":
            Debug.Log("Friendly Tag");
            break;
            case "Finish":
            Debug.Log("Finish tag");
            FinishSuccessSeqeuence();
            break;
            default:
            Debug.Log("Çarpışma algılanmadı");
            StartCrashSeqeuence();
            break;

        }
        
    }
    void StartCrashSeqeuence(){
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }
    void FinishSuccessSeqeuence(){
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1 ;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

}
