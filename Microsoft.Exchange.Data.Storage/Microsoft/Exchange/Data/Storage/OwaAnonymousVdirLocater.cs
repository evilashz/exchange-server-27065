using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D56 RID: 3414
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OwaAnonymousVdirLocater
	{
		// Token: 0x0600763C RID: 30268 RVA: 0x0020A34D File Offset: 0x0020854D
		private OwaAnonymousVdirLocater()
		{
			this.InitializeAnonymousCalendarHostUrl();
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x0600763D RID: 30269 RVA: 0x0020A35C File Offset: 0x0020855C
		public static OwaAnonymousVdirLocater Instance
		{
			get
			{
				if (OwaAnonymousVdirLocater.instance == null)
				{
					lock (OwaAnonymousVdirLocater.syncLock)
					{
						if (OwaAnonymousVdirLocater.instance == null)
						{
							OwaAnonymousVdirLocater.instance = new OwaAnonymousVdirLocater();
						}
					}
				}
				return OwaAnonymousVdirLocater.instance;
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x0600763E RID: 30270 RVA: 0x0020A3B4 File Offset: 0x002085B4
		private bool IsMultitenancyEnabled
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x0600763F RID: 30271 RVA: 0x0020A3D8 File Offset: 0x002085D8
		public Uri GetOwaVdirUrl(IExchangePrincipal exchangePrincipal, IFrontEndLocator frontEndLocator)
		{
			Uri owaServiceUrl = this.GetOwaServiceUrl(exchangePrincipal, frontEndLocator);
			UriBuilder uriBuilder = new UriBuilder(owaServiceUrl);
			uriBuilder.Port = -1;
			uriBuilder.Scheme = Uri.UriSchemeHttp;
			if (this.anonymousCalendarHostUrl != null)
			{
				uriBuilder.Host = this.anonymousCalendarHostUrl.Host;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06007640 RID: 30272 RVA: 0x0020A42C File Offset: 0x0020862C
		public bool IsPublishingAvailable(IExchangePrincipal exchangePrincipal, IFrontEndLocator frontEndLocator)
		{
			bool result;
			try
			{
				this.GetOwaVdirUrl(exchangePrincipal, frontEndLocator);
				result = true;
			}
			catch (NoExternalOwaAvailableException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x0020A45C File Offset: 0x0020865C
		private void InitializeAnonymousCalendarHostUrl()
		{
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 141, "InitializeAnonymousCalendarHostUrl", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\OwaAnonymousVdirLocater.cs");
				ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
				ServiceEndpoint endpoint = endpointContainer.GetEndpoint(ServiceEndpointId.AnonymousCalendarHostUrl);
				this.anonymousCalendarHostUrl = endpoint.Uri;
			}
			catch (EndpointContainerNotFoundException)
			{
			}
			catch (ServiceEndpointNotFoundException)
			{
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x0020A4D0 File Offset: 0x002086D0
		private Uri GetOwaServiceUrl(IExchangePrincipal exchangePrincipal, IFrontEndLocator frontEndLocator)
		{
			if (exchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E15MinVersion && this.IsMultitenancyEnabled)
			{
				return this.GetE15MultitenancyOwaServiceUrl(exchangePrincipal, frontEndLocator);
			}
			return this.GetEnterpriseOrE14OwaServiceUrl(exchangePrincipal);
		}

		// Token: 0x06007643 RID: 30275 RVA: 0x0020A504 File Offset: 0x00208704
		private Uri GetE15MultitenancyOwaServiceUrl(IExchangePrincipal exchangePrincipal, IFrontEndLocator frontEndLocator)
		{
			Uri result = null;
			Exception ex = null;
			try
			{
				result = frontEndLocator.GetOwaUrl(exchangePrincipal);
			}
			catch (ServerNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				ex = ex3;
			}
			catch (DataSourceOperationException ex4)
			{
				ex = ex4;
			}
			catch (DataValidationException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (ex != null)
				{
					throw new NoExternalOwaAvailableException(ex);
				}
			}
			return result;
		}

		// Token: 0x06007644 RID: 30276 RVA: 0x0020A590 File Offset: 0x00208790
		private Uri GetEnterpriseOrE14OwaServiceUrl(IExchangePrincipal exchangePrincipal)
		{
			ServiceTopology serviceTopology = this.IsMultitenancyEnabled ? ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\OwaAnonymousVdirLocater.cs", "GetEnterpriseOrE14OwaServiceUrl", 230) : ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\OwaAnonymousVdirLocater.cs", "GetEnterpriseOrE14OwaServiceUrl", 230);
			Predicate<OwaService> serviceFilter = (OwaService service) => service.AnonymousFeaturesEnabled;
			IList<OwaService> list = serviceTopology.FindAll<OwaService>(exchangePrincipal, ClientAccessType.External, serviceFilter, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\OwaAnonymousVdirLocater.cs", "GetEnterpriseOrE14OwaServiceUrl", 235);
			OwaService owaService;
			if (list.Count > 0)
			{
				owaService = list[0];
			}
			else
			{
				owaService = serviceTopology.FindAny<OwaService>(ClientAccessType.External, serviceFilter, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\OwaAnonymousVdirLocater.cs", "GetEnterpriseOrE14OwaServiceUrl", 247);
				if (owaService == null)
				{
					throw new NoExternalOwaAvailableException();
				}
			}
			return owaService.Url;
		}

		// Token: 0x040051E5 RID: 20965
		private const string OwaAnonymousVdirName = "calendar";

		// Token: 0x040051E6 RID: 20966
		private static OwaAnonymousVdirLocater instance;

		// Token: 0x040051E7 RID: 20967
		private static readonly object syncLock = new object();

		// Token: 0x040051E8 RID: 20968
		private Uri anonymousCalendarHostUrl;
	}
}
