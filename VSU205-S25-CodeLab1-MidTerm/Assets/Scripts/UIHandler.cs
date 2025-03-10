using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject UICanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideUI()
    {
        UICanvas.SetActive(false);
        Debug.Log("Hide UI");
    }

    public void ShowUI()
    {
        UICanvas.SetActive(true);
        Debug.Log("Show UI");
    }
    
    
}
