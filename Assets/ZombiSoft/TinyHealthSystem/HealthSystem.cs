//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public static HealthSystem Instance;

	public Image currentHealthBar;
	public Image currentHealthGlobe;
	public Text healthText;
	public float hitPoint = 100f;
	public float maxHitPoint = 100f;

	public Image currentManaBar;
	public Image currentManaGlobe;
	public Text manaText;
	public float manaPoint = 100f;
	public float maxManaPoint = 100f;
	public GameObject gameover;
	public AudioSource audioSource;
	public AudioClip damage;
	public AudioClip lose;
	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	public bool Regenerate = true;

	private float timeleft = 0.0f;  // Left time for current interval
	public float regenUpdateInterval = 1f;

	public bool GodMode;
	//static float remeiang;
    //==============================================================
    // Awake
    //==============================================================
    void Awake()
	{
		Instance = this;
	}

	//==============================================================
	// Awake
	//==============================================================
	void Start()
	{
		UpdateGraphics();
		timeleft = regenUpdateInterval;
		
		gameover.SetActive(false);
		audioSource = GetComponent<AudioSource>();
	}

	//==============================================================
	// Update
	//==============================================================


	//==============================================================
	// Regenerate Health & Mana
	//==============================================================


	//==============================================================
	// Health Logic
	//==============================================================
	private void UpdateHealthBar()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthBar.rectTransform.localPosition = new Vector3(currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width, 0, 0);
		healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
	}

	private void UpdateHealthGlobe()
	{
		float ratio = hitPoint / maxHitPoint;
		currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
		healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");
	}

	public void TakeDamage(float Damage)
	{
		
		hitPoint -= Damage;
        UpdateGraphics();
		audioSource.PlayOneShot(damage);
        if (hitPoint < 1)
		{
            audioSource.PlayOneShot(lose);
            gameover.SetActive(true);
			StartCoroutine(PauseAfterAnimation());
        }


		


	}
	public float Gethealthremaining()
	{


		return hitPoint;
	}

	public void HealDamage(float Heal)
	{
		hitPoint += Heal;
		if (hitPoint > maxHitPoint)
			hitPoint = maxHitPoint;

		UpdateGraphics();
	}
	public void SetMaxHealth(float max)
	{
		maxHitPoint += (int)(maxHitPoint * max / 100);

		UpdateGraphics();
	}

	//==============================================================
	// Mana Logic
	//==============================================================
	private void UpdateManaBar()
	{
		float ratio = manaPoint / maxManaPoint;
		currentManaBar.rectTransform.localPosition = new Vector3(currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width, 0, 0);
		manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
	}

	private void UpdateManaGlobe()
	{
		float ratio = manaPoint / maxManaPoint;
		currentManaGlobe.rectTransform.localPosition = new Vector3(0, currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height, 0);
		manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
	}

	public void UseMana(float Mana)
	{
		manaPoint -= Mana;
		if (manaPoint < 1) // Mana is Zero!!
			manaPoint = 0;

		UpdateGraphics();
	}

	public void RestoreMana(float Mana)
	{
		manaPoint += Mana;
		if (manaPoint > maxManaPoint)
			manaPoint = maxManaPoint;

		UpdateGraphics();
	}
	public void SetMaxMana(float max)
	{
		maxManaPoint += (int)(maxManaPoint * max / 100);

		UpdateGraphics();
	}

	//==============================================================
	// Update all Bars & Globes UI graphics
	//==============================================================
	private void UpdateGraphics()
	{
		UpdateHealthBar();
		UpdateHealthGlobe();
		UpdateManaBar();
		UpdateManaGlobe();
	}

    //==============================================================
    // Coroutine Player Hurts
    //==============================================================


    //==============================================================
    // Hero is dead
    //==============================================================
    
    IEnumerator PauseAfterAnimation()
    {
        // Wait in real time, NOT affected by timeScale
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0; // Pause AFTER animation completes
    }
}
