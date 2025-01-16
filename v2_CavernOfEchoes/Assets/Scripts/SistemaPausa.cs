using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SistemaPausa : MonoBehaviour
{
    [SerializeField] private GameObject MenuPausa;

    [SerializeField] private GameObject BotonPausa;

    [SerializeField] private GameObject GameOver;
    
    private bool pausa;
    private bool estamina = false;

    // Start is called before the first frame update
    void Start(){
        MenuPausa.SetActive(false);
        BotonPausa.SetActive(true);
        GameOver.SetActive(false);
        pausa = false;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (pausa){
                ResumeGame();
            }
            else if(!pausa){
                PauseGame();
            }
        }
    }
    public void PauseGame(){
        MenuPausa.SetActive(true);
        BotonPausa.SetActive(false);
        GameOver.SetActive(false);
        Time.timeScale = 0;
        pausa = true;
    }
    public void ResumeGame(){
        MenuPausa.SetActive(false);
        BotonPausa.SetActive(true);
        GameOver.SetActive(false);
        Time.timeScale = 1;
        pausa = false;
    }
    
    public void RestartGame(){
        MenuPausa.SetActive(false);
        BotonPausa.SetActive(true);
        GameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        pausa = false;
    }

    public void GameOverFun(){
        MenuPausa.SetActive(false); 
        BotonPausa.SetActive(false); 
        GameOver.SetActive(true);
        Time.timeScale = 0.01f;
    }
   
}
