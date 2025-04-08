using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Projectile : ProjectileBase
    {
        [SerializeField] private ImpactEffect m_TmpactEffectPrefab; //взрыв при взрыве
        protected override void OnHit(Destructible destructible)
        {
            if(m_Parent==Player.Instance.ActiveShip)
            {
                Player.Instance.AddScore(destructible.ScoreValue);
                if(destructible is SpaceShip)
                {
                    if (destructible.HitPoints <= 0)
                    {
                        Player.Instance.AddKill();
                    }

                    
                }
            }
        }

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pas)
        {
            if(m_TmpactEffectPrefab != null)
            {
               Instantiate(m_TmpactEffectPrefab, pas , Quaternion.identity); 
            }
            Destroy(gameObject, 0);
        }

    }
}

        

        