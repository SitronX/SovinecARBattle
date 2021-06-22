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
        Destroy(spawnedObject);
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
    public void TapToPlaceEnable()
    {
        this.GetComponent<TapToPlace>().enabled = true;
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

            Physics.gravity = new Vector3(trackedImage.transform.position.x, trackedImage.transform.position.y-9.81f, trackedImage.transform.position.z);   //Quaternion.identity je v ARFoundation divne naklonen, musi se proto pouzit rotace z raycastu, nebo z rozpoznavaneho obrazku. 
                                                                                                                                                            //Pote se obrazek polozi do spravne rotace, ale physics.gravity funguje porad s (0,-9.81f,0), kvuli tomu ze je zde vyuzita primitivni fyzika k delovym koulim a kamenum, musi se upravit
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
