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
    public string 도로명주소;
    public string 사무소명;
    public string 신고구분;
    public int 연번;
    public string 영업구분;
    public string 전화번호;

    public Office() : this("", "", "", 0, "", "") { }//아무것도 없는 생성자..? ->값을 초기화 해주기위함.
    //{
    //    this.도로명주소 = "";
    //    this.사무소명 = "";
    //    this.신고구분 = "";
    //    this.연번 = 0;
    //    this.영업구분 = "";
    //    this.전화번호 = "";
    //} 
    public Office(string address, string name, string regist, int number, string business, string phone)// 생성자
    {
        this.도로명주소 = address;
        this.사무소명=name;
        this.신고구분 =regist;
        this.연번 =number;
        this.영업구분=business;
        this.전화번호=phone;

    }

    public Office(string name): this("", name, "", 0, "", "")
    {

    }
}

public class OfficeData
{
    public int currentCount;
    public Office[] data; //배열
    public int matchCount;
    public int page;
    public int perPage;
    public int totalCount;
}
