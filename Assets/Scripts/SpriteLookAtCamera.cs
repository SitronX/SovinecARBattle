using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class SpriteLookAtCamera : MonoBehaviour
{
    List<GameObject> staticSoldiers = new List<GameObject>();
    List<GameObject> movingSoldiers = new List<GameObject>();
    List<GameObject> soldiers = new List<GameObject>();
    [SerializeField] string chosenTag;
    ParticleSystem ps;
    [SerializeField] float groundOffset; 
    ParticleSystem.Particle[] particles;

    static bool turnedOn = true;
    void Start()
    {
        List<GameObject> preSort = GameObject.FindGameObjectsWithTag(chosenTag).ToList();
        foreach(GameObject i in preSort)
        {
            if(i.isStatic)
            {
                staticSoldiers.Add(i);       
            }
            else
            {
                movingSoldiers.Add(i);
            }
        }
        soldiers.AddRange(movingSoldiers);
        soldiers.AddRange(staticSoldiers);

        ps=GetComponent<ParticleSystem>();

        ParticleSystem.Burst burst = new ParticleSystem.Burst(0, soldiers.Count, int.MaxValue, 30);
        ps.emission.SetBurst(0, burst);

         particles = new ParticleSystem.Particle[soldiers.Count];
    }
    void Update()
    {
        if(turnedOn)
        {            
            ps.GetParticles(particles);

            for (int i = 0; i < particles.Count(); i++)
            {
                if (soldiers[i].activeInHierarchy)
                {
                    particles[i].position = soldiers[i].transform.position + new Vector3(0, groundOffset, 0);
                    particles[i].startSize = 1;
                }
                else
                {
                    particles[i].startSize = 0;
                }

            }
            ps.SetParticles(particles, particles.Count());
        }
    }
}
