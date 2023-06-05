using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    public PacMan PacMan;
    public Transform pellets;

    public int ghostMutilPlayer { get; private set; } = 1;
    public int Score { get; private set; }

    public int Lives { get; private set; }

    private void Start()
    {
        SetScore(0);
        SetLives(3);
        NewGame();
    }
    private void Update()
    {
        if (this.Lives < 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
    private void NewGame()
    {

    }
    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMutiplayer();

        for (int i = 0; i < ghost.Length; i++)
        {
            this.ghost[i].gameObject.SetActive(true);
        }
        this.PacMan.gameObject.SetActive(true);
    }

    private void GameOVer()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            this.ghost[i].gameObject.SetActive(false);
        }
        this.PacMan.gameObject.SetActive(false);
    }
    private void SetScore(int score)
    {
        this.Score = score;
    }
    private void SetLives(int lives)
    {
        this.Lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMutilPlayer;
        SetScore(this.Score + points);

        this.ghostMutilPlayer++;
    }

    public void PacmanEaten(PacMan pacMan)
    {
        this.PacMan.gameObject.SetActive(false);

        SetLives(this.Lives - 1);

        if(this.Lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOVer();
        }
    }

    public void PelletEaten(Pellet _pellet)
    {
        _pellet.gameObject.SetActive(false);

        SetScore(this.Score + _pellet.points);

        if (!HasRemainingPellets())
        {
            this.PacMan.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPellet _powerpellet)
    {
        PelletEaten(_powerpellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMutiplayer), _powerpellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMutiplayer()
    {
        this.ghostMutilPlayer = 1;
    }
}
