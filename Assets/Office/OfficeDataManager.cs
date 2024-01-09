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
    
    //�ҷ��� ��� ���ǽ� ����Ʈ
    private List<Office> officeList = new List<Office>();

    private int currentPage = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadData(currentPage));
    }


    IEnumerator LoadData(int page, GameObject previousMoreButton = null)
    {
        //���� url����
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
                    officeCellController.SetData(officeArray[i].�繫�Ҹ�, officeArray[i].��������, officeArray[i].��ȭ��ȣ, officeList.Count-1);

                    officeCellController.cellDelegate = this; //�����͸Ŵ������� delegate�� ����� ���� ���
                }

                //morebutton����
                if (previousMoreButton != null) Destroy(previousMoreButton);

                //morebutton�߰�
                int currentTotalCount = officeData.perPage * (officeData.perPage-1) + officeData.currentCount;
                if(currentTotalCount < officeData.totalCount) //���� �ҷ��� ������ ��ü �������� ���ٸ�
                {
                    MoreButtonCellController moreButtonCellController = Instantiate(moreButtonPrefab, content);
                    moreButtonCellController.loadMoreData = (/*�Լ��Ű����� �ް����ϴ� �Լ��� �������*/) =>
                    {                     
                        StartCoroutine(LoadData(officeData.page+1));
                    };
                }
                
            }



        }
    }

    public void OnClickCell(int index) //cell�� ȣ������
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
    /// +��ư Ŭ���ÿ� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickAddButton()
    {

       
        CreatePanelController createPanelController= Instantiate(createPanelPrefab, canvas);
       ///////////////////////////////////////////////
       // //1.delegate

       // // createPanelController.createData= new CreateData(saveData);

       ////���ٽ��� ����� ��� �Ű������� �Ҵ�� ������ Ÿ���� �������ֱ⶧���� "Office"������ �ص���
       // createPanelController.createData = (office) =>
       // {
       //     officeList.Add(office);

       //     OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
       //     officeCellController.SetData(office.�繫�Ҹ�, office.��������, office.��ȭ��ȣ, officeList.Count - 1);

       //     officeCellController.cellDelegate = this;
       //     officeCellController.transform.SetSiblingIndex(officeList.Count - 1); //������ ��ư ���� �������� ����� ��
       //     //reloadData();
       // };
       //////////////////////////////////////////

        //2.Action
        createPanelController.createDataAction = (office) =>
        {
            officeList.Add(office);

            OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
            officeCellController.SetData(office.�繫�Ҹ�, office.��������, office.��ȭ��ȣ, officeList.Count - 1);

            officeCellController.cellDelegate = this;
            officeCellController.transform.SetSiblingIndex(officeList.Count - 1); //������ ��ư ���� �������� ����� ��
            //reloadData();
        };

    }

    public void saveData(Office office)
    {
        //CreatepPanelController���� ���޹��� office��ü�� ��ü����Ʈ�� ���� officeList�� �߰�
        //officeList.Add(office);
        officeList.Insert(0, office);

        //����Ʈ ����
        reloadData();
    }

    private void reloadData()

    {
        //��� �� �����
        foreach(Transform transform in content.GetComponent<Transform>())
        {
            Destroy(transform.gameObject);
        }

        //�ٽ� officeList�� ���� cell�� ����
        for(int i = 0; i < officeList.Count; i++)
        {
            OfficeCellController officeCellController = Instantiate(officeCellPrefab, content);
            officeCellController.SetData(officeList[i].�繫�Ҹ�, officeList[i].��������, officeList[i].��ȭ��ȣ, i);

            officeCellController.cellDelegate = this;
            
        }

        //������ ��ư ����

    }
}
