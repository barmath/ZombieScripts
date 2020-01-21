using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{
    public GameObject Jogador;
    public float Velocidade = 5;

    void Start(){
        Jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    void Update(){
        
    }

    void FixedUpdate(){

        //Calculo da distacia do inimigo para o jogador
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        //Define coordenada onde o jogador esta
        Vector3 direcao = Jogador.transform.position - transform.position;

        //Vira inimigo para a direcao do jogador 
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        GetComponent<Rigidbody>().MoveRotation(novaRotacao);

        if(distancia > 2.5){

           //move-se na direcao do jogador 
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + direcao.normalized * Velocidade * Time.deltaTime);
            GetComponent<Animator>().SetBool("Atacando", false);

        }else{

            //Ativa animação de ataque
            GetComponent<Animator>().SetBool("Atacando", true);
        }

        
        
    }
    //Idicar quando zombie ataca jogador 
    void AtacaJogador(){
        Time.timeScale = 0;
        Jogador.GetComponent<ControleJogador>().TextoGameOver.SetActive(true);
        Jogador.GetComponent<ControleJogador>().Vivo = false;
    }
}
