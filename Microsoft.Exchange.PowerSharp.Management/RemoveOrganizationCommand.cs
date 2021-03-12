using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000B8 RID: 184
	public class RemoveOrganizationCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationIdParameter>
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x0003A376 File Offset: 0x00038576
		private RemoveOrganizationCommand() : base("Remove-Organization")
		{
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0003A383 File Offset: 0x00038583
		public RemoveOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0003A392 File Offset: 0x00038592
		public virtual RemoveOrganizationCommand SetParameters(RemoveOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0003A39C File Offset: 0x0003859C
		public virtual RemoveOrganizationCommand SetParameters(RemoveOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000B9 RID: 185
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170004FB RID: 1275
			// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0003A3A6 File Offset: 0x000385A6
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x170004FC RID: 1276
			// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x0003A3BE File Offset: 0x000385BE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x0003A3D6 File Offset: 0x000385D6
			public virtual SwitchParameter Async
			{
				set
				{
					base.PowerSharpParameters["Async"] = value;
				}
			}

			// Token: 0x170004FE RID: 1278
			// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0003A3EE File Offset: 0x000385EE
			public virtual SwitchParameter AuthoritativeOnly
			{
				set
				{
					base.PowerSharpParameters["AuthoritativeOnly"] = value;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (set) Token: 0x06001ABA RID: 6842 RVA: 0x0003A406 File Offset: 0x00038606
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x17000500 RID: 1280
			// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0003A41E File Offset: 0x0003861E
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (set) Token: 0x06001ABC RID: 6844 RVA: 0x0003A436 File Offset: 0x00038636
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (set) Token: 0x06001ABD RID: 6845 RVA: 0x0003A44E File Offset: 0x0003864E
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0003A466 File Offset: 0x00038666
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000504 RID: 1284
			// (set) Token: 0x06001ABF RID: 6847 RVA: 0x0003A47E File Offset: 0x0003867E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0003A496 File Offset: 0x00038696
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x0003A4AE File Offset: 0x000386AE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000507 RID: 1287
			// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x0003A4C6 File Offset: 0x000386C6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000508 RID: 1288
			// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0003A4DE File Offset: 0x000386DE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000BA RID: 186
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000509 RID: 1289
			// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x0003A4FE File Offset: 0x000386FE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700050A RID: 1290
			// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x0003A51C File Offset: 0x0003871C
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700050B RID: 1291
			// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0003A534 File Offset: 0x00038734
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700050C RID: 1292
			// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x0003A54C File Offset: 0x0003874C
			public virtual SwitchParameter Async
			{
				set
				{
					base.PowerSharpParameters["Async"] = value;
				}
			}

			// Token: 0x1700050D RID: 1293
			// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0003A564 File Offset: 0x00038764
			public virtual SwitchParameter AuthoritativeOnly
			{
				set
				{
					base.PowerSharpParameters["AuthoritativeOnly"] = value;
				}
			}

			// Token: 0x1700050E RID: 1294
			// (set) Token: 0x06001ACA RID: 6858 RVA: 0x0003A57C File Offset: 0x0003877C
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0003A594 File Offset: 0x00038794
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000510 RID: 1296
			// (set) Token: 0x06001ACC RID: 6860 RVA: 0x0003A5AC File Offset: 0x000387AC
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000511 RID: 1297
			// (set) Token: 0x06001ACD RID: 6861 RVA: 0x0003A5C4 File Offset: 0x000387C4
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x17000512 RID: 1298
			// (set) Token: 0x06001ACE RID: 6862 RVA: 0x0003A5DC File Offset: 0x000387DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000513 RID: 1299
			// (set) Token: 0x06001ACF RID: 6863 RVA: 0x0003A5F4 File Offset: 0x000387F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000514 RID: 1300
			// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x0003A60C File Offset: 0x0003880C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000515 RID: 1301
			// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x0003A624 File Offset: 0x00038824
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000516 RID: 1302
			// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x0003A63C File Offset: 0x0003883C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x0003A654 File Offset: 0x00038854
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
