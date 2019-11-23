using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHighlight : MonoBehaviour
{
    private GameObject lastHighlighted;
    [SerializeField] private float scale = 1.2f;

    [SerializeField] private Sprite emptySpaceOriginal;
    [SerializeField] private Sprite emptySpaceHighlight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            if(hit.transform.gameObject.tag == "Block" || 
                hit.transform.gameObject.tag == "Wood" ||
                hit.transform.gameObject.tag == "IceBlock")
            {
                // If the last block highlighed is not null and it was an empty block
                if (lastHighlighted != null && lastHighlighted.tag == "emptyBlock")
                {
                    // Reset its scale back to 1 and swap its sprite back to its original
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    lastHighlighted.GetComponent<SpriteRenderer>().sprite = emptySpaceOriginal;
                }
                // If the last block highlighed is not null
                else if (lastHighlighted != null)
                {
                    // Reset its scale back to 1
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                }

                // Set the last highlighted block to be the current block hit by the raycast
                lastHighlighted = hit.transform.gameObject;
                // Scale it up by the scale amount to make it noticable
                hit.transform.localScale = new Vector3(scale,scale,scale);
                // Scales the box collider down a little bit so the object looks bigger but the spriggan doesn't collide with the larger block
                hit.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.83f,0.83f);
            }
            else if(hit.transform.gameObject.tag == "emptyBlock")
            {
                // If the last block highlighed is not null and it was an empty block
                if (lastHighlighted != null && lastHighlighted.tag == "emptyBlock")
                {
                    // Reset its scale back to 1 and swap its sprite back to its original
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    lastHighlighted.GetComponent<SpriteRenderer>().sprite = emptySpaceOriginal;
                }
                // If the last block highlighed is not null
                else if (lastHighlighted != null)
                {
                    // Reset its scale back to 1
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                }

                // Set the last highlighted block to be the current block hit by the raycast
                lastHighlighted = hit.transform.gameObject;
                // Change the emptyblocks sprite to one that has corners so the player can actually see the empty space
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = emptySpaceHighlight;
            }
            else
            {
                // If the last block highlighed is not null and it was an empty block
                if (lastHighlighted != null && lastHighlighted.tag == "emptyBlock")
                {
                    // Reset its scale back to 1 and swap its sprite back to its original
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    lastHighlighted.GetComponent<SpriteRenderer>().sprite = emptySpaceOriginal;
                }
                // If the last block highlighed is not null
                else if (lastHighlighted != null)
                {
                    // Reset its scale back to 1
                    lastHighlighted.transform.localScale = new Vector3(1, 1, 1);
                    lastHighlighted.transform.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                }
            }
        }
    }
}
