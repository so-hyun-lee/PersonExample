using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableViewController : MonoBehaviour //추상클래스:틀만 있는 ???! 
{
    //cell을 생성하기 위한  cell prefab
    [SerializeField] protected TableViewCell cellPrefab;
    //cell을 생성하게 될 content 객체
    [SerializeField] protected Transform content;
    //cell재사용을 위한 queue
    protected Queue<TableViewCell> reuseQueue = new Queue<TableViewCell>();
    //셀간의 연결고리를 관리하기 위한 list
    protected LinkedList<TableViewCell> cellLinkedList = new LinkedList<TableViewCell>();
    //cell의 높이
    private float cellHeight;

    //전체 cell의 개수
    private int totalRows;

    //자식 클래스에서 TableViewController가 표시해야할 전체 data의 수를 반환하는 메서드
    //미래에 자식이 여기에 셀을 몇개 반환할것인지 ?
    protected abstract int numberOfRows();

    //각 index의 cell을 반환하는 메서드
    protected abstract TableViewCell cellForRowAtIndex(int index);

    protected virtual void Start()
    {
        totalRows = this.numberOfRows();
        float screenHeight = Screen.height; //화면 높이
        cellHeight = cellPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //표시해야할 cell의 수 계산
        int maxRows = (int)(screenHeight / cellHeight) + 2; //여유 할당
        maxRows = (maxRows > totalRows) ? totalRows : maxRows;

        //content 크기 조정
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(0, totalRows * cellHeight);

        // 초기 Cell을 생성
        for (int i = 0; i < maxRows; i++)
        {
            TableViewCell cell = cellForRowAtIndex(i);
            cell.gameObject.transform.localPosition = new Vector3(0, -i * cellHeight, 0);
            cellLinkedList.AddLast(cell);

        }
    }
        //재사용이 가능한 cell을 반환해 주는 메서드

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
            //하단에 새로운 cell이 만들어지는 상황

            //처음에 있던 cell은 reuse queue에 저장
            LinkedListNode<TableViewCell> firstCellNode = cellLinkedList.First;
            cellLinkedList.RemoveFirst();
            firstCellNode.Value.gameObject.SetActive(false);
            reuseQueue.Enqueue(firstCellNode.Value);

            //하단에 새로운 cell생성
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
            //상단에 새로운 cell이 만들어지는 상황

            //마지막에 있던 cell은 reuse queue에 저장
            LinkedListNode<TableViewCell> lastCellNode = cellLinkedList.Last;
            cellLinkedList.RemoveLast();
            lastCellNode.Value.gameObject.SetActive(false);
            reuseQueue.Enqueue(lastCellNode.Value);

            //상단에 새로운 cell생성
            LinkedListNode<TableViewCell> firstCellNode = cellLinkedList.First;
            int currentIndex = firstCellNode.Value.Index;
            TableViewCell cell = cellForRowAtIndex(currentIndex - 1);
            cell.gameObject.transform.localPosition = new Vector3(0, -(currentIndex-1) * cellHeight, 0);    
            cellLinkedList.AddBefore(firstCellNode, cell);  
            cell.gameObject.transform.SetAsFirstSibling();
        }
    
    }
}
