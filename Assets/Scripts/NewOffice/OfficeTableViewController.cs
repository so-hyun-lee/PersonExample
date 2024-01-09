using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OfficeTableViewController : TableViewController
{
    [SerializeField] private string serviceKey;

    private List<Office> officeList = new List<Office>();

    private void Start()
    {
        StartCoroutine(LoadData());
    }

    protected override int numberOfRows() //뿌릴 전체 데이터의 개수 알기...
    {
        return officeList.Count;
    }

    protected override TableViewCell cellForRowAtIndex(int index)
    {
        return null;
    }


    IEnumerator LoadData()
    {
        //서버 url설정
        string url = string.Format("{0}?page={1}&perPage={2}&serviceKey={3}", Constants.url, 0, 100, serviceKey);

        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string result = request.downloadHandler.text;

                OfficeData officeData = JsonUtility.FromJson<OfficeData>(result);

                Office[] officeArray = officeData.data;

                for (int i = 0; i < officeArray.Length; i++)
                {
                    officeList.Add(officeArray[i]);

                }             
            }
        }
    }
}
