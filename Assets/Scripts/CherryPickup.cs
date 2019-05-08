using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPickup : MonoBehaviour {

    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsForCoinPickUp = 100;

    bool addedToScore = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        // will play sound, but not hear it as object is destroyed too quickly 
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.clip = coinPickUpSFX;
        //audio.Play();

        //This does not work
        //AudioSource.Clip = coinPickUpSFX;
        //AudioSource.Play(coinPickUpSFX);



        // First Bug Fix for double counting due to two colliders-----------------------------
        // Assumes only one capsule collider
        //  if (collider is CapsuleCollider2D) {
        //      FindObjectOfType<GameSession> ().AddToScore (pointsForCoinPickUp);
        //      AudioSource.PlayClipAtPoint (coinPickUpSFX, Camera.main.transform.position);
        //      Destroy (gameObject);
        //  }

        // Alternative Bug Fix ----------------------------------------------------------------
        if (!addedToScore)
        {
            addedToScore = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickUp);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }

    }
}
