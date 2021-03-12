using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000059 RID: 89
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UncompressedSyncStateSizeExceededException : SyncStateSizeExceededException
	{
		// Token: 0x0600024C RID: 588 RVA: 0x000068CD File Offset: 0x00004ACD
		public UncompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, ByteQuantifiedSize currentUncompressedSyncStateSize, ByteQuantifiedSize loadedSyncStateSizeLimit) : base(Strings.UncompressedSyncStateSizeExceededException(syncStateId, subscriptionId, currentUncompressedSyncStateSize, loadedSyncStateSizeLimit))
		{
			this.syncStateId = syncStateId;
			this.subscriptionId = subscriptionId;
			this.currentUncompressedSyncStateSize = currentUncompressedSyncStateSize;
			this.loadedSyncStateSizeLimit = loadedSyncStateSizeLimit;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000068FC File Offset: 0x00004AFC
		public UncompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, ByteQuantifiedSize currentUncompressedSyncStateSize, ByteQuantifiedSize loadedSyncStateSizeLimit, Exception innerException) : base(Strings.UncompressedSyncStateSizeExceededException(syncStateId, subscriptionId, currentUncompressedSyncStateSize, loadedSyncStateSizeLimit), innerException)
		{
			this.syncStateId = syncStateId;
			this.subscriptionId = subscriptionId;
			this.currentUncompressedSyncStateSize = currentUncompressedSyncStateSize;
			this.loadedSyncStateSizeLimit = loadedSyncStateSizeLimit;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006930 File Offset: 0x00004B30
		protected UncompressedSyncStateSizeExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.syncStateId = (string)info.GetValue("syncStateId", typeof(string));
			this.subscriptionId = (Guid)info.GetValue("subscriptionId", typeof(Guid));
			this.currentUncompressedSyncStateSize = (ByteQuantifiedSize)info.GetValue("currentUncompressedSyncStateSize", typeof(ByteQuantifiedSize));
			this.loadedSyncStateSizeLimit = (ByteQuantifiedSize)info.GetValue("loadedSyncStateSizeLimit", typeof(ByteQuantifiedSize));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000069C8 File Offset: 0x00004BC8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("syncStateId", this.syncStateId);
			info.AddValue("subscriptionId", this.subscriptionId);
			info.AddValue("currentUncompressedSyncStateSize", this.currentUncompressedSyncStateSize);
			info.AddValue("loadedSyncStateSizeLimit", this.loadedSyncStateSizeLimit);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006A30 File Offset: 0x00004C30
		public string SyncStateId
		{
			get
			{
				return this.syncStateId;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006A38 File Offset: 0x00004C38
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00006A40 File Offset: 0x00004C40
		public ByteQuantifiedSize CurrentUncompressedSyncStateSize
		{
			get
			{
				return this.currentUncompressedSyncStateSize;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00006A48 File Offset: 0x00004C48
		public ByteQuantifiedSize LoadedSyncStateSizeLimit
		{
			get
			{
				return this.loadedSyncStateSizeLimit;
			}
		}

		// Token: 0x04000105 RID: 261
		private readonly string syncStateId;

		// Token: 0x04000106 RID: 262
		private readonly Guid subscriptionId;

		// Token: 0x04000107 RID: 263
		private readonly ByteQuantifiedSize currentUncompressedSyncStateSize;

		// Token: 0x04000108 RID: 264
		private readonly ByteQuantifiedSize loadedSyncStateSizeLimit;
	}
}
