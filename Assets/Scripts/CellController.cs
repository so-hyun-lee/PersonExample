using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text phoneText;
    [SerializeField] Text addressText;

    private void Awake()
    {
        nameText.text = "";
        phoneText.text = "";
        addressText.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    /// <summary>
    /// Dictionary �����͸� �����ϴ� �޼���
    /// </summary>
    /// <param name="data">Name,Phone, Address������ �� Dictionary </param> 
    public void SetData(Dictionary<string, string>data)
    {
        nameText.text = data["Name"];
        phoneText.text = data["Phone"];
        addressText.text = data["Address"];
    }
}
