using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnEvent
{
    public float timestamp;
    public int gameObjectIndex; 
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}


public class LevelController : MonoBehaviour
{
    public AudioSource music;

    public float spawnEarlyTime;
    public Transform cameraTransform;
    public float SpawnDistance;
    [SerializeField] private TextAsset spawnEventsFile;
    [SerializeField] public GameObject BlueBox1;
    [SerializeField] public GameObject PurpleBox2;


    private List<SpawnEvent> spawnEvents = new List<SpawnEvent>();
    private int currentIndex = 0;
    private bool hasInstantiatedAllObjects = false;

    // Start is called before the first frame update
    void Start()
    {
        if (music == null)
        {
            Debug.LogError("Audio Source is not assigned.");
            return;
        }
        LoadSpawnEvents();

        music.Play();
    }
    void LoadSpawnEvents()
    {
        if (spawnEventsFile != null)
        {
            string[] lines = spawnEventsFile.text.Split('\n');

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                string[] values = trimmedLine.Split(',');

                if (values.Length == 4)
                {
                    SpawnEvent spawnEvent = new SpawnEvent();
                    spawnEvent.timestamp = float.Parse(values[0]);
                    spawnEvent.gameObjectIndex = int.Parse(values[1]);
                    spawnEvent.spawnPosition = getSpawnPosition(int.Parse(values[2]));
                    spawnEvent.spawnRotation = getRotation(values[3]);
                    spawnEvents.Add(spawnEvent);
                }
                else
                {
                    Debug.LogWarning("Invalid line in the text file: " + line);
                }
            }
        }
        else
        {
            Debug.LogError("Spawn events file is not assigned.");
        }
    }

    Quaternion getRotation(string rotation)
    {
        rotation = rotation.ToLower();
        if (rotation == "u")
        {
            return Quaternion.Euler(180f, 0f, 0f);
        }
        else if (rotation == "d")
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        else if (rotation == "l")
        {
            return Quaternion.Euler(90f, 0f, 0f);
        }
        else if (rotation == "r")
        {
            return Quaternion.Euler(-90f, 0f, 0f);
        }
        else
        {
            Debug.LogError("Wrong rotation input: "+rotation);
            return Quaternion.Euler(0f, 0f, 0f);
        }
    }

    Vector3 getSpawnPosition(int position)
    {
        float x, y, z;
        x = cameraTransform.position.x - SpawnDistance;
        y = cameraTransform.position.y - (position / 4 - 2 + (position/4>=2 ? 1:0)) * 0.2f;
        z = cameraTransform.position.z + (position % 4 - 2) * 0.2f;
        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex < spawnEvents.Count && !hasInstantiatedAllObjects)
        {
            if (music.time >= spawnEvents[currentIndex].timestamp - spawnEarlyTime)
            {
                GameObject prefabToInstantiate = spawnEvents[currentIndex].gameObjectIndex == 1 ? BlueBox1 : PurpleBox2;
                GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnEvents[currentIndex].spawnPosition, spawnEvents[currentIndex].spawnRotation);

                MoveTowardCamera moveScript = instantiatedObject.GetComponent<MoveTowardCamera>();
                moveScript.InitializeMove(cameraTransform.position,spawnEarlyTime);

                currentIndex++;

                if (currentIndex == spawnEvents.Count)
                {
                    hasInstantiatedAllObjects = true;
                }
            }
        }
    }
}
