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
    //�ڽ� Ŭ�������� TableViewController�� ǥ���ؾ��� ��ü data�� ���� ��ȯ�ϴ� �޼���
    //�̷��� �ڽ��� ���⿡ ���� � ��ȯ�Ұ����� ?
    protected abstract int numberOfRows();
    //�� index�� cell�� ��ȯ�ϴ� �޼���
    protected abstract TableViewCell cellForRowAtIndex(int index);
}
