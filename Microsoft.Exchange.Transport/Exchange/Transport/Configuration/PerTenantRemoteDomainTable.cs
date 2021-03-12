using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002E9 RID: 745
	internal sealed class PerTenantRemoteDomainTable : TenantConfigurationCacheableItem<DomainContentConfig>
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x0007D77D File Offset: 0x0007B97D
		public PerTenantRemoteDomainTable()
		{
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0007D785 File Offset: 0x0007B985
		public PerTenantRemoteDomainTable(RemoteDomainTable rdt) : base(true)
		{
			this.SetInternalData(rdt);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0007D795 File Offset: 0x0007B995
		public RemoteDomainTable RemoteDomainTable
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.remoteDomainTable;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0007D7A4 File Offset: 0x0007B9A4
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return (long)(this.estimatedSize + 4);
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0007D7B8 File Offset: 0x0007B9B8
		public override void ReadData(IConfigurationSession session)
		{
			this.estimatedSize = 0;
			ArgumentValidator.ThrowIfNull("session", session);
			ADPagedReader<DomainContentConfig> adpagedReader = session.FindAllPaged<DomainContentConfig>();
			List<RemoteDomainEntry> list = new List<RemoteDomainEntry>();
			if (adpagedReader != null)
			{
				foreach (DomainContentConfig domainContentConfig in adpagedReader)
				{
					if (domainContentConfig.DomainName != null)
					{
						RemoteDomainEntry remoteDomainEntry = new RemoteDomainEntry(domainContentConfig);
						list.Add(remoteDomainEntry);
						this.estimatedSize += remoteDomainEntry.EstimateSize;
					}
				}
			}
			this.SetInternalData(new RemoteDomainTable(list));
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0007D858 File Offset: 0x0007BA58
		private void SetInternalData(RemoteDomainTable remoteDomainTable)
		{
			ArgumentValidator.ThrowIfNull("remoteDomainTable", remoteDomainTable);
			this.remoteDomainTable = remoteDomainTable;
		}

		// Token: 0x04001151 RID: 4433
		private RemoteDomainTable remoteDomainTable;

		// Token: 0x04001152 RID: 4434
		private int estimatedSize;
	}
}
