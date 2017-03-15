using UnityEngine;
using System.Collections;

public class NX_IOUtilityTest : MonoBehaviour {

    public string sourceDirectionRoot = "file:///" + "//..//IO//Xml";
    public string targetDirectionRoot = "file:///" + "//..//IO//Xml222";

    [ContextMenu("TestScanAll")]
    void TestScanAll()
    {
        NX_IOUtility.ScanAllTo(sourceDirectionRoot, targetDirectionRoot);
    }
}
