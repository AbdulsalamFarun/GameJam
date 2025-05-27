using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SorceMangeScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;


    public UnityEvent<string, int> SubmitScoreEvent;
    void Start()
    {
        inputName.text = "";
    }
    void EndGame()
    {

        PlayerPrefs.SetString("Player", inputName.text);
    }

    public void SubmitScore()
    {
        EndGame();
        SceneManager.LoadScene("NawafMainMenu");
    }

}