using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000245 RID: 581
	internal static class ProxyEventLogHelper
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0004BA10 File Offset: 0x00049C10
		public static void LogServicesDiscoveryFailure(string serverFQDN)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_ServiceDiscoveryFailure, "EWSProxy_ServiceDiscoveryFailure", new object[]
			{
				localServerInformation.Name,
				localServerInformation.ExchangeVersion,
				SingleProxyDeterministicCASBoxScoring.GetSiteIdForServer(serverFQDN),
				serverFQDN
			});
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0004BA64 File Offset: 0x00049C64
		public static void LogNoApplicableDestinationCAS(string destinationServerFQDN)
		{
			string siteIdForServer = SingleProxyDeterministicCASBoxScoring.GetSiteIdForServer(destinationServerFQDN);
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_NoApplicableDestinationCAS, "EWSProxy_NoApplicableDestinationCAS", new object[]
			{
				localServerInformation.Name,
				localServerInformation.SiteDistinguishedName,
				siteIdForServer,
				siteIdForServer
			});
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		public static void LogNoRespondingDestinationCAS(string siteId)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_NoRespondingDestinationCAS, string.Empty, new object[]
			{
				localServerInformation.Name,
				siteId,
				siteId
			});
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0004BAFC File Offset: 0x00049CFC
		public static void LogCallerDeniedProxyRight(SecurityIdentifier callerSid)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_ProxyRightDenied, "EWSProxy_ProxyRightDenied_" + callerSid.ToString(), new object[]
			{
				callerSid,
				localServerInformation.Name,
				localServerInformation.Name
			});
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0004BB50 File Offset: 0x00049D50
		public static void LogNoTrustedCertificateOnDestinationCAS(string destinationServerFQDN)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_NoTrustedCertificateOnDestinationCAS, "EWSProxy_NoTrustedCertificateOnDestinationCAS" + destinationServerFQDN, new object[]
			{
				localServerInformation.Name,
				destinationServerFQDN,
				localServerInformation.Name
			});
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0004BBA0 File Offset: 0x00049DA0
		public static void LogKerberosConfigurationProblem(WebServicesInfo destinationEws)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_KerberosConfigurationProblem, "EWSProxy_KerberosConfigurationProblem_" + destinationEws.ServerFullyQualifiedDomainName, new object[]
			{
				localServerInformation.Name,
				destinationEws.ServerFullyQualifiedDomainName,
				destinationEws.ServerFullyQualifiedDomainName,
				destinationEws.ServerFullyQualifiedDomainName
			});
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0004BC00 File Offset: 0x00049E00
		public static void LogExceededGroupSidLimit(SecurityIdentifier userSid, int sidLimit)
		{
			ProxyEventLogHelper.LocalServerInformation localServerInformation = ProxyEventLogHelper.LocalServerInformation.Create();
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_ExceededGroupSidLimit, "EWSProxy_ExceededGroupSidLimit_" + userSid.ToString(), new object[]
			{
				localServerInformation.Name,
				userSid,
				sidLimit
			});
		}

		// Token: 0x02000246 RID: 582
		private struct LocalServerInformation
		{
			// Token: 0x06000F62 RID: 3938 RVA: 0x0004BC54 File Offset: 0x00049E54
			internal static ProxyEventLogHelper.LocalServerInformation Create()
			{
				Server server = null;
				try
				{
					FaultInjection.GenerateFault((FaultInjection.LIDs)3454414141U);
					server = LocalServer.GetServer();
				}
				catch (ADTransientException arg)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<ADTransientException>(0L, "LocalServerInformation constructor encountered exception in LocalServer.GetServer() call: {0}", arg);
				}
				return new ProxyEventLogHelper.LocalServerInformation(server);
			}

			// Token: 0x06000F63 RID: 3939 RVA: 0x0004BCA0 File Offset: 0x00049EA0
			private LocalServerInformation(Server server)
			{
				if (server == null)
				{
					this.name = string.Empty;
					this.exchangeVersion = string.Empty;
					this.siteDistinguishedName = string.Empty;
					return;
				}
				this.name = server.Name;
				this.exchangeVersion = server.ExchangeVersion.ToString();
				this.siteDistinguishedName = server.ServerSite.DistinguishedName;
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0004BD00 File Offset: 0x00049F00
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0004BD08 File Offset: 0x00049F08
			internal string ExchangeVersion
			{
				get
				{
					return this.exchangeVersion;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0004BD10 File Offset: 0x00049F10
			internal string SiteDistinguishedName
			{
				get
				{
					return this.siteDistinguishedName;
				}
			}

			// Token: 0x04000BB0 RID: 2992
			private string name;

			// Token: 0x04000BB1 RID: 2993
			private string exchangeVersion;

			// Token: 0x04000BB2 RID: 2994
			private string siteDistinguishedName;
		}
	}
}
