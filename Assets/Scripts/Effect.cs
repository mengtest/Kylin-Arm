using UnityEngine;

public class Effect : MonoBehaviour
{
    public float delayTime = 2.0f;

    void Start()
    {
        // 爆炸效果在一段时间后销毁
        Destroy(this.gameObject, delayTime);
    }
}
