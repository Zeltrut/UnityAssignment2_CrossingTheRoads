using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SegmentGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [Tooltip("The different segment prefabs that can be spawned.")]
    public GameObject[] segment;
    [Tooltip("The final segment prefab with the portal.")]
    [SerializeField] private GameObject endSegmentPrefab;
    [Tooltip("How many random segments to generate before the end segment.")]
    [SerializeField] private int maxSegments = 35;
    
    [Tooltip("The Z-axis position where the FIRST segment will spawn.")]
    [SerializeField] private float zPos = 50;
    
    [Tooltip("The length of one segment. This value will be added to zPos each time.")]
    [SerializeField] private float segmentLength = 50f;

    [SerializeField] private bool creatingSegment = false;

    private int segmentsGeneratedCount = 0;
    private bool isLevelFinished = false;

    [Header("Dynamic Generation Speed")]
    [SerializeField] private float baseGenerationDelay = 2.3f;
    [SerializeField] private float fastGenerationDelay = 1.0f;
    [SerializeField] private float speedThreshold = 20f;
    
    [Header("References")]
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("Optimization")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float destroyDistance = 100f;

    private List<GameObject> spawnedSegments = new List<GameObject>();
    public List<SegmentData> generatedSegmentData = new List<SegmentData>();

    void Start()
    {
        segmentsGeneratedCount = 0;
        isLevelFinished = false;

        if (playerTransform == null || firstPersonController == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                firstPersonController = player.GetComponent<FirstPersonController>();
            }
            else
            {
                Debug.LogError("SegmentGenerator Error: Could not find GameObject with tag 'Player'. Dynamic speed and segment destruction will not work.");
            }
        }
    }

    void Update()
    {
        if (creatingSegment == false && !isLevelFinished)
        {
            creatingSegment = true;
            StartCoroutine(GenerateSegmentCoroutine());
        }

        CheckAndDestroySegments();
    }

    private void CheckAndDestroySegments()
    {
        if (playerTransform == null) return;

        for (int i = spawnedSegments.Count - 1; i >= 0; i--)
        {
            if (playerTransform.position.z > spawnedSegments[i].transform.position.z + destroyDistance)
            {
                Destroy(spawnedSegments[i]);
                spawnedSegments.RemoveAt(i);
                generatedSegmentData.RemoveAt(i);
            }
        }
    }

    IEnumerator GenerateSegmentCoroutine()
    {
        Vector3 spawnPosition = new Vector3(0, 0, zPos);

        if (segmentsGeneratedCount < maxSegments)
        {
            int segmentNum = Random.Range(0, segment.Length);
            GameObject newSegment = Instantiate(segment[segmentNum], spawnPosition, Quaternion.identity);
            spawnedSegments.Add(newSegment);
            generatedSegmentData.Add(new SegmentData { prefabIndex = segmentNum, position = spawnPosition });
            segmentsGeneratedCount++;
        }
        else if (segmentsGeneratedCount == maxSegments)
        {
            GameObject newSegment = Instantiate(endSegmentPrefab, spawnPosition, Quaternion.identity);
            spawnedSegments.Add(newSegment);
            generatedSegmentData.Add(new SegmentData { prefabIndex = -1, position = spawnPosition });
            segmentsGeneratedCount++;
            isLevelFinished = true;
        }

        zPos += segmentLength;

        float delay = baseGenerationDelay;
        if (firstPersonController != null && firstPersonController.CurrentSpeed > speedThreshold)
        {
            delay = fastGenerationDelay;
        }

        yield return new WaitForSeconds(delay);
        creatingSegment = false;
    }

    public void LoadSegments(List<SegmentData> segmentsToLoad)
    {
        StopAllCoroutines();
        creatingSegment = false;
        
        segmentsGeneratedCount = 0;
        isLevelFinished = false;

        foreach (GameObject spawnedSegment in spawnedSegments)
        {
            Destroy(spawnedSegment);
        }
        spawnedSegments.Clear();
        generatedSegmentData.Clear();

        if (segmentsToLoad == null || segmentsToLoad.Count == 0) return;

        foreach (SegmentData data in segmentsToLoad)
        {
            GameObject loadedSegment;
            if (data.prefabIndex == -1)
            {
                loadedSegment = Instantiate(endSegmentPrefab, data.position, Quaternion.identity);
                isLevelFinished = true;
            }
            else
            {
                loadedSegment = Instantiate(segment[data.prefabIndex], data.position, Quaternion.identity);
                segmentsGeneratedCount++;
            }
            
            spawnedSegments.Add(loadedSegment);
            generatedSegmentData.Add(data);
        }

        if (spawnedSegments.Count > 0)
        {
            zPos = spawnedSegments[spawnedSegments.Count - 1].transform.position.z + segmentLength;
        }
    }
}