using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    [Header("Force Settings")]
    public float forceStrength = 500f; // 力量大小，可调
    public bool useDeltaTime = true;    // 是否乘 Time.deltaTime

    [Header("References")]
    public MemoryManager memoryManager;  // 场景里的 MemoryManager
    public Animator animator;            // 玩家 Animator
    public string hitSuccessTrigger = "HitSuccess"; // 成功动画 Trigger
    public string hitFailTrigger = "HitFail";       // 失败动画 Trigger

    private void Update()
    {
        if (memoryManager == null) return;

        if (Input.GetKey(upKey))
            ApplyForceToClosest(Vector2.up);

        if (Input.GetKey(downKey))
            ApplyForceToClosest(Vector2.down);
    }

    void ApplyForceToClosest(Vector2 direction)
    {
        if (memoryManager.activeObjects.Count == 0) return;

        // 找最近的泡泡
        PhysicsObject target = memoryManager.FindClosestToX(memoryManager.transform.position.x);
        if (target == null) return;

        // 添加力
        float multiplier = useDeltaTime ? Time.deltaTime : 1f;
        target.Rigidbody.AddForce(direction * forceStrength * multiplier, ForceMode2D.Force);

        // 检查是否碰撞触发分数
        CheckMemoryHit(target, direction);
    }

    void CheckMemoryHit(PhysicsObject memoryObj, Vector2 direction)
    {
        // 这里假设 negativeSprite 是坏泡泡，positiveSprite 是好泡泡
        bool isGood = memoryObj.Attribute > 0;
        bool isFail = (isGood && direction == Vector2.down) || (!isGood && direction == Vector2.up);

        if (isFail)
        {
            ScoreManager_dog.Instance.AddScore(-1);
            if (animator != null)
                animator.SetTrigger(hitFailTrigger);
        }
        else
        {
            ScoreManager_dog.Instance.AddScore(1);
            if (animator != null)
                animator.SetTrigger(hitSuccessTrigger);
        }

        // 播放戳破效果后删除泡泡
        Destroy(memoryObj.gameObject);
    }
}
