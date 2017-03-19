using UnityEngine;

public class Monster : MonoBehaviour
{
    // 随机颜色
    public int color { get; set; }
    // 随机速度
    public int speed { get; set; }

    private string playerTag = "Player";

    void Update()
    {
        // 怪物移动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // 相同颜色为朋友，碰撞加分
            if (color == GameController.Instance.playerColor)
            {
                Destroy(this.gameObject);
                GameController.Instance.UpdateScore(1);
            }
            // 不同颜色为敌人，碰撞游戏结束
            else
            {
                GameController.Instance.GameOver();
                Debug.Log("Game Over!!!");
            }
        }
    }
}
