using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001DC RID: 476
	public class NewSiteMailboxProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<TeamMailboxProvisioningPolicy, TeamMailboxProvisioningPolicy>
	{
		// Token: 0x0600277A RID: 10106 RVA: 0x0004AFB5 File Offset: 0x000491B5
		private NewSiteMailboxProvisioningPolicyCommand() : base("New-SiteMailboxProvisioningPolicy")
		{
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0004AFC2 File Offset: 0x000491C2
		public NewSiteMailboxProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0004AFD1 File Offset: 0x000491D1
		public virtual NewSiteMailboxProvisioningPolicyCommand SetParameters(NewSiteMailboxProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001DD RID: 477
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000F7B RID: 3963
			// (set) Token: 0x0600277D RID: 10109 RVA: 0x0004AFDB File Offset: 0x000491DB
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000F7C RID: 3964
			// (set) Token: 0x0600277E RID: 10110 RVA: 0x0004AFF3 File Offset: 0x000491F3
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000F7D RID: 3965
			// (set) Token: 0x0600277F RID: 10111 RVA: 0x0004B00B File Offset: 0x0004920B
			public virtual ByteQuantifiedSize MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17000F7E RID: 3966
			// (set) Token: 0x06002780 RID: 10112 RVA: 0x0004B023 File Offset: 0x00049223
			public virtual ByteQuantifiedSize IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000F7F RID: 3967
			// (set) Token: 0x06002781 RID: 10113 RVA: 0x0004B03B File Offset: 0x0004923B
			public virtual ByteQuantifiedSize ProhibitSendReceiveQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitSendReceiveQuota"] = value;
				}
			}

			// Token: 0x17000F80 RID: 3968
			// (set) Token: 0x06002782 RID: 10114 RVA: 0x0004B053 File Offset: 0x00049253
			public virtual bool DefaultAliasPrefixEnabled
			{
				set
				{
					base.PowerSharpParameters["DefaultAliasPrefixEnabled"] = value;
				}
			}

			// Token: 0x17000F81 RID: 3969
			// (set) Token: 0x06002783 RID: 10115 RVA: 0x0004B06B File Offset: 0x0004926B
			public virtual string AliasPrefix
			{
				set
				{
					base.PowerSharpParameters["AliasPrefix"] = value;
				}
			}

			// Token: 0x17000F82 RID: 3970
			// (set) Token: 0x06002784 RID: 10116 RVA: 0x0004B07E File Offset: 0x0004927E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000F83 RID: 3971
			// (set) Token: 0x06002785 RID: 10117 RVA: 0x0004B09C File Offset: 0x0004929C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000F84 RID: 3972
			// (set) Token: 0x06002786 RID: 10118 RVA: 0x0004B0AF File Offset: 0x000492AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000F85 RID: 3973
			// (set) Token: 0x06002787 RID: 10119 RVA: 0x0004B0C2 File Offset: 0x000492C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (set) Token: 0x06002788 RID: 10120 RVA: 0x0004B0DA File Offset: 0x000492DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F87 RID: 3975
			// (set) Token: 0x06002789 RID: 10121 RVA: 0x0004B0F2 File Offset: 0x000492F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F88 RID: 3976
			// (set) Token: 0x0600278A RID: 10122 RVA: 0x0004B10A File Offset: 0x0004930A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000F89 RID: 3977
			// (set) Token: 0x0600278B RID: 10123 RVA: 0x0004B122 File Offset: 0x00049322
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
