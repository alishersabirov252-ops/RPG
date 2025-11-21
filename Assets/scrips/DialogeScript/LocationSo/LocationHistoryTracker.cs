using UnityEngine;
using System.Collections.Generic;
public class LocationHistoryTracker : MonoBehaviour
{
    private static LocationHistoryTracker _instance;
    public static LocationHistoryTracker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LocationHistoryTracker>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("LocationHistoryTracker");
                    _instance = go.AddComponent<LocationHistoryTracker>();
                }
            }
            return _instance;
        }
    }

    private readonly List<LocationSo> locationsVisited = new List<LocationSo>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RecordLocation(LocationSo locationSo)
    {
        locationsVisited.Add(locationSo);
        Debug.Log("Just visited " + locationSo.DisplayName);
    }

    public bool HasVisited(LocationSo locationSo)
    {
        return locationsVisited.Contains(locationSo);
    }
}
