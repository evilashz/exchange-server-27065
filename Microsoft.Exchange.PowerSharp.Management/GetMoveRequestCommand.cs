using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009CC RID: 2508
	public class GetMoveRequestCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06007D9F RID: 32159 RVA: 0x000BACE5 File Offset: 0x000B8EE5
		private GetMoveRequestCommand() : base("Get-MoveRequest")
		{
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x000BACF2 File Offset: 0x000B8EF2
		public GetMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x000BAD01 File Offset: 0x000B8F01
		public virtual GetMoveRequestCommand SetParameters(GetMoveRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x000BAD0B File Offset: 0x000B8F0B
		public virtual GetMoveRequestCommand SetParameters(GetMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x000BAD15 File Offset: 0x000B8F15
		public virtual GetMoveRequestCommand SetParameters(GetMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009CD RID: 2509
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x170055C0 RID: 21952
			// (set) Token: 0x06007DA4 RID: 32164 RVA: 0x000BAD1F File Offset: 0x000B8F1F
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x170055C1 RID: 21953
			// (set) Token: 0x06007DA5 RID: 32165 RVA: 0x000BAD32 File Offset: 0x000B8F32
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x170055C2 RID: 21954
			// (set) Token: 0x06007DA6 RID: 32166 RVA: 0x000BAD45 File Offset: 0x000B8F45
			public virtual RequestStatus MoveStatus
			{
				set
				{
					base.PowerSharpParameters["MoveStatus"] = value;
				}
			}

			// Token: 0x170055C3 RID: 21955
			// (set) Token: 0x06007DA7 RID: 32167 RVA: 0x000BAD5D File Offset: 0x000B8F5D
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x170055C4 RID: 21956
			// (set) Token: 0x06007DA8 RID: 32168 RVA: 0x000BAD70 File Offset: 0x000B8F70
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170055C5 RID: 21957
			// (set) Token: 0x06007DA9 RID: 32169 RVA: 0x000BAD83 File Offset: 0x000B8F83
			public virtual bool Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x170055C6 RID: 21958
			// (set) Token: 0x06007DAA RID: 32170 RVA: 0x000BAD9B File Offset: 0x000B8F9B
			public virtual bool Offline
			{
				set
				{
					base.PowerSharpParameters["Offline"] = value;
				}
			}

			// Token: 0x170055C7 RID: 21959
			// (set) Token: 0x06007DAB RID: 32171 RVA: 0x000BADB3 File Offset: 0x000B8FB3
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170055C8 RID: 21960
			// (set) Token: 0x06007DAC RID: 32172 RVA: 0x000BADCB File Offset: 0x000B8FCB
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170055C9 RID: 21961
			// (set) Token: 0x06007DAD RID: 32173 RVA: 0x000BADE3 File Offset: 0x000B8FE3
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x170055CA RID: 21962
			// (set) Token: 0x06007DAE RID: 32174 RVA: 0x000BADFB File Offset: 0x000B8FFB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170055CB RID: 21963
			// (set) Token: 0x06007DAF RID: 32175 RVA: 0x000BAE0E File Offset: 0x000B900E
			public virtual RequestFlags Flags
			{
				set
				{
					base.PowerSharpParameters["Flags"] = value;
				}
			}

			// Token: 0x170055CC RID: 21964
			// (set) Token: 0x06007DB0 RID: 32176 RVA: 0x000BAE26 File Offset: 0x000B9026
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170055CD RID: 21965
			// (set) Token: 0x06007DB1 RID: 32177 RVA: 0x000BAE3E File Offset: 0x000B903E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170055CE RID: 21966
			// (set) Token: 0x06007DB2 RID: 32178 RVA: 0x000BAE5C File Offset: 0x000B905C
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170055CF RID: 21967
			// (set) Token: 0x06007DB3 RID: 32179 RVA: 0x000BAE6F File Offset: 0x000B906F
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170055D0 RID: 21968
			// (set) Token: 0x06007DB4 RID: 32180 RVA: 0x000BAE8D File Offset: 0x000B908D
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170055D1 RID: 21969
			// (set) Token: 0x06007DB5 RID: 32181 RVA: 0x000BAEA0 File Offset: 0x000B90A0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170055D2 RID: 21970
			// (set) Token: 0x06007DB6 RID: 32182 RVA: 0x000BAEB8 File Offset: 0x000B90B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055D3 RID: 21971
			// (set) Token: 0x06007DB7 RID: 32183 RVA: 0x000BAECB File Offset: 0x000B90CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055D4 RID: 21972
			// (set) Token: 0x06007DB8 RID: 32184 RVA: 0x000BAEE3 File Offset: 0x000B90E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055D5 RID: 21973
			// (set) Token: 0x06007DB9 RID: 32185 RVA: 0x000BAEFB File Offset: 0x000B90FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055D6 RID: 21974
			// (set) Token: 0x06007DBA RID: 32186 RVA: 0x000BAF13 File Offset: 0x000B9113
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009CE RID: 2510
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170055D7 RID: 21975
			// (set) Token: 0x06007DBC RID: 32188 RVA: 0x000BAF33 File Offset: 0x000B9133
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170055D8 RID: 21976
			// (set) Token: 0x06007DBD RID: 32189 RVA: 0x000BAF46 File Offset: 0x000B9146
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170055D9 RID: 21977
			// (set) Token: 0x06007DBE RID: 32190 RVA: 0x000BAF5E File Offset: 0x000B915E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170055DA RID: 21978
			// (set) Token: 0x06007DBF RID: 32191 RVA: 0x000BAF7C File Offset: 0x000B917C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170055DB RID: 21979
			// (set) Token: 0x06007DC0 RID: 32192 RVA: 0x000BAF9A File Offset: 0x000B919A
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170055DC RID: 21980
			// (set) Token: 0x06007DC1 RID: 32193 RVA: 0x000BAFAD File Offset: 0x000B91AD
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170055DD RID: 21981
			// (set) Token: 0x06007DC2 RID: 32194 RVA: 0x000BAFCB File Offset: 0x000B91CB
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170055DE RID: 21982
			// (set) Token: 0x06007DC3 RID: 32195 RVA: 0x000BAFDE File Offset: 0x000B91DE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170055DF RID: 21983
			// (set) Token: 0x06007DC4 RID: 32196 RVA: 0x000BAFF6 File Offset: 0x000B91F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055E0 RID: 21984
			// (set) Token: 0x06007DC5 RID: 32197 RVA: 0x000BB009 File Offset: 0x000B9209
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055E1 RID: 21985
			// (set) Token: 0x06007DC6 RID: 32198 RVA: 0x000BB021 File Offset: 0x000B9221
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055E2 RID: 21986
			// (set) Token: 0x06007DC7 RID: 32199 RVA: 0x000BB039 File Offset: 0x000B9239
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055E3 RID: 21987
			// (set) Token: 0x06007DC8 RID: 32200 RVA: 0x000BB051 File Offset: 0x000B9251
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009CF RID: 2511
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170055E4 RID: 21988
			// (set) Token: 0x06007DCA RID: 32202 RVA: 0x000BB071 File Offset: 0x000B9271
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170055E5 RID: 21989
			// (set) Token: 0x06007DCB RID: 32203 RVA: 0x000BB08F File Offset: 0x000B928F
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170055E6 RID: 21990
			// (set) Token: 0x06007DCC RID: 32204 RVA: 0x000BB0A2 File Offset: 0x000B92A2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170055E7 RID: 21991
			// (set) Token: 0x06007DCD RID: 32205 RVA: 0x000BB0C0 File Offset: 0x000B92C0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170055E8 RID: 21992
			// (set) Token: 0x06007DCE RID: 32206 RVA: 0x000BB0D3 File Offset: 0x000B92D3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170055E9 RID: 21993
			// (set) Token: 0x06007DCF RID: 32207 RVA: 0x000BB0EB File Offset: 0x000B92EB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055EA RID: 21994
			// (set) Token: 0x06007DD0 RID: 32208 RVA: 0x000BB0FE File Offset: 0x000B92FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055EB RID: 21995
			// (set) Token: 0x06007DD1 RID: 32209 RVA: 0x000BB116 File Offset: 0x000B9316
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055EC RID: 21996
			// (set) Token: 0x06007DD2 RID: 32210 RVA: 0x000BB12E File Offset: 0x000B932E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055ED RID: 21997
			// (set) Token: 0x06007DD3 RID: 32211 RVA: 0x000BB146 File Offset: 0x000B9346
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
