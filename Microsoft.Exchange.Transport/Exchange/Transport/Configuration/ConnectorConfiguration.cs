using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002D3 RID: 723
	internal static class ConnectorConfiguration
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x0007AE70 File Offset: 0x00079070
		public static void SetRoutingOverride(ResolvedMessageEventSource source, Guid tenantId, TenantOutboundConnector recipientConnector, EnvelopeRecipient recipient, Trace tracer, HeaderList headers)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			RoutingDomain routingDomain = new RoutingDomain(ConnectorConfiguration.RoutingConfiguration.Instance.DefaultOpportunisticTlsConnectorRoutingDomain);
			List<RoutingHost> list = null;
			DeliveryQueueDomain deliveryQueueDomain = DeliveryQueueDomain.UseRecipientDomain;
			string overrideSource = null;
			if (recipientConnector != null)
			{
				if (!recipientConnector.Enabled)
				{
					throw new InvalidOperationException(string.Format("Connector {0} is not enabled.", recipientConnector.Name));
				}
				overrideSource = string.Format("{0}:{1}\\{2}", "Connector", tenantId, recipientConnector.Name);
				if (recipientConnector.Identity != null)
				{
					recipient.Properties["Microsoft.Exchange.Hygiene.TenantOutboundConnectorId"] = recipientConnector.Identity;
					recipient.Properties["Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData"] = string.Format("Name={0};ConnectorType={1};UseMxRecord={2}", recipientConnector.Name, recipientConnector.ConnectorType, recipientConnector.UseMXRecord);
				}
				if (headers != null)
				{
					recipient.Properties["PreserveCrossPremisesHeaders"] = recipientConnector.CloudServicesMailEnabled;
				}
				if (!recipientConnector.UseMXRecord)
				{
					tracer.TraceInformation<MultiValuedProperty<SmartHost>>(0, (long)typeof(ConnectorConfiguration).GetHashCode(), "Matching connector found and {0} will be applied as routing override", recipientConnector.SmartHosts);
					list = ConnectorConfiguration.GetRoutingHostCollection(recipientConnector.SmartHosts);
					deliveryQueueDomain = DeliveryQueueDomain.UseAlternateDeliveryRoutingHosts;
				}
				if (recipientConnector.TlsSettings != null)
				{
					TlsAuthLevel valueOrDefault = recipientConnector.TlsSettings.GetValueOrDefault();
					TlsAuthLevel? tlsAuthLevel;
					if (tlsAuthLevel != null)
					{
						switch (valueOrDefault)
						{
						case TlsAuthLevel.EncryptionOnly:
							routingDomain = new RoutingDomain(ConnectorConfiguration.RoutingConfiguration.Instance.ForcedTlsEncryptionOnlyConnectorRoutingDomain);
							goto IL_1CC;
						case TlsAuthLevel.CertificateValidation:
							routingDomain = new RoutingDomain(ConnectorConfiguration.RoutingConfiguration.Instance.ForcedTlsCertificateAuthConnectorRoutingDomain);
							goto IL_1CC;
						case TlsAuthLevel.DomainValidation:
							routingDomain = new RoutingDomain(ConnectorConfiguration.RoutingConfiguration.Instance.ForcedTlsDomainValidationConnectorRoutingDomain);
							if (recipientConnector.TlsDomain != null && !string.IsNullOrEmpty(recipientConnector.TlsDomain.Address))
							{
								source.SetTlsDomain(recipient, recipientConnector.TlsDomain.Address);
								goto IL_1CC;
							}
							goto IL_1CC;
						}
					}
					throw new InvalidOperationException("Unexpected TlsAuthLevel enumeration value encountered");
				}
			}
			IL_1CC:
			source.SetRoutingOverride(recipient, (list == null) ? new RoutingOverride(routingDomain, deliveryQueueDomain) : new RoutingOverride(routingDomain, list, deliveryQueueDomain), overrideSource);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0007B068 File Offset: 0x00079268
		private static List<RoutingHost> GetRoutingHostCollection(IList<SmartHost> smartHosts)
		{
			if (smartHosts == null)
			{
				return null;
			}
			List<RoutingHost> list = new List<RoutingHost>(smartHosts.Count);
			foreach (SmartHost smartHost in smartHosts)
			{
				list.Add(smartHost.InnerRoutingHost);
			}
			return list;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0007B138 File Offset: 0x00079338
		public static ADOperationResult GetOutboundConnectors(OrganizationId orgId, Predicate<TenantOutboundConnector> predicate, out IEnumerable<TenantOutboundConnector> outboundConnectors)
		{
			IEnumerable<TenantOutboundConnector> queryResult = null;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 167, "GetOutboundConnectors", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\ConnectorConfiguration.cs");
				queryResult = from toc in tenantOrTopologyConfigurationSession.Find<TenantOutboundConnector>(null, QueryScope.SubTree, null, null, 0)
				where predicate(toc)
				select toc;
			});
			outboundConnectors = queryResult;
			return result;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0007B1BC File Offset: 0x000793BC
		public static ADOperationResult GetOutboundConnectors(IConfigurationSession tenantConfigurationSession, Predicate<TenantOutboundConnector> predicate, out IEnumerable<TenantOutboundConnector> outboundConnectors)
		{
			IEnumerable<TenantOutboundConnector> queryResult = null;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				queryResult = from toc in tenantConfigurationSession.Find<TenantOutboundConnector>(null, QueryScope.SubTree, null, null, 0)
				where predicate(toc)
				select toc;
			});
			outboundConnectors = queryResult;
			return result;
		}

		// Token: 0x040010CD RID: 4301
		public const string OutboundConnectorPrefix = "Connector";

		// Token: 0x020002D4 RID: 724
		private class RoutingConfiguration
		{
			// Token: 0x170009FD RID: 2557
			// (get) Token: 0x06002022 RID: 8226 RVA: 0x0007B200 File Offset: 0x00079400
			public static ConnectorConfiguration.RoutingConfiguration Instance
			{
				get
				{
					if (ConnectorConfiguration.RoutingConfiguration.instance == null)
					{
						ConnectorConfiguration.RoutingConfiguration value = ConnectorConfiguration.RoutingConfiguration.Load();
						Interlocked.CompareExchange<ConnectorConfiguration.RoutingConfiguration>(ref ConnectorConfiguration.RoutingConfiguration.instance, value, null);
					}
					return ConnectorConfiguration.RoutingConfiguration.instance;
				}
			}

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x06002023 RID: 8227 RVA: 0x0007B22C File Offset: 0x0007942C
			// (set) Token: 0x06002024 RID: 8228 RVA: 0x0007B234 File Offset: 0x00079434
			public string ForcedTlsEncryptionOnlyConnectorRoutingDomain { get; internal set; }

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x06002025 RID: 8229 RVA: 0x0007B23D File Offset: 0x0007943D
			// (set) Token: 0x06002026 RID: 8230 RVA: 0x0007B245 File Offset: 0x00079445
			public string ForcedTlsCertificateAuthConnectorRoutingDomain { get; internal set; }

			// Token: 0x17000A00 RID: 2560
			// (get) Token: 0x06002027 RID: 8231 RVA: 0x0007B24E File Offset: 0x0007944E
			// (set) Token: 0x06002028 RID: 8232 RVA: 0x0007B256 File Offset: 0x00079456
			public string ForcedTlsDomainValidationConnectorRoutingDomain { get; internal set; }

			// Token: 0x17000A01 RID: 2561
			// (get) Token: 0x06002029 RID: 8233 RVA: 0x0007B25F File Offset: 0x0007945F
			// (set) Token: 0x0600202A RID: 8234 RVA: 0x0007B267 File Offset: 0x00079467
			public string DefaultOpportunisticTlsConnectorRoutingDomain { get; internal set; }

			// Token: 0x0600202B RID: 8235 RVA: 0x0007B270 File Offset: 0x00079470
			public static ConnectorConfiguration.RoutingConfiguration Load()
			{
				ConnectorConfiguration.RoutingConfiguration routingConfiguration = new ConnectorConfiguration.RoutingConfiguration();
				string configString = TransportAppConfig.GetConfigString("OpportunisticTlsConnectorRoutingDomain", "DefaultTlsOpportunistic");
				routingConfiguration.DefaultOpportunisticTlsConnectorRoutingDomain = ((!string.IsNullOrEmpty(configString)) ? configString : "DefaultTlsOpportunistic");
				string configString2 = TransportAppConfig.GetConfigString("ForcedTlsEncryptionOnlyConnectorRoutingDomain", "ForcedTlsEncryptionOnly");
				routingConfiguration.ForcedTlsEncryptionOnlyConnectorRoutingDomain = ((!string.IsNullOrEmpty(configString2)) ? configString2 : "ForcedTlsEncryptionOnly");
				string configString3 = TransportAppConfig.GetConfigString("ForcedTlsCAValidationConnectorRoutingDomain", "ForcedTlsCertificateAuth");
				routingConfiguration.ForcedTlsCertificateAuthConnectorRoutingDomain = ((!string.IsNullOrEmpty(configString3)) ? configString3 : "ForcedTlsCertificateAuth");
				string configString4 = TransportAppConfig.GetConfigString("ForcedTlsDomainValidationConnectorRoutingDomain", "ForcedTlsDomainValidation");
				routingConfiguration.ForcedTlsDomainValidationConnectorRoutingDomain = ((!string.IsNullOrEmpty(configString4)) ? configString4 : "ForcedTlsDomainValidation");
				return routingConfiguration;
			}

			// Token: 0x040010CE RID: 4302
			private const string DefaultTlsOpportunistic = "DefaultTlsOpportunistic";

			// Token: 0x040010CF RID: 4303
			private const string ForcedTlsEncryptionOnly = "ForcedTlsEncryptionOnly";

			// Token: 0x040010D0 RID: 4304
			private const string ForcedTlsCertificateAuth = "ForcedTlsCertificateAuth";

			// Token: 0x040010D1 RID: 4305
			private const string ForcedTlsDomainValidation = "ForcedTlsDomainValidation";

			// Token: 0x040010D2 RID: 4306
			private const string ForcedTLSEncryptionOnlyConnectorRoutingDomainParameterName = "ForcedTlsEncryptionOnlyConnectorRoutingDomain";

			// Token: 0x040010D3 RID: 4307
			private const string ForcedTLSCertificateAuthConnectorRoutingDomainParameterName = "ForcedTlsCAValidationConnectorRoutingDomain";

			// Token: 0x040010D4 RID: 4308
			private const string ForcedTLSDomainValidationConnectorRoutingDomainParameterName = "ForcedTlsDomainValidationConnectorRoutingDomain";

			// Token: 0x040010D5 RID: 4309
			private const string DefaultOpportunisticTlsConnectorRoutingDomainParameterName = "OpportunisticTlsConnectorRoutingDomain";

			// Token: 0x040010D6 RID: 4310
			private static ConnectorConfiguration.RoutingConfiguration instance;
		}
	}
}
