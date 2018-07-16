using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour {

	public void AbrirJogo(){
        SceneManager.LoadScene("Game");
    }
}
