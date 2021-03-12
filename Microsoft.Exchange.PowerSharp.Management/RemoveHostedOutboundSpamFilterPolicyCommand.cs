using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200072E RID: 1838
	public class RemoveHostedOutboundSpamFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedOutboundSpamFilterPolicy, HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x06005EA4 RID: 24228 RVA: 0x0009271A File Offset: 0x0009091A
		private RemoveHostedOutboundSpamFilterPolicyCommand() : base("Remove-HostedOutboundSpamFilterPolicy")
		{
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x00092727 File Offset: 0x00090927
		public RemoveHostedOutboundSpamFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x00092736 File Offset: 0x00090936
		public virtual RemoveHostedOutboundSpamFilterPolicyCommand SetParameters(RemoveHostedOutboundSpamFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x00092740 File Offset: 0x00090940
		public virtual RemoveHostedOutboundSpamFilterPolicyCommand SetParameters(RemoveHostedOutboundSpamFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200072F RID: 1839
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C01 RID: 15361
			// (set) Token: 0x06005EA8 RID: 24232 RVA: 0x0009274A File Offset: 0x0009094A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003C02 RID: 15362
			// (set) Token: 0x06005EA9 RID: 24233 RVA: 0x0009275D File Offset: 0x0009095D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C03 RID: 15363
			// (set) Token: 0x06005EAA RID: 24234 RVA: 0x00092775 File Offset: 0x00090975
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C04 RID: 15364
			// (set) Token: 0x06005EAB RID: 24235 RVA: 0x0009278D File Offset: 0x0009098D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C05 RID: 15365
			// (set) Token: 0x06005EAC RID: 24236 RVA: 0x000927A5 File Offset: 0x000909A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000730 RID: 1840
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003C06 RID: 15366
			// (set) Token: 0x06005EAE RID: 24238 RVA: 0x000927C5 File Offset: 0x000909C5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedOutboundSpamFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003C07 RID: 15367
			// (set) Token: 0x06005EAF RID: 24239 RVA: 0x000927E3 File Offset: 0x000909E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003C08 RID: 15368
			// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x000927F6 File Offset: 0x000909F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C09 RID: 15369
			// (set) Token: 0x06005EB1 RID: 24241 RVA: 0x0009280E File Offset: 0x00090A0E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C0A RID: 15370
			// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x00092826 File Offset: 0x00090A26
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C0B RID: 15371
			// (set) Token: 0x06005EB3 RID: 24243 RVA: 0x0009283E File Offset: 0x00090A3E
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
