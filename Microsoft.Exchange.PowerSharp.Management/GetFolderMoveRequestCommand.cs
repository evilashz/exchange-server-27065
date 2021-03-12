using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009B5 RID: 2485
	public class GetFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<FolderMoveRequest, FolderMoveRequest>
	{
		// Token: 0x06007CD0 RID: 31952 RVA: 0x000B9C36 File Offset: 0x000B7E36
		private GetFolderMoveRequestCommand() : base("Get-FolderMoveRequest")
		{
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x000B9C43 File Offset: 0x000B7E43
		public GetFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x000B9C52 File Offset: 0x000B7E52
		public virtual GetFolderMoveRequestCommand SetParameters(GetFolderMoveRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x000B9C5C File Offset: 0x000B7E5C
		public virtual GetFolderMoveRequestCommand SetParameters(GetFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x000B9C66 File Offset: 0x000B7E66
		public virtual GetFolderMoveRequestCommand SetParameters(GetFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009B6 RID: 2486
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x1700551F RID: 21791
			// (set) Token: 0x06007CD5 RID: 31957 RVA: 0x000B9C70 File Offset: 0x000B7E70
			public virtual string SourceMailbox
			{
				set
				{
					base.PowerSharpParameters["SourceMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005520 RID: 21792
			// (set) Token: 0x06007CD6 RID: 31958 RVA: 0x000B9C8E File Offset: 0x000B7E8E
			public virtual string TargetMailbox
			{
				set
				{
					base.PowerSharpParameters["TargetMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005521 RID: 21793
			// (set) Token: 0x06007CD7 RID: 31959 RVA: 0x000B9CAC File Offset: 0x000B7EAC
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005522 RID: 21794
			// (set) Token: 0x06007CD8 RID: 31960 RVA: 0x000B9CC4 File Offset: 0x000B7EC4
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005523 RID: 21795
			// (set) Token: 0x06007CD9 RID: 31961 RVA: 0x000B9CD7 File Offset: 0x000B7ED7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005524 RID: 21796
			// (set) Token: 0x06007CDA RID: 31962 RVA: 0x000B9CEA File Offset: 0x000B7EEA
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005525 RID: 21797
			// (set) Token: 0x06007CDB RID: 31963 RVA: 0x000B9D02 File Offset: 0x000B7F02
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005526 RID: 21798
			// (set) Token: 0x06007CDC RID: 31964 RVA: 0x000B9D1A File Offset: 0x000B7F1A
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005527 RID: 21799
			// (set) Token: 0x06007CDD RID: 31965 RVA: 0x000B9D2D File Offset: 0x000B7F2D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005528 RID: 21800
			// (set) Token: 0x06007CDE RID: 31966 RVA: 0x000B9D4B File Offset: 0x000B7F4B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005529 RID: 21801
			// (set) Token: 0x06007CDF RID: 31967 RVA: 0x000B9D5E File Offset: 0x000B7F5E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700552A RID: 21802
			// (set) Token: 0x06007CE0 RID: 31968 RVA: 0x000B9D71 File Offset: 0x000B7F71
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700552B RID: 21803
			// (set) Token: 0x06007CE1 RID: 31969 RVA: 0x000B9D89 File Offset: 0x000B7F89
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700552C RID: 21804
			// (set) Token: 0x06007CE2 RID: 31970 RVA: 0x000B9DA1 File Offset: 0x000B7FA1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700552D RID: 21805
			// (set) Token: 0x06007CE3 RID: 31971 RVA: 0x000B9DB9 File Offset: 0x000B7FB9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700552E RID: 21806
			// (set) Token: 0x06007CE4 RID: 31972 RVA: 0x000B9DD1 File Offset: 0x000B7FD1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009B7 RID: 2487
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700552F RID: 21807
			// (set) Token: 0x06007CE6 RID: 31974 RVA: 0x000B9DF1 File Offset: 0x000B7FF1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005530 RID: 21808
			// (set) Token: 0x06007CE7 RID: 31975 RVA: 0x000B9E0F File Offset: 0x000B800F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005531 RID: 21809
			// (set) Token: 0x06007CE8 RID: 31976 RVA: 0x000B9E22 File Offset: 0x000B8022
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005532 RID: 21810
			// (set) Token: 0x06007CE9 RID: 31977 RVA: 0x000B9E40 File Offset: 0x000B8040
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005533 RID: 21811
			// (set) Token: 0x06007CEA RID: 31978 RVA: 0x000B9E53 File Offset: 0x000B8053
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005534 RID: 21812
			// (set) Token: 0x06007CEB RID: 31979 RVA: 0x000B9E6B File Offset: 0x000B806B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005535 RID: 21813
			// (set) Token: 0x06007CEC RID: 31980 RVA: 0x000B9E83 File Offset: 0x000B8083
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005536 RID: 21814
			// (set) Token: 0x06007CED RID: 31981 RVA: 0x000B9E9B File Offset: 0x000B809B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005537 RID: 21815
			// (set) Token: 0x06007CEE RID: 31982 RVA: 0x000B9EB3 File Offset: 0x000B80B3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009B8 RID: 2488
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005538 RID: 21816
			// (set) Token: 0x06007CF0 RID: 31984 RVA: 0x000B9ED3 File Offset: 0x000B80D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005539 RID: 21817
			// (set) Token: 0x06007CF1 RID: 31985 RVA: 0x000B9EE6 File Offset: 0x000B80E6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700553A RID: 21818
			// (set) Token: 0x06007CF2 RID: 31986 RVA: 0x000B9EFE File Offset: 0x000B80FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700553B RID: 21819
			// (set) Token: 0x06007CF3 RID: 31987 RVA: 0x000B9F16 File Offset: 0x000B8116
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700553C RID: 21820
			// (set) Token: 0x06007CF4 RID: 31988 RVA: 0x000B9F2E File Offset: 0x000B812E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700553D RID: 21821
			// (set) Token: 0x06007CF5 RID: 31989 RVA: 0x000B9F46 File Offset: 0x000B8146
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
