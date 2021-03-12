using System;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200054C RID: 1356
	internal class ExchangeServerRoleEndpoint : IEndpoint
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000CCE0B File Offset: 0x000CB00B
		public bool IsBridgeheadRoleInstalled
		{
			get
			{
				return this.isBridgeheadRoleInstalled;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x000CCE13 File Offset: 0x000CB013
		public bool IsGatewayRoleInstalled
		{
			get
			{
				return this.isGatewayRoleInstalled;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000CCE1B File Offset: 0x000CB01B
		public bool IsClientAccessRoleInstalled
		{
			get
			{
				return this.isClientAccessRoleInstalled;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x000CCE23 File Offset: 0x000CB023
		public bool IsMailboxRoleInstalled
		{
			get
			{
				return this.isMailboxRoleInstalled;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000CCE2B File Offset: 0x000CB02B
		public bool IsUnifiedMessagingRoleInstalled
		{
			get
			{
				return this.isUnifiedMessagingRoleInstalled;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x000CCE33 File Offset: 0x000CB033
		public bool IsFrontendTransportRoleInstalled
		{
			get
			{
				return this.isFrontendTransportRoleInstalled;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000CCE3B File Offset: 0x000CB03B
		public bool IsAdminToolsRoleInstalled
		{
			get
			{
				return this.isAdminToolsRoleInstalled;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000CCE43 File Offset: 0x000CB043
		public bool IsMonitoringRoleInstalled
		{
			get
			{
				return this.isMonitoringRoleInstalled;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000CCE4B File Offset: 0x000CB04B
		public bool IsCentralAdminRoleInstalled
		{
			get
			{
				return this.isCentralAdminRoleInstalled;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000CCE53 File Offset: 0x000CB053
		public bool IsCentralAdminDatabaseRoleInstalled
		{
			get
			{
				return this.isCentralAdminDatabaseRoleInstalled;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000CCE5B File Offset: 0x000CB05B
		public bool IsCentralAdminFrontEndRoleInstalled
		{
			get
			{
				return this.isCentralAdminFrontEndRoleInstalled;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000CCE63 File Offset: 0x000CB063
		public bool IsLanguangePacksRoleInstalled
		{
			get
			{
				return this.isLanguangePacksRoleInstalled;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x000CCE6B File Offset: 0x000CB06B
		public bool IsCafeRoleInstalled
		{
			get
			{
				return this.isCafeRoleInstalled;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000CCE73 File Offset: 0x000CB073
		public bool IsFfoWebServiceRoleInstalled
		{
			get
			{
				return this.isFfoWebServiceRoleInstalled;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000CCE7B File Offset: 0x000CB07B
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000CCE7E File Offset: 0x000CB07E
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x000CCE86 File Offset: 0x000CB086
		public Exception Exception { get; set; }

		// Token: 0x060021B6 RID: 8630 RVA: 0x000CCE90 File Offset: 0x000CB090
		public void Initialize()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.ExchangeServerRoleEndpointTracer, this.traceContext, "Checking Exchange server role configuration", null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ExchangeServerRoleEndpoint.cs", 296);
			this.isBridgeheadRoleInstalled = new BridgeheadRole().IsInstalled;
			this.isGatewayRoleInstalled = new GatewayRole().IsInstalled;
			this.isClientAccessRoleInstalled = new ClientAccessRole().IsInstalled;
			this.isMailboxRoleInstalled = new MailboxRole().IsInstalled;
			this.isUnifiedMessagingRoleInstalled = new UnifiedMessagingRole().IsInstalled;
			this.isFrontendTransportRoleInstalled = new FrontendTransportRole().IsInstalled;
			this.isAdminToolsRoleInstalled = new AdminToolsRole().IsInstalled;
			this.isMonitoringRoleInstalled = new MonitoringRole().IsInstalled;
			this.isCentralAdminRoleInstalled = new CentralAdminRole().IsInstalled;
			this.isCentralAdminDatabaseRoleInstalled = new CentralAdminDatabaseRole().IsInstalled;
			this.isCentralAdminFrontEndRoleInstalled = new CentralAdminFrontEndRole().IsInstalled;
			this.isLanguangePacksRoleInstalled = new LanguagePacksRole().IsInstalled;
			this.isCafeRoleInstalled = new CafeRole().IsInstalled;
			this.isFfoWebServiceRoleInstalled = new FfoWebServiceRole().IsInstalled;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000CCFA4 File Offset: 0x000CB1A4
		public bool DetectChange()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.ExchangeServerRoleEndpointTracer, this.traceContext, "Detecting Exchange server role configuration", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\ExchangeServerRoleEndpoint.cs", 319);
			return this.isBridgeheadRoleInstalled != new BridgeheadRole().IsInstalled || this.isGatewayRoleInstalled != new GatewayRole().IsInstalled || this.isClientAccessRoleInstalled != new ClientAccessRole().IsInstalled || this.isMailboxRoleInstalled != new MailboxRole().IsInstalled || this.isUnifiedMessagingRoleInstalled != new UnifiedMessagingRole().IsInstalled || this.isFrontendTransportRoleInstalled != new FrontendTransportRole().IsInstalled || this.isAdminToolsRoleInstalled != new AdminToolsRole().IsInstalled || this.isMonitoringRoleInstalled != new MonitoringRole().IsInstalled || this.isCentralAdminRoleInstalled != new CentralAdminRole().IsInstalled || this.isCentralAdminDatabaseRoleInstalled != new CentralAdminDatabaseRole().IsInstalled || this.isCentralAdminFrontEndRoleInstalled != new CentralAdminFrontEndRole().IsInstalled || this.isLanguangePacksRoleInstalled != new LanguagePacksRole().IsInstalled || this.isCafeRoleInstalled != new CafeRole().IsInstalled || this.isFfoWebServiceRoleInstalled != new FfoWebServiceRole().IsInstalled;
		}

		// Token: 0x0400188A RID: 6282
		private bool isBridgeheadRoleInstalled;

		// Token: 0x0400188B RID: 6283
		private bool isGatewayRoleInstalled;

		// Token: 0x0400188C RID: 6284
		private bool isClientAccessRoleInstalled;

		// Token: 0x0400188D RID: 6285
		private bool isMailboxRoleInstalled;

		// Token: 0x0400188E RID: 6286
		private bool isUnifiedMessagingRoleInstalled;

		// Token: 0x0400188F RID: 6287
		private bool isFrontendTransportRoleInstalled;

		// Token: 0x04001890 RID: 6288
		private bool isAdminToolsRoleInstalled;

		// Token: 0x04001891 RID: 6289
		private bool isMonitoringRoleInstalled;

		// Token: 0x04001892 RID: 6290
		private bool isCentralAdminRoleInstalled;

		// Token: 0x04001893 RID: 6291
		private bool isCentralAdminDatabaseRoleInstalled;

		// Token: 0x04001894 RID: 6292
		private bool isCentralAdminFrontEndRoleInstalled;

		// Token: 0x04001895 RID: 6293
		private bool isLanguangePacksRoleInstalled;

		// Token: 0x04001896 RID: 6294
		private bool isCafeRoleInstalled;

		// Token: 0x04001897 RID: 6295
		private bool isFfoWebServiceRoleInstalled;

		// Token: 0x04001898 RID: 6296
		private TracingContext traceContext = TracingContext.Default;
	}
}
