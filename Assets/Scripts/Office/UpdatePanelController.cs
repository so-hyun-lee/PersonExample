using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void UpdateData(Office office, int index); //office타입의 변수를 받는 어떤 함수가 있다.

public class UpdatePanelController : MonoBehaviour
{
    [SerializeField] TMP_InputField addressInputField;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField registInputField;
    [SerializeField] TMP_InputField numberInputField;
    [SerializeField] TMP_InputField businessInputField;
    [SerializeField] TMP_InputField phoneInputField;

    //1.delegate
    public UpdateData updateData; //CreateData(Oiffice office)를 할당할 수 있다. 외부로 부터 전달받는 통로
    private int dataIndex;


    void Awake()
    {
        addressInputField.text = "";
        nameInputField.text = "";
        registInputField.text = "";
        numberInputField.text = "";
        businessInputField.text = "";
        phoneInputField.text = "";
    }

    public void SetData(Office office, int index)
    {
        addressInputField.text = office.도로명주소;
        nameInputField.text = office.사무소명;
        registInputField.text = office.신고구분;
        numberInputField.text = office.연번.ToString();
        businessInputField.text = office.영업구분;
        phoneInputField.text = office.전화번호;
    }


    public void OnClickUpdateButton()
    {
        Office office = new Office
               (
               addressInputField.text,
               nameInputField.text,
               registInputField.text,
               int.Parse(numberInputField.text),
               businessInputField.text,
               phoneInputField.text
               );


        //1.delegate
        if (this.updateData != null)
        {
            this.updateData(office,dataIndex);
        }


    }

    public void OnClickCloseButton()
    {
        Destroy(gameObject);
    }



}
