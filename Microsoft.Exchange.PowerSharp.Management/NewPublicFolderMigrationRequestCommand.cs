using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A82 RID: 2690
	public class NewPublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequest, PublicFolderMigrationRequest>
	{
		// Token: 0x06008540 RID: 34112 RVA: 0x000C4B86 File Offset: 0x000C2D86
		private NewPublicFolderMigrationRequestCommand() : base("New-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x000C4B93 File Offset: 0x000C2D93
		public NewPublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x000C4BA2 File Offset: 0x000C2DA2
		public virtual NewPublicFolderMigrationRequestCommand SetParameters(NewPublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008543 RID: 34115 RVA: 0x000C4BAC File Offset: 0x000C2DAC
		public virtual NewPublicFolderMigrationRequestCommand SetParameters(NewPublicFolderMigrationRequestCommand.MigrationLocalPublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x000C4BB6 File Offset: 0x000C2DB6
		public virtual NewPublicFolderMigrationRequestCommand SetParameters(NewPublicFolderMigrationRequestCommand.MigrationOutlookAnywherePublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A83 RID: 2691
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005BF5 RID: 23541
			// (set) Token: 0x06008545 RID: 34117 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005BF6 RID: 23542
			// (set) Token: 0x06008546 RID: 34118 RVA: 0x000C4BD3 File Offset: 0x000C2DD3
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005BF7 RID: 23543
			// (set) Token: 0x06008547 RID: 34119 RVA: 0x000C4BE6 File Offset: 0x000C2DE6
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005BF8 RID: 23544
			// (set) Token: 0x06008548 RID: 34120 RVA: 0x000C4BFE File Offset: 0x000C2DFE
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005BF9 RID: 23545
			// (set) Token: 0x06008549 RID: 34121 RVA: 0x000C4C16 File Offset: 0x000C2E16
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005BFA RID: 23546
			// (set) Token: 0x0600854A RID: 34122 RVA: 0x000C4C2E File Offset: 0x000C2E2E
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005BFB RID: 23547
			// (set) Token: 0x0600854B RID: 34123 RVA: 0x000C4C46 File Offset: 0x000C2E46
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005BFC RID: 23548
			// (set) Token: 0x0600854C RID: 34124 RVA: 0x000C4C59 File Offset: 0x000C2E59
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BFD RID: 23549
			// (set) Token: 0x0600854D RID: 34125 RVA: 0x000C4C6C File Offset: 0x000C2E6C
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005BFE RID: 23550
			// (set) Token: 0x0600854E RID: 34126 RVA: 0x000C4C84 File Offset: 0x000C2E84
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005BFF RID: 23551
			// (set) Token: 0x0600854F RID: 34127 RVA: 0x000C4C97 File Offset: 0x000C2E97
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005C00 RID: 23552
			// (set) Token: 0x06008550 RID: 34128 RVA: 0x000C4CAF File Offset: 0x000C2EAF
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005C01 RID: 23553
			// (set) Token: 0x06008551 RID: 34129 RVA: 0x000C4CC7 File Offset: 0x000C2EC7
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005C02 RID: 23554
			// (set) Token: 0x06008552 RID: 34130 RVA: 0x000C4CDF File Offset: 0x000C2EDF
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005C03 RID: 23555
			// (set) Token: 0x06008553 RID: 34131 RVA: 0x000C4CF7 File Offset: 0x000C2EF7
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005C04 RID: 23556
			// (set) Token: 0x06008554 RID: 34132 RVA: 0x000C4D0F File Offset: 0x000C2F0F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C05 RID: 23557
			// (set) Token: 0x06008555 RID: 34133 RVA: 0x000C4D27 File Offset: 0x000C2F27
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C06 RID: 23558
			// (set) Token: 0x06008556 RID: 34134 RVA: 0x000C4D3F File Offset: 0x000C2F3F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C07 RID: 23559
			// (set) Token: 0x06008557 RID: 34135 RVA: 0x000C4D57 File Offset: 0x000C2F57
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C08 RID: 23560
			// (set) Token: 0x06008558 RID: 34136 RVA: 0x000C4D6F File Offset: 0x000C2F6F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A84 RID: 2692
		public class MigrationLocalPublicFolderParameters : ParametersBase
		{
			// Token: 0x17005C09 RID: 23561
			// (set) Token: 0x0600855A RID: 34138 RVA: 0x000C4D8F File Offset: 0x000C2F8F
			public virtual DatabaseIdParameter SourceDatabase
			{
				set
				{
					base.PowerSharpParameters["SourceDatabase"] = value;
				}
			}

			// Token: 0x17005C0A RID: 23562
			// (set) Token: 0x0600855B RID: 34139 RVA: 0x000C4DA2 File Offset: 0x000C2FA2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005C0B RID: 23563
			// (set) Token: 0x0600855C RID: 34140 RVA: 0x000C4DB5 File Offset: 0x000C2FB5
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005C0C RID: 23564
			// (set) Token: 0x0600855D RID: 34141 RVA: 0x000C4DC8 File Offset: 0x000C2FC8
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005C0D RID: 23565
			// (set) Token: 0x0600855E RID: 34142 RVA: 0x000C4DE0 File Offset: 0x000C2FE0
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005C0E RID: 23566
			// (set) Token: 0x0600855F RID: 34143 RVA: 0x000C4DF8 File Offset: 0x000C2FF8
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005C0F RID: 23567
			// (set) Token: 0x06008560 RID: 34144 RVA: 0x000C4E10 File Offset: 0x000C3010
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005C10 RID: 23568
			// (set) Token: 0x06008561 RID: 34145 RVA: 0x000C4E28 File Offset: 0x000C3028
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005C11 RID: 23569
			// (set) Token: 0x06008562 RID: 34146 RVA: 0x000C4E3B File Offset: 0x000C303B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C12 RID: 23570
			// (set) Token: 0x06008563 RID: 34147 RVA: 0x000C4E4E File Offset: 0x000C304E
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005C13 RID: 23571
			// (set) Token: 0x06008564 RID: 34148 RVA: 0x000C4E66 File Offset: 0x000C3066
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005C14 RID: 23572
			// (set) Token: 0x06008565 RID: 34149 RVA: 0x000C4E79 File Offset: 0x000C3079
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005C15 RID: 23573
			// (set) Token: 0x06008566 RID: 34150 RVA: 0x000C4E91 File Offset: 0x000C3091
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005C16 RID: 23574
			// (set) Token: 0x06008567 RID: 34151 RVA: 0x000C4EA9 File Offset: 0x000C30A9
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005C17 RID: 23575
			// (set) Token: 0x06008568 RID: 34152 RVA: 0x000C4EC1 File Offset: 0x000C30C1
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005C18 RID: 23576
			// (set) Token: 0x06008569 RID: 34153 RVA: 0x000C4ED9 File Offset: 0x000C30D9
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005C19 RID: 23577
			// (set) Token: 0x0600856A RID: 34154 RVA: 0x000C4EF1 File Offset: 0x000C30F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C1A RID: 23578
			// (set) Token: 0x0600856B RID: 34155 RVA: 0x000C4F09 File Offset: 0x000C3109
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C1B RID: 23579
			// (set) Token: 0x0600856C RID: 34156 RVA: 0x000C4F21 File Offset: 0x000C3121
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C1C RID: 23580
			// (set) Token: 0x0600856D RID: 34157 RVA: 0x000C4F39 File Offset: 0x000C3139
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C1D RID: 23581
			// (set) Token: 0x0600856E RID: 34158 RVA: 0x000C4F51 File Offset: 0x000C3151
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A85 RID: 2693
		public class MigrationOutlookAnywherePublicFolderParameters : ParametersBase
		{
			// Token: 0x17005C1E RID: 23582
			// (set) Token: 0x06008570 RID: 34160 RVA: 0x000C4F71 File Offset: 0x000C3171
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005C1F RID: 23583
			// (set) Token: 0x06008571 RID: 34161 RVA: 0x000C4F8F File Offset: 0x000C318F
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005C20 RID: 23584
			// (set) Token: 0x06008572 RID: 34162 RVA: 0x000C4FA2 File Offset: 0x000C31A2
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005C21 RID: 23585
			// (set) Token: 0x06008573 RID: 34163 RVA: 0x000C4FB5 File Offset: 0x000C31B5
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005C22 RID: 23586
			// (set) Token: 0x06008574 RID: 34164 RVA: 0x000C4FC8 File Offset: 0x000C31C8
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005C23 RID: 23587
			// (set) Token: 0x06008575 RID: 34165 RVA: 0x000C4FE0 File Offset: 0x000C31E0
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005C24 RID: 23588
			// (set) Token: 0x06008576 RID: 34166 RVA: 0x000C4FF3 File Offset: 0x000C31F3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005C25 RID: 23589
			// (set) Token: 0x06008577 RID: 34167 RVA: 0x000C5006 File Offset: 0x000C3206
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x17005C26 RID: 23590
			// (set) Token: 0x06008578 RID: 34168 RVA: 0x000C5019 File Offset: 0x000C3219
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17005C27 RID: 23591
			// (set) Token: 0x06008579 RID: 34169 RVA: 0x000C5031 File Offset: 0x000C3231
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005C28 RID: 23592
			// (set) Token: 0x0600857A RID: 34170 RVA: 0x000C5049 File Offset: 0x000C3249
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005C29 RID: 23593
			// (set) Token: 0x0600857B RID: 34171 RVA: 0x000C5061 File Offset: 0x000C3261
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005C2A RID: 23594
			// (set) Token: 0x0600857C RID: 34172 RVA: 0x000C5079 File Offset: 0x000C3279
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005C2B RID: 23595
			// (set) Token: 0x0600857D RID: 34173 RVA: 0x000C508C File Offset: 0x000C328C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C2C RID: 23596
			// (set) Token: 0x0600857E RID: 34174 RVA: 0x000C509F File Offset: 0x000C329F
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005C2D RID: 23597
			// (set) Token: 0x0600857F RID: 34175 RVA: 0x000C50B7 File Offset: 0x000C32B7
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005C2E RID: 23598
			// (set) Token: 0x06008580 RID: 34176 RVA: 0x000C50CA File Offset: 0x000C32CA
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005C2F RID: 23599
			// (set) Token: 0x06008581 RID: 34177 RVA: 0x000C50E2 File Offset: 0x000C32E2
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005C30 RID: 23600
			// (set) Token: 0x06008582 RID: 34178 RVA: 0x000C50FA File Offset: 0x000C32FA
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005C31 RID: 23601
			// (set) Token: 0x06008583 RID: 34179 RVA: 0x000C5112 File Offset: 0x000C3312
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005C32 RID: 23602
			// (set) Token: 0x06008584 RID: 34180 RVA: 0x000C512A File Offset: 0x000C332A
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005C33 RID: 23603
			// (set) Token: 0x06008585 RID: 34181 RVA: 0x000C5142 File Offset: 0x000C3342
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C34 RID: 23604
			// (set) Token: 0x06008586 RID: 34182 RVA: 0x000C515A File Offset: 0x000C335A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C35 RID: 23605
			// (set) Token: 0x06008587 RID: 34183 RVA: 0x000C5172 File Offset: 0x000C3372
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C36 RID: 23606
			// (set) Token: 0x06008588 RID: 34184 RVA: 0x000C518A File Offset: 0x000C338A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C37 RID: 23607
			// (set) Token: 0x06008589 RID: 34185 RVA: 0x000C51A2 File Offset: 0x000C33A2
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
