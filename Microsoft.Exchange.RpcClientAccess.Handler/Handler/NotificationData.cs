using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationData
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00015645 File Offset: 0x00013845
		internal NotificationData(ServerObjectHandle handleValue, Logon logon, Notification notification, StoreId? rootFolderId, View view, Encoding string8Encoding)
		{
			this.notificationHandleValue = handleValue;
			this.logon = logon;
			this.notification = notification;
			this.rootFolderId = rootFolderId;
			this.view = view;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0001567A File Offset: 0x0001387A
		internal View View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00015682 File Offset: 0x00013882
		internal ServerObjectHandle NotificationHandleValue
		{
			get
			{
				return this.notificationHandleValue;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0001568A File Offset: 0x0001388A
		internal Logon Logon
		{
			get
			{
				return this.logon;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00015692 File Offset: 0x00013892
		internal Notification Notification
		{
			get
			{
				return this.notification;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0001569A File Offset: 0x0001389A
		internal StoreId? RootFolderId
		{
			get
			{
				return this.rootFolderId;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000156A2 File Offset: 0x000138A2
		internal Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000156AC File Offset: 0x000138AC
		public override string ToString()
		{
			return string.Format("Logon = {0}, handle = {1}, type = {2}, encoding = {3}, data = {4}", new object[]
			{
				this.logon,
				this.notificationHandleValue,
				(this.view == null) ? "Object" : "Table",
				this.string8Encoding.EncodingName,
				this.notification
			});
		}

		// Token: 0x040000E2 RID: 226
		private readonly ServerObjectHandle notificationHandleValue;

		// Token: 0x040000E3 RID: 227
		private readonly Logon logon;

		// Token: 0x040000E4 RID: 228
		private readonly Notification notification;

		// Token: 0x040000E5 RID: 229
		private readonly StoreId? rootFolderId;

		// Token: 0x040000E6 RID: 230
		private readonly View view;

		// Token: 0x040000E7 RID: 231
		private readonly Encoding string8Encoding;
	}
}
