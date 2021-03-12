using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C83 RID: 3203
	public class GetMailboxCommand : SyntheticCommandWithPipelineInput<Mailbox, Mailbox>
	{
		// Token: 0x06009DC6 RID: 40390 RVA: 0x000E4C65 File Offset: 0x000E2E65
		private GetMailboxCommand() : base("Get-Mailbox")
		{
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x000E4C72 File Offset: 0x000E2E72
		public GetMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009DC8 RID: 40392 RVA: 0x000E4C81 File Offset: 0x000E2E81
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009DC9 RID: 40393 RVA: 0x000E4C8B File Offset: 0x000E2E8B
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.MailboxPlanSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009DCA RID: 40394 RVA: 0x000E4C95 File Offset: 0x000E2E95
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.ServerSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009DCB RID: 40395 RVA: 0x000E4C9F File Offset: 0x000E2E9F
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.DatabaseSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009DCC RID: 40396 RVA: 0x000E4CA9 File Offset: 0x000E2EA9
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009DCD RID: 40397 RVA: 0x000E4CB3 File Offset: 0x000E2EB3
		public virtual GetMailboxCommand SetParameters(GetMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C84 RID: 3204
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007079 RID: 28793
			// (set) Token: 0x06009DCE RID: 40398 RVA: 0x000E4CBD File Offset: 0x000E2EBD
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700707A RID: 28794
			// (set) Token: 0x06009DCF RID: 40399 RVA: 0x000E4CD5 File Offset: 0x000E2ED5
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700707B RID: 28795
			// (set) Token: 0x06009DD0 RID: 40400 RVA: 0x000E4CED File Offset: 0x000E2EED
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700707C RID: 28796
			// (set) Token: 0x06009DD1 RID: 40401 RVA: 0x000E4D05 File Offset: 0x000E2F05
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x1700707D RID: 28797
			// (set) Token: 0x06009DD2 RID: 40402 RVA: 0x000E4D1D File Offset: 0x000E2F1D
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700707E RID: 28798
			// (set) Token: 0x06009DD3 RID: 40403 RVA: 0x000E4D35 File Offset: 0x000E2F35
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700707F RID: 28799
			// (set) Token: 0x06009DD4 RID: 40404 RVA: 0x000E4D4D File Offset: 0x000E2F4D
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17007080 RID: 28800
			// (set) Token: 0x06009DD5 RID: 40405 RVA: 0x000E4D65 File Offset: 0x000E2F65
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17007081 RID: 28801
			// (set) Token: 0x06009DD6 RID: 40406 RVA: 0x000E4D7D File Offset: 0x000E2F7D
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x17007082 RID: 28802
			// (set) Token: 0x06009DD7 RID: 40407 RVA: 0x000E4D95 File Offset: 0x000E2F95
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x17007083 RID: 28803
			// (set) Token: 0x06009DD8 RID: 40408 RVA: 0x000E4DAD File Offset: 0x000E2FAD
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17007084 RID: 28804
			// (set) Token: 0x06009DD9 RID: 40409 RVA: 0x000E4DC5 File Offset: 0x000E2FC5
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17007085 RID: 28805
			// (set) Token: 0x06009DDA RID: 40410 RVA: 0x000E4DDD File Offset: 0x000E2FDD
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007086 RID: 28806
			// (set) Token: 0x06009DDB RID: 40411 RVA: 0x000E4DF5 File Offset: 0x000E2FF5
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007087 RID: 28807
			// (set) Token: 0x06009DDC RID: 40412 RVA: 0x000E4E08 File Offset: 0x000E3008
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007088 RID: 28808
			// (set) Token: 0x06009DDD RID: 40413 RVA: 0x000E4E26 File Offset: 0x000E3026
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007089 RID: 28809
			// (set) Token: 0x06009DDE RID: 40414 RVA: 0x000E4E39 File Offset: 0x000E3039
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700708A RID: 28810
			// (set) Token: 0x06009DDF RID: 40415 RVA: 0x000E4E4C File Offset: 0x000E304C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700708B RID: 28811
			// (set) Token: 0x06009DE0 RID: 40416 RVA: 0x000E4E6A File Offset: 0x000E306A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700708C RID: 28812
			// (set) Token: 0x06009DE1 RID: 40417 RVA: 0x000E4E82 File Offset: 0x000E3082
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700708D RID: 28813
			// (set) Token: 0x06009DE2 RID: 40418 RVA: 0x000E4E95 File Offset: 0x000E3095
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700708E RID: 28814
			// (set) Token: 0x06009DE3 RID: 40419 RVA: 0x000E4EAD File Offset: 0x000E30AD
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700708F RID: 28815
			// (set) Token: 0x06009DE4 RID: 40420 RVA: 0x000E4EC5 File Offset: 0x000E30C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007090 RID: 28816
			// (set) Token: 0x06009DE5 RID: 40421 RVA: 0x000E4ED8 File Offset: 0x000E30D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007091 RID: 28817
			// (set) Token: 0x06009DE6 RID: 40422 RVA: 0x000E4EF0 File Offset: 0x000E30F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007092 RID: 28818
			// (set) Token: 0x06009DE7 RID: 40423 RVA: 0x000E4F08 File Offset: 0x000E3108
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007093 RID: 28819
			// (set) Token: 0x06009DE8 RID: 40424 RVA: 0x000E4F20 File Offset: 0x000E3120
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C85 RID: 3205
		public class MailboxPlanSetParameters : ParametersBase
		{
			// Token: 0x17007094 RID: 28820
			// (set) Token: 0x06009DEA RID: 40426 RVA: 0x000E4F40 File Offset: 0x000E3140
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007095 RID: 28821
			// (set) Token: 0x06009DEB RID: 40427 RVA: 0x000E4F5E File Offset: 0x000E315E
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17007096 RID: 28822
			// (set) Token: 0x06009DEC RID: 40428 RVA: 0x000E4F76 File Offset: 0x000E3176
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17007097 RID: 28823
			// (set) Token: 0x06009DED RID: 40429 RVA: 0x000E4F8E File Offset: 0x000E318E
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007098 RID: 28824
			// (set) Token: 0x06009DEE RID: 40430 RVA: 0x000E4FA6 File Offset: 0x000E31A6
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007099 RID: 28825
			// (set) Token: 0x06009DEF RID: 40431 RVA: 0x000E4FBE File Offset: 0x000E31BE
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700709A RID: 28826
			// (set) Token: 0x06009DF0 RID: 40432 RVA: 0x000E4FD6 File Offset: 0x000E31D6
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700709B RID: 28827
			// (set) Token: 0x06009DF1 RID: 40433 RVA: 0x000E4FEE File Offset: 0x000E31EE
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x1700709C RID: 28828
			// (set) Token: 0x06009DF2 RID: 40434 RVA: 0x000E5006 File Offset: 0x000E3206
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700709D RID: 28829
			// (set) Token: 0x06009DF3 RID: 40435 RVA: 0x000E501E File Offset: 0x000E321E
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700709E RID: 28830
			// (set) Token: 0x06009DF4 RID: 40436 RVA: 0x000E5036 File Offset: 0x000E3236
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x1700709F RID: 28831
			// (set) Token: 0x06009DF5 RID: 40437 RVA: 0x000E504E File Offset: 0x000E324E
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x170070A0 RID: 28832
			// (set) Token: 0x06009DF6 RID: 40438 RVA: 0x000E5066 File Offset: 0x000E3266
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170070A1 RID: 28833
			// (set) Token: 0x06009DF7 RID: 40439 RVA: 0x000E507E File Offset: 0x000E327E
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170070A2 RID: 28834
			// (set) Token: 0x06009DF8 RID: 40440 RVA: 0x000E5096 File Offset: 0x000E3296
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170070A3 RID: 28835
			// (set) Token: 0x06009DF9 RID: 40441 RVA: 0x000E50A9 File Offset: 0x000E32A9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170070A4 RID: 28836
			// (set) Token: 0x06009DFA RID: 40442 RVA: 0x000E50C7 File Offset: 0x000E32C7
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170070A5 RID: 28837
			// (set) Token: 0x06009DFB RID: 40443 RVA: 0x000E50DA File Offset: 0x000E32DA
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170070A6 RID: 28838
			// (set) Token: 0x06009DFC RID: 40444 RVA: 0x000E50ED File Offset: 0x000E32ED
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170070A7 RID: 28839
			// (set) Token: 0x06009DFD RID: 40445 RVA: 0x000E510B File Offset: 0x000E330B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170070A8 RID: 28840
			// (set) Token: 0x06009DFE RID: 40446 RVA: 0x000E5123 File Offset: 0x000E3323
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170070A9 RID: 28841
			// (set) Token: 0x06009DFF RID: 40447 RVA: 0x000E5136 File Offset: 0x000E3336
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170070AA RID: 28842
			// (set) Token: 0x06009E00 RID: 40448 RVA: 0x000E514E File Offset: 0x000E334E
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170070AB RID: 28843
			// (set) Token: 0x06009E01 RID: 40449 RVA: 0x000E5166 File Offset: 0x000E3366
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170070AC RID: 28844
			// (set) Token: 0x06009E02 RID: 40450 RVA: 0x000E5179 File Offset: 0x000E3379
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170070AD RID: 28845
			// (set) Token: 0x06009E03 RID: 40451 RVA: 0x000E5191 File Offset: 0x000E3391
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170070AE RID: 28846
			// (set) Token: 0x06009E04 RID: 40452 RVA: 0x000E51A9 File Offset: 0x000E33A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170070AF RID: 28847
			// (set) Token: 0x06009E05 RID: 40453 RVA: 0x000E51C1 File Offset: 0x000E33C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C86 RID: 3206
		public class ServerSetParameters : ParametersBase
		{
			// Token: 0x170070B0 RID: 28848
			// (set) Token: 0x06009E07 RID: 40455 RVA: 0x000E51E1 File Offset: 0x000E33E1
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170070B1 RID: 28849
			// (set) Token: 0x06009E08 RID: 40456 RVA: 0x000E51F4 File Offset: 0x000E33F4
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170070B2 RID: 28850
			// (set) Token: 0x06009E09 RID: 40457 RVA: 0x000E520C File Offset: 0x000E340C
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170070B3 RID: 28851
			// (set) Token: 0x06009E0A RID: 40458 RVA: 0x000E5224 File Offset: 0x000E3424
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170070B4 RID: 28852
			// (set) Token: 0x06009E0B RID: 40459 RVA: 0x000E523C File Offset: 0x000E343C
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170070B5 RID: 28853
			// (set) Token: 0x06009E0C RID: 40460 RVA: 0x000E5254 File Offset: 0x000E3454
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170070B6 RID: 28854
			// (set) Token: 0x06009E0D RID: 40461 RVA: 0x000E526C File Offset: 0x000E346C
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170070B7 RID: 28855
			// (set) Token: 0x06009E0E RID: 40462 RVA: 0x000E5284 File Offset: 0x000E3484
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170070B8 RID: 28856
			// (set) Token: 0x06009E0F RID: 40463 RVA: 0x000E529C File Offset: 0x000E349C
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070B9 RID: 28857
			// (set) Token: 0x06009E10 RID: 40464 RVA: 0x000E52B4 File Offset: 0x000E34B4
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070BA RID: 28858
			// (set) Token: 0x06009E11 RID: 40465 RVA: 0x000E52CC File Offset: 0x000E34CC
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x170070BB RID: 28859
			// (set) Token: 0x06009E12 RID: 40466 RVA: 0x000E52E4 File Offset: 0x000E34E4
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x170070BC RID: 28860
			// (set) Token: 0x06009E13 RID: 40467 RVA: 0x000E52FC File Offset: 0x000E34FC
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170070BD RID: 28861
			// (set) Token: 0x06009E14 RID: 40468 RVA: 0x000E5314 File Offset: 0x000E3514
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170070BE RID: 28862
			// (set) Token: 0x06009E15 RID: 40469 RVA: 0x000E532C File Offset: 0x000E352C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170070BF RID: 28863
			// (set) Token: 0x06009E16 RID: 40470 RVA: 0x000E533F File Offset: 0x000E353F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170070C0 RID: 28864
			// (set) Token: 0x06009E17 RID: 40471 RVA: 0x000E535D File Offset: 0x000E355D
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170070C1 RID: 28865
			// (set) Token: 0x06009E18 RID: 40472 RVA: 0x000E5370 File Offset: 0x000E3570
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170070C2 RID: 28866
			// (set) Token: 0x06009E19 RID: 40473 RVA: 0x000E5383 File Offset: 0x000E3583
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170070C3 RID: 28867
			// (set) Token: 0x06009E1A RID: 40474 RVA: 0x000E53A1 File Offset: 0x000E35A1
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170070C4 RID: 28868
			// (set) Token: 0x06009E1B RID: 40475 RVA: 0x000E53B9 File Offset: 0x000E35B9
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170070C5 RID: 28869
			// (set) Token: 0x06009E1C RID: 40476 RVA: 0x000E53CC File Offset: 0x000E35CC
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170070C6 RID: 28870
			// (set) Token: 0x06009E1D RID: 40477 RVA: 0x000E53E4 File Offset: 0x000E35E4
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170070C7 RID: 28871
			// (set) Token: 0x06009E1E RID: 40478 RVA: 0x000E53FC File Offset: 0x000E35FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170070C8 RID: 28872
			// (set) Token: 0x06009E1F RID: 40479 RVA: 0x000E540F File Offset: 0x000E360F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170070C9 RID: 28873
			// (set) Token: 0x06009E20 RID: 40480 RVA: 0x000E5427 File Offset: 0x000E3627
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170070CA RID: 28874
			// (set) Token: 0x06009E21 RID: 40481 RVA: 0x000E543F File Offset: 0x000E363F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170070CB RID: 28875
			// (set) Token: 0x06009E22 RID: 40482 RVA: 0x000E5457 File Offset: 0x000E3657
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C87 RID: 3207
		public class DatabaseSetParameters : ParametersBase
		{
			// Token: 0x170070CC RID: 28876
			// (set) Token: 0x06009E24 RID: 40484 RVA: 0x000E5477 File Offset: 0x000E3677
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170070CD RID: 28877
			// (set) Token: 0x06009E25 RID: 40485 RVA: 0x000E548A File Offset: 0x000E368A
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170070CE RID: 28878
			// (set) Token: 0x06009E26 RID: 40486 RVA: 0x000E54A2 File Offset: 0x000E36A2
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170070CF RID: 28879
			// (set) Token: 0x06009E27 RID: 40487 RVA: 0x000E54BA File Offset: 0x000E36BA
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170070D0 RID: 28880
			// (set) Token: 0x06009E28 RID: 40488 RVA: 0x000E54D2 File Offset: 0x000E36D2
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170070D1 RID: 28881
			// (set) Token: 0x06009E29 RID: 40489 RVA: 0x000E54EA File Offset: 0x000E36EA
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170070D2 RID: 28882
			// (set) Token: 0x06009E2A RID: 40490 RVA: 0x000E5502 File Offset: 0x000E3702
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170070D3 RID: 28883
			// (set) Token: 0x06009E2B RID: 40491 RVA: 0x000E551A File Offset: 0x000E371A
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170070D4 RID: 28884
			// (set) Token: 0x06009E2C RID: 40492 RVA: 0x000E5532 File Offset: 0x000E3732
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070D5 RID: 28885
			// (set) Token: 0x06009E2D RID: 40493 RVA: 0x000E554A File Offset: 0x000E374A
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070D6 RID: 28886
			// (set) Token: 0x06009E2E RID: 40494 RVA: 0x000E5562 File Offset: 0x000E3762
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x170070D7 RID: 28887
			// (set) Token: 0x06009E2F RID: 40495 RVA: 0x000E557A File Offset: 0x000E377A
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x170070D8 RID: 28888
			// (set) Token: 0x06009E30 RID: 40496 RVA: 0x000E5592 File Offset: 0x000E3792
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170070D9 RID: 28889
			// (set) Token: 0x06009E31 RID: 40497 RVA: 0x000E55AA File Offset: 0x000E37AA
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170070DA RID: 28890
			// (set) Token: 0x06009E32 RID: 40498 RVA: 0x000E55C2 File Offset: 0x000E37C2
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170070DB RID: 28891
			// (set) Token: 0x06009E33 RID: 40499 RVA: 0x000E55D5 File Offset: 0x000E37D5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170070DC RID: 28892
			// (set) Token: 0x06009E34 RID: 40500 RVA: 0x000E55F3 File Offset: 0x000E37F3
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170070DD RID: 28893
			// (set) Token: 0x06009E35 RID: 40501 RVA: 0x000E5606 File Offset: 0x000E3806
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170070DE RID: 28894
			// (set) Token: 0x06009E36 RID: 40502 RVA: 0x000E5619 File Offset: 0x000E3819
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170070DF RID: 28895
			// (set) Token: 0x06009E37 RID: 40503 RVA: 0x000E5637 File Offset: 0x000E3837
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170070E0 RID: 28896
			// (set) Token: 0x06009E38 RID: 40504 RVA: 0x000E564F File Offset: 0x000E384F
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170070E1 RID: 28897
			// (set) Token: 0x06009E39 RID: 40505 RVA: 0x000E5662 File Offset: 0x000E3862
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170070E2 RID: 28898
			// (set) Token: 0x06009E3A RID: 40506 RVA: 0x000E567A File Offset: 0x000E387A
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170070E3 RID: 28899
			// (set) Token: 0x06009E3B RID: 40507 RVA: 0x000E5692 File Offset: 0x000E3892
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170070E4 RID: 28900
			// (set) Token: 0x06009E3C RID: 40508 RVA: 0x000E56A5 File Offset: 0x000E38A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170070E5 RID: 28901
			// (set) Token: 0x06009E3D RID: 40509 RVA: 0x000E56BD File Offset: 0x000E38BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170070E6 RID: 28902
			// (set) Token: 0x06009E3E RID: 40510 RVA: 0x000E56D5 File Offset: 0x000E38D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170070E7 RID: 28903
			// (set) Token: 0x06009E3F RID: 40511 RVA: 0x000E56ED File Offset: 0x000E38ED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C88 RID: 3208
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170070E8 RID: 28904
			// (set) Token: 0x06009E41 RID: 40513 RVA: 0x000E570D File Offset: 0x000E390D
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170070E9 RID: 28905
			// (set) Token: 0x06009E42 RID: 40514 RVA: 0x000E5720 File Offset: 0x000E3920
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170070EA RID: 28906
			// (set) Token: 0x06009E43 RID: 40515 RVA: 0x000E5738 File Offset: 0x000E3938
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170070EB RID: 28907
			// (set) Token: 0x06009E44 RID: 40516 RVA: 0x000E5750 File Offset: 0x000E3950
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170070EC RID: 28908
			// (set) Token: 0x06009E45 RID: 40517 RVA: 0x000E5768 File Offset: 0x000E3968
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170070ED RID: 28909
			// (set) Token: 0x06009E46 RID: 40518 RVA: 0x000E5780 File Offset: 0x000E3980
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170070EE RID: 28910
			// (set) Token: 0x06009E47 RID: 40519 RVA: 0x000E5798 File Offset: 0x000E3998
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170070EF RID: 28911
			// (set) Token: 0x06009E48 RID: 40520 RVA: 0x000E57B0 File Offset: 0x000E39B0
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170070F0 RID: 28912
			// (set) Token: 0x06009E49 RID: 40521 RVA: 0x000E57C8 File Offset: 0x000E39C8
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070F1 RID: 28913
			// (set) Token: 0x06009E4A RID: 40522 RVA: 0x000E57E0 File Offset: 0x000E39E0
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x170070F2 RID: 28914
			// (set) Token: 0x06009E4B RID: 40523 RVA: 0x000E57F8 File Offset: 0x000E39F8
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x170070F3 RID: 28915
			// (set) Token: 0x06009E4C RID: 40524 RVA: 0x000E5810 File Offset: 0x000E3A10
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x170070F4 RID: 28916
			// (set) Token: 0x06009E4D RID: 40525 RVA: 0x000E5828 File Offset: 0x000E3A28
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170070F5 RID: 28917
			// (set) Token: 0x06009E4E RID: 40526 RVA: 0x000E5840 File Offset: 0x000E3A40
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170070F6 RID: 28918
			// (set) Token: 0x06009E4F RID: 40527 RVA: 0x000E5858 File Offset: 0x000E3A58
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170070F7 RID: 28919
			// (set) Token: 0x06009E50 RID: 40528 RVA: 0x000E586B File Offset: 0x000E3A6B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170070F8 RID: 28920
			// (set) Token: 0x06009E51 RID: 40529 RVA: 0x000E5889 File Offset: 0x000E3A89
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170070F9 RID: 28921
			// (set) Token: 0x06009E52 RID: 40530 RVA: 0x000E589C File Offset: 0x000E3A9C
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170070FA RID: 28922
			// (set) Token: 0x06009E53 RID: 40531 RVA: 0x000E58AF File Offset: 0x000E3AAF
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170070FB RID: 28923
			// (set) Token: 0x06009E54 RID: 40532 RVA: 0x000E58CD File Offset: 0x000E3ACD
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170070FC RID: 28924
			// (set) Token: 0x06009E55 RID: 40533 RVA: 0x000E58E5 File Offset: 0x000E3AE5
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170070FD RID: 28925
			// (set) Token: 0x06009E56 RID: 40534 RVA: 0x000E58F8 File Offset: 0x000E3AF8
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170070FE RID: 28926
			// (set) Token: 0x06009E57 RID: 40535 RVA: 0x000E5910 File Offset: 0x000E3B10
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170070FF RID: 28927
			// (set) Token: 0x06009E58 RID: 40536 RVA: 0x000E5928 File Offset: 0x000E3B28
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007100 RID: 28928
			// (set) Token: 0x06009E59 RID: 40537 RVA: 0x000E593B File Offset: 0x000E3B3B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007101 RID: 28929
			// (set) Token: 0x06009E5A RID: 40538 RVA: 0x000E5953 File Offset: 0x000E3B53
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007102 RID: 28930
			// (set) Token: 0x06009E5B RID: 40539 RVA: 0x000E596B File Offset: 0x000E3B6B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007103 RID: 28931
			// (set) Token: 0x06009E5C RID: 40540 RVA: 0x000E5983 File Offset: 0x000E3B83
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C89 RID: 3209
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007104 RID: 28932
			// (set) Token: 0x06009E5E RID: 40542 RVA: 0x000E59A3 File Offset: 0x000E3BA3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007105 RID: 28933
			// (set) Token: 0x06009E5F RID: 40543 RVA: 0x000E59C1 File Offset: 0x000E3BC1
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17007106 RID: 28934
			// (set) Token: 0x06009E60 RID: 40544 RVA: 0x000E59D9 File Offset: 0x000E3BD9
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17007107 RID: 28935
			// (set) Token: 0x06009E61 RID: 40545 RVA: 0x000E59F1 File Offset: 0x000E3BF1
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007108 RID: 28936
			// (set) Token: 0x06009E62 RID: 40546 RVA: 0x000E5A09 File Offset: 0x000E3C09
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007109 RID: 28937
			// (set) Token: 0x06009E63 RID: 40547 RVA: 0x000E5A21 File Offset: 0x000E3C21
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x1700710A RID: 28938
			// (set) Token: 0x06009E64 RID: 40548 RVA: 0x000E5A39 File Offset: 0x000E3C39
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700710B RID: 28939
			// (set) Token: 0x06009E65 RID: 40549 RVA: 0x000E5A51 File Offset: 0x000E3C51
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x1700710C RID: 28940
			// (set) Token: 0x06009E66 RID: 40550 RVA: 0x000E5A69 File Offset: 0x000E3C69
			public virtual SwitchParameter SoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700710D RID: 28941
			// (set) Token: 0x06009E67 RID: 40551 RVA: 0x000E5A81 File Offset: 0x000E3C81
			public virtual SwitchParameter IncludeSoftDeletedMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedMailbox"] = value;
				}
			}

			// Token: 0x1700710E RID: 28942
			// (set) Token: 0x06009E68 RID: 40552 RVA: 0x000E5A99 File Offset: 0x000E3C99
			public virtual SwitchParameter InactiveMailboxOnly
			{
				set
				{
					base.PowerSharpParameters["InactiveMailboxOnly"] = value;
				}
			}

			// Token: 0x1700710F RID: 28943
			// (set) Token: 0x06009E69 RID: 40553 RVA: 0x000E5AB1 File Offset: 0x000E3CB1
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17007110 RID: 28944
			// (set) Token: 0x06009E6A RID: 40554 RVA: 0x000E5AC9 File Offset: 0x000E3CC9
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17007111 RID: 28945
			// (set) Token: 0x06009E6B RID: 40555 RVA: 0x000E5AE1 File Offset: 0x000E3CE1
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007112 RID: 28946
			// (set) Token: 0x06009E6C RID: 40556 RVA: 0x000E5AF9 File Offset: 0x000E3CF9
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17007113 RID: 28947
			// (set) Token: 0x06009E6D RID: 40557 RVA: 0x000E5B0C File Offset: 0x000E3D0C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007114 RID: 28948
			// (set) Token: 0x06009E6E RID: 40558 RVA: 0x000E5B2A File Offset: 0x000E3D2A
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17007115 RID: 28949
			// (set) Token: 0x06009E6F RID: 40559 RVA: 0x000E5B3D File Offset: 0x000E3D3D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17007116 RID: 28950
			// (set) Token: 0x06009E70 RID: 40560 RVA: 0x000E5B50 File Offset: 0x000E3D50
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007117 RID: 28951
			// (set) Token: 0x06009E71 RID: 40561 RVA: 0x000E5B6E File Offset: 0x000E3D6E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007118 RID: 28952
			// (set) Token: 0x06009E72 RID: 40562 RVA: 0x000E5B86 File Offset: 0x000E3D86
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17007119 RID: 28953
			// (set) Token: 0x06009E73 RID: 40563 RVA: 0x000E5B99 File Offset: 0x000E3D99
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700711A RID: 28954
			// (set) Token: 0x06009E74 RID: 40564 RVA: 0x000E5BB1 File Offset: 0x000E3DB1
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700711B RID: 28955
			// (set) Token: 0x06009E75 RID: 40565 RVA: 0x000E5BC9 File Offset: 0x000E3DC9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700711C RID: 28956
			// (set) Token: 0x06009E76 RID: 40566 RVA: 0x000E5BDC File Offset: 0x000E3DDC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700711D RID: 28957
			// (set) Token: 0x06009E77 RID: 40567 RVA: 0x000E5BF4 File Offset: 0x000E3DF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700711E RID: 28958
			// (set) Token: 0x06009E78 RID: 40568 RVA: 0x000E5C0C File Offset: 0x000E3E0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700711F RID: 28959
			// (set) Token: 0x06009E79 RID: 40569 RVA: 0x000E5C24 File Offset: 0x000E3E24
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
