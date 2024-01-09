using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void UpdateData(Office office, int index); //officeŸ���� ������ �޴� � �Լ��� �ִ�.

public class UpdatePanelController : MonoBehaviour
{
    [SerializeField] TMP_InputField addressInputField;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField registInputField;
    [SerializeField] TMP_InputField numberInputField;
    [SerializeField] TMP_InputField businessInputField;
    [SerializeField] TMP_InputField phoneInputField;

    //1.delegate
    public UpdateData updateData; //CreateData(Oiffice office)�� �Ҵ��� �� �ִ�. �ܺη� ���� ���޹޴� ���
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
        addressInputField.text = office.���θ��ּ�;
        nameInputField.text = office.�繫�Ҹ�;
        registInputField.text = office.�Ű���;
        numberInputField.text = office.����.ToString();
        businessInputField.text = office.��������;
        phoneInputField.text = office.��ȭ��ȣ;
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
