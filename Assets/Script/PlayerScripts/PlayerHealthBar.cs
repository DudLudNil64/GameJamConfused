using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // sprites to change too -Lud
    [SerializeField] Sprite AliveHeartPREVEIW;
    [SerializeField] Sprite DeadHeart;

    // the hearts for showing health amount -Lud
    [SerializeField] Image Heart1;
    [SerializeField] Image Heart2;
    [SerializeField] Image Heart3;
    [SerializeField] Image Heart4;
    [SerializeField] Image Heart5;
    // Values for script
    PlayerData PlayerData;
    [SerializeField] int HealthPerHeart;
    void Start()
    {
        PlayerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }
    void FixedUpdate()
    {
        int FrameHealth = PlayerData.health;

        // This will only work for lossing health will fix for gaining
        if (FrameHealth < HealthPerHeart)
        {
            Heart1.sprite = DeadHeart;
            Debug.Log("Player should be dead");
        }
        else Heart1.sprite = AliveHeartPREVEIW;

        if (FrameHealth < HealthPerHeart * 2) Heart2.sprite = DeadHeart;
        else Heart2.sprite = AliveHeartPREVEIW;

        if (FrameHealth < HealthPerHeart * 3) Heart3.sprite = DeadHeart;
        else Heart3.sprite = AliveHeartPREVEIW;

        if (FrameHealth < HealthPerHeart * 4) Heart4.sprite = DeadHeart;
        else Heart4.sprite = AliveHeartPREVEIW;

        if (FrameHealth < HealthPerHeart * 5) Heart5.sprite = DeadHeart;
        else Heart5.sprite = AliveHeartPREVEIW;
    }
}
