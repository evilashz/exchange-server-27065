using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000007 RID: 7
	internal class MonitoringMailboxNotificationFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002186 File Offset: 0x00000386
		public MonitoringMailboxNotificationFactory()
		{
			this.RecipientFactories = new Dictionary<string, IMonitoringMailboxNotificationRecipientFactory>();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021A1 File Offset: 0x000003A1
		private Dictionary<string, IMonitoringMailboxNotificationRecipientFactory> RecipientFactories { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x000021AA File Offset: 0x000003AA
		public void RegisterAppToMonitor(string appId, IMonitoringMailboxNotificationRecipientFactory recipientFactory)
		{
			this.RecipientFactories.Add(appId, recipientFactory);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021BC File Offset: 0x000003BC
		public MailboxNotificationBatch CreateMonitoringNotificationBatch()
		{
			if (this.RecipientFactories.Count <= 0)
			{
				throw new InvalidOperationException("The factory has no registered apps");
			}
			List<MailboxNotificationRecipient> list = new List<MailboxNotificationRecipient>(this.RecipientFactories.Count);
			int num = 0;
			foreach (string text in this.RecipientFactories.Keys)
			{
				list.Add(this.RecipientFactories[text].CreateMonitoringRecipient(text, num++));
			}
			return new MailboxNotificationBatch
			{
				Notifications = new List<MailboxNotification>(1),
				Notifications = 
				{
					new MailboxNotification(MailboxNotificationPayload.CreateMonitoringPayload(""), list)
				}
			};
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002288 File Offset: 0x00000488
		public MailboxNotificationBatch CreateMonitoringNotificationBatchForAzure(string monitoringTenantId, string deviceTokenPrefix)
		{
			if (this.RecipientFactories.Count <= 0)
			{
				throw new InvalidOperationException("The factory has no registered apps");
			}
			List<MailboxNotificationRecipient> list = new List<MailboxNotificationRecipient>(this.RecipientFactories.Count);
			string recipientId = string.Empty;
			foreach (string text in this.RecipientFactories.Keys)
			{
				if (string.IsNullOrEmpty(deviceTokenPrefix))
				{
					recipientId = Guid.NewGuid().ToString();
				}
				else
				{
					recipientId = this.GetMonitoringDeviceToken(deviceTokenPrefix, text);
				}
				list.Add(this.RecipientFactories[text].CreateMonitoringRecipient(text, recipientId));
			}
			return new MailboxNotificationBatch
			{
				Notifications = new List<MailboxNotification>(1),
				Notifications = 
				{
					new MailboxNotification(MailboxNotificationPayload.CreateMonitoringPayload(monitoringTenantId), list)
				}
			};
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002378 File Offset: 0x00000578
		public MailboxNotificationBatch CreateExternalMonitoringNotificationBatch()
		{
			List<MailboxNotificationRecipient> list = new List<MailboxNotificationRecipient>(1);
			list.Add(ApnsNotificationFactory.Default.CreateMonitoringRecipient("MonitoringProbeAppId", 0));
			return new MailboxNotificationBatch
			{
				Notifications = new List<MailboxNotification>(1),
				Notifications = 
				{
					new MailboxNotification(MailboxNotificationPayload.CreateMonitoringPayload(""), list)
				}
			};
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023D0 File Offset: 0x000005D0
		public string GetMonitoringDeviceToken(string deviceTokenString, string appId)
		{
			deviceTokenString += appId;
			switch (PushNotificationsMonitoring.CannedAppPlatformSet[appId])
			{
			case PushNotificationPlatform.APNS:
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in deviceTokenString)
				{
					int num = (int)c;
					stringBuilder.AppendFormat("{0:x2}", Convert.ToUInt32(num.ToString()));
				}
				return stringBuilder.ToString();
			}
			case PushNotificationPlatform.WNS:
				return string.Format("http://127.0.0.1:0/send?id={0}", deviceTokenString);
			case PushNotificationPlatform.GCM:
				return deviceTokenString;
			}
			throw new InvalidOperationException(string.Format("App {0} is not supported for monitoring", appId));
		}

		// Token: 0x04000005 RID: 5
		public const string MonitoringProbeAppId = "MonitoringProbeAppId";
	}
}
