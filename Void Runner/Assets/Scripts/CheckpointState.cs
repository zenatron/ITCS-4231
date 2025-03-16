using UnityEngine;

[CreateAssetMenu(fileName = "CheckpointState", menuName = "Scriptable Objects/CheckpointState")]
public class CheckpointState : ScriptableObject
{
    public int lastCheckpointID;
    public Vector3 lastCheckpointPosition;
}
