using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A1 RID: 161
	internal class GcmPayload
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00012C23 File Offset: 0x00010E23
		public GcmPayload(int? unseenEmailCount = null, string message = null, string extraData = null, BackgroundSyncType backgroundSyncType = BackgroundSyncType.None)
		{
			this.UnseenEmailCount = unseenEmailCount;
			this.Message = message;
			this.ExtraData = extraData;
			this.BackgroundSyncType = backgroundSyncType;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00012C48 File Offset: 0x00010E48
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00012C50 File Offset: 0x00010E50
		public int? UnseenEmailCount { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00012C59 File Offset: 0x00010E59
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00012C61 File Offset: 0x00010E61
		public string Message { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00012C6A File Offset: 0x00010E6A
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00012C72 File Offset: 0x00010E72
		public string ExtraData { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00012C7B File Offset: 0x00010E7B
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00012C83 File Offset: 0x00010E83
		public BackgroundSyncType BackgroundSyncType { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00012C8C File Offset: 0x00010E8C
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00012C94 File Offset: 0x00010E94
		public string NotificationId { get; internal set; }

		// Token: 0x060005AC RID: 1452 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{{unseenEmailCount:{0}; message:{1}; data:{2}; backgroundSyncType:{3}; notificationId:{4}}}", new object[]
				{
					this.UnseenEmailCount.ToNullableString<int>(),
					this.Message.ToNullableString(),
					this.ExtraData.ToNullableString(),
					this.BackgroundSyncType.ToNullableString(null),
					this.NotificationId.ToNullableString()
				});
			}
			return this.toString;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00012D1C File Offset: 0x00010F1C
		internal void WriteGcmPayload(GcmPayloadWriter gpw)
		{
			ArgumentValidator.ThrowIfNull("gpw", gpw);
			gpw.WriteProperty<int>("data.UnseenEmailCount", this.UnseenEmailCount);
			gpw.WriteProperty("data.Message", this.Message);
			gpw.WriteProperty("data.ExtraData", this.ExtraData);
			gpw.WriteProperty<int>("data.BackgroundSyncType", (int)this.BackgroundSyncType);
			gpw.WriteProperty("data.ServerNotificationId", this.NotificationId);
		}

		// Token: 0x040002BE RID: 702
		private string toString;
	}
}
