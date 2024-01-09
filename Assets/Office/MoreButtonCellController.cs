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
        button = GetComponentInChildren<Button>(); //getcomponent는 반복적으로 사용 지양하기
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
