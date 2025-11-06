using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SegmentData
{
    public int prefabIndex;
    public Vector3 position;
}

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    // Renamed from carrotCounter
    public int collectibleCount;
    public List<SegmentData> generatedSegments;
}