using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x0200020B RID: 523
	internal class PushNotificationContext
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0007408D File Offset: 0x0007228D
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x00074095 File Offset: 0x00072295
		public int? UnseenEmailCount { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0007409E File Offset: 0x0007229E
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x000740A6 File Offset: 0x000722A6
		public BackgroundSyncType BackgroundSyncType { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000740AF File Offset: 0x000722AF
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x000740B7 File Offset: 0x000722B7
		public List<PushNotificationServerSubscription> Subscriptions { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000740C0 File Offset: 0x000722C0
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x000740C8 File Offset: 0x000722C8
		public string TenantId { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x000740D1 File Offset: 0x000722D1
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x000740D9 File Offset: 0x000722D9
		public ExDateTime OriginalTime { get; set; }

		// Token: 0x0600141A RID: 5146 RVA: 0x000740E4 File Offset: 0x000722E4
		public void Merge(PushNotificationContext pushNotificationContext)
		{
			this.OriginalTime = pushNotificationContext.OriginalTime;
			this.TenantId = pushNotificationContext.TenantId;
			if (this.UnseenEmailCount == null)
			{
				this.UnseenEmailCount = pushNotificationContext.UnseenEmailCount;
			}
			if (this.BackgroundSyncType == BackgroundSyncType.None)
			{
				this.BackgroundSyncType = pushNotificationContext.BackgroundSyncType;
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0007413C File Offset: 0x0007233C
		public override string ToString()
		{
			return string.Format("{{ tenantId:'{0}'; unseenEmailCount:'{1}'; backgroundSyncType:'{2}'; originalTime:'{3}'}}", new object[]
			{
				this.TenantId.ToNullableString(),
				this.UnseenEmailCount.ToNullableString<int>(),
				this.BackgroundSyncType.ToString(),
				this.OriginalTime.ToString()
			});
		}
	}
}
