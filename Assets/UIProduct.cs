using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Prismnova
{
    public class UIProduct : MonoBehaviour, IPointerClickHandler
    {
        public string Name;
        public WeaponProduct Product;
        public WeaponProductEvent onClick;
        public SimpleEvent onLock;
        // Use this for initialization
        void Start()
        {
            if (string.IsNullOrEmpty(Name))
                Name = name;
            Product = ProductManager.Instance.GetProduct(Name);
            var btn = GetComponent<Button>();
            if (Product == null)
            {
                Debug.LogError("Can not find name:" + Name);
                return;
            }
            if (btn != null)
                btn.interactable = Product.unlock;
            if (!Product.unlock && onLock != null)
                onLock.Invoke();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null && Product.unlock)
                onClick.Invoke(Product);
        }
    }
}