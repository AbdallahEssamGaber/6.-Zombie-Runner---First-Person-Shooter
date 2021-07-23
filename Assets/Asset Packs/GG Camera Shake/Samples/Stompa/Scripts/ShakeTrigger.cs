using UnityEngine;

// Don't forget to add this.
using CameraShake;

public class ShakeTrigger : MonoBehaviour
{
    // Parameters of the shake to tweak in the inspector.
    public BounceShake.Params shakeParams;

    public bool isShaked = true;
    Stability stability;
    Weapon weapon;

    [SerializeField] float shakeTime = 0.3f;

    private void Start()
    {
        stability = GetComponentInParent<Stability>();
        weapon = GetComponent<Weapon>();
    }

    // This is called by animator.
    public void Stomp()
    {
        isShaked = false;
        Vector3 sourcePosition = transform.position;

        // Creating new instance of a shake and registering it in the system.
        CameraShaker.Shake(new BounceShake(shakeParams, sourcePosition));
        Invoke(nameof(Activate), shakeTime);
        StartCoroutine(stability.messUp(weapon.maxUnStab, weapon.incUnStab));
    }
    private void Activate()
    {
        isShaked = true;
    }
}
