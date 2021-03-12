using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000823 RID: 2083
	public class SetOrganizationFlagsCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeConfigurationUnit>
	{
		// Token: 0x060067CD RID: 26573 RVA: 0x0009E240 File Offset: 0x0009C440
		private SetOrganizationFlagsCommand() : base("Set-OrganizationFlags")
		{
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x0009E24D File Offset: 0x0009C44D
		public SetOrganizationFlagsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x0009E25C File Offset: 0x0009C45C
		public virtual SetOrganizationFlagsCommand SetParameters(SetOrganizationFlagsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x0009E266 File Offset: 0x0009C466
		public virtual SetOrganizationFlagsCommand SetParameters(SetOrganizationFlagsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000824 RID: 2084
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004340 RID: 17216
			// (set) Token: 0x060067D1 RID: 26577 RVA: 0x0009E270 File Offset: 0x0009C470
			public virtual SwitchParameter IsFederated
			{
				set
				{
					base.PowerSharpParameters["IsFederated"] = value;
				}
			}

			// Token: 0x17004341 RID: 17217
			// (set) Token: 0x060067D2 RID: 26578 RVA: 0x0009E288 File Offset: 0x0009C488
			public virtual SwitchParameter HideAdminAccessWarning
			{
				set
				{
					base.PowerSharpParameters["HideAdminAccessWarning"] = value;
				}
			}

			// Token: 0x17004342 RID: 17218
			// (set) Token: 0x060067D3 RID: 26579 RVA: 0x0009E2A0 File Offset: 0x0009C4A0
			public virtual SwitchParameter SkipToUAndParentalControlCheck
			{
				set
				{
					base.PowerSharpParameters["SkipToUAndParentalControlCheck"] = value;
				}
			}

			// Token: 0x17004343 RID: 17219
			// (set) Token: 0x060067D4 RID: 26580 RVA: 0x0009E2B8 File Offset: 0x0009C4B8
			public virtual SwitchParameter IsUpgradingOrganization
			{
				set
				{
					base.PowerSharpParameters["IsUpgradingOrganization"] = value;
				}
			}

			// Token: 0x17004344 RID: 17220
			// (set) Token: 0x060067D5 RID: 26581 RVA: 0x0009E2D0 File Offset: 0x0009C4D0
			public virtual SwitchParameter IsPilotingOrganization
			{
				set
				{
					base.PowerSharpParameters["IsPilotingOrganization"] = value;
				}
			}

			// Token: 0x17004345 RID: 17221
			// (set) Token: 0x060067D6 RID: 26582 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
			public virtual SwitchParameter IsTemplateTenant
			{
				set
				{
					base.PowerSharpParameters["IsTemplateTenant"] = value;
				}
			}

			// Token: 0x17004346 RID: 17222
			// (set) Token: 0x060067D7 RID: 26583 RVA: 0x0009E300 File Offset: 0x0009C500
			public virtual SwitchParameter IsUpgradeOperationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsUpgradeOperationInProgress"] = value;
				}
			}

			// Token: 0x17004347 RID: 17223
			// (set) Token: 0x060067D8 RID: 26584 RVA: 0x0009E318 File Offset: 0x0009C518
			public virtual SwitchParameter SMTPAddressCheckWithAcceptedDomain
			{
				set
				{
					base.PowerSharpParameters["SMTPAddressCheckWithAcceptedDomain"] = value;
				}
			}

			// Token: 0x17004348 RID: 17224
			// (set) Token: 0x060067D9 RID: 26585 RVA: 0x0009E330 File Offset: 0x0009C530
			public virtual SwitchParameter IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x17004349 RID: 17225
			// (set) Token: 0x060067DA RID: 26586 RVA: 0x0009E348 File Offset: 0x0009C548
			public virtual SwitchParameter IsTenantAccessBlocked
			{
				set
				{
					base.PowerSharpParameters["IsTenantAccessBlocked"] = value;
				}
			}

			// Token: 0x1700434A RID: 17226
			// (set) Token: 0x060067DB RID: 26587 RVA: 0x0009E360 File Offset: 0x0009C560
			public virtual SwitchParameter AllowDeleteOfExternalIdentityUponRemove
			{
				set
				{
					base.PowerSharpParameters["AllowDeleteOfExternalIdentityUponRemove"] = value;
				}
			}

			// Token: 0x1700434B RID: 17227
			// (set) Token: 0x060067DC RID: 26588 RVA: 0x0009E378 File Offset: 0x0009C578
			public virtual SwitchParameter UseServicePlanAsCounterInstanceName
			{
				set
				{
					base.PowerSharpParameters["UseServicePlanAsCounterInstanceName"] = value;
				}
			}

			// Token: 0x1700434C RID: 17228
			// (set) Token: 0x060067DD RID: 26589 RVA: 0x0009E390 File Offset: 0x0009C590
			public virtual SoftDeletedFeatureStatusFlags SoftDeletedFeatureStatus
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedFeatureStatus"] = value;
				}
			}

			// Token: 0x1700434D RID: 17229
			// (set) Token: 0x060067DE RID: 26590 RVA: 0x0009E3A8 File Offset: 0x0009C5A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700434E RID: 17230
			// (set) Token: 0x060067DF RID: 26591 RVA: 0x0009E3BB File Offset: 0x0009C5BB
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x1700434F RID: 17231
			// (set) Token: 0x060067E0 RID: 26592 RVA: 0x0009E3D3 File Offset: 0x0009C5D3
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17004350 RID: 17232
			// (set) Token: 0x060067E1 RID: 26593 RVA: 0x0009E3EB File Offset: 0x0009C5EB
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17004351 RID: 17233
			// (set) Token: 0x060067E2 RID: 26594 RVA: 0x0009E403 File Offset: 0x0009C603
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17004352 RID: 17234
			// (set) Token: 0x060067E3 RID: 26595 RVA: 0x0009E416 File Offset: 0x0009C616
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17004353 RID: 17235
			// (set) Token: 0x060067E4 RID: 26596 RVA: 0x0009E429 File Offset: 0x0009C629
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17004354 RID: 17236
			// (set) Token: 0x060067E5 RID: 26597 RVA: 0x0009E441 File Offset: 0x0009C641
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17004355 RID: 17237
			// (set) Token: 0x060067E6 RID: 26598 RVA: 0x0009E459 File Offset: 0x0009C659
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17004356 RID: 17238
			// (set) Token: 0x060067E7 RID: 26599 RVA: 0x0009E471 File Offset: 0x0009C671
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17004357 RID: 17239
			// (set) Token: 0x060067E8 RID: 26600 RVA: 0x0009E489 File Offset: 0x0009C689
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17004358 RID: 17240
			// (set) Token: 0x060067E9 RID: 26601 RVA: 0x0009E4A1 File Offset: 0x0009C6A1
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17004359 RID: 17241
			// (set) Token: 0x060067EA RID: 26602 RVA: 0x0009E4B9 File Offset: 0x0009C6B9
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700435A RID: 17242
			// (set) Token: 0x060067EB RID: 26603 RVA: 0x0009E4D1 File Offset: 0x0009C6D1
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700435B RID: 17243
			// (set) Token: 0x060067EC RID: 26604 RVA: 0x0009E4E9 File Offset: 0x0009C6E9
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x1700435C RID: 17244
			// (set) Token: 0x060067ED RID: 26605 RVA: 0x0009E4FC File Offset: 0x0009C6FC
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x1700435D RID: 17245
			// (set) Token: 0x060067EE RID: 26606 RVA: 0x0009E50F File Offset: 0x0009C70F
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x1700435E RID: 17246
			// (set) Token: 0x060067EF RID: 26607 RVA: 0x0009E522 File Offset: 0x0009C722
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x1700435F RID: 17247
			// (set) Token: 0x060067F0 RID: 26608 RVA: 0x0009E535 File Offset: 0x0009C735
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17004360 RID: 17248
			// (set) Token: 0x060067F1 RID: 26609 RVA: 0x0009E548 File Offset: 0x0009C748
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004361 RID: 17249
			// (set) Token: 0x060067F2 RID: 26610 RVA: 0x0009E560 File Offset: 0x0009C760
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004362 RID: 17250
			// (set) Token: 0x060067F3 RID: 26611 RVA: 0x0009E578 File Offset: 0x0009C778
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004363 RID: 17251
			// (set) Token: 0x060067F4 RID: 26612 RVA: 0x0009E590 File Offset: 0x0009C790
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004364 RID: 17252
			// (set) Token: 0x060067F5 RID: 26613 RVA: 0x0009E5A8 File Offset: 0x0009C7A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000825 RID: 2085
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004365 RID: 17253
			// (set) Token: 0x060067F7 RID: 26615 RVA: 0x0009E5C8 File Offset: 0x0009C7C8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004366 RID: 17254
			// (set) Token: 0x060067F8 RID: 26616 RVA: 0x0009E5E6 File Offset: 0x0009C7E6
			public virtual SwitchParameter IsFederated
			{
				set
				{
					base.PowerSharpParameters["IsFederated"] = value;
				}
			}

			// Token: 0x17004367 RID: 17255
			// (set) Token: 0x060067F9 RID: 26617 RVA: 0x0009E5FE File Offset: 0x0009C7FE
			public virtual SwitchParameter HideAdminAccessWarning
			{
				set
				{
					base.PowerSharpParameters["HideAdminAccessWarning"] = value;
				}
			}

			// Token: 0x17004368 RID: 17256
			// (set) Token: 0x060067FA RID: 26618 RVA: 0x0009E616 File Offset: 0x0009C816
			public virtual SwitchParameter SkipToUAndParentalControlCheck
			{
				set
				{
					base.PowerSharpParameters["SkipToUAndParentalControlCheck"] = value;
				}
			}

			// Token: 0x17004369 RID: 17257
			// (set) Token: 0x060067FB RID: 26619 RVA: 0x0009E62E File Offset: 0x0009C82E
			public virtual SwitchParameter IsUpgradingOrganization
			{
				set
				{
					base.PowerSharpParameters["IsUpgradingOrganization"] = value;
				}
			}

			// Token: 0x1700436A RID: 17258
			// (set) Token: 0x060067FC RID: 26620 RVA: 0x0009E646 File Offset: 0x0009C846
			public virtual SwitchParameter IsPilotingOrganization
			{
				set
				{
					base.PowerSharpParameters["IsPilotingOrganization"] = value;
				}
			}

			// Token: 0x1700436B RID: 17259
			// (set) Token: 0x060067FD RID: 26621 RVA: 0x0009E65E File Offset: 0x0009C85E
			public virtual SwitchParameter IsTemplateTenant
			{
				set
				{
					base.PowerSharpParameters["IsTemplateTenant"] = value;
				}
			}

			// Token: 0x1700436C RID: 17260
			// (set) Token: 0x060067FE RID: 26622 RVA: 0x0009E676 File Offset: 0x0009C876
			public virtual SwitchParameter IsUpgradeOperationInProgress
			{
				set
				{
					base.PowerSharpParameters["IsUpgradeOperationInProgress"] = value;
				}
			}

			// Token: 0x1700436D RID: 17261
			// (set) Token: 0x060067FF RID: 26623 RVA: 0x0009E68E File Offset: 0x0009C88E
			public virtual SwitchParameter SMTPAddressCheckWithAcceptedDomain
			{
				set
				{
					base.PowerSharpParameters["SMTPAddressCheckWithAcceptedDomain"] = value;
				}
			}

			// Token: 0x1700436E RID: 17262
			// (set) Token: 0x06006800 RID: 26624 RVA: 0x0009E6A6 File Offset: 0x0009C8A6
			public virtual SwitchParameter IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x1700436F RID: 17263
			// (set) Token: 0x06006801 RID: 26625 RVA: 0x0009E6BE File Offset: 0x0009C8BE
			public virtual SwitchParameter IsTenantAccessBlocked
			{
				set
				{
					base.PowerSharpParameters["IsTenantAccessBlocked"] = value;
				}
			}

			// Token: 0x17004370 RID: 17264
			// (set) Token: 0x06006802 RID: 26626 RVA: 0x0009E6D6 File Offset: 0x0009C8D6
			public virtual SwitchParameter AllowDeleteOfExternalIdentityUponRemove
			{
				set
				{
					base.PowerSharpParameters["AllowDeleteOfExternalIdentityUponRemove"] = value;
				}
			}

			// Token: 0x17004371 RID: 17265
			// (set) Token: 0x06006803 RID: 26627 RVA: 0x0009E6EE File Offset: 0x0009C8EE
			public virtual SwitchParameter UseServicePlanAsCounterInstanceName
			{
				set
				{
					base.PowerSharpParameters["UseServicePlanAsCounterInstanceName"] = value;
				}
			}

			// Token: 0x17004372 RID: 17266
			// (set) Token: 0x06006804 RID: 26628 RVA: 0x0009E706 File Offset: 0x0009C906
			public virtual SoftDeletedFeatureStatusFlags SoftDeletedFeatureStatus
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedFeatureStatus"] = value;
				}
			}

			// Token: 0x17004373 RID: 17267
			// (set) Token: 0x06006805 RID: 26629 RVA: 0x0009E71E File Offset: 0x0009C91E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004374 RID: 17268
			// (set) Token: 0x06006806 RID: 26630 RVA: 0x0009E731 File Offset: 0x0009C931
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17004375 RID: 17269
			// (set) Token: 0x06006807 RID: 26631 RVA: 0x0009E749 File Offset: 0x0009C949
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17004376 RID: 17270
			// (set) Token: 0x06006808 RID: 26632 RVA: 0x0009E761 File Offset: 0x0009C961
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17004377 RID: 17271
			// (set) Token: 0x06006809 RID: 26633 RVA: 0x0009E779 File Offset: 0x0009C979
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17004378 RID: 17272
			// (set) Token: 0x0600680A RID: 26634 RVA: 0x0009E78C File Offset: 0x0009C98C
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17004379 RID: 17273
			// (set) Token: 0x0600680B RID: 26635 RVA: 0x0009E79F File Offset: 0x0009C99F
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x1700437A RID: 17274
			// (set) Token: 0x0600680C RID: 26636 RVA: 0x0009E7B7 File Offset: 0x0009C9B7
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x1700437B RID: 17275
			// (set) Token: 0x0600680D RID: 26637 RVA: 0x0009E7CF File Offset: 0x0009C9CF
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x1700437C RID: 17276
			// (set) Token: 0x0600680E RID: 26638 RVA: 0x0009E7E7 File Offset: 0x0009C9E7
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x1700437D RID: 17277
			// (set) Token: 0x0600680F RID: 26639 RVA: 0x0009E7FF File Offset: 0x0009C9FF
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x1700437E RID: 17278
			// (set) Token: 0x06006810 RID: 26640 RVA: 0x0009E817 File Offset: 0x0009CA17
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x1700437F RID: 17279
			// (set) Token: 0x06006811 RID: 26641 RVA: 0x0009E82F File Offset: 0x0009CA2F
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17004380 RID: 17280
			// (set) Token: 0x06006812 RID: 26642 RVA: 0x0009E847 File Offset: 0x0009CA47
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17004381 RID: 17281
			// (set) Token: 0x06006813 RID: 26643 RVA: 0x0009E85F File Offset: 0x0009CA5F
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17004382 RID: 17282
			// (set) Token: 0x06006814 RID: 26644 RVA: 0x0009E872 File Offset: 0x0009CA72
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17004383 RID: 17283
			// (set) Token: 0x06006815 RID: 26645 RVA: 0x0009E885 File Offset: 0x0009CA85
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17004384 RID: 17284
			// (set) Token: 0x06006816 RID: 26646 RVA: 0x0009E898 File Offset: 0x0009CA98
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17004385 RID: 17285
			// (set) Token: 0x06006817 RID: 26647 RVA: 0x0009E8AB File Offset: 0x0009CAAB
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17004386 RID: 17286
			// (set) Token: 0x06006818 RID: 26648 RVA: 0x0009E8BE File Offset: 0x0009CABE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004387 RID: 17287
			// (set) Token: 0x06006819 RID: 26649 RVA: 0x0009E8D6 File Offset: 0x0009CAD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004388 RID: 17288
			// (set) Token: 0x0600681A RID: 26650 RVA: 0x0009E8EE File Offset: 0x0009CAEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004389 RID: 17289
			// (set) Token: 0x0600681B RID: 26651 RVA: 0x0009E906 File Offset: 0x0009CB06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700438A RID: 17290
			// (set) Token: 0x0600681C RID: 26652 RVA: 0x0009E91E File Offset: 0x0009CB1E
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
