using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OfficeDataManager : MonoBehaviour , ICellDelegate
{ 
    [SerializeField] private string serviceKey;
    [SerializeField] private OfficeCellController officeCellPrefab;
    [SerializeField] private MoreButtonCellController moreButtonPrefab;
    [SerializeField] private DetailPanelController detailPanelPrefab;
    [SerializeField] private CreatePanelController createPanelPrefab;
    [SerializeField] private UpdatePanelController updatePanelPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Transform canvas;
    
    //불러온 모든 오피스 리스트
    private List<Office> officeList = new List<Office>();

    private int currentPage = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadData(currentPage));
    }


    IEnumerator LoadData(int page, GameObject previousMoreButton = null)
    {
        //서버 url설정
        string url = string.Format("{0}?page={1}&perPage={2}&serviceKey={3}",Constants.url, page, Constants.perPage, serviceKey);
        UnityWebRequest request = new UnityWebRequest();
        using (request= UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string result = request.downloadHandler.text;

                OfficeData officeData = JsonUtility.FromJson<OfficeData>(result);

                Office[] officeArray = officeData.data;

                for(int i = 0; i < officeArray.Length; i++)
                {
                    officeList.Add(officeArray[i]);

                    OfficeCellController officeCellController= Instantiate(officeCellPrefab, content);
                    officeCellController.SetData(officeArray[i].사무소명, officeArray[i].영업구분, officeArray[i].전화번호, officeList.Count-1);

                    officeCellController.cellDelegate = this; //데이터매니저에게 delegate가 나라는 것을 명시
                }

                //morebutton제거
                if (previousMoreButton != null) Destroy(previousMoreButton);

                //morebutton추가
                int currentTotalCount = officeData.perPage * (officeData.perPage-1) + officeData.currentCount;
                if(currentTotalCount < officeData.totalCount) //현재 불러온 개수가 전체 개수보다 적다면
                {
                    MoreButtonCellController moreButtonCellController = Instantiate(moreButtonPrefab, content);
                    moreButtonCellController.loadMoreData = (/*함수매개변수 받고자하는 함수에 맞춰야함*/) =>
                    {                     
                        StartCoroutine(LoadData(officeData.page+1));
                    };
                }
                
            }



        }
    }

    public void OnClickCell(int index) //cell이 호출해줌
    {
        //DetailPanelController detailPanelController = Instantiate(detailPanelPrefab, canvas);
        //detailPanelController.SetData(officeList[index]);

        UpdatePanelController updatePanelController = Instantiate(updatePanelPrefab, canvas);
        updatePanelController.SetData(officeList[index],index);
        updatePanelController.updateData = (office, updateindex) => 
        {
            officeList[updateindex] = office;
            reloadData();
        };
    }
    /// <summary>
    /// +버튼 클릭시에 호출되는 메서드
    /// </summary>
    public void OnClickAddButton()
    {

       
        CreatePanelController createPanelController= Instantiate(createPanelPrefab, canvas);
       ///////////////////////////////////////////////
       // //1.delegate

       // // createPanelController.createData= new CreateData(saveData);

       ////람다식을 사용한 방법 매개변수가 할당될 변수의 타입이 정해져있기때문에 "Office"생략을 해도됨
       // createPanelController.createData = (office) =>
       // {
       //     officeList.Add(office);

       //     OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
       //     officeCellController.SetData(office.사무소명, office.영업구분, office.전화번호, officeList.Count - 1);

       //     officeCellController.cellDelegate = this;
       //     officeCellController.transform.SetSiblingIndex(officeList.Count - 1); //더보기 버튼 전에 나오도록 만드는 것
       //     //reloadData();
       // };
       //////////////////////////////////////////

        //2.Action
        createPanelController.createDataAction = (office) =>
        {
            officeList.Add(office);

            OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
            officeCellController.SetData(office.사무소명, office.영업구분, office.전화번호, officeList.Count - 1);

            officeCellController.cellDelegate = this;
            officeCellController.transform.SetSiblingIndex(officeList.Count - 1); //더보기 버튼 전에 나오도록 만드는 것
            //reloadData();
        };

    }

    public void saveData(Office office)
    {
        //CreatepPanelController에서 전달받은 office객체를 전체리스트를 가진 officeList에 추가
        //officeList.Add(office);
        officeList.Insert(0, office);

        //리스트 갱신
        reloadData();
    }

    private void reloadData()

    {
        //모든 셀 지우기
        foreach(Transform transform in content.GetComponent<Transform>())
        {
            Destroy(transform.gameObject);
        }

        //다시 officeList의 값을 cell로 생성
        for(int i = 0; i < officeList.Count; i++)
        {
            OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
            officeCellController.SetData(officeList[i].사무소명, officeList[i].영업구분, officeList[i].전화번호, i);

            officeCellController.cellDelegate = this;
            
        }

        //더보기 버튼 생성

    }
}
