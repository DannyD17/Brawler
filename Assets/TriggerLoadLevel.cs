using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerLoadLevel : MonoBehaviour {

    public float wait_timer;
    public GameObject canvas;
    Task load_level_task = null;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            canvas.SetActive(true);
            load_level_task = new Task(WaitThenLoad(), true);
        }
    }

    void OnTriggerExit() {
        load_level_task.Stop();
        canvas.SetActive(false);
    }

    IEnumerator WaitThenLoad() {
       
        yield return new WaitForSeconds(wait_timer);
        LoadLevel();
        
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
