using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000BE RID: 190
	public class SetOrganizationCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeConfigurationUnit>
	{
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0003A8FD File Offset: 0x00038AFD
		private SetOrganizationCommand() : base("Set-Organization")
		{
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0003A90A File Offset: 0x00038B0A
		public SetOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0003A919 File Offset: 0x00038B19
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0003A923 File Offset: 0x00038B23
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.SharedConfigurationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0003A92D File Offset: 0x00038B2D
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.RemoveRelocationConstraintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0003A937 File Offset: 0x00038B37
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.SharedConfigurationInfoParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0003A941 File Offset: 0x00038B41
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.SharedConfigurationRemoveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0003A94B File Offset: 0x00038B4B
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.AddRelocationConstraintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0003A955 File Offset: 0x00038B55
		public virtual SetOrganizationCommand SetParameters(SetOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000BF RID: 191
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000531 RID: 1329
			// (set) Token: 0x06001AFD RID: 6909 RVA: 0x0003A95F File Offset: 0x00038B5F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000532 RID: 1330
			// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0003A97D File Offset: 0x00038B7D
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x17000533 RID: 1331
			// (set) Token: 0x06001AFF RID: 6911 RVA: 0x0003A995 File Offset: 0x00038B95
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x17000534 RID: 1332
			// (set) Token: 0x06001B00 RID: 6912 RVA: 0x0003A9AD File Offset: 0x00038BAD
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x17000535 RID: 1333
			// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0003A9C5 File Offset: 0x00038BC5
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x17000536 RID: 1334
			// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0003A9DD File Offset: 0x00038BDD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0003A9F5 File Offset: 0x00038BF5
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x17000538 RID: 1336
			// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0003AA08 File Offset: 0x00038C08
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x17000539 RID: 1337
			// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0003AA26 File Offset: 0x00038C26
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x1700053A RID: 1338
			// (set) Token: 0x06001B06 RID: 6918 RVA: 0x0003AA3E File Offset: 0x00038C3E
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x1700053B RID: 1339
			// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0003AA56 File Offset: 0x00038C56
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (set) Token: 0x06001B08 RID: 6920 RVA: 0x0003AA6E File Offset: 0x00038C6E
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x1700053D RID: 1341
			// (set) Token: 0x06001B09 RID: 6921 RVA: 0x0003AA81 File Offset: 0x00038C81
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x1700053E RID: 1342
			// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0003AA94 File Offset: 0x00038C94
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x1700053F RID: 1343
			// (set) Token: 0x06001B0B RID: 6923 RVA: 0x0003AAA7 File Offset: 0x00038CA7
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x17000540 RID: 1344
			// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0003AABF File Offset: 0x00038CBF
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x17000541 RID: 1345
			// (set) Token: 0x06001B0D RID: 6925 RVA: 0x0003AAD7 File Offset: 0x00038CD7
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000542 RID: 1346
			// (set) Token: 0x06001B0E RID: 6926 RVA: 0x0003AAEF File Offset: 0x00038CEF
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000543 RID: 1347
			// (set) Token: 0x06001B0F RID: 6927 RVA: 0x0003AB07 File Offset: 0x00038D07
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x17000544 RID: 1348
			// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0003AB1F File Offset: 0x00038D1F
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x17000545 RID: 1349
			// (set) Token: 0x06001B11 RID: 6929 RVA: 0x0003AB37 File Offset: 0x00038D37
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x17000546 RID: 1350
			// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0003AB4F File Offset: 0x00038D4F
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x17000547 RID: 1351
			// (set) Token: 0x06001B13 RID: 6931 RVA: 0x0003AB67 File Offset: 0x00038D67
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x17000548 RID: 1352
			// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0003AB7F File Offset: 0x00038D7F
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x17000549 RID: 1353
			// (set) Token: 0x06001B15 RID: 6933 RVA: 0x0003AB97 File Offset: 0x00038D97
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x1700054A RID: 1354
			// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0003ABAF File Offset: 0x00038DAF
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x1700054B RID: 1355
			// (set) Token: 0x06001B17 RID: 6935 RVA: 0x0003ABC7 File Offset: 0x00038DC7
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x1700054C RID: 1356
			// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0003ABDF File Offset: 0x00038DDF
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x1700054D RID: 1357
			// (set) Token: 0x06001B19 RID: 6937 RVA: 0x0003ABF2 File Offset: 0x00038DF2
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x1700054E RID: 1358
			// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0003AC05 File Offset: 0x00038E05
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x1700054F RID: 1359
			// (set) Token: 0x06001B1B RID: 6939 RVA: 0x0003AC18 File Offset: 0x00038E18
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x17000550 RID: 1360
			// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0003AC2B File Offset: 0x00038E2B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (set) Token: 0x06001B1D RID: 6941 RVA: 0x0003AC3E File Offset: 0x00038E3E
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0003AC56 File Offset: 0x00038E56
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (set) Token: 0x06001B1F RID: 6943 RVA: 0x0003AC6E File Offset: 0x00038E6E
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (set) Token: 0x06001B20 RID: 6944 RVA: 0x0003AC86 File Offset: 0x00038E86
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17000555 RID: 1365
			// (set) Token: 0x06001B21 RID: 6945 RVA: 0x0003AC99 File Offset: 0x00038E99
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17000556 RID: 1366
			// (set) Token: 0x06001B22 RID: 6946 RVA: 0x0003ACAC File Offset: 0x00038EAC
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (set) Token: 0x06001B23 RID: 6947 RVA: 0x0003ACC4 File Offset: 0x00038EC4
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17000558 RID: 1368
			// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0003ACDC File Offset: 0x00038EDC
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000559 RID: 1369
			// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0003ACF4 File Offset: 0x00038EF4
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x1700055A RID: 1370
			// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0003AD0C File Offset: 0x00038F0C
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x1700055B RID: 1371
			// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0003AD24 File Offset: 0x00038F24
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x1700055C RID: 1372
			// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0003AD3C File Offset: 0x00038F3C
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700055D RID: 1373
			// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0003AD54 File Offset: 0x00038F54
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700055E RID: 1374
			// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0003AD6C File Offset: 0x00038F6C
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x1700055F RID: 1375
			// (set) Token: 0x06001B2B RID: 6955 RVA: 0x0003AD7F File Offset: 0x00038F7F
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17000560 RID: 1376
			// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0003AD92 File Offset: 0x00038F92
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17000561 RID: 1377
			// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0003ADA5 File Offset: 0x00038FA5
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17000562 RID: 1378
			// (set) Token: 0x06001B2E RID: 6958 RVA: 0x0003ADB8 File Offset: 0x00038FB8
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17000563 RID: 1379
			// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0003ADCB File Offset: 0x00038FCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000564 RID: 1380
			// (set) Token: 0x06001B30 RID: 6960 RVA: 0x0003ADE3 File Offset: 0x00038FE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0003ADFB File Offset: 0x00038FFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000566 RID: 1382
			// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0003AE13 File Offset: 0x00039013
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000567 RID: 1383
			// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0003AE2B File Offset: 0x0003902B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C0 RID: 192
		public class SharedConfigurationParameters : ParametersBase
		{
			// Token: 0x17000568 RID: 1384
			// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0003AE4B File Offset: 0x0003904B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000569 RID: 1385
			// (set) Token: 0x06001B36 RID: 6966 RVA: 0x0003AE69 File Offset: 0x00039069
			public virtual MultiValuedProperty<OrganizationIdParameter> SharedConfiguration
			{
				set
				{
					base.PowerSharpParameters["SharedConfiguration"] = value;
				}
			}

			// Token: 0x1700056A RID: 1386
			// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0003AE7C File Offset: 0x0003907C
			public virtual SwitchParameter ClearPreviousSharedConfigurations
			{
				set
				{
					base.PowerSharpParameters["ClearPreviousSharedConfigurations"] = value;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (set) Token: 0x06001B38 RID: 6968 RVA: 0x0003AE94 File Offset: 0x00039094
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x1700056C RID: 1388
			// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0003AEAC File Offset: 0x000390AC
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0003AEC4 File Offset: 0x000390C4
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x1700056E RID: 1390
			// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0003AEDC File Offset: 0x000390DC
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x1700056F RID: 1391
			// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0003AEF4 File Offset: 0x000390F4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000570 RID: 1392
			// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0003AF0C File Offset: 0x0003910C
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x17000571 RID: 1393
			// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0003AF1F File Offset: 0x0003911F
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x17000572 RID: 1394
			// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0003AF3D File Offset: 0x0003913D
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17000573 RID: 1395
			// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0003AF55 File Offset: 0x00039155
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x17000574 RID: 1396
			// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0003AF6D File Offset: 0x0003916D
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x17000575 RID: 1397
			// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0003AF85 File Offset: 0x00039185
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x17000576 RID: 1398
			// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0003AF98 File Offset: 0x00039198
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x17000577 RID: 1399
			// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0003AFAB File Offset: 0x000391AB
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x17000578 RID: 1400
			// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0003AFBE File Offset: 0x000391BE
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x17000579 RID: 1401
			// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0003AFD6 File Offset: 0x000391D6
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x1700057A RID: 1402
			// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0003AFEE File Offset: 0x000391EE
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x1700057B RID: 1403
			// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0003B006 File Offset: 0x00039206
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x1700057C RID: 1404
			// (set) Token: 0x06001B49 RID: 6985 RVA: 0x0003B01E File Offset: 0x0003921E
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x1700057D RID: 1405
			// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0003B036 File Offset: 0x00039236
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x1700057E RID: 1406
			// (set) Token: 0x06001B4B RID: 6987 RVA: 0x0003B04E File Offset: 0x0003924E
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (set) Token: 0x06001B4C RID: 6988 RVA: 0x0003B066 File Offset: 0x00039266
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x17000580 RID: 1408
			// (set) Token: 0x06001B4D RID: 6989 RVA: 0x0003B07E File Offset: 0x0003927E
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0003B096 File Offset: 0x00039296
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0003B0AE File Offset: 0x000392AE
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0003B0C6 File Offset: 0x000392C6
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x17000584 RID: 1412
			// (set) Token: 0x06001B51 RID: 6993 RVA: 0x0003B0DE File Offset: 0x000392DE
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0003B0F6 File Offset: 0x000392F6
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x17000586 RID: 1414
			// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0003B109 File Offset: 0x00039309
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (set) Token: 0x06001B54 RID: 6996 RVA: 0x0003B11C File Offset: 0x0003931C
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x17000588 RID: 1416
			// (set) Token: 0x06001B55 RID: 6997 RVA: 0x0003B12F File Offset: 0x0003932F
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x17000589 RID: 1417
			// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0003B142 File Offset: 0x00039342
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (set) Token: 0x06001B57 RID: 6999 RVA: 0x0003B155 File Offset: 0x00039355
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0003B16D File Offset: 0x0003936D
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (set) Token: 0x06001B59 RID: 7001 RVA: 0x0003B185 File Offset: 0x00039385
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x1700058D RID: 1421
			// (set) Token: 0x06001B5A RID: 7002 RVA: 0x0003B19D File Offset: 0x0003939D
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x1700058E RID: 1422
			// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0003B1B0 File Offset: 0x000393B0
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x1700058F RID: 1423
			// (set) Token: 0x06001B5C RID: 7004 RVA: 0x0003B1C3 File Offset: 0x000393C3
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000590 RID: 1424
			// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0003B1DB File Offset: 0x000393DB
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17000591 RID: 1425
			// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0003B1F3 File Offset: 0x000393F3
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (set) Token: 0x06001B5F RID: 7007 RVA: 0x0003B20B File Offset: 0x0003940B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (set) Token: 0x06001B60 RID: 7008 RVA: 0x0003B223 File Offset: 0x00039423
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0003B23B File Offset: 0x0003943B
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (set) Token: 0x06001B62 RID: 7010 RVA: 0x0003B253 File Offset: 0x00039453
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0003B26B File Offset: 0x0003946B
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (set) Token: 0x06001B64 RID: 7012 RVA: 0x0003B283 File Offset: 0x00039483
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17000598 RID: 1432
			// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0003B296 File Offset: 0x00039496
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17000599 RID: 1433
			// (set) Token: 0x06001B66 RID: 7014 RVA: 0x0003B2A9 File Offset: 0x000394A9
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x1700059A RID: 1434
			// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0003B2BC File Offset: 0x000394BC
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0003B2CF File Offset: 0x000394CF
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0003B2E2 File Offset: 0x000394E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0003B2FA File Offset: 0x000394FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (set) Token: 0x06001B6B RID: 7019 RVA: 0x0003B312 File Offset: 0x00039512
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0003B32A File Offset: 0x0003952A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0003B342 File Offset: 0x00039542
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C1 RID: 193
		public class RemoveRelocationConstraintParameters : ParametersBase
		{
			// Token: 0x170005A1 RID: 1441
			// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0003B362 File Offset: 0x00039562
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (set) Token: 0x06001B70 RID: 7024 RVA: 0x0003B380 File Offset: 0x00039580
			public virtual SwitchParameter RemoveRelocationConstraint
			{
				set
				{
					base.PowerSharpParameters["RemoveRelocationConstraint"] = value;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0003B398 File Offset: 0x00039598
			public virtual PersistableRelocationConstraintType RelocationConstraintType
			{
				set
				{
					base.PowerSharpParameters["RelocationConstraintType"] = value;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0003B3B0 File Offset: 0x000395B0
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0003B3C8 File Offset: 0x000395C8
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0003B3E0 File Offset: 0x000395E0
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (set) Token: 0x06001B75 RID: 7029 RVA: 0x0003B3F8 File Offset: 0x000395F8
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0003B410 File Offset: 0x00039610
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0003B428 File Offset: 0x00039628
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (set) Token: 0x06001B78 RID: 7032 RVA: 0x0003B43B File Offset: 0x0003963B
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x170005AB RID: 1451
			// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0003B459 File Offset: 0x00039659
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (set) Token: 0x06001B7A RID: 7034 RVA: 0x0003B471 File Offset: 0x00039671
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x170005AD RID: 1453
			// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0003B489 File Offset: 0x00039689
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x170005AE RID: 1454
			// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0003B4A1 File Offset: 0x000396A1
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x170005AF RID: 1455
			// (set) Token: 0x06001B7D RID: 7037 RVA: 0x0003B4B4 File Offset: 0x000396B4
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x170005B0 RID: 1456
			// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0003B4C7 File Offset: 0x000396C7
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0003B4DA File Offset: 0x000396DA
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (set) Token: 0x06001B80 RID: 7040 RVA: 0x0003B4F2 File Offset: 0x000396F2
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0003B50A File Offset: 0x0003970A
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (set) Token: 0x06001B82 RID: 7042 RVA: 0x0003B522 File Offset: 0x00039722
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0003B53A File Offset: 0x0003973A
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0003B552 File Offset: 0x00039752
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (set) Token: 0x06001B85 RID: 7045 RVA: 0x0003B56A File Offset: 0x0003976A
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0003B582 File Offset: 0x00039782
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x170005B9 RID: 1465
			// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0003B59A File Offset: 0x0003979A
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0003B5B2 File Offset: 0x000397B2
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (set) Token: 0x06001B89 RID: 7049 RVA: 0x0003B5CA File Offset: 0x000397CA
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0003B5E2 File Offset: 0x000397E2
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (set) Token: 0x06001B8B RID: 7051 RVA: 0x0003B5FA File Offset: 0x000397FA
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x170005BE RID: 1470
			// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0003B612 File Offset: 0x00039812
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x170005BF RID: 1471
			// (set) Token: 0x06001B8D RID: 7053 RVA: 0x0003B625 File Offset: 0x00039825
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (set) Token: 0x06001B8E RID: 7054 RVA: 0x0003B638 File Offset: 0x00039838
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0003B64B File Offset: 0x0003984B
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (set) Token: 0x06001B90 RID: 7056 RVA: 0x0003B65E File Offset: 0x0003985E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0003B671 File Offset: 0x00039871
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (set) Token: 0x06001B92 RID: 7058 RVA: 0x0003B689 File Offset: 0x00039889
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (set) Token: 0x06001B93 RID: 7059 RVA: 0x0003B6A1 File Offset: 0x000398A1
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0003B6B9 File Offset: 0x000398B9
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0003B6CC File Offset: 0x000398CC
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0003B6DF File Offset: 0x000398DF
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (set) Token: 0x06001B97 RID: 7063 RVA: 0x0003B6F7 File Offset: 0x000398F7
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x170005CA RID: 1482
			// (set) Token: 0x06001B98 RID: 7064 RVA: 0x0003B70F File Offset: 0x0003990F
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x170005CB RID: 1483
			// (set) Token: 0x06001B99 RID: 7065 RVA: 0x0003B727 File Offset: 0x00039927
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x170005CC RID: 1484
			// (set) Token: 0x06001B9A RID: 7066 RVA: 0x0003B73F File Offset: 0x0003993F
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x170005CD RID: 1485
			// (set) Token: 0x06001B9B RID: 7067 RVA: 0x0003B757 File Offset: 0x00039957
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x170005CE RID: 1486
			// (set) Token: 0x06001B9C RID: 7068 RVA: 0x0003B76F File Offset: 0x0003996F
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x170005CF RID: 1487
			// (set) Token: 0x06001B9D RID: 7069 RVA: 0x0003B787 File Offset: 0x00039987
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (set) Token: 0x06001B9E RID: 7070 RVA: 0x0003B79F File Offset: 0x0003999F
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (set) Token: 0x06001B9F RID: 7071 RVA: 0x0003B7B2 File Offset: 0x000399B2
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x0003B7C5 File Offset: 0x000399C5
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x0003B7D8 File Offset: 0x000399D8
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x170005D4 RID: 1492
			// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x0003B7EB File Offset: 0x000399EB
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x0003B7FE File Offset: 0x000399FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x0003B816 File Offset: 0x00039A16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x0003B82E File Offset: 0x00039A2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x0003B846 File Offset: 0x00039A46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0003B85E File Offset: 0x00039A5E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C2 RID: 194
		public class SharedConfigurationInfoParameters : ParametersBase
		{
			// Token: 0x170005DA RID: 1498
			// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0003B87E File Offset: 0x00039A7E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170005DB RID: 1499
			// (set) Token: 0x06001BAA RID: 7082 RVA: 0x0003B89C File Offset: 0x00039A9C
			public virtual bool EnableAsSharedConfiguration
			{
				set
				{
					base.PowerSharpParameters["EnableAsSharedConfiguration"] = value;
				}
			}

			// Token: 0x170005DC RID: 1500
			// (set) Token: 0x06001BAB RID: 7083 RVA: 0x0003B8B4 File Offset: 0x00039AB4
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x170005DD RID: 1501
			// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0003B8CC File Offset: 0x00039ACC
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x170005DE RID: 1502
			// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0003B8E4 File Offset: 0x00039AE4
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (set) Token: 0x06001BAE RID: 7086 RVA: 0x0003B8FC File Offset: 0x00039AFC
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (set) Token: 0x06001BAF RID: 7087 RVA: 0x0003B914 File Offset: 0x00039B14
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x0003B92C File Offset: 0x00039B2C
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x170005E2 RID: 1506
			// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0003B93F File Offset: 0x00039B3F
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x0003B95D File Offset: 0x00039B5D
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x0003B975 File Offset: 0x00039B75
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x0003B98D File Offset: 0x00039B8D
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0003B9A5 File Offset: 0x00039BA5
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x0003B9B8 File Offset: 0x00039BB8
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0003B9CB File Offset: 0x00039BCB
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x0003B9DE File Offset: 0x00039BDE
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x170005EA RID: 1514
			// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x0003B9F6 File Offset: 0x00039BF6
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (set) Token: 0x06001BBA RID: 7098 RVA: 0x0003BA0E File Offset: 0x00039C0E
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (set) Token: 0x06001BBB RID: 7099 RVA: 0x0003BA26 File Offset: 0x00039C26
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (set) Token: 0x06001BBC RID: 7100 RVA: 0x0003BA3E File Offset: 0x00039C3E
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (set) Token: 0x06001BBD RID: 7101 RVA: 0x0003BA56 File Offset: 0x00039C56
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (set) Token: 0x06001BBE RID: 7102 RVA: 0x0003BA6E File Offset: 0x00039C6E
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0003BA86 File Offset: 0x00039C86
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x0003BA9E File Offset: 0x00039C9E
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x0003BAB6 File Offset: 0x00039CB6
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x0003BACE File Offset: 0x00039CCE
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x0003BAE6 File Offset: 0x00039CE6
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x0003BAFE File Offset: 0x00039CFE
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0003BB16 File Offset: 0x00039D16
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x0003BB29 File Offset: 0x00039D29
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0003BB3C File Offset: 0x00039D3C
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x0003BB4F File Offset: 0x00039D4F
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0003BB62 File Offset: 0x00039D62
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (set) Token: 0x06001BCA RID: 7114 RVA: 0x0003BB75 File Offset: 0x00039D75
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0003BB8D File Offset: 0x00039D8D
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (set) Token: 0x06001BCC RID: 7116 RVA: 0x0003BBA5 File Offset: 0x00039DA5
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0003BBBD File Offset: 0x00039DBD
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (set) Token: 0x06001BCE RID: 7118 RVA: 0x0003BBD0 File Offset: 0x00039DD0
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0003BBE3 File Offset: 0x00039DE3
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000601 RID: 1537
			// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x0003BBFB File Offset: 0x00039DFB
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0003BC13 File Offset: 0x00039E13
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x0003BC2B File Offset: 0x00039E2B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0003BC43 File Offset: 0x00039E43
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x0003BC5B File Offset: 0x00039E5B
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0003BC73 File Offset: 0x00039E73
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x0003BC8B File Offset: 0x00039E8B
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0003BCA3 File Offset: 0x00039EA3
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17000609 RID: 1545
			// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x0003BCB6 File Offset: 0x00039EB6
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0003BCC9 File Offset: 0x00039EC9
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x1700060B RID: 1547
			// (set) Token: 0x06001BDA RID: 7130 RVA: 0x0003BCDC File Offset: 0x00039EDC
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x1700060C RID: 1548
			// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0003BCEF File Offset: 0x00039EEF
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0003BD02 File Offset: 0x00039F02
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700060E RID: 1550
			// (set) Token: 0x06001BDD RID: 7133 RVA: 0x0003BD1A File Offset: 0x00039F1A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700060F RID: 1551
			// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0003BD32 File Offset: 0x00039F32
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (set) Token: 0x06001BDF RID: 7135 RVA: 0x0003BD4A File Offset: 0x00039F4A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0003BD62 File Offset: 0x00039F62
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C3 RID: 195
		public class SharedConfigurationRemoveParameters : ParametersBase
		{
			// Token: 0x17000612 RID: 1554
			// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0003BD82 File Offset: 0x00039F82
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000613 RID: 1555
			// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x0003BDA0 File Offset: 0x00039FA0
			public virtual SwitchParameter RemoveSharedConfigurations
			{
				set
				{
					base.PowerSharpParameters["RemoveSharedConfigurations"] = value;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x0003BDB8 File Offset: 0x00039FB8
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0003BDD0 File Offset: 0x00039FD0
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0003BDE8 File Offset: 0x00039FE8
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x17000617 RID: 1559
			// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0003BE00 File Offset: 0x0003A000
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x17000618 RID: 1560
			// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0003BE18 File Offset: 0x0003A018
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000619 RID: 1561
			// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x0003BE30 File Offset: 0x0003A030
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x1700061A RID: 1562
			// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0003BE43 File Offset: 0x0003A043
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x1700061B RID: 1563
			// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0003BE61 File Offset: 0x0003A061
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x1700061C RID: 1564
			// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0003BE79 File Offset: 0x0003A079
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x1700061D RID: 1565
			// (set) Token: 0x06001BED RID: 7149 RVA: 0x0003BE91 File Offset: 0x0003A091
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0003BEA9 File Offset: 0x0003A0A9
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x1700061F RID: 1567
			// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0003BEBC File Offset: 0x0003A0BC
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0003BECF File Offset: 0x0003A0CF
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0003BEE2 File Offset: 0x0003A0E2
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0003BEFA File Offset: 0x0003A0FA
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x17000623 RID: 1571
			// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0003BF12 File Offset: 0x0003A112
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0003BF2A File Offset: 0x0003A12A
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000625 RID: 1573
			// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0003BF42 File Offset: 0x0003A142
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0003BF5A File Offset: 0x0003A15A
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0003BF72 File Offset: 0x0003A172
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x0003BF8A File Offset: 0x0003A18A
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x17000629 RID: 1577
			// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x0003BFA2 File Offset: 0x0003A1A2
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0003BFBA File Offset: 0x0003A1BA
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x1700062B RID: 1579
			// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0003BFD2 File Offset: 0x0003A1D2
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0003BFEA File Offset: 0x0003A1EA
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0003C002 File Offset: 0x0003A202
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (set) Token: 0x06001BFE RID: 7166 RVA: 0x0003C01A File Offset: 0x0003A21A
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x1700062F RID: 1583
			// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0003C02D File Offset: 0x0003A22D
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x17000630 RID: 1584
			// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0003C040 File Offset: 0x0003A240
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x17000631 RID: 1585
			// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0003C053 File Offset: 0x0003A253
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x17000632 RID: 1586
			// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0003C066 File Offset: 0x0003A266
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000633 RID: 1587
			// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0003C079 File Offset: 0x0003A279
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17000634 RID: 1588
			// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0003C091 File Offset: 0x0003A291
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17000635 RID: 1589
			// (set) Token: 0x06001C05 RID: 7173 RVA: 0x0003C0A9 File Offset: 0x0003A2A9
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17000636 RID: 1590
			// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0003C0C1 File Offset: 0x0003A2C1
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17000637 RID: 1591
			// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17000638 RID: 1592
			// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0003C0E7 File Offset: 0x0003A2E7
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000639 RID: 1593
			// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0003C0FF File Offset: 0x0003A2FF
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x1700063A RID: 1594
			// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0003C117 File Offset: 0x0003A317
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x1700063B RID: 1595
			// (set) Token: 0x06001C0B RID: 7179 RVA: 0x0003C12F File Offset: 0x0003A32F
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x1700063C RID: 1596
			// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0003C147 File Offset: 0x0003A347
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x1700063D RID: 1597
			// (set) Token: 0x06001C0D RID: 7181 RVA: 0x0003C15F File Offset: 0x0003A35F
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x1700063E RID: 1598
			// (set) Token: 0x06001C0E RID: 7182 RVA: 0x0003C177 File Offset: 0x0003A377
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x1700063F RID: 1599
			// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0003C18F File Offset: 0x0003A38F
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17000640 RID: 1600
			// (set) Token: 0x06001C10 RID: 7184 RVA: 0x0003C1A7 File Offset: 0x0003A3A7
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17000641 RID: 1601
			// (set) Token: 0x06001C11 RID: 7185 RVA: 0x0003C1BA File Offset: 0x0003A3BA
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17000642 RID: 1602
			// (set) Token: 0x06001C12 RID: 7186 RVA: 0x0003C1CD File Offset: 0x0003A3CD
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17000643 RID: 1603
			// (set) Token: 0x06001C13 RID: 7187 RVA: 0x0003C1E0 File Offset: 0x0003A3E0
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17000644 RID: 1604
			// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0003C1F3 File Offset: 0x0003A3F3
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17000645 RID: 1605
			// (set) Token: 0x06001C15 RID: 7189 RVA: 0x0003C206 File Offset: 0x0003A406
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000646 RID: 1606
			// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0003C21E File Offset: 0x0003A41E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000647 RID: 1607
			// (set) Token: 0x06001C17 RID: 7191 RVA: 0x0003C236 File Offset: 0x0003A436
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000648 RID: 1608
			// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0003C24E File Offset: 0x0003A44E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000649 RID: 1609
			// (set) Token: 0x06001C19 RID: 7193 RVA: 0x0003C266 File Offset: 0x0003A466
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C4 RID: 196
		public class AddRelocationConstraintParameters : ParametersBase
		{
			// Token: 0x1700064A RID: 1610
			// (set) Token: 0x06001C1B RID: 7195 RVA: 0x0003C286 File Offset: 0x0003A486
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700064B RID: 1611
			// (set) Token: 0x06001C1C RID: 7196 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
			public virtual SwitchParameter AddRelocationConstraint
			{
				set
				{
					base.PowerSharpParameters["AddRelocationConstraint"] = value;
				}
			}

			// Token: 0x1700064C RID: 1612
			// (set) Token: 0x06001C1D RID: 7197 RVA: 0x0003C2BC File Offset: 0x0003A4BC
			public virtual PersistableRelocationConstraintType RelocationConstraintType
			{
				set
				{
					base.PowerSharpParameters["RelocationConstraintType"] = value;
				}
			}

			// Token: 0x1700064D RID: 1613
			// (set) Token: 0x06001C1E RID: 7198 RVA: 0x0003C2D4 File Offset: 0x0003A4D4
			public virtual int RelocationConstraintExpirationInDays
			{
				set
				{
					base.PowerSharpParameters["RelocationConstraintExpirationInDays"] = value;
				}
			}

			// Token: 0x1700064E RID: 1614
			// (set) Token: 0x06001C1F RID: 7199 RVA: 0x0003C2EC File Offset: 0x0003A4EC
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x1700064F RID: 1615
			// (set) Token: 0x06001C20 RID: 7200 RVA: 0x0003C304 File Offset: 0x0003A504
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x17000650 RID: 1616
			// (set) Token: 0x06001C21 RID: 7201 RVA: 0x0003C31C File Offset: 0x0003A51C
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x17000651 RID: 1617
			// (set) Token: 0x06001C22 RID: 7202 RVA: 0x0003C334 File Offset: 0x0003A534
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x17000652 RID: 1618
			// (set) Token: 0x06001C23 RID: 7203 RVA: 0x0003C34C File Offset: 0x0003A54C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000653 RID: 1619
			// (set) Token: 0x06001C24 RID: 7204 RVA: 0x0003C364 File Offset: 0x0003A564
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x17000654 RID: 1620
			// (set) Token: 0x06001C25 RID: 7205 RVA: 0x0003C377 File Offset: 0x0003A577
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x17000655 RID: 1621
			// (set) Token: 0x06001C26 RID: 7206 RVA: 0x0003C395 File Offset: 0x0003A595
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x17000656 RID: 1622
			// (set) Token: 0x06001C27 RID: 7207 RVA: 0x0003C3AD File Offset: 0x0003A5AD
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x17000657 RID: 1623
			// (set) Token: 0x06001C28 RID: 7208 RVA: 0x0003C3C5 File Offset: 0x0003A5C5
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x17000658 RID: 1624
			// (set) Token: 0x06001C29 RID: 7209 RVA: 0x0003C3DD File Offset: 0x0003A5DD
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x17000659 RID: 1625
			// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x1700065A RID: 1626
			// (set) Token: 0x06001C2B RID: 7211 RVA: 0x0003C403 File Offset: 0x0003A603
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x1700065B RID: 1627
			// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0003C416 File Offset: 0x0003A616
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x1700065C RID: 1628
			// (set) Token: 0x06001C2D RID: 7213 RVA: 0x0003C42E File Offset: 0x0003A62E
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x1700065D RID: 1629
			// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0003C446 File Offset: 0x0003A646
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x1700065E RID: 1630
			// (set) Token: 0x06001C2F RID: 7215 RVA: 0x0003C45E File Offset: 0x0003A65E
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x1700065F RID: 1631
			// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0003C476 File Offset: 0x0003A676
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x17000660 RID: 1632
			// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0003C48E File Offset: 0x0003A68E
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x17000661 RID: 1633
			// (set) Token: 0x06001C32 RID: 7218 RVA: 0x0003C4A6 File Offset: 0x0003A6A6
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x17000662 RID: 1634
			// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0003C4BE File Offset: 0x0003A6BE
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x17000663 RID: 1635
			// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0003C4D6 File Offset: 0x0003A6D6
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x17000664 RID: 1636
			// (set) Token: 0x06001C35 RID: 7221 RVA: 0x0003C4EE File Offset: 0x0003A6EE
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x17000665 RID: 1637
			// (set) Token: 0x06001C36 RID: 7222 RVA: 0x0003C506 File Offset: 0x0003A706
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x17000666 RID: 1638
			// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0003C51E File Offset: 0x0003A71E
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x17000667 RID: 1639
			// (set) Token: 0x06001C38 RID: 7224 RVA: 0x0003C536 File Offset: 0x0003A736
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x17000668 RID: 1640
			// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0003C54E File Offset: 0x0003A74E
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x17000669 RID: 1641
			// (set) Token: 0x06001C3A RID: 7226 RVA: 0x0003C561 File Offset: 0x0003A761
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x1700066A RID: 1642
			// (set) Token: 0x06001C3B RID: 7227 RVA: 0x0003C574 File Offset: 0x0003A774
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x1700066B RID: 1643
			// (set) Token: 0x06001C3C RID: 7228 RVA: 0x0003C587 File Offset: 0x0003A787
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x1700066C RID: 1644
			// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0003C59A File Offset: 0x0003A79A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700066D RID: 1645
			// (set) Token: 0x06001C3E RID: 7230 RVA: 0x0003C5AD File Offset: 0x0003A7AD
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x1700066E RID: 1646
			// (set) Token: 0x06001C3F RID: 7231 RVA: 0x0003C5C5 File Offset: 0x0003A7C5
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x1700066F RID: 1647
			// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0003C5DD File Offset: 0x0003A7DD
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17000670 RID: 1648
			// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0003C5F5 File Offset: 0x0003A7F5
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17000671 RID: 1649
			// (set) Token: 0x06001C42 RID: 7234 RVA: 0x0003C608 File Offset: 0x0003A808
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17000672 RID: 1650
			// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0003C61B File Offset: 0x0003A81B
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000673 RID: 1651
			// (set) Token: 0x06001C44 RID: 7236 RVA: 0x0003C633 File Offset: 0x0003A833
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17000674 RID: 1652
			// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0003C64B File Offset: 0x0003A84B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000675 RID: 1653
			// (set) Token: 0x06001C46 RID: 7238 RVA: 0x0003C663 File Offset: 0x0003A863
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17000676 RID: 1654
			// (set) Token: 0x06001C47 RID: 7239 RVA: 0x0003C67B File Offset: 0x0003A87B
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17000677 RID: 1655
			// (set) Token: 0x06001C48 RID: 7240 RVA: 0x0003C693 File Offset: 0x0003A893
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17000678 RID: 1656
			// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0003C6AB File Offset: 0x0003A8AB
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17000679 RID: 1657
			// (set) Token: 0x06001C4A RID: 7242 RVA: 0x0003C6C3 File Offset: 0x0003A8C3
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x1700067A RID: 1658
			// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0003C6DB File Offset: 0x0003A8DB
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x1700067B RID: 1659
			// (set) Token: 0x06001C4C RID: 7244 RVA: 0x0003C6EE File Offset: 0x0003A8EE
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x1700067C RID: 1660
			// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0003C701 File Offset: 0x0003A901
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x1700067D RID: 1661
			// (set) Token: 0x06001C4E RID: 7246 RVA: 0x0003C714 File Offset: 0x0003A914
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x1700067E RID: 1662
			// (set) Token: 0x06001C4F RID: 7247 RVA: 0x0003C727 File Offset: 0x0003A927
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x1700067F RID: 1663
			// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0003C73A File Offset: 0x0003A93A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000680 RID: 1664
			// (set) Token: 0x06001C51 RID: 7249 RVA: 0x0003C752 File Offset: 0x0003A952
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0003C76A File Offset: 0x0003A96A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000682 RID: 1666
			// (set) Token: 0x06001C53 RID: 7251 RVA: 0x0003C782 File Offset: 0x0003A982
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000683 RID: 1667
			// (set) Token: 0x06001C54 RID: 7252 RVA: 0x0003C79A File Offset: 0x0003A99A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000C5 RID: 197
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000684 RID: 1668
			// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0003C7BA File Offset: 0x0003A9BA
			public virtual bool ImmutableConfiguration
			{
				set
				{
					base.PowerSharpParameters["ImmutableConfiguration"] = value;
				}
			}

			// Token: 0x17000685 RID: 1669
			// (set) Token: 0x06001C57 RID: 7255 RVA: 0x0003C7D2 File Offset: 0x0003A9D2
			public virtual bool IsDehydrated
			{
				set
				{
					base.PowerSharpParameters["IsDehydrated"] = value;
				}
			}

			// Token: 0x17000686 RID: 1670
			// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0003C7EA File Offset: 0x0003A9EA
			public virtual bool IsStaticConfigurationShared
			{
				set
				{
					base.PowerSharpParameters["IsStaticConfigurationShared"] = value;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0003C802 File Offset: 0x0003AA02
			public virtual bool IsUpdatingServicePlan
			{
				set
				{
					base.PowerSharpParameters["IsUpdatingServicePlan"] = value;
				}
			}

			// Token: 0x17000688 RID: 1672
			// (set) Token: 0x06001C5A RID: 7258 RVA: 0x0003C81A File Offset: 0x0003AA1A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000689 RID: 1673
			// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0003C832 File Offset: 0x0003AA32
			public virtual MultiValuedProperty<Capability> PersistedCapabilities
			{
				set
				{
					base.PowerSharpParameters["PersistedCapabilities"] = value;
				}
			}

			// Token: 0x1700068A RID: 1674
			// (set) Token: 0x06001C5C RID: 7260 RVA: 0x0003C845 File Offset: 0x0003AA45
			public virtual string ExchangeUpgradeBucket
			{
				set
				{
					base.PowerSharpParameters["ExchangeUpgradeBucket"] = ((value != null) ? new ExchangeUpgradeBucketIdParameter(value) : null);
				}
			}

			// Token: 0x1700068B RID: 1675
			// (set) Token: 0x06001C5D RID: 7261 RVA: 0x0003C863 File Offset: 0x0003AA63
			public virtual SwitchParameter ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x1700068C RID: 1676
			// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0003C87B File Offset: 0x0003AA7B
			public virtual SwitchParameter ExcludedFromForwardSyncEDU2BPOS
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromForwardSyncEDU2BPOS"] = value;
				}
			}

			// Token: 0x1700068D RID: 1677
			// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0003C893 File Offset: 0x0003AA93
			public virtual int DefaultMovePriority
			{
				set
				{
					base.PowerSharpParameters["DefaultMovePriority"] = value;
				}
			}

			// Token: 0x1700068E RID: 1678
			// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0003C8AB File Offset: 0x0003AAAB
			public virtual string UpgradeMessage
			{
				set
				{
					base.PowerSharpParameters["UpgradeMessage"] = value;
				}
			}

			// Token: 0x1700068F RID: 1679
			// (set) Token: 0x06001C61 RID: 7265 RVA: 0x0003C8BE File Offset: 0x0003AABE
			public virtual string UpgradeDetails
			{
				set
				{
					base.PowerSharpParameters["UpgradeDetails"] = value;
				}
			}

			// Token: 0x17000690 RID: 1680
			// (set) Token: 0x06001C62 RID: 7266 RVA: 0x0003C8D1 File Offset: 0x0003AAD1
			public virtual UpgradeConstraintArray UpgradeConstraints
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraints"] = value;
				}
			}

			// Token: 0x17000691 RID: 1681
			// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0003C8E4 File Offset: 0x0003AAE4
			public virtual UpgradeStage? UpgradeStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeStage"] = value;
				}
			}

			// Token: 0x17000692 RID: 1682
			// (set) Token: 0x06001C64 RID: 7268 RVA: 0x0003C8FC File Offset: 0x0003AAFC
			public virtual DateTime? UpgradeStageTimeStamp
			{
				set
				{
					base.PowerSharpParameters["UpgradeStageTimeStamp"] = value;
				}
			}

			// Token: 0x17000693 RID: 1683
			// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0003C914 File Offset: 0x0003AB14
			public virtual int? UpgradeE14MbxCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14MbxCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000694 RID: 1684
			// (set) Token: 0x06001C66 RID: 7270 RVA: 0x0003C92C File Offset: 0x0003AB2C
			public virtual int? UpgradeE14RequestCountForCurrentStage
			{
				set
				{
					base.PowerSharpParameters["UpgradeE14RequestCountForCurrentStage"] = value;
				}
			}

			// Token: 0x17000695 RID: 1685
			// (set) Token: 0x06001C67 RID: 7271 RVA: 0x0003C944 File Offset: 0x0003AB44
			public virtual DateTime? UpgradeLastE14CountsUpdateTime
			{
				set
				{
					base.PowerSharpParameters["UpgradeLastE14CountsUpdateTime"] = value;
				}
			}

			// Token: 0x17000696 RID: 1686
			// (set) Token: 0x06001C68 RID: 7272 RVA: 0x0003C95C File Offset: 0x0003AB5C
			public virtual bool? UpgradeConstraintsDisabled
			{
				set
				{
					base.PowerSharpParameters["UpgradeConstraintsDisabled"] = value;
				}
			}

			// Token: 0x17000697 RID: 1687
			// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0003C974 File Offset: 0x0003AB74
			public virtual int? UpgradeUnitsOverride
			{
				set
				{
					base.PowerSharpParameters["UpgradeUnitsOverride"] = value;
				}
			}

			// Token: 0x17000698 RID: 1688
			// (set) Token: 0x06001C6A RID: 7274 RVA: 0x0003C98C File Offset: 0x0003AB8C
			public virtual int MaxOfflineAddressBooks
			{
				set
				{
					base.PowerSharpParameters["MaxOfflineAddressBooks"] = value;
				}
			}

			// Token: 0x17000699 RID: 1689
			// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
			public virtual int MaxAddressBookPolicies
			{
				set
				{
					base.PowerSharpParameters["MaxAddressBookPolicies"] = value;
				}
			}

			// Token: 0x1700069A RID: 1690
			// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0003C9BC File Offset: 0x0003ABBC
			public virtual MailboxRelease MailboxRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxRelease"] = value;
				}
			}

			// Token: 0x1700069B RID: 1691
			// (set) Token: 0x06001C6D RID: 7277 RVA: 0x0003C9D4 File Offset: 0x0003ABD4
			public virtual MailboxRelease PreviousMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PreviousMailboxRelease"] = value;
				}
			}

			// Token: 0x1700069C RID: 1692
			// (set) Token: 0x06001C6E RID: 7278 RVA: 0x0003C9EC File Offset: 0x0003ABEC
			public virtual MailboxRelease PilotMailboxRelease
			{
				set
				{
					base.PowerSharpParameters["PilotMailboxRelease"] = value;
				}
			}

			// Token: 0x1700069D RID: 1693
			// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0003CA04 File Offset: 0x0003AC04
			public virtual bool IsLicensingEnforced
			{
				set
				{
					base.PowerSharpParameters["IsLicensingEnforced"] = value;
				}
			}

			// Token: 0x1700069E RID: 1694
			// (set) Token: 0x06001C70 RID: 7280 RVA: 0x0003CA1C File Offset: 0x0003AC1C
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x1700069F RID: 1695
			// (set) Token: 0x06001C71 RID: 7281 RVA: 0x0003CA2F File Offset: 0x0003AC2F
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x170006A0 RID: 1696
			// (set) Token: 0x06001C72 RID: 7282 RVA: 0x0003CA42 File Offset: 0x0003AC42
			public virtual string ServicePlan
			{
				set
				{
					base.PowerSharpParameters["ServicePlan"] = value;
				}
			}

			// Token: 0x170006A1 RID: 1697
			// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0003CA55 File Offset: 0x0003AC55
			public virtual string TargetServicePlan
			{
				set
				{
					base.PowerSharpParameters["TargetServicePlan"] = value;
				}
			}

			// Token: 0x170006A2 RID: 1698
			// (set) Token: 0x06001C74 RID: 7284 RVA: 0x0003CA68 File Offset: 0x0003AC68
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006A3 RID: 1699
			// (set) Token: 0x06001C75 RID: 7285 RVA: 0x0003CA7B File Offset: 0x0003AC7B
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0003CA93 File Offset: 0x0003AC93
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x170006A5 RID: 1701
			// (set) Token: 0x06001C77 RID: 7287 RVA: 0x0003CAAB File Offset: 0x0003ACAB
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x170006A6 RID: 1702
			// (set) Token: 0x06001C78 RID: 7288 RVA: 0x0003CAC3 File Offset: 0x0003ACC3
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x170006A7 RID: 1703
			// (set) Token: 0x06001C79 RID: 7289 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x170006A8 RID: 1704
			// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0003CAE9 File Offset: 0x0003ACE9
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x170006A9 RID: 1705
			// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0003CB01 File Offset: 0x0003AD01
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x170006AA RID: 1706
			// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0003CB19 File Offset: 0x0003AD19
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x170006AB RID: 1707
			// (set) Token: 0x06001C7D RID: 7293 RVA: 0x0003CB31 File Offset: 0x0003AD31
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x170006AC RID: 1708
			// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0003CB49 File Offset: 0x0003AD49
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x170006AD RID: 1709
			// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0003CB61 File Offset: 0x0003AD61
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x170006AE RID: 1710
			// (set) Token: 0x06001C80 RID: 7296 RVA: 0x0003CB79 File Offset: 0x0003AD79
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x170006AF RID: 1711
			// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0003CB91 File Offset: 0x0003AD91
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x170006B0 RID: 1712
			// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0003CBA9 File Offset: 0x0003ADA9
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x170006B1 RID: 1713
			// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0003CBBC File Offset: 0x0003ADBC
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (set) Token: 0x06001C84 RID: 7300 RVA: 0x0003CBCF File Offset: 0x0003ADCF
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x170006B3 RID: 1715
			// (set) Token: 0x06001C85 RID: 7301 RVA: 0x0003CBE2 File Offset: 0x0003ADE2
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x170006B4 RID: 1716
			// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0003CBF5 File Offset: 0x0003ADF5
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x170006B5 RID: 1717
			// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0003CC08 File Offset: 0x0003AE08
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006B6 RID: 1718
			// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0003CC20 File Offset: 0x0003AE20
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006B7 RID: 1719
			// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0003CC38 File Offset: 0x0003AE38
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006B8 RID: 1720
			// (set) Token: 0x06001C8A RID: 7306 RVA: 0x0003CC50 File Offset: 0x0003AE50
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006B9 RID: 1721
			// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0003CC68 File Offset: 0x0003AE68
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
