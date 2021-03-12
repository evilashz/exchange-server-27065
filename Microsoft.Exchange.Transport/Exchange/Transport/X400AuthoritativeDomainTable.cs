using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002FA RID: 762
	internal class X400AuthoritativeDomainTable
	{
		// Token: 0x0600218A RID: 8586 RVA: 0x0007F1C1 File Offset: 0x0007D3C1
		private X400AuthoritativeDomainTable(List<X400AuthoritativeDomainEntry> domainEntries)
		{
			this.domainEntries = domainEntries;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0007F1D0 File Offset: 0x0007D3D0
		public bool CheckAccepted(RoutingX400Address address)
		{
			X400AuthoritativeDomainEntry x400AuthoritativeDomainEntry = this.FindBestMatch(address);
			return x400AuthoritativeDomainEntry != null && !x400AuthoritativeDomainEntry.ExternalRelay;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0007F1F4 File Offset: 0x0007D3F4
		public X400AuthoritativeDomainEntry FindBestMatch(RoutingX400Address address)
		{
			X400AuthoritativeDomainEntry x400AuthoritativeDomainEntry = null;
			int num = 0;
			foreach (X400AuthoritativeDomainEntry x400AuthoritativeDomainEntry2 in this.domainEntries)
			{
				if (x400AuthoritativeDomainEntry2.Domain.Match(address))
				{
					int count = x400AuthoritativeDomainEntry2.Domain.Components.Count;
					if (count == address.ComponentsCount)
					{
						return x400AuthoritativeDomainEntry2;
					}
					if (x400AuthoritativeDomainEntry == null || num < count)
					{
						x400AuthoritativeDomainEntry = x400AuthoritativeDomainEntry2;
						num = count;
					}
				}
			}
			return x400AuthoritativeDomainEntry;
		}

		// Token: 0x0400119F RID: 4511
		private readonly List<X400AuthoritativeDomainEntry> domainEntries;

		// Token: 0x020002FB RID: 763
		public class Builder : ConfigurationLoader<X400AuthoritativeDomainTable, X400AuthoritativeDomainTable.Builder>.SimpleBuilder<X400AuthoritativeDomain>
		{
			// Token: 0x0600218D RID: 8589 RVA: 0x0007F284 File Offset: 0x0007D484
			protected override X400AuthoritativeDomainTable BuildCache(List<X400AuthoritativeDomain> domains)
			{
				List<X400AuthoritativeDomainEntry> list = new List<X400AuthoritativeDomainEntry>(domains.Count);
				foreach (X400AuthoritativeDomain x400AuthoritativeDomain in domains)
				{
					if (x400AuthoritativeDomain.X400DomainName != null)
					{
						list.Add(new X400AuthoritativeDomainEntry(x400AuthoritativeDomain));
					}
				}
				return new X400AuthoritativeDomainTable(list);
			}

			// Token: 0x0600218E RID: 8590 RVA: 0x0007F2F4 File Offset: 0x0007D4F4
			protected override ADOperationResult TryRegisterChangeNotification<TConfigObject>(Func<ADObjectId> rootIdGetter, out ADNotificationRequestCookie cookie)
			{
				return TransportADNotificationAdapter.TryRegisterNotifications(rootIdGetter, new ADNotificationCallback(base.Reload), new TransportADNotificationAdapter.TransportADNotificationRegister(TransportADNotificationAdapter.Instance.RegisterForAcceptedDomainNotifications), 3, out cookie);
			}

			// Token: 0x0600218F RID: 8591 RVA: 0x0007F31A File Offset: 0x0007D51A
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				base.RootId = session.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Accepted Domains");
				base.LoadData(session, QueryScope.OneLevel);
			}
		}
	}
}
