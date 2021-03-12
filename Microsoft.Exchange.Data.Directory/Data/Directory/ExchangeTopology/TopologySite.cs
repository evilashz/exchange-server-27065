using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006BD RID: 1725
	internal sealed class TopologySite : ADSite, ITopologySite
	{
		// Token: 0x06004FB2 RID: 20402 RVA: 0x00126566 File Offset: 0x00124766
		internal TopologySite(ADSite site)
		{
			this.propertyBag = site.propertyBag;
			this.SetIsReadOnly(site.IsReadOnly);
			this.m_Session = site.Session;
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x00126592 File Offset: 0x00124792
		// (set) Token: 0x06004FB4 RID: 20404 RVA: 0x0012659A File Offset: 0x0012479A
		public ReadOnlyCollection<ITopologySiteLink> TopologySiteLinks
		{
			get
			{
				return this.topologySiteLinks;
			}
			internal set
			{
				this.topologySiteLinks = value;
			}
		}

		// Token: 0x04003669 RID: 13929
		private ReadOnlyCollection<ITopologySiteLink> topologySiteLinks;
	}
}
