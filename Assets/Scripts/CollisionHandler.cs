using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] ParticleSystem crashParticles;
  [SerializeField] float loadDelay = 1f;
  void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerControles>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel",loadDelay);
        
    }

    void ReloadLevel()
    {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);
    }
}
