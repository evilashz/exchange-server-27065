using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000260 RID: 608
	internal class RoutingTableLogger : DisposeTrackableBase
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x0006B53A File Offset: 0x0006973A
		private RoutingTableLogger(string fileName)
		{
			this.xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8);
			this.xmlWriter.Formatting = Formatting.Indented;
			this.xmlWriter.WriteStartDocument();
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006B56C File Offset: 0x0006976C
		public static void LogRoutingTables(RoutingTables routingTables, RoutingTopologyBase topologyConfig, RoutingContextCore context)
		{
			string text = RoutingTableLogFileManager.LogFilePath;
			try
			{
				text = RoutingTableLogFileManager.CleanupLogsAndGetLogFileName(topologyConfig.WhenCreated, context);
				RoutingDiag.Tracer.TraceDebug<string>(0L, "Start logging routing table to file {0}.", text);
				using (RoutingTableLogger routingTableLogger = new RoutingTableLogger(text))
				{
					routingTableLogger.WriteStartElement("RoutingConfiguration");
					routingTableLogger.WriteElement<string>("SchemaVersion", "15.00.0610.000");
					routingTableLogger.WriteElement<ProcessTransportRole>("ProcessRole", context.GetProcessRoleForDiagnostics());
					topologyConfig.LogData(routingTableLogger);
					routingTableLogger.WriteAppConfigSettings(context.Settings);
					routingTableLogger.WriteEndDocument();
				}
				RoutingDiag.Tracer.TraceDebug<string>(0L, "Finished logging routing table to file {0}.", text);
			}
			catch (UnauthorizedAccessException ex)
			{
				RoutingDiag.Tracer.TraceError<string, UnauthorizedAccessException>(0L, "Failed to log routing table to file: {0} Exception: {1}", text, ex);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogCreationFailure, null, new object[]
				{
					text,
					ex.ToString(),
					ex
				});
			}
			catch (IOException ex2)
			{
				RoutingDiag.Tracer.TraceError<string, IOException>(0L, "Failed to log routing table to file: {0} Exception: {1}", text, ex2);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogCreationFailure, null, new object[]
				{
					text,
					ex2.ToString(),
					ex2
				});
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0006B6C0 File Offset: 0x000698C0
		public void WriteStartElement(string elementName)
		{
			this.xmlWriter.WriteStartElement(elementName);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0006B6CE File Offset: 0x000698CE
		public void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0006B6DB File Offset: 0x000698DB
		public void WriteElement<T>(string elementName, T elementValue)
		{
			this.xmlWriter.WriteElementString(elementName, (elementValue == null) ? "null" : elementValue.ToString());
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0006B708 File Offset: 0x00069908
		public void WriteElement(string elementName, DateTime elementValue)
		{
			string elementValue2 = elementValue.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
			this.WriteElement<string>(elementName, elementValue2);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0006B72F File Offset: 0x0006992F
		public void WriteADObjectId(ADObjectId id)
		{
			this.WriteElement<string>("Name", id.Name);
			this.WriteElement<Guid>("ObjectGuid", id.ObjectGuid);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0006B753 File Offset: 0x00069953
		public void WriteADObjectIdWithDN(ADObjectId id)
		{
			this.WriteADObjectId(id);
			this.WriteElement<string>("DN", id.DistinguishedName ?? "null");
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0006B776 File Offset: 0x00069976
		public void WriteADReference(string elementName, ADObjectId reference)
		{
			this.WriteStartElement(elementName);
			this.WriteADObjectId(reference);
			this.WriteEndElement();
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0006B78C File Offset: 0x0006998C
		public void WriteServer(Server server)
		{
			this.WriteStartElement("Server");
			this.WriteCommonADObjectProperties(server);
			this.WriteElement<int>("MajorVersion", server.MajorVersion);
			if (!string.IsNullOrEmpty(server.Fqdn))
			{
				this.WriteElement<string>("FQDN", server.Fqdn);
			}
			if (!string.IsNullOrEmpty(server.ExchangeLegacyDN))
			{
				this.WriteElement<string>("LegacyDN", server.ExchangeLegacyDN);
			}
			if (server.IsExchange2007OrLater)
			{
				this.WriteElement<ServerRole>("ServerRoles", server.CurrentServerRole);
				if (server.ServerSite != null)
				{
					this.WriteADReference("ADSiteId", server.ServerSite);
				}
				if (server.DatabaseAvailabilityGroup != null)
				{
					this.WriteADReference("DagId", server.DatabaseAvailabilityGroup);
				}
				if (server.IsFrontendTransportServer)
				{
					this.WriteElement<bool>("FrontendComponentActive", RoutingServerInfo.IsFrontendComponentActive(server));
				}
				if (server.IsHubTransportServer)
				{
					this.WriteElement<bool>("HubComponentActive", RoutingServerInfo.IsHubComponentActive(server));
				}
			}
			else if (server.HomeRoutingGroup != null)
			{
				this.WriteADReference("HomeRoutingGroupId", server.HomeRoutingGroup);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0006B898 File Offset: 0x00069A98
		public void WriteTopologyServer(TopologyServer server)
		{
			this.WriteStartElement("Server");
			this.WriteCommonADObjectProperties(server);
			this.WriteElement<int>("MajorVersion", server.MajorVersion);
			if (!string.IsNullOrEmpty(server.Fqdn))
			{
				this.WriteElement<string>("FQDN", server.Fqdn);
			}
			if (!string.IsNullOrEmpty(server.ExchangeLegacyDN))
			{
				this.WriteElement<string>("LegacyDN", server.ExchangeLegacyDN);
			}
			if (server.IsExchange2007OrLater)
			{
				this.WriteElement<ServerRole>("ServerRoles", server.CurrentServerRole);
				if (server.ServerSite != null)
				{
					this.WriteADReference("ADSiteId", server.ServerSite);
				}
				if (server.DatabaseAvailabilityGroup != null)
				{
					this.WriteADReference("DagId", server.DatabaseAvailabilityGroup);
				}
				if (server.IsFrontendTransportServer)
				{
					this.WriteElement<bool>("FrontendComponentActive", RoutingServerInfo.IsFrontendComponentActive(server));
				}
				if (server.IsHubTransportServer)
				{
					this.WriteElement<bool>("HubComponentActive", RoutingServerInfo.IsHubComponentActive(server));
				}
			}
			else if (server.HomeRoutingGroup != null)
			{
				this.WriteADReference("HomeRoutingGroupId", server.HomeRoutingGroup);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0006B9A4 File Offset: 0x00069BA4
		public void WriteSendConnector(MailGateway connector)
		{
			this.WriteStartElement("SendConnector");
			this.WriteCommonConnectorProperties(connector);
			this.WriteElement<string>("ConnectorType", connector.GetType().Name);
			this.WriteElement<bool>("Enabled", connector.Enabled);
			this.WriteElement<bool>("IsScopedConnector", connector.IsScopedConnector);
			this.WriteStartElement("AddressSpaces");
			foreach (AddressSpace elementValue in connector.AddressSpaces)
			{
				this.WriteElement<AddressSpace>("AddressSpace", elementValue);
			}
			this.WriteEndElement();
			SmtpSendConnectorConfig smtpSendConnectorConfig = connector as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig != null)
			{
				this.WriteElement<bool>("DnsRoutingEnabled", smtpSendConnectorConfig.DNSRoutingEnabled);
				if (!smtpSendConnectorConfig.DNSRoutingEnabled)
				{
					this.WriteElement<string>("SmartHosts", smtpSendConnectorConfig.SmartHostsString);
				}
			}
			if (!RoutingUtils.IsNullOrEmpty<ConnectedDomain>(connector.ConnectedDomains))
			{
				this.WriteStartElement("ConnectedDomains");
				foreach (ConnectedDomain elementValue2 in connector.ConnectedDomains)
				{
					this.WriteElement<ConnectedDomain>("ConnectedDomain", elementValue2);
				}
				this.WriteEndElement();
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0006BAF8 File Offset: 0x00069CF8
		public void WriteDatabase(MiniDatabase database)
		{
			this.WriteStartElement("Database");
			this.WriteCommonADObjectProperties(database);
			if (database.Server != null)
			{
				this.WriteADReference("OwningServerId", database.Server);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0006BB2B File Offset: 0x00069D2B
		public void WriteADSite(TopologySite site)
		{
			this.WriteStartElement("ADSite");
			this.WriteCommonADObjectProperties(site);
			this.WriteElement<bool>("HubSiteEnabled", site.HubSiteEnabled);
			this.WriteElement<bool>("InboundMailEnabled", site.InboundMailEnabled);
			this.WriteEndElement();
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0006BB68 File Offset: 0x00069D68
		public void WriteADSiteLink(TopologySiteLink link)
		{
			this.WriteStartElement("ADSiteLink");
			this.WriteCommonADObjectProperties(link);
			this.WriteElement<int>("ADCost", link.ADCost);
			if (link.ExchangeCost != null)
			{
				this.WriteElement<int?>("ExchangeCost", link.ExchangeCost);
			}
			if (!link.MaxMessageSize.IsUnlimited)
			{
				this.WriteElement<Unlimited<ByteQuantifiedSize>>("MaxMessageSize", link.MaxMessageSize);
			}
			this.WriteStartElement("LinkedADSites");
			foreach (ADObjectId reference in link.Sites)
			{
				this.WriteADReference("ADSiteId", reference);
			}
			this.WriteEndElement();
			this.WriteEndElement();
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0006BC3C File Offset: 0x00069E3C
		public void WriteRoutingGroup(RoutingGroup routingGroup)
		{
			this.WriteStartElement("RoutingGroup");
			this.WriteCommonADObjectProperties(routingGroup);
			this.WriteEndElement();
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0006BC58 File Offset: 0x00069E58
		public void WriteRoutingGroupConnector(RoutingGroupConnector rgc)
		{
			this.WriteStartElement("RoutingGroupConnector");
			this.WriteCommonConnectorProperties(rgc);
			this.WriteElement<int>("Cost", rgc.Cost);
			if (rgc.TargetRoutingGroup != null)
			{
				this.WriteADReference("TargetRoutingGroupId", rgc.TargetRoutingGroup);
			}
			if (!RoutingUtils.IsNullOrEmpty<ADObjectId>(rgc.TargetTransportServers))
			{
				this.WriteStartElement("TargetServers");
				foreach (ADObjectId reference in rgc.TargetTransportServers)
				{
					this.WriteADReference("TargetServerId", reference);
				}
				this.WriteEndElement();
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0006BD10 File Offset: 0x00069F10
		public void WritePublicFolderTree(PublicFolderTree publicFolderTree)
		{
			this.WriteStartElement("PublicFolderTree");
			this.WriteCommonADObjectProperties(publicFolderTree);
			if (!RoutingUtils.IsNullOrEmpty<ADObjectId>(publicFolderTree.PublicDatabases))
			{
				this.WriteStartElement("PublicDatabases");
				foreach (ADObjectId reference in publicFolderTree.PublicDatabases)
				{
					this.WriteADReference("PublicDatabaseId", reference);
				}
				this.WriteEndElement();
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				this.Close();
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0006BDC8 File Offset: 0x00069FC8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RoutingTableLogger>(this);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0006BDD0 File Offset: 0x00069FD0
		private void Close()
		{
			if (this.xmlWriter != null)
			{
				this.xmlWriter.Close();
				this.xmlWriter = null;
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0006BDEC File Offset: 0x00069FEC
		private void WriteCommonConnectorProperties(SendConnector connector)
		{
			this.WriteCommonADObjectProperties(connector);
			this.WriteElement<Unlimited<ByteQuantifiedSize>>("MaxMessageSize", connector.MaxMessageSize);
			if (connector.SourceRoutingGroup != null)
			{
				this.WriteADReference("SourceRoutingGroupId", connector.SourceRoutingGroup);
			}
			this.WriteStartElement("SourceServers");
			foreach (ADObjectId reference in connector.SourceTransportServers)
			{
				this.WriteADReference("SourceServerId", reference);
			}
			this.WriteEndElement();
			if (connector.HomeMtaServerId != null)
			{
				this.WriteADReference("HomeMtaServerId", connector.HomeMtaServerId);
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0006BEA0 File Offset: 0x0006A0A0
		private void WriteCommonADObjectProperties(ADObject obj)
		{
			this.WriteADObjectIdWithDN(obj.Id);
			this.WriteElement<DateTime?>("WhenCreated", obj.WhenCreatedUTC);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0006BEC0 File Offset: 0x0006A0C0
		private void WriteAppConfigSettings(TransportAppConfig.RoutingConfig settings)
		{
			this.WriteStartElement("Settings");
			this.WriteElement<TimeSpan>("ConfigReloadInterval", settings.ConfigReloadInterval);
			this.WriteElement<TimeSpan>("DeferredReloadTimeout", settings.DeferredReloadInterval);
			this.WriteElement<int>("MaxDeferredNotifications", settings.MaxDeferredNotifications);
			this.WriteElement<TimeSpan>("MinConfigReloadInterval", settings.MinConfigReloadInterval);
			this.WriteElement<TimeSpan>("PFReplicaAgeThreshold", settings.PfReplicaAgeThreshold);
			this.WriteElement<bool>("DestinationRoutingToRemoteSitesEnabled", settings.DestinationRoutingToRemoteSitesEnabled);
			this.WriteElement<bool>("DagRoutingEnabled", settings.DagRoutingEnabled);
			this.WriteElement<bool>("RoutingToNonActiveServersEnabled", settings.RoutingToNonActiveServersEnabled);
			this.WriteElement<bool>("SmtpDeliveryToMailboxEnabled", settings.SmtpDeliveryToMailboxEnabled);
			this.WriteElement<int>("ProxyRoutingMaxTotalHubCount", settings.ProxyRoutingMaxTotalHubCount);
			this.WriteElement<int>("ProxyRoutingMaxRemoteSiteHubCount", settings.ProxyRoutingMaxRemoteSiteHubCount);
			if (!RoutingUtils.IsNullOrEmpty<int>(settings.ProxyRoutingAllowedTargetVersions))
			{
				this.WriteStartElement("ProxyRoutingAllowedTargetVersions");
				foreach (int elementValue in settings.ProxyRoutingAllowedTargetVersions)
				{
					this.WriteElement<int>("Version", elementValue);
				}
				this.WriteEndElement();
			}
			if (!RoutingUtils.IsNullOrEmpty<RoutingHost>(settings.OutboundFrontendServers))
			{
				this.WriteStartElement("OutboundFrontendServers");
				foreach (RoutingHost elementValue2 in settings.OutboundFrontendServers)
				{
					this.WriteElement<RoutingHost>("OutboundFrontendServer", elementValue2);
				}
				this.WriteEndElement();
				this.WriteElement<bool>("ExternalOutboundFrontendProxyEnabled", settings.ExternalOutboundFrontendProxyEnabled);
			}
			else
			{
				this.WriteElement<bool>("OutboundProxyRoutingXVersionEnabled", settings.OutboundProxyRoutingXVersionEnabled);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0006C080 File Offset: 0x0006A280
		private void WriteEndDocument()
		{
			this.xmlWriter.WriteEndDocument();
			this.xmlWriter.Flush();
		}

		// Token: 0x04000C84 RID: 3204
		private const string SchemaVersion = "15.00.0610.000";

		// Token: 0x04000C85 RID: 3205
		private const string DateTimeFormatSpecifier = "yyyy-MM-ddTHH\\:mm\\:ss.fffZ";

		// Token: 0x04000C86 RID: 3206
		private XmlTextWriter xmlWriter;
	}
}
