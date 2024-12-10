using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
  [SerializeField] public float velocidad = 20;
  [SerializeField] public int daño = 10;


  private void Start()
    {
        // Destruir la flecha después de 5 segundos
        Destroy(gameObject, 2f);
    }

private void Update(){
    transform.Translate(Vector2.right * velocidad * Time.deltaTime);

}
private void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Enemigo")){
       other.GetComponent<Damageable>().TakeDamage(daño);
       Destroy(gameObject);
    }
}
}
