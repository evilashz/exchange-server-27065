using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C0F RID: 3087
	public class RemoveDistributionGroupCommand : SyntheticCommandWithPipelineInputNoOutput<DistributionGroupIdParameter>
	{
		// Token: 0x060095FC RID: 38396 RVA: 0x000DA6E4 File Offset: 0x000D88E4
		private RemoveDistributionGroupCommand() : base("Remove-DistributionGroup")
		{
		}

		// Token: 0x060095FD RID: 38397 RVA: 0x000DA6F1 File Offset: 0x000D88F1
		public RemoveDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060095FE RID: 38398 RVA: 0x000DA700 File Offset: 0x000D8900
		public virtual RemoveDistributionGroupCommand SetParameters(RemoveDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060095FF RID: 38399 RVA: 0x000DA70A File Offset: 0x000D890A
		public virtual RemoveDistributionGroupCommand SetParameters(RemoveDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C10 RID: 3088
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006997 RID: 27031
			// (set) Token: 0x06009600 RID: 38400 RVA: 0x000DA714 File Offset: 0x000D8914
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17006998 RID: 27032
			// (set) Token: 0x06009601 RID: 38401 RVA: 0x000DA72C File Offset: 0x000D892C
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17006999 RID: 27033
			// (set) Token: 0x06009602 RID: 38402 RVA: 0x000DA744 File Offset: 0x000D8944
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700699A RID: 27034
			// (set) Token: 0x06009603 RID: 38403 RVA: 0x000DA75C File Offset: 0x000D895C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700699B RID: 27035
			// (set) Token: 0x06009604 RID: 38404 RVA: 0x000DA76F File Offset: 0x000D896F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700699C RID: 27036
			// (set) Token: 0x06009605 RID: 38405 RVA: 0x000DA787 File Offset: 0x000D8987
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700699D RID: 27037
			// (set) Token: 0x06009606 RID: 38406 RVA: 0x000DA79F File Offset: 0x000D899F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700699E RID: 27038
			// (set) Token: 0x06009607 RID: 38407 RVA: 0x000DA7B7 File Offset: 0x000D89B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700699F RID: 27039
			// (set) Token: 0x06009608 RID: 38408 RVA: 0x000DA7CF File Offset: 0x000D89CF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170069A0 RID: 27040
			// (set) Token: 0x06009609 RID: 38409 RVA: 0x000DA7E7 File Offset: 0x000D89E7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C11 RID: 3089
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170069A1 RID: 27041
			// (set) Token: 0x0600960B RID: 38411 RVA: 0x000DA807 File Offset: 0x000D8A07
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170069A2 RID: 27042
			// (set) Token: 0x0600960C RID: 38412 RVA: 0x000DA825 File Offset: 0x000D8A25
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x170069A3 RID: 27043
			// (set) Token: 0x0600960D RID: 38413 RVA: 0x000DA83D File Offset: 0x000D8A3D
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x170069A4 RID: 27044
			// (set) Token: 0x0600960E RID: 38414 RVA: 0x000DA855 File Offset: 0x000D8A55
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170069A5 RID: 27045
			// (set) Token: 0x0600960F RID: 38415 RVA: 0x000DA86D File Offset: 0x000D8A6D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170069A6 RID: 27046
			// (set) Token: 0x06009610 RID: 38416 RVA: 0x000DA880 File Offset: 0x000D8A80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170069A7 RID: 27047
			// (set) Token: 0x06009611 RID: 38417 RVA: 0x000DA898 File Offset: 0x000D8A98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170069A8 RID: 27048
			// (set) Token: 0x06009612 RID: 38418 RVA: 0x000DA8B0 File Offset: 0x000D8AB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170069A9 RID: 27049
			// (set) Token: 0x06009613 RID: 38419 RVA: 0x000DA8C8 File Offset: 0x000D8AC8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170069AA RID: 27050
			// (set) Token: 0x06009614 RID: 38420 RVA: 0x000DA8E0 File Offset: 0x000D8AE0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170069AB RID: 27051
			// (set) Token: 0x06009615 RID: 38421 RVA: 0x000DA8F8 File Offset: 0x000D8AF8
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
