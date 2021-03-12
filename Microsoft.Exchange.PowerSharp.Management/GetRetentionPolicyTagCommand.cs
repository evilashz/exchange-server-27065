using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000EB RID: 235
	public class GetRetentionPolicyTagCommand : SyntheticCommandWithPipelineInput<RetentionPolicyTag, RetentionPolicyTag>
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x0003E659 File Offset: 0x0003C859
		private GetRetentionPolicyTagCommand() : base("Get-RetentionPolicyTag")
		{
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0003E666 File Offset: 0x0003C866
		public GetRetentionPolicyTagCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0003E675 File Offset: 0x0003C875
		public virtual GetRetentionPolicyTagCommand SetParameters(GetRetentionPolicyTagCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0003E67F File Offset: 0x0003C87F
		public virtual GetRetentionPolicyTagCommand SetParameters(GetRetentionPolicyTagCommand.ParameterSetMailboxTaskParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0003E689 File Offset: 0x0003C889
		public virtual GetRetentionPolicyTagCommand SetParameters(GetRetentionPolicyTagCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000EC RID: 236
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170007B2 RID: 1970
			// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0003E693 File Offset: 0x0003C893
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007B3 RID: 1971
			// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0003E6AB File Offset: 0x0003C8AB
			public virtual SwitchParameter IncludeSystemTags
			{
				set
				{
					base.PowerSharpParameters["IncludeSystemTags"] = value;
				}
			}

			// Token: 0x170007B4 RID: 1972
			// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0003E6C3 File Offset: 0x0003C8C3
			public virtual ElcFolderType Types
			{
				set
				{
					base.PowerSharpParameters["Types"] = value;
				}
			}

			// Token: 0x170007B5 RID: 1973
			// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0003E6DB File Offset: 0x0003C8DB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007B6 RID: 1974
			// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0003E6F9 File Offset: 0x0003C8F9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007B7 RID: 1975
			// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0003E70C File Offset: 0x0003C90C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007B8 RID: 1976
			// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0003E724 File Offset: 0x0003C924
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007B9 RID: 1977
			// (set) Token: 0x06001DDB RID: 7643 RVA: 0x0003E73C File Offset: 0x0003C93C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007BA RID: 1978
			// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0003E754 File Offset: 0x0003C954
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000ED RID: 237
		public class ParameterSetMailboxTaskParameters : ParametersBase
		{
			// Token: 0x170007BB RID: 1979
			// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0003E774 File Offset: 0x0003C974
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170007BC RID: 1980
			// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0003E792 File Offset: 0x0003C992
			public virtual SwitchParameter OptionalInMailbox
			{
				set
				{
					base.PowerSharpParameters["OptionalInMailbox"] = value;
				}
			}

			// Token: 0x170007BD RID: 1981
			// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0003E7AA File Offset: 0x0003C9AA
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007BE RID: 1982
			// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x0003E7C2 File Offset: 0x0003C9C2
			public virtual SwitchParameter IncludeSystemTags
			{
				set
				{
					base.PowerSharpParameters["IncludeSystemTags"] = value;
				}
			}

			// Token: 0x170007BF RID: 1983
			// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0003E7DA File Offset: 0x0003C9DA
			public virtual ElcFolderType Types
			{
				set
				{
					base.PowerSharpParameters["Types"] = value;
				}
			}

			// Token: 0x170007C0 RID: 1984
			// (set) Token: 0x06001DE3 RID: 7651 RVA: 0x0003E7F2 File Offset: 0x0003C9F2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007C1 RID: 1985
			// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0003E810 File Offset: 0x0003CA10
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007C2 RID: 1986
			// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x0003E823 File Offset: 0x0003CA23
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007C3 RID: 1987
			// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0003E83B File Offset: 0x0003CA3B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007C4 RID: 1988
			// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x0003E853 File Offset: 0x0003CA53
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007C5 RID: 1989
			// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0003E86B File Offset: 0x0003CA6B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000EE RID: 238
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170007C6 RID: 1990
			// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0003E88B File Offset: 0x0003CA8B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RetentionPolicyTagIdParameter(value) : null);
				}
			}

			// Token: 0x170007C7 RID: 1991
			// (set) Token: 0x06001DEB RID: 7659 RVA: 0x0003E8A9 File Offset: 0x0003CAA9
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007C8 RID: 1992
			// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0003E8C1 File Offset: 0x0003CAC1
			public virtual SwitchParameter IncludeSystemTags
			{
				set
				{
					base.PowerSharpParameters["IncludeSystemTags"] = value;
				}
			}

			// Token: 0x170007C9 RID: 1993
			// (set) Token: 0x06001DED RID: 7661 RVA: 0x0003E8D9 File Offset: 0x0003CAD9
			public virtual ElcFolderType Types
			{
				set
				{
					base.PowerSharpParameters["Types"] = value;
				}
			}

			// Token: 0x170007CA RID: 1994
			// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0003E8F1 File Offset: 0x0003CAF1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007CB RID: 1995
			// (set) Token: 0x06001DEF RID: 7663 RVA: 0x0003E90F File Offset: 0x0003CB0F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007CC RID: 1996
			// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0003E922 File Offset: 0x0003CB22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007CD RID: 1997
			// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x0003E93A File Offset: 0x0003CB3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007CE RID: 1998
			// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0003E952 File Offset: 0x0003CB52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007CF RID: 1999
			// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0003E96A File Offset: 0x0003CB6A
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
