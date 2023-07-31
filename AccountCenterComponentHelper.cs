using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class AccountCenterComponentHelper
    {


        public static void GenerateSerials(this AccountCenterComponent self, int sindex)
        {
            DBCenterSerialInfo dBCenterSerialInfo = self.DBCenterSerialInfo;
            for (int i = dBCenterSerialInfo.SerialList.Count - 1; i >= 0; i--)
            {
                if (dBCenterSerialInfo.SerialList[i].KeyId == sindex)
                {
                    Log.Console("生成序列号: 重复");
                    return;
                }
            }

            Log.Console($"生成序列号{sindex}: start");
            string codelist = string.Empty;
            self.DBCenterSerialInfo.SerialIndex = sindex;
            SerialHelper serialHelper = new SerialHelper();
            serialHelper.rep = sindex * 1000;  //累加.每次生成1000
            for (int i = 0; i < 1000; i++)
            {
                string code = serialHelper.GenerateCheckCode(6);
                dBCenterSerialInfo.SerialList.Add(new KeyValuePair() { KeyId = sindex, Value = code, Value2 = "0" });
                codelist += code;
                codelist += "\r\n";
            }
            Log.Debug(codelist);
            Log.Console($"生成序列号{sindex}: end");
        }

    }
}
