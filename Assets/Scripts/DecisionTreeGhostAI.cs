using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DecisionTreeGhostAI : MonoBehaviour
{
    

    public enum ActionState
    {
        InHome = 0,
        Scatter = 1,
        Chassing = 2,
        Frightened = 3,
        Dead = 4
    }
    public ActionState currentState;

    public bool findPacMan = false;

    public bool GhostDead = false;

    public bool PacManEatPower = false;


    private void Start()
    {
        GetActionGhost(1);
    }

    private void Update()
    {
        if(currentState == ActionState.Scatter)
        {
            Debug.Log("Find new Position and go");
        }
        if (currentState == ActionState.Chassing)
        {
            Debug.Log("Find Position PacMan and chasing");
        }
        if (PacManEatPower)
        {
            Debug.Log("PacMan has power!");
            GetActionGhost(3);
            if (!GhostDead) return;
            if (GhostDead)
            {
                GetActionGhost(4);
            }
        }
        if (findPacMan)
        {
            ResetState();
        }
    }
    public void GetActionGhost(int index)
    {        
        switch (index)
        {
            case 0:
                currentState = ActionState.InHome;
                Home();
                break;
            case 1:
                currentState = ActionState.Scatter;
                Scatter();
                break;
            case 2:
                currentState = ActionState.Chassing;
                Chasing();
                break;
            case 3:
                currentState = ActionState.Frightened;
                Frightened();
                break;
            case 4:
                currentState = ActionState.Dead;
                Debug.Log("Ghost Dead!");
                Dead();
                break;
            default:
                break;
        }
    }

    private void ResetState()
    {        
        GetActionGhost(0);  
    }

    private void Home()
    {
        Debug.Log("Home");
        StartCoroutine(TransHome(2));
    }
    private void Scatter()
    {
        Debug.Log("Scatter");

        StartCoroutine(TransScatter(7));
    }
    private void Chasing()
    {
        Debug.Log("Chasing");

        StartCoroutine(TransChaing(20));
    }
    private void Frightened()
    {
        Debug.Log("Frightened");

        StartCoroutine(TransFrightened(8));
    }

    private void Dead()
    {
        Debug.Log("Dead");

        StartCoroutine(Dead(20));
    }

    private IEnumerator Dead(float duration)
    {
        Debug.Log("Wait in home");

        yield return new WaitForSeconds(20);
        PacManEatPower = false;
        GhostDead = false;
        ResetState();
    }

    private IEnumerator TransScatter(float duration)
    {
        Debug.Log("Go Any Where");
        yield return new WaitForSeconds(duration);
        GhostDead = false;
        GetActionGhost(2);
    }

    private IEnumerator TransChaing(float duration)
    {
        Debug.Log("Go Find PacMan!");    
        yield return new WaitForSeconds(duration);
        GhostDead = false;
        if (!findPacMan && !PacManEatPower)
        {
            GetActionGhost(1);
        }

        if (PacManEatPower)
        {
            GetActionGhost(3);
        }
        if (findPacMan)
        {
            Debug.Log("Catched PacMan");
            ResetState();
        }

    }

    private IEnumerator TransFrightened(float duration)
    {
        Debug.Log("Start Frighten");
        
        yield return new WaitForSeconds(duration);
        GhostDead = false;
        PacManEatPower = false;
        GetActionGhost(2);
        
    }

    private IEnumerator TransHome(float duration)
    {
        yield return new WaitForSeconds(duration);
        findPacMan = false;
        GhostDead = false;
        GetActionGhost(1);
    }
}
