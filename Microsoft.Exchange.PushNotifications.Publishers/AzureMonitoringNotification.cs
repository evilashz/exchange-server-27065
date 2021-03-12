using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004A RID: 74
	internal sealed class AzureMonitoringNotification : AzureNotification
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000A565 File Offset: 0x00008765
		public AzureMonitoringNotification(string appId, string hubName, string recipientId) : base(appId, recipientId, hubName, new AzurePayload(new int?(6), null, null, null), false)
		{
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000A57F File Offset: 0x0000877F
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000A582 File Offset: 0x00008782
		public override string RecipientId
		{
			get
			{
				return string.Format("{0}{1}", base.RecipientId, "MonitoringTag");
			}
		}

		// Token: 0x04000138 RID: 312
		private const string MonitoringAzureTagSuffix = "MonitoringTag";
	}
}
