using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform Player ; 
    public GameEnding gameEnding;
    
    bool m_IsPlayerInRange ;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == Player)
        {
            m_IsPlayerInRange = true ;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == Player)
        {
            m_IsPlayerInRange = false ;
        }
    }
    
    void Update()// Update is called once per frame
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = Player.position - transform.position + Vector3.up;
            Ray ray = new Ray ( transform.position , direction );
            RaycastHit raycastHit;
            if (Physics.Raycast( ray , out raycastHit))
            {
                if (raycastHit.collider.transform == Player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
