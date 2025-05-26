using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Dan.Main;
public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> Scores;
    private string PublicLeaderboardKey = "173dfa58e70bb93f4f4e476093f56f1ff28a635f63680ccdfc92e801f9b5bf3b";
    private void Start()
    {

        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(PublicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                Scores[i].text = msg[i].Score.ToString();
            }

        }));
    }
}