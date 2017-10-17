using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

// Token: 0x02000017 RID: 23
public class GClass3 : WebSocketBehavior
{
    // Token: 0x06000168 RID: 360 RVA: 0x00007798 File Offset: 0x00005998
    protected override void OnOpen()
    {
        GClass3.gclass3_0 = this;
    }

    // Token: 0x06000169 RID: 361 RVA: 0x000077AC File Offset: 0x000059AC
    protected override void OnMessage(MessageEventArgs messageEventArgs_0)
    {
        JObject jObject = JObject.Parse(messageEventArgs_0.Data);
        string a = (string)jObject["Action"];
        if (a == "Output")
        {
            return;
        }
        if (a == "OutputColor")
        {
            return;
        }
        if (a == "ClearConsole")
        {
            return;
        }
        if (a == "NewClient")
        {
            base.Send("{\"Action\":\"ConnectionAccepted\"}");
            return;
        }
        if (a == "CurrentMethod")
        {
            return;
        }
        if (a == "ReadyForAutoExec")
        {
            return;
        }
    }

    // Token: 0x04000052 RID: 82
    public static GClass3 gclass3_0;
}
