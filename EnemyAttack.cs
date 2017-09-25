//configurando o inimigo para atacar automaticamente.
//author: Keven Douglas.
//github: kevendouglas.github.io. 

using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // O tempo em segundos entre cada ataque.
        public int attackDamage = 10;               // A quantidade de saúde removida por ataque.


        Animator anim;                              // Referencia ao componente animador.
        GameObject player;                          // Referencia ao GameObject do jogador.
        PlayerHealth playerHealth;                  // Referencia a saude do jogador.
        EnemyHealth enemyHealth;                  	// Referencia à saúde deste inimigo.
        bool playerInRange;                        	// Se o jogador esta dentro do colisor do gatilho e pode ser atacado.
        float timer;                                // Temporizador para contar ate o proximo ataque.


        void Awake ()
        {
             	// Configurando as referencias.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
        }


        void OnTriggerEnter (Collider other)
        {
             	// Se o colisor de entrada eh o jogador ...
            if(other.gameObject == player)
            {
            	// ... o jogador esta no alcance.
                playerInRange = true;
            }
        }


        void OnTriggerExit (Collider other)
        {
                 // Se o colisor existente eh o jogador ...
            if(other.gameObject == player)
            {
                // ... o jogador não esta mais no alcance.
                playerInRange = false;
            }
        }


        void Update ()
        {
            	// Adicione a hora desde a ultima vez que o ultimo foi programado para o temporizador.
            timer += Time.deltaTime;

           		// Se o temporizador exceder o tempo entre ataques, o jogador esta no alcance e este inimigo esta vivo ...           
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                // ... ataque
                Attack ();
            }

            	// Se o jogador tiver zero ou menos saude ...
            if(playerHealth.currentHealth <= 0)
            {
                // ... diga ao animador que o jogador esta morto.
                anim.SetTrigger ("PlayerDead");
            }
        }


        void Attack ()
        {
            	// Reset temporizador.
            timer = 0f;

            	// Se o jogador tem sau	de a perder ...
            if(playerHealth.currentHealth > 0)
            {
                // ... ferir o jogador.
                playerHealth.TakeDamage (attackDamage);
            }
        }
    }
}