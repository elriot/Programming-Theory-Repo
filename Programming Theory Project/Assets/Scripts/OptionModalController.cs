using UnityEngine;

public class OptionModalController : MonoBehaviour
{
	public GameObject optionModal; // InfoModal Panel
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowModal()
    {
        optionModal.SetActive(true);
    }

    public void HideModal()
    {
        optionModal.SetActive(false);
    }
}
