using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{   
    public float Velocidade = 20;
    
    // Update is called once per frame
    void FixedUpdate(){
        //Define movimento da bala quando instaciada
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Velocidade * Time.deltaTime);
    }

    //Com o IsTrigger marcado na unity
    void OnTriggerEnter(Collider objetoDeColisao){
        if(objetoDeColisao.tag == "Inimigo"){
            Destroy(objetoDeColisao.gameObject);
        }
        //Destroi a propria bala 
        Destroy(gameObject);
    }
}
