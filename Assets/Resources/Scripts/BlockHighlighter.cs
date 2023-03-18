//https://forum.unity.com/threads/raycast-fps-highlighting-object-on-the-cursor.488609/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
 
public class BlockHighlighter : MonoBehaviour {

    [SerializeField]
    GameObject highlight = null;
    //public Material highlightMaterial;
    //Material originalMaterial;
    GameObject lastHighlightedObject;
    GameObject highlighted;
 
    void HighlightObject(GameObject gameObject)
    {
        // If this object is different to the last highlighted
        if (lastHighlightedObject != gameObject)
        {
            // Clear the last highlight from block, update last to new block
            ClearHighlighted();
            lastHighlightedObject = gameObject;

            // Create highlight material at the position and rotation of block
            highlighted = Instantiate(highlight, gameObject.transform.position, gameObject.transform.rotation);
            // Scale highlight to slightly bigger than the block size
            highlighted.transform.localScale = Vector3.Scale(new Vector3(1.02f, 1.02f, 1.02f),
                gameObject.transform.lossyScale);
            // Drop down slightly, as highlight has spawned in line with the bottom of block
            // Use scale of the block as it may be 1-5
            highlighted.transform.position -= Vector3.Scale(new Vector3(0f, 0.01f, 0f),
                gameObject.transform.lossyScale);
            // Attach highlight to a child of the block
            highlighted.transform.parent = gameObject.transform;
        }
 
    }
 
    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            Destroy(highlighted);
            lastHighlightedObject = null;
        }
    }
 
    void HighlightObjectInCenterOfCam()
    {
        float rayDistance = 1000.0f;
        // Ray from the center of the viewport.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            // Get the object that was hit and pass to highlight method
            GameObject hitObject = rayHit.collider.gameObject;
            Debug.Log(hitObject.tag);
            if (hitObject.tag == "Block")
            {
                HighlightObject(hitObject);
            }
        } else
        {
            // If ray didn't hit, remove the highlight (no block in cursor)
            ClearHighlighted();
        }
    }
 
    void Update()
    {
        HighlightObjectInCenterOfCam();
    }
}