using System;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000334 RID: 820
	internal static class LiveConfiguration
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0007C243 File Offset: 0x0007A443
		public static EnhancedTimeSpan DefaultFederatedMetadataTimeout
		{
			get
			{
				return EnhancedTimeSpan.OneDay;
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0007C24C File Offset: 0x0007A44C
		internal static Uri GetLiveIdFederationMetadataEpr(FederationTrust.NamespaceProvisionerType provisionerType)
		{
			switch (provisionerType)
			{
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices:
				return LiveConfiguration.GetEndpoint(ServiceEndpointId.LiveFederationMetadata);
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices2:
				return LiveConfiguration.GetEndpoint(ServiceEndpointId.MsoFederationMetadata);
			default:
				throw new ArgumentException("provisionerType");
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0007C28B File Offset: 0x0007A48B
		internal static Uri GetDomainServicesEpr()
		{
			return LiveConfiguration.GetEndpoint(ServiceEndpointId.DomainPartnerManageDelegation);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007C297 File Offset: 0x0007A497
		internal static Uri GetDomainServices2Epr()
		{
			return LiveConfiguration.GetEndpoint(ServiceEndpointId.DomainPartnerManageDelegation2);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0007C2A4 File Offset: 0x0007A4A4
		internal static WebProxy GetWebProxy(WriteVerboseDelegate writeVerbose)
		{
			WebProxy result;
			try
			{
				WebProxy webProxy = null;
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					writeVerbose(Strings.WebProxy(localServer.InternetWebProxy.ToString()));
					webProxy = new WebProxy(localServer.InternetWebProxy);
				}
				result = webProxy;
			}
			catch (NotSupportedException ex)
			{
				throw new LiveDomainServicesException(Strings.CannotSetProxy(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0007C314 File Offset: 0x0007A514
		private static Uri GetEndpoint(string serviceEndpointId)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 129, "GetEndpoint", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\FederationProvisioning\\LiveConfiguration.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			ServiceEndpoint endpoint = endpointContainer.GetEndpoint(serviceEndpointId);
			return new Uri(endpoint.Uri.AbsoluteUri, UriKind.Absolute);
		}

		// Token: 0x04000C28 RID: 3112
		public const string LiveExchangeProgramId = "ExchangeConnector";

		// Token: 0x04000C29 RID: 3113
		public const string DefaultTokenPolicyUri = "EX_MBI_FED_SSL";
	}
}
