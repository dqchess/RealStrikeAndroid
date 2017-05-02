using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
namespace Prismnova
{
    public class IAPProduct : MonoBehaviour, IPointerClickHandler
    {
        public string Id;
        void Start() {
            var purchaser = FindObjectOfType<Purchaser>();
            purchaser.onProductChanged.AddListener(UpState);
            if (string.IsNullOrEmpty(Id))
                Id = gameObject.name;
            UpState(Id);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            var purchaser = FindObjectOfType<Purchaser>();
            purchaser.SetActiveProduct(Id);

        }
        void OnDestory() {
            var purchaser = FindObjectOfType<Purchaser>();
            purchaser.onProductChanged.RemoveListener(UpState);
        }
        void UpState(string id)
        {
            if (!id.EndsWith(Id)) 
                return;
            bool bl = ProductManager.Instance.IsHaving(Id);
            transform.Find("Purchased").gameObject.SetActive(bl);
            transform.Find("Text").gameObject.SetActive(!bl);
        }
    }
    [Serializable]
    public class IAPProductPackage {
        public string id;
        public UnityEngine.Purchasing.ProductType type = UnityEngine.Purchasing.ProductType.NonConsumable;
        public string[] Names;
    }
}
