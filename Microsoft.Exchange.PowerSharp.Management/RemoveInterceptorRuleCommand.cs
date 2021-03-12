using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008BD RID: 2237
	public class RemoveInterceptorRuleCommand : SyntheticCommandWithPipelineInput<InterceptorRule, InterceptorRule>
	{
		// Token: 0x06007011 RID: 28689 RVA: 0x000A9229 File Offset: 0x000A7429
		private RemoveInterceptorRuleCommand() : base("Remove-InterceptorRule")
		{
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x000A9236 File Offset: 0x000A7436
		public RemoveInterceptorRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x000A9245 File Offset: 0x000A7445
		public virtual RemoveInterceptorRuleCommand SetParameters(RemoveInterceptorRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007014 RID: 28692 RVA: 0x000A924F File Offset: 0x000A744F
		public virtual RemoveInterceptorRuleCommand SetParameters(RemoveInterceptorRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008BE RID: 2238
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A50 RID: 19024
			// (set) Token: 0x06007015 RID: 28693 RVA: 0x000A9259 File Offset: 0x000A7459
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A51 RID: 19025
			// (set) Token: 0x06007016 RID: 28694 RVA: 0x000A926C File Offset: 0x000A746C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A52 RID: 19026
			// (set) Token: 0x06007017 RID: 28695 RVA: 0x000A9284 File Offset: 0x000A7484
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A53 RID: 19027
			// (set) Token: 0x06007018 RID: 28696 RVA: 0x000A929C File Offset: 0x000A749C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A54 RID: 19028
			// (set) Token: 0x06007019 RID: 28697 RVA: 0x000A92B4 File Offset: 0x000A74B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A55 RID: 19029
			// (set) Token: 0x0600701A RID: 28698 RVA: 0x000A92CC File Offset: 0x000A74CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A56 RID: 19030
			// (set) Token: 0x0600701B RID: 28699 RVA: 0x000A92E4 File Offset: 0x000A74E4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008BF RID: 2239
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A57 RID: 19031
			// (set) Token: 0x0600701D RID: 28701 RVA: 0x000A9304 File Offset: 0x000A7504
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InterceptorRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004A58 RID: 19032
			// (set) Token: 0x0600701E RID: 28702 RVA: 0x000A9322 File Offset: 0x000A7522
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A59 RID: 19033
			// (set) Token: 0x0600701F RID: 28703 RVA: 0x000A9335 File Offset: 0x000A7535
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A5A RID: 19034
			// (set) Token: 0x06007020 RID: 28704 RVA: 0x000A934D File Offset: 0x000A754D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A5B RID: 19035
			// (set) Token: 0x06007021 RID: 28705 RVA: 0x000A9365 File Offset: 0x000A7565
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A5C RID: 19036
			// (set) Token: 0x06007022 RID: 28706 RVA: 0x000A937D File Offset: 0x000A757D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A5D RID: 19037
			// (set) Token: 0x06007023 RID: 28707 RVA: 0x000A9395 File Offset: 0x000A7595
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A5E RID: 19038
			// (set) Token: 0x06007024 RID: 28708 RVA: 0x000A93AD File Offset: 0x000A75AD
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
