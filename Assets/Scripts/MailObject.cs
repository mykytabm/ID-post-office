using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailObject : MonoBehaviour
{
    public EMail mailType;
}

public enum EMail
{
    Regular,
    Special,
    Intergalactical
}
