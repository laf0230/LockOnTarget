using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterectionArea : MonoBehaviour
{
    public IInterectionable interectableObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<IInterectionable>(out interectableObject);
        if(interectableObject != null)
            interectableObject.Highlight(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(interectableObject != null)
            interectableObject.Highlight(false);
        interectableObject = null;
    }
}
