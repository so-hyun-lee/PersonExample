using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableViewController : MonoBehaviour //추상클래스:틀만 있는 ???! 
{
    //cell을 생성하기 위한  cell prefab
    [SerializeField] protected TableViewCell cellPrefab;
    //cell을 생성하게 될 content 객체
    [SerializeField] protected Transform content;

    protected Queue<TableViewCell> reuseQueue = new Queue<TableViewCell>();

    //cell의 높이
    private float cellHeight;

    //전체 cell의 개수
    private int totalRows;

    //자식 클래스에서 TableViewController가 표시해야할 전체 data의 수를 반환하는 메서드
    //미래에 자식이 여기에 셀을 몇개 반환할것인지 ?
    protected abstract int numberOfRows();

    //각 index의 cell을 반환하는 메서드
    protected abstract TableViewCell cellForRowAtIndex(int index);

    private void Start()
    {
        totalRows = this.numberOfRows(); 
        float screenHeight = Screen.height; //화면 높이
        cellHeight = cellPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //표시해야할 cell의 수 계산
        int maxRows = (int)(screenHeight/cellHeight)+2; //여유 할당
        maxRows = (maxRows > totalRows) ? totalRows : maxRows;
    
        //content 크기 조정
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, totalRows * cellHeight);


    }
}
