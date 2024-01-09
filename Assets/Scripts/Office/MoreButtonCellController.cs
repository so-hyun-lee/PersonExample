using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void LoadMoreData();

public class MoreButtonCellController : MonoBehaviour
{
    public Button button;
    public LoadMoreData loadMoreData;


    private void Start()
    {
        button = GetComponentInChildren<Button>(); //getcomponent�� �ݺ������� ��� �����ϱ�
    }
    public void OnClickMoreButton()
    {
        button.interactable = false;
        if(loadMoreData != null)
        {
            loadMoreData.Invoke();
        }
    }
}
