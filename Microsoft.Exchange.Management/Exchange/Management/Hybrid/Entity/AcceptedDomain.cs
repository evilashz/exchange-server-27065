using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008E2 RID: 2274
	internal class AcceptedDomain : IAcceptedDomain
	{
		// Token: 0x060050A0 RID: 20640 RVA: 0x00151094 File Offset: 0x0014F294
		public AcceptedDomain(AcceptedDomain ad)
		{
			this.DomainNameDomain = ad.DomainName.Domain;
			this.IsCoexistenceDomain = ad.IsCoexistenceDomain;
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x001510B9 File Offset: 0x0014F2B9
		// (set) Token: 0x060050A2 RID: 20642 RVA: 0x001510C1 File Offset: 0x0014F2C1
		public string DomainNameDomain { get; set; }

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060050A3 RID: 20643 RVA: 0x001510CA File Offset: 0x0014F2CA
		// (set) Token: 0x060050A4 RID: 20644 RVA: 0x001510D2 File Offset: 0x0014F2D2
		public bool IsCoexistenceDomain { get; set; }

		// Token: 0x060050A5 RID: 20645 RVA: 0x001510DB File Offset: 0x0014F2DB
		public override string ToString()
		{
			return "{" + string.Format("Domain:'{0}' IsCoexistenceDomain={1}", this.DomainNameDomain, this.IsCoexistenceDomain) + "}";
		}
	}
}
