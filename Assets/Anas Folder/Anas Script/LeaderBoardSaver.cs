using Dan.Main;
using UnityEngine;

public class LeaderBoardSaver : MonoBehaviour
{
    private string PublicLeaderboardKey = "173dfa58e70bb93f4f4e476093f56f1ff28a635f63680ccdfc92e801f9b5bf3b";

    public static LeaderBoardSaver Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void SeetLeaderbordEntry(string Username, int Score)
    {
        LeaderboardCreator.UploadNewEntry(PublicLeaderboardKey, Username, Score);

        LeaderboardCreator.ResetPlayer();
    }
}

