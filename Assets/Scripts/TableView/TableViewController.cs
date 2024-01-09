using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableViewController : MonoBehaviour //�߻�Ŭ����:Ʋ�� �ִ� ???! 
{
    //cell�� �����ϱ� ����  cell prefab
    [SerializeField] protected TableViewCell cellPrefab;
    //cell�� �����ϰ� �� content ��ü
    [SerializeField] protected Transform content;
    //cell������ ���� queue
    protected Queue<TableViewCell> reuseQueue = new Queue<TableViewCell>();
    //������ ������� �����ϱ� ���� list
    protected LinkedList<TableViewCell> cellLinkedList = new LinkedList<TableViewCell>();
    //cell�� ����
    private float cellHeight;

    //��ü cell�� ����
    private int totalRows;

    //�ڽ� Ŭ�������� TableViewController�� ǥ���ؾ��� ��ü data�� ���� ��ȯ�ϴ� �޼���
    //�̷��� �ڽ��� ���⿡ ���� � ��ȯ�Ұ����� ?
    protected abstract int numberOfRows();

    //�� index�� cell�� ��ȯ�ϴ� �޼���
    protected abstract TableViewCell cellForRowAtIndex(int index);

    protected virtual void Start()
    {
        totalRows = this.numberOfRows();
        float screenHeight = Screen.height; //ȭ�� ����
        cellHeight = cellPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //ǥ���ؾ��� cell�� �� ���
        int maxRows = (int)(screenHeight / cellHeight) + 2; //���� �Ҵ�
        maxRows = (maxRows > totalRows) ? totalRows : maxRows;

        //content ũ�� ����
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(0, totalRows * cellHeight);

        // �ʱ� Cell�� ����
        for (int i = 0; i < maxRows; i++)
        {
            TableViewCell cell = cellForRowAtIndex(i);
            cell.gameObject.transform.localPosition = new Vector3(0, -i * cellHeight, 0);
            cellLinkedList.AddLast(cell);

        }
    }
        //������ ������ cell�� ��ȯ�� �ִ� �޼���

        protected TableViewCell dequeueReuseableCell()
    {
        if (reuseQueue.Count > 0)
        {
            TableViewCell cell = reuseQueue.Dequeue();
            cell.gameObject.SetActive(true);
            return cell;
        }
        else
        {
            return null;
        }
    }

    public void OnValueChanged(Vector2 vector)
    {
        Debug.Log($"Screen.height:{Screen.height}, content.localPosition.y:{content.localPosition.y}");
    
        if((cellLinkedList.Last.Value.Index < totalRows -1 ) && 
            (content.localPosition.y + Screen.height > cellLinkedList.Last.Value.Index * cellHeight + cellHeight))
        {
            //�ϴܿ� ���ο� cell�� ��������� ��Ȳ

            //ó���� �ִ� cell�� reuse queue�� ����
            LinkedListNode<TableViewCell> firstCellNode = cellLinkedList.First;
            cellLinkedList.RemoveFirst();
            firstCellNode.Value.gameObject.SetActive(false);
            reuseQueue.Enqueue(firstCellNode.Value);

            //�ϴܿ� ���ο� cell����
            LinkedListNode<TableViewCell> lastCellNode = cellLinkedList.Last;
            int currentIndex = lastCellNode.Value.Index;
            TableViewCell cell = cellForRowAtIndex(currentIndex + 1);
            cell.gameObject.transform.localPosition = new Vector3(0, -(currentIndex+1) * cellHeight, 0);
            cellLinkedList.AddAfter(lastCellNode, cell);
            cell.gameObject.transform.SetAsLastSibling();
        }
        else if ((cellLinkedList.First.Value.Index > 0) && 
            (content.localPosition.y < cellLinkedList.First.Value.Index * cellHeight))
        {
            //��ܿ� ���ο� cell�� ��������� ��Ȳ

            //�������� �ִ� cell�� reuse queue�� ����
            LinkedListNode<TableViewCell> lastCellNode = cellLinkedList.Last;
            cellLinkedList.RemoveLast();
            lastCellNode.Value.gameObject.SetActive(false);
            reuseQueue.Enqueue(lastCellNode.Value);

            //��ܿ� ���ο� cell����
            LinkedListNode<TableViewCell> firstCellNode = cellLinkedList.First;
            int currentIndex = firstCellNode.Value.Index;
            TableViewCell cell = cellForRowAtIndex(currentIndex - 1);
            cell.gameObject.transform.localPosition = new Vector3(0, -(currentIndex-1) * cellHeight, 0);    
            cellLinkedList.AddBefore(firstCellNode, cell);  
            cell.gameObject.transform.SetAsFirstSibling();
        }
    
    }
}
