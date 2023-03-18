﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    [SerializeField]
    private GameObject[] blocks = null;
    private ARRaycastManager raycastManager;
    [SerializeField]
    private LayerMask blockLayer = 0;
    private int selectedBlock;
    [SerializeField]
    public float blockScale = 1;

    void Awake()
    {
        Debug.Log("raycastManager Instantiating");
        raycastManager = GetComponent<ARRaycastManager>();
        Debug.Log("raycastManager Instantiated");
    }

    public void OnBuildButtonPressed()
    {
        List<ARRaycastHit> arHits = new List<ARRaycastHit>();
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(rayToCast, out hitInfo, 800, blockLayer))
        {
            Debug.Log("raycastManager hit an existing block");
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
        Debug.Log("SetBlock to block id " + block);
        selectedBlock = block;
    }

    void Build(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Building block at position: " + position + " with rotation: " + rotation);
        GameObject block = Instantiate(blocks[selectedBlock], position, rotation);
        block.transform.localScale = new Vector3(blockScale, blockScale, blockScale);
    }

    public void OnDeleteButtonPressed()
    {
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(rayToCast, out hitInfo, 800, blockLayer))
        {
            Debug.Log("Deleting block gameObject: " + hitInfo.collider.gameObject.name);
            Destroy(hitInfo.collider.gameObject);
        }
    }

    public void SetBlockScale(Slider slider)
    {
        Debug.Log("SetBlockScale with value: " + slider.value);
        blockScale = slider.value;
    }
}
