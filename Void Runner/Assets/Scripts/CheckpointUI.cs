using UnityEngine;
using TMPro;

public class CheckpointUI : MonoBehaviour
{
    public TextMeshProUGUI checkpointCurrent;
    private int cp;

    void Start()
    {
        cp = CheckpointManager.Instance.GetCurrentCheckpoint();
        SetCheckpointText();

    }

    void Update()
    {
        cp = CheckpointManager.Instance.GetCurrentCheckpoint();
        SetCheckpointText();
    }

    void SetCheckpointText()
    {
        checkpointCurrent.text = "Checkpoint: " + cp.ToString();
    }
}