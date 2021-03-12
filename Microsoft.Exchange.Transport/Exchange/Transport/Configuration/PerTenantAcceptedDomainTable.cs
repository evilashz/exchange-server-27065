using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002DD RID: 733
	internal class PerTenantAcceptedDomainTable : TenantConfigurationCacheableItem<AcceptedDomain>
	{
		// Token: 0x0600206F RID: 8303 RVA: 0x0007C20A File Offset: 0x0007A40A
		public PerTenantAcceptedDomainTable()
		{
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0007C212 File Offset: 0x0007A412
		public PerTenantAcceptedDomainTable(AcceptedDomainTable adt) : base(true)
		{
			this.SetInternalData(adt);
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x0007C222 File Offset: 0x0007A422
		public virtual AcceptedDomainTable AcceptedDomainTable
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.acceptedDomainTable;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x0007C231 File Offset: 0x0007A431
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return (long)(this.estimatedSize + 4);
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0007C244 File Offset: 0x0007A444
		public override void ReadData(IConfigurationSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ADPagedReader<AcceptedDomain> domains = session.FindAllPaged<AcceptedDomain>();
			List<AcceptedDomainEntry> entries;
			AcceptedDomainEntry defaultDomain;
			List<string> internalDomains;
			this.estimatedSize = AcceptedDomainTable.Builder.CreateAcceptedDomainEntries(domains, out entries, out defaultDomain, out internalDomains);
			this.SetInternalData(new AcceptedDomainTable(internalDomains, defaultDomain, entries));
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0007C283 File Offset: 0x0007A483
		private void SetInternalData(AcceptedDomainTable acceptedDomainTable)
		{
			ArgumentValidator.ThrowIfNull("acceptedDomainTable", acceptedDomainTable);
			this.acceptedDomainTable = acceptedDomainTable;
		}

		// Token: 0x040010FC RID: 4348
		private AcceptedDomainTable acceptedDomainTable;

		// Token: 0x040010FD RID: 4349
		private int estimatedSize;
	}
}
