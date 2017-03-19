using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 爆炸效果
    public GameObject effect;
    // 常量
    private const float delayTime = 2.0f;
    private const float speed = 8.0f;

    private string MonsterTag = "Monster";

    void Start()
    {
        // 子弹一段时间后自动销毁
        Destroy(this.gameObject, delayTime);
    }

    void Update()
    {
        // 子弹移动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(MonsterTag))
        {
            // 相同颜色为朋友，碰撞游戏结束
            if (other.GetComponent<Monster>().color == GameController.Instance.playerColor)
            {
                GameController.Instance.GameOver();
                Debug.Log("Game Over!!!");
            }
            // 不同颜色为敌人，碰撞加分
            else
            {
                // 设置分数
                GameController.Instance.UpdateScore(1);
                // 爆炸效果
                GameObject go = Instantiate(effect);
                go.transform.position = other.transform.position;
                // 播放音效
                AudioManager.Instance.PlayClip("bang");
                // 销毁物体
                Destroy(other.gameObject);
                // 销毁子弹
                Destroy(this.gameObject);
            }
        }
    }
}
