using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000F6 RID: 246
	public class SetMailboxComplianceConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<ADUser>
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x0003F101 File Offset: 0x0003D301
		private SetMailboxComplianceConfigurationCommand() : base("Set-MailboxComplianceConfiguration")
		{
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0003F10E File Offset: 0x0003D30E
		public SetMailboxComplianceConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0003F11D File Offset: 0x0003D31D
		public virtual SetMailboxComplianceConfigurationCommand SetParameters(SetMailboxComplianceConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0003F127 File Offset: 0x0003D327
		public virtual SetMailboxComplianceConfigurationCommand SetParameters(SetMailboxComplianceConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000F7 RID: 247
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700081C RID: 2076
			// (set) Token: 0x06001E53 RID: 7763 RVA: 0x0003F131 File Offset: 0x0003D331
			public virtual bool RetentionAutoTaggingEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionAutoTaggingEnabled"] = value;
				}
			}

			// Token: 0x1700081D RID: 2077
			// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0003F149 File Offset: 0x0003D349
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700081E RID: 2078
			// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0003F161 File Offset: 0x0003D361
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700081F RID: 2079
			// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0003F174 File Offset: 0x0003D374
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000820 RID: 2080
			// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0003F187 File Offset: 0x0003D387
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000821 RID: 2081
			// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0003F19F File Offset: 0x0003D39F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000822 RID: 2082
			// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0003F1B7 File Offset: 0x0003D3B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000823 RID: 2083
			// (set) Token: 0x06001E5A RID: 7770 RVA: 0x0003F1CF File Offset: 0x0003D3CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000824 RID: 2084
			// (set) Token: 0x06001E5B RID: 7771 RVA: 0x0003F1E7 File Offset: 0x0003D3E7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000F8 RID: 248
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000825 RID: 2085
			// (set) Token: 0x06001E5D RID: 7773 RVA: 0x0003F207 File Offset: 0x0003D407
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000826 RID: 2086
			// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0003F225 File Offset: 0x0003D425
			public virtual bool RetentionAutoTaggingEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionAutoTaggingEnabled"] = value;
				}
			}

			// Token: 0x17000827 RID: 2087
			// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0003F23D File Offset: 0x0003D43D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17000828 RID: 2088
			// (set) Token: 0x06001E60 RID: 7776 RVA: 0x0003F255 File Offset: 0x0003D455
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000829 RID: 2089
			// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0003F268 File Offset: 0x0003D468
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700082A RID: 2090
			// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0003F27B File Offset: 0x0003D47B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700082B RID: 2091
			// (set) Token: 0x06001E63 RID: 7779 RVA: 0x0003F293 File Offset: 0x0003D493
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700082C RID: 2092
			// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0003F2AB File Offset: 0x0003D4AB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700082D RID: 2093
			// (set) Token: 0x06001E65 RID: 7781 RVA: 0x0003F2C3 File Offset: 0x0003D4C3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700082E RID: 2094
			// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0003F2DB File Offset: 0x0003D4DB
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
