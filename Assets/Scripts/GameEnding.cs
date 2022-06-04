using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeduration = 1f;
    public float displayImageDuration = 1f;
    public GameObject Player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer ;
    bool m_HasAudioPlayed;
    

    void Endlevel ( CanvasGroup imageCanvasGroup , bool doRestart , AudioSource audioSource )
    {
        
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true ;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeduration ;
        
        if ( m_Timer > fadeduration + displayImageDuration )
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == Player)
        {
            m_IsPlayerAtExit = true ;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true ;
    }
    
    void Update()// Update is called once per frame
    {
        if (m_IsPlayerAtExit)
        {
            Endlevel( exitBackgroundImageCanvasGroup , false , exitAudio );
        }
        else if (m_IsPlayerCaught)
        {
            Endlevel( caughtBackgroundImageCanvasGroup , true , caughtAudio );
        }

    }
}


