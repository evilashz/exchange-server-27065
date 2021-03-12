using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000058 RID: 88
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CompressedSyncStateSizeExceededException : SyncStateSizeExceededException
	{
		// Token: 0x06000245 RID: 581 RVA: 0x000067A2 File Offset: 0x000049A2
		public CompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, StoragePermanentException e) : base(Strings.CompressedSyncStateSizeExceededException(syncStateId, subscriptionId, e))
		{
			this.syncStateId = syncStateId;
			this.subscriptionId = subscriptionId;
			this.e = e;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000067C7 File Offset: 0x000049C7
		public CompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, StoragePermanentException e, Exception innerException) : base(Strings.CompressedSyncStateSizeExceededException(syncStateId, subscriptionId, e), innerException)
		{
			this.syncStateId = syncStateId;
			this.subscriptionId = subscriptionId;
			this.e = e;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000067F0 File Offset: 0x000049F0
		protected CompressedSyncStateSizeExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.syncStateId = (string)info.GetValue("syncStateId", typeof(string));
			this.subscriptionId = (Guid)info.GetValue("subscriptionId", typeof(Guid));
			this.e = (StoragePermanentException)info.GetValue("e", typeof(StoragePermanentException));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006868 File Offset: 0x00004A68
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("syncStateId", this.syncStateId);
			info.AddValue("subscriptionId", this.subscriptionId);
			info.AddValue("e", this.e);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000068B5 File Offset: 0x00004AB5
		public string SyncStateId
		{
			get
			{
				return this.syncStateId;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000068BD File Offset: 0x00004ABD
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000068C5 File Offset: 0x00004AC5
		public StoragePermanentException E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04000102 RID: 258
		private readonly string syncStateId;

		// Token: 0x04000103 RID: 259
		private readonly Guid subscriptionId;

		// Token: 0x04000104 RID: 260
		private readonly StoragePermanentException e;
	}
}
