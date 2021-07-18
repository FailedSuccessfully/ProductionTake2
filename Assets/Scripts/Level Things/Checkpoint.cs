using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public bool isActive{get; private set;}

    private void Start() {
        anim = GetComponent<Animator>();
        SetActive(false);
    }

    public void SetActive(bool active){
        this.isActive = active;
        anim.SetBool("active", active);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            GameManager.SetCheckpoint(this);
        }
    }
}
