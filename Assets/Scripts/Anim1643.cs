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
    
    public void MainAnimStart()
    {
        StopAllCoroutines();
        gameObject.GetComponent<Animator>().SetTrigger("1643trigger");


        //Defending ARMY-------------------------------------------------------------------------

        StartCoroutine(SoldierAttack(DefendingArmy[0], 0));
        StartCoroutine(SoldierDie(DefendingArmy[0], 25));

        StartCoroutine(SoldierAttack(DefendingArmy[2], 0));
        StartCoroutine(SoldierDie(DefendingArmy[2], 29));

        StartCoroutine(SoldierAttack(DefendingArmy[3], 0));
        StartCoroutine(SoldierDie(DefendingArmy[3], 37));

        StartCoroutine(SoldierAttack(DefendingArmy[4], 0));
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
        StartCoroutine(SoldierAttack(DefendingArmy[7], 32));
        StartCoroutine(SoldierDie(DefendingArmy[7], 38));

        StartCoroutine(SoldierIdle(DefendingArmy[8], 0));
        StartCoroutine(SoldierWalk(DefendingArmy[8], 24));
        StartCoroutine(SoldierAttack(DefendingArmy[8], 26));
        StartCoroutine(SoldierDie(DefendingArmy[8], 30));


        StartCoroutine(SoldierIdle(DefendingArmy[9], 0));
        StartCoroutine(SoldierWalk(DefendingArmy[9], 24));
        StartCoroutine(SoldierAttack(DefendingArmy[9], 26));
        StartCoroutine(SoldierDie(DefendingArmy[9], 28));

        StartCoroutine(SoldierIdle(DefendingArmy[10], 0));
        StartCoroutine(SoldierWalk(DefendingArmy[10], 24));
        StartCoroutine(SoldierAttack(DefendingArmy[10], 28));
        StartCoroutine(SoldierDie(DefendingArmy[10], 32));






        //ATTACKING ARMY----------------------------------------------------------------------------

        StartCoroutine(CannonShot(cannonCrew, 9));

        StartCoroutine(SoldierWalk(AttackingArmy[0], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[0], 20));
        StartCoroutine(SoldierWalk(AttackingArmy[0], 26));
        StartCoroutine(SoldierAttack(AttackingArmy[0], 28));
        StartCoroutine(SoldierWalk(AttackingArmy[0], 33));
        StartCoroutine(SoldierIdle(AttackingArmy[0], 36));



        StartCoroutine(SoldierWalk(AttackingArmy[1], 0));
        StartCoroutine(SoldierDie(AttackingArmy[1], 7));


        StartCoroutine(SoldierWalk(AttackingArmy[2], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[2], 21));
        StartCoroutine(SoldierWalk(AttackingArmy[2], 25));
        StartCoroutine(SoldierAttack(AttackingArmy[2], 35));
        StartCoroutine(SoldierIdle(AttackingArmy[2], 39));



        StartCoroutine(SoldierWalk(AttackingArmy[3], 0));
        StartCoroutine(SoldierDie(AttackingArmy[3], 4));

        StartCoroutine(SoldierWalk(AttackingArmy[4], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[4], 19));
        StartCoroutine(SoldierAttack(AttackingArmy[4], 25));
        StartCoroutine(SoldierDie(AttackingArmy[4], 33));


        StartCoroutine(SoldierWalk(AttackingArmy[5], 0));
        StartCoroutine(SoldierDie(AttackingArmy[5], 7));

        StartCoroutine(SoldierWalk(AttackingArmy[6], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[6], 17));
        StartCoroutine(SoldierDie(AttackingArmy[6], 25));

        StartCoroutine(SoldierWalk(AttackingArmy[7], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[7], 16));
        StartCoroutine(SoldierAttack(AttackingArmy[7], 20));
        StartCoroutine(SoldierDie(AttackingArmy[7], 22));







        StartCoroutine(SoldierWalk(AttackingArmy[8], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[8], 18));
        StartCoroutine(SoldierAttack(AttackingArmy[8], 23));
        StartCoroutine(SoldierWalk(AttackingArmy[8], 30));
        StartCoroutine(SoldierAttack(AttackingArmy[8], 36));
        StartCoroutine(SoldierIdle(AttackingArmy[8], 39));





        StartCoroutine(SoldierWalk(AttackingArmy[9], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[9], 12));
        StartCoroutine(SoldierAttack(AttackingArmy[9], 20));
        StartCoroutine(SoldierWalk(AttackingArmy[9], 23));
        StartCoroutine(SoldierAttack(AttackingArmy[9], 26));
        StartCoroutine(SoldierDie(AttackingArmy[9], 28));




        StartCoroutine(SoldierWalk(AttackingArmy[10], 0));
        StartCoroutine(SoldierClimb(AttackingArmy[10], 15));
        StartCoroutine(SoldierWalk(AttackingArmy[10], 23));
        StartCoroutine(SoldierAttack(AttackingArmy[10], 26));
        StartCoroutine(SoldierDie(AttackingArmy[10], 33));

        

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
        obj.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");


        yield return new WaitForSeconds(seconds);

        obj.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shot");


        yield return new WaitForSeconds(1);

        obj.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");




    }

}
