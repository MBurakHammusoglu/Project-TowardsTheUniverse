
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip bomb;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

 
    AudioSource audioSource;

    bool isTransitioning=false;
    bool collisionDisabled=false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        NextLevelEditor();
    }
    void NextLevelEditor(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }else if(Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(isTransitioning || collisionDisabled){ return;}
        switch(other.gameObject.tag){
            case "Friendly":
            Debug.Log("Friendly Tag");
            break;
            case "Finish":
            Debug.Log("Finish tag");
            FinishSuccessSeqeuence();
            break;
            default:
            Debug.Log("Carpisma algilanmadi");
            StartCrashSeqeuence();
            break;

        }
        
    }


    void StartCrashSeqeuence(){
        isTransitioning=true;
        //audioSource.Stop();
        audioSource.PlayOneShot(bomb);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }
    void FinishSuccessSeqeuence(){
        isTransitioning = true;
        //audioSource.Stop();
        audioSource.PlayOneShot(success);
        crashParticles.Play();
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
