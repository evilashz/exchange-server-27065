using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A7A RID: 2682
	public class GetPublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequest, PublicFolderMigrationRequest>
	{
		// Token: 0x060084F8 RID: 34040 RVA: 0x000C45D5 File Offset: 0x000C27D5
		private GetPublicFolderMigrationRequestCommand() : base("Get-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x060084F9 RID: 34041 RVA: 0x000C45E2 File Offset: 0x000C27E2
		public GetPublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060084FA RID: 34042 RVA: 0x000C45F1 File Offset: 0x000C27F1
		public virtual GetPublicFolderMigrationRequestCommand SetParameters(GetPublicFolderMigrationRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x000C45FB File Offset: 0x000C27FB
		public virtual GetPublicFolderMigrationRequestCommand SetParameters(GetPublicFolderMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x000C4605 File Offset: 0x000C2805
		public virtual GetPublicFolderMigrationRequestCommand SetParameters(GetPublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A7B RID: 2683
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005BBD RID: 23485
			// (set) Token: 0x060084FD RID: 34045 RVA: 0x000C460F File Offset: 0x000C280F
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005BBE RID: 23486
			// (set) Token: 0x060084FE RID: 34046 RVA: 0x000C4627 File Offset: 0x000C2827
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005BBF RID: 23487
			// (set) Token: 0x060084FF RID: 34047 RVA: 0x000C463A File Offset: 0x000C283A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005BC0 RID: 23488
			// (set) Token: 0x06008500 RID: 34048 RVA: 0x000C464D File Offset: 0x000C284D
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005BC1 RID: 23489
			// (set) Token: 0x06008501 RID: 34049 RVA: 0x000C4665 File Offset: 0x000C2865
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005BC2 RID: 23490
			// (set) Token: 0x06008502 RID: 34050 RVA: 0x000C467D File Offset: 0x000C287D
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005BC3 RID: 23491
			// (set) Token: 0x06008503 RID: 34051 RVA: 0x000C4690 File Offset: 0x000C2890
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005BC4 RID: 23492
			// (set) Token: 0x06008504 RID: 34052 RVA: 0x000C46AE File Offset: 0x000C28AE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005BC5 RID: 23493
			// (set) Token: 0x06008505 RID: 34053 RVA: 0x000C46C1 File Offset: 0x000C28C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BC6 RID: 23494
			// (set) Token: 0x06008506 RID: 34054 RVA: 0x000C46D4 File Offset: 0x000C28D4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005BC7 RID: 23495
			// (set) Token: 0x06008507 RID: 34055 RVA: 0x000C46EC File Offset: 0x000C28EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BC8 RID: 23496
			// (set) Token: 0x06008508 RID: 34056 RVA: 0x000C4704 File Offset: 0x000C2904
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BC9 RID: 23497
			// (set) Token: 0x06008509 RID: 34057 RVA: 0x000C471C File Offset: 0x000C291C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BCA RID: 23498
			// (set) Token: 0x0600850A RID: 34058 RVA: 0x000C4734 File Offset: 0x000C2934
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A7C RID: 2684
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005BCB RID: 23499
			// (set) Token: 0x0600850C RID: 34060 RVA: 0x000C4754 File Offset: 0x000C2954
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005BCC RID: 23500
			// (set) Token: 0x0600850D RID: 34061 RVA: 0x000C4772 File Offset: 0x000C2972
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005BCD RID: 23501
			// (set) Token: 0x0600850E RID: 34062 RVA: 0x000C4785 File Offset: 0x000C2985
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005BCE RID: 23502
			// (set) Token: 0x0600850F RID: 34063 RVA: 0x000C47A3 File Offset: 0x000C29A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BCF RID: 23503
			// (set) Token: 0x06008510 RID: 34064 RVA: 0x000C47B6 File Offset: 0x000C29B6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005BD0 RID: 23504
			// (set) Token: 0x06008511 RID: 34065 RVA: 0x000C47CE File Offset: 0x000C29CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BD1 RID: 23505
			// (set) Token: 0x06008512 RID: 34066 RVA: 0x000C47E6 File Offset: 0x000C29E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BD2 RID: 23506
			// (set) Token: 0x06008513 RID: 34067 RVA: 0x000C47FE File Offset: 0x000C29FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BD3 RID: 23507
			// (set) Token: 0x06008514 RID: 34068 RVA: 0x000C4816 File Offset: 0x000C2A16
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A7D RID: 2685
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005BD4 RID: 23508
			// (set) Token: 0x06008516 RID: 34070 RVA: 0x000C4836 File Offset: 0x000C2A36
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BD5 RID: 23509
			// (set) Token: 0x06008517 RID: 34071 RVA: 0x000C4849 File Offset: 0x000C2A49
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005BD6 RID: 23510
			// (set) Token: 0x06008518 RID: 34072 RVA: 0x000C4861 File Offset: 0x000C2A61
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BD7 RID: 23511
			// (set) Token: 0x06008519 RID: 34073 RVA: 0x000C4879 File Offset: 0x000C2A79
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BD8 RID: 23512
			// (set) Token: 0x0600851A RID: 34074 RVA: 0x000C4891 File Offset: 0x000C2A91
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BD9 RID: 23513
			// (set) Token: 0x0600851B RID: 34075 RVA: 0x000C48A9 File Offset: 0x000C2AA9
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
