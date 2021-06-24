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

    public GameObject gravityPrefab;
    private GameObject gravityCenter;



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
    private void UpdateImage(ARTrackedImage trackedImage)
    {
       

        if(spawnedObject==null)
        {
            spawnedObject= Instantiate(placeablePrefabs[0], trackedImage.transform.position, trackedImage.transform.rotation);
            gravityCenter = Instantiate(gravityPrefab, trackedImage.transform.position, trackedImage.transform.rotation);

            spawnedObject.transform.parent = trackedImage.gameObject.transform;
            gravityCenter.transform.parent = trackedImage.gameObject.transform;

            gravityCenter.transform.Translate(new Vector3(0, -9.81f, 0), Space.Self);

            Physics.gravity = gravityCenter.transform.position;     //Quaternion.identity je v ARFoundation divne naklonen, musi se proto pouzit rotace z raycastu, nebo z rozpoznavaneho obrazku. 
                                                                    //Pote se obrazek polozi do spravne rotace, ale physics.gravity funguje porad s (0,-9.81f,0), kvuli tomu ze je zde vyuzita primitivni fyzika k delovym koulim a kamenum, musi se upravit s pomoci metody vyse se vytahne korektni vector 3 hodnota a ta se priradi
        }

    }
}
