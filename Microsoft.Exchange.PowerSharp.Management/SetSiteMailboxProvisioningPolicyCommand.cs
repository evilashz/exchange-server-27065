using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001E4 RID: 484
	public class SetSiteMailboxProvisioningPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x060027B7 RID: 10167 RVA: 0x0004B496 File Offset: 0x00049696
		private SetSiteMailboxProvisioningPolicyCommand() : base("Set-SiteMailboxProvisioningPolicy")
		{
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0004B4A3 File Offset: 0x000496A3
		public SetSiteMailboxProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0004B4B2 File Offset: 0x000496B2
		public virtual SetSiteMailboxProvisioningPolicyCommand SetParameters(SetSiteMailboxProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0004B4BC File Offset: 0x000496BC
		public virtual SetSiteMailboxProvisioningPolicyCommand SetParameters(SetSiteMailboxProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001E5 RID: 485
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000FA8 RID: 4008
			// (set) Token: 0x060027BB RID: 10171 RVA: 0x0004B4C6 File Offset: 0x000496C6
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000FA9 RID: 4009
			// (set) Token: 0x060027BC RID: 10172 RVA: 0x0004B4DE File Offset: 0x000496DE
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000FAA RID: 4010
			// (set) Token: 0x060027BD RID: 10173 RVA: 0x0004B4F6 File Offset: 0x000496F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FAB RID: 4011
			// (set) Token: 0x060027BE RID: 10174 RVA: 0x0004B509 File Offset: 0x00049709
			public virtual ByteQuantifiedSize MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17000FAC RID: 4012
			// (set) Token: 0x060027BF RID: 10175 RVA: 0x0004B521 File Offset: 0x00049721
			public virtual ByteQuantifiedSize IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000FAD RID: 4013
			// (set) Token: 0x060027C0 RID: 10176 RVA: 0x0004B539 File Offset: 0x00049739
			public virtual ByteQuantifiedSize ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17000FAE RID: 4014
			// (set) Token: 0x060027C1 RID: 10177 RVA: 0x0004B551 File Offset: 0x00049751
			public virtual bool DefaultAliasPrefixEnabled
			{
				set
				{
					base.PowerSharpParameters["DefaultAliasPrefixEnabled"] = value;
				}
			}

			// Token: 0x17000FAF RID: 4015
			// (set) Token: 0x060027C2 RID: 10178 RVA: 0x0004B569 File Offset: 0x00049769
			public virtual string AliasPrefix
			{
				set
				{
					base.PowerSharpParameters["AliasPrefix"] = value;
				}
			}

			// Token: 0x17000FB0 RID: 4016
			// (set) Token: 0x060027C3 RID: 10179 RVA: 0x0004B57C File Offset: 0x0004977C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000FB1 RID: 4017
			// (set) Token: 0x060027C4 RID: 10180 RVA: 0x0004B58F File Offset: 0x0004978F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FB2 RID: 4018
			// (set) Token: 0x060027C5 RID: 10181 RVA: 0x0004B5A7 File Offset: 0x000497A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FB3 RID: 4019
			// (set) Token: 0x060027C6 RID: 10182 RVA: 0x0004B5BF File Offset: 0x000497BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FB4 RID: 4020
			// (set) Token: 0x060027C7 RID: 10183 RVA: 0x0004B5D7 File Offset: 0x000497D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000FB5 RID: 4021
			// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0004B5EF File Offset: 0x000497EF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001E6 RID: 486
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000FB6 RID: 4022
			// (set) Token: 0x060027CA RID: 10186 RVA: 0x0004B60F File Offset: 0x0004980F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000FB7 RID: 4023
			// (set) Token: 0x060027CB RID: 10187 RVA: 0x0004B62D File Offset: 0x0004982D
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000FB8 RID: 4024
			// (set) Token: 0x060027CC RID: 10188 RVA: 0x0004B645 File Offset: 0x00049845
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000FB9 RID: 4025
			// (set) Token: 0x060027CD RID: 10189 RVA: 0x0004B65D File Offset: 0x0004985D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FBA RID: 4026
			// (set) Token: 0x060027CE RID: 10190 RVA: 0x0004B670 File Offset: 0x00049870
			public virtual ByteQuantifiedSize MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17000FBB RID: 4027
			// (set) Token: 0x060027CF RID: 10191 RVA: 0x0004B688 File Offset: 0x00049888
			public virtual ByteQuantifiedSize IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000FBC RID: 4028
			// (set) Token: 0x060027D0 RID: 10192 RVA: 0x0004B6A0 File Offset: 0x000498A0
			public virtual ByteQuantifiedSize ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17000FBD RID: 4029
			// (set) Token: 0x060027D1 RID: 10193 RVA: 0x0004B6B8 File Offset: 0x000498B8
			public virtual bool DefaultAliasPrefixEnabled
			{
				set
				{
					base.PowerSharpParameters["DefaultAliasPrefixEnabled"] = value;
				}
			}

			// Token: 0x17000FBE RID: 4030
			// (set) Token: 0x060027D2 RID: 10194 RVA: 0x0004B6D0 File Offset: 0x000498D0
			public virtual string AliasPrefix
			{
				set
				{
					base.PowerSharpParameters["AliasPrefix"] = value;
				}
			}

			// Token: 0x17000FBF RID: 4031
			// (set) Token: 0x060027D3 RID: 10195 RVA: 0x0004B6E3 File Offset: 0x000498E3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000FC0 RID: 4032
			// (set) Token: 0x060027D4 RID: 10196 RVA: 0x0004B6F6 File Offset: 0x000498F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FC1 RID: 4033
			// (set) Token: 0x060027D5 RID: 10197 RVA: 0x0004B70E File Offset: 0x0004990E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FC2 RID: 4034
			// (set) Token: 0x060027D6 RID: 10198 RVA: 0x0004B726 File Offset: 0x00049926
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FC3 RID: 4035
			// (set) Token: 0x060027D7 RID: 10199 RVA: 0x0004B73E File Offset: 0x0004993E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000FC4 RID: 4036
			// (set) Token: 0x060027D8 RID: 10200 RVA: 0x0004B756 File Offset: 0x00049956
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
