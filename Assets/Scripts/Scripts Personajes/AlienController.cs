using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    bool isAlive = true;
    public int life = 3;
    public PlayerController player;
    public float distancia = 6;
    public float enemySpeed = 1f;
    float timeDestroy = 5f;
    public Animator anim;

    void Start()
    {

    }

    void Update()
    {
        DistanceToPlayer();
        DeathCondition(life);        
    }

    void DistanceToPlayer()
    {
        if (isAlive == true)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            
            if(dist < distancia)  
            {
                anim.SetBool("Asustando", true);
                LookAt();
                SeguirLerp();
            }
            else    anim.SetBool("Asustando", false);
        }
    }

    void LookAt()
    {
        transform.LookAt(player.transform.position);
    }

    void SeguirLerp()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position , enemySpeed * Time.deltaTime);
    }

    void DeathCondition(int life)
    {

        if(life <= 0)
        {
            isAlive = false;
            anim.SetBool("Muriendo", true);
            ManagerSonido.Sonido_MuerteMarciano();
            Destroy(this.gameObject, timeDestroy);
        }
        else    isAlive = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.gameObject.tag == "Jugador")
        {
                if(Input.GetKey(KeyCode.C))
                {
                    life--;
                }
                DeathCondition(life);                               
        }      
    } 

        void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.tag == "Jugador")  anim.SetBool("Asustando", false);
    }
}