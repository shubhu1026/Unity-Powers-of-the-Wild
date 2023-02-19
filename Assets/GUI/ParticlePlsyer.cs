using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlsyer : MonoBehaviour
{
    [SerializeField] ParticleSystem skill;
    public void PlaySkillParticle()
    {
        skill.Play();
    }
}
