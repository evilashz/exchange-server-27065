using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200007A RID: 122
	internal static class Strings
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00026854 File Offset: 0x00024A54
		static Strings()
		{
			Strings.stringIDs.Add(3076655694U, "ExchangeVersionBlock");
			Strings.stringIDs.Add(2041384979U, "InflatingAndDecodingPassedInHash");
			Strings.stringIDs.Add(3961637084U, "MSFilterPackV2NotInstalled");
			Strings.stringIDs.Add(286971094U, "MailboxRoleAlreadyExists");
			Strings.stringIDs.Add(3853989365U, "TooManyResults");
			Strings.stringIDs.Add(3498036640U, "EventSystemStopped");
			Strings.stringIDs.Add(971944032U, "WindowsServer2008CoreServerEdition");
			Strings.stringIDs.Add(2386277257U, "DelegatedClientAccessFirstInstall");
			Strings.stringIDs.Add(564094161U, "NoConnectorToStar");
			Strings.stringIDs.Add(1256831615U, "LangPackUpgradeVersioning");
			Strings.stringIDs.Add(2177637643U, "PreviousVersionOfExchangeAlreadyInstalled");
			Strings.stringIDs.Add(3383574593U, "CannotUninstallClusterNode");
			Strings.stringIDs.Add(173856461U, "TestDCSettingsForHybridEnabledTenantFailed");
			Strings.stringIDs.Add(975440992U, "NoE12ServerWarning");
			Strings.stringIDs.Add(2186576589U, "DelegatedCafeFirstInstall");
			Strings.stringIDs.Add(4277708119U, "ParsingXmlDataFromDCFileOrDCCmdlet");
			Strings.stringIDs.Add(3412170869U, "InvalidPSCredential");
			Strings.stringIDs.Add(233623067U, "FoundOrgConfigHashInConfigFile");
			Strings.stringIDs.Add(1884243248U, "PSCredentialAndTheOrganizationHashIsNull");
			Strings.stringIDs.Add(1760005143U, "ClientAccessRoleAlreadyExists");
			Strings.stringIDs.Add(2779888852U, "AdcFound");
			Strings.stringIDs.Add(1542399594U, "ForestLevelNotWin2003Native");
			Strings.stringIDs.Add(2107927772U, "InstallExchangeRolesOnDomainController");
			Strings.stringIDs.Add(1889541029U, "ServerFQDNDisplayName");
			Strings.stringIDs.Add(2033421306U, "MemberOfDatabaseAvailabilityGroup");
			Strings.stringIDs.Add(2446058696U, "DelegatedMailboxFirstInstall");
			Strings.stringIDs.Add(3207300105U, "CannotSetADSplitPermissionValidator");
			Strings.stringIDs.Add(168558763U, "FrontendTransportRoleAlreadyExists");
			Strings.stringIDs.Add(4109056594U, "GatewayUpgrade605Block");
			Strings.stringIDs.Add(3696305017U, "ShouldReRunSetupForW3SVC");
			Strings.stringIDs.Add(2679933748U, "InvalidLocalServerName");
			Strings.stringIDs.Add(3945170485U, "VC2012PrereqMissing");
			Strings.stringIDs.Add(3419466150U, "DelegatedClientAccessFirstSP1upgrade");
			Strings.stringIDs.Add(3480633415U, "TestNotRunDueToRegistryKey");
			Strings.stringIDs.Add(782041369U, "GetOrgConfigWasRunOnPremises");
			Strings.stringIDs.Add(1141647571U, "AccessedFailedResult");
			Strings.stringIDs.Add(3552416860U, "Iis32BitMode");
			Strings.stringIDs.Add(719584928U, "EitherTheOnPremAcceptedDomainListOrTheDCAcceptedDomainsAreEmpty");
			Strings.stringIDs.Add(3601945308U, "DCAdminDisplayVersionFound");
			Strings.stringIDs.Add(1667305647U, "GlobalServerInstall");
			Strings.stringIDs.Add(3878879664U, "SetupLogStarted");
			Strings.stringIDs.Add(3187185748U, "HybridInfoObjectNotFound");
			Strings.stringIDs.Add(1950741283U, "DelegatedMailboxFirstSP1upgrade");
			Strings.stringIDs.Add(2076519121U, "RunningTenantTestDCSettingsForHybridEnabledTenant");
			Strings.stringIDs.Add(2999486005U, "PowerShellExecutionPolicyCheck");
			Strings.stringIDs.Add(1793580687U, "DCEndPointIsEmpty");
			Strings.stringIDs.Add(3615196321U, "OSMinVersionNotMet");
			Strings.stringIDs.Add(704357583U, "AttemptingToParseTheXmlData");
			Strings.stringIDs.Add(2759777131U, "DCPreviousAdminDisplayVersionFound");
			Strings.stringIDs.Add(601266997U, "DotNetFrameworkMinVersionNotMet");
			Strings.stringIDs.Add(3940011457U, "WinRMIISExtensionInstalled");
			Strings.stringIDs.Add(1727977492U, "SearchFoundationAssemblyLoaderKBNotInstalled");
			Strings.stringIDs.Add(841751018U, "TenantIsRunningE15");
			Strings.stringIDs.Add(541611516U, "Win7RpcHttpShouldRejectDuplicateConnB2PacketsUpdateNotInstalled");
			Strings.stringIDs.Add(219074271U, "DelegatedBridgeheadFirstInstall");
			Strings.stringIDs.Add(3254033387U, "GetOrganizationConfigCmdletNotFound");
			Strings.stringIDs.Add(3375133662U, "TheFilePassedInIsNotFromGetOrganizationConfig");
			Strings.stringIDs.Add(3667045786U, "W3SVCDisabledOrNotInstalled");
			Strings.stringIDs.Add(1853686419U, "HostingModeNotAvailable");
			Strings.stringIDs.Add(1062077161U, "LangPackInstalled");
			Strings.stringIDs.Add(3068577726U, "InsufficientPrivledges");
			Strings.stringIDs.Add(1587969884U, "BadFilePassedIn");
			Strings.stringIDs.Add(3070759373U, "OSCheckedBuild");
			Strings.stringIDs.Add(3443808673U, "NetBIOSNameNotMatchesDNSHostName");
			Strings.stringIDs.Add(1073237881U, "NoGCInSite");
			Strings.stringIDs.Add(2097390153U, "InflateAndDecodeReturnedDataFromGetOrgConfig");
			Strings.stringIDs.Add(4278863281U, "LocalDomainNotPrepared");
			Strings.stringIDs.Add(1570727447U, "Exchange2000or2003PresentInOrg");
			Strings.stringIDs.Add(4143233965U, "Exchange2013AnyOnExchange2007or2010Server");
			Strings.stringIDs.Add(4199205241U, "RunningTenantHybridTest");
			Strings.stringIDs.Add(1023311823U, "DCIsDataCenterBitFound");
			Strings.stringIDs.Add(24167743U, "PrereqAnalysisNullValue");
			Strings.stringIDs.Add(3736002086U, "MinVersionCheck");
			Strings.stringIDs.Add(2170309289U, "ThereWasAnExceptionWhileCheckingForHybridConfiguration");
			Strings.stringIDs.Add(715361555U, "ConnectingToTheDCToRunGetOrgConfig");
			Strings.stringIDs.Add(3133604821U, "BridgeheadRoleAlreadyExists");
			Strings.stringIDs.Add(1976971220U, "MSFilterPackV2SP1NotInstalled");
			Strings.stringIDs.Add(4164286807U, "EmptyResults");
			Strings.stringIDs.Add(1626517757U, "ComputerRODC");
			Strings.stringIDs.Add(1186202058U, "DelegatedCafeFirstSP1upgrade");
			Strings.stringIDs.Add(3145781320U, "Win7RpcHttpAssocCookieGuidUpdateNotInstalled");
			Strings.stringIDs.Add(1376602368U, "AccessedValueWhenMultipleResults");
			Strings.stringIDs.Add(1828122210U, "UpgradeGateway605Block");
			Strings.stringIDs.Add(3401172205U, "NotLoggedOntoDomain");
			Strings.stringIDs.Add(4130092005U, "DataReturnedFromDCIsInvalid");
			Strings.stringIDs.Add(1040666117U, "ValueNotFoundInCollection");
			Strings.stringIDs.Add(841278233U, "MpsSvcStopped");
			Strings.stringIDs.Add(1554849195U, "DCAcceptedDomainNameFound");
			Strings.stringIDs.Add(641150786U, "CannotAccessAD");
			Strings.stringIDs.Add(1752859078U, "UpdateProgressForWrongAnalysis");
			Strings.stringIDs.Add(53953329U, "ServerNotPrepared");
			Strings.stringIDs.Add(2931842358U, "CheckingTheAcceptedDomainAgainstOrgRelationshipDomains");
			Strings.stringIDs.Add(1081610576U, "VC2013PrereqMissing");
			Strings.stringIDs.Add(1463453412U, "DomainPrepRequired");
			Strings.stringIDs.Add(4229980062U, "NoMatchWasFoundBetweenTheOrgRelDomainsAndDCAcceptedDomains");
			Strings.stringIDs.Add(3125428864U, "MSDTCStopped");
			Strings.stringIDs.Add(4101630194U, "CannotUninstallOABServer");
			Strings.stringIDs.Add(2459322720U, "TestDCSettingsForHybridEnabledTenantPassed");
			Strings.stringIDs.Add(1194462944U, "Win7LDRGDRRMSManifestExpiryUpdateNotInstalled");
			Strings.stringIDs.Add(3696227166U, "NoE14ServerWarning");
			Strings.stringIDs.Add(3265622739U, "InstallOnDCInADSplitPermissionMode");
			Strings.stringIDs.Add(2337843968U, "DelegatedBridgeheadFirstSP1upgrade");
			Strings.stringIDs.Add(2879980483U, "CafeRoleAlreadyExists");
			Strings.stringIDs.Add(3469704679U, "UnifiedMessagingRoleNotInstalled");
			Strings.stringIDs.Add(2121009437U, "CannotUninstallDelegatedServer");
			Strings.stringIDs.Add(3379016135U, "WinRMDisabledOrNotInstalled");
			Strings.stringIDs.Add(3219123481U, "W2K8R2PrepareAdLdifdeNotInstalled");
			Strings.stringIDs.Add(4056098580U, "DelegatedFrontendTransportFirstSP1upgrade");
			Strings.stringIDs.Add(3957588404U, "PendingReboot");
			Strings.stringIDs.Add(3440414664U, "LangPackDiskSpaceCheck");
			Strings.stringIDs.Add(4195359116U, "SetupLogEnd");
			Strings.stringIDs.Add(1829824776U, "DelegatedUnifiedMessagingFirstInstall");
			Strings.stringIDs.Add(569737677U, "Win7WindowsIdentityFoundationUpdateNotInstalled");
			Strings.stringIDs.Add(43746744U, "E14BridgeheadRoleNotFound");
			Strings.stringIDs.Add(2882191588U, "UnifiedMessagingRoleAlreadyExists");
			Strings.stringIDs.Add(3915669625U, "NetTcpPortSharingSvcNotAuto");
			Strings.stringIDs.Add(4251553116U, "WindowsInstallerServiceDisabledOrNotInstalled");
			Strings.stringIDs.Add(3733193906U, "SMTPSvcInstalled");
			Strings.stringIDs.Add(2364459045U, "DelegatedUnifiedMessagingFirstSP1upgrade");
			Strings.stringIDs.Add(392652342U, "OldADAMInstalled");
			Strings.stringIDs.Add(4254576536U, "RunningOnPremTest");
			Strings.stringIDs.Add(3978292472U, "UcmaRedistMsi");
			Strings.stringIDs.Add(3827371534U, "EitherOrgRelOrAcceptDomainsWhereNull");
			Strings.stringIDs.Add(2570206190U, "ADAMSvcStopped");
			Strings.stringIDs.Add(212290597U, "W2K8R2PrepareSchemaLdifdeNotInstalled");
			Strings.stringIDs.Add(2160575392U, "LocalComputerIsDCInChildDomain");
			Strings.stringIDs.Add(275692458U, "ClusSvcInstalledRoleBlock");
			Strings.stringIDs.Add(1958335472U, "PrereqAnalysisStarted");
			Strings.stringIDs.Add(2120465116U, "BridgeheadRoleNotInstalled");
			Strings.stringIDs.Add(2369286853U, "OrgConfigDataIsEmptyOrNull");
			Strings.stringIDs.Add(1550596250U, "EdgeSubscriptionExists");
			Strings.stringIDs.Add(1584908934U, "InvalidOrTamperedFile");
			Strings.stringIDs.Add(160644360U, "SendConnectorException");
			Strings.stringIDs.Add(1212051581U, "FqdnMissing");
			Strings.stringIDs.Add(1980036245U, "SchemaNotPreparedExtendedRights");
			Strings.stringIDs.Add(1622330535U, "LangPackBundleCheck");
			Strings.stringIDs.Add(2543736554U, "FailedResult");
			Strings.stringIDs.Add(2361234894U, "Win2k12RefsUpdateNotInstalled");
			Strings.stringIDs.Add(3990514141U, "CannotAccessHttpSiteForEngineUpdates");
			Strings.stringIDs.Add(1332821968U, "SpeechRedistMsi");
			Strings.stringIDs.Add(1132011277U, "PendingRebootWindowsComponents");
			Strings.stringIDs.Add(87247761U, "OSMinVersionForFSMONotMet");
			Strings.stringIDs.Add(2387324271U, "InvalidDNSDomainName");
			Strings.stringIDs.Add(1254031586U, "UMLangPackDiskSpaceCheck");
			Strings.stringIDs.Add(3272988597U, "Win7LDRRMSManifestExpiryUpdateNotInstalled");
			Strings.stringIDs.Add(2902280231U, "TenantIsBeingUpgradedFromE14");
			Strings.stringIDs.Add(1412296371U, "InvalidADSite");
			Strings.stringIDs.Add(3588122483U, "RemoteRegException");
			Strings.stringIDs.Add(1276355988U, "ADNotPreparedForHostingValidator");
			Strings.stringIDs.Add(4039112198U, "Win2k12RollupUpdateNotInstalled");
			Strings.stringIDs.Add(2380070307U, "InvalidOSVersion");
			Strings.stringIDs.Add(2017435239U, "MinimumFrameworkNotInstalled");
			Strings.stringIDs.Add(3014916009U, "SchemaNotPrepared");
			Strings.stringIDs.Add(2501606560U, "HostingActiveDirectorySplitPermissionsNotSupported");
			Strings.stringIDs.Add(2053317018U, "DisplayVersionDCBitUpgradeStatusBitAndVersionAreCorrect");
			Strings.stringIDs.Add(624846257U, "UpgradeMinVersionBlock");
			Strings.stringIDs.Add(2938058350U, "InvalidOSVersionForAdminTools");
			Strings.stringIDs.Add(3831181830U, "ConditionIsFalse");
			Strings.stringIDs.Add(346774553U, "OrgConfigHashDoesNotExist");
			Strings.stringIDs.Add(2890177564U, "ProvisionedUpdateRequired");
			Strings.stringIDs.Add(1981068904U, "ComputerNotPartofDomain");
			Strings.stringIDs.Add(985137777U, "Win2k12UrefsUpdateNotInstalled");
			Strings.stringIDs.Add(260127048U, "CannotReturnNullForResult");
			Strings.stringIDs.Add(2452722357U, "SetADSplitPermissionWhenExchangeServerRolesOnDC");
			Strings.stringIDs.Add(3769154951U, "InflateAndDecoding");
			Strings.stringIDs.Add(2393957831U, "ADAMLonghornWin7ServerNotInstalled");
			Strings.stringIDs.Add(2715471913U, "DCIsUpgradingOrganizationFound");
			Strings.stringIDs.Add(3301968605U, "MailboxRoleNotInstalled");
			Strings.stringIDs.Add(3344709799U, "PrereqAnalysisParentExceptionValue");
			Strings.stringIDs.Add(1535334837U, "TenantHasNotYetBeenUpgradedToE15");
			Strings.stringIDs.Add(758899952U, "DotNetFrameworkNeedsUpdate");
			Strings.stringIDs.Add(1799611416U, "OSMinVersionForAdminToolsNotMet");
			Strings.stringIDs.Add(3629514284U, "NullLoggerHasBeenPassedIn");
			Strings.stringIDs.Add(888551196U, "HybridInfoPurePSObjectsNotSupported");
			Strings.stringIDs.Add(3204362251U, "LangPackBundleVersioning");
			Strings.stringIDs.Add(3457078824U, "SearchingForAttributes");
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00027653 File Offset: 0x00025853
		public static LocalizedString ExchangeVersionBlock
		{
			get
			{
				return new LocalizedString("ExchangeVersionBlock", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002766C File Offset: 0x0002586C
		public static LocalizedString LessThan(string first, string second)
		{
			return new LocalizedString("LessThan", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00027698 File Offset: 0x00025898
		public static LocalizedString OnPremisesOrgRelationshipDomainsCrossWithAcceptedDomainReturnResult(string result)
		{
			return new LocalizedString("OnPremisesOrgRelationshipDomainsCrossWithAcceptedDomainReturnResult", Strings.ResourceManager, new object[]
			{
				result
			});
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000276C0 File Offset: 0x000258C0
		public static LocalizedString InflatingAndDecodingPassedInHash
		{
			get
			{
				return new LocalizedString("InflatingAndDecodingPassedInHash", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000276D7 File Offset: 0x000258D7
		public static LocalizedString MSFilterPackV2NotInstalled
		{
			get
			{
				return new LocalizedString("MSFilterPackV2NotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000276EE File Offset: 0x000258EE
		public static LocalizedString MailboxRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("MailboxRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00027708 File Offset: 0x00025908
		public static LocalizedString QueryReturnedNull(string queryName)
		{
			return new LocalizedString("QueryReturnedNull", Strings.ResourceManager, new object[]
			{
				queryName
			});
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00027730 File Offset: 0x00025930
		public static LocalizedString ShouldNotBeNullOrEmpty(string name)
		{
			return new LocalizedString("ShouldNotBeNullOrEmpty", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00027758 File Offset: 0x00025958
		public static LocalizedString TooManyResults
		{
			get
			{
				return new LocalizedString("TooManyResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002776F File Offset: 0x0002596F
		public static LocalizedString EventSystemStopped
		{
			get
			{
				return new LocalizedString("EventSystemStopped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00027788 File Offset: 0x00025988
		public static LocalizedString ServicesAreMarkedForDeletion(string ListofServices)
		{
			return new LocalizedString("ServicesAreMarkedForDeletion", Strings.ResourceManager, new object[]
			{
				ListofServices
			});
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x000277B0 File Offset: 0x000259B0
		public static LocalizedString WindowsServer2008CoreServerEdition
		{
			get
			{
				return new LocalizedString("WindowsServer2008CoreServerEdition", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000277C7 File Offset: 0x000259C7
		public static LocalizedString DelegatedClientAccessFirstInstall
		{
			get
			{
				return new LocalizedString("DelegatedClientAccessFirstInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000277DE File Offset: 0x000259DE
		public static LocalizedString NoConnectorToStar
		{
			get
			{
				return new LocalizedString("NoConnectorToStar", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000277F5 File Offset: 0x000259F5
		public static LocalizedString LangPackUpgradeVersioning
		{
			get
			{
				return new LocalizedString("LangPackUpgradeVersioning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002780C File Offset: 0x00025A0C
		public static LocalizedString AssemblyVersion(string assemblyVersion)
		{
			return new LocalizedString("AssemblyVersion", Strings.ResourceManager, new object[]
			{
				assemblyVersion
			});
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00027834 File Offset: 0x00025A34
		public static LocalizedString PrimaryDNSTestFailed(string dns)
		{
			return new LocalizedString("PrimaryDNSTestFailed", Strings.ResourceManager, new object[]
			{
				dns
			});
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002785C File Offset: 0x00025A5C
		public static LocalizedString DRMinVersionNotMet(string version)
		{
			return new LocalizedString("DRMinVersionNotMet", Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00027884 File Offset: 0x00025A84
		public static LocalizedString ADUpdateForDomainPrep(string name)
		{
			return new LocalizedString("ADUpdateForDomainPrep", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000278AC File Offset: 0x00025AAC
		public static LocalizedString PreviousVersionOfExchangeAlreadyInstalled
		{
			get
			{
				return new LocalizedString("PreviousVersionOfExchangeAlreadyInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000278C4 File Offset: 0x00025AC4
		public static LocalizedString ProcessNeedsToBeClosedOnUninstall(string list)
		{
			return new LocalizedString("ProcessNeedsToBeClosedOnUninstall", Strings.ResourceManager, new object[]
			{
				list
			});
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000278EC File Offset: 0x00025AEC
		public static LocalizedString InvalidDomainToPrepare(string name)
		{
			return new LocalizedString("InvalidDomainToPrepare", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00027914 File Offset: 0x00025B14
		public static LocalizedString CannotUninstallClusterNode
		{
			get
			{
				return new LocalizedString("CannotUninstallClusterNode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002792B File Offset: 0x00025B2B
		public static LocalizedString TestDCSettingsForHybridEnabledTenantFailed
		{
			get
			{
				return new LocalizedString("TestDCSettingsForHybridEnabledTenantFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00027944 File Offset: 0x00025B44
		public static LocalizedString ErrorWhileRunning(string exceptionMsg)
		{
			return new LocalizedString("ErrorWhileRunning", Strings.ResourceManager, new object[]
			{
				exceptionMsg
			});
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002796C File Offset: 0x00025B6C
		public static LocalizedString NoE12ServerWarning
		{
			get
			{
				return new LocalizedString("NoE12ServerWarning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00027984 File Offset: 0x00025B84
		public static LocalizedString RegistryKeyNotFound(string key)
		{
			return new LocalizedString("RegistryKeyNotFound", Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000279AC File Offset: 0x00025BAC
		public static LocalizedString DelegatedCafeFirstInstall
		{
			get
			{
				return new LocalizedString("DelegatedCafeFirstInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000279C3 File Offset: 0x00025BC3
		public static LocalizedString ParsingXmlDataFromDCFileOrDCCmdlet
		{
			get
			{
				return new LocalizedString("ParsingXmlDataFromDCFileOrDCCmdlet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000279DA File Offset: 0x00025BDA
		public static LocalizedString InvalidPSCredential
		{
			get
			{
				return new LocalizedString("InvalidPSCredential", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000279F4 File Offset: 0x00025BF4
		public static LocalizedString ConfigDCHostNameMismatch(string dc, string dcInRegistry)
		{
			return new LocalizedString("ConfigDCHostNameMismatch", Strings.ResourceManager, new object[]
			{
				dc,
				dcInRegistry
			});
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00027A20 File Offset: 0x00025C20
		public static LocalizedString FoundOrgConfigHashInConfigFile
		{
			get
			{
				return new LocalizedString("FoundOrgConfigHashInConfigFile", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00027A37 File Offset: 0x00025C37
		public static LocalizedString PSCredentialAndTheOrganizationHashIsNull
		{
			get
			{
				return new LocalizedString("PSCredentialAndTheOrganizationHashIsNull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00027A4E File Offset: 0x00025C4E
		public static LocalizedString ClientAccessRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("ClientAccessRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00027A65 File Offset: 0x00025C65
		public static LocalizedString AdcFound
		{
			get
			{
				return new LocalizedString("AdcFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00027A7C File Offset: 0x00025C7C
		public static LocalizedString LocalTimeZone(string LocalZone)
		{
			return new LocalizedString("LocalTimeZone", Strings.ResourceManager, new object[]
			{
				LocalZone
			});
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public static LocalizedString ForestLevelNotWin2003Native
		{
			get
			{
				return new LocalizedString("ForestLevelNotWin2003Native", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00027ABB File Offset: 0x00025CBB
		public static LocalizedString InstallExchangeRolesOnDomainController
		{
			get
			{
				return new LocalizedString("InstallExchangeRolesOnDomainController", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00027AD4 File Offset: 0x00025CD4
		public static LocalizedString HybridInfoOpeningRunspace(string uri)
		{
			return new LocalizedString("HybridInfoOpeningRunspace", Strings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00027AFC File Offset: 0x00025CFC
		public static LocalizedString E15E14CoexistenceMinOSReqFailure(string servers)
		{
			return new LocalizedString("E15E14CoexistenceMinOSReqFailure", Strings.ResourceManager, new object[]
			{
				servers
			});
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00027B24 File Offset: 0x00025D24
		public static LocalizedString PrereqAnalysisFailureToAccessResults(string memberName, string message)
		{
			return new LocalizedString("PrereqAnalysisFailureToAccessResults", Strings.ResourceManager, new object[]
			{
				memberName,
				message
			});
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00027B50 File Offset: 0x00025D50
		public static LocalizedString ServerFQDNDisplayName
		{
			get
			{
				return new LocalizedString("ServerFQDNDisplayName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00027B68 File Offset: 0x00025D68
		public static LocalizedString InvalidOrTamperedConfigFile(string pathToConfigFile)
		{
			return new LocalizedString("InvalidOrTamperedConfigFile", Strings.ResourceManager, new object[]
			{
				pathToConfigFile
			});
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00027B90 File Offset: 0x00025D90
		public static LocalizedString MemberOfDatabaseAvailabilityGroup
		{
			get
			{
				return new LocalizedString("MemberOfDatabaseAvailabilityGroup", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00027BA7 File Offset: 0x00025DA7
		public static LocalizedString DelegatedMailboxFirstInstall
		{
			get
			{
				return new LocalizedString("DelegatedMailboxFirstInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00027BBE File Offset: 0x00025DBE
		public static LocalizedString CannotSetADSplitPermissionValidator
		{
			get
			{
				return new LocalizedString("CannotSetADSplitPermissionValidator", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00027BD8 File Offset: 0x00025DD8
		public static LocalizedString MailboxEDBDriveDoesNotExist(string drive)
		{
			return new LocalizedString("MailboxEDBDriveDoesNotExist", Strings.ResourceManager, new object[]
			{
				drive
			});
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00027C00 File Offset: 0x00025E00
		public static LocalizedString OSVersion(string version)
		{
			return new LocalizedString("OSVersion", Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00027C28 File Offset: 0x00025E28
		public static LocalizedString LonghornIISManagementConsoleInstalledValidator(string iisversion)
		{
			return new LocalizedString("LonghornIISManagementConsoleInstalledValidator", Strings.ResourceManager, new object[]
			{
				iisversion
			});
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00027C50 File Offset: 0x00025E50
		public static LocalizedString FrontendTransportRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("FrontendTransportRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00027C67 File Offset: 0x00025E67
		public static LocalizedString GatewayUpgrade605Block
		{
			get
			{
				return new LocalizedString("GatewayUpgrade605Block", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027C80 File Offset: 0x00025E80
		public static LocalizedString WatermarkPresent(string role)
		{
			return new LocalizedString("WatermarkPresent", Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00027CA8 File Offset: 0x00025EA8
		public static LocalizedString LonghornIISMetabaseNotInstalled(string iisversion)
		{
			return new LocalizedString("LonghornIISMetabaseNotInstalled", Strings.ResourceManager, new object[]
			{
				iisversion
			});
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00027CD0 File Offset: 0x00025ED0
		public static LocalizedString VoiceMessagesInQueue(string path)
		{
			return new LocalizedString("VoiceMessagesInQueue", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00027CF8 File Offset: 0x00025EF8
		public static LocalizedString ShouldReRunSetupForW3SVC
		{
			get
			{
				return new LocalizedString("ShouldReRunSetupForW3SVC", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00027D0F File Offset: 0x00025F0F
		public static LocalizedString InvalidLocalServerName
		{
			get
			{
				return new LocalizedString("InvalidLocalServerName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00027D26 File Offset: 0x00025F26
		public static LocalizedString VC2012PrereqMissing
		{
			get
			{
				return new LocalizedString("VC2012PrereqMissing", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00027D3D File Offset: 0x00025F3D
		public static LocalizedString DelegatedClientAccessFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedClientAccessFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00027D54 File Offset: 0x00025F54
		public static LocalizedString CannotRemoveProvisionedServerValidator(string server)
		{
			return new LocalizedString("CannotRemoveProvisionedServerValidator", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00027D7C File Offset: 0x00025F7C
		public static LocalizedString VistaNoIPv4(string product)
		{
			return new LocalizedString("VistaNoIPv4", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00027DA4 File Offset: 0x00025FA4
		public static LocalizedString Equal(string first, string second)
		{
			return new LocalizedString("Equal", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00027DD0 File Offset: 0x00025FD0
		public static LocalizedString FileInUse(string mode, string process)
		{
			return new LocalizedString("FileInUse", Strings.ResourceManager, new object[]
			{
				mode,
				process
			});
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00027DFC File Offset: 0x00025FFC
		public static LocalizedString UserNameError(string error)
		{
			return new LocalizedString("UserNameError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00027E24 File Offset: 0x00026024
		public static LocalizedString E15E14CoexistenceMinOSReqFailureInDC(string servers)
		{
			return new LocalizedString("E15E14CoexistenceMinOSReqFailureInDC", Strings.ResourceManager, new object[]
			{
				servers
			});
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00027E4C File Offset: 0x0002604C
		public static LocalizedString TestNotRunDueToRegistryKey
		{
			get
			{
				return new LocalizedString("TestNotRunDueToRegistryKey", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00027E63 File Offset: 0x00026063
		public static LocalizedString GetOrgConfigWasRunOnPremises
		{
			get
			{
				return new LocalizedString("GetOrgConfigWasRunOnPremises", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00027E7A File Offset: 0x0002607A
		public static LocalizedString AccessedFailedResult
		{
			get
			{
				return new LocalizedString("AccessedFailedResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00027E91 File Offset: 0x00026091
		public static LocalizedString Iis32BitMode
		{
			get
			{
				return new LocalizedString("Iis32BitMode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00027EA8 File Offset: 0x000260A8
		public static LocalizedString EitherTheOnPremAcceptedDomainListOrTheDCAcceptedDomainsAreEmpty
		{
			get
			{
				return new LocalizedString("EitherTheOnPremAcceptedDomainListOrTheDCAcceptedDomainsAreEmpty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00027EBF File Offset: 0x000260BF
		public static LocalizedString DCAdminDisplayVersionFound
		{
			get
			{
				return new LocalizedString("DCAdminDisplayVersionFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00027ED6 File Offset: 0x000260D6
		public static LocalizedString GlobalServerInstall
		{
			get
			{
				return new LocalizedString("GlobalServerInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00027EF0 File Offset: 0x000260F0
		public static LocalizedString DomainNameExistsInAcceptedDomainAndOrgRel(string domain)
		{
			return new LocalizedString("DomainNameExistsInAcceptedDomainAndOrgRel", Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00027F18 File Offset: 0x00026118
		public static LocalizedString RunningTentantHybridTestWithFile(string pathToConfigFile)
		{
			return new LocalizedString("RunningTentantHybridTestWithFile", Strings.ResourceManager, new object[]
			{
				pathToConfigFile
			});
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00027F40 File Offset: 0x00026140
		public static LocalizedString SetupLogStarted
		{
			get
			{
				return new LocalizedString("SetupLogStarted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00027F57 File Offset: 0x00026157
		public static LocalizedString HybridInfoObjectNotFound
		{
			get
			{
				return new LocalizedString("HybridInfoObjectNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00027F6E File Offset: 0x0002616E
		public static LocalizedString DelegatedMailboxFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedMailboxFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00027F85 File Offset: 0x00026185
		public static LocalizedString RunningTenantTestDCSettingsForHybridEnabledTenant
		{
			get
			{
				return new LocalizedString("RunningTenantTestDCSettingsForHybridEnabledTenant", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00027F9C File Offset: 0x0002619C
		public static LocalizedString HybridInfoCmdletStart(string session, string cmdlet, string parameters)
		{
			return new LocalizedString("HybridInfoCmdletStart", Strings.ResourceManager, new object[]
			{
				session,
				cmdlet,
				parameters
			});
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00027FCC File Offset: 0x000261CC
		public static LocalizedString TraceFunctionEnter(Type type, string methodName, string argumentList)
		{
			return new LocalizedString("TraceFunctionEnter", Strings.ResourceManager, new object[]
			{
				type,
				methodName,
				argumentList
			});
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00027FFC File Offset: 0x000261FC
		public static LocalizedString PowerShellExecutionPolicyCheck
		{
			get
			{
				return new LocalizedString("PowerShellExecutionPolicyCheck", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00028014 File Offset: 0x00026214
		public static LocalizedString MailboxLogDriveDoesNotExist(string drive)
		{
			return new LocalizedString("MailboxLogDriveDoesNotExist", Strings.ResourceManager, new object[]
			{
				drive
			});
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002803C File Offset: 0x0002623C
		public static LocalizedString DCEndPointIsEmpty
		{
			get
			{
				return new LocalizedString("DCEndPointIsEmpty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00028053 File Offset: 0x00026253
		public static LocalizedString OSMinVersionNotMet
		{
			get
			{
				return new LocalizedString("OSMinVersionNotMet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002806A File Offset: 0x0002626A
		public static LocalizedString AttemptingToParseTheXmlData
		{
			get
			{
				return new LocalizedString("AttemptingToParseTheXmlData", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00028084 File Offset: 0x00026284
		public static LocalizedString TestingConfigFile(string pathToConfigFile)
		{
			return new LocalizedString("TestingConfigFile", Strings.ResourceManager, new object[]
			{
				pathToConfigFile
			});
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000280AC File Offset: 0x000262AC
		public static LocalizedString DCPreviousAdminDisplayVersionFound
		{
			get
			{
				return new LocalizedString("DCPreviousAdminDisplayVersionFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000280C3 File Offset: 0x000262C3
		public static LocalizedString DotNetFrameworkMinVersionNotMet
		{
			get
			{
				return new LocalizedString("DotNetFrameworkMinVersionNotMet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000280DC File Offset: 0x000262DC
		public static LocalizedString InterruptedUninstallNotContinued(string role)
		{
			return new LocalizedString("InterruptedUninstallNotContinued", Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00028104 File Offset: 0x00026304
		public static LocalizedString WinRMIISExtensionInstalled
		{
			get
			{
				return new LocalizedString("WinRMIISExtensionInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002811C File Offset: 0x0002631C
		public static LocalizedString ErrorDNSQueryA(string ipDNSName, string pName, string message)
		{
			return new LocalizedString("ErrorDNSQueryA", Strings.ResourceManager, new object[]
			{
				ipDNSName,
				pName,
				message
			});
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002814C File Offset: 0x0002634C
		public static LocalizedString SearchFoundationAssemblyLoaderKBNotInstalled
		{
			get
			{
				return new LocalizedString("SearchFoundationAssemblyLoaderKBNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00028163 File Offset: 0x00026363
		public static LocalizedString TenantIsRunningE15
		{
			get
			{
				return new LocalizedString("TenantIsRunningE15", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002817A File Offset: 0x0002637A
		public static LocalizedString Win7RpcHttpShouldRejectDuplicateConnB2PacketsUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win7RpcHttpShouldRejectDuplicateConnB2PacketsUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00028194 File Offset: 0x00026394
		public static LocalizedString LonghornNoIPv4(string product)
		{
			return new LocalizedString("LonghornNoIPv4", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000281BC File Offset: 0x000263BC
		public static LocalizedString DelegatedBridgeheadFirstInstall
		{
			get
			{
				return new LocalizedString("DelegatedBridgeheadFirstInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000281D3 File Offset: 0x000263D3
		public static LocalizedString GetOrganizationConfigCmdletNotFound
		{
			get
			{
				return new LocalizedString("GetOrganizationConfigCmdletNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000281EA File Offset: 0x000263EA
		public static LocalizedString TheFilePassedInIsNotFromGetOrganizationConfig
		{
			get
			{
				return new LocalizedString("TheFilePassedInIsNotFromGetOrganizationConfig", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00028204 File Offset: 0x00026404
		public static LocalizedString NotSupportedRecipientPolicyAddressFormatValidator(string name, string address)
		{
			return new LocalizedString("NotSupportedRecipientPolicyAddressFormatValidator", Strings.ResourceManager, new object[]
			{
				name,
				address
			});
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00028230 File Offset: 0x00026430
		public static LocalizedString W3SVCDisabledOrNotInstalled
		{
			get
			{
				return new LocalizedString("W3SVCDisabledOrNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00028247 File Offset: 0x00026447
		public static LocalizedString HostingModeNotAvailable
		{
			get
			{
				return new LocalizedString("HostingModeNotAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002825E File Offset: 0x0002645E
		public static LocalizedString LangPackInstalled
		{
			get
			{
				return new LocalizedString("LangPackInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00028275 File Offset: 0x00026475
		public static LocalizedString InsufficientPrivledges
		{
			get
			{
				return new LocalizedString("InsufficientPrivledges", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002828C File Offset: 0x0002648C
		public static LocalizedString BadFilePassedIn
		{
			get
			{
				return new LocalizedString("BadFilePassedIn", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000282A3 File Offset: 0x000264A3
		public static LocalizedString OSCheckedBuild
		{
			get
			{
				return new LocalizedString("OSCheckedBuild", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000282BA File Offset: 0x000264BA
		public static LocalizedString NetBIOSNameNotMatchesDNSHostName
		{
			get
			{
				return new LocalizedString("NetBIOSNameNotMatchesDNSHostName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000282D1 File Offset: 0x000264D1
		public static LocalizedString NoGCInSite
		{
			get
			{
				return new LocalizedString("NoGCInSite", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000282E8 File Offset: 0x000264E8
		public static LocalizedString InflateAndDecodeReturnedDataFromGetOrgConfig
		{
			get
			{
				return new LocalizedString("InflateAndDecodeReturnedDataFromGetOrgConfig", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000282FF File Offset: 0x000264FF
		public static LocalizedString LocalDomainNotPrepared
		{
			get
			{
				return new LocalizedString("LocalDomainNotPrepared", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00028316 File Offset: 0x00026516
		public static LocalizedString Exchange2000or2003PresentInOrg
		{
			get
			{
				return new LocalizedString("Exchange2000or2003PresentInOrg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00028330 File Offset: 0x00026530
		public static LocalizedString InconsistentlyConfiguredDomain(string name)
		{
			return new LocalizedString("InconsistentlyConfiguredDomain", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00028358 File Offset: 0x00026558
		public static LocalizedString Exchange2013AnyOnExchange2007or2010Server
		{
			get
			{
				return new LocalizedString("Exchange2013AnyOnExchange2007or2010Server", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00028370 File Offset: 0x00026570
		public static LocalizedString PrereqAnalysisSettingStarted(string memberName, string parentName, string valueType)
		{
			return new LocalizedString("PrereqAnalysisSettingStarted", Strings.ResourceManager, new object[]
			{
				memberName,
				parentName,
				valueType
			});
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x000283A0 File Offset: 0x000265A0
		public static LocalizedString RunningTenantHybridTest
		{
			get
			{
				return new LocalizedString("RunningTenantHybridTest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000283B7 File Offset: 0x000265B7
		public static LocalizedString DCIsDataCenterBitFound
		{
			get
			{
				return new LocalizedString("DCIsDataCenterBitFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000283D0 File Offset: 0x000265D0
		public static LocalizedString ClientAccessRoleNotPresentInSite(string adSite)
		{
			return new LocalizedString("ClientAccessRoleNotPresentInSite", Strings.ResourceManager, new object[]
			{
				adSite
			});
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x000283F8 File Offset: 0x000265F8
		public static LocalizedString PrereqAnalysisNullValue
		{
			get
			{
				return new LocalizedString("PrereqAnalysisNullValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002840F File Offset: 0x0002660F
		public static LocalizedString MinVersionCheck
		{
			get
			{
				return new LocalizedString("MinVersionCheck", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00028426 File Offset: 0x00026626
		public static LocalizedString ThereWasAnExceptionWhileCheckingForHybridConfiguration
		{
			get
			{
				return new LocalizedString("ThereWasAnExceptionWhileCheckingForHybridConfiguration", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002843D File Offset: 0x0002663D
		public static LocalizedString ConnectingToTheDCToRunGetOrgConfig
		{
			get
			{
				return new LocalizedString("ConnectingToTheDCToRunGetOrgConfig", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00028454 File Offset: 0x00026654
		public static LocalizedString BridgeheadRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("BridgeheadRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002846C File Offset: 0x0002666C
		public static LocalizedString RegistryKeyDoesntExist(string regKey)
		{
			return new LocalizedString("RegistryKeyDoesntExist", Strings.ResourceManager, new object[]
			{
				regKey
			});
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00028494 File Offset: 0x00026694
		public static LocalizedString BridgeheadRoleNotPresentInSite(string adSite)
		{
			return new LocalizedString("BridgeheadRoleNotPresentInSite", Strings.ResourceManager, new object[]
			{
				adSite
			});
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000284BC File Offset: 0x000266BC
		public static LocalizedString NoIPv4Assigned(string product)
		{
			return new LocalizedString("NoIPv4Assigned", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000284E4 File Offset: 0x000266E4
		public static LocalizedString QueryStart(string queryName, bool forceRun)
		{
			return new LocalizedString("QueryStart", Strings.ResourceManager, new object[]
			{
				queryName,
				forceRun
			});
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00028515 File Offset: 0x00026715
		public static LocalizedString MSFilterPackV2SP1NotInstalled
		{
			get
			{
				return new LocalizedString("MSFilterPackV2SP1NotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0002852C File Offset: 0x0002672C
		public static LocalizedString EmptyResults
		{
			get
			{
				return new LocalizedString("EmptyResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00028543 File Offset: 0x00026743
		public static LocalizedString ComputerRODC
		{
			get
			{
				return new LocalizedString("ComputerRODC", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002855C File Offset: 0x0002675C
		public static LocalizedString RecipientUpdateServiceNotAvailable(string name)
		{
			return new LocalizedString("RecipientUpdateServiceNotAvailable", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00028584 File Offset: 0x00026784
		public static LocalizedString DelegatedCafeFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedCafeFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002859B File Offset: 0x0002679B
		public static LocalizedString Win7RpcHttpAssocCookieGuidUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win7RpcHttpAssocCookieGuidUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000285B2 File Offset: 0x000267B2
		public static LocalizedString AccessedValueWhenMultipleResults
		{
			get
			{
				return new LocalizedString("AccessedValueWhenMultipleResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000285CC File Offset: 0x000267CC
		public static LocalizedString ValidationFailed(string name)
		{
			return new LocalizedString("ValidationFailed", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000285F4 File Offset: 0x000267F4
		public static LocalizedString UpgradeGateway605Block
		{
			get
			{
				return new LocalizedString("UpgradeGateway605Block", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002860B File Offset: 0x0002680B
		public static LocalizedString NotLoggedOntoDomain
		{
			get
			{
				return new LocalizedString("NotLoggedOntoDomain", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00028622 File Offset: 0x00026822
		public static LocalizedString DataReturnedFromDCIsInvalid
		{
			get
			{
				return new LocalizedString("DataReturnedFromDCIsInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002863C File Offset: 0x0002683C
		public static LocalizedString DelegatedFirstSP1Upgrade(string roleName)
		{
			return new LocalizedString("DelegatedFirstSP1Upgrade", Strings.ResourceManager, new object[]
			{
				roleName
			});
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00028664 File Offset: 0x00026864
		public static LocalizedString ValueNotFoundInCollection
		{
			get
			{
				return new LocalizedString("ValueNotFoundInCollection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002867C File Offset: 0x0002687C
		public static LocalizedString ErrorTooManyMatchingResults(string identity)
		{
			return new LocalizedString("ErrorTooManyMatchingResults", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000286A4 File Offset: 0x000268A4
		public static LocalizedString InstallViaServerManager(string component)
		{
			return new LocalizedString("InstallViaServerManager", Strings.ResourceManager, new object[]
			{
				component
			});
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000286CC File Offset: 0x000268CC
		public static LocalizedString ServerIsDynamicGroupExpansionServer(int count)
		{
			return new LocalizedString("ServerIsDynamicGroupExpansionServer", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000286FC File Offset: 0x000268FC
		public static LocalizedString NoIPv4AssignedForAdminTools(string product)
		{
			return new LocalizedString("NoIPv4AssignedForAdminTools", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00028724 File Offset: 0x00026924
		public static LocalizedString NoPreviousExchangeExistsInTopoWhilePrepareAD(string product)
		{
			return new LocalizedString("NoPreviousExchangeExistsInTopoWhilePrepareAD", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002874C File Offset: 0x0002694C
		public static LocalizedString MpsSvcStopped
		{
			get
			{
				return new LocalizedString("MpsSvcStopped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00028763 File Offset: 0x00026963
		public static LocalizedString DCAcceptedDomainNameFound
		{
			get
			{
				return new LocalizedString("DCAcceptedDomainNameFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002877A File Offset: 0x0002697A
		public static LocalizedString CannotAccessAD
		{
			get
			{
				return new LocalizedString("CannotAccessAD", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00028794 File Offset: 0x00026994
		public static LocalizedString DuplicateShortProvisionedName(string name)
		{
			return new LocalizedString("DuplicateShortProvisionedName", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000287BC File Offset: 0x000269BC
		public static LocalizedString ServerIsLastHubForEdgeSubscription(string siteName)
		{
			return new LocalizedString("ServerIsLastHubForEdgeSubscription", Strings.ResourceManager, new object[]
			{
				siteName
			});
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000287E4 File Offset: 0x000269E4
		public static LocalizedString ServerIsSourceForSendConnector(int count)
		{
			return new LocalizedString("ServerIsSourceForSendConnector", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00028811 File Offset: 0x00026A11
		public static LocalizedString UpdateProgressForWrongAnalysis
		{
			get
			{
				return new LocalizedString("UpdateProgressForWrongAnalysis", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00028828 File Offset: 0x00026A28
		public static LocalizedString OrgRelTargetAppUriToSearch(string uri)
		{
			return new LocalizedString("OrgRelTargetAppUriToSearch", Strings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00028850 File Offset: 0x00026A50
		public static LocalizedString ServerNotPrepared
		{
			get
			{
				return new LocalizedString("ServerNotPrepared", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00028867 File Offset: 0x00026A67
		public static LocalizedString CheckingTheAcceptedDomainAgainstOrgRelationshipDomains
		{
			get
			{
				return new LocalizedString("CheckingTheAcceptedDomainAgainstOrgRelationshipDomains", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002887E File Offset: 0x00026A7E
		public static LocalizedString VC2013PrereqMissing
		{
			get
			{
				return new LocalizedString("VC2013PrereqMissing", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00028898 File Offset: 0x00026A98
		public static LocalizedString HttpSiteAccessError(string uri, string message)
		{
			return new LocalizedString("HttpSiteAccessError", Strings.ResourceManager, new object[]
			{
				uri,
				message
			});
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000288C4 File Offset: 0x00026AC4
		public static LocalizedString DomainPrepRequired
		{
			get
			{
				return new LocalizedString("DomainPrepRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x000288DB File Offset: 0x00026ADB
		public static LocalizedString NoMatchWasFoundBetweenTheOrgRelDomainsAndDCAcceptedDomains
		{
			get
			{
				return new LocalizedString("NoMatchWasFoundBetweenTheOrgRelDomainsAndDCAcceptedDomains", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000288F2 File Offset: 0x00026AF2
		public static LocalizedString MSDTCStopped
		{
			get
			{
				return new LocalizedString("MSDTCStopped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00028909 File Offset: 0x00026B09
		public static LocalizedString CannotUninstallOABServer
		{
			get
			{
				return new LocalizedString("CannotUninstallOABServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00028920 File Offset: 0x00026B20
		public static LocalizedString StringContains(string first, string second)
		{
			return new LocalizedString("StringContains", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002894C File Offset: 0x00026B4C
		public static LocalizedString TestDCSettingsForHybridEnabledTenantPassed
		{
			get
			{
				return new LocalizedString("TestDCSettingsForHybridEnabledTenantPassed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00028963 File Offset: 0x00026B63
		public static LocalizedString Win7LDRGDRRMSManifestExpiryUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win7LDRGDRRMSManifestExpiryUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002897A File Offset: 0x00026B7A
		public static LocalizedString NoE14ServerWarning
		{
			get
			{
				return new LocalizedString("NoE14ServerWarning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00028991 File Offset: 0x00026B91
		public static LocalizedString InstallOnDCInADSplitPermissionMode
		{
			get
			{
				return new LocalizedString("InstallOnDCInADSplitPermissionMode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000289A8 File Offset: 0x00026BA8
		public static LocalizedString PrereqAnalysisRuleStarted(string memberName, string parentName, string ruleType)
		{
			return new LocalizedString("PrereqAnalysisRuleStarted", Strings.ResourceManager, new object[]
			{
				memberName,
				parentName,
				ruleType
			});
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x000289D8 File Offset: 0x00026BD8
		public static LocalizedString DelegatedBridgeheadFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedBridgeheadFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x000289EF File Offset: 0x00026BEF
		public static LocalizedString CafeRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("CafeRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00028A06 File Offset: 0x00026C06
		public static LocalizedString UnifiedMessagingRoleNotInstalled
		{
			get
			{
				return new LocalizedString("UnifiedMessagingRoleNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00028A1D File Offset: 0x00026C1D
		public static LocalizedString CannotUninstallDelegatedServer
		{
			get
			{
				return new LocalizedString("CannotUninstallDelegatedServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00028A34 File Offset: 0x00026C34
		public static LocalizedString ResultAncestorNotFound(string ancestorName)
		{
			return new LocalizedString("ResultAncestorNotFound", Strings.ResourceManager, new object[]
			{
				ancestorName
			});
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00028A5C File Offset: 0x00026C5C
		public static LocalizedString MessagesInQueue(string queueNames)
		{
			return new LocalizedString("MessagesInQueue", Strings.ResourceManager, new object[]
			{
				queueNames
			});
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00028A84 File Offset: 0x00026C84
		public static LocalizedString WinRMDisabledOrNotInstalled
		{
			get
			{
				return new LocalizedString("WinRMDisabledOrNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00028A9B File Offset: 0x00026C9B
		public static LocalizedString W2K8R2PrepareAdLdifdeNotInstalled
		{
			get
			{
				return new LocalizedString("W2K8R2PrepareAdLdifdeNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00028AB2 File Offset: 0x00026CB2
		public static LocalizedString DelegatedFrontendTransportFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedFrontendTransportFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00028AC9 File Offset: 0x00026CC9
		public static LocalizedString PendingReboot
		{
			get
			{
				return new LocalizedString("PendingReboot", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public static LocalizedString ScanningFailed(string name)
		{
			return new LocalizedString("ScanningFailed", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00028B08 File Offset: 0x00026D08
		public static LocalizedString NotLocalAdmin(string currentLoggedOnUser)
		{
			return new LocalizedString("NotLocalAdmin", Strings.ResourceManager, new object[]
			{
				currentLoggedOnUser
			});
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00028B30 File Offset: 0x00026D30
		public static LocalizedString SGFilesExist(string path)
		{
			return new LocalizedString("SGFilesExist", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00028B58 File Offset: 0x00026D58
		public static LocalizedString RootDomainMixedMode(string dn, string product)
		{
			return new LocalizedString("RootDomainMixedMode", Strings.ResourceManager, new object[]
			{
				dn,
				product
			});
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00028B84 File Offset: 0x00026D84
		public static LocalizedString LangPackDiskSpaceCheck
		{
			get
			{
				return new LocalizedString("LangPackDiskSpaceCheck", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00028B9B File Offset: 0x00026D9B
		public static LocalizedString SetupLogEnd
		{
			get
			{
				return new LocalizedString("SetupLogEnd", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00028BB4 File Offset: 0x00026DB4
		public static LocalizedString ServerRemoveProvisioningCheck(string removeServerName)
		{
			return new LocalizedString("ServerRemoveProvisioningCheck", Strings.ResourceManager, new object[]
			{
				removeServerName
			});
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00028BDC File Offset: 0x00026DDC
		public static LocalizedString DelegatedUnifiedMessagingFirstInstall
		{
			get
			{
				return new LocalizedString("DelegatedUnifiedMessagingFirstInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00028BF4 File Offset: 0x00026DF4
		public static LocalizedString HybridInfoTotalCmdletTime(string session, double timeSeconds)
		{
			return new LocalizedString("HybridInfoTotalCmdletTime", Strings.ResourceManager, new object[]
			{
				session,
				timeSeconds
			});
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00028C25 File Offset: 0x00026E25
		public static LocalizedString Win7WindowsIdentityFoundationUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win7WindowsIdentityFoundationUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00028C3C File Offset: 0x00026E3C
		public static LocalizedString DelegatedFirstInstall(string roleName)
		{
			return new LocalizedString("DelegatedFirstInstall", Strings.ResourceManager, new object[]
			{
				roleName
			});
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00028C64 File Offset: 0x00026E64
		public static LocalizedString E14BridgeheadRoleNotFound
		{
			get
			{
				return new LocalizedString("E14BridgeheadRoleNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00028C7B File Offset: 0x00026E7B
		public static LocalizedString UnifiedMessagingRoleAlreadyExists
		{
			get
			{
				return new LocalizedString("UnifiedMessagingRoleAlreadyExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00028C94 File Offset: 0x00026E94
		public static LocalizedString PrereqAnalysisFailedRule(string ruleName, string message)
		{
			return new LocalizedString("PrereqAnalysisFailedRule", Strings.ResourceManager, new object[]
			{
				ruleName,
				message
			});
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public static LocalizedString SetupLogInitializeFailure(string msg)
		{
			return new LocalizedString("SetupLogInitializeFailure", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00028CE8 File Offset: 0x00026EE8
		public static LocalizedString NetTcpPortSharingSvcNotAuto
		{
			get
			{
				return new LocalizedString("NetTcpPortSharingSvcNotAuto", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00028D00 File Offset: 0x00026F00
		public static LocalizedString HybridInfoGetObjectValue(string value, string identity, string command)
		{
			return new LocalizedString("HybridInfoGetObjectValue", Strings.ResourceManager, new object[]
			{
				value,
				identity,
				command
			});
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00028D30 File Offset: 0x00026F30
		public static LocalizedString InhBlockPublicFolderTree(string publicFolderTree, string distinguishedName)
		{
			return new LocalizedString("InhBlockPublicFolderTree", Strings.ResourceManager, new object[]
			{
				publicFolderTree,
				distinguishedName
			});
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00028D5C File Offset: 0x00026F5C
		public static LocalizedString ResourcePropertySchemaException(string exception)
		{
			return new LocalizedString("ResourcePropertySchemaException", Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00028D84 File Offset: 0x00026F84
		public static LocalizedString WindowsInstallerServiceDisabledOrNotInstalled
		{
			get
			{
				return new LocalizedString("WindowsInstallerServiceDisabledOrNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00028D9B File Offset: 0x00026F9B
		public static LocalizedString SMTPSvcInstalled
		{
			get
			{
				return new LocalizedString("SMTPSvcInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00028DB2 File Offset: 0x00026FB2
		public static LocalizedString DelegatedUnifiedMessagingFirstSP1upgrade
		{
			get
			{
				return new LocalizedString("DelegatedUnifiedMessagingFirstSP1upgrade", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00028DCC File Offset: 0x00026FCC
		public static LocalizedString MsfteUpgradeIssue(string user)
		{
			return new LocalizedString("MsfteUpgradeIssue", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00028DF4 File Offset: 0x00026FF4
		public static LocalizedString OnPremisesTestFailedWithException(string error)
		{
			return new LocalizedString("OnPremisesTestFailedWithException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00028E1C File Offset: 0x0002701C
		public static LocalizedString OldADAMInstalled
		{
			get
			{
				return new LocalizedString("OldADAMInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00028E33 File Offset: 0x00027033
		public static LocalizedString RunningOnPremTest
		{
			get
			{
				return new LocalizedString("RunningOnPremTest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00028E4C File Offset: 0x0002704C
		public static LocalizedString ServerIsGroupExpansionServer(int count)
		{
			return new LocalizedString("ServerIsGroupExpansionServer", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00028E7C File Offset: 0x0002707C
		public static LocalizedString ProcessNeedsToBeClosedOnUpgrade(string list)
		{
			return new LocalizedString("ProcessNeedsToBeClosedOnUpgrade", Strings.ResourceManager, new object[]
			{
				list
			});
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00028EA4 File Offset: 0x000270A4
		public static LocalizedString UcmaRedistMsi
		{
			get
			{
				return new LocalizedString("UcmaRedistMsi", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00028EBB File Offset: 0x000270BB
		public static LocalizedString EitherOrgRelOrAcceptDomainsWhereNull
		{
			get
			{
				return new LocalizedString("EitherOrgRelOrAcceptDomainsWhereNull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00028ED2 File Offset: 0x000270D2
		public static LocalizedString ADAMSvcStopped
		{
			get
			{
				return new LocalizedString("ADAMSvcStopped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00028EE9 File Offset: 0x000270E9
		public static LocalizedString W2K8R2PrepareSchemaLdifdeNotInstalled
		{
			get
			{
				return new LocalizedString("W2K8R2PrepareSchemaLdifdeNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00028F00 File Offset: 0x00027100
		public static LocalizedString LocalComputerIsDCInChildDomain
		{
			get
			{
				return new LocalizedString("LocalComputerIsDCInChildDomain", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00028F17 File Offset: 0x00027117
		public static LocalizedString ClusSvcInstalledRoleBlock
		{
			get
			{
				return new LocalizedString("ClusSvcInstalledRoleBlock", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00028F30 File Offset: 0x00027130
		public static LocalizedString ComponentIsRequiredToInstall(string name)
		{
			return new LocalizedString("ComponentIsRequiredToInstall", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00028F58 File Offset: 0x00027158
		public static LocalizedString HybridInfoCmdletEnd(string session, string cmdlet, string elapsed)
		{
			return new LocalizedString("HybridInfoCmdletEnd", Strings.ResourceManager, new object[]
			{
				session,
				cmdlet,
				elapsed
			});
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00028F88 File Offset: 0x00027188
		public static LocalizedString ExchangeAlreadyInstalled(string name)
		{
			return new LocalizedString("ExchangeAlreadyInstalled", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00028FB0 File Offset: 0x000271B0
		public static LocalizedString PrereqAnalysisStarted
		{
			get
			{
				return new LocalizedString("PrereqAnalysisStarted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00028FC7 File Offset: 0x000271C7
		public static LocalizedString BridgeheadRoleNotInstalled
		{
			get
			{
				return new LocalizedString("BridgeheadRoleNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00028FDE File Offset: 0x000271DE
		public static LocalizedString OrgConfigDataIsEmptyOrNull
		{
			get
			{
				return new LocalizedString("OrgConfigDataIsEmptyOrNull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00028FF5 File Offset: 0x000271F5
		public static LocalizedString EdgeSubscriptionExists
		{
			get
			{
				return new LocalizedString("EdgeSubscriptionExists", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002900C File Offset: 0x0002720C
		public static LocalizedString QueryFinish(string queryName, string result)
		{
			return new LocalizedString("QueryFinish", Strings.ResourceManager, new object[]
			{
				queryName,
				result
			});
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00029038 File Offset: 0x00027238
		public static LocalizedString InvalidOrTamperedFile
		{
			get
			{
				return new LocalizedString("InvalidOrTamperedFile", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002904F File Offset: 0x0002724F
		public static LocalizedString SendConnectorException
		{
			get
			{
				return new LocalizedString("SendConnectorException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00029066 File Offset: 0x00027266
		public static LocalizedString FqdnMissing
		{
			get
			{
				return new LocalizedString("FqdnMissing", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002907D File Offset: 0x0002727D
		public static LocalizedString SchemaNotPreparedExtendedRights
		{
			get
			{
				return new LocalizedString("SchemaNotPreparedExtendedRights", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00029094 File Offset: 0x00027294
		public static LocalizedString LangPackBundleCheck
		{
			get
			{
				return new LocalizedString("LangPackBundleCheck", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000290AB File Offset: 0x000272AB
		public static LocalizedString FailedResult
		{
			get
			{
				return new LocalizedString("FailedResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000290C4 File Offset: 0x000272C4
		public static LocalizedString ComponentIsRequired(string componentName)
		{
			return new LocalizedString("ComponentIsRequired", Strings.ResourceManager, new object[]
			{
				componentName
			});
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000290EC File Offset: 0x000272EC
		public static LocalizedString Win2k12RefsUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win2k12RefsUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00029103 File Offset: 0x00027303
		public static LocalizedString CannotAccessHttpSiteForEngineUpdates
		{
			get
			{
				return new LocalizedString("CannotAccessHttpSiteForEngineUpdates", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002911A File Offset: 0x0002731A
		public static LocalizedString SpeechRedistMsi
		{
			get
			{
				return new LocalizedString("SpeechRedistMsi", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00029134 File Offset: 0x00027334
		public static LocalizedString PrereqAnalysisStopped(TimeSpan duration)
		{
			return new LocalizedString("PrereqAnalysisStopped", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00029164 File Offset: 0x00027364
		public static LocalizedString InvalidLocalComputerFQDN(string name)
		{
			return new LocalizedString("InvalidLocalComputerFQDN", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002918C File Offset: 0x0002738C
		public static LocalizedString PendingRebootWindowsComponents
		{
			get
			{
				return new LocalizedString("PendingRebootWindowsComponents", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000291A4 File Offset: 0x000273A4
		public static LocalizedString ComponentIsRecommended(string componentName)
		{
			return new LocalizedString("ComponentIsRecommended", Strings.ResourceManager, new object[]
			{
				componentName
			});
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000291CC File Offset: 0x000273CC
		public static LocalizedString OSMinVersionForFSMONotMet
		{
			get
			{
				return new LocalizedString("OSMinVersionForFSMONotMet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000291E4 File Offset: 0x000273E4
		public static LocalizedString Exchange2007ServerInstalled(string server)
		{
			return new LocalizedString("Exchange2007ServerInstalled", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002920C File Offset: 0x0002740C
		public static LocalizedString UserName(string userName)
		{
			return new LocalizedString("UserName", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00029234 File Offset: 0x00027434
		public static LocalizedString MissingDNSHostRecord(string dns)
		{
			return new LocalizedString("MissingDNSHostRecord", Strings.ResourceManager, new object[]
			{
				dns
			});
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002925C File Offset: 0x0002745C
		public static LocalizedString AdditionalUMLangPackExists(string languagePack)
		{
			return new LocalizedString("AdditionalUMLangPackExists", Strings.ResourceManager, new object[]
			{
				languagePack
			});
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00029284 File Offset: 0x00027484
		public static LocalizedString InstallWatermark(string role)
		{
			return new LocalizedString("InstallWatermark", Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000292AC File Offset: 0x000274AC
		public static LocalizedString TargetPathCompressed(string targetDir)
		{
			return new LocalizedString("TargetPathCompressed", Strings.ResourceManager, new object[]
			{
				targetDir
			});
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000292D4 File Offset: 0x000274D4
		public static LocalizedString InvalidDNSDomainName
		{
			get
			{
				return new LocalizedString("InvalidDNSDomainName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x000292EB File Offset: 0x000274EB
		public static LocalizedString UMLangPackDiskSpaceCheck
		{
			get
			{
				return new LocalizedString("UMLangPackDiskSpaceCheck", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00029304 File Offset: 0x00027504
		public static LocalizedString PrereqAnalysisMemberStopped(string memberType, string memberName, TimeSpan duration)
		{
			return new LocalizedString("PrereqAnalysisMemberStopped", Strings.ResourceManager, new object[]
			{
				memberType,
				memberName,
				duration
			});
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002933C File Offset: 0x0002753C
		public static LocalizedString PrepareDomainNotAdmin(string prepareDomain)
		{
			return new LocalizedString("PrepareDomainNotAdmin", Strings.ResourceManager, new object[]
			{
				prepareDomain
			});
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00029364 File Offset: 0x00027564
		public static LocalizedString ServerNotInSchemaMasterSite(string name)
		{
			return new LocalizedString("ServerNotInSchemaMasterSite", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002938C File Offset: 0x0002758C
		public static LocalizedString Win7LDRRMSManifestExpiryUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win7LDRRMSManifestExpiryUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000293A3 File Offset: 0x000275A3
		public static LocalizedString TenantIsBeingUpgradedFromE14
		{
			get
			{
				return new LocalizedString("TenantIsBeingUpgradedFromE14", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000293BA File Offset: 0x000275BA
		public static LocalizedString InvalidADSite
		{
			get
			{
				return new LocalizedString("InvalidADSite", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000293D4 File Offset: 0x000275D4
		public static LocalizedString ShouldBeNullOrEmpty(string name)
		{
			return new LocalizedString("ShouldBeNullOrEmpty", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000293FC File Offset: 0x000275FC
		public static LocalizedString E15E12CoexistenceMinOSReqFailure(string servers)
		{
			return new LocalizedString("E15E12CoexistenceMinOSReqFailure", Strings.ResourceManager, new object[]
			{
				servers
			});
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00029424 File Offset: 0x00027624
		public static LocalizedString RemoteRegException
		{
			get
			{
				return new LocalizedString("RemoteRegException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002943B File Offset: 0x0002763B
		public static LocalizedString ADNotPreparedForHostingValidator
		{
			get
			{
				return new LocalizedString("ADNotPreparedForHostingValidator", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00029454 File Offset: 0x00027654
		public static LocalizedString AlreadyInstalledUMLangPacks(string cultures)
		{
			return new LocalizedString("AlreadyInstalledUMLangPacks", Strings.ResourceManager, new object[]
			{
				cultures
			});
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002947C File Offset: 0x0002767C
		public static LocalizedString StringNotContains(string first, string second)
		{
			return new LocalizedString("StringNotContains", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000294A8 File Offset: 0x000276A8
		public static LocalizedString Win2k12RollupUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win2k12RollupUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000294C0 File Offset: 0x000276C0
		public static LocalizedString NotEqual(string first, string second)
		{
			return new LocalizedString("NotEqual", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x000294EC File Offset: 0x000276EC
		public static LocalizedString InvalidOSVersion
		{
			get
			{
				return new LocalizedString("InvalidOSVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00029504 File Offset: 0x00027704
		public static LocalizedString StringNotStartsWith(string first, string second)
		{
			return new LocalizedString("StringNotStartsWith", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00029530 File Offset: 0x00027730
		public static LocalizedString ServerNotInSchemaMasterDomain(string name)
		{
			return new LocalizedString("ServerNotInSchemaMasterDomain", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00029558 File Offset: 0x00027758
		public static LocalizedString TraceFunctionExit(Type type, string methodName)
		{
			return new LocalizedString("TraceFunctionExit", Strings.ResourceManager, new object[]
			{
				type,
				methodName
			});
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00029584 File Offset: 0x00027784
		public static LocalizedString MinimumFrameworkNotInstalled
		{
			get
			{
				return new LocalizedString("MinimumFrameworkNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002959B File Offset: 0x0002779B
		public static LocalizedString SchemaNotPrepared
		{
			get
			{
				return new LocalizedString("SchemaNotPrepared", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000295B2 File Offset: 0x000277B2
		public static LocalizedString HostingActiveDirectorySplitPermissionsNotSupported
		{
			get
			{
				return new LocalizedString("HostingActiveDirectorySplitPermissionsNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000295C9 File Offset: 0x000277C9
		public static LocalizedString DisplayVersionDCBitUpgradeStatusBitAndVersionAreCorrect
		{
			get
			{
				return new LocalizedString("DisplayVersionDCBitUpgradeStatusBitAndVersionAreCorrect", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000295E0 File Offset: 0x000277E0
		public static LocalizedString ADAMPortAlreadyInUse(string adamPort)
		{
			return new LocalizedString("ADAMPortAlreadyInUse", Strings.ResourceManager, new object[]
			{
				adamPort
			});
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00029608 File Offset: 0x00027808
		public static LocalizedString UpgradeMinVersionBlock
		{
			get
			{
				return new LocalizedString("UpgradeMinVersionBlock", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002961F File Offset: 0x0002781F
		public static LocalizedString InvalidOSVersionForAdminTools
		{
			get
			{
				return new LocalizedString("InvalidOSVersionForAdminTools", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00029638 File Offset: 0x00027838
		public static LocalizedString AllServersOfHigherVersionFailure(string servers)
		{
			return new LocalizedString("AllServersOfHigherVersionFailure", Strings.ResourceManager, new object[]
			{
				servers
			});
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00029660 File Offset: 0x00027860
		public static LocalizedString ConditionIsFalse
		{
			get
			{
				return new LocalizedString("ConditionIsFalse", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00029677 File Offset: 0x00027877
		public static LocalizedString OrgConfigHashDoesNotExist
		{
			get
			{
				return new LocalizedString("OrgConfigHashDoesNotExist", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00029690 File Offset: 0x00027890
		public static LocalizedString HelpIdNotDefined(string ruleName)
		{
			return new LocalizedString("HelpIdNotDefined", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000296B8 File Offset: 0x000278B8
		public static LocalizedString ProvisionedUpdateRequired
		{
			get
			{
				return new LocalizedString("ProvisionedUpdateRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x000296CF File Offset: 0x000278CF
		public static LocalizedString ComputerNotPartofDomain
		{
			get
			{
				return new LocalizedString("ComputerNotPartofDomain", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000296E8 File Offset: 0x000278E8
		public static LocalizedString StringStartsWith(string first, string second)
		{
			return new LocalizedString("StringStartsWith", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00029714 File Offset: 0x00027914
		public static LocalizedString Win2k12UrefsUpdateNotInstalled
		{
			get
			{
				return new LocalizedString("Win2k12UrefsUpdateNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002972B File Offset: 0x0002792B
		public static LocalizedString CannotReturnNullForResult
		{
			get
			{
				return new LocalizedString("CannotReturnNullForResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00029742 File Offset: 0x00027942
		public static LocalizedString SetADSplitPermissionWhenExchangeServerRolesOnDC
		{
			get
			{
				return new LocalizedString("SetADSplitPermissionWhenExchangeServerRolesOnDC", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00029759 File Offset: 0x00027959
		public static LocalizedString InflateAndDecoding
		{
			get
			{
				return new LocalizedString("InflateAndDecoding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00029770 File Offset: 0x00027970
		public static LocalizedString OffLineABServerDeleted(string oabName)
		{
			return new LocalizedString("OffLineABServerDeleted", Strings.ResourceManager, new object[]
			{
				oabName
			});
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00029798 File Offset: 0x00027998
		public static LocalizedString ADAMLonghornWin7ServerNotInstalled
		{
			get
			{
				return new LocalizedString("ADAMLonghornWin7ServerNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000297AF File Offset: 0x000279AF
		public static LocalizedString DCIsUpgradingOrganizationFound
		{
			get
			{
				return new LocalizedString("DCIsUpgradingOrganizationFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000297C6 File Offset: 0x000279C6
		public static LocalizedString MailboxRoleNotInstalled
		{
			get
			{
				return new LocalizedString("MailboxRoleNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000297E0 File Offset: 0x000279E0
		public static LocalizedString PrereqAnalysisMemberEvaluated(string memberType, string memberName, bool hasException, string value, string parentValue, int threadId, TimeSpan duration)
		{
			return new LocalizedString("PrereqAnalysisMemberEvaluated", Strings.ResourceManager, new object[]
			{
				memberType,
				memberName,
				hasException,
				value,
				parentValue,
				threadId,
				duration
			});
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00029832 File Offset: 0x00027A32
		public static LocalizedString PrereqAnalysisParentExceptionValue
		{
			get
			{
				return new LocalizedString("PrereqAnalysisParentExceptionValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00029849 File Offset: 0x00027A49
		public static LocalizedString TenantHasNotYetBeenUpgradedToE15
		{
			get
			{
				return new LocalizedString("TenantHasNotYetBeenUpgradedToE15", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00029860 File Offset: 0x00027A60
		public static LocalizedString OSWebEditionValidator(string product)
		{
			return new LocalizedString("OSWebEditionValidator", Strings.ResourceManager, new object[]
			{
				product
			});
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00029888 File Offset: 0x00027A88
		public static LocalizedString DotNetFrameworkNeedsUpdate
		{
			get
			{
				return new LocalizedString("DotNetFrameworkNeedsUpdate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000298A0 File Offset: 0x00027AA0
		public static LocalizedString NotLessThan(string first, string second)
		{
			return new LocalizedString("NotLessThan", Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000298CC File Offset: 0x00027ACC
		public static LocalizedString ADAMSSLPortAlreadyInUse(string adamSSLPort)
		{
			return new LocalizedString("ADAMSSLPortAlreadyInUse", Strings.ResourceManager, new object[]
			{
				adamSSLPort
			});
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000298F4 File Offset: 0x00027AF4
		public static LocalizedString OSMinVersionForAdminToolsNotMet
		{
			get
			{
				return new LocalizedString("OSMinVersionForAdminToolsNotMet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002990C File Offset: 0x00027B0C
		public static LocalizedString InvalidRecordType(string recordType)
		{
			return new LocalizedString("InvalidRecordType", Strings.ResourceManager, new object[]
			{
				recordType
			});
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00029934 File Offset: 0x00027B34
		public static LocalizedString NullLoggerHasBeenPassedIn
		{
			get
			{
				return new LocalizedString("NullLoggerHasBeenPassedIn", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002994C File Offset: 0x00027B4C
		public static LocalizedString PrereqAnalysisExpectedFailure(string memberName, string message)
		{
			return new LocalizedString("PrereqAnalysisExpectedFailure", Strings.ResourceManager, new object[]
			{
				memberName,
				message
			});
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00029978 File Offset: 0x00027B78
		public static LocalizedString HybridInfoPurePSObjectsNotSupported
		{
			get
			{
				return new LocalizedString("HybridInfoPurePSObjectsNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0002998F File Offset: 0x00027B8F
		public static LocalizedString LangPackBundleVersioning
		{
			get
			{
				return new LocalizedString("LangPackBundleVersioning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000299A8 File Offset: 0x00027BA8
		public static LocalizedString DomainMixedMode(string dn, string product)
		{
			return new LocalizedString("DomainMixedMode", Strings.ResourceManager, new object[]
			{
				dn,
				product
			});
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000299D4 File Offset: 0x00027BD4
		public static LocalizedString DomainControllerOutOfSiteValidator(string adSiteName, string dcSiteName, string dc)
		{
			return new LocalizedString("DomainControllerOutOfSiteValidator", Strings.ResourceManager, new object[]
			{
				adSiteName,
				dcSiteName,
				dc
			});
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00029A04 File Offset: 0x00027C04
		public static LocalizedString PrepareDomainModeMixed(string ncName)
		{
			return new LocalizedString("PrepareDomainModeMixed", Strings.ResourceManager, new object[]
			{
				ncName
			});
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00029A2C File Offset: 0x00027C2C
		public static LocalizedString SearchingForAttributes
		{
			get
			{
				return new LocalizedString("SearchingForAttributes", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00029A44 File Offset: 0x00027C44
		public static LocalizedString LocalDomainMixedMode(string name, string product)
		{
			return new LocalizedString("LocalDomainMixedMode", Strings.ResourceManager, new object[]
			{
				name,
				product
			});
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00029A70 File Offset: 0x00027C70
		public static LocalizedString UnwillingToRemoveMailboxDatabase(string exception)
		{
			return new LocalizedString("UnwillingToRemoveMailboxDatabase", Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00029A98 File Offset: 0x00027C98
		public static LocalizedString ADAMDataPathExists(string adamDataPath)
		{
			return new LocalizedString("ADAMDataPathExists", Strings.ResourceManager, new object[]
			{
				adamDataPath
			});
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00029AC0 File Offset: 0x00027CC0
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040005DD RID: 1501
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(176);

		// Token: 0x040005DE RID: 1502
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Deployment.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200007B RID: 123
		public enum IDs : uint
		{
			// Token: 0x040005E0 RID: 1504
			ExchangeVersionBlock = 3076655694U,
			// Token: 0x040005E1 RID: 1505
			InflatingAndDecodingPassedInHash = 2041384979U,
			// Token: 0x040005E2 RID: 1506
			MSFilterPackV2NotInstalled = 3961637084U,
			// Token: 0x040005E3 RID: 1507
			MailboxRoleAlreadyExists = 286971094U,
			// Token: 0x040005E4 RID: 1508
			TooManyResults = 3853989365U,
			// Token: 0x040005E5 RID: 1509
			EventSystemStopped = 3498036640U,
			// Token: 0x040005E6 RID: 1510
			WindowsServer2008CoreServerEdition = 971944032U,
			// Token: 0x040005E7 RID: 1511
			DelegatedClientAccessFirstInstall = 2386277257U,
			// Token: 0x040005E8 RID: 1512
			NoConnectorToStar = 564094161U,
			// Token: 0x040005E9 RID: 1513
			LangPackUpgradeVersioning = 1256831615U,
			// Token: 0x040005EA RID: 1514
			PreviousVersionOfExchangeAlreadyInstalled = 2177637643U,
			// Token: 0x040005EB RID: 1515
			CannotUninstallClusterNode = 3383574593U,
			// Token: 0x040005EC RID: 1516
			TestDCSettingsForHybridEnabledTenantFailed = 173856461U,
			// Token: 0x040005ED RID: 1517
			NoE12ServerWarning = 975440992U,
			// Token: 0x040005EE RID: 1518
			DelegatedCafeFirstInstall = 2186576589U,
			// Token: 0x040005EF RID: 1519
			ParsingXmlDataFromDCFileOrDCCmdlet = 4277708119U,
			// Token: 0x040005F0 RID: 1520
			InvalidPSCredential = 3412170869U,
			// Token: 0x040005F1 RID: 1521
			FoundOrgConfigHashInConfigFile = 233623067U,
			// Token: 0x040005F2 RID: 1522
			PSCredentialAndTheOrganizationHashIsNull = 1884243248U,
			// Token: 0x040005F3 RID: 1523
			ClientAccessRoleAlreadyExists = 1760005143U,
			// Token: 0x040005F4 RID: 1524
			AdcFound = 2779888852U,
			// Token: 0x040005F5 RID: 1525
			ForestLevelNotWin2003Native = 1542399594U,
			// Token: 0x040005F6 RID: 1526
			InstallExchangeRolesOnDomainController = 2107927772U,
			// Token: 0x040005F7 RID: 1527
			ServerFQDNDisplayName = 1889541029U,
			// Token: 0x040005F8 RID: 1528
			MemberOfDatabaseAvailabilityGroup = 2033421306U,
			// Token: 0x040005F9 RID: 1529
			DelegatedMailboxFirstInstall = 2446058696U,
			// Token: 0x040005FA RID: 1530
			CannotSetADSplitPermissionValidator = 3207300105U,
			// Token: 0x040005FB RID: 1531
			FrontendTransportRoleAlreadyExists = 168558763U,
			// Token: 0x040005FC RID: 1532
			GatewayUpgrade605Block = 4109056594U,
			// Token: 0x040005FD RID: 1533
			ShouldReRunSetupForW3SVC = 3696305017U,
			// Token: 0x040005FE RID: 1534
			InvalidLocalServerName = 2679933748U,
			// Token: 0x040005FF RID: 1535
			VC2012PrereqMissing = 3945170485U,
			// Token: 0x04000600 RID: 1536
			DelegatedClientAccessFirstSP1upgrade = 3419466150U,
			// Token: 0x04000601 RID: 1537
			TestNotRunDueToRegistryKey = 3480633415U,
			// Token: 0x04000602 RID: 1538
			GetOrgConfigWasRunOnPremises = 782041369U,
			// Token: 0x04000603 RID: 1539
			AccessedFailedResult = 1141647571U,
			// Token: 0x04000604 RID: 1540
			Iis32BitMode = 3552416860U,
			// Token: 0x04000605 RID: 1541
			EitherTheOnPremAcceptedDomainListOrTheDCAcceptedDomainsAreEmpty = 719584928U,
			// Token: 0x04000606 RID: 1542
			DCAdminDisplayVersionFound = 3601945308U,
			// Token: 0x04000607 RID: 1543
			GlobalServerInstall = 1667305647U,
			// Token: 0x04000608 RID: 1544
			SetupLogStarted = 3878879664U,
			// Token: 0x04000609 RID: 1545
			HybridInfoObjectNotFound = 3187185748U,
			// Token: 0x0400060A RID: 1546
			DelegatedMailboxFirstSP1upgrade = 1950741283U,
			// Token: 0x0400060B RID: 1547
			RunningTenantTestDCSettingsForHybridEnabledTenant = 2076519121U,
			// Token: 0x0400060C RID: 1548
			PowerShellExecutionPolicyCheck = 2999486005U,
			// Token: 0x0400060D RID: 1549
			DCEndPointIsEmpty = 1793580687U,
			// Token: 0x0400060E RID: 1550
			OSMinVersionNotMet = 3615196321U,
			// Token: 0x0400060F RID: 1551
			AttemptingToParseTheXmlData = 704357583U,
			// Token: 0x04000610 RID: 1552
			DCPreviousAdminDisplayVersionFound = 2759777131U,
			// Token: 0x04000611 RID: 1553
			DotNetFrameworkMinVersionNotMet = 601266997U,
			// Token: 0x04000612 RID: 1554
			WinRMIISExtensionInstalled = 3940011457U,
			// Token: 0x04000613 RID: 1555
			SearchFoundationAssemblyLoaderKBNotInstalled = 1727977492U,
			// Token: 0x04000614 RID: 1556
			TenantIsRunningE15 = 841751018U,
			// Token: 0x04000615 RID: 1557
			Win7RpcHttpShouldRejectDuplicateConnB2PacketsUpdateNotInstalled = 541611516U,
			// Token: 0x04000616 RID: 1558
			DelegatedBridgeheadFirstInstall = 219074271U,
			// Token: 0x04000617 RID: 1559
			GetOrganizationConfigCmdletNotFound = 3254033387U,
			// Token: 0x04000618 RID: 1560
			TheFilePassedInIsNotFromGetOrganizationConfig = 3375133662U,
			// Token: 0x04000619 RID: 1561
			W3SVCDisabledOrNotInstalled = 3667045786U,
			// Token: 0x0400061A RID: 1562
			HostingModeNotAvailable = 1853686419U,
			// Token: 0x0400061B RID: 1563
			LangPackInstalled = 1062077161U,
			// Token: 0x0400061C RID: 1564
			InsufficientPrivledges = 3068577726U,
			// Token: 0x0400061D RID: 1565
			BadFilePassedIn = 1587969884U,
			// Token: 0x0400061E RID: 1566
			OSCheckedBuild = 3070759373U,
			// Token: 0x0400061F RID: 1567
			NetBIOSNameNotMatchesDNSHostName = 3443808673U,
			// Token: 0x04000620 RID: 1568
			NoGCInSite = 1073237881U,
			// Token: 0x04000621 RID: 1569
			InflateAndDecodeReturnedDataFromGetOrgConfig = 2097390153U,
			// Token: 0x04000622 RID: 1570
			LocalDomainNotPrepared = 4278863281U,
			// Token: 0x04000623 RID: 1571
			Exchange2000or2003PresentInOrg = 1570727447U,
			// Token: 0x04000624 RID: 1572
			Exchange2013AnyOnExchange2007or2010Server = 4143233965U,
			// Token: 0x04000625 RID: 1573
			RunningTenantHybridTest = 4199205241U,
			// Token: 0x04000626 RID: 1574
			DCIsDataCenterBitFound = 1023311823U,
			// Token: 0x04000627 RID: 1575
			PrereqAnalysisNullValue = 24167743U,
			// Token: 0x04000628 RID: 1576
			MinVersionCheck = 3736002086U,
			// Token: 0x04000629 RID: 1577
			ThereWasAnExceptionWhileCheckingForHybridConfiguration = 2170309289U,
			// Token: 0x0400062A RID: 1578
			ConnectingToTheDCToRunGetOrgConfig = 715361555U,
			// Token: 0x0400062B RID: 1579
			BridgeheadRoleAlreadyExists = 3133604821U,
			// Token: 0x0400062C RID: 1580
			MSFilterPackV2SP1NotInstalled = 1976971220U,
			// Token: 0x0400062D RID: 1581
			EmptyResults = 4164286807U,
			// Token: 0x0400062E RID: 1582
			ComputerRODC = 1626517757U,
			// Token: 0x0400062F RID: 1583
			DelegatedCafeFirstSP1upgrade = 1186202058U,
			// Token: 0x04000630 RID: 1584
			Win7RpcHttpAssocCookieGuidUpdateNotInstalled = 3145781320U,
			// Token: 0x04000631 RID: 1585
			AccessedValueWhenMultipleResults = 1376602368U,
			// Token: 0x04000632 RID: 1586
			UpgradeGateway605Block = 1828122210U,
			// Token: 0x04000633 RID: 1587
			NotLoggedOntoDomain = 3401172205U,
			// Token: 0x04000634 RID: 1588
			DataReturnedFromDCIsInvalid = 4130092005U,
			// Token: 0x04000635 RID: 1589
			ValueNotFoundInCollection = 1040666117U,
			// Token: 0x04000636 RID: 1590
			MpsSvcStopped = 841278233U,
			// Token: 0x04000637 RID: 1591
			DCAcceptedDomainNameFound = 1554849195U,
			// Token: 0x04000638 RID: 1592
			CannotAccessAD = 641150786U,
			// Token: 0x04000639 RID: 1593
			UpdateProgressForWrongAnalysis = 1752859078U,
			// Token: 0x0400063A RID: 1594
			ServerNotPrepared = 53953329U,
			// Token: 0x0400063B RID: 1595
			CheckingTheAcceptedDomainAgainstOrgRelationshipDomains = 2931842358U,
			// Token: 0x0400063C RID: 1596
			VC2013PrereqMissing = 1081610576U,
			// Token: 0x0400063D RID: 1597
			DomainPrepRequired = 1463453412U,
			// Token: 0x0400063E RID: 1598
			NoMatchWasFoundBetweenTheOrgRelDomainsAndDCAcceptedDomains = 4229980062U,
			// Token: 0x0400063F RID: 1599
			MSDTCStopped = 3125428864U,
			// Token: 0x04000640 RID: 1600
			CannotUninstallOABServer = 4101630194U,
			// Token: 0x04000641 RID: 1601
			TestDCSettingsForHybridEnabledTenantPassed = 2459322720U,
			// Token: 0x04000642 RID: 1602
			Win7LDRGDRRMSManifestExpiryUpdateNotInstalled = 1194462944U,
			// Token: 0x04000643 RID: 1603
			NoE14ServerWarning = 3696227166U,
			// Token: 0x04000644 RID: 1604
			InstallOnDCInADSplitPermissionMode = 3265622739U,
			// Token: 0x04000645 RID: 1605
			DelegatedBridgeheadFirstSP1upgrade = 2337843968U,
			// Token: 0x04000646 RID: 1606
			CafeRoleAlreadyExists = 2879980483U,
			// Token: 0x04000647 RID: 1607
			UnifiedMessagingRoleNotInstalled = 3469704679U,
			// Token: 0x04000648 RID: 1608
			CannotUninstallDelegatedServer = 2121009437U,
			// Token: 0x04000649 RID: 1609
			WinRMDisabledOrNotInstalled = 3379016135U,
			// Token: 0x0400064A RID: 1610
			W2K8R2PrepareAdLdifdeNotInstalled = 3219123481U,
			// Token: 0x0400064B RID: 1611
			DelegatedFrontendTransportFirstSP1upgrade = 4056098580U,
			// Token: 0x0400064C RID: 1612
			PendingReboot = 3957588404U,
			// Token: 0x0400064D RID: 1613
			LangPackDiskSpaceCheck = 3440414664U,
			// Token: 0x0400064E RID: 1614
			SetupLogEnd = 4195359116U,
			// Token: 0x0400064F RID: 1615
			DelegatedUnifiedMessagingFirstInstall = 1829824776U,
			// Token: 0x04000650 RID: 1616
			Win7WindowsIdentityFoundationUpdateNotInstalled = 569737677U,
			// Token: 0x04000651 RID: 1617
			E14BridgeheadRoleNotFound = 43746744U,
			// Token: 0x04000652 RID: 1618
			UnifiedMessagingRoleAlreadyExists = 2882191588U,
			// Token: 0x04000653 RID: 1619
			NetTcpPortSharingSvcNotAuto = 3915669625U,
			// Token: 0x04000654 RID: 1620
			WindowsInstallerServiceDisabledOrNotInstalled = 4251553116U,
			// Token: 0x04000655 RID: 1621
			SMTPSvcInstalled = 3733193906U,
			// Token: 0x04000656 RID: 1622
			DelegatedUnifiedMessagingFirstSP1upgrade = 2364459045U,
			// Token: 0x04000657 RID: 1623
			OldADAMInstalled = 392652342U,
			// Token: 0x04000658 RID: 1624
			RunningOnPremTest = 4254576536U,
			// Token: 0x04000659 RID: 1625
			UcmaRedistMsi = 3978292472U,
			// Token: 0x0400065A RID: 1626
			EitherOrgRelOrAcceptDomainsWhereNull = 3827371534U,
			// Token: 0x0400065B RID: 1627
			ADAMSvcStopped = 2570206190U,
			// Token: 0x0400065C RID: 1628
			W2K8R2PrepareSchemaLdifdeNotInstalled = 212290597U,
			// Token: 0x0400065D RID: 1629
			LocalComputerIsDCInChildDomain = 2160575392U,
			// Token: 0x0400065E RID: 1630
			ClusSvcInstalledRoleBlock = 275692458U,
			// Token: 0x0400065F RID: 1631
			PrereqAnalysisStarted = 1958335472U,
			// Token: 0x04000660 RID: 1632
			BridgeheadRoleNotInstalled = 2120465116U,
			// Token: 0x04000661 RID: 1633
			OrgConfigDataIsEmptyOrNull = 2369286853U,
			// Token: 0x04000662 RID: 1634
			EdgeSubscriptionExists = 1550596250U,
			// Token: 0x04000663 RID: 1635
			InvalidOrTamperedFile = 1584908934U,
			// Token: 0x04000664 RID: 1636
			SendConnectorException = 160644360U,
			// Token: 0x04000665 RID: 1637
			FqdnMissing = 1212051581U,
			// Token: 0x04000666 RID: 1638
			SchemaNotPreparedExtendedRights = 1980036245U,
			// Token: 0x04000667 RID: 1639
			LangPackBundleCheck = 1622330535U,
			// Token: 0x04000668 RID: 1640
			FailedResult = 2543736554U,
			// Token: 0x04000669 RID: 1641
			Win2k12RefsUpdateNotInstalled = 2361234894U,
			// Token: 0x0400066A RID: 1642
			CannotAccessHttpSiteForEngineUpdates = 3990514141U,
			// Token: 0x0400066B RID: 1643
			SpeechRedistMsi = 1332821968U,
			// Token: 0x0400066C RID: 1644
			PendingRebootWindowsComponents = 1132011277U,
			// Token: 0x0400066D RID: 1645
			OSMinVersionForFSMONotMet = 87247761U,
			// Token: 0x0400066E RID: 1646
			InvalidDNSDomainName = 2387324271U,
			// Token: 0x0400066F RID: 1647
			UMLangPackDiskSpaceCheck = 1254031586U,
			// Token: 0x04000670 RID: 1648
			Win7LDRRMSManifestExpiryUpdateNotInstalled = 3272988597U,
			// Token: 0x04000671 RID: 1649
			TenantIsBeingUpgradedFromE14 = 2902280231U,
			// Token: 0x04000672 RID: 1650
			InvalidADSite = 1412296371U,
			// Token: 0x04000673 RID: 1651
			RemoteRegException = 3588122483U,
			// Token: 0x04000674 RID: 1652
			ADNotPreparedForHostingValidator = 1276355988U,
			// Token: 0x04000675 RID: 1653
			Win2k12RollupUpdateNotInstalled = 4039112198U,
			// Token: 0x04000676 RID: 1654
			InvalidOSVersion = 2380070307U,
			// Token: 0x04000677 RID: 1655
			MinimumFrameworkNotInstalled = 2017435239U,
			// Token: 0x04000678 RID: 1656
			SchemaNotPrepared = 3014916009U,
			// Token: 0x04000679 RID: 1657
			HostingActiveDirectorySplitPermissionsNotSupported = 2501606560U,
			// Token: 0x0400067A RID: 1658
			DisplayVersionDCBitUpgradeStatusBitAndVersionAreCorrect = 2053317018U,
			// Token: 0x0400067B RID: 1659
			UpgradeMinVersionBlock = 624846257U,
			// Token: 0x0400067C RID: 1660
			InvalidOSVersionForAdminTools = 2938058350U,
			// Token: 0x0400067D RID: 1661
			ConditionIsFalse = 3831181830U,
			// Token: 0x0400067E RID: 1662
			OrgConfigHashDoesNotExist = 346774553U,
			// Token: 0x0400067F RID: 1663
			ProvisionedUpdateRequired = 2890177564U,
			// Token: 0x04000680 RID: 1664
			ComputerNotPartofDomain = 1981068904U,
			// Token: 0x04000681 RID: 1665
			Win2k12UrefsUpdateNotInstalled = 985137777U,
			// Token: 0x04000682 RID: 1666
			CannotReturnNullForResult = 260127048U,
			// Token: 0x04000683 RID: 1667
			SetADSplitPermissionWhenExchangeServerRolesOnDC = 2452722357U,
			// Token: 0x04000684 RID: 1668
			InflateAndDecoding = 3769154951U,
			// Token: 0x04000685 RID: 1669
			ADAMLonghornWin7ServerNotInstalled = 2393957831U,
			// Token: 0x04000686 RID: 1670
			DCIsUpgradingOrganizationFound = 2715471913U,
			// Token: 0x04000687 RID: 1671
			MailboxRoleNotInstalled = 3301968605U,
			// Token: 0x04000688 RID: 1672
			PrereqAnalysisParentExceptionValue = 3344709799U,
			// Token: 0x04000689 RID: 1673
			TenantHasNotYetBeenUpgradedToE15 = 1535334837U,
			// Token: 0x0400068A RID: 1674
			DotNetFrameworkNeedsUpdate = 758899952U,
			// Token: 0x0400068B RID: 1675
			OSMinVersionForAdminToolsNotMet = 1799611416U,
			// Token: 0x0400068C RID: 1676
			NullLoggerHasBeenPassedIn = 3629514284U,
			// Token: 0x0400068D RID: 1677
			HybridInfoPurePSObjectsNotSupported = 888551196U,
			// Token: 0x0400068E RID: 1678
			LangPackBundleVersioning = 3204362251U,
			// Token: 0x0400068F RID: 1679
			SearchingForAttributes = 3457078824U
		}

		// Token: 0x0200007C RID: 124
		private enum ParamIDs
		{
			// Token: 0x04000691 RID: 1681
			LessThan,
			// Token: 0x04000692 RID: 1682
			OnPremisesOrgRelationshipDomainsCrossWithAcceptedDomainReturnResult,
			// Token: 0x04000693 RID: 1683
			QueryReturnedNull,
			// Token: 0x04000694 RID: 1684
			ShouldNotBeNullOrEmpty,
			// Token: 0x04000695 RID: 1685
			ServicesAreMarkedForDeletion,
			// Token: 0x04000696 RID: 1686
			AssemblyVersion,
			// Token: 0x04000697 RID: 1687
			PrimaryDNSTestFailed,
			// Token: 0x04000698 RID: 1688
			DRMinVersionNotMet,
			// Token: 0x04000699 RID: 1689
			ADUpdateForDomainPrep,
			// Token: 0x0400069A RID: 1690
			ProcessNeedsToBeClosedOnUninstall,
			// Token: 0x0400069B RID: 1691
			InvalidDomainToPrepare,
			// Token: 0x0400069C RID: 1692
			ErrorWhileRunning,
			// Token: 0x0400069D RID: 1693
			RegistryKeyNotFound,
			// Token: 0x0400069E RID: 1694
			ConfigDCHostNameMismatch,
			// Token: 0x0400069F RID: 1695
			LocalTimeZone,
			// Token: 0x040006A0 RID: 1696
			HybridInfoOpeningRunspace,
			// Token: 0x040006A1 RID: 1697
			E15E14CoexistenceMinOSReqFailure,
			// Token: 0x040006A2 RID: 1698
			PrereqAnalysisFailureToAccessResults,
			// Token: 0x040006A3 RID: 1699
			InvalidOrTamperedConfigFile,
			// Token: 0x040006A4 RID: 1700
			MailboxEDBDriveDoesNotExist,
			// Token: 0x040006A5 RID: 1701
			OSVersion,
			// Token: 0x040006A6 RID: 1702
			LonghornIISManagementConsoleInstalledValidator,
			// Token: 0x040006A7 RID: 1703
			WatermarkPresent,
			// Token: 0x040006A8 RID: 1704
			LonghornIISMetabaseNotInstalled,
			// Token: 0x040006A9 RID: 1705
			VoiceMessagesInQueue,
			// Token: 0x040006AA RID: 1706
			CannotRemoveProvisionedServerValidator,
			// Token: 0x040006AB RID: 1707
			VistaNoIPv4,
			// Token: 0x040006AC RID: 1708
			Equal,
			// Token: 0x040006AD RID: 1709
			FileInUse,
			// Token: 0x040006AE RID: 1710
			UserNameError,
			// Token: 0x040006AF RID: 1711
			E15E14CoexistenceMinOSReqFailureInDC,
			// Token: 0x040006B0 RID: 1712
			DomainNameExistsInAcceptedDomainAndOrgRel,
			// Token: 0x040006B1 RID: 1713
			RunningTentantHybridTestWithFile,
			// Token: 0x040006B2 RID: 1714
			HybridInfoCmdletStart,
			// Token: 0x040006B3 RID: 1715
			TraceFunctionEnter,
			// Token: 0x040006B4 RID: 1716
			MailboxLogDriveDoesNotExist,
			// Token: 0x040006B5 RID: 1717
			TestingConfigFile,
			// Token: 0x040006B6 RID: 1718
			InterruptedUninstallNotContinued,
			// Token: 0x040006B7 RID: 1719
			ErrorDNSQueryA,
			// Token: 0x040006B8 RID: 1720
			LonghornNoIPv4,
			// Token: 0x040006B9 RID: 1721
			NotSupportedRecipientPolicyAddressFormatValidator,
			// Token: 0x040006BA RID: 1722
			InconsistentlyConfiguredDomain,
			// Token: 0x040006BB RID: 1723
			PrereqAnalysisSettingStarted,
			// Token: 0x040006BC RID: 1724
			ClientAccessRoleNotPresentInSite,
			// Token: 0x040006BD RID: 1725
			RegistryKeyDoesntExist,
			// Token: 0x040006BE RID: 1726
			BridgeheadRoleNotPresentInSite,
			// Token: 0x040006BF RID: 1727
			NoIPv4Assigned,
			// Token: 0x040006C0 RID: 1728
			QueryStart,
			// Token: 0x040006C1 RID: 1729
			RecipientUpdateServiceNotAvailable,
			// Token: 0x040006C2 RID: 1730
			ValidationFailed,
			// Token: 0x040006C3 RID: 1731
			DelegatedFirstSP1Upgrade,
			// Token: 0x040006C4 RID: 1732
			ErrorTooManyMatchingResults,
			// Token: 0x040006C5 RID: 1733
			InstallViaServerManager,
			// Token: 0x040006C6 RID: 1734
			ServerIsDynamicGroupExpansionServer,
			// Token: 0x040006C7 RID: 1735
			NoIPv4AssignedForAdminTools,
			// Token: 0x040006C8 RID: 1736
			NoPreviousExchangeExistsInTopoWhilePrepareAD,
			// Token: 0x040006C9 RID: 1737
			DuplicateShortProvisionedName,
			// Token: 0x040006CA RID: 1738
			ServerIsLastHubForEdgeSubscription,
			// Token: 0x040006CB RID: 1739
			ServerIsSourceForSendConnector,
			// Token: 0x040006CC RID: 1740
			OrgRelTargetAppUriToSearch,
			// Token: 0x040006CD RID: 1741
			HttpSiteAccessError,
			// Token: 0x040006CE RID: 1742
			StringContains,
			// Token: 0x040006CF RID: 1743
			PrereqAnalysisRuleStarted,
			// Token: 0x040006D0 RID: 1744
			ResultAncestorNotFound,
			// Token: 0x040006D1 RID: 1745
			MessagesInQueue,
			// Token: 0x040006D2 RID: 1746
			ScanningFailed,
			// Token: 0x040006D3 RID: 1747
			NotLocalAdmin,
			// Token: 0x040006D4 RID: 1748
			SGFilesExist,
			// Token: 0x040006D5 RID: 1749
			RootDomainMixedMode,
			// Token: 0x040006D6 RID: 1750
			ServerRemoveProvisioningCheck,
			// Token: 0x040006D7 RID: 1751
			HybridInfoTotalCmdletTime,
			// Token: 0x040006D8 RID: 1752
			DelegatedFirstInstall,
			// Token: 0x040006D9 RID: 1753
			PrereqAnalysisFailedRule,
			// Token: 0x040006DA RID: 1754
			SetupLogInitializeFailure,
			// Token: 0x040006DB RID: 1755
			HybridInfoGetObjectValue,
			// Token: 0x040006DC RID: 1756
			InhBlockPublicFolderTree,
			// Token: 0x040006DD RID: 1757
			ResourcePropertySchemaException,
			// Token: 0x040006DE RID: 1758
			MsfteUpgradeIssue,
			// Token: 0x040006DF RID: 1759
			OnPremisesTestFailedWithException,
			// Token: 0x040006E0 RID: 1760
			ServerIsGroupExpansionServer,
			// Token: 0x040006E1 RID: 1761
			ProcessNeedsToBeClosedOnUpgrade,
			// Token: 0x040006E2 RID: 1762
			ComponentIsRequiredToInstall,
			// Token: 0x040006E3 RID: 1763
			HybridInfoCmdletEnd,
			// Token: 0x040006E4 RID: 1764
			ExchangeAlreadyInstalled,
			// Token: 0x040006E5 RID: 1765
			QueryFinish,
			// Token: 0x040006E6 RID: 1766
			ComponentIsRequired,
			// Token: 0x040006E7 RID: 1767
			PrereqAnalysisStopped,
			// Token: 0x040006E8 RID: 1768
			InvalidLocalComputerFQDN,
			// Token: 0x040006E9 RID: 1769
			ComponentIsRecommended,
			// Token: 0x040006EA RID: 1770
			Exchange2007ServerInstalled,
			// Token: 0x040006EB RID: 1771
			UserName,
			// Token: 0x040006EC RID: 1772
			MissingDNSHostRecord,
			// Token: 0x040006ED RID: 1773
			AdditionalUMLangPackExists,
			// Token: 0x040006EE RID: 1774
			InstallWatermark,
			// Token: 0x040006EF RID: 1775
			TargetPathCompressed,
			// Token: 0x040006F0 RID: 1776
			PrereqAnalysisMemberStopped,
			// Token: 0x040006F1 RID: 1777
			PrepareDomainNotAdmin,
			// Token: 0x040006F2 RID: 1778
			ServerNotInSchemaMasterSite,
			// Token: 0x040006F3 RID: 1779
			ShouldBeNullOrEmpty,
			// Token: 0x040006F4 RID: 1780
			E15E12CoexistenceMinOSReqFailure,
			// Token: 0x040006F5 RID: 1781
			AlreadyInstalledUMLangPacks,
			// Token: 0x040006F6 RID: 1782
			StringNotContains,
			// Token: 0x040006F7 RID: 1783
			NotEqual,
			// Token: 0x040006F8 RID: 1784
			StringNotStartsWith,
			// Token: 0x040006F9 RID: 1785
			ServerNotInSchemaMasterDomain,
			// Token: 0x040006FA RID: 1786
			TraceFunctionExit,
			// Token: 0x040006FB RID: 1787
			ADAMPortAlreadyInUse,
			// Token: 0x040006FC RID: 1788
			AllServersOfHigherVersionFailure,
			// Token: 0x040006FD RID: 1789
			HelpIdNotDefined,
			// Token: 0x040006FE RID: 1790
			StringStartsWith,
			// Token: 0x040006FF RID: 1791
			OffLineABServerDeleted,
			// Token: 0x04000700 RID: 1792
			PrereqAnalysisMemberEvaluated,
			// Token: 0x04000701 RID: 1793
			OSWebEditionValidator,
			// Token: 0x04000702 RID: 1794
			NotLessThan,
			// Token: 0x04000703 RID: 1795
			ADAMSSLPortAlreadyInUse,
			// Token: 0x04000704 RID: 1796
			InvalidRecordType,
			// Token: 0x04000705 RID: 1797
			PrereqAnalysisExpectedFailure,
			// Token: 0x04000706 RID: 1798
			DomainMixedMode,
			// Token: 0x04000707 RID: 1799
			DomainControllerOutOfSiteValidator,
			// Token: 0x04000708 RID: 1800
			PrepareDomainModeMixed,
			// Token: 0x04000709 RID: 1801
			LocalDomainMixedMode,
			// Token: 0x0400070A RID: 1802
			UnwillingToRemoveMailboxDatabase,
			// Token: 0x0400070B RID: 1803
			ADAMDataPathExists
		}
	}
}
