using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000228 RID: 552
	internal class EdgeRoutingTopology : RoutingTopologyBase
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00062CF1 File Offset: 0x00060EF1
		public override TopologyServer LocalServer
		{
			get
			{
				throw new NotSupportedException("LocalServer property is not supported");
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00062CFD File Offset: 0x00060EFD
		public override IEnumerable<MiniDatabase> GetDatabases(bool forcedReload)
		{
			throw new NotSupportedException("Databases property is not supported");
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00062D09 File Offset: 0x00060F09
		public override IEnumerable<TopologyServer> Servers
		{
			get
			{
				throw new NotSupportedException("Servers property is not supported");
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00062D15 File Offset: 0x00060F15
		public override IList<TopologySite> Sites
		{
			get
			{
				throw new NotSupportedException("Sites property is not supported");
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00062D21 File Offset: 0x00060F21
		public override IList<MailGateway> SendConnectors
		{
			get
			{
				return this.sendConnectors;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00062D29 File Offset: 0x00060F29
		public override IList<PublicFolderTree> PublicFolderTrees
		{
			get
			{
				throw new NotSupportedException("PublicFolderTrees property is not supported");
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00062D35 File Offset: 0x00060F35
		public override IList<RoutingGroup> RoutingGroups
		{
			get
			{
				throw new NotSupportedException("RoutingGroups property is not supported");
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00062D41 File Offset: 0x00060F41
		public override IList<RoutingGroupConnector> RoutingGroupConnectors
		{
			get
			{
				throw new NotSupportedException("RoutingGroupConnectors property is not supported");
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00062D4D File Offset: 0x00060F4D
		public override IList<Server> HubServersOnEdge
		{
			get
			{
				return this.hubServersOnEdge;
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00062D55 File Offset: 0x00060F55
		public override void LogData(RoutingTableLogger logger)
		{
			logger.WriteStartElement("EdgeRoutingTopology");
			base.LogSendConnectors(logger);
			this.LogHubServers(logger);
			logger.WriteEndElement();
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00062DF1 File Offset: 0x00060FF1
		protected override void PreLoadInternal()
		{
			this.sendConnectors = base.LoadAll<MailGateway>();
			this.hubServersOnEdge = base.LoadAll<Server>(delegate(Server server)
			{
				if (!server.IsHubTransportServer)
				{
					return false;
				}
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] No FQDN for Server object with DN: {1}. Skipping it.", base.WhenCreated, server.DistinguishedName);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoServerFqdn, null, new object[]
					{
						server.DistinguishedName,
						base.WhenCreated
					});
					return false;
				}
				return true;
			});
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00062E17 File Offset: 0x00061017
		protected override void RegisterForADNotifications(ADNotificationCallback callback, IList<ADNotificationRequestCookie> cookies)
		{
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForMailGatewayNotifications(base.RootId, callback));
			cookies.Add(TransportADNotificationAdapter.Instance.RegisterForExchangeServerNotifications(base.RootId, callback));
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00062E47 File Offset: 0x00061047
		protected override void Validate()
		{
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00062E4C File Offset: 0x0006104C
		private void LogHubServers(RoutingTableLogger logger)
		{
			logger.WriteStartElement("SyncedServers");
			foreach (Server server in this.hubServersOnEdge)
			{
				logger.WriteServer(server);
			}
			logger.WriteEndElement();
		}

		// Token: 0x04000BCE RID: 3022
		private IList<MailGateway> sendConnectors;

		// Token: 0x04000BCF RID: 3023
		private IList<Server> hubServersOnEdge;
	}
}
