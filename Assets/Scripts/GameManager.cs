using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static Player player;
    // Start is called before the first frame update
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
        ((PlayerBoost)player.compDict[typeof(PlayerBoost)]).AddBoost(enemy.boostOnKill);
        GameObject.Destroy(enemy.gameObject);
    }
}
