using System;

namespace ET
{
    public static class MailSceneComponentHelper
    {

        /// <summary>
        /// 全服邮件
        /// </summary>
        public static void OnServerMail(this MailSceneComponent self, M2E_GMEMailSendRequest request)
        {
            int mailid = self.dBServerMailInfo.ServerMailList.Count + 1;
            ServerMailItem serverMailItem = new ServerMailItem();
            serverMailItem.MailType = request.MailType;

            string[] rewardStrList = request.Itemlist.Split('@');
            for (int i = 0; i < rewardStrList.Length; i++)
            {
                string[] rewardList = rewardStrList[i].Split(';');
                serverMailItem.ItemList.Add(new BagInfo() { ItemID = int.Parse(rewardList[0]), ItemNum = int.Parse(rewardList[1]), GetWay = $"{ItemGetWay.ReceieMail}_{TimeHelper.ServerNow()}" });
            }

            serverMailItem.Parasm = request.Param;
            serverMailItem.ServerMailIId = mailid;
            self.dBServerMailInfo.ServerMailList.Add(mailid, serverMailItem);

            self.SendAllOnLineMail(serverMailItem).Coroutine();
            ;
        }

        public static async ETTask SendAllOnLineMail(this MailSceneComponent self, ServerMailItem serverMailItem)
        {
            try
            {
                int zone = self.DomainZone();
                long chatServerId = StartSceneConfigCategory.Instance.GetBySceneName(zone, Enum.GetName(SceneType.Chat)).InstanceId;
                Chat2Mail_GetUnitList chat2G_EnterChat = (Chat2Mail_GetUnitList)await MessageHelper.CallActor(chatServerId, new Mail2Chat_GetUnitList()
                {
                });
                if (chat2G_EnterChat.Error != ErrorCore.ERR_Success)
                {
                    return;
                }

                for (int i = 0; i < chat2G_EnterChat.OnlineUnitIdList.Count; i++)
                {
                    MailHelp.ServerMailItem(zone, chat2G_EnterChat.OnlineUnitIdList[i], serverMailItem).Coroutine();
                    MailHelp.SendServerMail(zone, chat2G_EnterChat.OnlineUnitIdList[i], serverMailItem);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

        }

    }
}
