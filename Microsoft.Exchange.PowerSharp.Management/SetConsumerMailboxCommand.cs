using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BE9 RID: 3049
	public class SetConsumerMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<ADUser>
	{
		// Token: 0x06009382 RID: 37762 RVA: 0x000D7372 File Offset: 0x000D5572
		private SetConsumerMailboxCommand() : base("Set-ConsumerMailbox")
		{
		}

		// Token: 0x06009383 RID: 37763 RVA: 0x000D737F File Offset: 0x000D557F
		public SetConsumerMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009384 RID: 37764 RVA: 0x000D738E File Offset: 0x000D558E
		public virtual SetConsumerMailboxCommand SetParameters(SetConsumerMailboxCommand.ConsumerPrimaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009385 RID: 37765 RVA: 0x000D7398 File Offset: 0x000D5598
		public virtual SetConsumerMailboxCommand SetParameters(SetConsumerMailboxCommand.ConsumerSecondaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009386 RID: 37766 RVA: 0x000D73A2 File Offset: 0x000D55A2
		public virtual SetConsumerMailboxCommand SetParameters(SetConsumerMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009387 RID: 37767 RVA: 0x000D73AC File Offset: 0x000D55AC
		public virtual SetConsumerMailboxCommand SetParameters(SetConsumerMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BEA RID: 3050
		public class ConsumerPrimaryMailboxParameters : ParametersBase
		{
			// Token: 0x17006769 RID: 26473
			// (set) Token: 0x06009388 RID: 37768 RVA: 0x000D73B6 File Offset: 0x000D55B6
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700676A RID: 26474
			// (set) Token: 0x06009389 RID: 37769 RVA: 0x000D73C9 File Offset: 0x000D55C9
			public virtual SwitchParameter MakeExoPrimary
			{
				set
				{
					base.PowerSharpParameters["MakeExoPrimary"] = value;
				}
			}

			// Token: 0x1700676B RID: 26475
			// (set) Token: 0x0600938A RID: 37770 RVA: 0x000D73E1 File Offset: 0x000D55E1
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x1700676C RID: 26476
			// (set) Token: 0x0600938B RID: 37771 RVA: 0x000D73F9 File Offset: 0x000D55F9
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x1700676D RID: 26477
			// (set) Token: 0x0600938C RID: 37772 RVA: 0x000D740C File Offset: 0x000D560C
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x1700676E RID: 26478
			// (set) Token: 0x0600938D RID: 37773 RVA: 0x000D741F File Offset: 0x000D561F
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x1700676F RID: 26479
			// (set) Token: 0x0600938E RID: 37774 RVA: 0x000D7432 File Offset: 0x000D5632
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x17006770 RID: 26480
			// (set) Token: 0x0600938F RID: 37775 RVA: 0x000D7445 File Offset: 0x000D5645
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x17006771 RID: 26481
			// (set) Token: 0x06009390 RID: 37776 RVA: 0x000D745D File Offset: 0x000D565D
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x17006772 RID: 26482
			// (set) Token: 0x06009391 RID: 37777 RVA: 0x000D7470 File Offset: 0x000D5670
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x17006773 RID: 26483
			// (set) Token: 0x06009392 RID: 37778 RVA: 0x000D7483 File Offset: 0x000D5683
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x17006774 RID: 26484
			// (set) Token: 0x06009393 RID: 37779 RVA: 0x000D7496 File Offset: 0x000D5696
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006775 RID: 26485
			// (set) Token: 0x06009394 RID: 37780 RVA: 0x000D74A9 File Offset: 0x000D56A9
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x17006776 RID: 26486
			// (set) Token: 0x06009395 RID: 37781 RVA: 0x000D74BC File Offset: 0x000D56BC
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x17006777 RID: 26487
			// (set) Token: 0x06009396 RID: 37782 RVA: 0x000D74CF File Offset: 0x000D56CF
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006778 RID: 26488
			// (set) Token: 0x06009397 RID: 37783 RVA: 0x000D74E2 File Offset: 0x000D56E2
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006779 RID: 26489
			// (set) Token: 0x06009398 RID: 37784 RVA: 0x000D74F5 File Offset: 0x000D56F5
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x1700677A RID: 26490
			// (set) Token: 0x06009399 RID: 37785 RVA: 0x000D7508 File Offset: 0x000D5708
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700677B RID: 26491
			// (set) Token: 0x0600939A RID: 37786 RVA: 0x000D751B File Offset: 0x000D571B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700677C RID: 26492
			// (set) Token: 0x0600939B RID: 37787 RVA: 0x000D752E File Offset: 0x000D572E
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x1700677D RID: 26493
			// (set) Token: 0x0600939C RID: 37788 RVA: 0x000D7541 File Offset: 0x000D5741
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x1700677E RID: 26494
			// (set) Token: 0x0600939D RID: 37789 RVA: 0x000D7559 File Offset: 0x000D5759
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700677F RID: 26495
			// (set) Token: 0x0600939E RID: 37790 RVA: 0x000D756C File Offset: 0x000D576C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006780 RID: 26496
			// (set) Token: 0x0600939F RID: 37791 RVA: 0x000D7584 File Offset: 0x000D5784
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006781 RID: 26497
			// (set) Token: 0x060093A0 RID: 37792 RVA: 0x000D759C File Offset: 0x000D579C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006782 RID: 26498
			// (set) Token: 0x060093A1 RID: 37793 RVA: 0x000D75B4 File Offset: 0x000D57B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006783 RID: 26499
			// (set) Token: 0x060093A2 RID: 37794 RVA: 0x000D75CC File Offset: 0x000D57CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BEB RID: 3051
		public class ConsumerSecondaryMailboxParameters : ParametersBase
		{
			// Token: 0x17006784 RID: 26500
			// (set) Token: 0x060093A4 RID: 37796 RVA: 0x000D75EC File Offset: 0x000D57EC
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006785 RID: 26501
			// (set) Token: 0x060093A5 RID: 37797 RVA: 0x000D75FF File Offset: 0x000D57FF
			public virtual SwitchParameter MakeExoSecondary
			{
				set
				{
					base.PowerSharpParameters["MakeExoSecondary"] = value;
				}
			}

			// Token: 0x17006786 RID: 26502
			// (set) Token: 0x060093A6 RID: 37798 RVA: 0x000D7617 File Offset: 0x000D5817
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x17006787 RID: 26503
			// (set) Token: 0x060093A7 RID: 37799 RVA: 0x000D762F File Offset: 0x000D582F
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x17006788 RID: 26504
			// (set) Token: 0x060093A8 RID: 37800 RVA: 0x000D7642 File Offset: 0x000D5842
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x17006789 RID: 26505
			// (set) Token: 0x060093A9 RID: 37801 RVA: 0x000D7655 File Offset: 0x000D5855
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x1700678A RID: 26506
			// (set) Token: 0x060093AA RID: 37802 RVA: 0x000D7668 File Offset: 0x000D5868
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x1700678B RID: 26507
			// (set) Token: 0x060093AB RID: 37803 RVA: 0x000D767B File Offset: 0x000D587B
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x1700678C RID: 26508
			// (set) Token: 0x060093AC RID: 37804 RVA: 0x000D7693 File Offset: 0x000D5893
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x1700678D RID: 26509
			// (set) Token: 0x060093AD RID: 37805 RVA: 0x000D76A6 File Offset: 0x000D58A6
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x1700678E RID: 26510
			// (set) Token: 0x060093AE RID: 37806 RVA: 0x000D76B9 File Offset: 0x000D58B9
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x1700678F RID: 26511
			// (set) Token: 0x060093AF RID: 37807 RVA: 0x000D76CC File Offset: 0x000D58CC
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006790 RID: 26512
			// (set) Token: 0x060093B0 RID: 37808 RVA: 0x000D76DF File Offset: 0x000D58DF
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x17006791 RID: 26513
			// (set) Token: 0x060093B1 RID: 37809 RVA: 0x000D76F2 File Offset: 0x000D58F2
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x17006792 RID: 26514
			// (set) Token: 0x060093B2 RID: 37810 RVA: 0x000D7705 File Offset: 0x000D5905
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006793 RID: 26515
			// (set) Token: 0x060093B3 RID: 37811 RVA: 0x000D7718 File Offset: 0x000D5918
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006794 RID: 26516
			// (set) Token: 0x060093B4 RID: 37812 RVA: 0x000D772B File Offset: 0x000D592B
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17006795 RID: 26517
			// (set) Token: 0x060093B5 RID: 37813 RVA: 0x000D773E File Offset: 0x000D593E
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17006796 RID: 26518
			// (set) Token: 0x060093B6 RID: 37814 RVA: 0x000D7751 File Offset: 0x000D5951
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006797 RID: 26519
			// (set) Token: 0x060093B7 RID: 37815 RVA: 0x000D7764 File Offset: 0x000D5964
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x17006798 RID: 26520
			// (set) Token: 0x060093B8 RID: 37816 RVA: 0x000D7777 File Offset: 0x000D5977
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x17006799 RID: 26521
			// (set) Token: 0x060093B9 RID: 37817 RVA: 0x000D778F File Offset: 0x000D598F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700679A RID: 26522
			// (set) Token: 0x060093BA RID: 37818 RVA: 0x000D77A2 File Offset: 0x000D59A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700679B RID: 26523
			// (set) Token: 0x060093BB RID: 37819 RVA: 0x000D77BA File Offset: 0x000D59BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700679C RID: 26524
			// (set) Token: 0x060093BC RID: 37820 RVA: 0x000D77D2 File Offset: 0x000D59D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700679D RID: 26525
			// (set) Token: 0x060093BD RID: 37821 RVA: 0x000D77EA File Offset: 0x000D59EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700679E RID: 26526
			// (set) Token: 0x060093BE RID: 37822 RVA: 0x000D7802 File Offset: 0x000D5A02
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BEC RID: 3052
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700679F RID: 26527
			// (set) Token: 0x060093C0 RID: 37824 RVA: 0x000D7822 File Offset: 0x000D5A22
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170067A0 RID: 26528
			// (set) Token: 0x060093C1 RID: 37825 RVA: 0x000D7835 File Offset: 0x000D5A35
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x170067A1 RID: 26529
			// (set) Token: 0x060093C2 RID: 37826 RVA: 0x000D784D File Offset: 0x000D5A4D
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x170067A2 RID: 26530
			// (set) Token: 0x060093C3 RID: 37827 RVA: 0x000D7860 File Offset: 0x000D5A60
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x170067A3 RID: 26531
			// (set) Token: 0x060093C4 RID: 37828 RVA: 0x000D7873 File Offset: 0x000D5A73
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x170067A4 RID: 26532
			// (set) Token: 0x060093C5 RID: 37829 RVA: 0x000D7886 File Offset: 0x000D5A86
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x170067A5 RID: 26533
			// (set) Token: 0x060093C6 RID: 37830 RVA: 0x000D7899 File Offset: 0x000D5A99
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x170067A6 RID: 26534
			// (set) Token: 0x060093C7 RID: 37831 RVA: 0x000D78B1 File Offset: 0x000D5AB1
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x170067A7 RID: 26535
			// (set) Token: 0x060093C8 RID: 37832 RVA: 0x000D78C4 File Offset: 0x000D5AC4
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x170067A8 RID: 26536
			// (set) Token: 0x060093C9 RID: 37833 RVA: 0x000D78D7 File Offset: 0x000D5AD7
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x170067A9 RID: 26537
			// (set) Token: 0x060093CA RID: 37834 RVA: 0x000D78EA File Offset: 0x000D5AEA
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170067AA RID: 26538
			// (set) Token: 0x060093CB RID: 37835 RVA: 0x000D78FD File Offset: 0x000D5AFD
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x170067AB RID: 26539
			// (set) Token: 0x060093CC RID: 37836 RVA: 0x000D7910 File Offset: 0x000D5B10
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x170067AC RID: 26540
			// (set) Token: 0x060093CD RID: 37837 RVA: 0x000D7923 File Offset: 0x000D5B23
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170067AD RID: 26541
			// (set) Token: 0x060093CE RID: 37838 RVA: 0x000D7936 File Offset: 0x000D5B36
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170067AE RID: 26542
			// (set) Token: 0x060093CF RID: 37839 RVA: 0x000D7949 File Offset: 0x000D5B49
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170067AF RID: 26543
			// (set) Token: 0x060093D0 RID: 37840 RVA: 0x000D795C File Offset: 0x000D5B5C
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170067B0 RID: 26544
			// (set) Token: 0x060093D1 RID: 37841 RVA: 0x000D796F File Offset: 0x000D5B6F
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170067B1 RID: 26545
			// (set) Token: 0x060093D2 RID: 37842 RVA: 0x000D7982 File Offset: 0x000D5B82
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x170067B2 RID: 26546
			// (set) Token: 0x060093D3 RID: 37843 RVA: 0x000D7995 File Offset: 0x000D5B95
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x170067B3 RID: 26547
			// (set) Token: 0x060093D4 RID: 37844 RVA: 0x000D79AD File Offset: 0x000D5BAD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170067B4 RID: 26548
			// (set) Token: 0x060093D5 RID: 37845 RVA: 0x000D79C0 File Offset: 0x000D5BC0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170067B5 RID: 26549
			// (set) Token: 0x060093D6 RID: 37846 RVA: 0x000D79D8 File Offset: 0x000D5BD8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170067B6 RID: 26550
			// (set) Token: 0x060093D7 RID: 37847 RVA: 0x000D79F0 File Offset: 0x000D5BF0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170067B7 RID: 26551
			// (set) Token: 0x060093D8 RID: 37848 RVA: 0x000D7A08 File Offset: 0x000D5C08
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170067B8 RID: 26552
			// (set) Token: 0x060093D9 RID: 37849 RVA: 0x000D7A20 File Offset: 0x000D5C20
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BED RID: 3053
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170067B9 RID: 26553
			// (set) Token: 0x060093DB RID: 37851 RVA: 0x000D7A40 File Offset: 0x000D5C40
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x170067BA RID: 26554
			// (set) Token: 0x060093DC RID: 37852 RVA: 0x000D7A58 File Offset: 0x000D5C58
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x170067BB RID: 26555
			// (set) Token: 0x060093DD RID: 37853 RVA: 0x000D7A6B File Offset: 0x000D5C6B
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x170067BC RID: 26556
			// (set) Token: 0x060093DE RID: 37854 RVA: 0x000D7A7E File Offset: 0x000D5C7E
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x170067BD RID: 26557
			// (set) Token: 0x060093DF RID: 37855 RVA: 0x000D7A91 File Offset: 0x000D5C91
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x170067BE RID: 26558
			// (set) Token: 0x060093E0 RID: 37856 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x170067BF RID: 26559
			// (set) Token: 0x060093E1 RID: 37857 RVA: 0x000D7ABC File Offset: 0x000D5CBC
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x170067C0 RID: 26560
			// (set) Token: 0x060093E2 RID: 37858 RVA: 0x000D7ACF File Offset: 0x000D5CCF
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x170067C1 RID: 26561
			// (set) Token: 0x060093E3 RID: 37859 RVA: 0x000D7AE2 File Offset: 0x000D5CE2
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x170067C2 RID: 26562
			// (set) Token: 0x060093E4 RID: 37860 RVA: 0x000D7AF5 File Offset: 0x000D5CF5
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x170067C3 RID: 26563
			// (set) Token: 0x060093E5 RID: 37861 RVA: 0x000D7B08 File Offset: 0x000D5D08
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x170067C4 RID: 26564
			// (set) Token: 0x060093E6 RID: 37862 RVA: 0x000D7B1B File Offset: 0x000D5D1B
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x170067C5 RID: 26565
			// (set) Token: 0x060093E7 RID: 37863 RVA: 0x000D7B2E File Offset: 0x000D5D2E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170067C6 RID: 26566
			// (set) Token: 0x060093E8 RID: 37864 RVA: 0x000D7B41 File Offset: 0x000D5D41
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170067C7 RID: 26567
			// (set) Token: 0x060093E9 RID: 37865 RVA: 0x000D7B54 File Offset: 0x000D5D54
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x170067C8 RID: 26568
			// (set) Token: 0x060093EA RID: 37866 RVA: 0x000D7B67 File Offset: 0x000D5D67
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170067C9 RID: 26569
			// (set) Token: 0x060093EB RID: 37867 RVA: 0x000D7B7A File Offset: 0x000D5D7A
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170067CA RID: 26570
			// (set) Token: 0x060093EC RID: 37868 RVA: 0x000D7B8D File Offset: 0x000D5D8D
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x170067CB RID: 26571
			// (set) Token: 0x060093ED RID: 37869 RVA: 0x000D7BA0 File Offset: 0x000D5DA0
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x170067CC RID: 26572
			// (set) Token: 0x060093EE RID: 37870 RVA: 0x000D7BB8 File Offset: 0x000D5DB8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170067CD RID: 26573
			// (set) Token: 0x060093EF RID: 37871 RVA: 0x000D7BCB File Offset: 0x000D5DCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170067CE RID: 26574
			// (set) Token: 0x060093F0 RID: 37872 RVA: 0x000D7BE3 File Offset: 0x000D5DE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170067CF RID: 26575
			// (set) Token: 0x060093F1 RID: 37873 RVA: 0x000D7BFB File Offset: 0x000D5DFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170067D0 RID: 26576
			// (set) Token: 0x060093F2 RID: 37874 RVA: 0x000D7C13 File Offset: 0x000D5E13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170067D1 RID: 26577
			// (set) Token: 0x060093F3 RID: 37875 RVA: 0x000D7C2B File Offset: 0x000D5E2B
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
