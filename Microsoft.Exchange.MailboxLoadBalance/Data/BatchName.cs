using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class BatchName : IExtensibleDataObject
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00008072 File Offset: 0x00006272
		private BatchName(DateTime creationTimestamp, string batchPurpose) : this(string.Format("{0}:{1}:{2:yyyyMMddhhmm}", "MsExchMlb", batchPurpose, creationTimestamp))
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008090 File Offset: 0x00006290
		private BatchName(string batchName)
		{
			this.batchName = (batchName ?? string.Empty);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000080A8 File Offset: 0x000062A8
		// (set) Token: 0x06000269 RID: 617 RVA: 0x000080B0 File Offset: 0x000062B0
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000080B9 File Offset: 0x000062B9
		public bool IsLoadBalancingBatch
		{
			get
			{
				return this.batchName.StartsWith("MsExchMlb");
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000080CB File Offset: 0x000062CB
		public static BatchName CreateBandBalanceBatch()
		{
			return new BatchName(TimeProvider.UtcNow, "Band");
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000080DC File Offset: 0x000062DC
		public static BatchName CreateDrainBatch(DirectoryIdentity identity)
		{
			return new BatchName(TimeProvider.UtcNow, string.Format("Drain:{0}", identity.Name));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000080F8 File Offset: 0x000062F8
		public static BatchName CreateItemUpgradeBatch()
		{
			return new BatchName(TimeProvider.UtcNow.Date, "ItemUpgrade");
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000811C File Offset: 0x0000631C
		public static BatchName CreateProvisioningConstraintFixBatch()
		{
			return new BatchName(TimeProvider.UtcNow, "ProvisioningConstraintsMailboxProcessor");
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000812D File Offset: 0x0000632D
		public static BatchName FromString(string batchName)
		{
			return new BatchName(batchName);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008135 File Offset: 0x00006335
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((BatchName)obj)));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000816E File Offset: 0x0000636E
		public override int GetHashCode()
		{
			if (this.batchName == null)
			{
				return 0;
			}
			return this.batchName.GetHashCode();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008185 File Offset: 0x00006385
		public override string ToString()
		{
			return this.batchName;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000818D File Offset: 0x0000638D
		protected bool Equals(BatchName other)
		{
			return string.Equals(this.batchName, other.batchName);
		}

		// Token: 0x040000AB RID: 171
		private const string BatchNamePrefix = "MsExchMlb";

		// Token: 0x040000AC RID: 172
		[DataMember]
		private readonly string batchName;
	}
}
