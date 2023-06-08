using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;

    public MoveMent moveMent { get; private set; }

    public GhostHome home { get; private set; }
    public GhostScatter ghostScatter { get; private set; }

    public GhostChase ghostChase { get; private set; }

    public GhostFrightened ghostFrightened { get; private set; }

    public GhostBehavior initialBehavior;

    public Transform target;
    private void Awake()
    {
        this.moveMent = GetComponent<MoveMent>();
        this.home = GetComponent<GhostHome>();
        this.ghostScatter = GetComponent<GhostScatter>();
        this.ghostChase = GetComponent<GhostChase>();
        this.ghostFrightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.moveMent.ResetState();

        this.ghostFrightened.Disable();
        this.ghostChase.Disable();
        this.ghostScatter.Enable();
        
        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.ghostFrightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
