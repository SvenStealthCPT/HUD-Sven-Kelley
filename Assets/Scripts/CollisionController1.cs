using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    public bool changeColor;
    public Color myColor;

    public bool destroyEnemy;
    public bool destroyCollectibles;

    public AudioClip collisionAudio;
    private AudioSource audioSource;
    
    public float pushPower;
    
    public TMPro.TMP_Text scoreUI;
    public int increaseScore = 1;
    public int decreaseScore = 1;
    private int score = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (scoreUI != null)
        {
            scoreUI.text = score.ToString();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (changeColor == true)
        {
            gameObject.GetComponent<Renderer>().material.color = myColor;
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collisionAudio, 0.5F);
        }
        
        if (destroyEnemy == true && other.gameObject.tag == "Enemy" || destroyCollectibles == true && other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
        }
        if (scoreUI != null && other.gameObject.tag == "Collectible")
        {
            score += increaseScore;
        }
        if (scoreUI != null && other.gameObject.tag == "Enemy")
        {
            score -= decreaseScore;
        }
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }
        
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        if (hit.gameObject.tag == "Object")
        {
            body.velocity = pushDir * pushPower;
        }
        
        if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collisionAudio,
                                    0.5F);
        }

        if ((destroyEnemy == true && hit.gameObject.tag == "Enemy") || (destroyCollectibles == true && hit.gameObject.tag == "Collectible"))
        {
            Destroy(hit.gameObject);
        }
        if (scoreUI != null && hit.gameObject.tag == "Collectible")
        {
            score += increaseScore;
        }
        if (scoreUI != null && hit.gameObject.tag == "Enemy")
        {
            score -= decreaseScore;
        }
    }
}