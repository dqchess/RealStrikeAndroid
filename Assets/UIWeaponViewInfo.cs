using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using I2.Loc;
namespace Prismnova
{
    public class UIWeaponViewInfo : MonoBehaviour
    {
        public WeaponProduct product;
        public Localize PreView;
        public Localize Desc;
        // Use this for initialization
        void Start()
        {

        }

        public void SwitchWeapon(WeaponProduct product)
        {
            PreView.SetTerm("Specs/" + product.Path);
            Desc.SetTerm("Desc/" + product.Path);
            this.product = product;
        }
        public void ApplyWeapon()
        {
            var armoryBhvr = FindObjectOfType<ArmoryBhvr>();
            if (armoryBhvr != null && product != null)
                armoryBhvr.SwitchWeapon(product);
        }
    }
}