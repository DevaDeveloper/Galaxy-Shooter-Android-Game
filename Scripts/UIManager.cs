using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject _playAgain;
    

    public int score;
    public int bestScore;
    public Text _bestScore;
    public Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    private Monetization1 _Monetization1;
    [SerializeField]
    private GameObject AdVisualizator;

    // Start is called before the first frame update
    void Start()
    {
        
        _Monetization1 = GameObject.Find("AdController").GetComponent<Monetization1>();
        bestScore = PlayerPrefs.GetInt("HighScore", defaultValue: 0);
        _bestScore.text = "Best Score: " + bestScore;
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("Game Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void UpdateScore()
    {
        score += 10;
        _scoreText.text = "Score: " + score;
    }
    //public void UpdateScore(int playerScore)
    // {

    //     _scoreText.text = "Score: " + playerScore.ToString();
    // }
    public void CheckBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            _bestScore.text = "Best Score: " + bestScore;
        }
    }
    
    



    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        AdVisualizator.SetActive(false);
        _playAgain.SetActive(true);
        StartCoroutine(GameAd());
        //ContinueWithAd();

    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumeGame()
    {
        _gameManager.ContinueGame();

    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    

    IEnumerator GameAd()
    {
        _Monetization1.RewardedVideoAd();
        yield return new WaitForSeconds(0.5f);
        
    }

}


