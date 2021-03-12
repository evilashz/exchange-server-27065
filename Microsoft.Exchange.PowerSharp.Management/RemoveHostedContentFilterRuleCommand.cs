using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000725 RID: 1829
	public class RemoveHostedContentFilterRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005E53 RID: 24147 RVA: 0x000920A2 File Offset: 0x000902A2
		private RemoveHostedContentFilterRuleCommand() : base("Remove-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x000920AF File Offset: 0x000902AF
		public RemoveHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x000920BE File Offset: 0x000902BE
		public virtual RemoveHostedContentFilterRuleCommand SetParameters(RemoveHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x000920C8 File Offset: 0x000902C8
		public virtual RemoveHostedContentFilterRuleCommand SetParameters(RemoveHostedContentFilterRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000726 RID: 1830
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003BC2 RID: 15298
			// (set) Token: 0x06005E57 RID: 24151 RVA: 0x000920D2 File Offset: 0x000902D2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BC3 RID: 15299
			// (set) Token: 0x06005E58 RID: 24152 RVA: 0x000920E5 File Offset: 0x000902E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BC4 RID: 15300
			// (set) Token: 0x06005E59 RID: 24153 RVA: 0x000920FD File Offset: 0x000902FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BC5 RID: 15301
			// (set) Token: 0x06005E5A RID: 24154 RVA: 0x00092115 File Offset: 0x00090315
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BC6 RID: 15302
			// (set) Token: 0x06005E5B RID: 24155 RVA: 0x0009212D File Offset: 0x0009032D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BC7 RID: 15303
			// (set) Token: 0x06005E5C RID: 24156 RVA: 0x00092145 File Offset: 0x00090345
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003BC8 RID: 15304
			// (set) Token: 0x06005E5D RID: 24157 RVA: 0x0009215D File Offset: 0x0009035D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000727 RID: 1831
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003BC9 RID: 15305
			// (set) Token: 0x06005E5F RID: 24159 RVA: 0x0009217D File Offset: 0x0009037D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003BCA RID: 15306
			// (set) Token: 0x06005E60 RID: 24160 RVA: 0x0009219B File Offset: 0x0009039B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BCB RID: 15307
			// (set) Token: 0x06005E61 RID: 24161 RVA: 0x000921AE File Offset: 0x000903AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BCC RID: 15308
			// (set) Token: 0x06005E62 RID: 24162 RVA: 0x000921C6 File Offset: 0x000903C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BCD RID: 15309
			// (set) Token: 0x06005E63 RID: 24163 RVA: 0x000921DE File Offset: 0x000903DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BCE RID: 15310
			// (set) Token: 0x06005E64 RID: 24164 RVA: 0x000921F6 File Offset: 0x000903F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BCF RID: 15311
			// (set) Token: 0x06005E65 RID: 24165 RVA: 0x0009220E File Offset: 0x0009040E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003BD0 RID: 15312
			// (set) Token: 0x06005E66 RID: 24166 RVA: 0x00092226 File Offset: 0x00090426
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
