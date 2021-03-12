using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D73 RID: 3443
	public class RemoveSyncDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DistributionGroupIdParameter>
	{
		// Token: 0x0600B7CA RID: 47050 RVA: 0x0010845C File Offset: 0x0010665C
		private RemoveSyncDistributionGroupCommand() : base("Remove-SyncDistributionGroup")
		{
		}

		// Token: 0x0600B7CB RID: 47051 RVA: 0x00108469 File Offset: 0x00106669
		public RemoveSyncDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B7CC RID: 47052 RVA: 0x00108478 File Offset: 0x00106678
		public virtual RemoveSyncDistributionGroupCommand SetParameters(RemoveSyncDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x00108482 File Offset: 0x00106682
		public virtual RemoveSyncDistributionGroupCommand SetParameters(RemoveSyncDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D74 RID: 3444
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700889D RID: 34973
			// (set) Token: 0x0600B7CE RID: 47054 RVA: 0x0010848C File Offset: 0x0010668C
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x1700889E RID: 34974
			// (set) Token: 0x0600B7CF RID: 47055 RVA: 0x001084A4 File Offset: 0x001066A4
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700889F RID: 34975
			// (set) Token: 0x0600B7D0 RID: 47056 RVA: 0x001084BC File Offset: 0x001066BC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088A0 RID: 34976
			// (set) Token: 0x0600B7D1 RID: 47057 RVA: 0x001084CF File Offset: 0x001066CF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088A1 RID: 34977
			// (set) Token: 0x0600B7D2 RID: 47058 RVA: 0x001084E7 File Offset: 0x001066E7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088A2 RID: 34978
			// (set) Token: 0x0600B7D3 RID: 47059 RVA: 0x001084FF File Offset: 0x001066FF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088A3 RID: 34979
			// (set) Token: 0x0600B7D4 RID: 47060 RVA: 0x00108517 File Offset: 0x00106717
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088A4 RID: 34980
			// (set) Token: 0x0600B7D5 RID: 47061 RVA: 0x0010852F File Offset: 0x0010672F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170088A5 RID: 34981
			// (set) Token: 0x0600B7D6 RID: 47062 RVA: 0x00108547 File Offset: 0x00106747
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D75 RID: 3445
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170088A6 RID: 34982
			// (set) Token: 0x0600B7D8 RID: 47064 RVA: 0x00108567 File Offset: 0x00106767
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170088A7 RID: 34983
			// (set) Token: 0x0600B7D9 RID: 47065 RVA: 0x00108585 File Offset: 0x00106785
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170088A8 RID: 34984
			// (set) Token: 0x0600B7DA RID: 47066 RVA: 0x0010859D File Offset: 0x0010679D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170088A9 RID: 34985
			// (set) Token: 0x0600B7DB RID: 47067 RVA: 0x001085B5 File Offset: 0x001067B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088AA RID: 34986
			// (set) Token: 0x0600B7DC RID: 47068 RVA: 0x001085C8 File Offset: 0x001067C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088AB RID: 34987
			// (set) Token: 0x0600B7DD RID: 47069 RVA: 0x001085E0 File Offset: 0x001067E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088AC RID: 34988
			// (set) Token: 0x0600B7DE RID: 47070 RVA: 0x001085F8 File Offset: 0x001067F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088AD RID: 34989
			// (set) Token: 0x0600B7DF RID: 47071 RVA: 0x00108610 File Offset: 0x00106810
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170088AE RID: 34990
			// (set) Token: 0x0600B7E0 RID: 47072 RVA: 0x00108628 File Offset: 0x00106828
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170088AF RID: 34991
			// (set) Token: 0x0600B7E1 RID: 47073 RVA: 0x00108640 File Offset: 0x00106840
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
