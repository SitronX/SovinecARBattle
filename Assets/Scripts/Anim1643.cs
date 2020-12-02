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
        gameObject.GetComponent<Animator>().SetTrigger("1643trigger");


        //Defending ARMY-------------------------------------------------------------------------

        StartCoroutine(SoldierDie(DefendingArmy[0], 25));

        StartCoroutine(SoldierDie(DefendingArmy[2], 29));

        StartCoroutine(SoldierDie(DefendingArmy[3], 37));

        StartCoroutine(SoldierDie(DefendingArmy[4], 23));


        StartCoroutine(SoldierIdle(DefendingArmy[5], 0));
        StartCoroutine(SoldierAttack(DefendingArmy[5], 20));
        StartCoroutine(SoldierDie(DefendingArmy[5], 27));



        StartCoroutine(SoldierIdle(DefendingArmy[6], 0));
        StartCoroutine(SoldierAttack(DefendingArmy[6], 28));
        StartCoroutine(SoldierDie(DefendingArmy[6], 32));


        StartCoroutine(SoldierIdle(DefendingArmy[7], 0));
        StartCoroutine(SoldierAttack(DefendingArmy[7], 23));
        StartCoroutine(SoldierIdle(DefendingArmy[7], 28));
        StartCoroutine(SoldierAttack(DefendingArmy[7], 36));
        StartCoroutine(SoldierDie(DefendingArmy[7], 38));















        //ATTACKING ARMY----------------------------------------------------------------------------

        StartCoroutine(CannonShot(cannonCrew, 9));

        StartCoroutine(SoldierClimb(AttackingArmy[0], 20));
        StartCoroutine(SoldierIdle(AttackingArmy[0], 26));


        StartCoroutine(SoldierClimb(AttackingArmy[2], 21));
        StartCoroutine(SoldierWalk(AttackingArmy[2], 25));
        StartCoroutine(SoldierAttack(AttackingArmy[2], 35));



        StartCoroutine(SoldierDie(AttackingArmy[3], 4));

        StartCoroutine(SoldierClimb(AttackingArmy[4], 19));
        StartCoroutine(SoldierAttack(AttackingArmy[4], 25));
        StartCoroutine(SoldierIdle(AttackingArmy[4], 34));



        StartCoroutine(SoldierDie(AttackingArmy[5], 7));

        StartCoroutine(SoldierClimb(AttackingArmy[6], 17));
        StartCoroutine(SoldierDie(AttackingArmy[6], 25));



        StartCoroutine(SoldierClimb(AttackingArmy[7], 17));
        StartCoroutine(SoldierDie(AttackingArmy[7], 22));

        StartCoroutine(SoldierClimb(AttackingArmy[8], 18));
        StartCoroutine(SoldierAttack(AttackingArmy[8], 23));
        StartCoroutine(SoldierWalk(AttackingArmy[8], 30));
        StartCoroutine(SoldierAttack(AttackingArmy[8], 36));





        StartCoroutine(SoldierClimb(AttackingArmy[9], 12));
        StartCoroutine(SoldierAttack(AttackingArmy[9], 21));
        StartCoroutine(SoldierIdle(AttackingArmy[9], 25));



        StartCoroutine(SoldierClimb(AttackingArmy[10], 15));
        StartCoroutine(SoldierIdle(AttackingArmy[10], 24));

















    }

    IEnumerator SoldierDie(GameObject obj,int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
    }
    IEnumerator SoldierClimb(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Climb");
    }
    IEnumerator SoldierAttack(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
    }
    IEnumerator SoldierWalk(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
    }
    IEnumerator SoldierIdle(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
    }
    IEnumerator CannonShot(GameObject obj, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("CannonShot");



    }

}
