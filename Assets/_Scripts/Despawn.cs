using UnityEngine.Events;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] float timer = 0f;
    public UnityEvent OnTimerEnd;

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            OnTimerEnd?.Invoke();
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
