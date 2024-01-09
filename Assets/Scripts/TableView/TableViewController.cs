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
    //자식 클래스에서 TableViewController가 표시해야할 전체 data의 수를 반환하는 메서드
    //미래에 자식이 여기에 셀을 몇개 반환할것인지 ?
    protected abstract int numberOfRows();
    //각 index의 cell을 반환하는 메서드
    protected abstract TableViewCell cellForRowAtIndex(int index);
}
