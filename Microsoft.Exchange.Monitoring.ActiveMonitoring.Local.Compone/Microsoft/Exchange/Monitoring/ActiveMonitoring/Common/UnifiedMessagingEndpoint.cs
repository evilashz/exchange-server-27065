using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200054E RID: 1358
	internal abstract class UnifiedMessagingEndpoint : IEndpoint
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000CD133 File Offset: 0x000CB333
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x000CD13B File Offset: 0x000CB33B
		public int SipTcpListeningPort { get; protected set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x000CD144 File Offset: 0x000CB344
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x000CD14C File Offset: 0x000CB34C
		public int SipTlsListeningPort { get; protected set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x000CD155 File Offset: 0x000CB355
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x000CD15D File Offset: 0x000CB35D
		public string CertificateThumbprint { get; protected set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000CD166 File Offset: 0x000CB366
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x000CD16E File Offset: 0x000CB36E
		public UMStartupMode StartupMode { get; protected set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000CD177 File Offset: 0x000CB377
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x000CD17F File Offset: 0x000CB37F
		public string CertificateSubjectName { get; protected set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000CD188 File Offset: 0x000CB388
		protected TracingContext TraceContext
		{
			get
			{
				return this.traceContext;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000CD190 File Offset: 0x000CB390
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x000CD198 File Offset: 0x000CB398
		protected ITopologyConfigurationSession TopologyConfigurationSession { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000CD1A1 File Offset: 0x000CB3A1
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000CD1A9 File Offset: 0x000CB3A9
		protected Server Server { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000CD1B2 File Offset: 0x000CB3B2
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000CD1B5 File Offset: 0x000CB3B5
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x000CD1BD File Offset: 0x000CB3BD
		public Exception Exception { get; set; }

		// Token: 0x060021D2 RID: 8658
		public abstract bool DetectChange();

		// Token: 0x060021D3 RID: 8659
		public abstract void Initialize();

		// Token: 0x060021D4 RID: 8660
		protected abstract void GetUMPropertiesFromAD(Server server, out UMStartupMode startupMode, out int sipTcpListeningPort, out int sipTlsListeningPort, out string certificateThumbprint);

		// Token: 0x060021D5 RID: 8661 RVA: 0x000CD1C8 File Offset: 0x000CB3C8
		protected string GetCertificateSubjectNameFromThumbprint(string certificateThumbprint)
		{
			string result;
			if (string.IsNullOrEmpty(certificateThumbprint))
			{
				result = string.Empty;
			}
			else
			{
				result = Utils.GetCertificateSubjectNameFromThumbprint(this.TraceContext, certificateThumbprint);
			}
			return result;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000CD1F4 File Offset: 0x000CB3F4
		protected bool HasAnyUMPropertyChanged(Server server)
		{
			UMStartupMode umstartupMode;
			int num;
			int num2;
			string text;
			this.GetUMPropertiesFromAD(server, out umstartupMode, out num, out num2, out text);
			if (this.StartupMode != umstartupMode)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, this.TraceContext, string.Format("UMStartupMode changed Initial Value {0} New Value {1}", this.StartupMode, umstartupMode), null, "HasAnyUMPropertyChanged", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingEndpoint.cs", 179);
				return true;
			}
			if (this.SipTcpListeningPort != num)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, this.TraceContext, string.Format("SipTCPListeningPort changed Initial Value {0} New Value {1}", this.SipTcpListeningPort, num), null, "HasAnyUMPropertyChanged", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingEndpoint.cs", 185);
				return true;
			}
			if (this.SipTlsListeningPort != num2)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, this.TraceContext, string.Format("SipTLSListeningPort changed Initial Value {0} New Value {1}", this.SipTlsListeningPort, num2), null, "HasAnyUMPropertyChanged", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingEndpoint.cs", 191);
				return true;
			}
			if (!string.IsNullOrEmpty(this.CertificateThumbprint) && !string.IsNullOrEmpty(text))
			{
				if (!this.CertificateThumbprint.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, this.TraceContext, string.Format("CertificateThumbprint changed Initial Value {0} New Value {1}", this.CertificateThumbprint, text), null, "HasAnyUMPropertyChanged", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingEndpoint.cs", 199);
					return true;
				}
			}
			else if (!string.IsNullOrEmpty(this.CertificateThumbprint) || !string.IsNullOrEmpty(text))
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.UnifiedMessagingEndpointTracer, this.TraceContext, string.Format("CertificateThumbprint changed Initial Value {0} New Value {1}", this.CertificateThumbprint, text), null, "HasAnyUMPropertyChanged", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\UnifiedMessagingEndpoint.cs", 207);
				return true;
			}
			return false;
		}

		// Token: 0x0400189D RID: 6301
		private TracingContext traceContext = TracingContext.Default;
	}
}
