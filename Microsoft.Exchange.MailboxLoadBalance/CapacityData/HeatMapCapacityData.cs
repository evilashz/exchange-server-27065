using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000027 RID: 39
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class HeatMapCapacityData : IExtensibleDataObject
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006AC5 File Offset: 0x00004CC5
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006ACD File Offset: 0x00004CCD
		[DataMember]
		public ByteQuantifiedSize ConsumerSize { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006AD6 File Offset: 0x00004CD6
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006ADE File Offset: 0x00004CDE
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006AE7 File Offset: 0x00004CE7
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006AEF File Offset: 0x00004CEF
		[DataMember]
		public DirectoryIdentity Identity { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006AF8 File Offset: 0x00004CF8
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006B00 File Offset: 0x00004D00
		[DataMember]
		public LoadMetricStorage LoadMetrics { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006B09 File Offset: 0x00004D09
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006B11 File Offset: 0x00004D11
		[DataMember]
		public ByteQuantifiedSize LogicalSize { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006B1A File Offset: 0x00004D1A
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006B22 File Offset: 0x00004D22
		[DataMember]
		public ByteQuantifiedSize OrganizationSize { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006B2B File Offset: 0x00004D2B
		public ByteQuantifiedSize PhysicalSize
		{
			get
			{
				return this.ConsumerSize + this.OrganizationSize;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006B3E File Offset: 0x00004D3E
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006B46 File Offset: 0x00004D46
		[DataMember]
		public DateTime RetrievedTimestamp { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006B4F File Offset: 0x00004D4F
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006B57 File Offset: 0x00004D57
		[DataMember]
		public ByteQuantifiedSize TotalCapacity { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006B60 File Offset: 0x00004D60
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006B68 File Offset: 0x00004D68
		[DataMember]
		public long TotalMailboxCount { get; set; }

		// Token: 0x06000149 RID: 329 RVA: 0x00006B71 File Offset: 0x00004D71
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((HeatMapCapacityData)obj)));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006BAC File Offset: 0x00004DAC
		public override int GetHashCode()
		{
			int num = this.ConsumerSize.GetHashCode();
			num = (num * 397 ^ this.OrganizationSize.GetHashCode());
			num = (num * 397 ^ this.TotalCapacity.GetHashCode());
			num = (num * 397 ^ ((this.Identity != null) ? this.Identity.GetHashCode() : 0));
			num = (num * 397 ^ this.LogicalSize.GetHashCode());
			num = (num * 397 ^ this.TotalMailboxCount.GetHashCode());
			return num * 397 ^ ((this.LoadMetrics != null) ? this.LoadMetrics.GetHashCode() : 0);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006C80 File Offset: 0x00004E80
		public override string ToString()
		{
			return string.Format("{0} consumer mailbox size, {1} org mailbox size, {2} available, {3}, {4}, {5}, {6}, {7}.", new object[]
			{
				this.ConsumerSize,
				this.OrganizationSize,
				this.TotalCapacity,
				this.Identity,
				this.LogicalSize,
				this.PhysicalSize,
				this.TotalMailboxCount,
				this.LoadMetrics
			});
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006D08 File Offset: 0x00004F08
		protected bool Equals(HeatMapCapacityData other)
		{
			return this.ConsumerSize.Equals(other.ConsumerSize) && this.OrganizationSize.Equals(other.OrganizationSize) && this.TotalCapacity.Equals(other.TotalCapacity) && object.Equals(this.Identity, other.Identity) && this.LogicalSize.Equals(other.LogicalSize) && this.TotalMailboxCount == other.TotalMailboxCount;
		}
	}
}
