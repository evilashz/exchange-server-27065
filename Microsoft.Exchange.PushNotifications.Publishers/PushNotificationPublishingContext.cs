using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000015 RID: 21
	internal class PushNotificationPublishingContext
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003F14 File Offset: 0x00002114
		public PushNotificationPublishingContext(string source, OrganizationId orgId, bool requiresRegistration = false, string hubName = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("source", source);
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			this.Source = source;
			this.OrgId = orgId;
			this.ReceivedTime = ExDateTime.UtcNow;
			this.RequireDeviceRegistration = requiresRegistration;
			this.HubName = hubName;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003F65 File Offset: 0x00002165
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003F6D File Offset: 0x0000216D
		public string Source { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003F76 File Offset: 0x00002176
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003F7E File Offset: 0x0000217E
		public ExDateTime ReceivedTime { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003F87 File Offset: 0x00002187
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003F8F File Offset: 0x0000218F
		public OrganizationId OrgId { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003F98 File Offset: 0x00002198
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003FA0 File Offset: 0x000021A0
		public bool RequireDeviceRegistration { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003FA9 File Offset: 0x000021A9
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003FB1 File Offset: 0x000021B1
		public string HubName { get; private set; }
	}
}
