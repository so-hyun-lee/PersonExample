using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableViewController : MonoBehaviour //�߻�Ŭ����:Ʋ�� �ִ� ???! 
{
    //cell�� �����ϱ� ����  cell prefab
    [SerializeField] protected TableViewCell cellPrefab;
    //cell�� �����ϰ� �� content ��ü
    [SerializeField] protected Transform content;

    protected Queue<TableViewCell> reuseQueue = new Queue<TableViewCell>();

    //cell�� ����
    private float cellHeight;

    //��ü cell�� ����
    private int totalRows;

    //�ڽ� Ŭ�������� TableViewController�� ǥ���ؾ��� ��ü data�� ���� ��ȯ�ϴ� �޼���
    //�̷��� �ڽ��� ���⿡ ���� � ��ȯ�Ұ����� ?
    protected abstract int numberOfRows();

    //�� index�� cell�� ��ȯ�ϴ� �޼���
    protected abstract TableViewCell cellForRowAtIndex(int index);

    private void Start()
    {
        totalRows = this.numberOfRows(); 
        float screenHeight = Screen.height; //ȭ�� ����
        cellHeight = cellPrefab.GetComponent<RectTransform>().sizeDelta.y;

        //ǥ���ؾ��� cell�� �� ���
        int maxRows = (int)(screenHeight/cellHeight)+2; //���� �Ҵ�
        maxRows = (maxRows > totalRows) ? totalRows : maxRows;
    
        //content ũ�� ����
        RectTransform contentTransform = content.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, totalRows * cellHeight);


    }
}
