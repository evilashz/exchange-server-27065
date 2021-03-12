using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000550 RID: 1360
	internal class UnifiedMessagingServiceEndpoint : UnifiedMessagingEndpoint
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x000CD4E4 File Offset: 0x000CB6E4
		public override bool DetectChange()
		{
			Server server = base.TopologyConfigurationSession.FindServerByName(Environment.MachineName);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, base.TraceContext, "Detecting Changes on UMServer configuration", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingServiceEndpoint.cs", 39);
			return server != null && server.IsUnifiedMessagingServer && base.HasAnyUMPropertyChanged(server);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000CD538 File Offset: 0x000CB738
		public override void Initialize()
		{
			base.TopologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 55, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingServiceEndpoint.cs");
			base.Server = base.TopologyConfigurationSession.FindServerByName(Environment.MachineName);
			if (base.Server == null || !base.Server.IsUnifiedMessagingServer)
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

		// Token: 0x060021DF RID: 8671 RVA: 0x000CD5D0 File Offset: 0x000CB7D0
		protected override void GetUMPropertiesFromAD(Server server, out UMStartupMode startupMode, out int sipTcpListeningPort, out int sipTlsListeningPort, out string certificateThumbprint)
		{
			sipTcpListeningPort = 0;
			sipTlsListeningPort = 0;
			certificateThumbprint = null;
			startupMode = UMStartupMode.TCP;
			if (server.IsUnifiedMessagingServer)
			{
				UMServer umserver = new UMServer(server);
				sipTcpListeningPort = umserver.SipTcpListeningPort;
				sipTlsListeningPort = umserver.SipTlsListeningPort;
				startupMode = umserver.UMStartupMode;
				certificateThumbprint = umserver.UMCertificateThumbprint;
			}
		}
	}
}
