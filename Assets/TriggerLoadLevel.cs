using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class TriggerLoadLevel : MonoBehaviour {

    public float wait_timer;

    void OnTriggerEnter() {
        StartCoroutine(LoadLevel);
    }

    IEnumerator LoadLevel() {
        yield return new WaitForSeconds(wait_timer);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
