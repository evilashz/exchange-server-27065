using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000F3 RID: 243
	public class RemoveRetentionPolicyTagCommand : SyntheticCommandWithPipelineInput<RetentionPolicyTag, RetentionPolicyTag>
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0003EF5D File Offset: 0x0003D15D
		private RemoveRetentionPolicyTagCommand() : base("Remove-RetentionPolicyTag")
		{
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0003EF6A File Offset: 0x0003D16A
		public RemoveRetentionPolicyTagCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0003EF79 File Offset: 0x0003D179
		public virtual RemoveRetentionPolicyTagCommand SetParameters(RemoveRetentionPolicyTagCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0003EF83 File Offset: 0x0003D183
		public virtual RemoveRetentionPolicyTagCommand SetParameters(RemoveRetentionPolicyTagCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000F4 RID: 244
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700080D RID: 2061
			// (set) Token: 0x06001E3E RID: 7742 RVA: 0x0003EF8D File Offset: 0x0003D18D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700080E RID: 2062
			// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0003EFA0 File Offset: 0x0003D1A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700080F RID: 2063
			// (set) Token: 0x06001E40 RID: 7744 RVA: 0x0003EFB8 File Offset: 0x0003D1B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000810 RID: 2064
			// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000811 RID: 2065
			// (set) Token: 0x06001E42 RID: 7746 RVA: 0x0003EFE8 File Offset: 0x0003D1E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000812 RID: 2066
			// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0003F000 File Offset: 0x0003D200
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000813 RID: 2067
			// (set) Token: 0x06001E44 RID: 7748 RVA: 0x0003F018 File Offset: 0x0003D218
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000F5 RID: 245
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000814 RID: 2068
			// (set) Token: 0x06001E46 RID: 7750 RVA: 0x0003F038 File Offset: 0x0003D238
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RetentionPolicyTagIdParameter(value) : null);
				}
			}

			// Token: 0x17000815 RID: 2069
			// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0003F056 File Offset: 0x0003D256
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000816 RID: 2070
			// (set) Token: 0x06001E48 RID: 7752 RVA: 0x0003F069 File Offset: 0x0003D269
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000817 RID: 2071
			// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0003F081 File Offset: 0x0003D281
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000818 RID: 2072
			// (set) Token: 0x06001E4A RID: 7754 RVA: 0x0003F099 File Offset: 0x0003D299
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000819 RID: 2073
			// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0003F0B1 File Offset: 0x0003D2B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700081A RID: 2074
			// (set) Token: 0x06001E4C RID: 7756 RVA: 0x0003F0C9 File Offset: 0x0003D2C9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700081B RID: 2075
			// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0003F0E1 File Offset: 0x0003D2E1
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
