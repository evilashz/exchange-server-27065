using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200071D RID: 1821
	public class EnableHostedContentFilterRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005E15 RID: 24085 RVA: 0x00091BB7 File Offset: 0x0008FDB7
		private EnableHostedContentFilterRuleCommand() : base("Enable-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x00091BC4 File Offset: 0x0008FDC4
		public EnableHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x00091BD3 File Offset: 0x0008FDD3
		public virtual EnableHostedContentFilterRuleCommand SetParameters(EnableHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x00091BDD File Offset: 0x0008FDDD
		public virtual EnableHostedContentFilterRuleCommand SetParameters(EnableHostedContentFilterRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200071E RID: 1822
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003B94 RID: 15252
			// (set) Token: 0x06005E19 RID: 24089 RVA: 0x00091BE7 File Offset: 0x0008FDE7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B95 RID: 15253
			// (set) Token: 0x06005E1A RID: 24090 RVA: 0x00091BFA File Offset: 0x0008FDFA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B96 RID: 15254
			// (set) Token: 0x06005E1B RID: 24091 RVA: 0x00091C12 File Offset: 0x0008FE12
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B97 RID: 15255
			// (set) Token: 0x06005E1C RID: 24092 RVA: 0x00091C2A File Offset: 0x0008FE2A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B98 RID: 15256
			// (set) Token: 0x06005E1D RID: 24093 RVA: 0x00091C42 File Offset: 0x0008FE42
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B99 RID: 15257
			// (set) Token: 0x06005E1E RID: 24094 RVA: 0x00091C5A File Offset: 0x0008FE5A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200071F RID: 1823
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003B9A RID: 15258
			// (set) Token: 0x06005E20 RID: 24096 RVA: 0x00091C7A File Offset: 0x0008FE7A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003B9B RID: 15259
			// (set) Token: 0x06005E21 RID: 24097 RVA: 0x00091C98 File Offset: 0x0008FE98
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B9C RID: 15260
			// (set) Token: 0x06005E22 RID: 24098 RVA: 0x00091CAB File Offset: 0x0008FEAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B9D RID: 15261
			// (set) Token: 0x06005E23 RID: 24099 RVA: 0x00091CC3 File Offset: 0x0008FEC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B9E RID: 15262
			// (set) Token: 0x06005E24 RID: 24100 RVA: 0x00091CDB File Offset: 0x0008FEDB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B9F RID: 15263
			// (set) Token: 0x06005E25 RID: 24101 RVA: 0x00091CF3 File Offset: 0x0008FEF3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BA0 RID: 15264
			// (set) Token: 0x06005E26 RID: 24102 RVA: 0x00091D0B File Offset: 0x0008FF0B
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
