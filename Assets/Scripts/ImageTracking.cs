﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] placeablePrefabs;
    public Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;



    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();



        foreach (GameObject prefab in placeablePrefabs)
        {
            
            
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;

    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        
        string name = trackedImage.referenceImage.name;

        /*if(!spawnedPrefabs[name].activeSelf)
        {
            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
            prefab.SetActive(true);

        }*/


        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = Vector3.Lerp(prefab.transform.position, trackedImage.transform.position, 0.02f);
        prefab.transform.rotation = Quaternion.Lerp(prefab.transform.rotation, trackedImage.transform.rotation, 0.02f);
        prefab.SetActive(true);

        /*foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name!=name)
            {
                go.SetActive(false);
            }
        }*/
    }
}
