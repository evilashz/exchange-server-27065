using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200071A RID: 1818
	public class DisableHostedContentFilterRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005E00 RID: 24064 RVA: 0x00091A13 File Offset: 0x0008FC13
		private DisableHostedContentFilterRuleCommand() : base("Disable-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x00091A20 File Offset: 0x0008FC20
		public DisableHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x00091A2F File Offset: 0x0008FC2F
		public virtual DisableHostedContentFilterRuleCommand SetParameters(DisableHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x00091A39 File Offset: 0x0008FC39
		public virtual DisableHostedContentFilterRuleCommand SetParameters(DisableHostedContentFilterRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200071B RID: 1819
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003B85 RID: 15237
			// (set) Token: 0x06005E04 RID: 24068 RVA: 0x00091A43 File Offset: 0x0008FC43
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B86 RID: 15238
			// (set) Token: 0x06005E05 RID: 24069 RVA: 0x00091A56 File Offset: 0x0008FC56
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B87 RID: 15239
			// (set) Token: 0x06005E06 RID: 24070 RVA: 0x00091A6E File Offset: 0x0008FC6E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B88 RID: 15240
			// (set) Token: 0x06005E07 RID: 24071 RVA: 0x00091A86 File Offset: 0x0008FC86
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B89 RID: 15241
			// (set) Token: 0x06005E08 RID: 24072 RVA: 0x00091A9E File Offset: 0x0008FC9E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B8A RID: 15242
			// (set) Token: 0x06005E09 RID: 24073 RVA: 0x00091AB6 File Offset: 0x0008FCB6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003B8B RID: 15243
			// (set) Token: 0x06005E0A RID: 24074 RVA: 0x00091ACE File Offset: 0x0008FCCE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200071C RID: 1820
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003B8C RID: 15244
			// (set) Token: 0x06005E0C RID: 24076 RVA: 0x00091AEE File Offset: 0x0008FCEE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003B8D RID: 15245
			// (set) Token: 0x06005E0D RID: 24077 RVA: 0x00091B0C File Offset: 0x0008FD0C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B8E RID: 15246
			// (set) Token: 0x06005E0E RID: 24078 RVA: 0x00091B1F File Offset: 0x0008FD1F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B8F RID: 15247
			// (set) Token: 0x06005E0F RID: 24079 RVA: 0x00091B37 File Offset: 0x0008FD37
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B90 RID: 15248
			// (set) Token: 0x06005E10 RID: 24080 RVA: 0x00091B4F File Offset: 0x0008FD4F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B91 RID: 15249
			// (set) Token: 0x06005E11 RID: 24081 RVA: 0x00091B67 File Offset: 0x0008FD67
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B92 RID: 15250
			// (set) Token: 0x06005E12 RID: 24082 RVA: 0x00091B7F File Offset: 0x0008FD7F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003B93 RID: 15251
			// (set) Token: 0x06005E13 RID: 24083 RVA: 0x00091B97 File Offset: 0x0008FD97
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
