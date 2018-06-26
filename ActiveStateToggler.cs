using UnityEngine;
using System.Collections;

public class ActiveStateToggler : MonoBehaviour {

    public GameObject Panel1;
    public GameObject Panel2;

	public void ToggleActive () {
		Panel1.SetActive (!Panel1.activeSelf);
		Panel2.SetActive (!Panel2.activeSelf);
	}
}
