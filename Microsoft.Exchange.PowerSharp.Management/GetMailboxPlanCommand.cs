using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C8A RID: 3210
	public class GetMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x06009E7B RID: 40571 RVA: 0x000E5C44 File Offset: 0x000E3E44
		private GetMailboxPlanCommand() : base("Get-MailboxPlan")
		{
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x000E5C51 File Offset: 0x000E3E51
		public GetMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009E7D RID: 40573 RVA: 0x000E5C60 File Offset: 0x000E3E60
		public virtual GetMailboxPlanCommand SetParameters(GetMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x000E5C6A File Offset: 0x000E3E6A
		public virtual GetMailboxPlanCommand SetParameters(GetMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C8B RID: 3211
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007120 RID: 28960
			// (set) Token: 0x06009E7F RID: 40575 RVA: 0x000E5C74 File Offset: 0x000E3E74
			public virtual SwitchParameter AllMailboxPlanReleases
			{
				set
				{
					base.PowerSharpParameters["AllMailboxPlanReleases"] = value;
				}
			}

			// Token: 0x17007121 RID: 28961
			// (set) Token: 0x06009E80 RID: 40576 RVA: 0x000E5C8C File Offset: 0x000E3E8C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007122 RID: 28962
			// (set) Token: 0x06009E81 RID: 40577 RVA: 0x000E5C9F File Offset: 0x000E3E9F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007123 RID: 28963
			// (set) Token: 0x06009E82 RID: 40578 RVA: 0x000E5CBD File Offset: 0x000E3EBD
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007124 RID: 28964
			// (set) Token: 0x06009E83 RID: 40579 RVA: 0x000E5CD0 File Offset: 0x000E3ED0
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007125 RID: 28965
			// (set) Token: 0x06009E84 RID: 40580 RVA: 0x000E5CE3 File Offset: 0x000E3EE3
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007126 RID: 28966
			// (set) Token: 0x06009E85 RID: 40581 RVA: 0x000E5D01 File Offset: 0x000E3F01
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007127 RID: 28967
			// (set) Token: 0x06009E86 RID: 40582 RVA: 0x000E5D19 File Offset: 0x000E3F19
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007128 RID: 28968
			// (set) Token: 0x06009E87 RID: 40583 RVA: 0x000E5D2C File Offset: 0x000E3F2C
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007129 RID: 28969
			// (set) Token: 0x06009E88 RID: 40584 RVA: 0x000E5D44 File Offset: 0x000E3F44
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700712A RID: 28970
			// (set) Token: 0x06009E89 RID: 40585 RVA: 0x000E5D5C File Offset: 0x000E3F5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700712B RID: 28971
			// (set) Token: 0x06009E8A RID: 40586 RVA: 0x000E5D6F File Offset: 0x000E3F6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700712C RID: 28972
			// (set) Token: 0x06009E8B RID: 40587 RVA: 0x000E5D87 File Offset: 0x000E3F87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700712D RID: 28973
			// (set) Token: 0x06009E8C RID: 40588 RVA: 0x000E5D9F File Offset: 0x000E3F9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700712E RID: 28974
			// (set) Token: 0x06009E8D RID: 40589 RVA: 0x000E5DB7 File Offset: 0x000E3FB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C8C RID: 3212
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700712F RID: 28975
			// (set) Token: 0x06009E8F RID: 40591 RVA: 0x000E5DD7 File Offset: 0x000E3FD7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007130 RID: 28976
			// (set) Token: 0x06009E90 RID: 40592 RVA: 0x000E5DF5 File Offset: 0x000E3FF5
			public virtual SwitchParameter AllMailboxPlanReleases
			{
				set
				{
					base.PowerSharpParameters["AllMailboxPlanReleases"] = value;
				}
			}

			// Token: 0x17007131 RID: 28977
			// (set) Token: 0x06009E91 RID: 40593 RVA: 0x000E5E0D File Offset: 0x000E400D
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007132 RID: 28978
			// (set) Token: 0x06009E92 RID: 40594 RVA: 0x000E5E20 File Offset: 0x000E4020
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007133 RID: 28979
			// (set) Token: 0x06009E93 RID: 40595 RVA: 0x000E5E3E File Offset: 0x000E403E
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007134 RID: 28980
			// (set) Token: 0x06009E94 RID: 40596 RVA: 0x000E5E51 File Offset: 0x000E4051
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007135 RID: 28981
			// (set) Token: 0x06009E95 RID: 40597 RVA: 0x000E5E64 File Offset: 0x000E4064
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007136 RID: 28982
			// (set) Token: 0x06009E96 RID: 40598 RVA: 0x000E5E82 File Offset: 0x000E4082
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007137 RID: 28983
			// (set) Token: 0x06009E97 RID: 40599 RVA: 0x000E5E9A File Offset: 0x000E409A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007138 RID: 28984
			// (set) Token: 0x06009E98 RID: 40600 RVA: 0x000E5EAD File Offset: 0x000E40AD
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007139 RID: 28985
			// (set) Token: 0x06009E99 RID: 40601 RVA: 0x000E5EC5 File Offset: 0x000E40C5
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700713A RID: 28986
			// (set) Token: 0x06009E9A RID: 40602 RVA: 0x000E5EDD File Offset: 0x000E40DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700713B RID: 28987
			// (set) Token: 0x06009E9B RID: 40603 RVA: 0x000E5EF0 File Offset: 0x000E40F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700713C RID: 28988
			// (set) Token: 0x06009E9C RID: 40604 RVA: 0x000E5F08 File Offset: 0x000E4108
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700713D RID: 28989
			// (set) Token: 0x06009E9D RID: 40605 RVA: 0x000E5F20 File Offset: 0x000E4120
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700713E RID: 28990
			// (set) Token: 0x06009E9E RID: 40606 RVA: 0x000E5F38 File Offset: 0x000E4138
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
