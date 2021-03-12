using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A63 RID: 2659
	public class GetPublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequest, PublicFolderMoveRequest>
	{
		// Token: 0x06008431 RID: 33841 RVA: 0x000C35F2 File Offset: 0x000C17F2
		private GetPublicFolderMoveRequestCommand() : base("Get-PublicFolderMoveRequest")
		{
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x000C35FF File Offset: 0x000C17FF
		public GetPublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x000C360E File Offset: 0x000C180E
		public virtual GetPublicFolderMoveRequestCommand SetParameters(GetPublicFolderMoveRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x000C3618 File Offset: 0x000C1818
		public virtual GetPublicFolderMoveRequestCommand SetParameters(GetPublicFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008435 RID: 33845 RVA: 0x000C3622 File Offset: 0x000C1822
		public virtual GetPublicFolderMoveRequestCommand SetParameters(GetPublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A64 RID: 2660
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005B24 RID: 23332
			// (set) Token: 0x06008436 RID: 33846 RVA: 0x000C362C File Offset: 0x000C182C
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005B25 RID: 23333
			// (set) Token: 0x06008437 RID: 33847 RVA: 0x000C3644 File Offset: 0x000C1844
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005B26 RID: 23334
			// (set) Token: 0x06008438 RID: 33848 RVA: 0x000C3657 File Offset: 0x000C1857
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005B27 RID: 23335
			// (set) Token: 0x06008439 RID: 33849 RVA: 0x000C366A File Offset: 0x000C186A
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005B28 RID: 23336
			// (set) Token: 0x0600843A RID: 33850 RVA: 0x000C3682 File Offset: 0x000C1882
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005B29 RID: 23337
			// (set) Token: 0x0600843B RID: 33851 RVA: 0x000C369A File Offset: 0x000C189A
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005B2A RID: 23338
			// (set) Token: 0x0600843C RID: 33852 RVA: 0x000C36AD File Offset: 0x000C18AD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005B2B RID: 23339
			// (set) Token: 0x0600843D RID: 33853 RVA: 0x000C36CB File Offset: 0x000C18CB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005B2C RID: 23340
			// (set) Token: 0x0600843E RID: 33854 RVA: 0x000C36DE File Offset: 0x000C18DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B2D RID: 23341
			// (set) Token: 0x0600843F RID: 33855 RVA: 0x000C36F1 File Offset: 0x000C18F1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005B2E RID: 23342
			// (set) Token: 0x06008440 RID: 33856 RVA: 0x000C3709 File Offset: 0x000C1909
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B2F RID: 23343
			// (set) Token: 0x06008441 RID: 33857 RVA: 0x000C3721 File Offset: 0x000C1921
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B30 RID: 23344
			// (set) Token: 0x06008442 RID: 33858 RVA: 0x000C3739 File Offset: 0x000C1939
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B31 RID: 23345
			// (set) Token: 0x06008443 RID: 33859 RVA: 0x000C3751 File Offset: 0x000C1951
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A65 RID: 2661
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B32 RID: 23346
			// (set) Token: 0x06008445 RID: 33861 RVA: 0x000C3771 File Offset: 0x000C1971
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005B33 RID: 23347
			// (set) Token: 0x06008446 RID: 33862 RVA: 0x000C378F File Offset: 0x000C198F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005B34 RID: 23348
			// (set) Token: 0x06008447 RID: 33863 RVA: 0x000C37A2 File Offset: 0x000C19A2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B35 RID: 23349
			// (set) Token: 0x06008448 RID: 33864 RVA: 0x000C37C0 File Offset: 0x000C19C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B36 RID: 23350
			// (set) Token: 0x06008449 RID: 33865 RVA: 0x000C37D3 File Offset: 0x000C19D3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005B37 RID: 23351
			// (set) Token: 0x0600844A RID: 33866 RVA: 0x000C37EB File Offset: 0x000C19EB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B38 RID: 23352
			// (set) Token: 0x0600844B RID: 33867 RVA: 0x000C3803 File Offset: 0x000C1A03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B39 RID: 23353
			// (set) Token: 0x0600844C RID: 33868 RVA: 0x000C381B File Offset: 0x000C1A1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B3A RID: 23354
			// (set) Token: 0x0600844D RID: 33869 RVA: 0x000C3833 File Offset: 0x000C1A33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A66 RID: 2662
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B3B RID: 23355
			// (set) Token: 0x0600844F RID: 33871 RVA: 0x000C3853 File Offset: 0x000C1A53
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B3C RID: 23356
			// (set) Token: 0x06008450 RID: 33872 RVA: 0x000C3866 File Offset: 0x000C1A66
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005B3D RID: 23357
			// (set) Token: 0x06008451 RID: 33873 RVA: 0x000C387E File Offset: 0x000C1A7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B3E RID: 23358
			// (set) Token: 0x06008452 RID: 33874 RVA: 0x000C3896 File Offset: 0x000C1A96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B3F RID: 23359
			// (set) Token: 0x06008453 RID: 33875 RVA: 0x000C38AE File Offset: 0x000C1AAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B40 RID: 23360
			// (set) Token: 0x06008454 RID: 33876 RVA: 0x000C38C6 File Offset: 0x000C1AC6
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
