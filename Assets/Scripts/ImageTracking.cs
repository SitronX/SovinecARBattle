using System.Collections;
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
    private GameObject spawnedObject;
    public ARTrackedImageManager trackedImageManager;



    private void Awake()
    {
        



        /*foreach (GameObject prefab in placeablePrefabs)
        {
            
            
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }*/
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
        
    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        /*
        string name = trackedImage.referenceImage.name;


            
        GameObject prefab = spawnedPrefabs[name];


        
        prefab.transform.position = trackedImage.transform.position;
        prefab.transform.rotation = trackedImage.transform.rotation;
        prefab.SetActive(true);

        prefab.transform.parent = trackedImage.gameObject.transform;

        */

        if(spawnedObject==null)
        {
            spawnedObject= Instantiate(placeablePrefabs[0], trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObject.transform.parent = trackedImage.gameObject.transform;
        }


            

     


        

        /*foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name!=name)
            {
                go.SetActive(false);
            }
        }*/
    }
}
