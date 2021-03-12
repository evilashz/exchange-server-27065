using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E1B RID: 3611
	public class GetUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInput<UMCallAnsweringRule, UMCallAnsweringRule>
	{
		// Token: 0x0600D6B7 RID: 54967 RVA: 0x0013113E File Offset: 0x0012F33E
		private GetUMCallAnsweringRuleCommand() : base("Get-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D6B8 RID: 54968 RVA: 0x0013114B File Offset: 0x0012F34B
		public GetUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D6B9 RID: 54969 RVA: 0x0013115A File Offset: 0x0012F35A
		public virtual GetUMCallAnsweringRuleCommand SetParameters(GetUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D6BA RID: 54970 RVA: 0x00131164 File Offset: 0x0012F364
		public virtual GetUMCallAnsweringRuleCommand SetParameters(GetUMCallAnsweringRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E1C RID: 3612
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A63A RID: 42554
			// (set) Token: 0x0600D6BB RID: 54971 RVA: 0x0013116E File Offset: 0x0012F36E
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A63B RID: 42555
			// (set) Token: 0x0600D6BC RID: 54972 RVA: 0x0013118C File Offset: 0x0012F38C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A63C RID: 42556
			// (set) Token: 0x0600D6BD RID: 54973 RVA: 0x0013119F File Offset: 0x0012F39F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A63D RID: 42557
			// (set) Token: 0x0600D6BE RID: 54974 RVA: 0x001311B7 File Offset: 0x0012F3B7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A63E RID: 42558
			// (set) Token: 0x0600D6BF RID: 54975 RVA: 0x001311CF File Offset: 0x0012F3CF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A63F RID: 42559
			// (set) Token: 0x0600D6C0 RID: 54976 RVA: 0x001311E7 File Offset: 0x0012F3E7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E1D RID: 3613
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A640 RID: 42560
			// (set) Token: 0x0600D6C2 RID: 54978 RVA: 0x00131207 File Offset: 0x0012F407
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700A641 RID: 42561
			// (set) Token: 0x0600D6C3 RID: 54979 RVA: 0x00131225 File Offset: 0x0012F425
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A642 RID: 42562
			// (set) Token: 0x0600D6C4 RID: 54980 RVA: 0x00131243 File Offset: 0x0012F443
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A643 RID: 42563
			// (set) Token: 0x0600D6C5 RID: 54981 RVA: 0x00131256 File Offset: 0x0012F456
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A644 RID: 42564
			// (set) Token: 0x0600D6C6 RID: 54982 RVA: 0x0013126E File Offset: 0x0012F46E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A645 RID: 42565
			// (set) Token: 0x0600D6C7 RID: 54983 RVA: 0x00131286 File Offset: 0x0012F486
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A646 RID: 42566
			// (set) Token: 0x0600D6C8 RID: 54984 RVA: 0x0013129E File Offset: 0x0012F49E
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
