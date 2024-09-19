using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControles : MonoBehaviour
{
    [Header("Genel Ayarlar")]
    [Tooltip("Girilen rakama göre kullanıcının gemiyi yönetme hızı.")] [SerializeField] float controlSpeed = 10f;
    [Tooltip("Sağ ve sola doğru geminin dönüşünü girilen rakama göre sınırlandırılması.")] [SerializeField] float xRange = 10f;
    [Tooltip("Yukarıya ve aşşağıya doğru geminin hareketlerinin girilen rakama göre sınırlandırılması.")] [SerializeField] float yRange = 10f;

    [Header("Lazer dizisi.")]
    [Tooltip("Tüm lazerleri buraya ekleyin.")]
    [SerializeField] GameObject[] lasers;

    [Header("Ayarlara göre ekran pozisyonu.")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor=2;

    [Header("Ayarlara göre oyuncu inputları.")]
    [SerializeField] float controlRollFactor=-20f;
    [SerializeField] float controlPitchFactor = -10f;
    // Update is called once per frame

    float xThrow, yThrow;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow*controlPitchFactor;

        //pitch anlam = aşağıda ilk part position impacting pitch ikinci part control input impacting pitch
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x  + positionYawFactor;
        float roll= xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
        (clampedXPos,
         clampedYpos,
         transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

   
}
