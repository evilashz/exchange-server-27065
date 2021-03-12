using System;
using System.Management.Automation;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200053B RID: 1339
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class PerimeterConfig : ADContainer
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06003C10 RID: 15376 RVA: 0x000E5E9A File Offset: 0x000E409A
		internal override ADObjectSchema Schema
		{
			get
			{
				return PerimeterConfig.adSchema;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000E5EA1 File Offset: 0x000E40A1
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchTenantPerimeterSettings";
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x000E5EA8 File Offset: 0x000E40A8
		internal override ADObjectId ParentPath
		{
			get
			{
				return PerimeterConfig.parentPath;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000E5EAF File Offset: 0x000E40AF
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x000E5EB7 File Offset: 0x000E40B7
		// (set) Token: 0x06003C15 RID: 15381 RVA: 0x000E5EC9 File Offset: 0x000E40C9
		[Parameter(Mandatory = false)]
		public string PerimeterOrgId
		{
			get
			{
				return (string)this[PerimeterConfigSchema.PerimeterOrgId];
			}
			set
			{
				this[PerimeterConfigSchema.PerimeterOrgId] = value;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000E5ED7 File Offset: 0x000E40D7
		// (set) Token: 0x06003C17 RID: 15383 RVA: 0x000E5EE9 File Offset: 0x000E40E9
		[Parameter(Mandatory = false)]
		public bool SyncToHotmailEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.SyncToHotmailEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.SyncToHotmailEnabled] = value;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000E5EFC File Offset: 0x000E40FC
		// (set) Token: 0x06003C19 RID: 15385 RVA: 0x000E5F0E File Offset: 0x000E410E
		[Parameter(Mandatory = false)]
		public bool RouteOutboundViaEhfEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.RouteOutboundViaEhfEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.RouteOutboundViaEhfEnabled] = value;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000E5F21 File Offset: 0x000E4121
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x000E5F33 File Offset: 0x000E4133
		[Parameter(Mandatory = false)]
		public bool IPSkiplistingEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.IPSkiplistingEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.IPSkiplistingEnabled] = value;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000E5F46 File Offset: 0x000E4146
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000E5F58 File Offset: 0x000E4158
		[Parameter(Mandatory = false)]
		public bool EhfConfigSyncEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.EhfConfigSyncEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.EhfConfigSyncEnabled] = value;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000E5F6B File Offset: 0x000E416B
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000E5F7D File Offset: 0x000E417D
		[Parameter(Mandatory = false)]
		public bool EhfAdminAccountSyncEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.EhfAdminAccountSyncEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.EhfAdminAccountSyncEnabled] = value;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000E5F90 File Offset: 0x000E4190
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000E5FA2 File Offset: 0x000E41A2
		[Parameter(Mandatory = false)]
		public bool IPSafelistingSyncEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.IPSafelistingSyncEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.IPSafelistingSyncEnabled] = value;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000E5FB5 File Offset: 0x000E41B5
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000E5FC7 File Offset: 0x000E41C7
		[Parameter(Mandatory = false)]
		public bool MigrationInProgress
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.MigrationInProgress];
			}
			set
			{
				this[PerimeterConfigSchema.MigrationInProgress] = value;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000E5FDA File Offset: 0x000E41DA
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000E5FEC File Offset: 0x000E41EC
		[Parameter(Mandatory = false)]
		public bool RouteOutboundViaFfoFrontendEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.RouteOutboundViaFfoFrontendEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.RouteOutboundViaFfoFrontendEnabled] = value;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000E5FFF File Offset: 0x000E41FF
		// (set) Token: 0x06003C27 RID: 15399 RVA: 0x000E6011 File Offset: 0x000E4211
		[Parameter(Mandatory = false)]
		public bool EheEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.EheEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.EheEnabled] = value;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000E6024 File Offset: 0x000E4224
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x000E6036 File Offset: 0x000E4236
		[Parameter(Mandatory = false)]
		public bool RMSOFwdSyncEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.RMSOFwdSyncEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.RMSOFwdSyncEnabled] = value;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000E6049 File Offset: 0x000E4249
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000E605B File Offset: 0x000E425B
		[Parameter(Mandatory = false)]
		public bool EheDecryptEnabled
		{
			get
			{
				return (bool)this[PerimeterConfigSchema.EheDecryptEnabled];
			}
			set
			{
				this[PerimeterConfigSchema.EheDecryptEnabled] = value;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000E606E File Offset: 0x000E426E
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000E6080 File Offset: 0x000E4280
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> GatewayIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[PerimeterConfigSchema.GatewayIPAddresses];
			}
			set
			{
				this[PerimeterConfigSchema.GatewayIPAddresses] = value;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000E608E File Offset: 0x000E428E
		// (set) Token: 0x06003C2F RID: 15407 RVA: 0x000E60A0 File Offset: 0x000E42A0
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> InternalServerIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[PerimeterConfigSchema.InternalServerIPAddresses];
			}
			set
			{
				this[PerimeterConfigSchema.InternalServerIPAddresses] = value;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x000E60AE File Offset: 0x000E42AE
		// (set) Token: 0x06003C31 RID: 15409 RVA: 0x000E60C0 File Offset: 0x000E42C0
		[Parameter(Mandatory = false)]
		public SmtpDomain PartnerRoutingDomain
		{
			get
			{
				return (SmtpDomain)this[PerimeterConfigSchema.PartnerRoutingDomain];
			}
			set
			{
				this[PerimeterConfigSchema.PartnerRoutingDomain] = value;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000E60CE File Offset: 0x000E42CE
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x000E60E0 File Offset: 0x000E42E0
		[Parameter(Mandatory = false)]
		public SmtpDomain PartnerConnectorDomain
		{
			get
			{
				return (SmtpDomain)this[PerimeterConfigSchema.PartnerConnectorDomain];
			}
			set
			{
				this[PerimeterConfigSchema.PartnerConnectorDomain] = value;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x000E60EE File Offset: 0x000E42EE
		// (set) Token: 0x06003C35 RID: 15413 RVA: 0x000E6100 File Offset: 0x000E4300
		public ADObjectId MailFlowPartner
		{
			get
			{
				return (ADObjectId)this[PerimeterConfigSchema.MailFlowPartner];
			}
			set
			{
				this[PerimeterConfigSchema.MailFlowPartner] = value;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000E610E File Offset: 0x000E430E
		// (set) Token: 0x06003C37 RID: 15415 RVA: 0x000E6120 File Offset: 0x000E4320
		[Parameter(Mandatory = false)]
		public SafelistingUIMode SafelistingUIMode
		{
			get
			{
				return (SafelistingUIMode)((int)this[PerimeterConfigSchema.SafelistingUIMode]);
			}
			set
			{
				this[PerimeterConfigSchema.SafelistingUIMode] = (int)value;
			}
		}

		// Token: 0x040028AB RID: 10411
		private const string MostDerivedClassInternal = "msExchTenantPerimeterSettings";

		// Token: 0x040028AC RID: 10412
		private static readonly PerimeterConfigSchema adSchema = ObjectSchema.GetInstance<PerimeterConfigSchema>();

		// Token: 0x040028AD RID: 10413
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Transport Settings");
	}
}
