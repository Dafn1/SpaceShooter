using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public abstract class ProjectileBase : Entity //снаряд
    {
        [SerializeField] private float m_Velocity; //скорость снаряда

        [SerializeField] private float m_Lifetime; //время жизни снаряда

        [SerializeField] private int m_Damage; //урон

        

        protected virtual void OnHit(Destructible destructible) { }
        
        protected virtual void OnHit(Collider2D collider2) { }
        
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pas) { }
       

        private float m_Timer; //отвечает за время жизни снаряда
        protected Destructible m_Parent;

        private void Update() //каждый кадр 
        {
            float stepLength = Time.deltaTime * m_Velocity; //хранит смещение которое будет смещать снаряд каждый кадр
            Vector2 step = transform.up * stepLength; //шаг равняется направлению снаряда вверх

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                OnHit(hit.collider);
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent) //чтобы не стрелять в самого себя
                {
                    dest.ApplyDamage(m_Damage);

                    OnHit(dest);
                    
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
               
            }

            m_Timer += Time.deltaTime; //таймер жизни снаряда.

            if (m_Timer > m_Lifetime) //если он живет больше чем нужно,то
                OnProjectileLifeEnd(hit.collider, hit.point); //уничтожение снаряда

            transform.position += new Vector3(step.x, step.y, 0); //движение снаряда с напрвалением
        }

      

       

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

    }
}



