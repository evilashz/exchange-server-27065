using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000051 RID: 81
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncPoisonItemFoundException : LocalizedException
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00006390 File Offset: 0x00004590
		public SyncPoisonItemFoundException(string syncPoisonItem, Guid subscriptionId) : base(Strings.SyncPoisonItemFoundException(syncPoisonItem, subscriptionId))
		{
			this.syncPoisonItem = syncPoisonItem;
			this.subscriptionId = subscriptionId;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000063AD File Offset: 0x000045AD
		public SyncPoisonItemFoundException(string syncPoisonItem, Guid subscriptionId, Exception innerException) : base(Strings.SyncPoisonItemFoundException(syncPoisonItem, subscriptionId), innerException)
		{
			this.syncPoisonItem = syncPoisonItem;
			this.subscriptionId = subscriptionId;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000063CC File Offset: 0x000045CC
		protected SyncPoisonItemFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.syncPoisonItem = (string)info.GetValue("syncPoisonItem", typeof(string));
			this.subscriptionId = (Guid)info.GetValue("subscriptionId", typeof(Guid));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006421 File Offset: 0x00004621
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("syncPoisonItem", this.syncPoisonItem);
			info.AddValue("subscriptionId", this.subscriptionId);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006452 File Offset: 0x00004652
		public string SyncPoisonItem
		{
			get
			{
				return this.syncPoisonItem;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000645A File Offset: 0x0000465A
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x040000F9 RID: 249
		private readonly string syncPoisonItem;

		// Token: 0x040000FA RID: 250
		private readonly Guid subscriptionId;
	}
}
