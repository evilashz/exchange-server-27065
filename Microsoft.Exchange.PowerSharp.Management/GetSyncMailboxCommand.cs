using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DA0 RID: 3488
	public class GetSyncMailboxCommand : SyntheticCommandWithPipelineInput<SyncMailbox, SyncMailbox>
	{
		// Token: 0x0600C862 RID: 51298 RVA: 0x0011E3F3 File Offset: 0x0011C5F3
		private GetSyncMailboxCommand() : base("Get-SyncMailbox")
		{
		}

		// Token: 0x0600C863 RID: 51299 RVA: 0x0011E400 File Offset: 0x0011C600
		public GetSyncMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600C864 RID: 51300 RVA: 0x0011E40F File Offset: 0x0011C60F
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.CookieSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C865 RID: 51301 RVA: 0x0011E419 File Offset: 0x0011C619
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C866 RID: 51302 RVA: 0x0011E423 File Offset: 0x0011C623
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.MailboxPlanSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C867 RID: 51303 RVA: 0x0011E42D File Offset: 0x0011C62D
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.ServerSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C868 RID: 51304 RVA: 0x0011E437 File Offset: 0x0011C637
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C869 RID: 51305 RVA: 0x0011E441 File Offset: 0x0011C641
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.DatabaseSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C86A RID: 51306 RVA: 0x0011E44B File Offset: 0x0011C64B
		public virtual GetSyncMailboxCommand SetParameters(GetSyncMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DA1 RID: 3489
		public class CookieSetParameters : ParametersBase
		{
			// Token: 0x170098DB RID: 39131
			// (set) Token: 0x0600C86B RID: 51307 RVA: 0x0011E455 File Offset: 0x0011C655
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170098DC RID: 39132
			// (set) Token: 0x0600C86C RID: 51308 RVA: 0x0011E46D File Offset: 0x0011C66D
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x170098DD RID: 39133
			// (set) Token: 0x0600C86D RID: 51309 RVA: 0x0011E485 File Offset: 0x0011C685
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170098DE RID: 39134
			// (set) Token: 0x0600C86E RID: 51310 RVA: 0x0011E49D File Offset: 0x0011C69D
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170098DF RID: 39135
			// (set) Token: 0x0600C86F RID: 51311 RVA: 0x0011E4B5 File Offset: 0x0011C6B5
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170098E0 RID: 39136
			// (set) Token: 0x0600C870 RID: 51312 RVA: 0x0011E4CD File Offset: 0x0011C6CD
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170098E1 RID: 39137
			// (set) Token: 0x0600C871 RID: 51313 RVA: 0x0011E4E5 File Offset: 0x0011C6E5
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170098E2 RID: 39138
			// (set) Token: 0x0600C872 RID: 51314 RVA: 0x0011E4FD File Offset: 0x0011C6FD
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170098E3 RID: 39139
			// (set) Token: 0x0600C873 RID: 51315 RVA: 0x0011E515 File Offset: 0x0011C715
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170098E4 RID: 39140
			// (set) Token: 0x0600C874 RID: 51316 RVA: 0x0011E52D File Offset: 0x0011C72D
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170098E5 RID: 39141
			// (set) Token: 0x0600C875 RID: 51317 RVA: 0x0011E545 File Offset: 0x0011C745
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x170098E6 RID: 39142
			// (set) Token: 0x0600C876 RID: 51318 RVA: 0x0011E55D File Offset: 0x0011C75D
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x170098E7 RID: 39143
			// (set) Token: 0x0600C877 RID: 51319 RVA: 0x0011E575 File Offset: 0x0011C775
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170098E8 RID: 39144
			// (set) Token: 0x0600C878 RID: 51320 RVA: 0x0011E58D File Offset: 0x0011C78D
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170098E9 RID: 39145
			// (set) Token: 0x0600C879 RID: 51321 RVA: 0x0011E5A5 File Offset: 0x0011C7A5
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170098EA RID: 39146
			// (set) Token: 0x0600C87A RID: 51322 RVA: 0x0011E5B8 File Offset: 0x0011C7B8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170098EB RID: 39147
			// (set) Token: 0x0600C87B RID: 51323 RVA: 0x0011E5D6 File Offset: 0x0011C7D6
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170098EC RID: 39148
			// (set) Token: 0x0600C87C RID: 51324 RVA: 0x0011E5E9 File Offset: 0x0011C7E9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170098ED RID: 39149
			// (set) Token: 0x0600C87D RID: 51325 RVA: 0x0011E607 File Offset: 0x0011C807
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170098EE RID: 39150
			// (set) Token: 0x0600C87E RID: 51326 RVA: 0x0011E61A File Offset: 0x0011C81A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170098EF RID: 39151
			// (set) Token: 0x0600C87F RID: 51327 RVA: 0x0011E62D File Offset: 0x0011C82D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170098F0 RID: 39152
			// (set) Token: 0x0600C880 RID: 51328 RVA: 0x0011E645 File Offset: 0x0011C845
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170098F1 RID: 39153
			// (set) Token: 0x0600C881 RID: 51329 RVA: 0x0011E65D File Offset: 0x0011C85D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170098F2 RID: 39154
			// (set) Token: 0x0600C882 RID: 51330 RVA: 0x0011E675 File Offset: 0x0011C875
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA2 RID: 3490
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170098F3 RID: 39155
			// (set) Token: 0x0600C884 RID: 51332 RVA: 0x0011E695 File Offset: 0x0011C895
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170098F4 RID: 39156
			// (set) Token: 0x0600C885 RID: 51333 RVA: 0x0011E6AD File Offset: 0x0011C8AD
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170098F5 RID: 39157
			// (set) Token: 0x0600C886 RID: 51334 RVA: 0x0011E6C5 File Offset: 0x0011C8C5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170098F6 RID: 39158
			// (set) Token: 0x0600C887 RID: 51335 RVA: 0x0011E6DD File Offset: 0x0011C8DD
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170098F7 RID: 39159
			// (set) Token: 0x0600C888 RID: 51336 RVA: 0x0011E6F0 File Offset: 0x0011C8F0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170098F8 RID: 39160
			// (set) Token: 0x0600C889 RID: 51337 RVA: 0x0011E70E File Offset: 0x0011C90E
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170098F9 RID: 39161
			// (set) Token: 0x0600C88A RID: 51338 RVA: 0x0011E726 File Offset: 0x0011C926
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170098FA RID: 39162
			// (set) Token: 0x0600C88B RID: 51339 RVA: 0x0011E73E File Offset: 0x0011C93E
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170098FB RID: 39163
			// (set) Token: 0x0600C88C RID: 51340 RVA: 0x0011E756 File Offset: 0x0011C956
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170098FC RID: 39164
			// (set) Token: 0x0600C88D RID: 51341 RVA: 0x0011E76E File Offset: 0x0011C96E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170098FD RID: 39165
			// (set) Token: 0x0600C88E RID: 51342 RVA: 0x0011E786 File Offset: 0x0011C986
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170098FE RID: 39166
			// (set) Token: 0x0600C88F RID: 51343 RVA: 0x0011E79E File Offset: 0x0011C99E
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170098FF RID: 39167
			// (set) Token: 0x0600C890 RID: 51344 RVA: 0x0011E7B6 File Offset: 0x0011C9B6
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009900 RID: 39168
			// (set) Token: 0x0600C891 RID: 51345 RVA: 0x0011E7CE File Offset: 0x0011C9CE
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x17009901 RID: 39169
			// (set) Token: 0x0600C892 RID: 51346 RVA: 0x0011E7E6 File Offset: 0x0011C9E6
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17009902 RID: 39170
			// (set) Token: 0x0600C893 RID: 51347 RVA: 0x0011E7FE File Offset: 0x0011C9FE
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17009903 RID: 39171
			// (set) Token: 0x0600C894 RID: 51348 RVA: 0x0011E816 File Offset: 0x0011CA16
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009904 RID: 39172
			// (set) Token: 0x0600C895 RID: 51349 RVA: 0x0011E82E File Offset: 0x0011CA2E
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009905 RID: 39173
			// (set) Token: 0x0600C896 RID: 51350 RVA: 0x0011E841 File Offset: 0x0011CA41
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009906 RID: 39174
			// (set) Token: 0x0600C897 RID: 51351 RVA: 0x0011E85F File Offset: 0x0011CA5F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009907 RID: 39175
			// (set) Token: 0x0600C898 RID: 51352 RVA: 0x0011E872 File Offset: 0x0011CA72
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009908 RID: 39176
			// (set) Token: 0x0600C899 RID: 51353 RVA: 0x0011E890 File Offset: 0x0011CA90
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009909 RID: 39177
			// (set) Token: 0x0600C89A RID: 51354 RVA: 0x0011E8A3 File Offset: 0x0011CAA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700990A RID: 39178
			// (set) Token: 0x0600C89B RID: 51355 RVA: 0x0011E8B6 File Offset: 0x0011CAB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700990B RID: 39179
			// (set) Token: 0x0600C89C RID: 51356 RVA: 0x0011E8CE File Offset: 0x0011CACE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700990C RID: 39180
			// (set) Token: 0x0600C89D RID: 51357 RVA: 0x0011E8E6 File Offset: 0x0011CAE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700990D RID: 39181
			// (set) Token: 0x0600C89E RID: 51358 RVA: 0x0011E8FE File Offset: 0x0011CAFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA3 RID: 3491
		public class MailboxPlanSetParameters : ParametersBase
		{
			// Token: 0x1700990E RID: 39182
			// (set) Token: 0x0600C8A0 RID: 51360 RVA: 0x0011E91E File Offset: 0x0011CB1E
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700990F RID: 39183
			// (set) Token: 0x0600C8A1 RID: 51361 RVA: 0x0011E936 File Offset: 0x0011CB36
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17009910 RID: 39184
			// (set) Token: 0x0600C8A2 RID: 51362 RVA: 0x0011E94E File Offset: 0x0011CB4E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009911 RID: 39185
			// (set) Token: 0x0600C8A3 RID: 51363 RVA: 0x0011E966 File Offset: 0x0011CB66
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17009912 RID: 39186
			// (set) Token: 0x0600C8A4 RID: 51364 RVA: 0x0011E979 File Offset: 0x0011CB79
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17009913 RID: 39187
			// (set) Token: 0x0600C8A5 RID: 51365 RVA: 0x0011E997 File Offset: 0x0011CB97
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009914 RID: 39188
			// (set) Token: 0x0600C8A6 RID: 51366 RVA: 0x0011E9AF File Offset: 0x0011CBAF
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17009915 RID: 39189
			// (set) Token: 0x0600C8A7 RID: 51367 RVA: 0x0011E9C7 File Offset: 0x0011CBC7
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17009916 RID: 39190
			// (set) Token: 0x0600C8A8 RID: 51368 RVA: 0x0011E9DF File Offset: 0x0011CBDF
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17009917 RID: 39191
			// (set) Token: 0x0600C8A9 RID: 51369 RVA: 0x0011E9F7 File Offset: 0x0011CBF7
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009918 RID: 39192
			// (set) Token: 0x0600C8AA RID: 51370 RVA: 0x0011EA0F File Offset: 0x0011CC0F
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17009919 RID: 39193
			// (set) Token: 0x0600C8AB RID: 51371 RVA: 0x0011EA27 File Offset: 0x0011CC27
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700991A RID: 39194
			// (set) Token: 0x0600C8AC RID: 51372 RVA: 0x0011EA3F File Offset: 0x0011CC3F
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700991B RID: 39195
			// (set) Token: 0x0600C8AD RID: 51373 RVA: 0x0011EA57 File Offset: 0x0011CC57
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x1700991C RID: 39196
			// (set) Token: 0x0600C8AE RID: 51374 RVA: 0x0011EA6F File Offset: 0x0011CC6F
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x1700991D RID: 39197
			// (set) Token: 0x0600C8AF RID: 51375 RVA: 0x0011EA87 File Offset: 0x0011CC87
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x1700991E RID: 39198
			// (set) Token: 0x0600C8B0 RID: 51376 RVA: 0x0011EA9F File Offset: 0x0011CC9F
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700991F RID: 39199
			// (set) Token: 0x0600C8B1 RID: 51377 RVA: 0x0011EAB7 File Offset: 0x0011CCB7
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009920 RID: 39200
			// (set) Token: 0x0600C8B2 RID: 51378 RVA: 0x0011EACA File Offset: 0x0011CCCA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009921 RID: 39201
			// (set) Token: 0x0600C8B3 RID: 51379 RVA: 0x0011EAE8 File Offset: 0x0011CCE8
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009922 RID: 39202
			// (set) Token: 0x0600C8B4 RID: 51380 RVA: 0x0011EAFB File Offset: 0x0011CCFB
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009923 RID: 39203
			// (set) Token: 0x0600C8B5 RID: 51381 RVA: 0x0011EB19 File Offset: 0x0011CD19
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009924 RID: 39204
			// (set) Token: 0x0600C8B6 RID: 51382 RVA: 0x0011EB2C File Offset: 0x0011CD2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009925 RID: 39205
			// (set) Token: 0x0600C8B7 RID: 51383 RVA: 0x0011EB3F File Offset: 0x0011CD3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009926 RID: 39206
			// (set) Token: 0x0600C8B8 RID: 51384 RVA: 0x0011EB57 File Offset: 0x0011CD57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009927 RID: 39207
			// (set) Token: 0x0600C8B9 RID: 51385 RVA: 0x0011EB6F File Offset: 0x0011CD6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009928 RID: 39208
			// (set) Token: 0x0600C8BA RID: 51386 RVA: 0x0011EB87 File Offset: 0x0011CD87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA4 RID: 3492
		public class ServerSetParameters : ParametersBase
		{
			// Token: 0x17009929 RID: 39209
			// (set) Token: 0x0600C8BC RID: 51388 RVA: 0x0011EBA7 File Offset: 0x0011CDA7
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700992A RID: 39210
			// (set) Token: 0x0600C8BD RID: 51389 RVA: 0x0011EBBF File Offset: 0x0011CDBF
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700992B RID: 39211
			// (set) Token: 0x0600C8BE RID: 51390 RVA: 0x0011EBD7 File Offset: 0x0011CDD7
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700992C RID: 39212
			// (set) Token: 0x0600C8BF RID: 51391 RVA: 0x0011EBEF File Offset: 0x0011CDEF
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700992D RID: 39213
			// (set) Token: 0x0600C8C0 RID: 51392 RVA: 0x0011EC02 File Offset: 0x0011CE02
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700992E RID: 39214
			// (set) Token: 0x0600C8C1 RID: 51393 RVA: 0x0011EC15 File Offset: 0x0011CE15
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700992F RID: 39215
			// (set) Token: 0x0600C8C2 RID: 51394 RVA: 0x0011EC2D File Offset: 0x0011CE2D
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17009930 RID: 39216
			// (set) Token: 0x0600C8C3 RID: 51395 RVA: 0x0011EC45 File Offset: 0x0011CE45
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17009931 RID: 39217
			// (set) Token: 0x0600C8C4 RID: 51396 RVA: 0x0011EC5D File Offset: 0x0011CE5D
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17009932 RID: 39218
			// (set) Token: 0x0600C8C5 RID: 51397 RVA: 0x0011EC75 File Offset: 0x0011CE75
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009933 RID: 39219
			// (set) Token: 0x0600C8C6 RID: 51398 RVA: 0x0011EC8D File Offset: 0x0011CE8D
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17009934 RID: 39220
			// (set) Token: 0x0600C8C7 RID: 51399 RVA: 0x0011ECA5 File Offset: 0x0011CEA5
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009935 RID: 39221
			// (set) Token: 0x0600C8C8 RID: 51400 RVA: 0x0011ECBD File Offset: 0x0011CEBD
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009936 RID: 39222
			// (set) Token: 0x0600C8C9 RID: 51401 RVA: 0x0011ECD5 File Offset: 0x0011CED5
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x17009937 RID: 39223
			// (set) Token: 0x0600C8CA RID: 51402 RVA: 0x0011ECED File Offset: 0x0011CEED
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17009938 RID: 39224
			// (set) Token: 0x0600C8CB RID: 51403 RVA: 0x0011ED05 File Offset: 0x0011CF05
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17009939 RID: 39225
			// (set) Token: 0x0600C8CC RID: 51404 RVA: 0x0011ED1D File Offset: 0x0011CF1D
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700993A RID: 39226
			// (set) Token: 0x0600C8CD RID: 51405 RVA: 0x0011ED35 File Offset: 0x0011CF35
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700993B RID: 39227
			// (set) Token: 0x0600C8CE RID: 51406 RVA: 0x0011ED48 File Offset: 0x0011CF48
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700993C RID: 39228
			// (set) Token: 0x0600C8CF RID: 51407 RVA: 0x0011ED66 File Offset: 0x0011CF66
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700993D RID: 39229
			// (set) Token: 0x0600C8D0 RID: 51408 RVA: 0x0011ED79 File Offset: 0x0011CF79
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700993E RID: 39230
			// (set) Token: 0x0600C8D1 RID: 51409 RVA: 0x0011ED97 File Offset: 0x0011CF97
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700993F RID: 39231
			// (set) Token: 0x0600C8D2 RID: 51410 RVA: 0x0011EDAA File Offset: 0x0011CFAA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009940 RID: 39232
			// (set) Token: 0x0600C8D3 RID: 51411 RVA: 0x0011EDBD File Offset: 0x0011CFBD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009941 RID: 39233
			// (set) Token: 0x0600C8D4 RID: 51412 RVA: 0x0011EDD5 File Offset: 0x0011CFD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009942 RID: 39234
			// (set) Token: 0x0600C8D5 RID: 51413 RVA: 0x0011EDED File Offset: 0x0011CFED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009943 RID: 39235
			// (set) Token: 0x0600C8D6 RID: 51414 RVA: 0x0011EE05 File Offset: 0x0011D005
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA5 RID: 3493
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17009944 RID: 39236
			// (set) Token: 0x0600C8D8 RID: 51416 RVA: 0x0011EE25 File Offset: 0x0011D025
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17009945 RID: 39237
			// (set) Token: 0x0600C8D9 RID: 51417 RVA: 0x0011EE3D File Offset: 0x0011D03D
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17009946 RID: 39238
			// (set) Token: 0x0600C8DA RID: 51418 RVA: 0x0011EE55 File Offset: 0x0011D055
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009947 RID: 39239
			// (set) Token: 0x0600C8DB RID: 51419 RVA: 0x0011EE6D File Offset: 0x0011D06D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17009948 RID: 39240
			// (set) Token: 0x0600C8DC RID: 51420 RVA: 0x0011EE80 File Offset: 0x0011D080
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17009949 RID: 39241
			// (set) Token: 0x0600C8DD RID: 51421 RVA: 0x0011EE93 File Offset: 0x0011D093
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700994A RID: 39242
			// (set) Token: 0x0600C8DE RID: 51422 RVA: 0x0011EEAB File Offset: 0x0011D0AB
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700994B RID: 39243
			// (set) Token: 0x0600C8DF RID: 51423 RVA: 0x0011EEC3 File Offset: 0x0011D0C3
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700994C RID: 39244
			// (set) Token: 0x0600C8E0 RID: 51424 RVA: 0x0011EEDB File Offset: 0x0011D0DB
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700994D RID: 39245
			// (set) Token: 0x0600C8E1 RID: 51425 RVA: 0x0011EEF3 File Offset: 0x0011D0F3
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700994E RID: 39246
			// (set) Token: 0x0600C8E2 RID: 51426 RVA: 0x0011EF0B File Offset: 0x0011D10B
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x1700994F RID: 39247
			// (set) Token: 0x0600C8E3 RID: 51427 RVA: 0x0011EF23 File Offset: 0x0011D123
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009950 RID: 39248
			// (set) Token: 0x0600C8E4 RID: 51428 RVA: 0x0011EF3B File Offset: 0x0011D13B
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009951 RID: 39249
			// (set) Token: 0x0600C8E5 RID: 51429 RVA: 0x0011EF53 File Offset: 0x0011D153
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x17009952 RID: 39250
			// (set) Token: 0x0600C8E6 RID: 51430 RVA: 0x0011EF6B File Offset: 0x0011D16B
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17009953 RID: 39251
			// (set) Token: 0x0600C8E7 RID: 51431 RVA: 0x0011EF83 File Offset: 0x0011D183
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17009954 RID: 39252
			// (set) Token: 0x0600C8E8 RID: 51432 RVA: 0x0011EF9B File Offset: 0x0011D19B
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009955 RID: 39253
			// (set) Token: 0x0600C8E9 RID: 51433 RVA: 0x0011EFB3 File Offset: 0x0011D1B3
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009956 RID: 39254
			// (set) Token: 0x0600C8EA RID: 51434 RVA: 0x0011EFC6 File Offset: 0x0011D1C6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009957 RID: 39255
			// (set) Token: 0x0600C8EB RID: 51435 RVA: 0x0011EFE4 File Offset: 0x0011D1E4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009958 RID: 39256
			// (set) Token: 0x0600C8EC RID: 51436 RVA: 0x0011EFF7 File Offset: 0x0011D1F7
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009959 RID: 39257
			// (set) Token: 0x0600C8ED RID: 51437 RVA: 0x0011F015 File Offset: 0x0011D215
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700995A RID: 39258
			// (set) Token: 0x0600C8EE RID: 51438 RVA: 0x0011F028 File Offset: 0x0011D228
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700995B RID: 39259
			// (set) Token: 0x0600C8EF RID: 51439 RVA: 0x0011F03B File Offset: 0x0011D23B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700995C RID: 39260
			// (set) Token: 0x0600C8F0 RID: 51440 RVA: 0x0011F053 File Offset: 0x0011D253
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700995D RID: 39261
			// (set) Token: 0x0600C8F1 RID: 51441 RVA: 0x0011F06B File Offset: 0x0011D26B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700995E RID: 39262
			// (set) Token: 0x0600C8F2 RID: 51442 RVA: 0x0011F083 File Offset: 0x0011D283
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA6 RID: 3494
		public class DatabaseSetParameters : ParametersBase
		{
			// Token: 0x1700995F RID: 39263
			// (set) Token: 0x0600C8F4 RID: 51444 RVA: 0x0011F0A3 File Offset: 0x0011D2A3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17009960 RID: 39264
			// (set) Token: 0x0600C8F5 RID: 51445 RVA: 0x0011F0BB File Offset: 0x0011D2BB
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17009961 RID: 39265
			// (set) Token: 0x0600C8F6 RID: 51446 RVA: 0x0011F0D3 File Offset: 0x0011D2D3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009962 RID: 39266
			// (set) Token: 0x0600C8F7 RID: 51447 RVA: 0x0011F0EB File Offset: 0x0011D2EB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17009963 RID: 39267
			// (set) Token: 0x0600C8F8 RID: 51448 RVA: 0x0011F0FE File Offset: 0x0011D2FE
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17009964 RID: 39268
			// (set) Token: 0x0600C8F9 RID: 51449 RVA: 0x0011F111 File Offset: 0x0011D311
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17009965 RID: 39269
			// (set) Token: 0x0600C8FA RID: 51450 RVA: 0x0011F129 File Offset: 0x0011D329
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17009966 RID: 39270
			// (set) Token: 0x0600C8FB RID: 51451 RVA: 0x0011F141 File Offset: 0x0011D341
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17009967 RID: 39271
			// (set) Token: 0x0600C8FC RID: 51452 RVA: 0x0011F159 File Offset: 0x0011D359
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17009968 RID: 39272
			// (set) Token: 0x0600C8FD RID: 51453 RVA: 0x0011F171 File Offset: 0x0011D371
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17009969 RID: 39273
			// (set) Token: 0x0600C8FE RID: 51454 RVA: 0x0011F189 File Offset: 0x0011D389
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x1700996A RID: 39274
			// (set) Token: 0x0600C8FF RID: 51455 RVA: 0x0011F1A1 File Offset: 0x0011D3A1
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700996B RID: 39275
			// (set) Token: 0x0600C900 RID: 51456 RVA: 0x0011F1B9 File Offset: 0x0011D3B9
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700996C RID: 39276
			// (set) Token: 0x0600C901 RID: 51457 RVA: 0x0011F1D1 File Offset: 0x0011D3D1
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x1700996D RID: 39277
			// (set) Token: 0x0600C902 RID: 51458 RVA: 0x0011F1E9 File Offset: 0x0011D3E9
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x1700996E RID: 39278
			// (set) Token: 0x0600C903 RID: 51459 RVA: 0x0011F201 File Offset: 0x0011D401
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x1700996F RID: 39279
			// (set) Token: 0x0600C904 RID: 51460 RVA: 0x0011F219 File Offset: 0x0011D419
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009970 RID: 39280
			// (set) Token: 0x0600C905 RID: 51461 RVA: 0x0011F231 File Offset: 0x0011D431
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009971 RID: 39281
			// (set) Token: 0x0600C906 RID: 51462 RVA: 0x0011F244 File Offset: 0x0011D444
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009972 RID: 39282
			// (set) Token: 0x0600C907 RID: 51463 RVA: 0x0011F262 File Offset: 0x0011D462
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009973 RID: 39283
			// (set) Token: 0x0600C908 RID: 51464 RVA: 0x0011F275 File Offset: 0x0011D475
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17009974 RID: 39284
			// (set) Token: 0x0600C909 RID: 51465 RVA: 0x0011F293 File Offset: 0x0011D493
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17009975 RID: 39285
			// (set) Token: 0x0600C90A RID: 51466 RVA: 0x0011F2A6 File Offset: 0x0011D4A6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009976 RID: 39286
			// (set) Token: 0x0600C90B RID: 51467 RVA: 0x0011F2B9 File Offset: 0x0011D4B9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009977 RID: 39287
			// (set) Token: 0x0600C90C RID: 51468 RVA: 0x0011F2D1 File Offset: 0x0011D4D1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009978 RID: 39288
			// (set) Token: 0x0600C90D RID: 51469 RVA: 0x0011F2E9 File Offset: 0x0011D4E9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009979 RID: 39289
			// (set) Token: 0x0600C90E RID: 51470 RVA: 0x0011F301 File Offset: 0x0011D501
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000DA7 RID: 3495
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700997A RID: 39290
			// (set) Token: 0x0600C910 RID: 51472 RVA: 0x0011F321 File Offset: 0x0011D521
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700997B RID: 39291
			// (set) Token: 0x0600C911 RID: 51473 RVA: 0x0011F339 File Offset: 0x0011D539
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700997C RID: 39292
			// (set) Token: 0x0600C912 RID: 51474 RVA: 0x0011F351 File Offset: 0x0011D551
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700997D RID: 39293
			// (set) Token: 0x0600C913 RID: 51475 RVA: 0x0011F369 File Offset: 0x0011D569
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700997E RID: 39294
			// (set) Token: 0x0600C914 RID: 51476 RVA: 0x0011F381 File Offset: 0x0011D581
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700997F RID: 39295
			// (set) Token: 0x0600C915 RID: 51477 RVA: 0x0011F399 File Offset: 0x0011D599
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17009980 RID: 39296
			// (set) Token: 0x0600C916 RID: 51478 RVA: 0x0011F3B1 File Offset: 0x0011D5B1
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009981 RID: 39297
			// (set) Token: 0x0600C917 RID: 51479 RVA: 0x0011F3C9 File Offset: 0x0011D5C9
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17009982 RID: 39298
			// (set) Token: 0x0600C918 RID: 51480 RVA: 0x0011F3E1 File Offset: 0x0011D5E1
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x17009983 RID: 39299
			// (set) Token: 0x0600C919 RID: 51481 RVA: 0x0011F3F9 File Offset: 0x0011D5F9
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17009984 RID: 39300
			// (set) Token: 0x0600C91A RID: 51482 RVA: 0x0011F411 File Offset: 0x0011D611
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17009985 RID: 39301
			// (set) Token: 0x0600C91B RID: 51483 RVA: 0x0011F429 File Offset: 0x0011D629
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009986 RID: 39302
			// (set) Token: 0x0600C91C RID: 51484 RVA: 0x0011F441 File Offset: 0x0011D641
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17009987 RID: 39303
			// (set) Token: 0x0600C91D RID: 51485 RVA: 0x0011F454 File Offset: 0x0011D654
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009988 RID: 39304
			// (set) Token: 0x0600C91E RID: 51486 RVA: 0x0011F472 File Offset: 0x0011D672
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17009989 RID: 39305
			// (set) Token: 0x0600C91F RID: 51487 RVA: 0x0011F485 File Offset: 0x0011D685
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700998A RID: 39306
			// (set) Token: 0x0600C920 RID: 51488 RVA: 0x0011F4A3 File Offset: 0x0011D6A3
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700998B RID: 39307
			// (set) Token: 0x0600C921 RID: 51489 RVA: 0x0011F4B6 File Offset: 0x0011D6B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700998C RID: 39308
			// (set) Token: 0x0600C922 RID: 51490 RVA: 0x0011F4C9 File Offset: 0x0011D6C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700998D RID: 39309
			// (set) Token: 0x0600C923 RID: 51491 RVA: 0x0011F4E1 File Offset: 0x0011D6E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700998E RID: 39310
			// (set) Token: 0x0600C924 RID: 51492 RVA: 0x0011F4F9 File Offset: 0x0011D6F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700998F RID: 39311
			// (set) Token: 0x0600C925 RID: 51493 RVA: 0x0011F511 File Offset: 0x0011D711
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
