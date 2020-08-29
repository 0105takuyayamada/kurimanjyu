using UnityEngine;
using UniRx;

public class Copy : MonoBehaviour
{
    const int COPY_COUNT = 8;
    const float RANDOM_RANGE = 0.3f;

    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    CircleCollider2D circleCollider;

    int count = 1;
    public int sumCount = 1;

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetMouseButtonDown(0))
            {
                count++;
                sumCount++;
                if (sumCount - count >= COPY_COUNT) return;

                Copy copy = Instantiate(gameObject,
                    transform.position
                    + new Vector3(
                        Random.Range(-RANDOM_RANGE, RANDOM_RANGE),
                        Random.Range(0f, RANDOM_RANGE)),
                    Quaternion.identity, transform.parent).GetComponent<Copy>();
                copy.sumCount = sumCount;
            }

        }).AddTo(this);
    }
}