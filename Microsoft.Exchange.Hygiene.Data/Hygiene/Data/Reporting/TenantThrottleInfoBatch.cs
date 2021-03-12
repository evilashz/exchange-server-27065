using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001CB RID: 459
	internal class TenantThrottleInfoBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0003A7D5 File Offset: 0x000389D5
		public TenantThrottleInfoBatch()
		{
			this.TenantThrottleInfoList = new MultiValuedProperty<TenantThrottleInfo>();
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0003A7F4 File Offset: 0x000389F4
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0003A81A File Offset: 0x00038A1A
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0003A82C File Offset: 0x00038A2C
		public int PartitionId
		{
			get
			{
				return (int)this[TenantThrottleInfoBatchSchema.PhysicalInstanceKeyProp];
			}
			set
			{
				this[TenantThrottleInfoBatchSchema.PhysicalInstanceKeyProp] = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0003A83F File Offset: 0x00038A3F
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0003A851 File Offset: 0x00038A51
		public int FssCopyId
		{
			get
			{
				return (int)this[TenantThrottleInfoBatchSchema.FssCopyIdProp];
			}
			set
			{
				this[TenantThrottleInfoBatchSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0003A864 File Offset: 0x00038A64
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x0003A876 File Offset: 0x00038A76
		public MultiValuedProperty<TenantThrottleInfo> TenantThrottleInfoList
		{
			get
			{
				return (MultiValuedProperty<TenantThrottleInfo>)this[TenantThrottleInfoBatchSchema.TenantThrottleInfoListProperty];
			}
			set
			{
				this[TenantThrottleInfoBatchSchema.TenantThrottleInfoListProperty] = value;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003A884 File Offset: 0x00038A84
		public override Type GetSchemaType()
		{
			return typeof(TenantThrottleInfoBatchSchema);
		}

		// Token: 0x04000949 RID: 2377
		private readonly Guid identity = CombGuidGenerator.NewGuid();
	}
}
