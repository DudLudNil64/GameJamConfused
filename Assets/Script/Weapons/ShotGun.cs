using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShotGun : WeaponBase
{
    public override void UseWeapon()
    {
        // le gun work
        GameObject CurrentBullet = Instantiate(Bullet);
        // Gives the tag from the shooter so that you dont shoot yourself -Lud
        CurrentBullet.tag = tag.ToString();
        // Sets the transform -Lud
        CurrentBullet.transform.position = FirePosition;
        CurrentBullet.transform.rotation = transform.rotation;

        CurrentBullet.GetComponent<BulletMoveForward>().damage = Damage;

        // delay
        StartCoroutine("StartUseDelay");
    }
}