using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlot
    {
        public GameObject itemPrefab;
        public int currCount;
        public int maxCount;
    }

    [Header("About Item..")]
    [SerializeField] private List<ItemSlot> _items;
    [SerializeField] private List<Text> _itemCountTexts;
    private GameObject _selectedItem;
    private int _selectedIndex;
    private bool isPointerDown = false;

    public void PointerDown_Item(int itemIndex)
    {
        //사용할 아이템 등록
        _selectedIndex = itemIndex;

        if (_items[_selectedIndex].currCount <= 0)
        {
            _selectedIndex = -1;
            _selectedItem = null;

            return;
        }

        _selectedItem = Instantiate(_items[_selectedIndex].itemPrefab);
        _selectedItem.GetComponent<Collider2D>().enabled = false;
        isPointerDown = true;

        StartCoroutine(IE_TrackingItem_MousePointer());
    }
    
    public void PointerUp_Item()
    {
        if (_selectedItem == null) return;
        isPointerDown = false;

        //아이템 UI 위에 커서가 있을 경우 취소(사용할 아이템 등록 해제)
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Destroy(_selectedItem);
            _selectedItem = null;

            return;
        }

        //그 외 설치
        _selectedItem.GetComponent<Obstacle>().Release_Object();

        _items[_selectedIndex].currCount--;
        _itemCountTexts[_selectedIndex].text = "x" + _items[_selectedIndex].currCount;
    }

    private IEnumerator IE_TrackingItem_MousePointer()
    {
        while(isPointerDown)
        {
            if (_selectedItem == null) yield break;

            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _selectedItem.transform.position = new Vector2(world.x, world.y);
            yield return null;
        }
    }
}
