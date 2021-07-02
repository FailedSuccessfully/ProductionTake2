using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive{get; private set;}

    public void SetActive(bool active){
        this.isActive = active;
        GetComponent<SpriteRenderer>().color = isActive ? Color.green : Color.red;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            GameManager.SetCheckpoint(this);
        }
    }
}
