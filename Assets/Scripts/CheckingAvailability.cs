using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingAvailability : MonoBehaviour
{
    [SerializeField] private SpawnerGarbage _spawnerGarbage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Garbage garbage))
        {
            if (garbage.Removed)
            {
                return;
            }
            else
            {
                garbage.gameObject.SetActive(false);
                garbage.transform.position = _spawnerGarbage.transform.position;
                garbage.CheckVisibleGarbage();
                garbage.gameObject.SetActive(true);
                garbage.transform.position = garbage.transform.position;
                garbage.CheckInVisible();
            }
        }
    }
}
