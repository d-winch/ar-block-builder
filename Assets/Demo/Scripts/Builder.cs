using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject[] blocks;
    private ARRaycastManager raycastManager;
    [SerializeField]
    private LayerMask blockLayer;
    private int selectedBlock;
    [SerializeField]
    public float blockScale;
    // Start is called before the first frame update
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public void OnBuildButtonPressed()
    {
        List<ARRaycastHit> arHits = new List<ARRaycastHit>();
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(rayToCast, out hitInfo, 800, blockLayer))
        {
            GameObject raycastReturn = hitInfo.collider.gameObject.transform.parent.gameObject;

            /*text.text = hitInfo.normal.ToString() + " "
                + hitInfo.transform.position.ToString() + " "
                + raycastReturn.transform.lossyScale.ToString() + " "
                + raycastReturn.GetType().ToString() + " "
                + raycastReturn.GetInstanceID().ToString() + " "
                + blockScale.ToString();*/

            Vector3 buildablePosition =
                Vector3.Scale(hitInfo.normal, raycastReturn.transform.lossyScale)
                + hitInfo.transform.position;
            Quaternion buildableRotation = hitInfo.transform.rotation;
            Build(buildablePosition, buildableRotation);
        }
        else
        {
            raycastManager.Raycast(rayToCast, arHits, TrackableType.Planes);
            if (arHits.Count > 0)
            {
                Vector3 buildablePosition = new Vector3(
                    Mathf.Round(arHits[0].pose.position.x / 1) * 1,
                    arHits[0].pose.position.y,
                    Mathf.Round(arHits[0].pose.position.z / 1) * 1
                    );
                Quaternion buildableRotation = arHits[0].pose.rotation;
                Build(buildablePosition, buildableRotation);
            }
        }
    }

    public void SetBlock(int block)
    {
        selectedBlock = block;
    }

    void Build(Vector3 position, Quaternion rotation)
    {
        GameObject block = Instantiate(blocks[selectedBlock], position, rotation);
        block.transform.localScale = new Vector3(blockScale, blockScale, blockScale);
    }

    public void OnDeleteButtonPressed()
    {
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(rayToCast, out hitInfo, 800, blockLayer))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }

    public void SetBlockScale(Slider slider)
    {
        blockScale = slider.value;
    }
}
