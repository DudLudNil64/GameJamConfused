using System.Collections;
using UnityEngine;

public class ShotGun : WeaponBase
{
    public override void UseWeapon()
    {
        // delay
        StartCoroutine("StartUseDelay");
        // le gun work
        GameObject CurrentBullet = Instantiate(Bullet);
        // Sets the transform -Lud
        CurrentBullet.transform.position = FirePosition;
        CurrentBullet.transform.rotation = transform.rotation;
        // Gives the tag from the shooter so that you dont shoot yourself -Lud
        Bullet.tag = gameObject.tag;
        Bullet.GetComponent<BulletMoveForward>().damage = Damage;
    }
}