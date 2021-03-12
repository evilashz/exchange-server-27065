using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallGwyCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007906 File Offset: 0x00005B06
		public bool StartTransportService
		{
			get
			{
				return this.gatewayRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007913 File Offset: 0x00005B13
		public InstallGwyCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "GatewayRole", "Install-GatewayRole", connection)
		{
			this.gatewayRoleConfigurationInfo = (GatewayRoleConfigurationInfo)base.InstallableUnitConfigurationInfo;
			this.isADDependentRole = false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007940 File Offset: 0x00005B40
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("AdamLdapPort", this.AdamLdapPort);
			base.Parameters.AddWithValue("AdamSslPort", this.AdamSslPort);
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			base.Parameters.AddWithValue("Industry", base.SetupContext.Industry);
			SetupLogger.TraceExit();
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000079D8 File Offset: 0x00005BD8
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000079E5 File Offset: 0x00005BE5
		public ushort AdamLdapPort
		{
			get
			{
				return this.gatewayRoleConfigurationInfo.AdamLdapPort;
			}
			set
			{
				this.gatewayRoleConfigurationInfo.AdamLdapPort = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000079F3 File Offset: 0x00005BF3
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00007A00 File Offset: 0x00005C00
		public ushort AdamSslPort
		{
			get
			{
				return this.gatewayRoleConfigurationInfo.AdamSslPort;
			}
			set
			{
				this.gatewayRoleConfigurationInfo.AdamSslPort = value;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007A10 File Offset: 0x00005C10
		public override void UpdatePreCheckTaskDataHandler()
		{
			base.UpdatePreCheckTaskDataHandler();
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AdamLdapPort = this.AdamLdapPort;
			instance.AdamSslPort = this.AdamSslPort;
		}

		// Token: 0x04000077 RID: 119
		private GatewayRoleConfigurationInfo gatewayRoleConfigurationInfo;
	}
}
