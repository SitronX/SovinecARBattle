using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim1643 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> AttackingArmy;
    public List<GameObject> DefendingArmy;
    public GameObject cannonCrew;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainAnimStart()
    {
        DefendingArmy[5].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Iddle");
        DefendingArmy[6].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Iddle");
        DefendingArmy[7].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Iddle");

        StartCoroutine(SoldierDie(AttackingArmy[3], 4));
        StartCoroutine(SoldierDie(AttackingArmy[5], 8));
        StartCoroutine(CannonShot(cannonCrew, 9));

    }

    IEnumerator SoldierDie(GameObject obj,int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
    }
    IEnumerator CannonShot(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shot");



    }

}
