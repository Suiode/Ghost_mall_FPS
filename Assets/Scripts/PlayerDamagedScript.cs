using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedScript : MonoBehaviour
{
    [SerializeField] Material handMaterial;
    [SerializeField] private Texture fullHealthTexture;
    [SerializeField] private Texture theeQuartsHealthTexture;
    [SerializeField] private Texture halfHealthTexture;
    [SerializeField] private Texture quarterHealthTexture;
    [SerializeField] private float damageFlashTime = 0.25f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Shader damageFlashShader;
    public Renderer rend;


    void Start()
    {
        handMaterial.mainTexture = fullHealthTexture;
        rend = GetComponent<Renderer>();
    }


    //Called by the Player Controller, takes in health and changes the texture according to how much health is left
    public void PlayerDamageVisualUpdate(float health)
    {



        if (health <= (maxHealth / 4))
        {
            //rend.material.mainTexture = quarterHealthTexture;
            handMaterial.mainTexture = quarterHealthTexture;
        }
        else if (health <= (maxHealth / 2))
        {
            //rend.material.mainTexture = halfHealthTexture;
            handMaterial.mainTexture = halfHealthTexture;
        }
        else if (health <= ((maxHealth / 4) * 3))
        {
            //rend.material.mainTexture = theeQuartsHealthTexture;
            handMaterial.mainTexture = theeQuartsHealthTexture;
        }


        StartCoroutine(PlayerDamageFlash(handMaterial.mainTexture));
    }

    public IEnumerator PlayerDamageFlash(Texture newTexture)
    {
        float timer = damageFlashTime;



        while (timer >= 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            //handMaterial.SetColor("_Color", Color.Lerp(damageColor, defaultColor, (timer / damageFlashTime)));
            rend.material.SetFloat("_Timer", timer);
            rend.material.SetTexture("_HandTexture", newTexture);
        }
    }
}
