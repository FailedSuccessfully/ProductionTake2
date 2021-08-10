using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static Player player;
    private static GameManager instance;
    public static GameManager Instance {
        get {return instance;}
    }
    private static Checkpoint activeCP;
    private static Vector3 acpPos;
    private static float acpTime;
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
        if (activeCP != null){
            GameManager.AnimateACP();
        }
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
        acpPos = point.transform.position;
        acpTime = Time.time;
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

    private static void AnimateACP(){
        int range = 4;
        float a = range * (Mathf.Sin(Time.time - acpTime));
        Vector3 pos = new Vector3{
            x = acpPos.x,
            y =  a + acpPos.y,
            z = acpPos.z
        };
        //Debug.Log(a);

        activeCP.transform.position = pos;
    }
}
