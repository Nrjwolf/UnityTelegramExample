using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    [System.Serializable]
    public class View
    {
        [Header("Buttons")]
        public Button getMe;
        public Button sendDocument;
        public Button sendPhoto;
        public Button sendMessage;

        [Space(10)]
        [Header("Text")]
        public Text inputText;
    }

    public Telegram telegram;
    public View view;

    void Start()
    {
        // Bot info
        view.getMe.onClick.AddListener(() => telegram.GetMe());

        // Send any document
        view.sendDocument.onClick.AddListener(() =>
        {
			// send txt file
            var bytes = System.Text.Encoding.UTF8.GetBytes(GetUserInfo());
            telegram.SendFile(bytes, "file.txt", GetUserInfo());
        });

        // send file as photo
        view.sendPhoto.onClick.AddListener(() =>
        {
            var screenshotName = "Screenshot.png";
            ScreenCapture.CaptureScreenshot("Assets/" + screenshotName);
            var bytes = File.ReadAllBytes(Application.dataPath + "/" + screenshotName);
            telegram.SendPhoto(bytes, screenshotName);
        });

        // send message
        view.sendMessage.onClick.AddListener(() =>
        {
            telegram.SendMessage(view.inputText.text);
        });
    }

    /**
	* Utils
	*
	*/

    private string GetUserInfo()
    {
        return string.Format("From :\n{0}\n{1}", SystemInfo.deviceName, SystemInfo.deviceModel);
    }
}
