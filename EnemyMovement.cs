//configurando a movimentaçao do inimigo
//author: Keven Douglas.
//github: kevendouglas.github.io. 


using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        Transform player;               // Referencia a posição do jogador.
        PlayerHealth playerHealth;      // Referencia a saude do jogador.
        EnemyHealth enemyHealth;        // Referencia a saude do inimigo.
        NavMeshAgent nav;               // Referencia ao agente (nav) 


        void Awake ()
        {
            //  Configura as referencias.
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <NavMeshAgent> ();
        }


        void Update ()
        {
            // Se o inimigo e o jogador estao em saude ...
            if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                // ... configure o alvo do agente de malha para o jogador.
                nav.SetDestination (player.position);
            }
            // Se nao...
            else
            {
                // ... desativar o agente(nav)
                nav.enabled = false;
            }
        }
    }
}