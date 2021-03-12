using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000270 RID: 624
	public class NewMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatch, MigrationBatch>
	{
		// Token: 0x06002D6C RID: 11628 RVA: 0x00052C10 File Offset: 0x00050E10
		private NewMigrationBatchCommand() : base("New-MigrationBatch")
		{
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x00052C1D File Offset: 0x00050E1D
		public NewMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00052C2C File Offset: 0x00050E2C
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.OnboardingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00052C36 File Offset: 0x00050E36
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.LocalPublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00052C40 File Offset: 0x00050E40
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.XO1Parameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x00052C4A File Offset: 0x00050E4A
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.OffboardingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00052C54 File Offset: 0x00050E54
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.LocalParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x00052C5E File Offset: 0x00050E5E
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00052C68 File Offset: 0x00050E68
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.PreexistingUserIdsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x00052C72 File Offset: 0x00050E72
		public virtual NewMigrationBatchCommand SetParameters(NewMigrationBatchCommand.PreexistingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000271 RID: 625
		public class OnboardingParameters : ParametersBase
		{
			// Token: 0x17001445 RID: 5189
			// (set) Token: 0x06002D76 RID: 11638 RVA: 0x00052C7C File Offset: 0x00050E7C
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17001446 RID: 5190
			// (set) Token: 0x06002D77 RID: 11639 RVA: 0x00052C94 File Offset: 0x00050E94
			public virtual MultiValuedProperty<string> ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x17001447 RID: 5191
			// (set) Token: 0x06002D78 RID: 11640 RVA: 0x00052CA7 File Offset: 0x00050EA7
			public virtual SwitchParameter DisallowExistingUsers
			{
				set
				{
					base.PowerSharpParameters["DisallowExistingUsers"] = value;
				}
			}

			// Token: 0x17001448 RID: 5192
			// (set) Token: 0x06002D79 RID: 11641 RVA: 0x00052CBF File Offset: 0x00050EBF
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x17001449 RID: 5193
			// (set) Token: 0x06002D7A RID: 11642 RVA: 0x00052CD7 File Offset: 0x00050ED7
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x1700144A RID: 5194
			// (set) Token: 0x06002D7B RID: 11643 RVA: 0x00052CEF File Offset: 0x00050EEF
			public virtual string SourceEndpoint
			{
				set
				{
					base.PowerSharpParameters["SourceEndpoint"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x1700144B RID: 5195
			// (set) Token: 0x06002D7C RID: 11644 RVA: 0x00052D0D File Offset: 0x00050F0D
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700144C RID: 5196
			// (set) Token: 0x06002D7D RID: 11645 RVA: 0x00052D25 File Offset: 0x00050F25
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700144D RID: 5197
			// (set) Token: 0x06002D7E RID: 11646 RVA: 0x00052D3D File Offset: 0x00050F3D
			public virtual MultiValuedProperty<string> TargetArchiveDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetArchiveDatabases"] = value;
				}
			}

			// Token: 0x1700144E RID: 5198
			// (set) Token: 0x06002D7F RID: 11647 RVA: 0x00052D50 File Offset: 0x00050F50
			public virtual MultiValuedProperty<string> TargetDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetDatabases"] = value;
				}
			}

			// Token: 0x1700144F RID: 5199
			// (set) Token: 0x06002D80 RID: 11648 RVA: 0x00052D63 File Offset: 0x00050F63
			public virtual string TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x17001450 RID: 5200
			// (set) Token: 0x06002D81 RID: 11649 RVA: 0x00052D76 File Offset: 0x00050F76
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x17001451 RID: 5201
			// (set) Token: 0x06002D82 RID: 11650 RVA: 0x00052D8E File Offset: 0x00050F8E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001452 RID: 5202
			// (set) Token: 0x06002D83 RID: 11651 RVA: 0x00052DA1 File Offset: 0x00050FA1
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001453 RID: 5203
			// (set) Token: 0x06002D84 RID: 11652 RVA: 0x00052DB9 File Offset: 0x00050FB9
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x17001454 RID: 5204
			// (set) Token: 0x06002D85 RID: 11653 RVA: 0x00052DCC File Offset: 0x00050FCC
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x17001455 RID: 5205
			// (set) Token: 0x06002D86 RID: 11654 RVA: 0x00052DE4 File Offset: 0x00050FE4
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17001456 RID: 5206
			// (set) Token: 0x06002D87 RID: 11655 RVA: 0x00052DF7 File Offset: 0x00050FF7
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001457 RID: 5207
			// (set) Token: 0x06002D88 RID: 11656 RVA: 0x00052E0F File Offset: 0x0005100F
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001458 RID: 5208
			// (set) Token: 0x06002D89 RID: 11657 RVA: 0x00052E22 File Offset: 0x00051022
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x17001459 RID: 5209
			// (set) Token: 0x06002D8A RID: 11658 RVA: 0x00052E3A File Offset: 0x0005103A
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x1700145A RID: 5210
			// (set) Token: 0x06002D8B RID: 11659 RVA: 0x00052E52 File Offset: 0x00051052
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700145B RID: 5211
			// (set) Token: 0x06002D8C RID: 11660 RVA: 0x00052E6A File Offset: 0x0005106A
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700145C RID: 5212
			// (set) Token: 0x06002D8D RID: 11661 RVA: 0x00052E82 File Offset: 0x00051082
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x1700145D RID: 5213
			// (set) Token: 0x06002D8E RID: 11662 RVA: 0x00052E9A File Offset: 0x0005109A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700145E RID: 5214
			// (set) Token: 0x06002D8F RID: 11663 RVA: 0x00052EB8 File Offset: 0x000510B8
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700145F RID: 5215
			// (set) Token: 0x06002D90 RID: 11664 RVA: 0x00052ED6 File Offset: 0x000510D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001460 RID: 5216
			// (set) Token: 0x06002D91 RID: 11665 RVA: 0x00052EE9 File Offset: 0x000510E9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001461 RID: 5217
			// (set) Token: 0x06002D92 RID: 11666 RVA: 0x00052F01 File Offset: 0x00051101
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001462 RID: 5218
			// (set) Token: 0x06002D93 RID: 11667 RVA: 0x00052F19 File Offset: 0x00051119
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001463 RID: 5219
			// (set) Token: 0x06002D94 RID: 11668 RVA: 0x00052F31 File Offset: 0x00051131
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001464 RID: 5220
			// (set) Token: 0x06002D95 RID: 11669 RVA: 0x00052F49 File Offset: 0x00051149
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000272 RID: 626
		public class LocalPublicFolderParameters : ParametersBase
		{
			// Token: 0x17001465 RID: 5221
			// (set) Token: 0x06002D97 RID: 11671 RVA: 0x00052F69 File Offset: 0x00051169
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17001466 RID: 5222
			// (set) Token: 0x06002D98 RID: 11672 RVA: 0x00052F81 File Offset: 0x00051181
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17001467 RID: 5223
			// (set) Token: 0x06002D99 RID: 11673 RVA: 0x00052F99 File Offset: 0x00051199
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17001468 RID: 5224
			// (set) Token: 0x06002D9A RID: 11674 RVA: 0x00052FB1 File Offset: 0x000511B1
			public virtual DatabaseIdParameter SourcePublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["SourcePublicFolderDatabase"] = value;
				}
			}

			// Token: 0x17001469 RID: 5225
			// (set) Token: 0x06002D9B RID: 11675 RVA: 0x00052FC4 File Offset: 0x000511C4
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x1700146A RID: 5226
			// (set) Token: 0x06002D9C RID: 11676 RVA: 0x00052FDC File Offset: 0x000511DC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700146B RID: 5227
			// (set) Token: 0x06002D9D RID: 11677 RVA: 0x00052FEF File Offset: 0x000511EF
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x1700146C RID: 5228
			// (set) Token: 0x06002D9E RID: 11678 RVA: 0x00053007 File Offset: 0x00051207
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x1700146D RID: 5229
			// (set) Token: 0x06002D9F RID: 11679 RVA: 0x0005301A File Offset: 0x0005121A
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x1700146E RID: 5230
			// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x00053032 File Offset: 0x00051232
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x1700146F RID: 5231
			// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x00053045 File Offset: 0x00051245
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001470 RID: 5232
			// (set) Token: 0x06002DA2 RID: 11682 RVA: 0x0005305D File Offset: 0x0005125D
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001471 RID: 5233
			// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x00053070 File Offset: 0x00051270
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x17001472 RID: 5234
			// (set) Token: 0x06002DA4 RID: 11684 RVA: 0x00053088 File Offset: 0x00051288
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x17001473 RID: 5235
			// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x000530A0 File Offset: 0x000512A0
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17001474 RID: 5236
			// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000530B8 File Offset: 0x000512B8
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17001475 RID: 5237
			// (set) Token: 0x06002DA7 RID: 11687 RVA: 0x000530D0 File Offset: 0x000512D0
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x17001476 RID: 5238
			// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x000530E8 File Offset: 0x000512E8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001477 RID: 5239
			// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x00053106 File Offset: 0x00051306
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001478 RID: 5240
			// (set) Token: 0x06002DAA RID: 11690 RVA: 0x00053124 File Offset: 0x00051324
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001479 RID: 5241
			// (set) Token: 0x06002DAB RID: 11691 RVA: 0x00053137 File Offset: 0x00051337
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700147A RID: 5242
			// (set) Token: 0x06002DAC RID: 11692 RVA: 0x0005314F File Offset: 0x0005134F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700147B RID: 5243
			// (set) Token: 0x06002DAD RID: 11693 RVA: 0x00053167 File Offset: 0x00051367
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700147C RID: 5244
			// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0005317F File Offset: 0x0005137F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700147D RID: 5245
			// (set) Token: 0x06002DAF RID: 11695 RVA: 0x00053197 File Offset: 0x00051397
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000273 RID: 627
		public class XO1Parameters : ParametersBase
		{
			// Token: 0x1700147E RID: 5246
			// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x000531B7 File Offset: 0x000513B7
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x1700147F RID: 5247
			// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000531CF File Offset: 0x000513CF
			public virtual SwitchParameter XO1
			{
				set
				{
					base.PowerSharpParameters["XO1"] = value;
				}
			}

			// Token: 0x17001480 RID: 5248
			// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x000531E7 File Offset: 0x000513E7
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x17001481 RID: 5249
			// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000531FF File Offset: 0x000513FF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001482 RID: 5250
			// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x00053212 File Offset: 0x00051412
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001483 RID: 5251
			// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x0005322A File Offset: 0x0005142A
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x17001484 RID: 5252
			// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x0005323D File Offset: 0x0005143D
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x17001485 RID: 5253
			// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x00053255 File Offset: 0x00051455
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17001486 RID: 5254
			// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x00053268 File Offset: 0x00051468
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001487 RID: 5255
			// (set) Token: 0x06002DBA RID: 11706 RVA: 0x00053280 File Offset: 0x00051480
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001488 RID: 5256
			// (set) Token: 0x06002DBB RID: 11707 RVA: 0x00053293 File Offset: 0x00051493
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x17001489 RID: 5257
			// (set) Token: 0x06002DBC RID: 11708 RVA: 0x000532AB File Offset: 0x000514AB
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x1700148A RID: 5258
			// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000532C3 File Offset: 0x000514C3
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700148B RID: 5259
			// (set) Token: 0x06002DBE RID: 11710 RVA: 0x000532DB File Offset: 0x000514DB
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700148C RID: 5260
			// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000532F3 File Offset: 0x000514F3
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x1700148D RID: 5261
			// (set) Token: 0x06002DC0 RID: 11712 RVA: 0x0005330B File Offset: 0x0005150B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700148E RID: 5262
			// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x00053329 File Offset: 0x00051529
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700148F RID: 5263
			// (set) Token: 0x06002DC2 RID: 11714 RVA: 0x00053347 File Offset: 0x00051547
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001490 RID: 5264
			// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x0005335A File Offset: 0x0005155A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001491 RID: 5265
			// (set) Token: 0x06002DC4 RID: 11716 RVA: 0x00053372 File Offset: 0x00051572
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001492 RID: 5266
			// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x0005338A File Offset: 0x0005158A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001493 RID: 5267
			// (set) Token: 0x06002DC6 RID: 11718 RVA: 0x000533A2 File Offset: 0x000515A2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001494 RID: 5268
			// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000533BA File Offset: 0x000515BA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000274 RID: 628
		public class OffboardingParameters : ParametersBase
		{
			// Token: 0x17001495 RID: 5269
			// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000533DA File Offset: 0x000515DA
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17001496 RID: 5270
			// (set) Token: 0x06002DCA RID: 11722 RVA: 0x000533F2 File Offset: 0x000515F2
			public virtual SwitchParameter DisallowExistingUsers
			{
				set
				{
					base.PowerSharpParameters["DisallowExistingUsers"] = value;
				}
			}

			// Token: 0x17001497 RID: 5271
			// (set) Token: 0x06002DCB RID: 11723 RVA: 0x0005340A File Offset: 0x0005160A
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x17001498 RID: 5272
			// (set) Token: 0x06002DCC RID: 11724 RVA: 0x00053422 File Offset: 0x00051622
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x17001499 RID: 5273
			// (set) Token: 0x06002DCD RID: 11725 RVA: 0x0005343A File Offset: 0x0005163A
			public virtual string TargetEndpoint
			{
				set
				{
					base.PowerSharpParameters["TargetEndpoint"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x1700149A RID: 5274
			// (set) Token: 0x06002DCE RID: 11726 RVA: 0x00053458 File Offset: 0x00051658
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700149B RID: 5275
			// (set) Token: 0x06002DCF RID: 11727 RVA: 0x00053470 File Offset: 0x00051670
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700149C RID: 5276
			// (set) Token: 0x06002DD0 RID: 11728 RVA: 0x00053488 File Offset: 0x00051688
			public virtual MultiValuedProperty<string> TargetArchiveDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetArchiveDatabases"] = value;
				}
			}

			// Token: 0x1700149D RID: 5277
			// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x0005349B File Offset: 0x0005169B
			public virtual MultiValuedProperty<string> TargetDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetDatabases"] = value;
				}
			}

			// Token: 0x1700149E RID: 5278
			// (set) Token: 0x06002DD2 RID: 11730 RVA: 0x000534AE File Offset: 0x000516AE
			public virtual string TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x1700149F RID: 5279
			// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000534C1 File Offset: 0x000516C1
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x170014A0 RID: 5280
			// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x000534D9 File Offset: 0x000516D9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170014A1 RID: 5281
			// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x000534EC File Offset: 0x000516EC
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170014A2 RID: 5282
			// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x00053504 File Offset: 0x00051704
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170014A3 RID: 5283
			// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x00053517 File Offset: 0x00051717
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x170014A4 RID: 5284
			// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x0005352F File Offset: 0x0005172F
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170014A5 RID: 5285
			// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x00053542 File Offset: 0x00051742
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x170014A6 RID: 5286
			// (set) Token: 0x06002DDA RID: 11738 RVA: 0x0005355A File Offset: 0x0005175A
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170014A7 RID: 5287
			// (set) Token: 0x06002DDB RID: 11739 RVA: 0x0005356D File Offset: 0x0005176D
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x170014A8 RID: 5288
			// (set) Token: 0x06002DDC RID: 11740 RVA: 0x00053585 File Offset: 0x00051785
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x170014A9 RID: 5289
			// (set) Token: 0x06002DDD RID: 11741 RVA: 0x0005359D File Offset: 0x0005179D
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170014AA RID: 5290
			// (set) Token: 0x06002DDE RID: 11742 RVA: 0x000535B5 File Offset: 0x000517B5
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170014AB RID: 5291
			// (set) Token: 0x06002DDF RID: 11743 RVA: 0x000535CD File Offset: 0x000517CD
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x170014AC RID: 5292
			// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x000535E5 File Offset: 0x000517E5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170014AD RID: 5293
			// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x00053603 File Offset: 0x00051803
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170014AE RID: 5294
			// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x00053621 File Offset: 0x00051821
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170014AF RID: 5295
			// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x00053634 File Offset: 0x00051834
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170014B0 RID: 5296
			// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x0005364C File Offset: 0x0005184C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170014B1 RID: 5297
			// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x00053664 File Offset: 0x00051864
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170014B2 RID: 5298
			// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x0005367C File Offset: 0x0005187C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170014B3 RID: 5299
			// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x00053694 File Offset: 0x00051894
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000275 RID: 629
		public class LocalParameters : ParametersBase
		{
			// Token: 0x170014B4 RID: 5300
			// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x000536B4 File Offset: 0x000518B4
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x170014B5 RID: 5301
			// (set) Token: 0x06002DEA RID: 11754 RVA: 0x000536CC File Offset: 0x000518CC
			public virtual SwitchParameter DisallowExistingUsers
			{
				set
				{
					base.PowerSharpParameters["DisallowExistingUsers"] = value;
				}
			}

			// Token: 0x170014B6 RID: 5302
			// (set) Token: 0x06002DEB RID: 11755 RVA: 0x000536E4 File Offset: 0x000518E4
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x170014B7 RID: 5303
			// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000536FC File Offset: 0x000518FC
			public virtual SwitchParameter Local
			{
				set
				{
					base.PowerSharpParameters["Local"] = value;
				}
			}

			// Token: 0x170014B8 RID: 5304
			// (set) Token: 0x06002DED RID: 11757 RVA: 0x00053714 File Offset: 0x00051914
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x170014B9 RID: 5305
			// (set) Token: 0x06002DEE RID: 11758 RVA: 0x0005372C File Offset: 0x0005192C
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170014BA RID: 5306
			// (set) Token: 0x06002DEF RID: 11759 RVA: 0x00053744 File Offset: 0x00051944
			public virtual MultiValuedProperty<string> TargetArchiveDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetArchiveDatabases"] = value;
				}
			}

			// Token: 0x170014BB RID: 5307
			// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x00053757 File Offset: 0x00051957
			public virtual MultiValuedProperty<string> TargetDatabases
			{
				set
				{
					base.PowerSharpParameters["TargetDatabases"] = value;
				}
			}

			// Token: 0x170014BC RID: 5308
			// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x0005376A File Offset: 0x0005196A
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x170014BD RID: 5309
			// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x00053782 File Offset: 0x00051982
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170014BE RID: 5310
			// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x00053795 File Offset: 0x00051995
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170014BF RID: 5311
			// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x000537AD File Offset: 0x000519AD
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170014C0 RID: 5312
			// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x000537C0 File Offset: 0x000519C0
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x170014C1 RID: 5313
			// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000537D8 File Offset: 0x000519D8
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170014C2 RID: 5314
			// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x000537EB File Offset: 0x000519EB
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x170014C3 RID: 5315
			// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x00053803 File Offset: 0x00051A03
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170014C4 RID: 5316
			// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x00053816 File Offset: 0x00051A16
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x170014C5 RID: 5317
			// (set) Token: 0x06002DFA RID: 11770 RVA: 0x0005382E File Offset: 0x00051A2E
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x170014C6 RID: 5318
			// (set) Token: 0x06002DFB RID: 11771 RVA: 0x00053846 File Offset: 0x00051A46
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170014C7 RID: 5319
			// (set) Token: 0x06002DFC RID: 11772 RVA: 0x0005385E File Offset: 0x00051A5E
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170014C8 RID: 5320
			// (set) Token: 0x06002DFD RID: 11773 RVA: 0x00053876 File Offset: 0x00051A76
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x170014C9 RID: 5321
			// (set) Token: 0x06002DFE RID: 11774 RVA: 0x0005388E File Offset: 0x00051A8E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170014CA RID: 5322
			// (set) Token: 0x06002DFF RID: 11775 RVA: 0x000538AC File Offset: 0x00051AAC
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170014CB RID: 5323
			// (set) Token: 0x06002E00 RID: 11776 RVA: 0x000538CA File Offset: 0x00051ACA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170014CC RID: 5324
			// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000538DD File Offset: 0x00051ADD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170014CD RID: 5325
			// (set) Token: 0x06002E02 RID: 11778 RVA: 0x000538F5 File Offset: 0x00051AF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170014CE RID: 5326
			// (set) Token: 0x06002E03 RID: 11779 RVA: 0x0005390D File Offset: 0x00051B0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170014CF RID: 5327
			// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00053925 File Offset: 0x00051B25
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170014D0 RID: 5328
			// (set) Token: 0x06002E05 RID: 11781 RVA: 0x0005393D File Offset: 0x00051B3D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000276 RID: 630
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170014D1 RID: 5329
			// (set) Token: 0x06002E07 RID: 11783 RVA: 0x0005395D File Offset: 0x00051B5D
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x170014D2 RID: 5330
			// (set) Token: 0x06002E08 RID: 11784 RVA: 0x00053975 File Offset: 0x00051B75
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170014D3 RID: 5331
			// (set) Token: 0x06002E09 RID: 11785 RVA: 0x00053988 File Offset: 0x00051B88
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170014D4 RID: 5332
			// (set) Token: 0x06002E0A RID: 11786 RVA: 0x000539A0 File Offset: 0x00051BA0
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170014D5 RID: 5333
			// (set) Token: 0x06002E0B RID: 11787 RVA: 0x000539B3 File Offset: 0x00051BB3
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x170014D6 RID: 5334
			// (set) Token: 0x06002E0C RID: 11788 RVA: 0x000539CB File Offset: 0x00051BCB
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170014D7 RID: 5335
			// (set) Token: 0x06002E0D RID: 11789 RVA: 0x000539DE File Offset: 0x00051BDE
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x170014D8 RID: 5336
			// (set) Token: 0x06002E0E RID: 11790 RVA: 0x000539F6 File Offset: 0x00051BF6
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170014D9 RID: 5337
			// (set) Token: 0x06002E0F RID: 11791 RVA: 0x00053A09 File Offset: 0x00051C09
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x170014DA RID: 5338
			// (set) Token: 0x06002E10 RID: 11792 RVA: 0x00053A21 File Offset: 0x00051C21
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x170014DB RID: 5339
			// (set) Token: 0x06002E11 RID: 11793 RVA: 0x00053A39 File Offset: 0x00051C39
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170014DC RID: 5340
			// (set) Token: 0x06002E12 RID: 11794 RVA: 0x00053A51 File Offset: 0x00051C51
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170014DD RID: 5341
			// (set) Token: 0x06002E13 RID: 11795 RVA: 0x00053A69 File Offset: 0x00051C69
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x170014DE RID: 5342
			// (set) Token: 0x06002E14 RID: 11796 RVA: 0x00053A81 File Offset: 0x00051C81
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170014DF RID: 5343
			// (set) Token: 0x06002E15 RID: 11797 RVA: 0x00053A9F File Offset: 0x00051C9F
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170014E0 RID: 5344
			// (set) Token: 0x06002E16 RID: 11798 RVA: 0x00053ABD File Offset: 0x00051CBD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170014E1 RID: 5345
			// (set) Token: 0x06002E17 RID: 11799 RVA: 0x00053AD0 File Offset: 0x00051CD0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170014E2 RID: 5346
			// (set) Token: 0x06002E18 RID: 11800 RVA: 0x00053AE8 File Offset: 0x00051CE8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170014E3 RID: 5347
			// (set) Token: 0x06002E19 RID: 11801 RVA: 0x00053B00 File Offset: 0x00051D00
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170014E4 RID: 5348
			// (set) Token: 0x06002E1A RID: 11802 RVA: 0x00053B18 File Offset: 0x00051D18
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170014E5 RID: 5349
			// (set) Token: 0x06002E1B RID: 11803 RVA: 0x00053B30 File Offset: 0x00051D30
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000277 RID: 631
		public class PreexistingUserIdsParameters : ParametersBase
		{
			// Token: 0x170014E6 RID: 5350
			// (set) Token: 0x06002E1D RID: 11805 RVA: 0x00053B50 File Offset: 0x00051D50
			public virtual SwitchParameter DisableOnCopy
			{
				set
				{
					base.PowerSharpParameters["DisableOnCopy"] = value;
				}
			}

			// Token: 0x170014E7 RID: 5351
			// (set) Token: 0x06002E1E RID: 11806 RVA: 0x00053B68 File Offset: 0x00051D68
			public virtual MultiValuedProperty<MigrationUserIdParameter> UserIds
			{
				set
				{
					base.PowerSharpParameters["UserIds"] = value;
				}
			}

			// Token: 0x170014E8 RID: 5352
			// (set) Token: 0x06002E1F RID: 11807 RVA: 0x00053B7B File Offset: 0x00051D7B
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x170014E9 RID: 5353
			// (set) Token: 0x06002E20 RID: 11808 RVA: 0x00053B93 File Offset: 0x00051D93
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170014EA RID: 5354
			// (set) Token: 0x06002E21 RID: 11809 RVA: 0x00053BA6 File Offset: 0x00051DA6
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x170014EB RID: 5355
			// (set) Token: 0x06002E22 RID: 11810 RVA: 0x00053BBE File Offset: 0x00051DBE
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x170014EC RID: 5356
			// (set) Token: 0x06002E23 RID: 11811 RVA: 0x00053BD1 File Offset: 0x00051DD1
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x170014ED RID: 5357
			// (set) Token: 0x06002E24 RID: 11812 RVA: 0x00053BE9 File Offset: 0x00051DE9
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x170014EE RID: 5358
			// (set) Token: 0x06002E25 RID: 11813 RVA: 0x00053BFC File Offset: 0x00051DFC
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x170014EF RID: 5359
			// (set) Token: 0x06002E26 RID: 11814 RVA: 0x00053C14 File Offset: 0x00051E14
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170014F0 RID: 5360
			// (set) Token: 0x06002E27 RID: 11815 RVA: 0x00053C27 File Offset: 0x00051E27
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x170014F1 RID: 5361
			// (set) Token: 0x06002E28 RID: 11816 RVA: 0x00053C3F File Offset: 0x00051E3F
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x170014F2 RID: 5362
			// (set) Token: 0x06002E29 RID: 11817 RVA: 0x00053C57 File Offset: 0x00051E57
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170014F3 RID: 5363
			// (set) Token: 0x06002E2A RID: 11818 RVA: 0x00053C6F File Offset: 0x00051E6F
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170014F4 RID: 5364
			// (set) Token: 0x06002E2B RID: 11819 RVA: 0x00053C87 File Offset: 0x00051E87
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x170014F5 RID: 5365
			// (set) Token: 0x06002E2C RID: 11820 RVA: 0x00053C9F File Offset: 0x00051E9F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170014F6 RID: 5366
			// (set) Token: 0x06002E2D RID: 11821 RVA: 0x00053CBD File Offset: 0x00051EBD
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170014F7 RID: 5367
			// (set) Token: 0x06002E2E RID: 11822 RVA: 0x00053CDB File Offset: 0x00051EDB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170014F8 RID: 5368
			// (set) Token: 0x06002E2F RID: 11823 RVA: 0x00053CEE File Offset: 0x00051EEE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170014F9 RID: 5369
			// (set) Token: 0x06002E30 RID: 11824 RVA: 0x00053D06 File Offset: 0x00051F06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170014FA RID: 5370
			// (set) Token: 0x06002E31 RID: 11825 RVA: 0x00053D1E File Offset: 0x00051F1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170014FB RID: 5371
			// (set) Token: 0x06002E32 RID: 11826 RVA: 0x00053D36 File Offset: 0x00051F36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170014FC RID: 5372
			// (set) Token: 0x06002E33 RID: 11827 RVA: 0x00053D4E File Offset: 0x00051F4E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000278 RID: 632
		public class PreexistingParameters : ParametersBase
		{
			// Token: 0x170014FD RID: 5373
			// (set) Token: 0x06002E35 RID: 11829 RVA: 0x00053D6E File Offset: 0x00051F6E
			public virtual SwitchParameter DisableOnCopy
			{
				set
				{
					base.PowerSharpParameters["DisableOnCopy"] = value;
				}
			}

			// Token: 0x170014FE RID: 5374
			// (set) Token: 0x06002E36 RID: 11830 RVA: 0x00053D86 File Offset: 0x00051F86
			public virtual MultiValuedProperty<MigrationUser> Users
			{
				set
				{
					base.PowerSharpParameters["Users"] = value;
				}
			}

			// Token: 0x170014FF RID: 5375
			// (set) Token: 0x06002E37 RID: 11831 RVA: 0x00053D99 File Offset: 0x00051F99
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x17001500 RID: 5376
			// (set) Token: 0x06002E38 RID: 11832 RVA: 0x00053DB1 File Offset: 0x00051FB1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001501 RID: 5377
			// (set) Token: 0x06002E39 RID: 11833 RVA: 0x00053DC4 File Offset: 0x00051FC4
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001502 RID: 5378
			// (set) Token: 0x06002E3A RID: 11834 RVA: 0x00053DDC File Offset: 0x00051FDC
			public virtual ExTimeZoneValue TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x17001503 RID: 5379
			// (set) Token: 0x06002E3B RID: 11835 RVA: 0x00053DEF File Offset: 0x00051FEF
			public virtual SkippableMigrationSteps SkipSteps
			{
				set
				{
					base.PowerSharpParameters["SkipSteps"] = value;
				}
			}

			// Token: 0x17001504 RID: 5380
			// (set) Token: 0x06002E3C RID: 11836 RVA: 0x00053E07 File Offset: 0x00052007
			public virtual CultureInfo Locale
			{
				set
				{
					base.PowerSharpParameters["Locale"] = value;
				}
			}

			// Token: 0x17001505 RID: 5381
			// (set) Token: 0x06002E3D RID: 11837 RVA: 0x00053E1A File Offset: 0x0005201A
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001506 RID: 5382
			// (set) Token: 0x06002E3E RID: 11838 RVA: 0x00053E32 File Offset: 0x00052032
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001507 RID: 5383
			// (set) Token: 0x06002E3F RID: 11839 RVA: 0x00053E45 File Offset: 0x00052045
			public virtual SwitchParameter AutoStart
			{
				set
				{
					base.PowerSharpParameters["AutoStart"] = value;
				}
			}

			// Token: 0x17001508 RID: 5384
			// (set) Token: 0x06002E40 RID: 11840 RVA: 0x00053E5D File Offset: 0x0005205D
			public virtual SwitchParameter AutoComplete
			{
				set
				{
					base.PowerSharpParameters["AutoComplete"] = value;
				}
			}

			// Token: 0x17001509 RID: 5385
			// (set) Token: 0x06002E41 RID: 11841 RVA: 0x00053E75 File Offset: 0x00052075
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700150A RID: 5386
			// (set) Token: 0x06002E42 RID: 11842 RVA: 0x00053E8D File Offset: 0x0005208D
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700150B RID: 5387
			// (set) Token: 0x06002E43 RID: 11843 RVA: 0x00053EA5 File Offset: 0x000520A5
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x1700150C RID: 5388
			// (set) Token: 0x06002E44 RID: 11844 RVA: 0x00053EBD File Offset: 0x000520BD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700150D RID: 5389
			// (set) Token: 0x06002E45 RID: 11845 RVA: 0x00053EDB File Offset: 0x000520DB
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700150E RID: 5390
			// (set) Token: 0x06002E46 RID: 11846 RVA: 0x00053EF9 File Offset: 0x000520F9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700150F RID: 5391
			// (set) Token: 0x06002E47 RID: 11847 RVA: 0x00053F0C File Offset: 0x0005210C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001510 RID: 5392
			// (set) Token: 0x06002E48 RID: 11848 RVA: 0x00053F24 File Offset: 0x00052124
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001511 RID: 5393
			// (set) Token: 0x06002E49 RID: 11849 RVA: 0x00053F3C File Offset: 0x0005213C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001512 RID: 5394
			// (set) Token: 0x06002E4A RID: 11850 RVA: 0x00053F54 File Offset: 0x00052154
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001513 RID: 5395
			// (set) Token: 0x06002E4B RID: 11851 RVA: 0x00053F6C File Offset: 0x0005216C
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
