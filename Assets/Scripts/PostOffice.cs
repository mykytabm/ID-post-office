using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostOffice : Singleton<PostOffice>
{

    public SendMailObject sendMailObject;
    public GameObject mailPrefab;
    public Transform mailSendRoot;

    void Awake()
    {
        sendMailObject.mouseDownAction += SendMail;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void SendMail()
    {
        var mail = Instantiate(mailPrefab, mailSendRoot.transform.position, Quaternion.identity);
    }
}
