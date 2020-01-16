using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [System.Serializable]
    public struct SDamageInfo
    {
        public bool Killed;
        public float DamageDealt;
        public float OverkillDamage;
        public float RemainingHealth;
    }


    [Tooltip("Prevents this object from taking any damage.")]
    [SerializeField]
    private bool Invincible = false;

    [Tooltip("Represents if this object is dead.")]
    [SerializeField]
    private bool Dead = false;

    [Tooltip("The current amount of health this object has.")]
    [SerializeField]
    private float Health = 100.0f;

    [Tooltip("The total health this object can have.")]
    [SerializeField]
    private float MaxHealth = 100.0f;


    [Header("Particles")]

    [Tooltip("The particle prefab that is played when this object takes damage.")]
    [SerializeField]
    private ParticleSystem HurtParticle = null;

    private ParticleSystem HurtParticleInst = null;


    [Header("Events")]


    [Tooltip("Runs when this object is hurt.")]
    [SerializeField]
    private UnityEvent OnHurt = null;

    [Tooltip("Runs when this object is killed.")]
    [SerializeField]
    private UnityEvent OnDeath = null;


    [Tooltip("Runs when this object is healed.")]
    [SerializeField]
    private UnityEvent OnHeal = null;

    [Tooltip("what sounds you want to use ")]
    [SerializeField]
    private SoundScript DeathSound = null;

    public SDamageInfo ApplyDamage(float Amount)
    {
        SDamageInfo Info = new SDamageInfo();
        if (!Dead && !Invincible && Amount != 0.0f)
        {
            Health -= Amount;
            Dead = (Health <= 0.0f);

            Info.DamageDealt = Amount;
            Info.Killed = Dead;
            Info.RemainingHealth = (Health > 0.0f) ? Health : 0.0f;

            if (HurtParticle)
            {
                HurtParticleInst = Instantiate(HurtParticle, transform.position, Quaternion.identity);
                float Percent = (Amount / MaxHealth) + HurtParticleInst.main.startSize.constant;
                HurtParticleInst.transform.localScale = new Vector3(Percent, Percent, Percent);
                Destroy(HurtParticleInst, HurtParticleInst.main.duration + HurtParticleInst.main.startLifetime.constant);
            }

            if (Dead)
            {
                Info.OverkillDamage = Health * -1;
                DeathSound.Play();

                if (OnDeath != null)
                {
                    OnDeath.Invoke();
                }
            }
            else
            {
                if (OnHurt != null)
                {
                    OnHurt.Invoke();
                }
            }
        }
        return Info;
    }


    public void ApplyHeal(float Amount)
    {
        if (!Dead && Amount != 0.0f)
        {
            Health += Amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            if (OnHeal != null)
            {
                OnHeal.Invoke();
            }
        }
    }


    public float GetCurrent()
    {
        return Health;
    }


    public void SetCurrent(float Amount)
    {
        Health = Amount;
    }


    public float GetMax()
    {
        return MaxHealth;
    }
}
