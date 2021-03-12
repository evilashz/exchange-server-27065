using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000720 RID: 1824
	public class GetHostedContentFilterRuleCommand : SyntheticCommand<object>
	{
		// Token: 0x06005E28 RID: 24104 RVA: 0x00091D2B File Offset: 0x0008FF2B
		private GetHostedContentFilterRuleCommand() : base("Get-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x00091D38 File Offset: 0x0008FF38
		public GetHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x00091D47 File Offset: 0x0008FF47
		public virtual GetHostedContentFilterRuleCommand SetParameters(GetHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x00091D51 File Offset: 0x0008FF51
		public virtual GetHostedContentFilterRuleCommand SetParameters(GetHostedContentFilterRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000721 RID: 1825
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003BA1 RID: 15265
			// (set) Token: 0x06005E2C RID: 24108 RVA: 0x00091D5B File Offset: 0x0008FF5B
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17003BA2 RID: 15266
			// (set) Token: 0x06005E2D RID: 24109 RVA: 0x00091D73 File Offset: 0x0008FF73
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003BA3 RID: 15267
			// (set) Token: 0x06005E2E RID: 24110 RVA: 0x00091D91 File Offset: 0x0008FF91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BA4 RID: 15268
			// (set) Token: 0x06005E2F RID: 24111 RVA: 0x00091DA4 File Offset: 0x0008FFA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BA5 RID: 15269
			// (set) Token: 0x06005E30 RID: 24112 RVA: 0x00091DBC File Offset: 0x0008FFBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BA6 RID: 15270
			// (set) Token: 0x06005E31 RID: 24113 RVA: 0x00091DD4 File Offset: 0x0008FFD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BA7 RID: 15271
			// (set) Token: 0x06005E32 RID: 24114 RVA: 0x00091DEC File Offset: 0x0008FFEC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000722 RID: 1826
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003BA8 RID: 15272
			// (set) Token: 0x06005E34 RID: 24116 RVA: 0x00091E0C File Offset: 0x0009000C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003BA9 RID: 15273
			// (set) Token: 0x06005E35 RID: 24117 RVA: 0x00091E2A File Offset: 0x0009002A
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17003BAA RID: 15274
			// (set) Token: 0x06005E36 RID: 24118 RVA: 0x00091E42 File Offset: 0x00090042
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003BAB RID: 15275
			// (set) Token: 0x06005E37 RID: 24119 RVA: 0x00091E60 File Offset: 0x00090060
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BAC RID: 15276
			// (set) Token: 0x06005E38 RID: 24120 RVA: 0x00091E73 File Offset: 0x00090073
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BAD RID: 15277
			// (set) Token: 0x06005E39 RID: 24121 RVA: 0x00091E8B File Offset: 0x0009008B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BAE RID: 15278
			// (set) Token: 0x06005E3A RID: 24122 RVA: 0x00091EA3 File Offset: 0x000900A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BAF RID: 15279
			// (set) Token: 0x06005E3B RID: 24123 RVA: 0x00091EBB File Offset: 0x000900BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
