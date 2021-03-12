using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200054F RID: 1359
	internal class UnifiedMessagingCallRouterEndpoint : UnifiedMessagingEndpoint
	{
		// Token: 0x060021D9 RID: 8665 RVA: 0x000CD3A0 File Offset: 0x000CB5A0
		public override bool DetectChange()
		{
			Server server = base.TopologyConfigurationSession.FindServerByName(Environment.MachineName);
			if (server != null && server.IsCafeServer)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, base.TraceContext, "Detecting Changes on UMCallRouter configuration", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingCallRouterEndpoint.cs", 39);
				return base.HasAnyUMPropertyChanged(server);
			}
			return false;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000CD3F4 File Offset: 0x000CB5F4
		public override void Initialize()
		{
			base.TopologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 53, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingCallRouterEndpoint.cs");
			base.Server = base.TopologyConfigurationSession.FindServerByName(Environment.MachineName);
			if (base.Server == null || !base.Server.IsCafeServer)
			{
				return;
			}
			UMStartupMode startupMode;
			int sipTcpListeningPort;
			int sipTlsListeningPort;
			string certificateThumbprint;
			this.GetUMPropertiesFromAD(base.Server, out startupMode, out sipTcpListeningPort, out sipTlsListeningPort, out certificateThumbprint);
			base.StartupMode = startupMode;
			base.SipTcpListeningPort = sipTcpListeningPort;
			base.SipTlsListeningPort = sipTlsListeningPort;
			base.CertificateThumbprint = certificateThumbprint;
			base.CertificateSubjectName = base.GetCertificateSubjectNameFromThumbprint(certificateThumbprint);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000CD48C File Offset: 0x000CB68C
		protected override void GetUMPropertiesFromAD(Server server, out UMStartupMode startupMode, out int sipTcpListeningPort, out int sipTlsListeningPort, out string certificateThumbprint)
		{
			sipTcpListeningPort = 0;
			sipTlsListeningPort = 0;
			certificateThumbprint = null;
			startupMode = UMStartupMode.TCP;
			if (server != null && server.IsCafeServer)
			{
				SIPFEServerConfiguration sipfeserverConfiguration = SIPFEServerConfiguration.Find();
				sipTcpListeningPort = sipfeserverConfiguration.SipTcpListeningPort;
				sipTlsListeningPort = sipfeserverConfiguration.SipTlsListeningPort;
				startupMode = sipfeserverConfiguration.UMStartupMode;
				certificateThumbprint = sipfeserverConfiguration.UMCertificateThumbprint;
			}
		}
	}
}
