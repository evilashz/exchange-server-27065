using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002EA RID: 746
	internal class RemoteDomainTable : RemoteDomainMap
	{
		// Token: 0x06002122 RID: 8482 RVA: 0x0007D86C File Offset: 0x0007BA6C
		public RemoteDomainTable(IList<RemoteDomainEntry> entries) : base(entries)
		{
		}

		// Token: 0x020002EB RID: 747
		public class Builder : ConfigurationLoader<RemoteDomainTable, RemoteDomainTable.Builder>.SimpleBuilder<DomainContentConfig>
		{
			// Token: 0x06002123 RID: 8483 RVA: 0x0007D875 File Offset: 0x0007BA75
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				base.RootId = session.GetOrgContainerId().GetChildId("Global Settings").GetChildId("Internet Message Formats");
				base.LoadData(session, QueryScope.OneLevel);
			}

			// Token: 0x06002124 RID: 8484 RVA: 0x0007D8A0 File Offset: 0x0007BAA0
			protected override RemoteDomainTable BuildCache(List<DomainContentConfig> domains)
			{
				List<RemoteDomainEntry> list = new List<RemoteDomainEntry>(domains.Count);
				foreach (DomainContentConfig domainContentConfig in domains)
				{
					if (domainContentConfig.DomainName != null)
					{
						RemoteDomainEntry item = new RemoteDomainEntry(domainContentConfig);
						list.Add(item);
					}
				}
				return new RemoteDomainTable(list);
			}

			// Token: 0x06002125 RID: 8485 RVA: 0x0007D910 File Offset: 0x0007BB10
			protected override ADOperationResult TryRegisterChangeNotification<TConfigObject>(Func<ADObjectId> rootIdGetter, out ADNotificationRequestCookie cookie)
			{
				return TransportADNotificationAdapter.TryRegisterNotifications(new Func<ADObjectId>(ConfigurationLoader<RemoteDomainTable, RemoteDomainTable.Builder>.Builder.GetFirstOrgContainerId), new ADNotificationCallback(base.Reload), new TransportADNotificationAdapter.TransportADNotificationRegister(TransportADNotificationAdapter.Instance.RegisterForRemoteDomainNotifications), 3, out cookie);
			}
		}
	}
}
