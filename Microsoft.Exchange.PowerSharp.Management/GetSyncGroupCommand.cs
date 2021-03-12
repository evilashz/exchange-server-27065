using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DF2 RID: 3570
	public class GetSyncGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600D4D8 RID: 54488 RVA: 0x0012E979 File Offset: 0x0012CB79
		private GetSyncGroupCommand() : base("Get-SyncGroup")
		{
		}

		// Token: 0x0600D4D9 RID: 54489 RVA: 0x0012E986 File Offset: 0x0012CB86
		public GetSyncGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D4DA RID: 54490 RVA: 0x0012E995 File Offset: 0x0012CB95
		public virtual GetSyncGroupCommand SetParameters(GetSyncGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D4DB RID: 54491 RVA: 0x0012E99F File Offset: 0x0012CB9F
		public virtual GetSyncGroupCommand SetParameters(GetSyncGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D4DC RID: 54492 RVA: 0x0012E9A9 File Offset: 0x0012CBA9
		public virtual GetSyncGroupCommand SetParameters(GetSyncGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DF3 RID: 3571
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A4AD RID: 42157
			// (set) Token: 0x0600D4DD RID: 54493 RVA: 0x0012E9B3 File Offset: 0x0012CBB3
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A4AE RID: 42158
			// (set) Token: 0x0600D4DE RID: 54494 RVA: 0x0012E9CB File Offset: 0x0012CBCB
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A4AF RID: 42159
			// (set) Token: 0x0600D4DF RID: 54495 RVA: 0x0012E9DE File Offset: 0x0012CBDE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4B0 RID: 42160
			// (set) Token: 0x0600D4E0 RID: 54496 RVA: 0x0012E9FC File Offset: 0x0012CBFC
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A4B1 RID: 42161
			// (set) Token: 0x0600D4E1 RID: 54497 RVA: 0x0012EA0F File Offset: 0x0012CC0F
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A4B2 RID: 42162
			// (set) Token: 0x0600D4E2 RID: 54498 RVA: 0x0012EA22 File Offset: 0x0012CC22
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4B3 RID: 42163
			// (set) Token: 0x0600D4E3 RID: 54499 RVA: 0x0012EA40 File Offset: 0x0012CC40
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A4B4 RID: 42164
			// (set) Token: 0x0600D4E4 RID: 54500 RVA: 0x0012EA58 File Offset: 0x0012CC58
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A4B5 RID: 42165
			// (set) Token: 0x0600D4E5 RID: 54501 RVA: 0x0012EA6B File Offset: 0x0012CC6B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A4B6 RID: 42166
			// (set) Token: 0x0600D4E6 RID: 54502 RVA: 0x0012EA83 File Offset: 0x0012CC83
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A4B7 RID: 42167
			// (set) Token: 0x0600D4E7 RID: 54503 RVA: 0x0012EA9B File Offset: 0x0012CC9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4B8 RID: 42168
			// (set) Token: 0x0600D4E8 RID: 54504 RVA: 0x0012EAAE File Offset: 0x0012CCAE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4B9 RID: 42169
			// (set) Token: 0x0600D4E9 RID: 54505 RVA: 0x0012EAC6 File Offset: 0x0012CCC6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4BA RID: 42170
			// (set) Token: 0x0600D4EA RID: 54506 RVA: 0x0012EADE File Offset: 0x0012CCDE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4BB RID: 42171
			// (set) Token: 0x0600D4EB RID: 54507 RVA: 0x0012EAF6 File Offset: 0x0012CCF6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DF4 RID: 3572
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700A4BC RID: 42172
			// (set) Token: 0x0600D4ED RID: 54509 RVA: 0x0012EB16 File Offset: 0x0012CD16
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700A4BD RID: 42173
			// (set) Token: 0x0600D4EE RID: 54510 RVA: 0x0012EB29 File Offset: 0x0012CD29
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A4BE RID: 42174
			// (set) Token: 0x0600D4EF RID: 54511 RVA: 0x0012EB41 File Offset: 0x0012CD41
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A4BF RID: 42175
			// (set) Token: 0x0600D4F0 RID: 54512 RVA: 0x0012EB54 File Offset: 0x0012CD54
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4C0 RID: 42176
			// (set) Token: 0x0600D4F1 RID: 54513 RVA: 0x0012EB72 File Offset: 0x0012CD72
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A4C1 RID: 42177
			// (set) Token: 0x0600D4F2 RID: 54514 RVA: 0x0012EB85 File Offset: 0x0012CD85
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A4C2 RID: 42178
			// (set) Token: 0x0600D4F3 RID: 54515 RVA: 0x0012EB98 File Offset: 0x0012CD98
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4C3 RID: 42179
			// (set) Token: 0x0600D4F4 RID: 54516 RVA: 0x0012EBB6 File Offset: 0x0012CDB6
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A4C4 RID: 42180
			// (set) Token: 0x0600D4F5 RID: 54517 RVA: 0x0012EBCE File Offset: 0x0012CDCE
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A4C5 RID: 42181
			// (set) Token: 0x0600D4F6 RID: 54518 RVA: 0x0012EBE1 File Offset: 0x0012CDE1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A4C6 RID: 42182
			// (set) Token: 0x0600D4F7 RID: 54519 RVA: 0x0012EBF9 File Offset: 0x0012CDF9
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A4C7 RID: 42183
			// (set) Token: 0x0600D4F8 RID: 54520 RVA: 0x0012EC11 File Offset: 0x0012CE11
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4C8 RID: 42184
			// (set) Token: 0x0600D4F9 RID: 54521 RVA: 0x0012EC24 File Offset: 0x0012CE24
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4C9 RID: 42185
			// (set) Token: 0x0600D4FA RID: 54522 RVA: 0x0012EC3C File Offset: 0x0012CE3C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4CA RID: 42186
			// (set) Token: 0x0600D4FB RID: 54523 RVA: 0x0012EC54 File Offset: 0x0012CE54
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4CB RID: 42187
			// (set) Token: 0x0600D4FC RID: 54524 RVA: 0x0012EC6C File Offset: 0x0012CE6C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DF5 RID: 3573
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A4CC RID: 42188
			// (set) Token: 0x0600D4FE RID: 54526 RVA: 0x0012EC8C File Offset: 0x0012CE8C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4CD RID: 42189
			// (set) Token: 0x0600D4FF RID: 54527 RVA: 0x0012ECAA File Offset: 0x0012CEAA
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700A4CE RID: 42190
			// (set) Token: 0x0600D500 RID: 54528 RVA: 0x0012ECC2 File Offset: 0x0012CEC2
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700A4CF RID: 42191
			// (set) Token: 0x0600D501 RID: 54529 RVA: 0x0012ECD5 File Offset: 0x0012CED5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4D0 RID: 42192
			// (set) Token: 0x0600D502 RID: 54530 RVA: 0x0012ECF3 File Offset: 0x0012CEF3
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700A4D1 RID: 42193
			// (set) Token: 0x0600D503 RID: 54531 RVA: 0x0012ED06 File Offset: 0x0012CF06
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700A4D2 RID: 42194
			// (set) Token: 0x0600D504 RID: 54532 RVA: 0x0012ED19 File Offset: 0x0012CF19
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4D3 RID: 42195
			// (set) Token: 0x0600D505 RID: 54533 RVA: 0x0012ED37 File Offset: 0x0012CF37
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A4D4 RID: 42196
			// (set) Token: 0x0600D506 RID: 54534 RVA: 0x0012ED4F File Offset: 0x0012CF4F
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700A4D5 RID: 42197
			// (set) Token: 0x0600D507 RID: 54535 RVA: 0x0012ED62 File Offset: 0x0012CF62
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A4D6 RID: 42198
			// (set) Token: 0x0600D508 RID: 54536 RVA: 0x0012ED7A File Offset: 0x0012CF7A
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700A4D7 RID: 42199
			// (set) Token: 0x0600D509 RID: 54537 RVA: 0x0012ED92 File Offset: 0x0012CF92
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4D8 RID: 42200
			// (set) Token: 0x0600D50A RID: 54538 RVA: 0x0012EDA5 File Offset: 0x0012CFA5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4D9 RID: 42201
			// (set) Token: 0x0600D50B RID: 54539 RVA: 0x0012EDBD File Offset: 0x0012CFBD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4DA RID: 42202
			// (set) Token: 0x0600D50C RID: 54540 RVA: 0x0012EDD5 File Offset: 0x0012CFD5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4DB RID: 42203
			// (set) Token: 0x0600D50D RID: 54541 RVA: 0x0012EDED File Offset: 0x0012CFED
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
