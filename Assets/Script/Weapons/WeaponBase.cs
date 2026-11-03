using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponBase : MonoBehaviour
{
    // Used for fireing -Lud
    public Vector2 FirePosition;
    public Vector2 BulletDirection;
    public GameObject Bullet;

    // Main stats
    public int Damage;
    public int AttackCount;

    // Delay Veriables
    public float Delay;
    public bool InDelay;

    public virtual void UseWeapon()
    {
        GameObject CurrentBullet = Instantiate(Bullet);
        CurrentBullet.transform.position = FirePosition;
    }
}
