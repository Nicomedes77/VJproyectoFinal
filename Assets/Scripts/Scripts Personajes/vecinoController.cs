using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vecinoController : MonoBehaviour
{
    public PlayerController player;
    public float distancia = 6;
    public Animator anim;
    bool yaLoVisito = false;

    void Start()
    {

    }

    void Update()
    {
        DistanceToPlayer();      
    }

    void DistanceToPlayer()
    {
            float dist = Vector3.Distance(transform.position, player.transform.position);            
            if(dist < distancia)  
            {
                LookAt();
                //SeguirLerp();
            }

    }

    void LookAt()
    {
        transform.LookAt(player.transform.position);
    }

    void OnTriggerStay(Collider col)
    {
        if ((col.transform.gameObject.tag == "Jugador") &&  
                                    yaLoVisito == false &&
                                   Input.GetKey(KeyCode.V))
        {
                //audioSource.PlayOneShot(damageSFX[Random.Range(0, damageSFX.Length)]);
            anim.SetBool("Saludando", true);
            yaLoVisito = true;                        
        }
        //else anim.SetBool("Saludando", false);     
    } 

    void OnTriggerExit(Collider col)
    {
        anim.SetBool("Saludando", false);     
    }    
}
