using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxhealth = 100;
    public int health = 90;
    public bool hasKey = false;

    public void GiveHealth(int amount)
    {
       if (maxhealth > 0 && health < maxhealth)
        {
            health += amount;
        }
    }

    public void GiveKey()
    {
        hasKey = true;
    }
}
