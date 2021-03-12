using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B4 RID: 180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FrontEndLocator : IFrontEndLocator
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001DD48 File Offset: 0x0001BF48
		public static Uri GetDatacenterMapiHttpUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<MapiHttpService>();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001DD4F File Offset: 0x0001BF4F
		public static MiniVirtualDirectory GetDatacenterRpcHttpVdir()
		{
			return GlobalServiceUrls.GetRpcHttpVdir();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001DD56 File Offset: 0x0001BF56
		public static Uri GetDatacenterFrontEndWebServicesUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<WebServicesService>();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001DD5D File Offset: 0x0001BF5D
		public static Uri GetDatacenterFrontEndOwaUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<OwaService>();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public static Uri GetDatacenterFrontEndEcpUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<EcpService>();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		public static Uri GetDatacenterFrontEndEasUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<MobileSyncService>();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001DD72 File Offset: 0x0001BF72
		public static Uri GetDatacenterFrontEndOabUrl()
		{
			return GlobalServiceUrls.GetExternalUrl<OabService>();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001DD79 File Offset: 0x0001BF79
		public static Uri GetFrontEndWebServicesUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndHttpServiceUrl<WebServicesService>(exchangePrincipal);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001DD81 File Offset: 0x0001BF81
		public static Uri GetFrontEndWebServicesUrl(string serverFqdn)
		{
			return GlobalServiceUrls.GetExternalUrl<WebServicesService>(serverFqdn);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001DD89 File Offset: 0x0001BF89
		public static Uri GetFrontEndOwaUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndHttpServiceUrl<OwaService>(exchangePrincipal);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001DD91 File Offset: 0x0001BF91
		public static Uri GetFrontEndEcpUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndHttpServiceUrl<EcpService>(exchangePrincipal);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001DD99 File Offset: 0x0001BF99
		public static Uri GetFrontEndEasUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndHttpServiceUrl<MobileSyncService>(exchangePrincipal);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001DDA1 File Offset: 0x0001BFA1
		public static ProtocolConnectionSettings GetFrontEndPop3SettingsForLocalServer()
		{
			return GlobalServiceUrls.GetExternalProtocolSettingsForLocalServer<Pop3Service>();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public static ProtocolConnectionSettings GetFrontEndImap4SettingsForLocalServer()
		{
			return GlobalServiceUrls.GetExternalProtocolSettingsForLocalServer<Imap4Service>();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001DDAF File Offset: 0x0001BFAF
		public static ProtocolConnectionSettings GetFrontEndSmtpSettingsForLocalServer()
		{
			return GlobalServiceUrls.GetExternalProtocolSettingsForLocalServer<SmtpService>();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001DDB6 File Offset: 0x0001BFB6
		public static ProtocolConnectionSettings GetInternalPop3SettingsForLocalServer()
		{
			return GlobalServiceUrls.GetInternalProtocolSettingsForLocalServer<Pop3Service>();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001DDBD File Offset: 0x0001BFBD
		public static ProtocolConnectionSettings GetInternalImap4SettingsForLocalServer()
		{
			return GlobalServiceUrls.GetInternalProtocolSettingsForLocalServer<Imap4Service>();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public static ProtocolConnectionSettings GetInternalSmtpSettingsForLocalServer()
		{
			return GlobalServiceUrls.GetInternalProtocolSettingsForLocalServer<SmtpService>();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001DDCB File Offset: 0x0001BFCB
		public Uri GetWebServicesUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndWebServicesUrl(exchangePrincipal);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001DDD3 File Offset: 0x0001BFD3
		public Uri GetOwaUrl(IExchangePrincipal exchangePrincipal)
		{
			return FrontEndLocator.GetFrontEndOwaUrl(exchangePrincipal);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001DE28 File Offset: 0x0001C028
		private static Uri GetFrontEndHttpServiceUrl<ServiceType>(IExchangePrincipal exchangePrincipal) where ServiceType : HttpService
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (exchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E15MinVersion && FrontEndLocator.IsDatacenter)
			{
				return GlobalServiceUrls.GetExternalUrl<ServiceType>();
			}
			ServiceTopology serviceTopology = FrontEndLocator.IsDatacenter ? ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\FrontEndLocator.cs", "GetFrontEndHttpServiceUrl", 276) : ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\FrontEndLocator.cs", "GetFrontEndHttpServiceUrl", 276);
			ServerVersion serverVersion = new ServerVersion(exchangePrincipal.MailboxInfo.Location.ServerVersion);
			int majorversion = serverVersion.Major;
			IList<ServiceType> services = serviceTopology.FindAll<ServiceType>(exchangePrincipal, ClientAccessType.External, (ServiceType service) => new ServerVersion(service.ServerVersionNumber).Major == majorversion, "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\FrontEndLocator.cs", "GetFrontEndHttpServiceUrl", 281);
			Uri uri = FrontEndLocator.FindServiceInList<ServiceType>(services);
			if (uri == null)
			{
				services = serviceTopology.FindAll<ServiceType>(exchangePrincipal, ClientAccessType.Internal, (ServiceType service) => new ServerVersion(service.ServerVersionNumber).Major == majorversion, "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\FrontEndLocator.cs", "GetFrontEndHttpServiceUrl", 285);
				uri = FrontEndLocator.FindServiceInList<ServiceType>(services);
			}
			if (uri != null)
			{
				ExTraceGlobals.CafeTracer.TraceDebug<string>(0L, "[FrontEndLocator.GetFrontEndHttpServiceUrl] Found HTTP service for the specified back end server {0}.", exchangePrincipal.MailboxInfo.Location.ServerFqdn);
				return uri;
			}
			throw new ServerNotFoundException("Unable to find proper HTTP service.");
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001DF68 File Offset: 0x0001C168
		private static Uri FindServiceInList<ServiceType>(IList<ServiceType> services) where ServiceType : HttpService
		{
			foreach (ServiceType serviceType in services)
			{
				if (serviceType != null)
				{
					return serviceType.Url;
				}
			}
			return null;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		private static bool IsDatacenter
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}
	}
}
