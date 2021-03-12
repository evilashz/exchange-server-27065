using System;
using System.Xml.Linq;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000027 RID: 39
	internal class MigrationTestIntegration : TestIntegrationBase
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00008E98 File Offset: 0x00007098
		public MigrationTestIntegration(bool autoRefresh = false) : base("SOFTWARE\\Microsoft\\Exchange_Test\\v15\\MigrationService", autoRefresh)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008EA6 File Offset: 0x000070A6
		public static MigrationTestIntegration Instance
		{
			get
			{
				return MigrationTestIntegration.SingletonInstance;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008EAD File Offset: 0x000070AD
		public bool IsMigrationNotificationRpcEndpointEnabled
		{
			get
			{
				return base.GetFlagValue("IsMigrationNotificationRpcEndpointEnabled", true);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008EBB File Offset: 0x000070BB
		public bool IsMigrationServiceRpcEndpointEnabled
		{
			get
			{
				return base.GetFlagValue("SyncMigrationIsMigrationServiceRpcEndpointEnabled", true);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008EC9 File Offset: 0x000070C9
		public bool IsMigrationProxyRpcClientEnabled
		{
			get
			{
				return base.GetFlagValue("MigrationProxyRpcClientEnabled", false);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008ED7 File Offset: 0x000070D7
		public string MigrationAccessorEndpoint
		{
			get
			{
				return base.GetStrValue("SyncMigrationAccessorEndpoint");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008EE4 File Offset: 0x000070E4
		public string MigrationFaultInjectionHandler
		{
			get
			{
				return base.GetStrValue("MigrationFaultInjectionHandler");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008EF1 File Offset: 0x000070F1
		public string ReportMessageEndpoint
		{
			get
			{
				return base.GetStrValue("ReportMessageEndpoint");
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008F00 File Offset: 0x00007100
		public XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("TestIntegration", new XElement("Enabled", base.Enabled));
			if (base.Enabled)
			{
				xelement.Add(new XElement("ReportMessageEndpoint", this.ReportMessageEndpoint));
				xelement.Add(new XElement("MigrationAccessorEndpoint", this.MigrationAccessorEndpoint));
				xelement.Add(new XElement("IsMigrationProxyRpcClientEnabled", this.IsMigrationProxyRpcClientEnabled));
				xelement.Add(new XElement("IsMigrationServiceRpcEndpointEnabled", this.IsMigrationServiceRpcEndpointEnabled));
				xelement.Add(new XElement("IsMigrationNotificationRpcEndpointEnabled", this.IsMigrationNotificationRpcEndpointEnabled));
				xelement.Add(new XElement("MigrationFaultInjectionHandler", this.MigrationFaultInjectionHandler));
			}
			return xelement;
		}

		// Token: 0x0400009F RID: 159
		public const string RegKeyName = "SOFTWARE\\Microsoft\\Exchange_Test\\v15\\MigrationService";

		// Token: 0x040000A0 RID: 160
		public const string IsMigrationNotificationRpcEndpointEnabledName = "IsMigrationNotificationRpcEndpointEnabled";

		// Token: 0x040000A1 RID: 161
		public const string IsMigrationServiceRpcEndpointEnabledName = "SyncMigrationIsMigrationServiceRpcEndpointEnabled";

		// Token: 0x040000A2 RID: 162
		internal const string IsMigrationProxyRpcClientEnabledName = "MigrationProxyRpcClientEnabled";

		// Token: 0x040000A3 RID: 163
		internal const string MigrationAccessorEndpointName = "SyncMigrationAccessorEndpoint";

		// Token: 0x040000A4 RID: 164
		internal const string MigrationFaultInjectionHandlerName = "MigrationFaultInjectionHandler";

		// Token: 0x040000A5 RID: 165
		internal const string ReportMessageEndpointName = "ReportMessageEndpoint";

		// Token: 0x040000A6 RID: 166
		private static readonly MigrationTestIntegration SingletonInstance = new MigrationTestIntegration(true);
	}
}
