using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BCF RID: 3023
	public class GetCASMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x060091C0 RID: 37312 RVA: 0x000D4DC3 File Offset: 0x000D2FC3
		private GetCASMailboxCommand() : base("Get-CASMailbox")
		{
		}

		// Token: 0x060091C1 RID: 37313 RVA: 0x000D4DD0 File Offset: 0x000D2FD0
		public GetCASMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x000D4DDF File Offset: 0x000D2FDF
		public virtual GetCASMailboxCommand SetParameters(GetCASMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x000D4DE9 File Offset: 0x000D2FE9
		public virtual GetCASMailboxCommand SetParameters(GetCASMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x000D4DF3 File Offset: 0x000D2FF3
		public virtual GetCASMailboxCommand SetParameters(GetCASMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BD0 RID: 3024
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065DB RID: 26075
			// (set) Token: 0x060091C5 RID: 37317 RVA: 0x000D4DFD File Offset: 0x000D2FFD
			public virtual SwitchParameter GetPopProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetPopProtocolLog"] = value;
				}
			}

			// Token: 0x170065DC RID: 26076
			// (set) Token: 0x060091C6 RID: 37318 RVA: 0x000D4E15 File Offset: 0x000D3015
			public virtual SwitchParameter GetImapProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetImapProtocolLog"] = value;
				}
			}

			// Token: 0x170065DD RID: 26077
			// (set) Token: 0x060091C7 RID: 37319 RVA: 0x000D4E2D File Offset: 0x000D302D
			public virtual MultiValuedProperty<SmtpAddress> SendLogsTo
			{
				set
				{
					base.PowerSharpParameters["SendLogsTo"] = value;
				}
			}

			// Token: 0x170065DE RID: 26078
			// (set) Token: 0x060091C8 RID: 37320 RVA: 0x000D4E40 File Offset: 0x000D3040
			public virtual SwitchParameter ProtocolSettings
			{
				set
				{
					base.PowerSharpParameters["ProtocolSettings"] = value;
				}
			}

			// Token: 0x170065DF RID: 26079
			// (set) Token: 0x060091C9 RID: 37321 RVA: 0x000D4E58 File Offset: 0x000D3058
			public virtual SwitchParameter ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x170065E0 RID: 26080
			// (set) Token: 0x060091CA RID: 37322 RVA: 0x000D4E70 File Offset: 0x000D3070
			public virtual SwitchParameter RecalculateHasActiveSyncDevicePartnership
			{
				set
				{
					base.PowerSharpParameters["RecalculateHasActiveSyncDevicePartnership"] = value;
				}
			}

			// Token: 0x170065E1 RID: 26081
			// (set) Token: 0x060091CB RID: 37323 RVA: 0x000D4E88 File Offset: 0x000D3088
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170065E2 RID: 26082
			// (set) Token: 0x060091CC RID: 37324 RVA: 0x000D4EA0 File Offset: 0x000D30A0
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170065E3 RID: 26083
			// (set) Token: 0x060091CD RID: 37325 RVA: 0x000D4EB3 File Offset: 0x000D30B3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170065E4 RID: 26084
			// (set) Token: 0x060091CE RID: 37326 RVA: 0x000D4ED1 File Offset: 0x000D30D1
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170065E5 RID: 26085
			// (set) Token: 0x060091CF RID: 37327 RVA: 0x000D4EE4 File Offset: 0x000D30E4
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170065E6 RID: 26086
			// (set) Token: 0x060091D0 RID: 37328 RVA: 0x000D4EF7 File Offset: 0x000D30F7
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170065E7 RID: 26087
			// (set) Token: 0x060091D1 RID: 37329 RVA: 0x000D4F15 File Offset: 0x000D3115
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170065E8 RID: 26088
			// (set) Token: 0x060091D2 RID: 37330 RVA: 0x000D4F2D File Offset: 0x000D312D
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170065E9 RID: 26089
			// (set) Token: 0x060091D3 RID: 37331 RVA: 0x000D4F40 File Offset: 0x000D3140
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170065EA RID: 26090
			// (set) Token: 0x060091D4 RID: 37332 RVA: 0x000D4F58 File Offset: 0x000D3158
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170065EB RID: 26091
			// (set) Token: 0x060091D5 RID: 37333 RVA: 0x000D4F70 File Offset: 0x000D3170
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170065EC RID: 26092
			// (set) Token: 0x060091D6 RID: 37334 RVA: 0x000D4F83 File Offset: 0x000D3183
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065ED RID: 26093
			// (set) Token: 0x060091D7 RID: 37335 RVA: 0x000D4F9B File Offset: 0x000D319B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065EE RID: 26094
			// (set) Token: 0x060091D8 RID: 37336 RVA: 0x000D4FB3 File Offset: 0x000D31B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065EF RID: 26095
			// (set) Token: 0x060091D9 RID: 37337 RVA: 0x000D4FCB File Offset: 0x000D31CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BD1 RID: 3025
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170065F0 RID: 26096
			// (set) Token: 0x060091DB RID: 37339 RVA: 0x000D4FEB File Offset: 0x000D31EB
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170065F1 RID: 26097
			// (set) Token: 0x060091DC RID: 37340 RVA: 0x000D4FFE File Offset: 0x000D31FE
			public virtual SwitchParameter GetPopProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetPopProtocolLog"] = value;
				}
			}

			// Token: 0x170065F2 RID: 26098
			// (set) Token: 0x060091DD RID: 37341 RVA: 0x000D5016 File Offset: 0x000D3216
			public virtual SwitchParameter GetImapProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetImapProtocolLog"] = value;
				}
			}

			// Token: 0x170065F3 RID: 26099
			// (set) Token: 0x060091DE RID: 37342 RVA: 0x000D502E File Offset: 0x000D322E
			public virtual MultiValuedProperty<SmtpAddress> SendLogsTo
			{
				set
				{
					base.PowerSharpParameters["SendLogsTo"] = value;
				}
			}

			// Token: 0x170065F4 RID: 26100
			// (set) Token: 0x060091DF RID: 37343 RVA: 0x000D5041 File Offset: 0x000D3241
			public virtual SwitchParameter ProtocolSettings
			{
				set
				{
					base.PowerSharpParameters["ProtocolSettings"] = value;
				}
			}

			// Token: 0x170065F5 RID: 26101
			// (set) Token: 0x060091E0 RID: 37344 RVA: 0x000D5059 File Offset: 0x000D3259
			public virtual SwitchParameter ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x170065F6 RID: 26102
			// (set) Token: 0x060091E1 RID: 37345 RVA: 0x000D5071 File Offset: 0x000D3271
			public virtual SwitchParameter RecalculateHasActiveSyncDevicePartnership
			{
				set
				{
					base.PowerSharpParameters["RecalculateHasActiveSyncDevicePartnership"] = value;
				}
			}

			// Token: 0x170065F7 RID: 26103
			// (set) Token: 0x060091E2 RID: 37346 RVA: 0x000D5089 File Offset: 0x000D3289
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170065F8 RID: 26104
			// (set) Token: 0x060091E3 RID: 37347 RVA: 0x000D50A1 File Offset: 0x000D32A1
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170065F9 RID: 26105
			// (set) Token: 0x060091E4 RID: 37348 RVA: 0x000D50B4 File Offset: 0x000D32B4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170065FA RID: 26106
			// (set) Token: 0x060091E5 RID: 37349 RVA: 0x000D50D2 File Offset: 0x000D32D2
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170065FB RID: 26107
			// (set) Token: 0x060091E6 RID: 37350 RVA: 0x000D50E5 File Offset: 0x000D32E5
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170065FC RID: 26108
			// (set) Token: 0x060091E7 RID: 37351 RVA: 0x000D50F8 File Offset: 0x000D32F8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170065FD RID: 26109
			// (set) Token: 0x060091E8 RID: 37352 RVA: 0x000D5116 File Offset: 0x000D3316
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170065FE RID: 26110
			// (set) Token: 0x060091E9 RID: 37353 RVA: 0x000D512E File Offset: 0x000D332E
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170065FF RID: 26111
			// (set) Token: 0x060091EA RID: 37354 RVA: 0x000D5141 File Offset: 0x000D3341
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006600 RID: 26112
			// (set) Token: 0x060091EB RID: 37355 RVA: 0x000D5159 File Offset: 0x000D3359
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006601 RID: 26113
			// (set) Token: 0x060091EC RID: 37356 RVA: 0x000D5171 File Offset: 0x000D3371
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006602 RID: 26114
			// (set) Token: 0x060091ED RID: 37357 RVA: 0x000D5184 File Offset: 0x000D3384
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006603 RID: 26115
			// (set) Token: 0x060091EE RID: 37358 RVA: 0x000D519C File Offset: 0x000D339C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006604 RID: 26116
			// (set) Token: 0x060091EF RID: 37359 RVA: 0x000D51B4 File Offset: 0x000D33B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006605 RID: 26117
			// (set) Token: 0x060091F0 RID: 37360 RVA: 0x000D51CC File Offset: 0x000D33CC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BD2 RID: 3026
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006606 RID: 26118
			// (set) Token: 0x060091F2 RID: 37362 RVA: 0x000D51EC File Offset: 0x000D33EC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006607 RID: 26119
			// (set) Token: 0x060091F3 RID: 37363 RVA: 0x000D520A File Offset: 0x000D340A
			public virtual SwitchParameter GetPopProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetPopProtocolLog"] = value;
				}
			}

			// Token: 0x17006608 RID: 26120
			// (set) Token: 0x060091F4 RID: 37364 RVA: 0x000D5222 File Offset: 0x000D3422
			public virtual SwitchParameter GetImapProtocolLog
			{
				set
				{
					base.PowerSharpParameters["GetImapProtocolLog"] = value;
				}
			}

			// Token: 0x17006609 RID: 26121
			// (set) Token: 0x060091F5 RID: 37365 RVA: 0x000D523A File Offset: 0x000D343A
			public virtual MultiValuedProperty<SmtpAddress> SendLogsTo
			{
				set
				{
					base.PowerSharpParameters["SendLogsTo"] = value;
				}
			}

			// Token: 0x1700660A RID: 26122
			// (set) Token: 0x060091F6 RID: 37366 RVA: 0x000D524D File Offset: 0x000D344D
			public virtual SwitchParameter ProtocolSettings
			{
				set
				{
					base.PowerSharpParameters["ProtocolSettings"] = value;
				}
			}

			// Token: 0x1700660B RID: 26123
			// (set) Token: 0x060091F7 RID: 37367 RVA: 0x000D5265 File Offset: 0x000D3465
			public virtual SwitchParameter ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x1700660C RID: 26124
			// (set) Token: 0x060091F8 RID: 37368 RVA: 0x000D527D File Offset: 0x000D347D
			public virtual SwitchParameter RecalculateHasActiveSyncDevicePartnership
			{
				set
				{
					base.PowerSharpParameters["RecalculateHasActiveSyncDevicePartnership"] = value;
				}
			}

			// Token: 0x1700660D RID: 26125
			// (set) Token: 0x060091F9 RID: 37369 RVA: 0x000D5295 File Offset: 0x000D3495
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x1700660E RID: 26126
			// (set) Token: 0x060091FA RID: 37370 RVA: 0x000D52AD File Offset: 0x000D34AD
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700660F RID: 26127
			// (set) Token: 0x060091FB RID: 37371 RVA: 0x000D52C0 File Offset: 0x000D34C0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006610 RID: 26128
			// (set) Token: 0x060091FC RID: 37372 RVA: 0x000D52DE File Offset: 0x000D34DE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006611 RID: 26129
			// (set) Token: 0x060091FD RID: 37373 RVA: 0x000D52F1 File Offset: 0x000D34F1
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006612 RID: 26130
			// (set) Token: 0x060091FE RID: 37374 RVA: 0x000D5304 File Offset: 0x000D3504
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006613 RID: 26131
			// (set) Token: 0x060091FF RID: 37375 RVA: 0x000D5322 File Offset: 0x000D3522
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006614 RID: 26132
			// (set) Token: 0x06009200 RID: 37376 RVA: 0x000D533A File Offset: 0x000D353A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006615 RID: 26133
			// (set) Token: 0x06009201 RID: 37377 RVA: 0x000D534D File Offset: 0x000D354D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006616 RID: 26134
			// (set) Token: 0x06009202 RID: 37378 RVA: 0x000D5365 File Offset: 0x000D3565
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006617 RID: 26135
			// (set) Token: 0x06009203 RID: 37379 RVA: 0x000D537D File Offset: 0x000D357D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006618 RID: 26136
			// (set) Token: 0x06009204 RID: 37380 RVA: 0x000D5390 File Offset: 0x000D3590
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006619 RID: 26137
			// (set) Token: 0x06009205 RID: 37381 RVA: 0x000D53A8 File Offset: 0x000D35A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700661A RID: 26138
			// (set) Token: 0x06009206 RID: 37382 RVA: 0x000D53C0 File Offset: 0x000D35C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700661B RID: 26139
			// (set) Token: 0x06009207 RID: 37383 RVA: 0x000D53D8 File Offset: 0x000D35D8
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
