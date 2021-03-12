using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200027F RID: 639
	public class SetMigrationBatchCommand : SyntheticCommandWithPipelineInputNoOutput<MigrationBatch>
	{
		// Token: 0x06002E83 RID: 11907 RVA: 0x00054424 File Offset: 0x00052624
		private SetMigrationBatchCommand() : base("Set-MigrationBatch")
		{
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00054431 File Offset: 0x00052631
		public SetMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x00054440 File Offset: 0x00052640
		public virtual SetMigrationBatchCommand SetParameters(SetMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0005444A File Offset: 0x0005264A
		public virtual SetMigrationBatchCommand SetParameters(SetMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000280 RID: 640
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700153E RID: 5438
			// (set) Token: 0x06002E87 RID: 11911 RVA: 0x00054454 File Offset: 0x00052654
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x1700153F RID: 5439
			// (set) Token: 0x06002E88 RID: 11912 RVA: 0x0005446C File Offset: 0x0005266C
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001540 RID: 5440
			// (set) Token: 0x06002E89 RID: 11913 RVA: 0x00054484 File Offset: 0x00052684
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17001541 RID: 5441
			// (set) Token: 0x06002E8A RID: 11914 RVA: 0x0005449C File Offset: 0x0005269C
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x17001542 RID: 5442
			// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000544B4 File Offset: 0x000526B4
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001543 RID: 5443
			// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000544C7 File Offset: 0x000526C7
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17001544 RID: 5444
			// (set) Token: 0x06002E8D RID: 11917 RVA: 0x000544DF File Offset: 0x000526DF
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17001545 RID: 5445
			// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000544F7 File Offset: 0x000526F7
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17001546 RID: 5446
			// (set) Token: 0x06002E8F RID: 11919 RVA: 0x0005450F File Offset: 0x0005270F
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17001547 RID: 5447
			// (set) Token: 0x06002E90 RID: 11920 RVA: 0x00054527 File Offset: 0x00052727
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x17001548 RID: 5448
			// (set) Token: 0x06002E91 RID: 11921 RVA: 0x0005453F File Offset: 0x0005273F
			public virtual bool UseAdvancedValidation
			{
				set
				{
					base.PowerSharpParameters["UseAdvancedValidation"] = value;
				}
			}

			// Token: 0x17001549 RID: 5449
			// (set) Token: 0x06002E92 RID: 11922 RVA: 0x00054557 File Offset: 0x00052757
			public virtual DatabaseIdParameter SourcePublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["SourcePublicFolderDatabase"] = value;
				}
			}

			// Token: 0x1700154A RID: 5450
			// (set) Token: 0x06002E93 RID: 11923 RVA: 0x0005456A File Offset: 0x0005276A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700154B RID: 5451
			// (set) Token: 0x06002E94 RID: 11924 RVA: 0x00054588 File Offset: 0x00052788
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700154C RID: 5452
			// (set) Token: 0x06002E95 RID: 11925 RVA: 0x000545A6 File Offset: 0x000527A6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700154D RID: 5453
			// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000545B9 File Offset: 0x000527B9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700154E RID: 5454
			// (set) Token: 0x06002E97 RID: 11927 RVA: 0x000545D1 File Offset: 0x000527D1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700154F RID: 5455
			// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000545E9 File Offset: 0x000527E9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001550 RID: 5456
			// (set) Token: 0x06002E99 RID: 11929 RVA: 0x00054601 File Offset: 0x00052801
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001551 RID: 5457
			// (set) Token: 0x06002E9A RID: 11930 RVA: 0x00054619 File Offset: 0x00052819
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000281 RID: 641
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001552 RID: 5458
			// (set) Token: 0x06002E9C RID: 11932 RVA: 0x00054639 File Offset: 0x00052839
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x17001553 RID: 5459
			// (set) Token: 0x06002E9D RID: 11933 RVA: 0x00054657 File Offset: 0x00052857
			public virtual bool? AllowIncrementalSyncs
			{
				set
				{
					base.PowerSharpParameters["AllowIncrementalSyncs"] = value;
				}
			}

			// Token: 0x17001554 RID: 5460
			// (set) Token: 0x06002E9E RID: 11934 RVA: 0x0005466F File Offset: 0x0005286F
			public virtual int? AutoRetryCount
			{
				set
				{
					base.PowerSharpParameters["AutoRetryCount"] = value;
				}
			}

			// Token: 0x17001555 RID: 5461
			// (set) Token: 0x06002E9F RID: 11935 RVA: 0x00054687 File Offset: 0x00052887
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x17001556 RID: 5462
			// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x0005469F File Offset: 0x0005289F
			public virtual bool AllowUnknownColumnsInCsv
			{
				set
				{
					base.PowerSharpParameters["AllowUnknownColumnsInCsv"] = value;
				}
			}

			// Token: 0x17001557 RID: 5463
			// (set) Token: 0x06002EA1 RID: 11937 RVA: 0x000546B7 File Offset: 0x000528B7
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001558 RID: 5464
			// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000546CA File Offset: 0x000528CA
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17001559 RID: 5465
			// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000546E2 File Offset: 0x000528E2
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700155A RID: 5466
			// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000546FA File Offset: 0x000528FA
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700155B RID: 5467
			// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x00054712 File Offset: 0x00052912
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700155C RID: 5468
			// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x0005472A File Offset: 0x0005292A
			public virtual TimeSpan? ReportInterval
			{
				set
				{
					base.PowerSharpParameters["ReportInterval"] = value;
				}
			}

			// Token: 0x1700155D RID: 5469
			// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x00054742 File Offset: 0x00052942
			public virtual bool UseAdvancedValidation
			{
				set
				{
					base.PowerSharpParameters["UseAdvancedValidation"] = value;
				}
			}

			// Token: 0x1700155E RID: 5470
			// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x0005475A File Offset: 0x0005295A
			public virtual DatabaseIdParameter SourcePublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["SourcePublicFolderDatabase"] = value;
				}
			}

			// Token: 0x1700155F RID: 5471
			// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x0005476D File Offset: 0x0005296D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001560 RID: 5472
			// (set) Token: 0x06002EAA RID: 11946 RVA: 0x0005478B File Offset: 0x0005298B
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001561 RID: 5473
			// (set) Token: 0x06002EAB RID: 11947 RVA: 0x000547A9 File Offset: 0x000529A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001562 RID: 5474
			// (set) Token: 0x06002EAC RID: 11948 RVA: 0x000547BC File Offset: 0x000529BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001563 RID: 5475
			// (set) Token: 0x06002EAD RID: 11949 RVA: 0x000547D4 File Offset: 0x000529D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001564 RID: 5476
			// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000547EC File Offset: 0x000529EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001565 RID: 5477
			// (set) Token: 0x06002EAF RID: 11951 RVA: 0x00054804 File Offset: 0x00052A04
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001566 RID: 5478
			// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x0005481C File Offset: 0x00052A1C
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
