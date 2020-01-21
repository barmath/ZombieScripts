using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    public float Velocidade = 10;
    // Update is called once per frame
    Vector3 direcao;
    //Limita raio para pegar no chao
    public LayerMask MascaraChao;

    public GameObject TextoGameOver;
    public bool Vivo = true;

    private void Start(){
        Time.timeScale = 1;
    }

    void Update(){
        
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if(direcao != Vector3.zero){
            GetComponent<Animator>().SetBool("Movendo", true);
        }else{
            GetComponent<Animator>().SetBool("Movendo", false);
        }

        //Reiniciar jogo
        if(Vivo == false){
            if(Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate(){

        //move personagem 
        GetComponent<Rigidbody>().MovePosition( GetComponent<Rigidbody>().position + (this.direcao * Velocidade * Time.deltaTime));

        //cria raio para pegar mouse
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        //desenha raio
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //armazena ponto de impacto 
        RaycastHit impacto;

        //checa se houve contato com o chao
        if(Physics.Raycast(raio, out impacto, 100)){
            
            //guarda posicao no Vector3
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            //Normaliza para jodor e ponto de impacto estarem na mesma posicao
            posicaoMiraJogador.y = transform.position.y;

            //Calcula uma rotaçao para a posição dada
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            //Atribui rotação para o jogador virar
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
