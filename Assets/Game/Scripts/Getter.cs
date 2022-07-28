using UnityEngine;
using UnityEngine.Events;

namespace SortItems
{
    public class Getter : MonoBehaviour
    {
        [SerializeField] private ItemType type;
        private DragItem _item;
        private Material _material;
        private Color _defaultColor;

        private int targetCount = 1;
        private int count = 0;

        private bool active = true;

        public UnityEvent<Getter> onCountChanged;

        public void SetCount(int value)
        {
            targetCount = value;

            if (count >= targetCount)
                {
                    _material.color = Color.gray;
                    active = false;                    
                }

        }


        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
            _defaultColor = _material.color;
        }


        private void OnTriggerStay(Collider other)
        {
            if(!active)
                return;

            var item = other.attachedRigidbody.GetComponent<DragItem>();

            if (item != null && item.isDraggable == true)
            {
                _item = item;

                if (_item.Type == type)
                {
                    _material.color = Color.green;
                }
                else
                {
                    _material.color = Color.red;
                }
                return;
            }

            if (item.isDraggable == false && _item == item)
            {
                TryGetItem();
                _item = null;
                _material.color = _defaultColor;

                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(!active)
                return;

            var item = other.attachedRigidbody.GetComponent<DragItem>();

            if (_item == item)
            {
                _material.color = _defaultColor;

                if (item.isDraggable == false)
                    TryGetItem();

                _item = null;
                
            }
        }


        private void TryGetItem()
        {
            if (_item.Type == type)
            {
                Destroy(_item.gameObject);
                count++;

                onCountChanged.Invoke(this);

                if (count >= targetCount)
                {
                    _material.color = Color.gray;
                    active = false;
                    //Destroy(this);
                }

            }
                
        }
    }
}
