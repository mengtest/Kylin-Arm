using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // 单例模式
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    // UI
    public Button restartBtn;
    public Button pauseBtn;
    public Text scoreText;
    public Text gameOverText;

    // 玩家
    public MeshRenderer playerRenderer;
    public int playerColor = 0;

    // 怪物
    public GameObject monsterPrefab;
    public Transform[] monsterPoint;

    // 材质
    private Material[] material = new Material[3];
    // 分数
    private int score = 0;
    // 是否暂停
    private bool isPause = false;
    // 是否结束
    private bool isOver = false;
    public bool IsOver { get { return isOver; } }
    // 常量
    private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
    private const float monsterTime = 0.5f;
    private const float playerTime = 5.0f;

    void Awake()
    {
        // 设置单例
        _instance = this;
        // 加载资源
        material[0] = Resources.Load<Material>("Red");
        material[1] = Resources.Load<Material>("Green");
        material[2] = Resources.Load<Material>("Blue");
    }

    void Start()
    {
        // 添加监听事件
        restartBtn.onClick.AddListener(delegate { OnRestartBtnClick(); });
        pauseBtn.onClick.AddListener(delegate { OnPauseBtnClick(); });
        // 设置怪物定时器
        InvokeRepeating("RandomMonster", 0, monsterTime);
        // 设置玩家定时器
        InvokeRepeating("RandomPlayer", 0, playerTime);
        // 游戏开始
        GameStart();
    }

    void Update()
    {
        // 按下空格暂停
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPauseBtnClick();
        }
        // 游戏结束按下回车重新开始
        if (isOver && Input.GetKeyDown(KeyCode.Return))
        {
            OnRestartBtnClick();
        }
    }

    // 随机出怪，包括颜色、速度、位置
    void RandomMonster()
    {
        GameObject go = Instantiate(monsterPrefab);
        // 随机颜色
        int color = Random.Range(0, 3);
        go.GetComponent<Monster>().color = color;
        go.GetComponent<MeshRenderer>().material = material[color];
        // 随机速度
        int speed = Random.Range(3, 5);
        go.GetComponent<Monster>().speed = speed;
        // 随机方向
        int dir = Random.Range(0, 4);
        go.transform.SetParent(monsterPoint[dir]);
        // 初始化位置、旋转、大小
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = scale;
    }

    // 随机玩家颜色
    void RandomPlayer()
    {
        playerColor = Random.Range(0, 3);
        playerRenderer.material = material[playerColor];
    }

    // 重新开始
    void OnRestartBtnClick()
    {
        SceneManager.LoadScene(0);
        GameStart();
    }

    // 暂停游戏
    void OnPauseBtnClick()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseBtn.GetComponentInChildren<Text>().text = "继续";
        }
        else
        {
            Time.timeScale = 1;
            pauseBtn.GetComponentInChildren<Text>().text = "暂停";
        }
    }

    // 更新分数
    public void UpdateScore(int cnt)
    {
        score += cnt;
        scoreText.text = score.ToString();
    }

    // 设置游戏开始
    void GameStart()
    {
        isOver = false;
        // timeScale时间缩放，1表示游戏正常
        Time.timeScale = 1;
        // 游戏中禁用重新游戏按钮，允许点击暂停按钮
        restartBtn.interactable = false;
        pauseBtn.interactable = true;
        gameOverText.enabled = false;
    }

    // 设置游戏结束
    public void GameOver()
    {
        isOver = true;
        // timeScale时间缩放，0表示游戏暂停
        Time.timeScale = 0;
        // 游戏结束允许点击重新开始按钮，禁用暂停按钮
        restartBtn.interactable = true;
        pauseBtn.interactable = false;
        gameOverText.enabled = true;
    }
}
