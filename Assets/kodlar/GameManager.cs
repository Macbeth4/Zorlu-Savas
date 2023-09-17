using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

 public enum GameStates { countdown, running, raceover };
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    GameStates gamestate = GameStates.countdown;
    private void Awake() {
        if (instance == null){instance = this;}
        else if(instance!= this){ Destroy(gameObject); return;}

        DontDestroyOnLoad(gameObject);
    }

    void levelStart(){
        gamestate = GameStates.countdown;
    }
    
    public GameStates GetGameStates(){
        return gamestate;
    }
    public void OnRaceStart(){
      gamestate = GameStates.running;  
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        levelStart();
    }


}
