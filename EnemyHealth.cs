//configurando a decomposiçao do inimigo e pontuaçao
//author: Keven Douglas.
//github: kevendouglas.github.io. 


using UnityEngine;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;            // A quantidade de saude com a qual o inimigo inicia o jogo.
        public int currentHealth;                   // A saude atual do inimigo.
        public float sinkSpeed = 2.5f;              // A velocidade com que o inimigo afunda no chao quando morto.
        public int scoreValue = 10;                 // A quantidade adicionada à pontuação do jogador quando o inimigo morre (CASO SEU JOGO TENHA SCORE).
        public AudioClip deathClip;                 // O som para tocar quando o inimigo morre.


        Animator anim;                              // Referencia ao animador.
        AudioSource enemyAudio;                     // Referencia a de onde veio o audio.
        ParticleSystem hitParticles;                // Uma referencia ao sistema de particulas que eh reproduzido quando o inimigo esta danificado.
        CapsuleCollider capsuleCollider;            // Referencia ao colisor da capsula.

        bool isDead;                                // Se o inimigo esta morto.
        bool isSinking;                             // Se o inimigo começou a afundar no chao.


        void Awake ()
        {
            // Configurando as referencias.
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            // Configurando a saude atual quando o inimigo eh gerado pela primeira vez.
            currentHealth = startingHealth;
        }


        void Update ()
        {
            // Se o inimigo estiver afundando ...
            if(isSinking)
            {
                // ... mova o inimigo para baixo pelo (sinkSpeed) por segundo.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            // Se o inimigo esta morto ...
            if(isDead)
                // ... não eh necessario sofrer danos, entao, saia da funçao.
                return;

            // Reproduzir o efeito de som doido.
            enemyAudio.Play ();

            // Reduzir a saude atual pela quantidade de dano sofrido.
            currentHealth -= amount;
            
            // Defina a posição do sistema de particulas para onde o golpe foi sustentado.
            hitParticles.transform.position = hitPoint;

            //  E jogue as particulas.
            hitParticles.Play();

            // Se a saude atual for menor ou igual a zero...
            if(currentHealth <= 0)
            {
                // ... o inimigo esta morto.
                Death ();
            }
        }


        void Death ()
        {
            // O inimigo esta morto.
            isDead = true;

            // Transforme o colisor em um gatilho para que os tiros possam passar por ele.
            capsuleCollider.isTrigger = true;

            // Diga ao animador que o inimigo esta morto.
            anim.SetTrigger ("Dead");

            // Altere o clipe de audio da fonte de audio para o clipe de morte e jogue-o (isso ira parar a dor clip play).

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Localizar e desativar o agente Nav Mesh.
            GetComponent <NavMeshAgent> ().enabled = false;

            // Encontre o componente do corpo rigido e o torne cinematico(uma vez que usamos o [Translate] para afundar o inimigo).
            GetComponent <Rigidbody> ().isKinematic = true;

            //  O inimigo nao deve afundar.
            isSinking = true;

            // Aumentar a pontuaçao pelo valor da pontuação do inimigo.
            ScoreManager.score += scoreValue;

            // Apos 2 segundos, destroi o inimigo.
            Destroy (gameObject, 2f);
        }
    }
}
