using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A9D RID: 2717
	public class SetPublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInputNoOutput<PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x0600868B RID: 34443 RVA: 0x000C66A3 File Offset: 0x000C48A3
		private SetPublicFolderMailboxMigrationRequestCommand() : base("Set-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x0600868C RID: 34444 RVA: 0x000C66B0 File Offset: 0x000C48B0
		public SetPublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600868D RID: 34445 RVA: 0x000C66BF File Offset: 0x000C48BF
		public virtual SetPublicFolderMailboxMigrationRequestCommand SetParameters(SetPublicFolderMailboxMigrationRequestCommand.MailboxMigrationOutlookAnywherePublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600868E RID: 34446 RVA: 0x000C66C9 File Offset: 0x000C48C9
		public virtual SetPublicFolderMailboxMigrationRequestCommand SetParameters(SetPublicFolderMailboxMigrationRequestCommand.MailboxMigrationLocalPublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600868F RID: 34447 RVA: 0x000C66D3 File Offset: 0x000C48D3
		public virtual SetPublicFolderMailboxMigrationRequestCommand SetParameters(SetPublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008690 RID: 34448 RVA: 0x000C66DD File Offset: 0x000C48DD
		public virtual SetPublicFolderMailboxMigrationRequestCommand SetParameters(SetPublicFolderMailboxMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008691 RID: 34449 RVA: 0x000C66E7 File Offset: 0x000C48E7
		public virtual SetPublicFolderMailboxMigrationRequestCommand SetParameters(SetPublicFolderMailboxMigrationRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A9E RID: 2718
		public class MailboxMigrationOutlookAnywherePublicFolderParameters : ParametersBase
		{
			// Token: 0x17005D0A RID: 23818
			// (set) Token: 0x06008692 RID: 34450 RVA: 0x000C66F1 File Offset: 0x000C48F1
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005D0B RID: 23819
			// (set) Token: 0x06008693 RID: 34451 RVA: 0x000C6704 File Offset: 0x000C4904
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005D0C RID: 23820
			// (set) Token: 0x06008694 RID: 34452 RVA: 0x000C6717 File Offset: 0x000C4917
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005D0D RID: 23821
			// (set) Token: 0x06008695 RID: 34453 RVA: 0x000C672A File Offset: 0x000C492A
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005D0E RID: 23822
			// (set) Token: 0x06008696 RID: 34454 RVA: 0x000C6742 File Offset: 0x000C4942
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005D0F RID: 23823
			// (set) Token: 0x06008697 RID: 34455 RVA: 0x000C6755 File Offset: 0x000C4955
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005D10 RID: 23824
			// (set) Token: 0x06008698 RID: 34456 RVA: 0x000C676D File Offset: 0x000C496D
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005D11 RID: 23825
			// (set) Token: 0x06008699 RID: 34457 RVA: 0x000C6785 File Offset: 0x000C4985
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005D12 RID: 23826
			// (set) Token: 0x0600869A RID: 34458 RVA: 0x000C6798 File Offset: 0x000C4998
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005D13 RID: 23827
			// (set) Token: 0x0600869B RID: 34459 RVA: 0x000C67B0 File Offset: 0x000C49B0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D14 RID: 23828
			// (set) Token: 0x0600869C RID: 34460 RVA: 0x000C67CE File Offset: 0x000C49CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D15 RID: 23829
			// (set) Token: 0x0600869D RID: 34461 RVA: 0x000C67E1 File Offset: 0x000C49E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D16 RID: 23830
			// (set) Token: 0x0600869E RID: 34462 RVA: 0x000C67F9 File Offset: 0x000C49F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D17 RID: 23831
			// (set) Token: 0x0600869F RID: 34463 RVA: 0x000C6811 File Offset: 0x000C4A11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D18 RID: 23832
			// (set) Token: 0x060086A0 RID: 34464 RVA: 0x000C6829 File Offset: 0x000C4A29
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D19 RID: 23833
			// (set) Token: 0x060086A1 RID: 34465 RVA: 0x000C6841 File Offset: 0x000C4A41
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A9F RID: 2719
		public class MailboxMigrationLocalPublicFolderParameters : ParametersBase
		{
			// Token: 0x17005D1A RID: 23834
			// (set) Token: 0x060086A3 RID: 34467 RVA: 0x000C6861 File Offset: 0x000C4A61
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x17005D1B RID: 23835
			// (set) Token: 0x060086A4 RID: 34468 RVA: 0x000C6874 File Offset: 0x000C4A74
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005D1C RID: 23836
			// (set) Token: 0x060086A5 RID: 34469 RVA: 0x000C688C File Offset: 0x000C4A8C
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005D1D RID: 23837
			// (set) Token: 0x060086A6 RID: 34470 RVA: 0x000C68A4 File Offset: 0x000C4AA4
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005D1E RID: 23838
			// (set) Token: 0x060086A7 RID: 34471 RVA: 0x000C68B7 File Offset: 0x000C4AB7
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005D1F RID: 23839
			// (set) Token: 0x060086A8 RID: 34472 RVA: 0x000C68CF File Offset: 0x000C4ACF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D20 RID: 23840
			// (set) Token: 0x060086A9 RID: 34473 RVA: 0x000C68ED File Offset: 0x000C4AED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D21 RID: 23841
			// (set) Token: 0x060086AA RID: 34474 RVA: 0x000C6900 File Offset: 0x000C4B00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D22 RID: 23842
			// (set) Token: 0x060086AB RID: 34475 RVA: 0x000C6918 File Offset: 0x000C4B18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D23 RID: 23843
			// (set) Token: 0x060086AC RID: 34476 RVA: 0x000C6930 File Offset: 0x000C4B30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D24 RID: 23844
			// (set) Token: 0x060086AD RID: 34477 RVA: 0x000C6948 File Offset: 0x000C4B48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D25 RID: 23845
			// (set) Token: 0x060086AE RID: 34478 RVA: 0x000C6960 File Offset: 0x000C4B60
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AA0 RID: 2720
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D26 RID: 23846
			// (set) Token: 0x060086B0 RID: 34480 RVA: 0x000C6980 File Offset: 0x000C4B80
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005D27 RID: 23847
			// (set) Token: 0x060086B1 RID: 34481 RVA: 0x000C6998 File Offset: 0x000C4B98
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005D28 RID: 23848
			// (set) Token: 0x060086B2 RID: 34482 RVA: 0x000C69B0 File Offset: 0x000C4BB0
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005D29 RID: 23849
			// (set) Token: 0x060086B3 RID: 34483 RVA: 0x000C69C3 File Offset: 0x000C4BC3
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005D2A RID: 23850
			// (set) Token: 0x060086B4 RID: 34484 RVA: 0x000C69DB File Offset: 0x000C4BDB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D2B RID: 23851
			// (set) Token: 0x060086B5 RID: 34485 RVA: 0x000C69F9 File Offset: 0x000C4BF9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D2C RID: 23852
			// (set) Token: 0x060086B6 RID: 34486 RVA: 0x000C6A0C File Offset: 0x000C4C0C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D2D RID: 23853
			// (set) Token: 0x060086B7 RID: 34487 RVA: 0x000C6A24 File Offset: 0x000C4C24
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D2E RID: 23854
			// (set) Token: 0x060086B8 RID: 34488 RVA: 0x000C6A3C File Offset: 0x000C4C3C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D2F RID: 23855
			// (set) Token: 0x060086B9 RID: 34489 RVA: 0x000C6A54 File Offset: 0x000C4C54
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D30 RID: 23856
			// (set) Token: 0x060086BA RID: 34490 RVA: 0x000C6A6C File Offset: 0x000C4C6C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AA1 RID: 2721
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005D31 RID: 23857
			// (set) Token: 0x060086BC RID: 34492 RVA: 0x000C6A8C File Offset: 0x000C4C8C
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005D32 RID: 23858
			// (set) Token: 0x060086BD RID: 34493 RVA: 0x000C6AA4 File Offset: 0x000C4CA4
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005D33 RID: 23859
			// (set) Token: 0x060086BE RID: 34494 RVA: 0x000C6ABC File Offset: 0x000C4CBC
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005D34 RID: 23860
			// (set) Token: 0x060086BF RID: 34495 RVA: 0x000C6AD4 File Offset: 0x000C4CD4
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005D35 RID: 23861
			// (set) Token: 0x060086C0 RID: 34496 RVA: 0x000C6AEC File Offset: 0x000C4CEC
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005D36 RID: 23862
			// (set) Token: 0x060086C1 RID: 34497 RVA: 0x000C6B04 File Offset: 0x000C4D04
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005D37 RID: 23863
			// (set) Token: 0x060086C2 RID: 34498 RVA: 0x000C6B1C File Offset: 0x000C4D1C
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005D38 RID: 23864
			// (set) Token: 0x060086C3 RID: 34499 RVA: 0x000C6B2F File Offset: 0x000C4D2F
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005D39 RID: 23865
			// (set) Token: 0x060086C4 RID: 34500 RVA: 0x000C6B47 File Offset: 0x000C4D47
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D3A RID: 23866
			// (set) Token: 0x060086C5 RID: 34501 RVA: 0x000C6B65 File Offset: 0x000C4D65
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D3B RID: 23867
			// (set) Token: 0x060086C6 RID: 34502 RVA: 0x000C6B78 File Offset: 0x000C4D78
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D3C RID: 23868
			// (set) Token: 0x060086C7 RID: 34503 RVA: 0x000C6B90 File Offset: 0x000C4D90
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D3D RID: 23869
			// (set) Token: 0x060086C8 RID: 34504 RVA: 0x000C6BA8 File Offset: 0x000C4DA8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D3E RID: 23870
			// (set) Token: 0x060086C9 RID: 34505 RVA: 0x000C6BC0 File Offset: 0x000C4DC0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D3F RID: 23871
			// (set) Token: 0x060086CA RID: 34506 RVA: 0x000C6BD8 File Offset: 0x000C4DD8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AA2 RID: 2722
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x17005D40 RID: 23872
			// (set) Token: 0x060086CC RID: 34508 RVA: 0x000C6BF8 File Offset: 0x000C4DF8
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x17005D41 RID: 23873
			// (set) Token: 0x060086CD RID: 34509 RVA: 0x000C6C10 File Offset: 0x000C4E10
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005D42 RID: 23874
			// (set) Token: 0x060086CE RID: 34510 RVA: 0x000C6C28 File Offset: 0x000C4E28
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005D43 RID: 23875
			// (set) Token: 0x060086CF RID: 34511 RVA: 0x000C6C40 File Offset: 0x000C4E40
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005D44 RID: 23876
			// (set) Token: 0x060086D0 RID: 34512 RVA: 0x000C6C53 File Offset: 0x000C4E53
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005D45 RID: 23877
			// (set) Token: 0x060086D1 RID: 34513 RVA: 0x000C6C6B File Offset: 0x000C4E6B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D46 RID: 23878
			// (set) Token: 0x060086D2 RID: 34514 RVA: 0x000C6C89 File Offset: 0x000C4E89
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D47 RID: 23879
			// (set) Token: 0x060086D3 RID: 34515 RVA: 0x000C6C9C File Offset: 0x000C4E9C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D48 RID: 23880
			// (set) Token: 0x060086D4 RID: 34516 RVA: 0x000C6CB4 File Offset: 0x000C4EB4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D49 RID: 23881
			// (set) Token: 0x060086D5 RID: 34517 RVA: 0x000C6CCC File Offset: 0x000C4ECC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D4A RID: 23882
			// (set) Token: 0x060086D6 RID: 34518 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D4B RID: 23883
			// (set) Token: 0x060086D7 RID: 34519 RVA: 0x000C6CFC File Offset: 0x000C4EFC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
