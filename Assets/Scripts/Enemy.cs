using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] GameObject deathFX;
   [SerializeField] GameObject hitVFX;
   [SerializeField] int scorePerHit = 15;
   [SerializeField] int hitPoints = 4;

   ScoreBoard scoreBoard;
   GameObject parentGameObject;

   void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        Rigidbody();
    }

    void Rigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if ( hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints --;
        
    }
}
