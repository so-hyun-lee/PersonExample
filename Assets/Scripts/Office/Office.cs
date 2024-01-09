using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Xml.Linq;
using Unity.VisualScripting;


[Serializable] 

public class Office 
{
    public string ���θ��ּ�;
    public string �繫�Ҹ�;
    public string �Ű���;
    public int ����;
    public string ��������;
    public string ��ȭ��ȣ;

    public Office() : this("", "", "", 0, "", "") { }//�ƹ��͵� ���� ������..? ->���� �ʱ�ȭ ���ֱ�����.
    //{
    //    this.���θ��ּ� = "";
    //    this.�繫�Ҹ� = "";
    //    this.�Ű��� = "";
    //    this.���� = 0;
    //    this.�������� = "";
    //    this.��ȭ��ȣ = "";
    //} 
    public Office(string address, string name, string regist, int number, string business, string phone)// ������
    {
        this.���θ��ּ� = address;
        this.�繫�Ҹ�=name;
        this.�Ű��� =regist;
        this.���� =number;
        this.��������=business;
        this.��ȭ��ȣ=phone;

    }

    public Office(string name): this("", name, "", 0, "", "")
    {

    }
}

public class OfficeData
{
    public int currentCount;
    public Office[] data; //�迭
    public int matchCount;
    public int page;
    public int perPage;
    public int totalCount;
}
