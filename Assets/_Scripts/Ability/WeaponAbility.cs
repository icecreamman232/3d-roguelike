using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    [CreateAssetMenu(menuName = "SGGames/Abilities/Weapon Ability")]
    public class WeaponAbility : AbilityBase
    {
        [Header("Weapon Settings")] 
        [SerializeField] protected GameObject m_weaponPrefab;

        protected Transform m_attachment;
        protected PlayerWeapon m_weaponRef;
        
        public virtual void AssignWeaponAttachment(Transform attachment)
        {
            m_attachment = attachment;
        }
        
        public override void OnEquipAbility()
        {
            base.OnEquipAbility();
            var weaponObj = Instantiate(m_weaponPrefab,m_attachment);
            m_weaponRef = weaponObj.GetComponent<PlayerWeapon>();
        }
        protected override bool CanUseAbility()
        {
            if (m_weaponRef == null) return false;
            
            return m_weaponRef.CanShoot();
        }

        protected override void UseAbility()
        {
            base.UseAbility();
            m_weaponRef.UseWeapon();
        }

        public override void CleanUp()
        {
            base.CleanUp();
            m_attachment = null;
            m_weaponRef = null;
        }
    }
}

