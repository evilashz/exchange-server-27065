using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000012 RID: 18
	internal class DefaultSynchronizationProvider : SynchronizationProvider
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00008476 File Offset: 0x00006676
		public override void Initialize(EdgeSyncConnector connector)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00008478 File Offset: 0x00006678
		public override string Identity
		{
			get
			{
				return "Default LDAP Synchronization Provider";
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000847F File Offset: 0x0000667F
		public override int LeaseLockTryCount
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00008484 File Offset: 0x00006684
		public override List<TargetServerConfig> TargetServerConfigs
		{
			get
			{
				List<TargetServerConfig> list = new List<TargetServerConfig>();
				Dictionary<string, Server> siteEdgeServers = EdgeSyncSvc.EdgeSync.Topology.SiteEdgeServers;
				foreach (Server server in siteEdgeServers.Values)
				{
					TargetServerConfig item = new TargetEdgeTransportServerConfig(server.Name, server.Fqdn, server.EdgeSyncAdamSslPort, server.VersionNumber);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00008510 File Offset: 0x00006710
		public override EnhancedTimeSpan RecipientSyncInterval
		{
			get
			{
				return EdgeSyncSvc.EdgeSync.Config.ServiceConfig.RecipientSyncInterval;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00008526 File Offset: 0x00006726
		public override EnhancedTimeSpan ConfigurationSyncInterval
		{
			get
			{
				return EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000853C File Offset: 0x0000673C
		public override List<TypeSynchronizer> CreateTypeSynchronizer(SyncTreeType type)
		{
			List<TypeSynchronizer> list = new List<TypeSynchronizer>();
			if (type == SyncTreeType.Configuration)
			{
				list.Add(new TypeSynchronizer(null, new PreDecorate(ConfigSynchronizerManager.SetApplicableTransportSettingFlags), null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "TransportSettings", Schema.Query.QueryTransportSettings, Schema.Query.QueryTransportSettings, SearchScope.Subtree, Schema.TransportConfig.PayloadAttributes, null, true));
				list.Add(new TypeSynchronizer(null, null, null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Exchange Server Recipients", Schema.Query.QueryExchangeServerRecipients, Schema.Query.QueryExchangeServerRecipients, SearchScope.Subtree, Schema.ExchangeRecipient.PayloadAttributes, null, true));
				list.Add(new TypeSynchronizer(null, null, null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Message Classifications", Schema.Query.QueryMessageClassifications, Schema.Query.QueryMessageClassifications, SearchScope.Subtree, Schema.MessageClassification.PayloadAttributes, null, true));
				list.Add(new TypeSynchronizer(new Filter(ConfigSynchronizerManager.FilterLocalSiteServers), null, null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Hub transport servers", Schema.Query.QueryBridgeheads, Schema.Query.QueryBridgeheads, SearchScope.Subtree, Schema.Server.PayloadAttributes, Schema.Server.FilterAttributes, true));
				list.Add(new TypeSynchronizer(null, null, null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Accepted domains", Schema.Query.QueryAcceptedDomains, Schema.Query.QueryAcceptedDomains, SearchScope.Subtree, Schema.AcceptedDomain.PayloadAttributes, null, true));
				list.Add(new TypeSynchronizer(null, null, new PostDecorate(ConfigSynchronizerManager.TransformSecurityDescriptor), new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Partner domains", Schema.Query.QueryPartnerDomains, Schema.Query.QueryPartnerDomains, SearchScope.Subtree, Schema.DomainConfig.PayloadAttributes, new string[]
				{
					"nTSecurityDescriptor"
				}, true));
				list.Add(new TypeSynchronizer(new Filter(ConfigSynchronizerManager.FilterSendConnector), new PreDecorate(ConfigSynchronizerManager.FilterSendConnectorNewerAttributes), new PostDecorate(ConfigSynchronizerManager.TransformSecurityDescriptor), new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Send connectors", Schema.Query.QuerySendConnectors, Schema.Query.QuerySendConnectors, SearchScope.Subtree, Schema.SendConnector.PayloadAttributes, new string[]
				{
					"msExchSourceBridgeheadServersDN",
					"nTSecurityDescriptor"
				}, true));
			}
			else if (type == SyncTreeType.Recipients)
			{
				list.Add(new TypeSynchronizer(new Filter(RecipientSynchronizerManager.FilterRecipientOnAdd), new PreDecorate(RecipientSynchronizerManager.HashRecipientProxyAddressesAndAdjustVersion), null, new LoadTargetCache(SynchronizerManager.LoadTargetCache), new TargetCacheLookup(SynchronizerManager.TargetCacheLookup), new TargetCacheRemoveTargetOnlyEntries(SynchronizerManager.TargetCacheRemoveTargetOnlyEntries), "Recipients", Schema.Query.QueryAllEnterpriseRecipients, "(proxyAddresses=*)", SearchScope.Subtree, RecipientSchema.AttributeNames, null, true));
			}
			return list;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008828 File Offset: 0x00006A28
		public override TargetConnection CreateTargetConnection(TargetServerConfig targetServerConfig, SyncTreeType type, TestShutdownAndLeaseDelegate testShutdownAndLease, EdgeSyncLogSession logSession)
		{
			Server localServer = EdgeSyncSvc.EdgeSync.Topology.LocalServer;
			NetworkCredential networkCredential = Util.ExtractNetworkCredential(localServer, targetServerConfig.Host, logSession);
			if (networkCredential == null)
			{
				return null;
			}
			TargetConnection result;
			try
			{
				result = new LdapTargetConnection(localServer.VersionNumber, targetServerConfig, networkCredential, type, logSession);
			}
			catch (ExDirectoryException ex)
			{
				if (ex.InnerException is LdapException)
				{
					LdapException ex2 = ex.InnerException as LdapException;
					if (ex2.ErrorCode == 49)
					{
						string userName = networkCredential.UserName;
						string host = targetServerConfig.Host;
						logSession.LogConnectFailure("Failed to connect due to invalid credentials.  Re-subscribe Edge.", userName, host, AdamUserManagement.GetPasswordHash(networkCredential.Password));
					}
				}
				throw;
			}
			return result;
		}

		// Token: 0x0400006A RID: 106
		private const string IdentityString = "Default LDAP Synchronization Provider";

		// Token: 0x0400006B RID: 107
		private const int LeaseLockTryCountInternal = 100;
	}
}
