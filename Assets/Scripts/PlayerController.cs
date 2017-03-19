using UnityEngine;
using System.Collections;

// 定义上下左右方向
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerController : MonoBehaviour
{
    // 子弹
    public GameObject bulletPrefab;
    public Transform upBulletPoint;
    public Transform downBulletPoint;
    public Transform leftBulletPoint;
    public Transform rightBulletPoint;

    // 常量
    private Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
    private const float delayTime = 0.02f;
    private const float offset = 0.02f;

    void Update()
    {
        if (GameController.Instance.IsOver) { return; }

        // 一共两个步骤：设置角色方向，开启协程
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            StartCoroutine("Click", Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.eulerAngles = new Vector3(0, 0, -180);
            StartCoroutine("Click", Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.eulerAngles = new Vector3(0, 0, -270);
            StartCoroutine("Click", Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.eulerAngles = new Vector3(0, 0, -90);
            StartCoroutine("Click", Direction.Right);
        }
    }

    // 按下对应方向键
    IEnumerator Click(Direction dir)
    {
        // 生成子弹
        GameObject go = Instantiate(bulletPrefab);
        // 播放声音
        AudioManager.Instance.PlayClip("bomb");
        switch (dir)
        {
            // 一共两个步骤：设置父物体，设置角色震动效果
            case Direction.Up:
                go.transform.SetParent(upBulletPoint);
                this.transform.position = new Vector3(0, offset, 0);
                break;
            case Direction.Down:
                go.transform.SetParent(downBulletPoint);
                this.transform.position = new Vector3(0, -offset, 0);
                break;
            case Direction.Left:
                go.transform.SetParent(leftBulletPoint);
                this.transform.position = new Vector3(-offset, 0, 0);
                break;
            case Direction.Right:
                go.transform.SetParent(rightBulletPoint);
                this.transform.position = new Vector3(offset, 0, 0);
                break;
            default:
                break;
        }
        // 初始化位置、旋转、大小
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = scale;

        // 延迟一下
        yield return new WaitForSeconds(delayTime);
        // 恢复到原点
        this.transform.position = Vector3.zero;
    }
}
