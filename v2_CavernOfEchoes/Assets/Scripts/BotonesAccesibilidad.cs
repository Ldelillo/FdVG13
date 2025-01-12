using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BotonesAccesibilidad : MonoBehaviour
{
    [SerializeField] private GameObject uncheckEsta;

    [SerializeField] private GameObject checkEsta;

    [SerializeField] private GameObject uncheckSensi;

    [SerializeField] private GameObject checkSensi;
    

    private bool estamina ;
    private bool sensi ;

    // Start is called before the first frame update
    void Start(){
        estamina = false;
        sensi = false;
        checkEsta.SetActive(false);
        uncheckEsta.SetActive(true);
        checkSensi.SetActive(false);
        uncheckSensi.SetActive(true);
    }
        public void Estamina(){
        (FindObjectOfType<HealthStamina>()).estamina = !(FindObjectOfType<HealthStamina>()).estamina;
        uncheckEsta.SetActive(estamina);
        estamina = !estamina;
        checkEsta.SetActive(estamina);

    }   
    public void Sensi(){
        (FindObjectOfType<Player>()).ataque.autoTarget = !(FindObjectOfType<Player>()).ataque.autoTarget;
        uncheckSensi.SetActive(sensi);
        sensi = !sensi;
        checkSensi.SetActive(sensi);

    }   
}
