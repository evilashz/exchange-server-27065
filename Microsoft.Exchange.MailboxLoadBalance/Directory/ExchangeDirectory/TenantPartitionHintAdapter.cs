using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory.ExchangeDirectory
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TenantPartitionHintAdapter
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000CB9E File Offset: 0x0000AD9E
		public TenantPartitionHintAdapter(Guid externalDirectoryOrganizationId, bool isConsumer)
		{
			this.externalDirectoryOrganizationId = externalDirectoryOrganizationId;
			this.isConsumer = isConsumer;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		public virtual Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return this.externalDirectoryOrganizationId;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public virtual bool IsConsumer
		{
			get
			{
				return this.isConsumer;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public static TenantPartitionHintAdapter FromPartitionHint(TenantPartitionHint partitionHint)
		{
			return new TenantPartitionHintAdapter(partitionHint.GetExternalDirectoryOrganizationId(), partitionHint.IsConsumer());
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		public static TenantPartitionHintAdapter FromPersistableTenantPartitionHint(byte[] persistedData)
		{
			TenantPartitionHint partitionHint;
			if (persistedData == null)
			{
				partitionHint = TenantPartitionHint.FromOrganizationId(OrganizationId.ForestWideOrgId);
			}
			else
			{
				partitionHint = TenantPartitionHint.FromPersistablePartitionHint(persistedData);
			}
			return TenantPartitionHintAdapter.FromPartitionHint(partitionHint);
		}

		// Token: 0x04000196 RID: 406
		private readonly Guid externalDirectoryOrganizationId;

		// Token: 0x04000197 RID: 407
		private readonly bool isConsumer;
	}
}
