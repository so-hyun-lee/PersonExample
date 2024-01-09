using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public interface ICellDelegate
{
    void OnClickCell(int index); //몇번째 인덱스인지
}


public class OfficeCellController : MonoBehaviour
{
    [SerializeField] TMP_Text officeName;
    [SerializeField] TMP_Text businessType;
    [SerializeField] TMP_Text phoneNumber;
    

    public ICellDelegate cellDelegate; //onclickcell을 했을때 해야할일을 시킬 대리자

    private int index;

    // Start is called before the first frame update
    public void Wake()
    {
        officeName.text = "";    
        businessType.text = "";    
        phoneNumber.text = "";    
    }

    public void SetData(string officeName, string businessType, string phoneNumber, int index)
    {
        this.officeName.text = officeName;
        this.businessType.text = businessType;
        this.phoneNumber.text = phoneNumber;
        this.index = index;
    }

    public void OnClickCell()
    {
        cellDelegate.OnClickCell(this.index);
    }
}
