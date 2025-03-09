using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Vector2Int inventorySize;
    // Vector2Int�� �̿��ؼ� �κ��丮 �����ϱ�
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
                // Todo: �ߺ��Ǵ� �������� ��� ���� �߰�
                slot.data.Amount++;
                break;
            }

            if(slot.data != null)
            {
                // Todo: ���� �߰��Ǵ� �������� ��� �� ���Կ� �߰�
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
                // �����,,,,
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
