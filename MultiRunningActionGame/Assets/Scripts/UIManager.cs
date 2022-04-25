using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

// UI를 담당하는 스크립트
public class UIManager : MonoBehaviour
{
    // 메인화면 UI
    public Text statusText;
	public Text roomName;
	public Text userInfo;
	public InputField nickNameInput;


	public PhotonView PV;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
    }

    void UIUpdate()
	{
		if(SceneManager.GetActiveScene().name == "MultiTest")
		{
			statusText.text = PhotonNetwork.NetworkClientState.ToString();
			if(PhotonNetwork.CurrentRoom != null)
				roomName.text = PhotonNetwork.CurrentRoom.Name;

			string playerStr = "방에 있는 플레이어 목록 : ";
			for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
			
			userInfo.text = playerStr;
		}
	}
}
