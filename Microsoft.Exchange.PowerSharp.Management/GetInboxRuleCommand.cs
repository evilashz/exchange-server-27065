using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C4B RID: 3147
	public class GetInboxRuleCommand : SyntheticCommandWithPipelineInput<InboxRule, InboxRule>
	{
		// Token: 0x060099CB RID: 39371 RVA: 0x000DF5F5 File Offset: 0x000DD7F5
		private GetInboxRuleCommand() : base("Get-InboxRule")
		{
		}

		// Token: 0x060099CC RID: 39372 RVA: 0x000DF602 File Offset: 0x000DD802
		public GetInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060099CD RID: 39373 RVA: 0x000DF611 File Offset: 0x000DD811
		public virtual GetInboxRuleCommand SetParameters(GetInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060099CE RID: 39374 RVA: 0x000DF61B File Offset: 0x000DD81B
		public virtual GetInboxRuleCommand SetParameters(GetInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C4C RID: 3148
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006CEE RID: 27886
			// (set) Token: 0x060099CF RID: 39375 RVA: 0x000DF625 File Offset: 0x000DD825
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CEF RID: 27887
			// (set) Token: 0x060099D0 RID: 39376 RVA: 0x000DF643 File Offset: 0x000DD843
			public virtual ExTimeZoneValue DescriptionTimeZone
			{
				set
				{
					base.PowerSharpParameters["DescriptionTimeZone"] = value;
				}
			}

			// Token: 0x17006CF0 RID: 27888
			// (set) Token: 0x060099D1 RID: 39377 RVA: 0x000DF656 File Offset: 0x000DD856
			public virtual string DescriptionTimeFormat
			{
				set
				{
					base.PowerSharpParameters["DescriptionTimeFormat"] = value;
				}
			}

			// Token: 0x17006CF1 RID: 27889
			// (set) Token: 0x060099D2 RID: 39378 RVA: 0x000DF669 File Offset: 0x000DD869
			public virtual SwitchParameter IncludeHidden
			{
				set
				{
					base.PowerSharpParameters["IncludeHidden"] = value;
				}
			}

			// Token: 0x17006CF2 RID: 27890
			// (set) Token: 0x060099D3 RID: 39379 RVA: 0x000DF681 File Offset: 0x000DD881
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CF3 RID: 27891
			// (set) Token: 0x060099D4 RID: 39380 RVA: 0x000DF694 File Offset: 0x000DD894
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CF4 RID: 27892
			// (set) Token: 0x060099D5 RID: 39381 RVA: 0x000DF6AC File Offset: 0x000DD8AC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CF5 RID: 27893
			// (set) Token: 0x060099D6 RID: 39382 RVA: 0x000DF6C4 File Offset: 0x000DD8C4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CF6 RID: 27894
			// (set) Token: 0x060099D7 RID: 39383 RVA: 0x000DF6DC File Offset: 0x000DD8DC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C4D RID: 3149
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006CF7 RID: 27895
			// (set) Token: 0x060099D9 RID: 39385 RVA: 0x000DF6FC File Offset: 0x000DD8FC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InboxRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17006CF8 RID: 27896
			// (set) Token: 0x060099DA RID: 39386 RVA: 0x000DF71A File Offset: 0x000DD91A
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CF9 RID: 27897
			// (set) Token: 0x060099DB RID: 39387 RVA: 0x000DF738 File Offset: 0x000DD938
			public virtual ExTimeZoneValue DescriptionTimeZone
			{
				set
				{
					base.PowerSharpParameters["DescriptionTimeZone"] = value;
				}
			}

			// Token: 0x17006CFA RID: 27898
			// (set) Token: 0x060099DC RID: 39388 RVA: 0x000DF74B File Offset: 0x000DD94B
			public virtual string DescriptionTimeFormat
			{
				set
				{
					base.PowerSharpParameters["DescriptionTimeFormat"] = value;
				}
			}

			// Token: 0x17006CFB RID: 27899
			// (set) Token: 0x060099DD RID: 39389 RVA: 0x000DF75E File Offset: 0x000DD95E
			public virtual SwitchParameter IncludeHidden
			{
				set
				{
					base.PowerSharpParameters["IncludeHidden"] = value;
				}
			}

			// Token: 0x17006CFC RID: 27900
			// (set) Token: 0x060099DE RID: 39390 RVA: 0x000DF776 File Offset: 0x000DD976
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CFD RID: 27901
			// (set) Token: 0x060099DF RID: 39391 RVA: 0x000DF789 File Offset: 0x000DD989
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CFE RID: 27902
			// (set) Token: 0x060099E0 RID: 39392 RVA: 0x000DF7A1 File Offset: 0x000DD9A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CFF RID: 27903
			// (set) Token: 0x060099E1 RID: 39393 RVA: 0x000DF7B9 File Offset: 0x000DD9B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D00 RID: 27904
			// (set) Token: 0x060099E2 RID: 39394 RVA: 0x000DF7D1 File Offset: 0x000DD9D1
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
