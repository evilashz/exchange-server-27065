using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000074 RID: 116
	internal static class Strings
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00012880 File Offset: 0x00010A80
		static Strings()
		{
			Strings.stringIDs.Add(436210406U, "ChoosingGlobalCatalog");
			Strings.stringIDs.Add(4081297114U, "LanguagePacksUpToDate");
			Strings.stringIDs.Add(2029598378U, "PostSetupText");
			Strings.stringIDs.Add(3976870610U, "CouldNotDeserializeStateFile");
			Strings.stringIDs.Add(590640951U, "AddLanguagePacksSuccessText");
			Strings.stringIDs.Add(944044270U, "CopyLanguagePacksDisplayName");
			Strings.stringIDs.Add(800186353U, "UmLanguagePacksToRemove");
			Strings.stringIDs.Add(3870436312U, "DRServerRoleText");
			Strings.stringIDs.Add(2394569239U, "AddSuccessText");
			Strings.stringIDs.Add(4289798847U, "RemoveIntroductionText");
			Strings.stringIDs.Add(406751805U, "HasConfiguredRoles");
			Strings.stringIDs.Add(776586043U, "AddUmLanguagePackText");
			Strings.stringIDs.Add(4258824323U, "SetupRebootRequired");
			Strings.stringIDs.Add(3988419612U, "UmLanguagePackDisplayName");
			Strings.stringIDs.Add(318591995U, "UnifiedMessagingRoleIsRequiredForLanguagePackInstalls");
			Strings.stringIDs.Add(3603864464U, "FrontendTransportRoleDisplayName");
			Strings.stringIDs.Add(3211475425U, "UpgradePrereq");
			Strings.stringIDs.Add(3150196311U, "CentralAdminFrontEndRoleDisplayName");
			Strings.stringIDs.Add(1623683896U, "PreConfigurationDisplayName");
			Strings.stringIDs.Add(523317789U, "RemoveFailText");
			Strings.stringIDs.Add(1831888326U, "AddSuccessStatus");
			Strings.stringIDs.Add(1377162733U, "PickingDomainController");
			Strings.stringIDs.Add(237451456U, "UmLanguagePacksToAdd");
			Strings.stringIDs.Add(3712765806U, "LPVersioningInvalidValue");
			Strings.stringIDs.Add(1055111548U, "LanguagePacksPackagePathNotSpecified");
			Strings.stringIDs.Add(2144854482U, "AddRolesToInstall");
			Strings.stringIDs.Add(2493777864U, "AddOtherRolesError");
			Strings.stringIDs.Add(35176418U, "NeedConfigureIpv6ForSecondSubnetWhenFirstSubnetConfiguresIPV6");
			Strings.stringIDs.Add(4234837096U, "ModeErrorForDisasterRecovery");
			Strings.stringIDs.Add(3904063070U, "CannotSpecifyIndustryTypeWhenOrgIsUpToDateDuringServerInstallation");
			Strings.stringIDs.Add(2285919320U, "RemoveUmLanguagePackFailText");
			Strings.stringIDs.Add(1771160928U, "ChoosingDomainController");
			Strings.stringIDs.Add(3069688065U, "CentralAdminRoleDisplayName");
			Strings.stringIDs.Add(2768015112U, "NeedConfigureIpv4StaticAddressForSecondSubnetWhenFirstSubnetConfiguresIPV4StaticAddress");
			Strings.stringIDs.Add(1833305185U, "PreSetupText");
			Strings.stringIDs.Add(2815048144U, "ModeErrorForUpgrade");
			Strings.stringIDs.Add(261522387U, "OrganizationInstallText");
			Strings.stringIDs.Add(415144177U, "CannotSpecifyServerCEIPWhenMachineIsNotCleanDuringServerInstallation");
			Strings.stringIDs.Add(1893181714U, "LanguagePacksToInstall");
			Strings.stringIDs.Add(1778403425U, "RemoveSuccessStatus");
			Strings.stringIDs.Add(4061842413U, "UnableToFindLPVersioning");
			Strings.stringIDs.Add(1048539684U, "CannotRemoveEnglishUSLanguagePack");
			Strings.stringIDs.Add(3469958538U, "LanguagePacksToAdd");
			Strings.stringIDs.Add(3908217517U, "UpgradeRolesToInstall");
			Strings.stringIDs.Add(3473605283U, "WillSkipAMEngineDownloadCheck");
			Strings.stringIDs.Add(1841681484U, "WillGetConfiguredRolesFromRegistry");
			Strings.stringIDs.Add(3319509477U, "MailboxRoleDisplayName");
			Strings.stringIDs.Add(3566628291U, "EnglishUSLanguagePackInstalled");
			Strings.stringIDs.Add(2515305841U, "NoRoleSelectedForUninstall");
			Strings.stringIDs.Add(2876792450U, "MustEnableLegacyOutlook");
			Strings.stringIDs.Add(1610115158U, "RemoveDatacenterFileText");
			Strings.stringIDs.Add(4024296328U, "CannotSpecifyCEIPWhenOrganizationHasE14OrLaterServersDuringPrepareAD");
			Strings.stringIDs.Add(1416621619U, "OrgAlreadyHasBridgeheadServers");
			Strings.stringIDs.Add(176205814U, "PrerequisiteAnalysis");
			Strings.stringIDs.Add(1959016418U, "NeedConfigureIpv4ForSecondSubnetWhenFirstSubnetConfiguresIPV4");
			Strings.stringIDs.Add(379286002U, "RemovePreCheckText");
			Strings.stringIDs.Add(590609003U, "RemoveUmLanguagePackSuccessText");
			Strings.stringIDs.Add(1869709987U, "DeterminingOrgPrepParameters");
			Strings.stringIDs.Add(3671143606U, "RemoveProgressText");
			Strings.stringIDs.Add(1281959852U, "RemoveUmLanguagePackText");
			Strings.stringIDs.Add(2770538346U, "DRServerNotFoundInAD");
			Strings.stringIDs.Add(1120765526U, "CannotSpecifyServerCEIPWhenGlobalCEIPIsOptedOutDuringServerInstallation");
			Strings.stringIDs.Add(3535416533U, "ServerOptDescriptionText");
			Strings.stringIDs.Add(2156205750U, "InstallationPathNotSet");
			Strings.stringIDs.Add(871634395U, "UnableToFindBuildVersion");
			Strings.stringIDs.Add(3002787153U, "AdminToolsRoleDisplayName");
			Strings.stringIDs.Add(3235153535U, "UmLanguagePackPackagePathNotSpecified");
			Strings.stringIDs.Add(1136733772U, "AddCannotChangeTargetDirectoryError");
			Strings.stringIDs.Add(2335571147U, "EdgeRoleInstalledButServerObjectNotFound");
			Strings.stringIDs.Add(1016568891U, "LanguagePacksNotFound");
			Strings.stringIDs.Add(2203024520U, "ApplyingDefaultRoleSelectionState");
			Strings.stringIDs.Add(2001599932U, "MustConfigureIPv4ForFirstSubnet");
			Strings.stringIDs.Add(2161842580U, "LanguagePacksLogFilePathNotSpecified");
			Strings.stringIDs.Add(1640598683U, "OrganizationPrereqText");
			Strings.stringIDs.Add(3360917926U, "AddFailText");
			Strings.stringIDs.Add(1124277255U, "RemoveRolesToInstall");
			Strings.stringIDs.Add(968377506U, "NeedConfigureIpv4DHCPForSecondSubnetWhenFirstSubnetConfiguresIPV4DHCP");
			Strings.stringIDs.Add(1772477316U, "MidFileCopyText");
			Strings.stringIDs.Add(2913262092U, "CentralAdminDatabaseRoleDisplayName");
			Strings.stringIDs.Add(3003755610U, "AddGatewayAloneError");
			Strings.stringIDs.Add(55657320U, "CafeRoleDisplayName");
			Strings.stringIDs.Add(3106188069U, "NoServerRolesToInstall");
			Strings.stringIDs.Add(3438721327U, "DRPrereq");
			Strings.stringIDs.Add(2059503147U, "ModeErrorForFreshInstall");
			Strings.stringIDs.Add(1931019259U, "LegacyRoutingServerNotValid");
			Strings.stringIDs.Add(1405894953U, "CopyDatacenterFileText");
			Strings.stringIDs.Add(3288868438U, "UnsupportedMode");
			Strings.stringIDs.Add(1947354702U, "ExchangeOrganizationNameRequired");
			Strings.stringIDs.Add(1144432722U, "UpgradePreCheckText");
			Strings.stringIDs.Add(55662122U, "Prereqs");
			Strings.stringIDs.Add(724167955U, "FailedToReadLCIDFromRegistryError");
			Strings.stringIDs.Add(2114641061U, "LanguagePacksDisplayName");
			Strings.stringIDs.Add(3856638130U, "SetupExitsBecauseOfTransientException");
			Strings.stringIDs.Add(3783483884U, "DRSuccessText");
			Strings.stringIDs.Add(2041466059U, "DRRolesToInstall");
			Strings.stringIDs.Add(3915936117U, "ParametersForTheTaskTitle");
			Strings.stringIDs.Add(2030347010U, "ADRelatedUnknownError");
			Strings.stringIDs.Add(16560395U, "AddFailStatus");
			Strings.stringIDs.Add(1439738700U, "AddLanguagePacksFailText");
			Strings.stringIDs.Add(4069246837U, "LanguagePacksInstalledVersionNull");
			Strings.stringIDs.Add(1695043038U, "SetupExitsBecauseOfLPPathNotFoundException");
			Strings.stringIDs.Add(4175779855U, "ServerIsProvisioned");
			Strings.stringIDs.Add(3668657674U, "NoUmLanguagePackSpecified");
			Strings.stringIDs.Add(209657916U, "PostFileCopyText");
			Strings.stringIDs.Add(2127358907U, "ExecutionCompleted");
			Strings.stringIDs.Add(3830028115U, "UnknownPreviousVersion");
			Strings.stringIDs.Add(3751538685U, "RemoveFileText");
			Strings.stringIDs.Add(2949666774U, "MSIIsCurrent");
			Strings.stringIDs.Add(139720102U, "AddPrereq");
			Strings.stringIDs.Add(3212518314U, "RemoveServerRoleText");
			Strings.stringIDs.Add(1066611882U, "DoesNotSupportCEIPForAdminOnlyInstallation");
			Strings.stringIDs.Add(4291904420U, "AddLanguagePacksText");
			Strings.stringIDs.Add(155941904U, "DRPreCheckText");
			Strings.stringIDs.Add(357116433U, "AddProgressText");
			Strings.stringIDs.Add(1940285462U, "DRFailStatus");
			Strings.stringIDs.Add(1540748327U, "RemoveUmLanguagePackFailStatus");
			Strings.stringIDs.Add(2888945725U, "UpgradeIntroductionText");
			Strings.stringIDs.Add(3353325242U, "NoUmLanguagePackInstalled");
			Strings.stringIDs.Add(806279811U, "RemoveUnifiedMessagingServerDescription");
			Strings.stringIDs.Add(3229808788U, "ClientAccessRoleDisplayName");
			Strings.stringIDs.Add(2809596612U, "UpgradeFailStatus");
			Strings.stringIDs.Add(1627958870U, "NoRoleSelectedForInstall");
			Strings.stringIDs.Add(1165007882U, "ExecutionError");
			Strings.stringIDs.Add(3076164745U, "InstallationLicenseAgreementShortDescription");
			Strings.stringIDs.Add(3635986314U, "RemoveSuccessText");
			Strings.stringIDs.Add(1223959701U, "InstalledLanguageComment");
			Strings.stringIDs.Add(3791960239U, "UpgradeFailText");
			Strings.stringIDs.Add(2036054501U, "PreFileCopyText");
			Strings.stringIDs.Add(3001810281U, "UnknownDestinationPath");
			Strings.stringIDs.Add(3520674066U, "UpgradeServerRoleText");
			Strings.stringIDs.Add(2572849171U, "GlobalOptDescriptionText");
			Strings.stringIDs.Add(1835758957U, "AddServerRoleText");
			Strings.stringIDs.Add(213042017U, "MaintenanceIntroduction");
			Strings.stringIDs.Add(3361199071U, "UmLanguagePathLogFilePathNotSpecified");
			Strings.stringIDs.Add(1232809750U, "UpgradeProgressText");
			Strings.stringIDs.Add(2702857678U, "ADHasBeenPrepared");
			Strings.stringIDs.Add(783977028U, "SpecifyExchangeOrganizationName");
			Strings.stringIDs.Add(2915089885U, "UpgradeMustUseBootStrapper");
			Strings.stringIDs.Add(1655961090U, "AddIntroductionText");
			Strings.stringIDs.Add(522237759U, "UpgradeSuccessStatus");
			Strings.stringIDs.Add(48624620U, "LegacyServerNameRequired");
			Strings.stringIDs.Add(2979515445U, "TheCurrentServerHasNoExchangeBits");
			Strings.stringIDs.Add(4193588254U, "UpgradeSuccessText");
			Strings.stringIDs.Add(1134594294U, "BridgeheadRoleDisplayName");
			Strings.stringIDs.Add(966578211U, "WaitForForestPrepReplicationToLocalDomainException");
			Strings.stringIDs.Add(49080419U, "RemovePrereq");
			Strings.stringIDs.Add(915558695U, "FreshIntroductionText");
			Strings.stringIDs.Add(126744571U, "UnifiedMessagingRoleDisplayName");
			Strings.stringIDs.Add(186454365U, "LanguagePackPathException");
			Strings.stringIDs.Add(2320733697U, "GatewayRoleDisplayName");
			Strings.stringIDs.Add(3864515574U, "WillDisableAMFiltering");
			Strings.stringIDs.Add(4222861002U, "TheCurrentServerHasExchangeBits");
			Strings.stringIDs.Add(2368998453U, "MonitoringRoleDisplayName");
			Strings.stringIDs.Add(3224205240U, "OrgAlreadyHasMailboxServers");
			Strings.stringIDs.Add(3656576577U, "LanguagePackPathNotFoundError");
			Strings.stringIDs.Add(1435960826U, "MustConfigureIPv6ForFirstSubnet");
			Strings.stringIDs.Add(2370029120U, "WillRestartSetupUI");
			Strings.stringIDs.Add(3059971080U, "RemoveFailStatus");
			Strings.stringIDs.Add(2070315917U, "AddConflictedRolesError");
			Strings.stringIDs.Add(3504188790U, "SchemaMasterAvailable");
			Strings.stringIDs.Add(2214617625U, "DRFailText");
			Strings.stringIDs.Add(1095004458U, "UpgradeIntroduction");
			Strings.stringIDs.Add(1616603373U, "AddPreCheckText");
			Strings.stringIDs.Add(3338459901U, "SourceDirNotSpecifiedError");
			Strings.stringIDs.Add(3315256202U, "FreshIntroduction");
			Strings.stringIDs.Add(2192506430U, "SchemaMasterDCNotFoundException");
			Strings.stringIDs.Add(2107252035U, "ADDriverBoundToAdam");
			Strings.stringIDs.Add(1331356275U, "CabUtilityWrapperError");
			Strings.stringIDs.Add(4044548133U, "SchemaMasterNotAvailable");
			Strings.stringIDs.Add(2024338705U, "OSPRoleDisplayName");
			Strings.stringIDs.Add(2927231269U, "UpgradeLicenseAgreementShortDescription");
			Strings.stringIDs.Add(759658685U, "DRSuccessStatus");
			Strings.stringIDs.Add(2340024118U, "CopyFileText");
			Strings.stringIDs.Add(302515857U, "WillNotStartTransportService");
			Strings.stringIDs.Add(3504655211U, "RemoveMESOObjectLink");
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001366C File Offset: 0x0001186C
		public static LocalizedString WillSearchForAServerObjectForLocalServer(string serverName)
		{
			return new LocalizedString("WillSearchForAServerObjectForLocalServer", "Ex0DF1DD", false, true, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001369B File Offset: 0x0001189B
		public static LocalizedString ChoosingGlobalCatalog
		{
			get
			{
				return new LocalizedString("ChoosingGlobalCatalog", "Ex49FC41", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000136B9 File Offset: 0x000118B9
		public static LocalizedString LanguagePacksUpToDate
		{
			get
			{
				return new LocalizedString("LanguagePacksUpToDate", "Ex3830E7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000136D7 File Offset: 0x000118D7
		public static LocalizedString PostSetupText
		{
			get
			{
				return new LocalizedString("PostSetupText", "Ex0C7BE7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000136F8 File Offset: 0x000118F8
		public static LocalizedString TargetDirectoryCannotBeChanged(string targetDirectory)
		{
			return new LocalizedString("TargetDirectoryCannotBeChanged", "Ex96C8A9", false, true, Strings.ResourceManager, new object[]
			{
				targetDirectory
			});
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00013727 File Offset: 0x00011927
		public static LocalizedString CouldNotDeserializeStateFile
		{
			get
			{
				return new LocalizedString("CouldNotDeserializeStateFile", "ExCEC759", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00013745 File Offset: 0x00011945
		public static LocalizedString AddLanguagePacksSuccessText
		{
			get
			{
				return new LocalizedString("AddLanguagePacksSuccessText", "ExCBD714", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00013763 File Offset: 0x00011963
		public static LocalizedString CopyLanguagePacksDisplayName
		{
			get
			{
				return new LocalizedString("CopyLanguagePacksDisplayName", "Ex400368", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00013781 File Offset: 0x00011981
		public static LocalizedString UmLanguagePacksToRemove
		{
			get
			{
				return new LocalizedString("UmLanguagePacksToRemove", "Ex1C4775", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001379F File Offset: 0x0001199F
		public static LocalizedString DRServerRoleText
		{
			get
			{
				return new LocalizedString("DRServerRoleText", "ExD19D7B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000137BD File Offset: 0x000119BD
		public static LocalizedString AddSuccessText
		{
			get
			{
				return new LocalizedString("AddSuccessText", "Ex07B8B7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000137DB File Offset: 0x000119DB
		public static LocalizedString RemoveIntroductionText
		{
			get
			{
				return new LocalizedString("RemoveIntroductionText", "Ex66717F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000137FC File Offset: 0x000119FC
		public static LocalizedString LanguagePackPackagePath(string path)
		{
			return new LocalizedString("LanguagePackPackagePath", "ExFF7939", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001382B File Offset: 0x00011A2B
		public static LocalizedString HasConfiguredRoles
		{
			get
			{
				return new LocalizedString("HasConfiguredRoles", "ExB05C95", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00013849 File Offset: 0x00011A49
		public static LocalizedString AddUmLanguagePackText
		{
			get
			{
				return new LocalizedString("AddUmLanguagePackText", "Ex62E3B2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00013868 File Offset: 0x00011A68
		public static LocalizedString ValidatingOptionsForRoles(int count)
		{
			return new LocalizedString("ValidatingOptionsForRoles", "Ex67CC0A", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001389C File Offset: 0x00011A9C
		public static LocalizedString ADSchemaVersionHigherThanSetupException(int adSchemaVersion, int setupSchemaVersion)
		{
			return new LocalizedString("ADSchemaVersionHigherThanSetupException", "Ex8467E9", false, true, Strings.ResourceManager, new object[]
			{
				adSchemaVersion,
				setupSchemaVersion
			});
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000138D9 File Offset: 0x00011AD9
		public static LocalizedString SetupRebootRequired
		{
			get
			{
				return new LocalizedString("SetupRebootRequired", "Ex4030F9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000138F8 File Offset: 0x00011AF8
		public static LocalizedString UserSpecifiedDCIsNotInLocalDomainException(string dc)
		{
			return new LocalizedString("UserSpecifiedDCIsNotInLocalDomainException", "ExB3049E", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013928 File Offset: 0x00011B28
		public static LocalizedString DeserializedStateXML(string xml)
		{
			return new LocalizedString("DeserializedStateXML", "ExD4730E", false, true, Strings.ResourceManager, new object[]
			{
				xml
			});
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00013958 File Offset: 0x00011B58
		public static LocalizedString InstallationPathInvalidDriveTypeInformation(string path)
		{
			return new LocalizedString("InstallationPathInvalidDriveTypeInformation", "Ex779C76", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00013987 File Offset: 0x00011B87
		public static LocalizedString UmLanguagePackDisplayName
		{
			get
			{
				return new LocalizedString("UmLanguagePackDisplayName", "Ex69990E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000139A5 File Offset: 0x00011BA5
		public static LocalizedString UnifiedMessagingRoleIsRequiredForLanguagePackInstalls
		{
			get
			{
				return new LocalizedString("UnifiedMessagingRoleIsRequiredForLanguagePackInstalls", "Ex42A83B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000139C4 File Offset: 0x00011BC4
		public static LocalizedString SettingArgumentBecauseItIsRequired(string argument)
		{
			return new LocalizedString("SettingArgumentBecauseItIsRequired", "Ex029459", false, true, Strings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000139F3 File Offset: 0x00011BF3
		public static LocalizedString FrontendTransportRoleDisplayName
		{
			get
			{
				return new LocalizedString("FrontendTransportRoleDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00013A11 File Offset: 0x00011C11
		public static LocalizedString UpgradePrereq
		{
			get
			{
				return new LocalizedString("UpgradePrereq", "Ex49E36B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00013A2F File Offset: 0x00011C2F
		public static LocalizedString CentralAdminFrontEndRoleDisplayName
		{
			get
			{
				return new LocalizedString("CentralAdminFrontEndRoleDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013A50 File Offset: 0x00011C50
		public static LocalizedString NotEnoughSpace(string requiredDiskSpace)
		{
			return new LocalizedString("NotEnoughSpace", "ExB35E2B", false, true, Strings.ResourceManager, new object[]
			{
				requiredDiskSpace
			});
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00013A80 File Offset: 0x00011C80
		public static LocalizedString RoleInstalledOnServer(string rolename)
		{
			return new LocalizedString("RoleInstalledOnServer", "ExBB1015", false, true, Strings.ResourceManager, new object[]
			{
				rolename
			});
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013AB0 File Offset: 0x00011CB0
		public static LocalizedString ExchangeVersionInvalid(string serverName, string message)
		{
			return new LocalizedString("ExchangeVersionInvalid", "ExA918F8", false, true, Strings.ResourceManager, new object[]
			{
				serverName,
				message
			});
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00013AE3 File Offset: 0x00011CE3
		public static LocalizedString PreConfigurationDisplayName
		{
			get
			{
				return new LocalizedString("PreConfigurationDisplayName", "Ex48B6C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00013B01 File Offset: 0x00011D01
		public static LocalizedString RemoveFailText
		{
			get
			{
				return new LocalizedString("RemoveFailText", "ExD96F96", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013B20 File Offset: 0x00011D20
		public static LocalizedString ExchangeOrganizationAlreadyExists(string orgname, string newname)
		{
			return new LocalizedString("ExchangeOrganizationAlreadyExists", "Ex2C189D", false, true, Strings.ResourceManager, new object[]
			{
				orgname,
				newname
			});
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013B54 File Offset: 0x00011D54
		public static LocalizedString InstallationPathInvalidProfilesInformation(string path)
		{
			return new LocalizedString("InstallationPathInvalidProfilesInformation", "Ex363458", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00013B84 File Offset: 0x00011D84
		public static LocalizedString ServerNotFound(string name)
		{
			return new LocalizedString("ServerNotFound", "Ex13669F", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00013BB3 File Offset: 0x00011DB3
		public static LocalizedString AddSuccessStatus
		{
			get
			{
				return new LocalizedString("AddSuccessStatus", "Ex42E48C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public static LocalizedString DomainControllerChosen(string dc)
		{
			return new LocalizedString("DomainControllerChosen", "Ex3D8FF4", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00013C03 File Offset: 0x00011E03
		public static LocalizedString PickingDomainController
		{
			get
			{
				return new LocalizedString("PickingDomainController", "Ex3F4D04", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00013C21 File Offset: 0x00011E21
		public static LocalizedString UmLanguagePacksToAdd
		{
			get
			{
				return new LocalizedString("UmLanguagePacksToAdd", "Ex99C84E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00013C3F File Offset: 0x00011E3F
		public static LocalizedString LPVersioningInvalidValue
		{
			get
			{
				return new LocalizedString("LPVersioningInvalidValue", "Ex5AE397", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013C60 File Offset: 0x00011E60
		public static LocalizedString UserSpecifiedTargetDir(string target)
		{
			return new LocalizedString("UserSpecifiedTargetDir", "ExA90987", false, true, Strings.ResourceManager, new object[]
			{
				target
			});
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013C90 File Offset: 0x00011E90
		public static LocalizedString NotAValidFqdn(string fqdn)
		{
			return new LocalizedString("NotAValidFqdn", "", false, false, Strings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00013CBF File Offset: 0x00011EBF
		public static LocalizedString LanguagePacksPackagePathNotSpecified
		{
			get
			{
				return new LocalizedString("LanguagePacksPackagePathNotSpecified", "Ex1DA650", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00013CDD File Offset: 0x00011EDD
		public static LocalizedString AddRolesToInstall
		{
			get
			{
				return new LocalizedString("AddRolesToInstall", "ExD97522", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00013CFB File Offset: 0x00011EFB
		public static LocalizedString AddOtherRolesError
		{
			get
			{
				return new LocalizedString("AddOtherRolesError", "Ex128927", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00013D19 File Offset: 0x00011F19
		public static LocalizedString NeedConfigureIpv6ForSecondSubnetWhenFirstSubnetConfiguresIPV6
		{
			get
			{
				return new LocalizedString("NeedConfigureIpv6ForSecondSubnetWhenFirstSubnetConfiguresIPV6", "Ex9882E9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00013D37 File Offset: 0x00011F37
		public static LocalizedString ModeErrorForDisasterRecovery
		{
			get
			{
				return new LocalizedString("ModeErrorForDisasterRecovery", "Ex08A873", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00013D58 File Offset: 0x00011F58
		public static LocalizedString HighLevelTaskStarted(string task)
		{
			return new LocalizedString("HighLevelTaskStarted", "Ex4AF958", false, true, Strings.ResourceManager, new object[]
			{
				task
			});
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00013D87 File Offset: 0x00011F87
		public static LocalizedString CannotSpecifyIndustryTypeWhenOrgIsUpToDateDuringServerInstallation
		{
			get
			{
				return new LocalizedString("CannotSpecifyIndustryTypeWhenOrgIsUpToDateDuringServerInstallation", "Ex4D051A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00013DA5 File Offset: 0x00011FA5
		public static LocalizedString RemoveUmLanguagePackFailText
		{
			get
			{
				return new LocalizedString("RemoveUmLanguagePackFailText", "Ex955291", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00013DC4 File Offset: 0x00011FC4
		public static LocalizedString UmLanguagePackPackagePath(string path)
		{
			return new LocalizedString("UmLanguagePackPackagePath", "Ex5D00DB", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00013DF3 File Offset: 0x00011FF3
		public static LocalizedString ChoosingDomainController
		{
			get
			{
				return new LocalizedString("ChoosingDomainController", "ExFC1196", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00013E11 File Offset: 0x00012011
		public static LocalizedString CentralAdminRoleDisplayName
		{
			get
			{
				return new LocalizedString("CentralAdminRoleDisplayName", "Ex6C64FD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00013E2F File Offset: 0x0001202F
		public static LocalizedString NeedConfigureIpv4StaticAddressForSecondSubnetWhenFirstSubnetConfiguresIPV4StaticAddress
		{
			get
			{
				return new LocalizedString("NeedConfigureIpv4StaticAddressForSecondSubnetWhenFirstSubnetConfiguresIPV4StaticAddress", "ExCFFD09", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00013E50 File Offset: 0x00012050
		public static LocalizedString UserSpecifiedDCIsNotAvailableException(string dc)
		{
			return new LocalizedString("UserSpecifiedDCIsNotAvailableException", "Ex353648", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013E80 File Offset: 0x00012080
		public static LocalizedString RootDataHandlerCount(int count)
		{
			return new LocalizedString("RootDataHandlerCount", "Ex1EDD13", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00013EB4 File Offset: 0x000120B4
		public static LocalizedString PreSetupText
		{
			get
			{
				return new LocalizedString("PreSetupText", "Ex83D051", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013ED4 File Offset: 0x000120D4
		public static LocalizedString SettingArgumentToValue(string argument, string value)
		{
			return new LocalizedString("SettingArgumentToValue", "Ex3D2685", false, true, Strings.ResourceManager, new object[]
			{
				argument,
				value
			});
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00013F07 File Offset: 0x00012107
		public static LocalizedString ModeErrorForUpgrade
		{
			get
			{
				return new LocalizedString("ModeErrorForUpgrade", "ExE4AE75", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00013F25 File Offset: 0x00012125
		public static LocalizedString OrganizationInstallText
		{
			get
			{
				return new LocalizedString("OrganizationInstallText", "ExCD0005", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00013F44 File Offset: 0x00012144
		public static LocalizedString TheFirstPathUnderTheSecondPath(string firstPath, string secondPath)
		{
			return new LocalizedString("TheFirstPathUnderTheSecondPath", "ExCA4270", false, true, Strings.ResourceManager, new object[]
			{
				firstPath,
				secondPath
			});
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00013F77 File Offset: 0x00012177
		public static LocalizedString CannotSpecifyServerCEIPWhenMachineIsNotCleanDuringServerInstallation
		{
			get
			{
				return new LocalizedString("CannotSpecifyServerCEIPWhenMachineIsNotCleanDuringServerInstallation", "Ex181B94", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013F98 File Offset: 0x00012198
		public static LocalizedString UnableToFindBuildVersion1(string xmlPath)
		{
			return new LocalizedString("UnableToFindBuildVersion1", "Ex8B0A6F", false, true, Strings.ResourceManager, new object[]
			{
				xmlPath
			});
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013FC8 File Offset: 0x000121C8
		public static LocalizedString BackupPath(string path)
		{
			return new LocalizedString("BackupPath", "ExEED501", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00013FF8 File Offset: 0x000121F8
		public static LocalizedString PackagePathSetTo(string path)
		{
			return new LocalizedString("PackagePathSetTo", "Ex20E52C", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00014028 File Offset: 0x00012228
		public static LocalizedString ADRelatedError(string error)
		{
			return new LocalizedString("ADRelatedError", "ExC4BD72", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014058 File Offset: 0x00012258
		public static LocalizedString AlwaysWatsonForDebug(string key, string name)
		{
			return new LocalizedString("AlwaysWatsonForDebug", "", false, false, Strings.ResourceManager, new object[]
			{
				key,
				name
			});
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001408B File Offset: 0x0001228B
		public static LocalizedString LanguagePacksToInstall
		{
			get
			{
				return new LocalizedString("LanguagePacksToInstall", "Ex91DD3E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000140AC File Offset: 0x000122AC
		public static LocalizedString LanguagePacksPackagePathNotFound(string path)
		{
			return new LocalizedString("LanguagePacksPackagePathNotFound", "Ex46A348", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000140DC File Offset: 0x000122DC
		public static LocalizedString UnifiedMessagingLanguagePackInstalled(string culture)
		{
			return new LocalizedString("UnifiedMessagingLanguagePackInstalled", "Ex9752C0", false, true, Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001410C File Offset: 0x0001230C
		public static LocalizedString InstalledVersion(Version version)
		{
			return new LocalizedString("InstalledVersion", "Ex47263B", false, true, Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001413B File Offset: 0x0001233B
		public static LocalizedString RemoveSuccessStatus
		{
			get
			{
				return new LocalizedString("RemoveSuccessStatus", "ExB940DB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001415C File Offset: 0x0001235C
		public static LocalizedString SetupWillUseDomainController(string dc)
		{
			return new LocalizedString("SetupWillUseDomainController", "Ex711424", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001418B File Offset: 0x0001238B
		public static LocalizedString UnableToFindLPVersioning
		{
			get
			{
				return new LocalizedString("UnableToFindLPVersioning", "Ex6A6BA1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000141AC File Offset: 0x000123AC
		public static LocalizedString AddRoleAlreadyInstalledError(string installedRoles)
		{
			return new LocalizedString("AddRoleAlreadyInstalledError", "ExDDF583", false, true, Strings.ResourceManager, new object[]
			{
				installedRoles
			});
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000141DC File Offset: 0x000123DC
		public static LocalizedString CommandLineParameterSpecified(string parameter)
		{
			return new LocalizedString("CommandLineParameterSpecified", "Ex5DF93D", false, true, Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001420B File Offset: 0x0001240B
		public static LocalizedString CannotRemoveEnglishUSLanguagePack
		{
			get
			{
				return new LocalizedString("CannotRemoveEnglishUSLanguagePack", "Ex4C25E0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00014229 File Offset: 0x00012429
		public static LocalizedString LanguagePacksToAdd
		{
			get
			{
				return new LocalizedString("LanguagePacksToAdd", "ExC5DCD9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00014248 File Offset: 0x00012448
		public static LocalizedString CannotFindFile(string file)
		{
			return new LocalizedString("CannotFindFile", "Ex622AD5", false, true, Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014277 File Offset: 0x00012477
		public static LocalizedString UpgradeRolesToInstall
		{
			get
			{
				return new LocalizedString("UpgradeRolesToInstall", "Ex024F9F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00014295 File Offset: 0x00012495
		public static LocalizedString WillSkipAMEngineDownloadCheck
		{
			get
			{
				return new LocalizedString("WillSkipAMEngineDownloadCheck", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000142B3 File Offset: 0x000124B3
		public static LocalizedString WillGetConfiguredRolesFromRegistry
		{
			get
			{
				return new LocalizedString("WillGetConfiguredRolesFromRegistry", "Ex23EA15", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000142D1 File Offset: 0x000124D1
		public static LocalizedString MailboxRoleDisplayName
		{
			get
			{
				return new LocalizedString("MailboxRoleDisplayName", "ExD34A06", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000142EF File Offset: 0x000124EF
		public static LocalizedString EnglishUSLanguagePackInstalled
		{
			get
			{
				return new LocalizedString("EnglishUSLanguagePackInstalled", "Ex0C9E29", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001430D File Offset: 0x0001250D
		public static LocalizedString NoRoleSelectedForUninstall
		{
			get
			{
				return new LocalizedString("NoRoleSelectedForUninstall", "Ex29C8F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001432C File Offset: 0x0001252C
		public static LocalizedString ParameterNotValidForCurrentRoles(string parameter)
		{
			return new LocalizedString("ParameterNotValidForCurrentRoles", "", false, false, Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001435B File Offset: 0x0001255B
		public static LocalizedString MustEnableLegacyOutlook
		{
			get
			{
				return new LocalizedString("MustEnableLegacyOutlook", "ExB435E8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00014379 File Offset: 0x00012579
		public static LocalizedString RemoveDatacenterFileText
		{
			get
			{
				return new LocalizedString("RemoveDatacenterFileText", "Ex267B5E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00014397 File Offset: 0x00012597
		public static LocalizedString CannotSpecifyCEIPWhenOrganizationHasE14OrLaterServersDuringPrepareAD
		{
			get
			{
				return new LocalizedString("CannotSpecifyCEIPWhenOrganizationHasE14OrLaterServersDuringPrepareAD", "ExE89E5A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000143B5 File Offset: 0x000125B5
		public static LocalizedString OrgAlreadyHasBridgeheadServers
		{
			get
			{
				return new LocalizedString("OrgAlreadyHasBridgeheadServers", "ExEABAD8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000143D3 File Offset: 0x000125D3
		public static LocalizedString PrerequisiteAnalysis
		{
			get
			{
				return new LocalizedString("PrerequisiteAnalysis", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000143F4 File Offset: 0x000125F4
		public static LocalizedString ExchangeServerFound(string name)
		{
			return new LocalizedString("ExchangeServerFound", "Ex3258E6", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00014423 File Offset: 0x00012623
		public static LocalizedString NeedConfigureIpv4ForSecondSubnetWhenFirstSubnetConfiguresIPV4
		{
			get
			{
				return new LocalizedString("NeedConfigureIpv4ForSecondSubnetWhenFirstSubnetConfiguresIPV4", "Ex21863C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00014441 File Offset: 0x00012641
		public static LocalizedString RemovePreCheckText
		{
			get
			{
				return new LocalizedString("RemovePreCheckText", "Ex9F1060", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001445F File Offset: 0x0001265F
		public static LocalizedString RemoveUmLanguagePackSuccessText
		{
			get
			{
				return new LocalizedString("RemoveUmLanguagePackSuccessText", "ExEB9DB9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014480 File Offset: 0x00012680
		public static LocalizedString AdInitializationStatus(bool status)
		{
			return new LocalizedString("AdInitializationStatus", "ExC4BBB4", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000144B4 File Offset: 0x000126B4
		public static LocalizedString InstallationPathUnderUserProfileInformation(string path)
		{
			return new LocalizedString("InstallationPathUnderUserProfileInformation", "Ex8CC260", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000144E3 File Offset: 0x000126E3
		public static LocalizedString DeterminingOrgPrepParameters
		{
			get
			{
				return new LocalizedString("DeterminingOrgPrepParameters", "Ex86C4E4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00014501 File Offset: 0x00012701
		public static LocalizedString RemoveProgressText
		{
			get
			{
				return new LocalizedString("RemoveProgressText", "Ex0B81D8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001451F File Offset: 0x0001271F
		public static LocalizedString RemoveUmLanguagePackText
		{
			get
			{
				return new LocalizedString("RemoveUmLanguagePackText", "Ex232907", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00014540 File Offset: 0x00012740
		public static LocalizedString UmLanguagePackDirectoryNotAvailable(string directory)
		{
			return new LocalizedString("UmLanguagePackDirectoryNotAvailable", "Ex4E887C", false, true, Strings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014570 File Offset: 0x00012770
		public static LocalizedString UpgradeRoleNotInstalledError(string missingRoles)
		{
			return new LocalizedString("UpgradeRoleNotInstalledError", "Ex51FFDA", false, true, Strings.ResourceManager, new object[]
			{
				missingRoles
			});
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000145A0 File Offset: 0x000127A0
		public static LocalizedString MsiDirectoryNotFound(string path)
		{
			return new LocalizedString("MsiDirectoryNotFound", "Ex0ECC86", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000145D0 File Offset: 0x000127D0
		public static LocalizedString ValidatingRoleOptions(int count)
		{
			return new LocalizedString("ValidatingRoleOptions", "ExA9D934", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00014604 File Offset: 0x00012804
		public static LocalizedString DRServerNotFoundInAD
		{
			get
			{
				return new LocalizedString("DRServerNotFoundInAD", "ExC442F0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00014622 File Offset: 0x00012822
		public static LocalizedString CannotSpecifyServerCEIPWhenGlobalCEIPIsOptedOutDuringServerInstallation
		{
			get
			{
				return new LocalizedString("CannotSpecifyServerCEIPWhenGlobalCEIPIsOptedOutDuringServerInstallation", "Ex2CF821", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00014640 File Offset: 0x00012840
		public static LocalizedString ServerOptDescriptionText
		{
			get
			{
				return new LocalizedString("ServerOptDescriptionText", "Ex8A6675", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001465E File Offset: 0x0001285E
		public static LocalizedString InstallationPathNotSet
		{
			get
			{
				return new LocalizedString("InstallationPathNotSet", "Ex85D389", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001467C File Offset: 0x0001287C
		public static LocalizedString UnexpectedFileFromBundle(string parameter)
		{
			return new LocalizedString("UnexpectedFileFromBundle", "Ex60031E", false, true, Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000146AC File Offset: 0x000128AC
		public static LocalizedString DisplayServerName(string serverName)
		{
			return new LocalizedString("DisplayServerName", "", false, false, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000146DC File Offset: 0x000128DC
		public static LocalizedString InvalidFqdn(string fqdn)
		{
			return new LocalizedString("InvalidFqdn", "", false, false, Strings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001470B File Offset: 0x0001290B
		public static LocalizedString UnableToFindBuildVersion
		{
			get
			{
				return new LocalizedString("UnableToFindBuildVersion", "Ex85B342", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001472C File Offset: 0x0001292C
		public static LocalizedString TargetInstallationDirectory(string directory)
		{
			return new LocalizedString("TargetInstallationDirectory", "ExCB6843", false, true, Strings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001475B File Offset: 0x0001295B
		public static LocalizedString AdminToolsRoleDisplayName
		{
			get
			{
				return new LocalizedString("AdminToolsRoleDisplayName", "Ex841C1E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00014779 File Offset: 0x00012979
		public static LocalizedString UmLanguagePackPackagePathNotSpecified
		{
			get
			{
				return new LocalizedString("UmLanguagePackPackagePathNotSpecified", "ExF5BDA9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00014798 File Offset: 0x00012998
		public static LocalizedString AddUmLanguagePackModeDataHandlerCount(int count)
		{
			return new LocalizedString("AddUmLanguagePackModeDataHandlerCount", "ExC0E9B0", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000147CC File Offset: 0x000129CC
		public static LocalizedString AddCannotChangeTargetDirectoryError
		{
			get
			{
				return new LocalizedString("AddCannotChangeTargetDirectoryError", "Ex1CD429", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000147EA File Offset: 0x000129EA
		public static LocalizedString EdgeRoleInstalledButServerObjectNotFound
		{
			get
			{
				return new LocalizedString("EdgeRoleInstalledButServerObjectNotFound", "ExEAEBA4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00014808 File Offset: 0x00012A08
		public static LocalizedString LanguagePacksNotFound
		{
			get
			{
				return new LocalizedString("LanguagePacksNotFound", "Ex25FA96", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014828 File Offset: 0x00012A28
		public static LocalizedString RunForestPrepInSchemaMasterDomainException(string dom, string site)
		{
			return new LocalizedString("RunForestPrepInSchemaMasterDomainException", "Ex8FD345", false, true, Strings.ResourceManager, new object[]
			{
				dom,
				site
			});
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001485B File Offset: 0x00012A5B
		public static LocalizedString ApplyingDefaultRoleSelectionState
		{
			get
			{
				return new LocalizedString("ApplyingDefaultRoleSelectionState", "Ex0A4DE9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001487C File Offset: 0x00012A7C
		public static LocalizedString LanguagePackagePathSetTo(string path)
		{
			return new LocalizedString("LanguagePackagePathSetTo", "ExA3F043", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000148AC File Offset: 0x00012AAC
		public static LocalizedString SettingArgumentBecauseOfCommandLineParameter(string parameter, string argument)
		{
			return new LocalizedString("SettingArgumentBecauseOfCommandLineParameter", "Ex249244", false, true, Strings.ResourceManager, new object[]
			{
				parameter,
				argument
			});
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000148DF File Offset: 0x00012ADF
		public static LocalizedString MustConfigureIPv4ForFirstSubnet
		{
			get
			{
				return new LocalizedString("MustConfigureIPv4ForFirstSubnet", "ExAC2BAD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014900 File Offset: 0x00012B00
		public static LocalizedString SetupNotFoundInSourceDirError(string fileName)
		{
			return new LocalizedString("SetupNotFoundInSourceDirError", "ExD40EF3", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001492F File Offset: 0x00012B2F
		public static LocalizedString LanguagePacksLogFilePathNotSpecified
		{
			get
			{
				return new LocalizedString("LanguagePacksLogFilePathNotSpecified", "Ex2522A2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001494D File Offset: 0x00012B4D
		public static LocalizedString OrganizationPrereqText
		{
			get
			{
				return new LocalizedString("OrganizationPrereqText", "Ex4CA534", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001496B File Offset: 0x00012B6B
		public static LocalizedString AddFailText
		{
			get
			{
				return new LocalizedString("AddFailText", "ExE604BB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00014989 File Offset: 0x00012B89
		public static LocalizedString RemoveRolesToInstall
		{
			get
			{
				return new LocalizedString("RemoveRolesToInstall", "Ex1C07AA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000149A7 File Offset: 0x00012BA7
		public static LocalizedString NeedConfigureIpv4DHCPForSecondSubnetWhenFirstSubnetConfiguresIPV4DHCP
		{
			get
			{
				return new LocalizedString("NeedConfigureIpv4DHCPForSecondSubnetWhenFirstSubnetConfiguresIPV4DHCP", "ExBE5185", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000149C5 File Offset: 0x00012BC5
		public static LocalizedString MidFileCopyText
		{
			get
			{
				return new LocalizedString("MidFileCopyText", "Ex02AED2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000149E3 File Offset: 0x00012BE3
		public static LocalizedString CentralAdminDatabaseRoleDisplayName
		{
			get
			{
				return new LocalizedString("CentralAdminDatabaseRoleDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00014A04 File Offset: 0x00012C04
		public static LocalizedString SchemaUpdateRequired(bool status)
		{
			return new LocalizedString("SchemaUpdateRequired", "Ex55BD23", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00014A38 File Offset: 0x00012C38
		public static LocalizedString AddGatewayAloneError
		{
			get
			{
				return new LocalizedString("AddGatewayAloneError", "Ex2CDDD2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00014A58 File Offset: 0x00012C58
		public static LocalizedString VersionMismatchWarning(ExchangeBuild version)
		{
			return new LocalizedString("VersionMismatchWarning", "", false, false, Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00014A8C File Offset: 0x00012C8C
		public static LocalizedString CafeRoleDisplayName
		{
			get
			{
				return new LocalizedString("CafeRoleDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00014AAA File Offset: 0x00012CAA
		public static LocalizedString NoServerRolesToInstall
		{
			get
			{
				return new LocalizedString("NoServerRolesToInstall", "Ex7B2B08", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public static LocalizedString DCNotResponding(string dc)
		{
			return new LocalizedString("DCNotResponding", "ExBF2F12", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public static LocalizedString ForestPrepAlreadyRun(string dc)
		{
			return new LocalizedString("ForestPrepAlreadyRun", "Ex46365A", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00014B28 File Offset: 0x00012D28
		public static LocalizedString DCNotInLocalDomain(string dc)
		{
			return new LocalizedString("DCNotInLocalDomain", "Ex5CBBFD", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00014B58 File Offset: 0x00012D58
		public static LocalizedString RemoveUmLanguagePackLogFilePath(string path)
		{
			return new LocalizedString("RemoveUmLanguagePackLogFilePath", "Ex00689F", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00014B88 File Offset: 0x00012D88
		public static LocalizedString AddUmLanguagePackLogFilePath(string path)
		{
			return new LocalizedString("AddUmLanguagePackLogFilePath", "ExFDCD56", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public static LocalizedString ExistingOrganizationName(string name)
		{
			return new LocalizedString("ExistingOrganizationName", "ExD18E39", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00014BE7 File Offset: 0x00012DE7
		public static LocalizedString DRPrereq
		{
			get
			{
				return new LocalizedString("DRPrereq", "Ex09295A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00014C08 File Offset: 0x00012E08
		public static LocalizedString ExceptionWhenDeserializingStateFile(Exception e)
		{
			return new LocalizedString("ExceptionWhenDeserializingStateFile", "ExDA30BB", false, true, Strings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00014C37 File Offset: 0x00012E37
		public static LocalizedString ModeErrorForFreshInstall
		{
			get
			{
				return new LocalizedString("ModeErrorForFreshInstall", "Ex98A60B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014C58 File Offset: 0x00012E58
		public static LocalizedString LanguagePacksVersionFileNotFound(string pathname)
		{
			return new LocalizedString("LanguagePacksVersionFileNotFound", "ExB754D8", false, true, Strings.ResourceManager, new object[]
			{
				pathname
			});
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00014C87 File Offset: 0x00012E87
		public static LocalizedString LegacyRoutingServerNotValid
		{
			get
			{
				return new LocalizedString("LegacyRoutingServerNotValid", "Ex6443D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00014CA5 File Offset: 0x00012EA5
		public static LocalizedString CopyDatacenterFileText
		{
			get
			{
				return new LocalizedString("CopyDatacenterFileText", "Ex45C8F9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00014CC3 File Offset: 0x00012EC3
		public static LocalizedString UnsupportedMode
		{
			get
			{
				return new LocalizedString("UnsupportedMode", "ExA7BB2C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00014CE1 File Offset: 0x00012EE1
		public static LocalizedString ExchangeOrganizationNameRequired
		{
			get
			{
				return new LocalizedString("ExchangeOrganizationNameRequired", "ExA8F9EC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00014CFF File Offset: 0x00012EFF
		public static LocalizedString UpgradePreCheckText
		{
			get
			{
				return new LocalizedString("UpgradePreCheckText", "Ex045128", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014D20 File Offset: 0x00012F20
		public static LocalizedString StateFileVersionMismatch(string versionFromFile, string versionFromContext)
		{
			return new LocalizedString("StateFileVersionMismatch", "Ex1CDE5B", false, true, Strings.ResourceManager, new object[]
			{
				versionFromFile,
				versionFromContext
			});
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00014D54 File Offset: 0x00012F54
		public static LocalizedString NoConfigurationInfoFoundForInstallableUnit(string name)
		{
			return new LocalizedString("NoConfigurationInfoFoundForInstallableUnit", "ExAD450A", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00014D84 File Offset: 0x00012F84
		public static LocalizedString SetupWillRunFromPath(string path)
		{
			return new LocalizedString("SetupWillRunFromPath", "Ex6AF63C", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public static LocalizedString ADDomainConfigVersionHigherThanSetupException(int adDomainConfigVersion, int setupDomainConfigVersion)
		{
			return new LocalizedString("ADDomainConfigVersionHigherThanSetupException", "Ex5653B3", false, true, Strings.ResourceManager, new object[]
			{
				adDomainConfigVersion,
				setupDomainConfigVersion
			});
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public static LocalizedString RemoveLanguagePacksLogFilePath(string path)
		{
			return new LocalizedString("RemoveLanguagePacksLogFilePath", "Ex40F3D2", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00014E24 File Offset: 0x00013024
		public static LocalizedString WarningUpperCase(string warning)
		{
			return new LocalizedString("WarningUpperCase", "", false, false, Strings.ResourceManager, new object[]
			{
				warning
			});
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00014E53 File Offset: 0x00013053
		public static LocalizedString Prereqs
		{
			get
			{
				return new LocalizedString("Prereqs", "Ex24C36F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014E74 File Offset: 0x00013074
		public static LocalizedString UmLanguagePackFullPackagePath(string path)
		{
			return new LocalizedString("UmLanguagePackFullPackagePath", "ExBE820B", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00014EA3 File Offset: 0x000130A3
		public static LocalizedString FailedToReadLCIDFromRegistryError
		{
			get
			{
				return new LocalizedString("FailedToReadLCIDFromRegistryError", "Ex9E48FE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00014EC1 File Offset: 0x000130C1
		public static LocalizedString LanguagePacksDisplayName
		{
			get
			{
				return new LocalizedString("LanguagePacksDisplayName", "ExC1C4F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014EE0 File Offset: 0x000130E0
		public static LocalizedString RoleNotInstalledError(string missingRoles)
		{
			return new LocalizedString("RoleNotInstalledError", "ExF69611", false, true, Strings.ResourceManager, new object[]
			{
				missingRoles
			});
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00014F10 File Offset: 0x00013110
		public static LocalizedString ErrorUpperCase(string error)
		{
			return new LocalizedString("ErrorUpperCase", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00014F40 File Offset: 0x00013140
		public static LocalizedString LanguagePacksBadVersionFormat(string version)
		{
			return new LocalizedString("LanguagePacksBadVersionFormat", "Ex548466", false, true, Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014F70 File Offset: 0x00013170
		public static LocalizedString NoSchemaEntry(string parameter)
		{
			return new LocalizedString("NoSchemaEntry", "", false, false, Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014FA0 File Offset: 0x000131A0
		public static LocalizedString InstallationPathInvalidDriveFormatInformation(string path)
		{
			return new LocalizedString("InstallationPathInvalidDriveFormatInformation", "Ex8B6E7D", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00014FCF File Offset: 0x000131CF
		public static LocalizedString SetupExitsBecauseOfTransientException
		{
			get
			{
				return new LocalizedString("SetupExitsBecauseOfTransientException", "Ex59FD2E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00014FF0 File Offset: 0x000131F0
		public static LocalizedString NoExchangeConfigurationContainerFound(string message)
		{
			return new LocalizedString("NoExchangeConfigurationContainerFound", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00015020 File Offset: 0x00013220
		public static LocalizedString OrganizationAlreadyExists(string name)
		{
			return new LocalizedString("OrganizationAlreadyExists", "ExC887C3", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001504F File Offset: 0x0001324F
		public static LocalizedString DRSuccessText
		{
			get
			{
				return new LocalizedString("DRSuccessText", "ExD9453B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001506D File Offset: 0x0001326D
		public static LocalizedString DRRolesToInstall
		{
			get
			{
				return new LocalizedString("DRRolesToInstall", "Ex5BF89B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001508B File Offset: 0x0001328B
		public static LocalizedString ParametersForTheTaskTitle
		{
			get
			{
				return new LocalizedString("ParametersForTheTaskTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000150AC File Offset: 0x000132AC
		public static LocalizedString DCAlreadySpecified(string dc)
		{
			return new LocalizedString("DCAlreadySpecified", "ExE93D5A", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000150DC File Offset: 0x000132DC
		public static LocalizedString ExchangeServerNotFound(string name)
		{
			return new LocalizedString("ExchangeServerNotFound", "Ex9C871E", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001510C File Offset: 0x0001330C
		public static LocalizedString SchemaMasterDCNotAvailableException(string dc)
		{
			return new LocalizedString("SchemaMasterDCNotAvailableException", "ExD1B80D", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001513B File Offset: 0x0001333B
		public static LocalizedString ADRelatedUnknownError
		{
			get
			{
				return new LocalizedString("ADRelatedUnknownError", "Ex724363", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00015159 File Offset: 0x00013359
		public static LocalizedString AddFailStatus
		{
			get
			{
				return new LocalizedString("AddFailStatus", "Ex16A8F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00015178 File Offset: 0x00013378
		public static LocalizedString BackupKeyIsWrongType(string keyName, string valueName)
		{
			return new LocalizedString("BackupKeyIsWrongType", "ExB275AF", false, true, Strings.ResourceManager, new object[]
			{
				keyName,
				valueName
			});
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000151AB File Offset: 0x000133AB
		public static LocalizedString AddLanguagePacksFailText
		{
			get
			{
				return new LocalizedString("AddLanguagePacksFailText", "ExE1D010", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000151CC File Offset: 0x000133CC
		public static LocalizedString CannotInstallDatacenterRole(string role)
		{
			return new LocalizedString("CannotInstallDatacenterRole", "ExCA2623", false, true, Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000151FB File Offset: 0x000133FB
		public static LocalizedString LanguagePacksInstalledVersionNull
		{
			get
			{
				return new LocalizedString("LanguagePacksInstalledVersionNull", "Ex741310", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001521C File Offset: 0x0001341C
		public static LocalizedString PersistedDomainController(string dc)
		{
			return new LocalizedString("PersistedDomainController", "Ex54F661", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001524C File Offset: 0x0001344C
		public static LocalizedString DomainConfigUpdateRequired(bool status)
		{
			return new LocalizedString("DomainConfigUpdateRequired", "Ex7D6A22", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00015280 File Offset: 0x00013480
		public static LocalizedString SetupExitsBecauseOfLPPathNotFoundException
		{
			get
			{
				return new LocalizedString("SetupExitsBecauseOfLPPathNotFoundException", "Ex19A6EB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001529E File Offset: 0x0001349E
		public static LocalizedString ServerIsProvisioned
		{
			get
			{
				return new LocalizedString("ServerIsProvisioned", "Ex48CE23", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000152BC File Offset: 0x000134BC
		public static LocalizedString NoUmLanguagePackSpecified
		{
			get
			{
				return new LocalizedString("NoUmLanguagePackSpecified", "Ex62D03F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000152DC File Offset: 0x000134DC
		public static LocalizedString WillSearchForAServerObjectForServer(string servername)
		{
			return new LocalizedString("WillSearchForAServerObjectForServer", "Ex6F0B6B", false, true, Strings.ResourceManager, new object[]
			{
				servername
			});
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001530B File Offset: 0x0001350B
		public static LocalizedString PostFileCopyText
		{
			get
			{
				return new LocalizedString("PostFileCopyText", "Ex1E2312", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00015329 File Offset: 0x00013529
		public static LocalizedString ExecutionCompleted
		{
			get
			{
				return new LocalizedString("ExecutionCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00015347 File Offset: 0x00013547
		public static LocalizedString UnknownPreviousVersion
		{
			get
			{
				return new LocalizedString("UnknownPreviousVersion", "ExC34950", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00015365 File Offset: 0x00013565
		public static LocalizedString RemoveFileText
		{
			get
			{
				return new LocalizedString("RemoveFileText", "Ex2F8908", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00015384 File Offset: 0x00013584
		public static LocalizedString AddedConfigurationInfoForInstallableUnit(string name)
		{
			return new LocalizedString("AddedConfigurationInfoForInstallableUnit", "Ex8A9FD8", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000153B3 File Offset: 0x000135B3
		public static LocalizedString MSIIsCurrent
		{
			get
			{
				return new LocalizedString("MSIIsCurrent", "ExFF9A2C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000153D1 File Offset: 0x000135D1
		public static LocalizedString AddPrereq
		{
			get
			{
				return new LocalizedString("AddPrereq", "ExCC8158", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000153EF File Offset: 0x000135EF
		public static LocalizedString RemoveServerRoleText
		{
			get
			{
				return new LocalizedString("RemoveServerRoleText", "ExEB0FD2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00015410 File Offset: 0x00013610
		public static LocalizedString InstallationPathInvalidRootDriveInformation(string path)
		{
			return new LocalizedString("InstallationPathInvalidRootDriveInformation", "ExE1FD64", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00015440 File Offset: 0x00013640
		public static LocalizedString ADOrgConfigVersionHigherThanSetupException(int adOrgConfigVersion, int setupOrgConfigVersion)
		{
			return new LocalizedString("ADOrgConfigVersionHigherThanSetupException", "Ex98A535", false, true, Strings.ResourceManager, new object[]
			{
				adOrgConfigVersion,
				setupOrgConfigVersion
			});
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001547D File Offset: 0x0001367D
		public static LocalizedString DoesNotSupportCEIPForAdminOnlyInstallation
		{
			get
			{
				return new LocalizedString("DoesNotSupportCEIPForAdminOnlyInstallation", "Ex783CB8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001549B File Offset: 0x0001369B
		public static LocalizedString AddLanguagePacksText
		{
			get
			{
				return new LocalizedString("AddLanguagePacksText", "ExDBEEE1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000154BC File Offset: 0x000136BC
		public static LocalizedString AttemptToSearchExchangeServerFailed(string serverName, string message)
		{
			return new LocalizedString("AttemptToSearchExchangeServerFailed", "Ex1E0D45", false, true, Strings.ResourceManager, new object[]
			{
				serverName,
				message
			});
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000154F0 File Offset: 0x000136F0
		public static LocalizedString UninstallModeDataHandlerHandlersAndWorkUnits(int handlers, int workunits)
		{
			return new LocalizedString("UninstallModeDataHandlerHandlersAndWorkUnits", "Ex51B22D", false, true, Strings.ResourceManager, new object[]
			{
				handlers,
				workunits
			});
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001552D File Offset: 0x0001372D
		public static LocalizedString DRPreCheckText
		{
			get
			{
				return new LocalizedString("DRPreCheckText", "Ex1D4658", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001554C File Offset: 0x0001374C
		public static LocalizedString CommandLine(string launcher, string cmdLine)
		{
			return new LocalizedString("CommandLine", "Ex9B88C7", false, true, Strings.ResourceManager, new object[]
			{
				launcher,
				cmdLine
			});
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001557F File Offset: 0x0001377F
		public static LocalizedString AddProgressText
		{
			get
			{
				return new LocalizedString("AddProgressText", "Ex68024D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001559D File Offset: 0x0001379D
		public static LocalizedString DRFailStatus
		{
			get
			{
				return new LocalizedString("DRFailStatus", "Ex910D3E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000155BB File Offset: 0x000137BB
		public static LocalizedString RemoveUmLanguagePackFailStatus
		{
			get
			{
				return new LocalizedString("RemoveUmLanguagePackFailStatus", "ExCA2907", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000155DC File Offset: 0x000137DC
		public static LocalizedString SetupSourceDirectory(string path)
		{
			return new LocalizedString("SetupSourceDirectory", "ExD92313", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001560C File Offset: 0x0001380C
		public static LocalizedString UmLanguagePackNotFoundForCulture(string culture)
		{
			return new LocalizedString("UmLanguagePackNotFoundForCulture", "Ex62501F", false, true, Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001563B File Offset: 0x0001383B
		public static LocalizedString UpgradeIntroductionText
		{
			get
			{
				return new LocalizedString("UpgradeIntroductionText", "ExA3000B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001565C File Offset: 0x0001385C
		public static LocalizedString UmLanguagePackDisplayNameWithCulture(string culture)
		{
			return new LocalizedString("UmLanguagePackDisplayNameWithCulture", "Ex2C411F", false, true, Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001568B File Offset: 0x0001388B
		public static LocalizedString NoUmLanguagePackInstalled
		{
			get
			{
				return new LocalizedString("NoUmLanguagePackInstalled", "Ex157C86", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000156A9 File Offset: 0x000138A9
		public static LocalizedString RemoveUnifiedMessagingServerDescription
		{
			get
			{
				return new LocalizedString("RemoveUnifiedMessagingServerDescription", "Ex379FD8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000156C7 File Offset: 0x000138C7
		public static LocalizedString ClientAccessRoleDisplayName
		{
			get
			{
				return new LocalizedString("ClientAccessRoleDisplayName", "Ex0CDD1A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000156E5 File Offset: 0x000138E5
		public static LocalizedString UpgradeFailStatus
		{
			get
			{
				return new LocalizedString("UpgradeFailStatus", "Ex3F26C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00015704 File Offset: 0x00013904
		public static LocalizedString MsiFilesDirectoryCannotBeChanged(string msiFileDirectory)
		{
			return new LocalizedString("MsiFilesDirectoryCannotBeChanged", "Ex36425E", false, true, Strings.ResourceManager, new object[]
			{
				msiFileDirectory
			});
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00015733 File Offset: 0x00013933
		public static LocalizedString NoRoleSelectedForInstall
		{
			get
			{
				return new LocalizedString("NoRoleSelectedForInstall", "Ex36E433", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00015754 File Offset: 0x00013954
		public static LocalizedString ExchangeConfigurationContainerName(string name)
		{
			return new LocalizedString("ExchangeConfigurationContainerName", "Ex6B0641", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00015783 File Offset: 0x00013983
		public static LocalizedString ExecutionError
		{
			get
			{
				return new LocalizedString("ExecutionError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000157A1 File Offset: 0x000139A1
		public static LocalizedString InstallationLicenseAgreementShortDescription
		{
			get
			{
				return new LocalizedString("InstallationLicenseAgreementShortDescription", "ExB3A6B6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000157C0 File Offset: 0x000139C0
		public static LocalizedString ForestPrepNotRunOrNotReplicatedException(string dc)
		{
			return new LocalizedString("ForestPrepNotRunOrNotReplicatedException", "ExC0B44B", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000157EF File Offset: 0x000139EF
		public static LocalizedString RemoveSuccessText
		{
			get
			{
				return new LocalizedString("RemoveSuccessText", "ExBE5014", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001580D File Offset: 0x00013A0D
		public static LocalizedString InstalledLanguageComment
		{
			get
			{
				return new LocalizedString("InstalledLanguageComment", "Ex20E2AB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001582C File Offset: 0x00013A2C
		public static LocalizedString LPVersioningExtractionFailed(string pathToBundle)
		{
			return new LocalizedString("LPVersioningExtractionFailed", "Ex37586B", false, true, Strings.ResourceManager, new object[]
			{
				pathToBundle
			});
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001585B File Offset: 0x00013A5B
		public static LocalizedString UpgradeFailText
		{
			get
			{
				return new LocalizedString("UpgradeFailText", "Ex6DBDBF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001587C File Offset: 0x00013A7C
		public static LocalizedString MSINotPresent(string path)
		{
			return new LocalizedString("MSINotPresent", "Ex686F6A", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000158AC File Offset: 0x00013AAC
		public static LocalizedString SetupWillUseGlobalCatalog(string gc)
		{
			return new LocalizedString("SetupWillUseGlobalCatalog", "Ex391158", false, true, Strings.ResourceManager, new object[]
			{
				gc
			});
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000158DC File Offset: 0x00013ADC
		public static LocalizedString NotALegacyServer(string name)
		{
			return new LocalizedString("NotALegacyServer", "Ex3C2284", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001590C File Offset: 0x00013B0C
		public static LocalizedString WillGetConfiguredRolesFromServerObject(string servername)
		{
			return new LocalizedString("WillGetConfiguredRolesFromServerObject", "Ex7A23A1", false, true, Strings.ResourceManager, new object[]
			{
				servername
			});
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001593B File Offset: 0x00013B3B
		public static LocalizedString PreFileCopyText
		{
			get
			{
				return new LocalizedString("PreFileCopyText", "Ex22878B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001595C File Offset: 0x00013B5C
		public static LocalizedString UninstallModeDataHandlerCount(int count)
		{
			return new LocalizedString("UninstallModeDataHandlerCount", "Ex60313D", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00015990 File Offset: 0x00013B90
		public static LocalizedString UnknownDestinationPath
		{
			get
			{
				return new LocalizedString("UnknownDestinationPath", "Ex264D80", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000159AE File Offset: 0x00013BAE
		public static LocalizedString UpgradeServerRoleText
		{
			get
			{
				return new LocalizedString("UpgradeServerRoleText", "ExE0C870", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000159CC File Offset: 0x00013BCC
		public static LocalizedString GlobalOptDescriptionText
		{
			get
			{
				return new LocalizedString("GlobalOptDescriptionText", "Ex12F431", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000159EC File Offset: 0x00013BEC
		public static LocalizedString WillExecuteHighLevelTask(string task)
		{
			return new LocalizedString("WillExecuteHighLevelTask", "Ex532305", false, true, Strings.ResourceManager, new object[]
			{
				task
			});
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00015A1C File Offset: 0x00013C1C
		public static LocalizedString BackupVersion(Version version)
		{
			return new LocalizedString("BackupVersion", "ExF72BC4", false, true, Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00015A4B File Offset: 0x00013C4B
		public static LocalizedString AddServerRoleText
		{
			get
			{
				return new LocalizedString("AddServerRoleText", "Ex69B15D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00015A6C File Offset: 0x00013C6C
		public static LocalizedString OrgConfigUpdateRequired(bool status)
		{
			return new LocalizedString("OrgConfigUpdateRequired", "Ex5E38B5", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public static LocalizedString MaintenanceIntroduction
		{
			get
			{
				return new LocalizedString("MaintenanceIntroduction", "ExDD5A89", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00015ABE File Offset: 0x00013CBE
		public static LocalizedString UmLanguagePathLogFilePathNotSpecified
		{
			get
			{
				return new LocalizedString("UmLanguagePathLogFilePathNotSpecified", "ExC64CD8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00015ADC File Offset: 0x00013CDC
		public static LocalizedString InstallModeDataHandlerCount(int count)
		{
			return new LocalizedString("InstallModeDataHandlerCount", "Ex5AA6C0", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00015B10 File Offset: 0x00013D10
		public static LocalizedString UpgradeProgressText
		{
			get
			{
				return new LocalizedString("UpgradeProgressText", "ExAE01D4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00015B30 File Offset: 0x00013D30
		public static LocalizedString DCNameNotValid(string dc)
		{
			return new LocalizedString("DCNameNotValid", "Ex46E1D1", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00015B60 File Offset: 0x00013D60
		public static LocalizedString GCChosen(string gc)
		{
			return new LocalizedString("GCChosen", "Ex936DF4", false, true, Strings.ResourceManager, new object[]
			{
				gc
			});
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00015B8F File Offset: 0x00013D8F
		public static LocalizedString ADHasBeenPrepared
		{
			get
			{
				return new LocalizedString("ADHasBeenPrepared", "Ex04700F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00015BB0 File Offset: 0x00013DB0
		public static LocalizedString SchemaMasterIsLocalDC(string dc)
		{
			return new LocalizedString("SchemaMasterIsLocalDC", "ExCA1161", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00015BE0 File Offset: 0x00013DE0
		public static LocalizedString InstallationPathInvalidInformation(string path)
		{
			return new LocalizedString("InstallationPathInvalidInformation", "Ex5EEC52", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00015C10 File Offset: 0x00013E10
		public static LocalizedString InvalidOrganizationName(string name)
		{
			return new LocalizedString("InvalidOrganizationName", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00015C3F File Offset: 0x00013E3F
		public static LocalizedString SpecifyExchangeOrganizationName
		{
			get
			{
				return new LocalizedString("SpecifyExchangeOrganizationName", "Ex31CAD7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00015C5D File Offset: 0x00013E5D
		public static LocalizedString UpgradeMustUseBootStrapper
		{
			get
			{
				return new LocalizedString("UpgradeMustUseBootStrapper", "Ex826246", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00015C7B File Offset: 0x00013E7B
		public static LocalizedString AddIntroductionText
		{
			get
			{
				return new LocalizedString("AddIntroductionText", "Ex5B0258", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00015C99 File Offset: 0x00013E99
		public static LocalizedString UpgradeSuccessStatus
		{
			get
			{
				return new LocalizedString("UpgradeSuccessStatus", "Ex7794F9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00015CB8 File Offset: 0x00013EB8
		public static LocalizedString NonCultureRegistryEntryFound(string value)
		{
			return new LocalizedString("NonCultureRegistryEntryFound", "ExFC9714", false, true, Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00015CE7 File Offset: 0x00013EE7
		public static LocalizedString LegacyServerNameRequired
		{
			get
			{
				return new LocalizedString("LegacyServerNameRequired", "Ex3682B9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00015D05 File Offset: 0x00013F05
		public static LocalizedString TheCurrentServerHasNoExchangeBits
		{
			get
			{
				return new LocalizedString("TheCurrentServerHasNoExchangeBits", "Ex5D0ED6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00015D23 File Offset: 0x00013F23
		public static LocalizedString UpgradeSuccessText
		{
			get
			{
				return new LocalizedString("UpgradeSuccessText", "ExBDE2A1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00015D41 File Offset: 0x00013F41
		public static LocalizedString BridgeheadRoleDisplayName
		{
			get
			{
				return new LocalizedString("BridgeheadRoleDisplayName", "Ex1A18F6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00015D5F File Offset: 0x00013F5F
		public static LocalizedString WaitForForestPrepReplicationToLocalDomainException
		{
			get
			{
				return new LocalizedString("WaitForForestPrepReplicationToLocalDomainException", "Ex34FF80", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00015D80 File Offset: 0x00013F80
		public static LocalizedString ForestPrepNotRun(string dc)
		{
			return new LocalizedString("ForestPrepNotRun", "Ex048927", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00015DAF File Offset: 0x00013FAF
		public static LocalizedString RemovePrereq
		{
			get
			{
				return new LocalizedString("RemovePrereq", "ExAA8B8F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public static LocalizedString InvalidExchangeOrganizationName(string message)
		{
			return new LocalizedString("InvalidExchangeOrganizationName", "ExE89C70", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00015E00 File Offset: 0x00014000
		public static LocalizedString UmLanguagePackNotInstalledForCulture(string culture)
		{
			return new LocalizedString("UmLanguagePackNotInstalledForCulture", "ExB292FC", false, true, Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00015E2F File Offset: 0x0001402F
		public static LocalizedString FreshIntroductionText
		{
			get
			{
				return new LocalizedString("FreshIntroductionText", "Ex5488AF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00015E50 File Offset: 0x00014050
		public static LocalizedString UserSpecifiedDCDoesNotExistException(string dc)
		{
			return new LocalizedString("UserSpecifiedDCDoesNotExistException", "ExE7D598", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00015E80 File Offset: 0x00014080
		public static LocalizedString LocalServerNameInvalid(string name)
		{
			return new LocalizedString("LocalServerNameInvalid", "Ex834285", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00015EB0 File Offset: 0x000140B0
		public static LocalizedString UpgradeModeDataHandlerHandlersAndWorkUnits(int handlers, int workunits)
		{
			return new LocalizedString("UpgradeModeDataHandlerHandlersAndWorkUnits", "Ex10F94B", false, true, Strings.ResourceManager, new object[]
			{
				handlers,
				workunits
			});
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00015EF0 File Offset: 0x000140F0
		public static LocalizedString CannotFindPath(string path)
		{
			return new LocalizedString("CannotFindPath", "Ex376692", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00015F20 File Offset: 0x00014120
		public static LocalizedString GCAlreadySpecified(string gc)
		{
			return new LocalizedString("GCAlreadySpecified", "ExF68485", false, true, Strings.ResourceManager, new object[]
			{
				gc
			});
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00015F4F File Offset: 0x0001414F
		public static LocalizedString UnifiedMessagingRoleDisplayName
		{
			get
			{
				return new LocalizedString("UnifiedMessagingRoleDisplayName", "ExCB5C80", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00015F6D File Offset: 0x0001416D
		public static LocalizedString LanguagePackPathException
		{
			get
			{
				return new LocalizedString("LanguagePackPathException", "ExC46EE5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00015F8C File Offset: 0x0001418C
		public static LocalizedString MsiFileNotFound(string path, string file)
		{
			return new LocalizedString("MsiFileNotFound", "Ex55B863", false, true, Strings.ResourceManager, new object[]
			{
				path,
				file
			});
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00015FC0 File Offset: 0x000141C0
		public static LocalizedString AdminToolCannotBeUninstalledWhenSomeRolesRemained(string admintools)
		{
			return new LocalizedString("AdminToolCannotBeUninstalledWhenSomeRolesRemained", "ExBBE499", false, true, Strings.ResourceManager, new object[]
			{
				admintools
			});
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00015FF0 File Offset: 0x000141F0
		public static LocalizedString DRRoleAlreadyInstalledError(string installedRoles)
		{
			return new LocalizedString("DRRoleAlreadyInstalledError", "Ex4104DB", false, true, Strings.ResourceManager, new object[]
			{
				installedRoles
			});
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001601F File Offset: 0x0001421F
		public static LocalizedString GatewayRoleDisplayName
		{
			get
			{
				return new LocalizedString("GatewayRoleDisplayName", "Ex5EE0FB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00016040 File Offset: 0x00014240
		public static LocalizedString RemoveUmLanguagePackModeDataHandlerCount(int count)
		{
			return new LocalizedString("RemoveUmLanguagePackModeDataHandlerCount", "Ex7DD30B", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00016074 File Offset: 0x00014274
		public static LocalizedString WillDisableAMFiltering
		{
			get
			{
				return new LocalizedString("WillDisableAMFiltering", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00016092 File Offset: 0x00014292
		public static LocalizedString TheCurrentServerHasExchangeBits
		{
			get
			{
				return new LocalizedString("TheCurrentServerHasExchangeBits", "ExA2F1FB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000160B0 File Offset: 0x000142B0
		public static LocalizedString BackupKeyInaccessible(string keyName)
		{
			return new LocalizedString("BackupKeyInaccessible", "Ex32CF3E", false, true, Strings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000160DF File Offset: 0x000142DF
		public static LocalizedString MonitoringRoleDisplayName
		{
			get
			{
				return new LocalizedString("MonitoringRoleDisplayName", "Ex020ED3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000160FD File Offset: 0x000142FD
		public static LocalizedString OrgAlreadyHasMailboxServers
		{
			get
			{
				return new LocalizedString("OrgAlreadyHasMailboxServers", "Ex63EAA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001611B File Offset: 0x0001431B
		public static LocalizedString LanguagePackPathNotFoundError
		{
			get
			{
				return new LocalizedString("LanguagePackPathNotFoundError", "Ex17AD9D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00016139 File Offset: 0x00014339
		public static LocalizedString MustConfigureIPv6ForFirstSubnet
		{
			get
			{
				return new LocalizedString("MustConfigureIPv6ForFirstSubnet", "Ex864617", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00016157 File Offset: 0x00014357
		public static LocalizedString WillRestartSetupUI
		{
			get
			{
				return new LocalizedString("WillRestartSetupUI", "Ex7B8552", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00016178 File Offset: 0x00014378
		public static LocalizedString UserSpecifiedDCIsNotSchemaMasterException(string dc)
		{
			return new LocalizedString("UserSpecifiedDCIsNotSchemaMasterException", "Ex8B7120", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000161A7 File Offset: 0x000143A7
		public static LocalizedString RemoveFailStatus
		{
			get
			{
				return new LocalizedString("RemoveFailStatus", "ExCBE89C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000161C5 File Offset: 0x000143C5
		public static LocalizedString AddConflictedRolesError
		{
			get
			{
				return new LocalizedString("AddConflictedRolesError", "ExDD7345", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000161E3 File Offset: 0x000143E3
		public static LocalizedString SchemaMasterAvailable
		{
			get
			{
				return new LocalizedString("SchemaMasterAvailable", "ExB09157", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00016201 File Offset: 0x00014401
		public static LocalizedString DRFailText
		{
			get
			{
				return new LocalizedString("DRFailText", "Ex53A96A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00016220 File Offset: 0x00014420
		public static LocalizedString UmLanguagePackFileNotFound(string file)
		{
			return new LocalizedString("UmLanguagePackFileNotFound", "Ex564938", false, true, Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016250 File Offset: 0x00014450
		public static LocalizedString NameValueFormat(string name, string value)
		{
			return new LocalizedString("NameValueFormat", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00016283 File Offset: 0x00014483
		public static LocalizedString UpgradeIntroduction
		{
			get
			{
				return new LocalizedString("UpgradeIntroduction", "Ex84D44A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000162A4 File Offset: 0x000144A4
		public static LocalizedString SettingOrganizationName(string name)
		{
			return new LocalizedString("SettingOrganizationName", "Ex095249", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000162D3 File Offset: 0x000144D3
		public static LocalizedString AddPreCheckText
		{
			get
			{
				return new LocalizedString("AddPreCheckText", "Ex14D2FA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000162F4 File Offset: 0x000144F4
		public static LocalizedString InstallationModeSetTo(string mode)
		{
			return new LocalizedString("InstallationModeSetTo", "Ex3AFFFA", false, true, Strings.ResourceManager, new object[]
			{
				mode
			});
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00016323 File Offset: 0x00014523
		public static LocalizedString SourceDirNotSpecifiedError
		{
			get
			{
				return new LocalizedString("SourceDirNotSpecifiedError", "Ex5FD860", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00016341 File Offset: 0x00014541
		public static LocalizedString FreshIntroduction
		{
			get
			{
				return new LocalizedString("FreshIntroduction", "Ex714EFE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001635F File Offset: 0x0001455F
		public static LocalizedString SchemaMasterDCNotFoundException
		{
			get
			{
				return new LocalizedString("SchemaMasterDCNotFoundException", "ExD715F3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001637D File Offset: 0x0001457D
		public static LocalizedString ADDriverBoundToAdam
		{
			get
			{
				return new LocalizedString("ADDriverBoundToAdam", "ExB03DEE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001639C File Offset: 0x0001459C
		public static LocalizedString InvalidMaximumRecordNumber(int maximumNumberToLog)
		{
			return new LocalizedString("InvalidMaximumRecordNumber", "", false, false, Strings.ResourceManager, new object[]
			{
				maximumNumberToLog
			});
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000163D0 File Offset: 0x000145D0
		public static LocalizedString CabUtilityWrapperError
		{
			get
			{
				return new LocalizedString("CabUtilityWrapperError", "ExED6BD4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000163EE File Offset: 0x000145EE
		public static LocalizedString SchemaMasterNotAvailable
		{
			get
			{
				return new LocalizedString("SchemaMasterNotAvailable", "ExAF3B47", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001640C File Offset: 0x0001460C
		public static LocalizedString NoExchangeOrganizationContainerFound(string message)
		{
			return new LocalizedString("NoExchangeOrganizationContainerFound", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001643B File Offset: 0x0001463B
		public static LocalizedString OSPRoleDisplayName
		{
			get
			{
				return new LocalizedString("OSPRoleDisplayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00016459 File Offset: 0x00014659
		public static LocalizedString UpgradeLicenseAgreementShortDescription
		{
			get
			{
				return new LocalizedString("UpgradeLicenseAgreementShortDescription", "ExAE3253", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00016478 File Offset: 0x00014678
		public static LocalizedString AddLanguagePacksLogFilePath(string path)
		{
			return new LocalizedString("AddLanguagePacksLogFilePath", "Ex6A1CD4", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000164A7 File Offset: 0x000146A7
		public static LocalizedString DRSuccessStatus
		{
			get
			{
				return new LocalizedString("DRSuccessStatus", "Ex9EE99D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000164C8 File Offset: 0x000146C8
		public static LocalizedString DRModeDataHandlerCount(int count)
		{
			return new LocalizedString("DRModeDataHandlerCount", "Ex274B3E", false, true, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000164FC File Offset: 0x000146FC
		public static LocalizedString ExchangeOrganizationContainerName(string name)
		{
			return new LocalizedString("ExchangeOrganizationContainerName", "Ex99A953", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001652B File Offset: 0x0001472B
		public static LocalizedString CopyFileText
		{
			get
			{
				return new LocalizedString("CopyFileText", "ExFC3446", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00016549 File Offset: 0x00014749
		public static LocalizedString WillNotStartTransportService
		{
			get
			{
				return new LocalizedString("WillNotStartTransportService", "ExE9F845", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00016567 File Offset: 0x00014767
		public static LocalizedString RemoveMESOObjectLink
		{
			get
			{
				return new LocalizedString("RemoveMESOObjectLink", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00016585 File Offset: 0x00014785
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040001BE RID: 446
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(175);

		// Token: 0x040001BF RID: 447
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000075 RID: 117
		public enum IDs : uint
		{
			// Token: 0x040001C1 RID: 449
			ChoosingGlobalCatalog = 436210406U,
			// Token: 0x040001C2 RID: 450
			LanguagePacksUpToDate = 4081297114U,
			// Token: 0x040001C3 RID: 451
			PostSetupText = 2029598378U,
			// Token: 0x040001C4 RID: 452
			CouldNotDeserializeStateFile = 3976870610U,
			// Token: 0x040001C5 RID: 453
			AddLanguagePacksSuccessText = 590640951U,
			// Token: 0x040001C6 RID: 454
			CopyLanguagePacksDisplayName = 944044270U,
			// Token: 0x040001C7 RID: 455
			UmLanguagePacksToRemove = 800186353U,
			// Token: 0x040001C8 RID: 456
			DRServerRoleText = 3870436312U,
			// Token: 0x040001C9 RID: 457
			AddSuccessText = 2394569239U,
			// Token: 0x040001CA RID: 458
			RemoveIntroductionText = 4289798847U,
			// Token: 0x040001CB RID: 459
			HasConfiguredRoles = 406751805U,
			// Token: 0x040001CC RID: 460
			AddUmLanguagePackText = 776586043U,
			// Token: 0x040001CD RID: 461
			SetupRebootRequired = 4258824323U,
			// Token: 0x040001CE RID: 462
			UmLanguagePackDisplayName = 3988419612U,
			// Token: 0x040001CF RID: 463
			UnifiedMessagingRoleIsRequiredForLanguagePackInstalls = 318591995U,
			// Token: 0x040001D0 RID: 464
			FrontendTransportRoleDisplayName = 3603864464U,
			// Token: 0x040001D1 RID: 465
			UpgradePrereq = 3211475425U,
			// Token: 0x040001D2 RID: 466
			CentralAdminFrontEndRoleDisplayName = 3150196311U,
			// Token: 0x040001D3 RID: 467
			PreConfigurationDisplayName = 1623683896U,
			// Token: 0x040001D4 RID: 468
			RemoveFailText = 523317789U,
			// Token: 0x040001D5 RID: 469
			AddSuccessStatus = 1831888326U,
			// Token: 0x040001D6 RID: 470
			PickingDomainController = 1377162733U,
			// Token: 0x040001D7 RID: 471
			UmLanguagePacksToAdd = 237451456U,
			// Token: 0x040001D8 RID: 472
			LPVersioningInvalidValue = 3712765806U,
			// Token: 0x040001D9 RID: 473
			LanguagePacksPackagePathNotSpecified = 1055111548U,
			// Token: 0x040001DA RID: 474
			AddRolesToInstall = 2144854482U,
			// Token: 0x040001DB RID: 475
			AddOtherRolesError = 2493777864U,
			// Token: 0x040001DC RID: 476
			NeedConfigureIpv6ForSecondSubnetWhenFirstSubnetConfiguresIPV6 = 35176418U,
			// Token: 0x040001DD RID: 477
			ModeErrorForDisasterRecovery = 4234837096U,
			// Token: 0x040001DE RID: 478
			CannotSpecifyIndustryTypeWhenOrgIsUpToDateDuringServerInstallation = 3904063070U,
			// Token: 0x040001DF RID: 479
			RemoveUmLanguagePackFailText = 2285919320U,
			// Token: 0x040001E0 RID: 480
			ChoosingDomainController = 1771160928U,
			// Token: 0x040001E1 RID: 481
			CentralAdminRoleDisplayName = 3069688065U,
			// Token: 0x040001E2 RID: 482
			NeedConfigureIpv4StaticAddressForSecondSubnetWhenFirstSubnetConfiguresIPV4StaticAddress = 2768015112U,
			// Token: 0x040001E3 RID: 483
			PreSetupText = 1833305185U,
			// Token: 0x040001E4 RID: 484
			ModeErrorForUpgrade = 2815048144U,
			// Token: 0x040001E5 RID: 485
			OrganizationInstallText = 261522387U,
			// Token: 0x040001E6 RID: 486
			CannotSpecifyServerCEIPWhenMachineIsNotCleanDuringServerInstallation = 415144177U,
			// Token: 0x040001E7 RID: 487
			LanguagePacksToInstall = 1893181714U,
			// Token: 0x040001E8 RID: 488
			RemoveSuccessStatus = 1778403425U,
			// Token: 0x040001E9 RID: 489
			UnableToFindLPVersioning = 4061842413U,
			// Token: 0x040001EA RID: 490
			CannotRemoveEnglishUSLanguagePack = 1048539684U,
			// Token: 0x040001EB RID: 491
			LanguagePacksToAdd = 3469958538U,
			// Token: 0x040001EC RID: 492
			UpgradeRolesToInstall = 3908217517U,
			// Token: 0x040001ED RID: 493
			WillSkipAMEngineDownloadCheck = 3473605283U,
			// Token: 0x040001EE RID: 494
			WillGetConfiguredRolesFromRegistry = 1841681484U,
			// Token: 0x040001EF RID: 495
			MailboxRoleDisplayName = 3319509477U,
			// Token: 0x040001F0 RID: 496
			EnglishUSLanguagePackInstalled = 3566628291U,
			// Token: 0x040001F1 RID: 497
			NoRoleSelectedForUninstall = 2515305841U,
			// Token: 0x040001F2 RID: 498
			MustEnableLegacyOutlook = 2876792450U,
			// Token: 0x040001F3 RID: 499
			RemoveDatacenterFileText = 1610115158U,
			// Token: 0x040001F4 RID: 500
			CannotSpecifyCEIPWhenOrganizationHasE14OrLaterServersDuringPrepareAD = 4024296328U,
			// Token: 0x040001F5 RID: 501
			OrgAlreadyHasBridgeheadServers = 1416621619U,
			// Token: 0x040001F6 RID: 502
			PrerequisiteAnalysis = 176205814U,
			// Token: 0x040001F7 RID: 503
			NeedConfigureIpv4ForSecondSubnetWhenFirstSubnetConfiguresIPV4 = 1959016418U,
			// Token: 0x040001F8 RID: 504
			RemovePreCheckText = 379286002U,
			// Token: 0x040001F9 RID: 505
			RemoveUmLanguagePackSuccessText = 590609003U,
			// Token: 0x040001FA RID: 506
			DeterminingOrgPrepParameters = 1869709987U,
			// Token: 0x040001FB RID: 507
			RemoveProgressText = 3671143606U,
			// Token: 0x040001FC RID: 508
			RemoveUmLanguagePackText = 1281959852U,
			// Token: 0x040001FD RID: 509
			DRServerNotFoundInAD = 2770538346U,
			// Token: 0x040001FE RID: 510
			CannotSpecifyServerCEIPWhenGlobalCEIPIsOptedOutDuringServerInstallation = 1120765526U,
			// Token: 0x040001FF RID: 511
			ServerOptDescriptionText = 3535416533U,
			// Token: 0x04000200 RID: 512
			InstallationPathNotSet = 2156205750U,
			// Token: 0x04000201 RID: 513
			UnableToFindBuildVersion = 871634395U,
			// Token: 0x04000202 RID: 514
			AdminToolsRoleDisplayName = 3002787153U,
			// Token: 0x04000203 RID: 515
			UmLanguagePackPackagePathNotSpecified = 3235153535U,
			// Token: 0x04000204 RID: 516
			AddCannotChangeTargetDirectoryError = 1136733772U,
			// Token: 0x04000205 RID: 517
			EdgeRoleInstalledButServerObjectNotFound = 2335571147U,
			// Token: 0x04000206 RID: 518
			LanguagePacksNotFound = 1016568891U,
			// Token: 0x04000207 RID: 519
			ApplyingDefaultRoleSelectionState = 2203024520U,
			// Token: 0x04000208 RID: 520
			MustConfigureIPv4ForFirstSubnet = 2001599932U,
			// Token: 0x04000209 RID: 521
			LanguagePacksLogFilePathNotSpecified = 2161842580U,
			// Token: 0x0400020A RID: 522
			OrganizationPrereqText = 1640598683U,
			// Token: 0x0400020B RID: 523
			AddFailText = 3360917926U,
			// Token: 0x0400020C RID: 524
			RemoveRolesToInstall = 1124277255U,
			// Token: 0x0400020D RID: 525
			NeedConfigureIpv4DHCPForSecondSubnetWhenFirstSubnetConfiguresIPV4DHCP = 968377506U,
			// Token: 0x0400020E RID: 526
			MidFileCopyText = 1772477316U,
			// Token: 0x0400020F RID: 527
			CentralAdminDatabaseRoleDisplayName = 2913262092U,
			// Token: 0x04000210 RID: 528
			AddGatewayAloneError = 3003755610U,
			// Token: 0x04000211 RID: 529
			CafeRoleDisplayName = 55657320U,
			// Token: 0x04000212 RID: 530
			NoServerRolesToInstall = 3106188069U,
			// Token: 0x04000213 RID: 531
			DRPrereq = 3438721327U,
			// Token: 0x04000214 RID: 532
			ModeErrorForFreshInstall = 2059503147U,
			// Token: 0x04000215 RID: 533
			LegacyRoutingServerNotValid = 1931019259U,
			// Token: 0x04000216 RID: 534
			CopyDatacenterFileText = 1405894953U,
			// Token: 0x04000217 RID: 535
			UnsupportedMode = 3288868438U,
			// Token: 0x04000218 RID: 536
			ExchangeOrganizationNameRequired = 1947354702U,
			// Token: 0x04000219 RID: 537
			UpgradePreCheckText = 1144432722U,
			// Token: 0x0400021A RID: 538
			Prereqs = 55662122U,
			// Token: 0x0400021B RID: 539
			FailedToReadLCIDFromRegistryError = 724167955U,
			// Token: 0x0400021C RID: 540
			LanguagePacksDisplayName = 2114641061U,
			// Token: 0x0400021D RID: 541
			SetupExitsBecauseOfTransientException = 3856638130U,
			// Token: 0x0400021E RID: 542
			DRSuccessText = 3783483884U,
			// Token: 0x0400021F RID: 543
			DRRolesToInstall = 2041466059U,
			// Token: 0x04000220 RID: 544
			ParametersForTheTaskTitle = 3915936117U,
			// Token: 0x04000221 RID: 545
			ADRelatedUnknownError = 2030347010U,
			// Token: 0x04000222 RID: 546
			AddFailStatus = 16560395U,
			// Token: 0x04000223 RID: 547
			AddLanguagePacksFailText = 1439738700U,
			// Token: 0x04000224 RID: 548
			LanguagePacksInstalledVersionNull = 4069246837U,
			// Token: 0x04000225 RID: 549
			SetupExitsBecauseOfLPPathNotFoundException = 1695043038U,
			// Token: 0x04000226 RID: 550
			ServerIsProvisioned = 4175779855U,
			// Token: 0x04000227 RID: 551
			NoUmLanguagePackSpecified = 3668657674U,
			// Token: 0x04000228 RID: 552
			PostFileCopyText = 209657916U,
			// Token: 0x04000229 RID: 553
			ExecutionCompleted = 2127358907U,
			// Token: 0x0400022A RID: 554
			UnknownPreviousVersion = 3830028115U,
			// Token: 0x0400022B RID: 555
			RemoveFileText = 3751538685U,
			// Token: 0x0400022C RID: 556
			MSIIsCurrent = 2949666774U,
			// Token: 0x0400022D RID: 557
			AddPrereq = 139720102U,
			// Token: 0x0400022E RID: 558
			RemoveServerRoleText = 3212518314U,
			// Token: 0x0400022F RID: 559
			DoesNotSupportCEIPForAdminOnlyInstallation = 1066611882U,
			// Token: 0x04000230 RID: 560
			AddLanguagePacksText = 4291904420U,
			// Token: 0x04000231 RID: 561
			DRPreCheckText = 155941904U,
			// Token: 0x04000232 RID: 562
			AddProgressText = 357116433U,
			// Token: 0x04000233 RID: 563
			DRFailStatus = 1940285462U,
			// Token: 0x04000234 RID: 564
			RemoveUmLanguagePackFailStatus = 1540748327U,
			// Token: 0x04000235 RID: 565
			UpgradeIntroductionText = 2888945725U,
			// Token: 0x04000236 RID: 566
			NoUmLanguagePackInstalled = 3353325242U,
			// Token: 0x04000237 RID: 567
			RemoveUnifiedMessagingServerDescription = 806279811U,
			// Token: 0x04000238 RID: 568
			ClientAccessRoleDisplayName = 3229808788U,
			// Token: 0x04000239 RID: 569
			UpgradeFailStatus = 2809596612U,
			// Token: 0x0400023A RID: 570
			NoRoleSelectedForInstall = 1627958870U,
			// Token: 0x0400023B RID: 571
			ExecutionError = 1165007882U,
			// Token: 0x0400023C RID: 572
			InstallationLicenseAgreementShortDescription = 3076164745U,
			// Token: 0x0400023D RID: 573
			RemoveSuccessText = 3635986314U,
			// Token: 0x0400023E RID: 574
			InstalledLanguageComment = 1223959701U,
			// Token: 0x0400023F RID: 575
			UpgradeFailText = 3791960239U,
			// Token: 0x04000240 RID: 576
			PreFileCopyText = 2036054501U,
			// Token: 0x04000241 RID: 577
			UnknownDestinationPath = 3001810281U,
			// Token: 0x04000242 RID: 578
			UpgradeServerRoleText = 3520674066U,
			// Token: 0x04000243 RID: 579
			GlobalOptDescriptionText = 2572849171U,
			// Token: 0x04000244 RID: 580
			AddServerRoleText = 1835758957U,
			// Token: 0x04000245 RID: 581
			MaintenanceIntroduction = 213042017U,
			// Token: 0x04000246 RID: 582
			UmLanguagePathLogFilePathNotSpecified = 3361199071U,
			// Token: 0x04000247 RID: 583
			UpgradeProgressText = 1232809750U,
			// Token: 0x04000248 RID: 584
			ADHasBeenPrepared = 2702857678U,
			// Token: 0x04000249 RID: 585
			SpecifyExchangeOrganizationName = 783977028U,
			// Token: 0x0400024A RID: 586
			UpgradeMustUseBootStrapper = 2915089885U,
			// Token: 0x0400024B RID: 587
			AddIntroductionText = 1655961090U,
			// Token: 0x0400024C RID: 588
			UpgradeSuccessStatus = 522237759U,
			// Token: 0x0400024D RID: 589
			LegacyServerNameRequired = 48624620U,
			// Token: 0x0400024E RID: 590
			TheCurrentServerHasNoExchangeBits = 2979515445U,
			// Token: 0x0400024F RID: 591
			UpgradeSuccessText = 4193588254U,
			// Token: 0x04000250 RID: 592
			BridgeheadRoleDisplayName = 1134594294U,
			// Token: 0x04000251 RID: 593
			WaitForForestPrepReplicationToLocalDomainException = 966578211U,
			// Token: 0x04000252 RID: 594
			RemovePrereq = 49080419U,
			// Token: 0x04000253 RID: 595
			FreshIntroductionText = 915558695U,
			// Token: 0x04000254 RID: 596
			UnifiedMessagingRoleDisplayName = 126744571U,
			// Token: 0x04000255 RID: 597
			LanguagePackPathException = 186454365U,
			// Token: 0x04000256 RID: 598
			GatewayRoleDisplayName = 2320733697U,
			// Token: 0x04000257 RID: 599
			WillDisableAMFiltering = 3864515574U,
			// Token: 0x04000258 RID: 600
			TheCurrentServerHasExchangeBits = 4222861002U,
			// Token: 0x04000259 RID: 601
			MonitoringRoleDisplayName = 2368998453U,
			// Token: 0x0400025A RID: 602
			OrgAlreadyHasMailboxServers = 3224205240U,
			// Token: 0x0400025B RID: 603
			LanguagePackPathNotFoundError = 3656576577U,
			// Token: 0x0400025C RID: 604
			MustConfigureIPv6ForFirstSubnet = 1435960826U,
			// Token: 0x0400025D RID: 605
			WillRestartSetupUI = 2370029120U,
			// Token: 0x0400025E RID: 606
			RemoveFailStatus = 3059971080U,
			// Token: 0x0400025F RID: 607
			AddConflictedRolesError = 2070315917U,
			// Token: 0x04000260 RID: 608
			SchemaMasterAvailable = 3504188790U,
			// Token: 0x04000261 RID: 609
			DRFailText = 2214617625U,
			// Token: 0x04000262 RID: 610
			UpgradeIntroduction = 1095004458U,
			// Token: 0x04000263 RID: 611
			AddPreCheckText = 1616603373U,
			// Token: 0x04000264 RID: 612
			SourceDirNotSpecifiedError = 3338459901U,
			// Token: 0x04000265 RID: 613
			FreshIntroduction = 3315256202U,
			// Token: 0x04000266 RID: 614
			SchemaMasterDCNotFoundException = 2192506430U,
			// Token: 0x04000267 RID: 615
			ADDriverBoundToAdam = 2107252035U,
			// Token: 0x04000268 RID: 616
			CabUtilityWrapperError = 1331356275U,
			// Token: 0x04000269 RID: 617
			SchemaMasterNotAvailable = 4044548133U,
			// Token: 0x0400026A RID: 618
			OSPRoleDisplayName = 2024338705U,
			// Token: 0x0400026B RID: 619
			UpgradeLicenseAgreementShortDescription = 2927231269U,
			// Token: 0x0400026C RID: 620
			DRSuccessStatus = 759658685U,
			// Token: 0x0400026D RID: 621
			CopyFileText = 2340024118U,
			// Token: 0x0400026E RID: 622
			WillNotStartTransportService = 302515857U,
			// Token: 0x0400026F RID: 623
			RemoveMESOObjectLink = 3504655211U
		}

		// Token: 0x02000076 RID: 118
		private enum ParamIDs
		{
			// Token: 0x04000271 RID: 625
			WillSearchForAServerObjectForLocalServer,
			// Token: 0x04000272 RID: 626
			TargetDirectoryCannotBeChanged,
			// Token: 0x04000273 RID: 627
			LanguagePackPackagePath,
			// Token: 0x04000274 RID: 628
			ValidatingOptionsForRoles,
			// Token: 0x04000275 RID: 629
			ADSchemaVersionHigherThanSetupException,
			// Token: 0x04000276 RID: 630
			UserSpecifiedDCIsNotInLocalDomainException,
			// Token: 0x04000277 RID: 631
			DeserializedStateXML,
			// Token: 0x04000278 RID: 632
			InstallationPathInvalidDriveTypeInformation,
			// Token: 0x04000279 RID: 633
			SettingArgumentBecauseItIsRequired,
			// Token: 0x0400027A RID: 634
			NotEnoughSpace,
			// Token: 0x0400027B RID: 635
			RoleInstalledOnServer,
			// Token: 0x0400027C RID: 636
			ExchangeVersionInvalid,
			// Token: 0x0400027D RID: 637
			ExchangeOrganizationAlreadyExists,
			// Token: 0x0400027E RID: 638
			InstallationPathInvalidProfilesInformation,
			// Token: 0x0400027F RID: 639
			ServerNotFound,
			// Token: 0x04000280 RID: 640
			DomainControllerChosen,
			// Token: 0x04000281 RID: 641
			UserSpecifiedTargetDir,
			// Token: 0x04000282 RID: 642
			NotAValidFqdn,
			// Token: 0x04000283 RID: 643
			HighLevelTaskStarted,
			// Token: 0x04000284 RID: 644
			UmLanguagePackPackagePath,
			// Token: 0x04000285 RID: 645
			UserSpecifiedDCIsNotAvailableException,
			// Token: 0x04000286 RID: 646
			RootDataHandlerCount,
			// Token: 0x04000287 RID: 647
			SettingArgumentToValue,
			// Token: 0x04000288 RID: 648
			TheFirstPathUnderTheSecondPath,
			// Token: 0x04000289 RID: 649
			UnableToFindBuildVersion1,
			// Token: 0x0400028A RID: 650
			BackupPath,
			// Token: 0x0400028B RID: 651
			PackagePathSetTo,
			// Token: 0x0400028C RID: 652
			ADRelatedError,
			// Token: 0x0400028D RID: 653
			AlwaysWatsonForDebug,
			// Token: 0x0400028E RID: 654
			LanguagePacksPackagePathNotFound,
			// Token: 0x0400028F RID: 655
			UnifiedMessagingLanguagePackInstalled,
			// Token: 0x04000290 RID: 656
			InstalledVersion,
			// Token: 0x04000291 RID: 657
			SetupWillUseDomainController,
			// Token: 0x04000292 RID: 658
			AddRoleAlreadyInstalledError,
			// Token: 0x04000293 RID: 659
			CommandLineParameterSpecified,
			// Token: 0x04000294 RID: 660
			CannotFindFile,
			// Token: 0x04000295 RID: 661
			ParameterNotValidForCurrentRoles,
			// Token: 0x04000296 RID: 662
			ExchangeServerFound,
			// Token: 0x04000297 RID: 663
			AdInitializationStatus,
			// Token: 0x04000298 RID: 664
			InstallationPathUnderUserProfileInformation,
			// Token: 0x04000299 RID: 665
			UmLanguagePackDirectoryNotAvailable,
			// Token: 0x0400029A RID: 666
			UpgradeRoleNotInstalledError,
			// Token: 0x0400029B RID: 667
			MsiDirectoryNotFound,
			// Token: 0x0400029C RID: 668
			ValidatingRoleOptions,
			// Token: 0x0400029D RID: 669
			UnexpectedFileFromBundle,
			// Token: 0x0400029E RID: 670
			DisplayServerName,
			// Token: 0x0400029F RID: 671
			InvalidFqdn,
			// Token: 0x040002A0 RID: 672
			TargetInstallationDirectory,
			// Token: 0x040002A1 RID: 673
			AddUmLanguagePackModeDataHandlerCount,
			// Token: 0x040002A2 RID: 674
			RunForestPrepInSchemaMasterDomainException,
			// Token: 0x040002A3 RID: 675
			LanguagePackagePathSetTo,
			// Token: 0x040002A4 RID: 676
			SettingArgumentBecauseOfCommandLineParameter,
			// Token: 0x040002A5 RID: 677
			SetupNotFoundInSourceDirError,
			// Token: 0x040002A6 RID: 678
			SchemaUpdateRequired,
			// Token: 0x040002A7 RID: 679
			VersionMismatchWarning,
			// Token: 0x040002A8 RID: 680
			DCNotResponding,
			// Token: 0x040002A9 RID: 681
			ForestPrepAlreadyRun,
			// Token: 0x040002AA RID: 682
			DCNotInLocalDomain,
			// Token: 0x040002AB RID: 683
			RemoveUmLanguagePackLogFilePath,
			// Token: 0x040002AC RID: 684
			AddUmLanguagePackLogFilePath,
			// Token: 0x040002AD RID: 685
			ExistingOrganizationName,
			// Token: 0x040002AE RID: 686
			ExceptionWhenDeserializingStateFile,
			// Token: 0x040002AF RID: 687
			LanguagePacksVersionFileNotFound,
			// Token: 0x040002B0 RID: 688
			StateFileVersionMismatch,
			// Token: 0x040002B1 RID: 689
			NoConfigurationInfoFoundForInstallableUnit,
			// Token: 0x040002B2 RID: 690
			SetupWillRunFromPath,
			// Token: 0x040002B3 RID: 691
			ADDomainConfigVersionHigherThanSetupException,
			// Token: 0x040002B4 RID: 692
			RemoveLanguagePacksLogFilePath,
			// Token: 0x040002B5 RID: 693
			WarningUpperCase,
			// Token: 0x040002B6 RID: 694
			UmLanguagePackFullPackagePath,
			// Token: 0x040002B7 RID: 695
			RoleNotInstalledError,
			// Token: 0x040002B8 RID: 696
			ErrorUpperCase,
			// Token: 0x040002B9 RID: 697
			LanguagePacksBadVersionFormat,
			// Token: 0x040002BA RID: 698
			NoSchemaEntry,
			// Token: 0x040002BB RID: 699
			InstallationPathInvalidDriveFormatInformation,
			// Token: 0x040002BC RID: 700
			NoExchangeConfigurationContainerFound,
			// Token: 0x040002BD RID: 701
			OrganizationAlreadyExists,
			// Token: 0x040002BE RID: 702
			DCAlreadySpecified,
			// Token: 0x040002BF RID: 703
			ExchangeServerNotFound,
			// Token: 0x040002C0 RID: 704
			SchemaMasterDCNotAvailableException,
			// Token: 0x040002C1 RID: 705
			BackupKeyIsWrongType,
			// Token: 0x040002C2 RID: 706
			CannotInstallDatacenterRole,
			// Token: 0x040002C3 RID: 707
			PersistedDomainController,
			// Token: 0x040002C4 RID: 708
			DomainConfigUpdateRequired,
			// Token: 0x040002C5 RID: 709
			WillSearchForAServerObjectForServer,
			// Token: 0x040002C6 RID: 710
			AddedConfigurationInfoForInstallableUnit,
			// Token: 0x040002C7 RID: 711
			InstallationPathInvalidRootDriveInformation,
			// Token: 0x040002C8 RID: 712
			ADOrgConfigVersionHigherThanSetupException,
			// Token: 0x040002C9 RID: 713
			AttemptToSearchExchangeServerFailed,
			// Token: 0x040002CA RID: 714
			UninstallModeDataHandlerHandlersAndWorkUnits,
			// Token: 0x040002CB RID: 715
			CommandLine,
			// Token: 0x040002CC RID: 716
			SetupSourceDirectory,
			// Token: 0x040002CD RID: 717
			UmLanguagePackNotFoundForCulture,
			// Token: 0x040002CE RID: 718
			UmLanguagePackDisplayNameWithCulture,
			// Token: 0x040002CF RID: 719
			MsiFilesDirectoryCannotBeChanged,
			// Token: 0x040002D0 RID: 720
			ExchangeConfigurationContainerName,
			// Token: 0x040002D1 RID: 721
			ForestPrepNotRunOrNotReplicatedException,
			// Token: 0x040002D2 RID: 722
			LPVersioningExtractionFailed,
			// Token: 0x040002D3 RID: 723
			MSINotPresent,
			// Token: 0x040002D4 RID: 724
			SetupWillUseGlobalCatalog,
			// Token: 0x040002D5 RID: 725
			NotALegacyServer,
			// Token: 0x040002D6 RID: 726
			WillGetConfiguredRolesFromServerObject,
			// Token: 0x040002D7 RID: 727
			UninstallModeDataHandlerCount,
			// Token: 0x040002D8 RID: 728
			WillExecuteHighLevelTask,
			// Token: 0x040002D9 RID: 729
			BackupVersion,
			// Token: 0x040002DA RID: 730
			OrgConfigUpdateRequired,
			// Token: 0x040002DB RID: 731
			InstallModeDataHandlerCount,
			// Token: 0x040002DC RID: 732
			DCNameNotValid,
			// Token: 0x040002DD RID: 733
			GCChosen,
			// Token: 0x040002DE RID: 734
			SchemaMasterIsLocalDC,
			// Token: 0x040002DF RID: 735
			InstallationPathInvalidInformation,
			// Token: 0x040002E0 RID: 736
			InvalidOrganizationName,
			// Token: 0x040002E1 RID: 737
			NonCultureRegistryEntryFound,
			// Token: 0x040002E2 RID: 738
			ForestPrepNotRun,
			// Token: 0x040002E3 RID: 739
			InvalidExchangeOrganizationName,
			// Token: 0x040002E4 RID: 740
			UmLanguagePackNotInstalledForCulture,
			// Token: 0x040002E5 RID: 741
			UserSpecifiedDCDoesNotExistException,
			// Token: 0x040002E6 RID: 742
			LocalServerNameInvalid,
			// Token: 0x040002E7 RID: 743
			UpgradeModeDataHandlerHandlersAndWorkUnits,
			// Token: 0x040002E8 RID: 744
			CannotFindPath,
			// Token: 0x040002E9 RID: 745
			GCAlreadySpecified,
			// Token: 0x040002EA RID: 746
			MsiFileNotFound,
			// Token: 0x040002EB RID: 747
			AdminToolCannotBeUninstalledWhenSomeRolesRemained,
			// Token: 0x040002EC RID: 748
			DRRoleAlreadyInstalledError,
			// Token: 0x040002ED RID: 749
			RemoveUmLanguagePackModeDataHandlerCount,
			// Token: 0x040002EE RID: 750
			BackupKeyInaccessible,
			// Token: 0x040002EF RID: 751
			UserSpecifiedDCIsNotSchemaMasterException,
			// Token: 0x040002F0 RID: 752
			UmLanguagePackFileNotFound,
			// Token: 0x040002F1 RID: 753
			NameValueFormat,
			// Token: 0x040002F2 RID: 754
			SettingOrganizationName,
			// Token: 0x040002F3 RID: 755
			InstallationModeSetTo,
			// Token: 0x040002F4 RID: 756
			InvalidMaximumRecordNumber,
			// Token: 0x040002F5 RID: 757
			NoExchangeOrganizationContainerFound,
			// Token: 0x040002F6 RID: 758
			AddLanguagePacksLogFilePath,
			// Token: 0x040002F7 RID: 759
			DRModeDataHandlerCount,
			// Token: 0x040002F8 RID: 760
			ExchangeOrganizationContainerName
		}
	}
}
