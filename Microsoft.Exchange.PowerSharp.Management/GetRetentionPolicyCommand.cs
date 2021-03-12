using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001C8 RID: 456
	public class GetRetentionPolicyCommand : SyntheticCommandWithPipelineInput<RetentionPolicy, RetentionPolicy>
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x0004941F File Offset: 0x0004761F
		private GetRetentionPolicyCommand() : base("Get-RetentionPolicy")
		{
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0004942C File Offset: 0x0004762C
		public GetRetentionPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0004943B File Offset: 0x0004763B
		public virtual GetRetentionPolicyCommand SetParameters(GetRetentionPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00049445 File Offset: 0x00047645
		public virtual GetRetentionPolicyCommand SetParameters(GetRetentionPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001C9 RID: 457
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E5F RID: 3679
			// (set) Token: 0x0600263A RID: 9786 RVA: 0x0004944F File Offset: 0x0004764F
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E60 RID: 3680
			// (set) Token: 0x0600263B RID: 9787 RVA: 0x00049467 File Offset: 0x00047667
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000E61 RID: 3681
			// (set) Token: 0x0600263C RID: 9788 RVA: 0x00049485 File Offset: 0x00047685
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E62 RID: 3682
			// (set) Token: 0x0600263D RID: 9789 RVA: 0x00049498 File Offset: 0x00047698
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E63 RID: 3683
			// (set) Token: 0x0600263E RID: 9790 RVA: 0x000494B0 File Offset: 0x000476B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E64 RID: 3684
			// (set) Token: 0x0600263F RID: 9791 RVA: 0x000494C8 File Offset: 0x000476C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E65 RID: 3685
			// (set) Token: 0x06002640 RID: 9792 RVA: 0x000494E0 File Offset: 0x000476E0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001CA RID: 458
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000E66 RID: 3686
			// (set) Token: 0x06002642 RID: 9794 RVA: 0x00049500 File Offset: 0x00047700
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000E67 RID: 3687
			// (set) Token: 0x06002643 RID: 9795 RVA: 0x0004951E File Offset: 0x0004771E
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E68 RID: 3688
			// (set) Token: 0x06002644 RID: 9796 RVA: 0x00049536 File Offset: 0x00047736
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000E69 RID: 3689
			// (set) Token: 0x06002645 RID: 9797 RVA: 0x00049554 File Offset: 0x00047754
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E6A RID: 3690
			// (set) Token: 0x06002646 RID: 9798 RVA: 0x00049567 File Offset: 0x00047767
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E6B RID: 3691
			// (set) Token: 0x06002647 RID: 9799 RVA: 0x0004957F File Offset: 0x0004777F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E6C RID: 3692
			// (set) Token: 0x06002648 RID: 9800 RVA: 0x00049597 File Offset: 0x00047797
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E6D RID: 3693
			// (set) Token: 0x06002649 RID: 9801 RVA: 0x000495AF File Offset: 0x000477AF
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
