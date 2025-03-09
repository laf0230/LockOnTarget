using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Vector2Int inventorySize;
    // Vector2Int를 이용해서 인벤토리 구성하기
    [SerializeField] public List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        // Create Slot
        for(int i = 0; i < inventorySize.x; i++)
        {
            for (int j = 0; j < inventorySize.y; j++)
            {
                slots.Add(
                    new InventorySlot(
                        new Vector2Int(i, j)
                    ));
            }
        }
    }

    private void AddItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if(slot.data == item)
            {
                // Todo: 중복되는 아이템일 경우 개수 추가
                slot.data.Amount++;
                break;
            }

            if(slot.data != null)
            {
                // Todo: 새로 추가되는 아이템일 경우 빈 슬롯에 추가
                slot.SetData(item);
                break;
            }
        }
    }

    private void RemoveItem(ItemData item)
    {
        foreach(var slot in slots)
        {
            if(slot.data == item)
            {
                // Remove Item
                // 장단점,,,,
            }
        }
    }
}

[System.Serializable]
public struct InventorySlot
{
    public ItemData data;
    public Vector2Int position;

    public void EmptySlot()
    {
        data = null;
    }

    public void SetData(ItemData data)
    {
        this.data = data;
    }

    public InventorySlot(Vector2Int position, ItemData data = null)
    {
        this.data = data;
        this.position = position;
    }
}
