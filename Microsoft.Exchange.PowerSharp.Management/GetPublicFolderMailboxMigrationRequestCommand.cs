using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A99 RID: 2713
	public class GetPublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequest, PublicFolderMailboxMigrationRequest>
	{
		// Token: 0x06008666 RID: 34406 RVA: 0x000C63AF File Offset: 0x000C45AF
		private GetPublicFolderMailboxMigrationRequestCommand() : base("Get-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x06008667 RID: 34407 RVA: 0x000C63BC File Offset: 0x000C45BC
		public GetPublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008668 RID: 34408 RVA: 0x000C63CB File Offset: 0x000C45CB
		public virtual GetPublicFolderMailboxMigrationRequestCommand SetParameters(GetPublicFolderMailboxMigrationRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008669 RID: 34409 RVA: 0x000C63D5 File Offset: 0x000C45D5
		public virtual GetPublicFolderMailboxMigrationRequestCommand SetParameters(GetPublicFolderMailboxMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600866A RID: 34410 RVA: 0x000C63DF File Offset: 0x000C45DF
		public virtual GetPublicFolderMailboxMigrationRequestCommand SetParameters(GetPublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A9A RID: 2714
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005CED RID: 23789
			// (set) Token: 0x0600866B RID: 34411 RVA: 0x000C63E9 File Offset: 0x000C45E9
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005CEE RID: 23790
			// (set) Token: 0x0600866C RID: 34412 RVA: 0x000C6401 File Offset: 0x000C4601
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005CEF RID: 23791
			// (set) Token: 0x0600866D RID: 34413 RVA: 0x000C6414 File Offset: 0x000C4614
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005CF0 RID: 23792
			// (set) Token: 0x0600866E RID: 34414 RVA: 0x000C6427 File Offset: 0x000C4627
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005CF1 RID: 23793
			// (set) Token: 0x0600866F RID: 34415 RVA: 0x000C643F File Offset: 0x000C463F
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005CF2 RID: 23794
			// (set) Token: 0x06008670 RID: 34416 RVA: 0x000C6457 File Offset: 0x000C4657
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005CF3 RID: 23795
			// (set) Token: 0x06008671 RID: 34417 RVA: 0x000C646A File Offset: 0x000C466A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005CF4 RID: 23796
			// (set) Token: 0x06008672 RID: 34418 RVA: 0x000C6488 File Offset: 0x000C4688
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005CF5 RID: 23797
			// (set) Token: 0x06008673 RID: 34419 RVA: 0x000C649B File Offset: 0x000C469B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CF6 RID: 23798
			// (set) Token: 0x06008674 RID: 34420 RVA: 0x000C64AE File Offset: 0x000C46AE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005CF7 RID: 23799
			// (set) Token: 0x06008675 RID: 34421 RVA: 0x000C64C6 File Offset: 0x000C46C6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005CF8 RID: 23800
			// (set) Token: 0x06008676 RID: 34422 RVA: 0x000C64DE File Offset: 0x000C46DE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005CF9 RID: 23801
			// (set) Token: 0x06008677 RID: 34423 RVA: 0x000C64F6 File Offset: 0x000C46F6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005CFA RID: 23802
			// (set) Token: 0x06008678 RID: 34424 RVA: 0x000C650E File Offset: 0x000C470E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A9B RID: 2715
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005CFB RID: 23803
			// (set) Token: 0x0600867A RID: 34426 RVA: 0x000C652E File Offset: 0x000C472E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005CFC RID: 23804
			// (set) Token: 0x0600867B RID: 34427 RVA: 0x000C654C File Offset: 0x000C474C
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005CFD RID: 23805
			// (set) Token: 0x0600867C RID: 34428 RVA: 0x000C655F File Offset: 0x000C475F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005CFE RID: 23806
			// (set) Token: 0x0600867D RID: 34429 RVA: 0x000C657D File Offset: 0x000C477D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CFF RID: 23807
			// (set) Token: 0x0600867E RID: 34430 RVA: 0x000C6590 File Offset: 0x000C4790
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005D00 RID: 23808
			// (set) Token: 0x0600867F RID: 34431 RVA: 0x000C65A8 File Offset: 0x000C47A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D01 RID: 23809
			// (set) Token: 0x06008680 RID: 34432 RVA: 0x000C65C0 File Offset: 0x000C47C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D02 RID: 23810
			// (set) Token: 0x06008681 RID: 34433 RVA: 0x000C65D8 File Offset: 0x000C47D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D03 RID: 23811
			// (set) Token: 0x06008682 RID: 34434 RVA: 0x000C65F0 File Offset: 0x000C47F0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A9C RID: 2716
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D04 RID: 23812
			// (set) Token: 0x06008684 RID: 34436 RVA: 0x000C6610 File Offset: 0x000C4810
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D05 RID: 23813
			// (set) Token: 0x06008685 RID: 34437 RVA: 0x000C6623 File Offset: 0x000C4823
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005D06 RID: 23814
			// (set) Token: 0x06008686 RID: 34438 RVA: 0x000C663B File Offset: 0x000C483B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D07 RID: 23815
			// (set) Token: 0x06008687 RID: 34439 RVA: 0x000C6653 File Offset: 0x000C4853
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D08 RID: 23816
			// (set) Token: 0x06008688 RID: 34440 RVA: 0x000C666B File Offset: 0x000C486B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D09 RID: 23817
			// (set) Token: 0x06008689 RID: 34441 RVA: 0x000C6683 File Offset: 0x000C4883
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
