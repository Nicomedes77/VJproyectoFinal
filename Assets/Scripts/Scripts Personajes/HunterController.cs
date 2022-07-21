using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
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
                anim.SetBool("Agazapado", true);
                LookAt();
                SeguirLerp();
            }
            else    anim.SetBool("Agazapado", false);
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
            anim.SetBool("Derrotado", true);
            ManagerSonido.Sonido_MuerteCazador();
            Destroy(this.gameObject, timeDestroy);
        }
        else    isAlive = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.gameObject.tag == "Jugador")
        {
                if(Input.GetKey(KeyCode.C)) life--;                 
        }
      
    } 
}