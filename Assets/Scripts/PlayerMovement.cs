using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    public float turnspeed = 20f;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set( horizontal , 0f , vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal , 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical , 0f);

        bool IsWalking = hasHorizontalInput || hasVerticalInput ;

        m_Animator.SetBool("IsWalking",IsWalking);

        if (IsWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
            
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredforward = Vector3.RotateTowards (transform.forward , m_Movement , turnspeed*Time.deltaTime , 0f  );
        m_Rotation = Quaternion.LookRotation (desiredforward);
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition ( m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude ) ;
        m_Rigidbody.MoveRotation (m_Rotation);
    }

}
