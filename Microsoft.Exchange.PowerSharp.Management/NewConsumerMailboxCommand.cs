using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BE1 RID: 3041
	public class NewConsumerMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600930C RID: 37644 RVA: 0x000D6A4C File Offset: 0x000D4C4C
		private NewConsumerMailboxCommand() : base("New-ConsumerMailbox")
		{
		}

		// Token: 0x0600930D RID: 37645 RVA: 0x000D6A59 File Offset: 0x000D4C59
		public NewConsumerMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600930E RID: 37646 RVA: 0x000D6A68 File Offset: 0x000D4C68
		public virtual NewConsumerMailboxCommand SetParameters(NewConsumerMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x000D6A72 File Offset: 0x000D4C72
		public virtual NewConsumerMailboxCommand SetParameters(NewConsumerMailboxCommand.ConsumerPrimaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x000D6A7C File Offset: 0x000D4C7C
		public virtual NewConsumerMailboxCommand SetParameters(NewConsumerMailboxCommand.ConsumerSecondaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BE2 RID: 3042
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006703 RID: 26371
			// (set) Token: 0x06009311 RID: 37649 RVA: 0x000D6A86 File Offset: 0x000D4C86
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17006704 RID: 26372
			// (set) Token: 0x06009312 RID: 37650 RVA: 0x000D6A99 File Offset: 0x000D4C99
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006705 RID: 26373
			// (set) Token: 0x06009313 RID: 37651 RVA: 0x000D6AAC File Offset: 0x000D4CAC
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x17006706 RID: 26374
			// (set) Token: 0x06009314 RID: 37652 RVA: 0x000D6AC4 File Offset: 0x000D4CC4
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x17006707 RID: 26375
			// (set) Token: 0x06009315 RID: 37653 RVA: 0x000D6AD7 File Offset: 0x000D4CD7
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x17006708 RID: 26376
			// (set) Token: 0x06009316 RID: 37654 RVA: 0x000D6AEA File Offset: 0x000D4CEA
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x17006709 RID: 26377
			// (set) Token: 0x06009317 RID: 37655 RVA: 0x000D6AFD File Offset: 0x000D4CFD
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x1700670A RID: 26378
			// (set) Token: 0x06009318 RID: 37656 RVA: 0x000D6B10 File Offset: 0x000D4D10
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x1700670B RID: 26379
			// (set) Token: 0x06009319 RID: 37657 RVA: 0x000D6B28 File Offset: 0x000D4D28
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x1700670C RID: 26380
			// (set) Token: 0x0600931A RID: 37658 RVA: 0x000D6B3B File Offset: 0x000D4D3B
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x1700670D RID: 26381
			// (set) Token: 0x0600931B RID: 37659 RVA: 0x000D6B4E File Offset: 0x000D4D4E
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x1700670E RID: 26382
			// (set) Token: 0x0600931C RID: 37660 RVA: 0x000D6B61 File Offset: 0x000D4D61
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700670F RID: 26383
			// (set) Token: 0x0600931D RID: 37661 RVA: 0x000D6B74 File Offset: 0x000D4D74
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x17006710 RID: 26384
			// (set) Token: 0x0600931E RID: 37662 RVA: 0x000D6B87 File Offset: 0x000D4D87
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x17006711 RID: 26385
			// (set) Token: 0x0600931F RID: 37663 RVA: 0x000D6B9A File Offset: 0x000D4D9A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006712 RID: 26386
			// (set) Token: 0x06009320 RID: 37664 RVA: 0x000D6BAD File Offset: 0x000D4DAD
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006713 RID: 26387
			// (set) Token: 0x06009321 RID: 37665 RVA: 0x000D6BC0 File Offset: 0x000D4DC0
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17006714 RID: 26388
			// (set) Token: 0x06009322 RID: 37666 RVA: 0x000D6BD3 File Offset: 0x000D4DD3
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006715 RID: 26389
			// (set) Token: 0x06009323 RID: 37667 RVA: 0x000D6BE6 File Offset: 0x000D4DE6
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x17006716 RID: 26390
			// (set) Token: 0x06009324 RID: 37668 RVA: 0x000D6BF9 File Offset: 0x000D4DF9
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x17006717 RID: 26391
			// (set) Token: 0x06009325 RID: 37669 RVA: 0x000D6C11 File Offset: 0x000D4E11
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006718 RID: 26392
			// (set) Token: 0x06009326 RID: 37670 RVA: 0x000D6C29 File Offset: 0x000D4E29
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006719 RID: 26393
			// (set) Token: 0x06009327 RID: 37671 RVA: 0x000D6C41 File Offset: 0x000D4E41
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700671A RID: 26394
			// (set) Token: 0x06009328 RID: 37672 RVA: 0x000D6C59 File Offset: 0x000D4E59
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700671B RID: 26395
			// (set) Token: 0x06009329 RID: 37673 RVA: 0x000D6C71 File Offset: 0x000D4E71
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BE3 RID: 3043
		public class ConsumerPrimaryMailboxParameters : ParametersBase
		{
			// Token: 0x1700671C RID: 26396
			// (set) Token: 0x0600932B RID: 37675 RVA: 0x000D6C91 File Offset: 0x000D4E91
			public virtual SwitchParameter MakeExoPrimary
			{
				set
				{
					base.PowerSharpParameters["MakeExoPrimary"] = value;
				}
			}

			// Token: 0x1700671D RID: 26397
			// (set) Token: 0x0600932C RID: 37676 RVA: 0x000D6CA9 File Offset: 0x000D4EA9
			public virtual SwitchParameter SkipMigration
			{
				set
				{
					base.PowerSharpParameters["SkipMigration"] = value;
				}
			}

			// Token: 0x1700671E RID: 26398
			// (set) Token: 0x0600932D RID: 37677 RVA: 0x000D6CC1 File Offset: 0x000D4EC1
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700671F RID: 26399
			// (set) Token: 0x0600932E RID: 37678 RVA: 0x000D6CD4 File Offset: 0x000D4ED4
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006720 RID: 26400
			// (set) Token: 0x0600932F RID: 37679 RVA: 0x000D6CE7 File Offset: 0x000D4EE7
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x17006721 RID: 26401
			// (set) Token: 0x06009330 RID: 37680 RVA: 0x000D6CFF File Offset: 0x000D4EFF
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x17006722 RID: 26402
			// (set) Token: 0x06009331 RID: 37681 RVA: 0x000D6D12 File Offset: 0x000D4F12
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x17006723 RID: 26403
			// (set) Token: 0x06009332 RID: 37682 RVA: 0x000D6D25 File Offset: 0x000D4F25
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x17006724 RID: 26404
			// (set) Token: 0x06009333 RID: 37683 RVA: 0x000D6D38 File Offset: 0x000D4F38
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x17006725 RID: 26405
			// (set) Token: 0x06009334 RID: 37684 RVA: 0x000D6D4B File Offset: 0x000D4F4B
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x17006726 RID: 26406
			// (set) Token: 0x06009335 RID: 37685 RVA: 0x000D6D63 File Offset: 0x000D4F63
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x17006727 RID: 26407
			// (set) Token: 0x06009336 RID: 37686 RVA: 0x000D6D76 File Offset: 0x000D4F76
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x17006728 RID: 26408
			// (set) Token: 0x06009337 RID: 37687 RVA: 0x000D6D89 File Offset: 0x000D4F89
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x17006729 RID: 26409
			// (set) Token: 0x06009338 RID: 37688 RVA: 0x000D6D9C File Offset: 0x000D4F9C
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700672A RID: 26410
			// (set) Token: 0x06009339 RID: 37689 RVA: 0x000D6DAF File Offset: 0x000D4FAF
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x1700672B RID: 26411
			// (set) Token: 0x0600933A RID: 37690 RVA: 0x000D6DC2 File Offset: 0x000D4FC2
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x1700672C RID: 26412
			// (set) Token: 0x0600933B RID: 37691 RVA: 0x000D6DD5 File Offset: 0x000D4FD5
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700672D RID: 26413
			// (set) Token: 0x0600933C RID: 37692 RVA: 0x000D6DE8 File Offset: 0x000D4FE8
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700672E RID: 26414
			// (set) Token: 0x0600933D RID: 37693 RVA: 0x000D6DFB File Offset: 0x000D4FFB
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700672F RID: 26415
			// (set) Token: 0x0600933E RID: 37694 RVA: 0x000D6E0E File Offset: 0x000D500E
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006730 RID: 26416
			// (set) Token: 0x0600933F RID: 37695 RVA: 0x000D6E21 File Offset: 0x000D5021
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x17006731 RID: 26417
			// (set) Token: 0x06009340 RID: 37696 RVA: 0x000D6E34 File Offset: 0x000D5034
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x17006732 RID: 26418
			// (set) Token: 0x06009341 RID: 37697 RVA: 0x000D6E4C File Offset: 0x000D504C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006733 RID: 26419
			// (set) Token: 0x06009342 RID: 37698 RVA: 0x000D6E64 File Offset: 0x000D5064
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006734 RID: 26420
			// (set) Token: 0x06009343 RID: 37699 RVA: 0x000D6E7C File Offset: 0x000D507C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006735 RID: 26421
			// (set) Token: 0x06009344 RID: 37700 RVA: 0x000D6E94 File Offset: 0x000D5094
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006736 RID: 26422
			// (set) Token: 0x06009345 RID: 37701 RVA: 0x000D6EAC File Offset: 0x000D50AC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BE4 RID: 3044
		public class ConsumerSecondaryMailboxParameters : ParametersBase
		{
			// Token: 0x17006737 RID: 26423
			// (set) Token: 0x06009347 RID: 37703 RVA: 0x000D6ECC File Offset: 0x000D50CC
			public virtual SwitchParameter MakeExoSecondary
			{
				set
				{
					base.PowerSharpParameters["MakeExoSecondary"] = value;
				}
			}

			// Token: 0x17006738 RID: 26424
			// (set) Token: 0x06009348 RID: 37704 RVA: 0x000D6EE4 File Offset: 0x000D50E4
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17006739 RID: 26425
			// (set) Token: 0x06009349 RID: 37705 RVA: 0x000D6EF7 File Offset: 0x000D50F7
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700673A RID: 26426
			// (set) Token: 0x0600934A RID: 37706 RVA: 0x000D6F0A File Offset: 0x000D510A
			public virtual SwitchParameter Repair
			{
				set
				{
					base.PowerSharpParameters["Repair"] = value;
				}
			}

			// Token: 0x1700673B RID: 26427
			// (set) Token: 0x0600934B RID: 37707 RVA: 0x000D6F22 File Offset: 0x000D5122
			public virtual string Gender
			{
				set
				{
					base.PowerSharpParameters["Gender"] = value;
				}
			}

			// Token: 0x1700673C RID: 26428
			// (set) Token: 0x0600934C RID: 37708 RVA: 0x000D6F35 File Offset: 0x000D5135
			public virtual string Occupation
			{
				set
				{
					base.PowerSharpParameters["Occupation"] = value;
				}
			}

			// Token: 0x1700673D RID: 26429
			// (set) Token: 0x0600934D RID: 37709 RVA: 0x000D6F48 File Offset: 0x000D5148
			public virtual string Region
			{
				set
				{
					base.PowerSharpParameters["Region"] = value;
				}
			}

			// Token: 0x1700673E RID: 26430
			// (set) Token: 0x0600934E RID: 37710 RVA: 0x000D6F5B File Offset: 0x000D515B
			public virtual string Timezone
			{
				set
				{
					base.PowerSharpParameters["Timezone"] = value;
				}
			}

			// Token: 0x1700673F RID: 26431
			// (set) Token: 0x0600934F RID: 37711 RVA: 0x000D6F6E File Offset: 0x000D516E
			public virtual DateTime Birthdate
			{
				set
				{
					base.PowerSharpParameters["Birthdate"] = value;
				}
			}

			// Token: 0x17006740 RID: 26432
			// (set) Token: 0x06009350 RID: 37712 RVA: 0x000D6F86 File Offset: 0x000D5186
			public virtual string BirthdayPrecision
			{
				set
				{
					base.PowerSharpParameters["BirthdayPrecision"] = value;
				}
			}

			// Token: 0x17006741 RID: 26433
			// (set) Token: 0x06009351 RID: 37713 RVA: 0x000D6F99 File Offset: 0x000D5199
			public virtual string NameVersion
			{
				set
				{
					base.PowerSharpParameters["NameVersion"] = value;
				}
			}

			// Token: 0x17006742 RID: 26434
			// (set) Token: 0x06009352 RID: 37714 RVA: 0x000D6FAC File Offset: 0x000D51AC
			public virtual string AlternateSupportEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["AlternateSupportEmailAddresses"] = value;
				}
			}

			// Token: 0x17006743 RID: 26435
			// (set) Token: 0x06009353 RID: 37715 RVA: 0x000D6FBF File Offset: 0x000D51BF
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006744 RID: 26436
			// (set) Token: 0x06009354 RID: 37716 RVA: 0x000D6FD2 File Offset: 0x000D51D2
			public virtual string OptInUser
			{
				set
				{
					base.PowerSharpParameters["OptInUser"] = value;
				}
			}

			// Token: 0x17006745 RID: 26437
			// (set) Token: 0x06009355 RID: 37717 RVA: 0x000D6FE5 File Offset: 0x000D51E5
			public virtual string MigrationDryRun
			{
				set
				{
					base.PowerSharpParameters["MigrationDryRun"] = value;
				}
			}

			// Token: 0x17006746 RID: 26438
			// (set) Token: 0x06009356 RID: 37718 RVA: 0x000D6FF8 File Offset: 0x000D51F8
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006747 RID: 26439
			// (set) Token: 0x06009357 RID: 37719 RVA: 0x000D700B File Offset: 0x000D520B
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006748 RID: 26440
			// (set) Token: 0x06009358 RID: 37720 RVA: 0x000D701E File Offset: 0x000D521E
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17006749 RID: 26441
			// (set) Token: 0x06009359 RID: 37721 RVA: 0x000D7031 File Offset: 0x000D5231
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700674A RID: 26442
			// (set) Token: 0x0600935A RID: 37722 RVA: 0x000D7044 File Offset: 0x000D5244
			public virtual MultiValuedProperty<int> LocaleID
			{
				set
				{
					base.PowerSharpParameters["LocaleID"] = value;
				}
			}

			// Token: 0x1700674B RID: 26443
			// (set) Token: 0x0600935B RID: 37723 RVA: 0x000D7057 File Offset: 0x000D5257
			public virtual bool FblEnabled
			{
				set
				{
					base.PowerSharpParameters["FblEnabled"] = value;
				}
			}

			// Token: 0x1700674C RID: 26444
			// (set) Token: 0x0600935C RID: 37724 RVA: 0x000D706F File Offset: 0x000D526F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700674D RID: 26445
			// (set) Token: 0x0600935D RID: 37725 RVA: 0x000D7087 File Offset: 0x000D5287
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700674E RID: 26446
			// (set) Token: 0x0600935E RID: 37726 RVA: 0x000D709F File Offset: 0x000D529F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700674F RID: 26447
			// (set) Token: 0x0600935F RID: 37727 RVA: 0x000D70B7 File Offset: 0x000D52B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006750 RID: 26448
			// (set) Token: 0x06009360 RID: 37728 RVA: 0x000D70CF File Offset: 0x000D52CF
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
