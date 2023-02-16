using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITraptable
{
    void ActivateTrap();
}
public class SpikeTrap : MonoBehaviour, ITraptable
{
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private Spikes spikes;

    public void ActivateTrap()
    {
        spikes.ActivateTrap(delayTime);
    }
}
