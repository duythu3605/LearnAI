using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;

    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.moveMent.SetDirection(-this.ghost.moveMent.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.moveMent.SetDirection(Vector2.up, true);
        this.ghost.moveMent.rigidbody.isKinematic = true;
        this.ghost.moveMent.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed/duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.moveMent.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.moveMent.rigidbody.isKinematic = false;
        this.ghost.moveMent.enabled = true;
    }
}
