using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winpoint : MonoBehaviour
{
    Canvas c;
    private void Start() {
        c = gameObject.GetComponentInChildren<Canvas>();
        c.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            GameManager.DoWin(this);
    }

    public void SetMessageActive(bool isEnable){
        c.gameObject.SetActive(isEnable);
    }

}
