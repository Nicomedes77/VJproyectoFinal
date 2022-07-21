using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentoController : MonoBehaviour
{
    float timeDestroy = 2f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if ((col.transform.gameObject.tag == "Jugador") &&  Input.GetKey(KeyCode.X))    Destroy(this.gameObject, timeDestroy);     
    }
}
