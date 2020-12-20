using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public int platformSize;

    [SerializeField] List<GameObject> platformPrefabs;
    [SerializeField] List<GameObject> hiddenPlatformPrefabs;

    public static int _platformSize;

    private static  List<GameObject> _platformsList;
    private static List<GameObject> _hiddenPlatformsList;
    private static Vector3 _nextSpawnLocation;
    private static Vector3 _nextHiddenSpawnLocation;


    private void Start()
    {
        _nextSpawnLocation= _nextHiddenSpawnLocation= new Vector3(0, 0, platformSize*7);
        _platformsList = platformPrefabs;        
        _hiddenPlatformsList = hiddenPlatformPrefabs;

        _platformSize = platformSize;
    }
    public static void PoolObject(GameObject pulledPlatform)
    {
        if (pulledPlatform.gameObject.CompareTag("Platform")|| pulledPlatform.gameObject.CompareTag("Booster"))
        {
            //turn off old platform
            pulledPlatform.gameObject.SetActive(false);

            //pull old platform
            _platformsList.Insert(Random.Range(0, _platformsList.Count), pulledPlatform);

            //shuffle
            int i = Random.Range(0, _platformsList.Count);

            //to make sure its not pulling an active platform
            while (_platformsList[i].activeSelf) i = Random.Range(0, _platformsList.Count);

            //move random platform to next position, without changing height
            _platformsList[i].transform.position = new Vector3(0, _platformsList[i].transform.position.y, _nextSpawnLocation.z);

            //activate
            _platformsList[i].SetActive(true);

            //remove form list
            _platformsList.RemoveAt(i);

            //calculate new next spwan location
            _nextSpawnLocation.z += _platformSize;
        }
        else
        {
            //turn off old platform
            pulledPlatform.gameObject.SetActive(false);

            //pull old platform
            _hiddenPlatformsList.Insert(Random.Range(0, _hiddenPlatformsList.Count), pulledPlatform);

            //shuffle
            int i = Random.Range(0, _hiddenPlatformsList.Count);

            //to make sure its not pulling an active platform
            while (_hiddenPlatformsList[i].activeSelf) i = Random.Range(0, _hiddenPlatformsList.Count);

            //move random platform to next position, without changing height
            _hiddenPlatformsList[i].transform.position = new Vector3(0, _hiddenPlatformsList[i].transform.position.y, _nextHiddenSpawnLocation.z);

            //activate
            _hiddenPlatformsList[i].SetActive(true);

            //remove form list
            _hiddenPlatformsList.RemoveAt(i);

            //calculate new next spwan location
            _nextHiddenSpawnLocation.z += _platformSize;
        }
    }
}
