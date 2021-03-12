using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000187 RID: 391
	internal class InstantSearchNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00034E64 File Offset: 0x00033064
		internal InstantSearchNotifier(string subscriptionId, UserContext userContext) : base(subscriptionId, userContext)
		{
			this.payloadCollection = new List<InstantSearchNotificationPayload>(3);
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00034E7A File Offset: 0x0003307A
		public override bool ShouldThrottle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00034E80 File Offset: 0x00033080
		protected override bool IsDataAvailableForPickup()
		{
			bool result;
			lock (this.payloadCollection)
			{
				result = (this.payloadCollection.Count > 0);
			}
			return result;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00034ECC File Offset: 0x000330CC
		internal void AddPayload(InstantSearchNotificationPayload payload)
		{
			lock (this.payloadCollection)
			{
				payload.InstantSearchPayload.SearchPerfMarkerContainer.SetPerfMarker(InstantSearchPerfKey.NotificationQueuedTime);
				this.payloadCollection.Add(payload);
			}
			this.PickupData();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00034F2C File Offset: 0x0003312C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			List<NotificationPayloadBase> list = new List<NotificationPayloadBase>();
			lock (this.payloadCollection)
			{
				foreach (InstantSearchNotificationPayload instantSearchNotificationPayload in this.payloadCollection)
				{
					instantSearchNotificationPayload.InstantSearchPayload.SearchPerfMarkerContainer.SetPerfMarker(InstantSearchPerfKey.NotificationPickupFromQueueTime);
				}
				list.AddRange(this.payloadCollection);
				this.payloadCollection.Clear();
			}
			return list;
		}

		// Token: 0x04000879 RID: 2169
		private List<InstantSearchNotificationPayload> payloadCollection;
	}
}
