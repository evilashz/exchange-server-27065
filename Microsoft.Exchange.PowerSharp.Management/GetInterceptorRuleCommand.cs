using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200088D RID: 2189
	public class GetInterceptorRuleCommand : SyntheticCommandWithPipelineInput<InterceptorRule, InterceptorRule>
	{
		// Token: 0x06006CF0 RID: 27888 RVA: 0x000A4F38 File Offset: 0x000A3138
		private GetInterceptorRuleCommand() : base("Get-InterceptorRule")
		{
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x000A4F45 File Offset: 0x000A3145
		public GetInterceptorRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x000A4F54 File Offset: 0x000A3154
		public virtual GetInterceptorRuleCommand SetParameters(GetInterceptorRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x000A4F5E File Offset: 0x000A315E
		public virtual GetInterceptorRuleCommand SetParameters(GetInterceptorRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200088E RID: 2190
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700478F RID: 18319
			// (set) Token: 0x06006CF4 RID: 27892 RVA: 0x000A4F68 File Offset: 0x000A3168
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004790 RID: 18320
			// (set) Token: 0x06006CF5 RID: 27893 RVA: 0x000A4F7B File Offset: 0x000A317B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004791 RID: 18321
			// (set) Token: 0x06006CF6 RID: 27894 RVA: 0x000A4F93 File Offset: 0x000A3193
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004792 RID: 18322
			// (set) Token: 0x06006CF7 RID: 27895 RVA: 0x000A4FAB File Offset: 0x000A31AB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004793 RID: 18323
			// (set) Token: 0x06006CF8 RID: 27896 RVA: 0x000A4FC3 File Offset: 0x000A31C3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004794 RID: 18324
			// (set) Token: 0x06006CF9 RID: 27897 RVA: 0x000A4FDB File Offset: 0x000A31DB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200088F RID: 2191
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004795 RID: 18325
			// (set) Token: 0x06006CFB RID: 27899 RVA: 0x000A4FFB File Offset: 0x000A31FB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InterceptorRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004796 RID: 18326
			// (set) Token: 0x06006CFC RID: 27900 RVA: 0x000A5019 File Offset: 0x000A3219
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004797 RID: 18327
			// (set) Token: 0x06006CFD RID: 27901 RVA: 0x000A502C File Offset: 0x000A322C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004798 RID: 18328
			// (set) Token: 0x06006CFE RID: 27902 RVA: 0x000A5044 File Offset: 0x000A3244
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004799 RID: 18329
			// (set) Token: 0x06006CFF RID: 27903 RVA: 0x000A505C File Offset: 0x000A325C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700479A RID: 18330
			// (set) Token: 0x06006D00 RID: 27904 RVA: 0x000A5074 File Offset: 0x000A3274
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700479B RID: 18331
			// (set) Token: 0x06006D01 RID: 27905 RVA: 0x000A508C File Offset: 0x000A328C
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
