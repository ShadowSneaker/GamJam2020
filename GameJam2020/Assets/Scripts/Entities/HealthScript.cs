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


    [Tooltip("")]
    [SerializeField]
    private bool Invincible = true;

    [Tooltip("")]
    [SerializeField]
    private bool Dead = false;

    [Tooltip("")]
    [SerializeField]
    private float Health = 100.0f;

    [Tooltip("")]
    [SerializeField]
    private float MaxHealth = 100.0f;


    [Header("Events")]


    [Tooltip("")]
    [SerializeField]
    private UnityEvent OnHurt = null;

    [Tooltip("")]
    [SerializeField]
    private UnityEvent OnDeath = null;




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


            if (Dead)
            {
                Info.OverkillDamage = Health * -1;

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
}
