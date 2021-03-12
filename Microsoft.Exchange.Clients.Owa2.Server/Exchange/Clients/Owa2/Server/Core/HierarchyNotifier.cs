using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000179 RID: 377
	internal class HierarchyNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003418D File Offset: 0x0003238D
		internal HierarchyNotifier(string subscriptionId, UserContext userContext) : base(subscriptionId, userContext)
		{
			this.folderCountTable = new Dictionary<StoreObjectId, HierarchyNotificationPayload>();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000341A2 File Offset: 0x000323A2
		public void Clear(bool clearRefreshPayload)
		{
			this.folderCountTable.Clear();
			if (clearRefreshPayload)
			{
				this.refreshPayload = null;
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000341BC File Offset: 0x000323BC
		internal void AddFolderCountPayload(StoreObjectId folderId, HierarchyNotificationPayload payload)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "HierarchyNotifier.AddFolderCountPayload Start");
			lock (this)
			{
				if (this.refreshPayload == null)
				{
					this.folderCountTable[folderId] = payload;
					if (this.folderCountTable.Count > 200)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "HierarchyNotifier.AddFolderCountPayload Exceeded maxfoldercount");
						this.AddRefreshPayload(payload.SubscriptionId);
					}
				}
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00034254 File Offset: 0x00032454
		internal void AddRefreshPayload(string subscriptionId)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "HierarchyNotifier.ReadDataAndResetStateInternal Start");
			lock (this)
			{
				this.Clear(false);
				if (this.refreshPayload == null)
				{
					this.refreshPayload = new HierarchyNotificationPayload
					{
						EventType = QueryNotificationType.Reload,
						SubscriptionId = subscriptionId
					};
				}
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000342CC File Offset: 0x000324CC
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "HierarchyNotifier.ReadDataAndResetStateInternal. User: {0}", base.UserContext.PrimarySmtpAddress);
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			if (this.refreshPayload != null)
			{
				list.Add(this.refreshPayload);
			}
			else if (this.folderCountTable.Count > 0)
			{
				foreach (KeyValuePair<StoreObjectId, HierarchyNotificationPayload> keyValuePair in this.folderCountTable)
				{
					list.Add(keyValuePair.Value);
				}
			}
			this.Clear(true);
			return list;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00034378 File Offset: 0x00032578
		protected override bool IsDataAvailableForPickup()
		{
			return this.folderCountTable.Count > 0 || this.refreshPayload != null;
		}

		// Token: 0x04000860 RID: 2144
		private const int MaxFolderCountTableSize = 200;

		// Token: 0x04000861 RID: 2145
		private Dictionary<StoreObjectId, HierarchyNotificationPayload> folderCountTable;

		// Token: 0x04000862 RID: 2146
		private HierarchyNotificationPayload refreshPayload;
	}
}
