using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.ghostChase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.ghostFrightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if(node.availableDirections[index] == -this.ghost.moveMent.direction && node.availableDirections.Count > 1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.moveMent.SetDirection(node.availableDirections[index]);
        }
    }
}
