using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplaySkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string texto;
    public Text obTexto;
    public Text nomeSkill;
    public GameObject descricaoOb;

    private bool Display;

    void Start()
    {
        obTexto.color = Color.clear;
        nomeSkill.text = "Toque Amaldiçoado";
        nomeSkill.color = Color.black;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Display = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Display = false;
    }

    private void Update()
    {
        if (Display)
        {
            descricaoOb.SetActive(true);
            obTexto.text = texto;
            obTexto.color = Color.white;
        }
        else
        {
            descricaoOb.SetActive(false);
            obTexto.color = Color.clear;
        }
    }
}
