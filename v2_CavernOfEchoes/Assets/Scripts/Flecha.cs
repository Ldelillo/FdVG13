using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
  [SerializeField] private float velocidad;
  [SerializeField] private int daño;

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
