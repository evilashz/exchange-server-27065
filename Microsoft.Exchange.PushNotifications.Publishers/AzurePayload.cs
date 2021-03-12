using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004E RID: 78
	internal class AzurePayload
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public AzurePayload(int? unseenEmailCount = null, string message = null, string storeObjectId = null, string backgroundSyncType = null)
		{
			this.UnseenEmailCount = unseenEmailCount;
			this.Message = message;
			this.StoreObjectId = storeObjectId;
			this.BackgroundSyncType = backgroundSyncType;
			this.IsBackground = (backgroundSyncType != null);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AB3F File Offset: 0x00008D3F
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000AB47 File Offset: 0x00008D47
		public string NotificationId { get; internal set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000AB50 File Offset: 0x00008D50
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000AB58 File Offset: 0x00008D58
		public int? UnseenEmailCount { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000AB61 File Offset: 0x00008D61
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000AB69 File Offset: 0x00008D69
		public string Message { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000AB72 File Offset: 0x00008D72
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000AB7A File Offset: 0x00008D7A
		public string StoreObjectId { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000AB83 File Offset: 0x00008D83
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000AB8B File Offset: 0x00008D8B
		public bool IsBackground { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000AB94 File Offset: 0x00008D94
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public string BackgroundSyncType { get; private set; }

		// Token: 0x06000306 RID: 774 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{{id:{0}; unseen:{1}; message:{2}; storeId:{3}; isBackground:{4}; syncType:{5}}}", new object[]
				{
					this.NotificationId,
					this.UnseenEmailCount.ToNullableString<int>(),
					this.Message.ToNullableString(),
					this.StoreObjectId.ToNullableString(),
					this.IsBackground,
					this.BackgroundSyncType
				});
			}
			return this.toString;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000AC28 File Offset: 0x00008E28
		internal void WriteAzurePayload(AzurePayloadWriter apw)
		{
			ArgumentValidator.ThrowIfNull("apw", apw);
			apw.WriteProperty("id", this.NotificationId);
			apw.WriteProperty<int>("unseen", this.UnseenEmailCount);
			apw.WriteProperty("message", this.Message);
			apw.WriteProperty("storeId", this.StoreObjectId);
			apw.WriteProperty<int>("background", this.IsBackground ? 1 : 0);
			apw.WriteProperty("syncType", this.BackgroundSyncType);
		}

		// Token: 0x0400013D RID: 317
		private string toString;
	}
}
