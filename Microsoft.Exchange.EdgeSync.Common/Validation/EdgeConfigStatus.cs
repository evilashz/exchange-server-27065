using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class EdgeConfigStatus
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00008674 File Offset: 0x00006874
		public EdgeConfigStatus()
		{
			this.syncStatus = SyncStatus.Skipped;
			this.orgOnlyObjects = new MultiValuedProperty<ADObjectId>();
			this.edgeOnlyObjects = new MultiValuedProperty<ADObjectId>();
			this.conflictObjects = new MultiValuedProperty<ADObjectId>();
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000086A4 File Offset: 0x000068A4
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000086AC File Offset: 0x000068AC
		public SyncStatus SyncStatus
		{
			get
			{
				return this.syncStatus;
			}
			set
			{
				this.syncStatus = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000086B5 File Offset: 0x000068B5
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000086BD File Offset: 0x000068BD
		public uint SynchronizedObjects
		{
			get
			{
				return this.synchronizedObjects;
			}
			set
			{
				this.synchronizedObjects = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000086C6 File Offset: 0x000068C6
		public MultiValuedProperty<ADObjectId> OrgOnlyObjects
		{
			get
			{
				return this.orgOnlyObjects;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000086CE File Offset: 0x000068CE
		public MultiValuedProperty<ADObjectId> EdgeOnlyObjects
		{
			get
			{
				return this.edgeOnlyObjects;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000086D6 File Offset: 0x000068D6
		public MultiValuedProperty<ADObjectId> ConflictObjects
		{
			get
			{
				return this.conflictObjects;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000086DE File Offset: 0x000068DE
		public override string ToString()
		{
			return this.syncStatus.ToString();
		}

		// Token: 0x04000113 RID: 275
		private SyncStatus syncStatus;

		// Token: 0x04000114 RID: 276
		private uint synchronizedObjects;

		// Token: 0x04000115 RID: 277
		private MultiValuedProperty<ADObjectId> orgOnlyObjects;

		// Token: 0x04000116 RID: 278
		private MultiValuedProperty<ADObjectId> edgeOnlyObjects;

		// Token: 0x04000117 RID: 279
		private MultiValuedProperty<ADObjectId> conflictObjects;
	}
}
