using UnityEngine;

public class ControlModalController : MonoBehaviour
{
	public GameObject infoModal; // InfoModal Panel

    public void ShowModal()
    {
        infoModal.SetActive(true);
    }

    public void HideModal()
    {
        infoModal.SetActive(false);
    }
}
