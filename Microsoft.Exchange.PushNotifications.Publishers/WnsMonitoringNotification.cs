using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000EA RID: 234
	internal sealed class WnsMonitoringNotification : WnsXmlNotification
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x000178D7 File Offset: 0x00015AD7
		public WnsMonitoringNotification(string appId, string deviceId) : base(appId, OrganizationId.ForestWideOrgId, deviceId)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000178E6 File Offset: 0x00015AE6
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000178E9 File Offset: 0x00015AE9
		protected override void WriteWnsXmlPayload(WnsPayloadWriter wpw)
		{
			wpw.WriteElementStart("monitoring", false);
			wpw.WriteElementEnd();
		}
	}
}
