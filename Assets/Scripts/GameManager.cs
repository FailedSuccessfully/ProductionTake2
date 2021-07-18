using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static Player player;
    private static Checkpoint activeCP;
    private static GameManager instance;
    private void Awake() {
         if (instance == null)
            instance = this;
        else
            GameObject.Destroy(this);
    }
    void Start()
    {
        if (player==null)
            player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static Vector2 GetPlayerPosition(){
        return player.transform.position;
    }

    public static void HandleEnemyDeath(Enemy enemy){
        //Debug.Log("dead'nt");
        ((PlayerBoost)player.compDict[typeof(PlayerBoost)]).AddBoost(enemy.boostOnKill);
        enemy.Die();
    }

    public static void SetCheckpoint(Checkpoint point){
        if (activeCP != null)
            activeCP.SetActive(false);
        activeCP = point;
        activeCP.SetActive(true);
        Debug.Log($"Active point is: {activeCP.gameObject.name}");
    }

    public static void RespawnPlayer(){
        if (activeCP != null)
            player.transform.position = activeCP.transform.position;
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void DoWin(Winpoint point){
        point.SetMessageActive(true);
        Time.timeScale = 0;
    }
}
