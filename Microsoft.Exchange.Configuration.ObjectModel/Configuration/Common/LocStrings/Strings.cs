using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x0200029A RID: 666
	internal static class Strings
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x00055F3C File Offset: 0x0005413C
		static Strings()
		{
			Strings.stringIDs.Add(3480090246U, "MissingImpersonatedUserSid");
			Strings.stringIDs.Add(3445110312U, "FalseString");
			Strings.stringIDs.Add(2329139938U, "VerboseFailedToGetServiceTopology");
			Strings.stringIDs.Add(831535795U, "NotGrantCustomScriptRole");
			Strings.stringIDs.Add(3609756192U, "ErrorChangeImmutableType");
			Strings.stringIDs.Add(1212809041U, "NotGrantAnyAdminRoles");
			Strings.stringIDs.Add(2716022819U, "ErrorOnlyDNSupportedWithIgnoreDefaultScope");
			Strings.stringIDs.Add(2866126871U, "ExceptionTaskNotInitialized");
			Strings.stringIDs.Add(311479382U, "VerboseLbNoDatabaseFoundInAD");
			Strings.stringIDs.Add(1464829243U, "WorkUnitStatusNotStarted");
			Strings.stringIDs.Add(444666707U, "TrueString");
			Strings.stringIDs.Add(1953440811U, "InvalidPswsDirectInvocationBlocked");
			Strings.stringIDs.Add(2103372448U, "InvalidProperties");
			Strings.stringIDs.Add(3344562832U, "ErrorInstanceObjectConatinsNullIdentity");
			Strings.stringIDs.Add(2895423986U, "ErrorAdminLoginUsingAppPassword");
			Strings.stringIDs.Add(2403641406U, "ErrorCannotFindMailboxToLogonPublicStore");
			Strings.stringIDs.Add(3498243205U, "WorkUnitStatusInProgress");
			Strings.stringIDs.Add(19923723U, "ErrorObjectHasValidationErrors");
			Strings.stringIDs.Add(452500353U, "ErrorNoProvisioningHandlerAvailable");
			Strings.stringIDs.Add(1811096880U, "WarningUnlicensedMailbox");
			Strings.stringIDs.Add(767622998U, "WorkUnitStatusFailed");
			Strings.stringIDs.Add(1306006239U, "VerboseInitializeRunspaceServerSettingsRemote");
			Strings.stringIDs.Add(585695277U, "ExceptionTaskInconsistent");
			Strings.stringIDs.Add(1520502444U, "WarningForceMessage");
			Strings.stringIDs.Add(4140272390U, "WarningCannotSetPrimarySmtpAddressWhenEapEnabled");
			Strings.stringIDs.Add(3204373538U, "ExecutingUserNameIsMissing");
			Strings.stringIDs.Add(2188568225U, "ExceptionNoChangesSpecified");
			Strings.stringIDs.Add(3805201920U, "ExceptionTaskAlreadyInitialized");
			Strings.stringIDs.Add(2951618283U, "ParameterValueTooLarge");
			Strings.stringIDs.Add(3718372071U, "ErrorNotSupportSingletonWildcard");
			Strings.stringIDs.Add(2784860353U, "WorkUnitCollectionConfigurationSummary");
			Strings.stringIDs.Add(143513383U, "ExceptionMDACommandNotExecuting");
			Strings.stringIDs.Add(1958023215U, "ErrorRemotePowershellConnectionBlocked");
			Strings.stringIDs.Add(2725322457U, "LogExecutionFailed");
			Strings.stringIDs.Add(3624978883U, "VerboseLbNoOabVDirReturned");
			Strings.stringIDs.Add(2158269158U, "VerboseLbEnterSiteMailboxEnterprise");
			Strings.stringIDs.Add(2890992798U, "HierarchicalIdentityNullOrEmpty");
			Strings.stringIDs.Add(1268762784U, "ExceptionObjectAlreadyExists");
			Strings.stringIDs.Add(2576106929U, "ErrorRemotePowerShellNotEnabled");
			Strings.stringIDs.Add(298822364U, "ErrorRbacConfigurationNotSupportedSharedConfiguration");
			Strings.stringIDs.Add(1356455742U, "UnknownEnumValue");
			Strings.stringIDs.Add(439815616U, "ErrorOperationRequiresManager");
			Strings.stringIDs.Add(2916171677U, "ErrorOrganizationWildcard");
			Strings.stringIDs.Add(687978330U, "ErrorDelegatedUserNotInOrg");
			Strings.stringIDs.Add(1837325848U, "VerboseSerializationDataNotExist");
			Strings.stringIDs.Add(1983570122U, "ErrorNoAvailablePublicFolderDatabase");
			Strings.stringIDs.Add(1655423524U, "SessionExpiredException");
			Strings.stringIDs.Add(2661346553U, "HierarchicalIdentityStartsOrEndsWithBackslash");
			Strings.stringIDs.Add(2859768776U, "VerboseInitializeRunspaceServerSettingsLocal");
			Strings.stringIDs.Add(1551194332U, "ExceptionMDACommandStillExecuting");
			Strings.stringIDs.Add(1960180119U, "WorkUnitError");
			Strings.stringIDs.Add(4205209694U, "VerboseNoSource");
			Strings.stringIDs.Add(797535012U, "ErrorFilteringOnlyUserLogin");
			Strings.stringIDs.Add(1654901580U, "ExceptionNullInstanceParameter");
			Strings.stringIDs.Add(498095919U, "VerboseLbCreateNewExRpcAdmin");
			Strings.stringIDs.Add(2137526570U, "ErrorCannotDiscoverDefaultOrganizationUnitForRecipient");
			Strings.stringIDs.Add(3713958116U, "CommandSucceeded");
			Strings.stringIDs.Add(84680862U, "ErrorUninitializedParameter");
			Strings.stringIDs.Add(2037166858U, "ErrorNotAllowedForPartnerAccess");
			Strings.stringIDs.Add(1859189834U, "ExceptionTaskNotExecuted");
			Strings.stringIDs.Add(3245318277U, "ErrorInvalidResultSize");
			Strings.stringIDs.Add(201309884U, "VerboseLbDeletedServer");
			Strings.stringIDs.Add(4282198592U, "TaskCompleted");
			Strings.stringIDs.Add(1234513041U, "ErrorMaxTenantPSConnectionLimitNotResolved");
			Strings.stringIDs.Add(1421372901U, "InvalidCharacterInComponentPartOfHierarchicalIdentity");
			Strings.stringIDs.Add(838517570U, "ExceptionReadOnlyPropertyBag");
			Strings.stringIDs.Add(3781767156U, "VerboseLbTryRetrieveDatabaseStatus");
			Strings.stringIDs.Add(3216817101U, "ErrorIgnoreDefaultScopeAndDCSetTogether");
			Strings.stringIDs.Add(3519173187U, "ExceptionGettingConditionObject");
			Strings.stringIDs.Add(4160063394U, "EnabledString");
			Strings.stringIDs.Add(3507110937U, "WorkUnitWarning");
			Strings.stringIDs.Add(1170623981U, "VerboseLbServerDownSoMarkDatabaseDown");
			Strings.stringIDs.Add(3828427927U, "BinaryDataStakeHodler");
			Strings.stringIDs.Add(1221524445U, "ErrorWriteOpOnDehydratedTenant");
			Strings.stringIDs.Add(3394388186U, "ExceptionMDACommandAlreadyExecuting");
			Strings.stringIDs.Add(2174831997U, "SipCultureInfoArgumentCheckFailure");
			Strings.stringIDs.Add(577240232U, "ConsecutiveWholeWildcardNamePartsInHierarchicalIdentity");
			Strings.stringIDs.Add(324189978U, "ErrorMapiPublicFolderTreeNotUnique");
			Strings.stringIDs.Add(3002944702U, "WarningMoreResultsAvailable");
			Strings.stringIDs.Add(3806422804U, "ErrorOperationOnInvalidObject");
			Strings.stringIDs.Add(4253079958U, "VerboseInitializeRunspaceServerSettingsAdam");
			Strings.stringIDs.Add(3746749985U, "VerboseLbNoAvailableDatabase");
			Strings.stringIDs.Add(3026495307U, "PswsManagementAutomationAssemblyLoadError");
			Strings.stringIDs.Add(2439453065U, "LogRollbackFailed");
			Strings.stringIDs.Add(2502725106U, "NullOrEmptyNamePartsInHierarchicalIdentity");
			Strings.stringIDs.Add(328156873U, "ErrorCloseServiceHandle");
			Strings.stringIDs.Add(3026665436U, "WorkUnitStatusCompleted");
			Strings.stringIDs.Add(2267892936U, "ErrorUrlInValid");
			Strings.stringIDs.Add(2717050851U, "ErrorNoMailboxUserInTheForest");
			Strings.stringIDs.Add(4074275529U, "ServerNotAvailable");
			Strings.stringIDs.Add(292337732U, "HelpUrlHeaderText");
			Strings.stringIDs.Add(2496835620U, "VersionMismatchDuringCreateRemoteRunspace");
			Strings.stringIDs.Add(2667845807U, "ErrorCannotOpenServiceControllerManager");
			Strings.stringIDs.Add(2472100876U, "VerboseLbNoEligibleServers");
			Strings.stringIDs.Add(3699804131U, "VerboseLbDatabaseFound");
			Strings.stringIDs.Add(3296071226U, "VerboseADObjectNoChangedProperties");
			Strings.stringIDs.Add(2442074922U, "VerbosePopulateScopeSet");
			Strings.stringIDs.Add(2293662344U, "ErrorCertificateDenied");
			Strings.stringIDs.Add(4083645591U, "ErrorCannotResolvePUIDToWindowsIdentity");
			Strings.stringIDs.Add(2468805291U, "ErrorMissOrganization");
			Strings.stringIDs.Add(1891802266U, "ExceptionMDAConnectionAlreadyOpened");
			Strings.stringIDs.Add(982491582U, "ExceptionObjectStillExists");
			Strings.stringIDs.Add(3844753652U, "ExceptionMDAConnectionMustBeOpened");
			Strings.stringIDs.Add(1602649260U, "WriteErrorMessage");
			Strings.stringIDs.Add(4100583810U, "GenericConditionFailure");
			Strings.stringIDs.Add(4285414215U, "VerboseLbExRpcAdminExists");
			Strings.stringIDs.Add(325596373U, "DisabledString");
			Strings.stringIDs.Add(2781337548U, "ExceptionRemoveNoneExistenceObject");
			Strings.stringIDs.Add(492587358U, "LogErrorPrefix");
			Strings.stringIDs.Add(1558360907U, "ErrorMapiPublicFolderTreeNotFound");
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00056810 File Offset: 0x00054A10
		public static LocalizedString LoadingRole(string account)
		{
			return new LocalizedString("LoadingRole", "ExDEEEDC", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00056840 File Offset: 0x00054A40
		public static LocalizedString LookupUserAsSAMAccount(string user)
		{
			return new LocalizedString("LookupUserAsSAMAccount", "Ex2FA584", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0005686F File Offset: 0x00054A6F
		public static LocalizedString MissingImpersonatedUserSid
		{
			get
			{
				return new LocalizedString("MissingImpersonatedUserSid", "Ex3FE822", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00056890 File Offset: 0x00054A90
		public static LocalizedString VerboseLbOABOwnedByServer(string oab, string server)
		{
			return new LocalizedString("VerboseLbOABOwnedByServer", "ExA33016", false, true, Strings.ResourceManager, new object[]
			{
				oab,
				server
			});
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000568C4 File Offset: 0x00054AC4
		public static LocalizedString VerboseLbNoServerForDatabaseException(string errorMessage)
		{
			return new LocalizedString("VerboseLbNoServerForDatabaseException", "Ex2AD546", false, true, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000568F4 File Offset: 0x00054AF4
		public static LocalizedString WrongTypeMailboxUser(string identity)
		{
			return new LocalizedString("WrongTypeMailboxUser", "ExD00A0F", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00056924 File Offset: 0x00054B24
		public static LocalizedString VerboseRequestFilterInGetTask(string filter)
		{
			return new LocalizedString("VerboseRequestFilterInGetTask", "", false, false, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00056954 File Offset: 0x00054B54
		public static LocalizedString CannotFindClassFactoryInAgentAssembly(string location)
		{
			return new LocalizedString("CannotFindClassFactoryInAgentAssembly", "ExCB2B56", false, true, Strings.ResourceManager, new object[]
			{
				location
			});
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00056984 File Offset: 0x00054B84
		public static LocalizedString ErrorInvalidStatePartnerOrgNotNull(string account)
		{
			return new LocalizedString("ErrorInvalidStatePartnerOrgNotNull", "Ex6D944B", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000569B4 File Offset: 0x00054BB4
		public static LocalizedString LoadingScopeErrorText(string account)
		{
			return new LocalizedString("LoadingScopeErrorText", "Ex88621B", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000569E4 File Offset: 0x00054BE4
		public static LocalizedString VerboseLbOABIsCurrentlyOnServer(string currentServer)
		{
			return new LocalizedString("VerboseLbOABIsCurrentlyOnServer", "ExFF7A07", false, true, Strings.ResourceManager, new object[]
			{
				currentServer
			});
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00056A14 File Offset: 0x00054C14
		public static LocalizedString AgentAssemblyDuplicateFound(string assemblyLocation)
		{
			return new LocalizedString("AgentAssemblyDuplicateFound", "ExAE0974", false, true, Strings.ResourceManager, new object[]
			{
				assemblyLocation
			});
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00056A44 File Offset: 0x00054C44
		public static LocalizedString LogHelpUrl(string helpUrl)
		{
			return new LocalizedString("LogHelpUrl", "", false, false, Strings.ResourceManager, new object[]
			{
				helpUrl
			});
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00056A74 File Offset: 0x00054C74
		public static LocalizedString NoRoleEntriesFound(string exchangeCmdletName)
		{
			return new LocalizedString("NoRoleEntriesFound", "Ex0BA5B8", false, true, Strings.ResourceManager, new object[]
			{
				exchangeCmdletName
			});
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00056AA4 File Offset: 0x00054CA4
		public static LocalizedString VerboseLbPermanentException(string errorMessage)
		{
			return new LocalizedString("VerboseLbPermanentException", "Ex7DABDA", false, true, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00056AD4 File Offset: 0x00054CD4
		public static LocalizedString ErrorIncompleteDCPublicFolderIdParameter(string parameterName)
		{
			return new LocalizedString("ErrorIncompleteDCPublicFolderIdParameter", "ExDA24DC", false, true, Strings.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00056B03 File Offset: 0x00054D03
		public static LocalizedString FalseString
		{
			get
			{
				return new LocalizedString("FalseString", "Ex38B631", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00056B24 File Offset: 0x00054D24
		public static LocalizedString ErrorInvalidServerName(string serverName)
		{
			return new LocalizedString("ErrorInvalidServerName", "ExD434BB", false, true, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00056B54 File Offset: 0x00054D54
		public static LocalizedString VerboseLbNoAvailableE15Database(int count)
		{
			return new LocalizedString("VerboseLbNoAvailableE15Database", "", false, false, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00056B88 File Offset: 0x00054D88
		public static LocalizedString MutuallyExclusiveArguments(string arg1, string arg2)
		{
			return new LocalizedString("MutuallyExclusiveArguments", "Ex190776", false, true, Strings.ResourceManager, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00056BBC File Offset: 0x00054DBC
		public static LocalizedString LogResolverInstantiated(Type taskType, Condition condition)
		{
			return new LocalizedString("LogResolverInstantiated", "Ex234AF0", false, true, Strings.ResourceManager, new object[]
			{
				taskType,
				condition
			});
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00056BF0 File Offset: 0x00054DF0
		public static LocalizedString ProvisionDefaultProperties(int handlerIndex)
		{
			return new LocalizedString("ProvisionDefaultProperties", "", false, false, Strings.ResourceManager, new object[]
			{
				handlerIndex
			});
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00056C24 File Offset: 0x00054E24
		public static LocalizedString VerboseReadADObject(string id, string type)
		{
			return new LocalizedString("VerboseReadADObject", "Ex9FC538", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type
			});
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00056C58 File Offset: 0x00054E58
		public static LocalizedString ResourceLoadDelayNotEnforcedMaxThreadsExceeded(int cappedDelay, bool required, string resource, string load, int threadNum)
		{
			return new LocalizedString("ResourceLoadDelayNotEnforcedMaxThreadsExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				cappedDelay,
				required,
				resource,
				load,
				threadNum
			});
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00056CA8 File Offset: 0x00054EA8
		public static LocalizedString ErrorNotAcceptedDomain(string domain)
		{
			return new LocalizedString("ErrorNotAcceptedDomain", "ExD48BF8", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00056CD8 File Offset: 0x00054ED8
		public static LocalizedString SortOrderFormatException(string input)
		{
			return new LocalizedString("SortOrderFormatException", "ExD6FEB2", false, true, Strings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00056D07 File Offset: 0x00054F07
		public static LocalizedString VerboseFailedToGetServiceTopology
		{
			get
			{
				return new LocalizedString("VerboseFailedToGetServiceTopology", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00056D28 File Offset: 0x00054F28
		public static LocalizedString LoadingScope(string account)
		{
			return new LocalizedString("LoadingScope", "Ex99C3E1", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00056D58 File Offset: 0x00054F58
		public static LocalizedString ExceptionObjectNotFound(string objectType)
		{
			return new LocalizedString("ExceptionObjectNotFound", "Ex71B980", false, true, Strings.ResourceManager, new object[]
			{
				objectType
			});
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00056D88 File Offset: 0x00054F88
		public static LocalizedString WrongTypeMailboxOrMailUser(string identity)
		{
			return new LocalizedString("WrongTypeMailboxOrMailUser", "ExA506BE", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00056DB8 File Offset: 0x00054FB8
		public static LocalizedString ErrorPolicyUserOrSecurityGroupNotFound(string id)
		{
			return new LocalizedString("ErrorPolicyUserOrSecurityGroupNotFound", "Ex976174", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00056DE8 File Offset: 0x00054FE8
		public static LocalizedString ExceptionSetupRegkeyMissing(string keyPath, string valueName)
		{
			return new LocalizedString("ExceptionSetupRegkeyMissing", "ExDA8280", false, true, Strings.ResourceManager, new object[]
			{
				keyPath,
				valueName
			});
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00056E1C File Offset: 0x0005501C
		public static LocalizedString PswsDeserializationError(string errorMessage)
		{
			return new LocalizedString("PswsDeserializationError", "", false, false, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00056E4C File Offset: 0x0005504C
		public static LocalizedString OnbehalfOf(string logonIdentity, string accessIdentity)
		{
			return new LocalizedString("OnbehalfOf", "ExEB9E55", false, true, Strings.ResourceManager, new object[]
			{
				logonIdentity,
				accessIdentity
			});
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00056E7F File Offset: 0x0005507F
		public static LocalizedString NotGrantCustomScriptRole
		{
			get
			{
				return new LocalizedString("NotGrantCustomScriptRole", "ExB022F4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00056EA0 File Offset: 0x000550A0
		public static LocalizedString ErrorIncompletePublicFolderIdParameter(string parameterName)
		{
			return new LocalizedString("ErrorIncompletePublicFolderIdParameter", "", false, false, Strings.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00056ED0 File Offset: 0x000550D0
		public static LocalizedString VerboseFailedToReadFromDC(string id, string dc)
		{
			return new LocalizedString("VerboseFailedToReadFromDC", "Ex9559FB", false, true, Strings.ResourceManager, new object[]
			{
				id,
				dc
			});
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00056F04 File Offset: 0x00055104
		public static LocalizedString ErrorNotMailboxServer(string server)
		{
			return new LocalizedString("ErrorNotMailboxServer", "Ex383749", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00056F34 File Offset: 0x00055134
		public static LocalizedString ExceptionNoConversion(Type oldType, Type newType)
		{
			return new LocalizedString("ExceptionNoConversion", "Ex48AF25", false, true, Strings.ResourceManager, new object[]
			{
				oldType,
				newType
			});
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00056F67 File Offset: 0x00055167
		public static LocalizedString ErrorChangeImmutableType
		{
			get
			{
				return new LocalizedString("ErrorChangeImmutableType", "Ex8959CE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00056F88 File Offset: 0x00055188
		public static LocalizedString PropertyProvisioned(int i, string propertyName, string propertyValue)
		{
			return new LocalizedString("PropertyProvisioned", "Ex81D4C3", false, true, Strings.ResourceManager, new object[]
			{
				i,
				propertyName,
				propertyValue
			});
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00056FC4 File Offset: 0x000551C4
		public static LocalizedString VerboseTaskReadDataObject(string id, string type)
		{
			return new LocalizedString("VerboseTaskReadDataObject", "Ex398E60", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type
			});
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00056FF7 File Offset: 0x000551F7
		public static LocalizedString NotGrantAnyAdminRoles
		{
			get
			{
				return new LocalizedString("NotGrantAnyAdminRoles", "Ex57FC72", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00057018 File Offset: 0x00055218
		public static LocalizedString UnExpectedElement(string expectedElement, string actualElement)
		{
			return new LocalizedString("UnExpectedElement", "ExC11811", false, true, Strings.ResourceManager, new object[]
			{
				expectedElement,
				actualElement
			});
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0005704C File Offset: 0x0005524C
		public static LocalizedString ExceptionLegacyObjects(string identities)
		{
			return new LocalizedString("ExceptionLegacyObjects", "ExC72065", false, true, Strings.ResourceManager, new object[]
			{
				identities
			});
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0005707C File Offset: 0x0005527C
		public static LocalizedString ExceptionObjectAmbiguous(string objectType)
		{
			return new LocalizedString("ExceptionObjectAmbiguous", "ExE26139", false, true, Strings.ResourceManager, new object[]
			{
				objectType
			});
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000570AC File Offset: 0x000552AC
		public static LocalizedString VerboseLbInitialProvisioningDatabaseExcluded(string databaseName)
		{
			return new LocalizedString("VerboseLbInitialProvisioningDatabaseExcluded", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000570DB File Offset: 0x000552DB
		public static LocalizedString ErrorOnlyDNSupportedWithIgnoreDefaultScope
		{
			get
			{
				return new LocalizedString("ErrorOnlyDNSupportedWithIgnoreDefaultScope", "Ex922448", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000570F9 File Offset: 0x000552F9
		public static LocalizedString ExceptionTaskNotInitialized
		{
			get
			{
				return new LocalizedString("ExceptionTaskNotInitialized", "Ex215352", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00057118 File Offset: 0x00055318
		public static LocalizedString ErrorMaxTenantPSConnectionLimit(string orgName)
		{
			return new LocalizedString("ErrorMaxTenantPSConnectionLimit", "Ex94B605", false, true, Strings.ResourceManager, new object[]
			{
				orgName
			});
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00057147 File Offset: 0x00055347
		public static LocalizedString VerboseLbNoDatabaseFoundInAD
		{
			get
			{
				return new LocalizedString("VerboseLbNoDatabaseFoundInAD", "ExEFC91E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00057168 File Offset: 0x00055368
		public static LocalizedString ProvisioningPreInternalProcessRecord(int handlerIndex, bool objectChangedFlag)
		{
			return new LocalizedString("ProvisioningPreInternalProcessRecord", "Ex8CC553", false, true, Strings.ResourceManager, new object[]
			{
				handlerIndex,
				objectChangedFlag
			});
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000571A8 File Offset: 0x000553A8
		public static LocalizedString VerboseLbDatabase(string databaseDn)
		{
			return new LocalizedString("VerboseLbDatabase", "Ex47BB9E", false, true, Strings.ResourceManager, new object[]
			{
				databaseDn
			});
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x000571D7 File Offset: 0x000553D7
		public static LocalizedString WorkUnitStatusNotStarted
		{
			get
			{
				return new LocalizedString("WorkUnitStatusNotStarted", "Ex70C849", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000571F8 File Offset: 0x000553F8
		public static LocalizedString VerboseSourceFromDC(string source)
		{
			return new LocalizedString("VerboseSourceFromDC", "ExA2D61C", false, true, Strings.ResourceManager, new object[]
			{
				source
			});
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00057227 File Offset: 0x00055427
		public static LocalizedString TrueString
		{
			get
			{
				return new LocalizedString("TrueString", "ExE4D1B7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00057248 File Offset: 0x00055448
		public static LocalizedString VerboseLbIsDatabaseLocal(string databaseName, string databaseSite, string localSite)
		{
			return new LocalizedString("VerboseLbIsDatabaseLocal", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName,
				databaseSite,
				localSite
			});
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0005727F File Offset: 0x0005547F
		public static LocalizedString InvalidPswsDirectInvocationBlocked
		{
			get
			{
				return new LocalizedString("InvalidPswsDirectInvocationBlocked", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000572A0 File Offset: 0x000554A0
		public static LocalizedString ErrorOrgOutOfPartnerScope(string identity, string org)
		{
			return new LocalizedString("ErrorOrgOutOfPartnerScope", "Ex892E1E", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				org
			});
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000572D4 File Offset: 0x000554D4
		public static LocalizedString ServiceAlreadyNotInstalled(string servicename)
		{
			return new LocalizedString("ServiceAlreadyNotInstalled", "Ex38C2AA", false, true, Strings.ResourceManager, new object[]
			{
				servicename
			});
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00057304 File Offset: 0x00055504
		public static LocalizedString WarningCmdletTarpittingByResourceLoad(string resourceKey, string delaySeconds)
		{
			return new LocalizedString("WarningCmdletTarpittingByResourceLoad", "", false, false, Strings.ResourceManager, new object[]
			{
				resourceKey,
				delaySeconds
			});
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00057338 File Offset: 0x00055538
		public static LocalizedString ErrorManagementObjectNotFoundByType(string type)
		{
			return new LocalizedString("ErrorManagementObjectNotFoundByType", "ExB4C70B", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00057368 File Offset: 0x00055568
		public static LocalizedString VerboseLbSitesAreNotBalanced(string localsite, int numberofdbs)
		{
			return new LocalizedString("VerboseLbSitesAreNotBalanced", "Ex1D6A4F", false, true, Strings.ResourceManager, new object[]
			{
				localsite,
				numberofdbs
			});
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000573A0 File Offset: 0x000555A0
		public static LocalizedString ErrorUserNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorUserNotFound", "Ex9BC9DD", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000573D0 File Offset: 0x000555D0
		public static LocalizedString PswsResponseChildElementNotExisingError(string parentElement, string name)
		{
			return new LocalizedString("PswsResponseChildElementNotExisingError", "", false, false, Strings.ResourceManager, new object[]
			{
				parentElement,
				name
			});
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00057404 File Offset: 0x00055604
		public static LocalizedString VerboseLbReturningServer(string server)
		{
			return new LocalizedString("VerboseLbReturningServer", "Ex286002", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00057434 File Offset: 0x00055634
		public static LocalizedString ErrorConfigurationUnitNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorConfigurationUnitNotFound", "ExB12F6E", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00057463 File Offset: 0x00055663
		public static LocalizedString InvalidProperties
		{
			get
			{
				return new LocalizedString("InvalidProperties", "Ex24B8E3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00057484 File Offset: 0x00055684
		public static LocalizedString PswsRequestException(string errorMessage)
		{
			return new LocalizedString("PswsRequestException", "", false, false, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000574B4 File Offset: 0x000556B4
		public static LocalizedString ErrorAddressListNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorAddressListNotFound", "Ex54304E", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000574E4 File Offset: 0x000556E4
		public static LocalizedString ErrorNoPartnerScopes(string identity)
		{
			return new LocalizedString("ErrorNoPartnerScopes", "Ex5F2E70", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00057514 File Offset: 0x00055714
		public static LocalizedString VerboseSkipObject(string id)
		{
			return new LocalizedString("VerboseSkipObject", "ExA55BB6", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00057544 File Offset: 0x00055744
		public static LocalizedString ErrorPolicyUserOrSecurityGroupNotUnique(string id)
		{
			return new LocalizedString("ErrorPolicyUserOrSecurityGroupNotUnique", "ExF91DF1", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00057574 File Offset: 0x00055774
		public static LocalizedString ErrorParentHasNewerVersion(string identity, string objectVersion, string parentVersion)
		{
			return new LocalizedString("ErrorParentHasNewerVersion", "ExA41B19", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				objectVersion,
				parentVersion
			});
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000575AC File Offset: 0x000557AC
		public static LocalizedString VerboseAdminSessionSettingsUserConfigDC(string configDC)
		{
			return new LocalizedString("VerboseAdminSessionSettingsUserConfigDC", "Ex6B05BB", false, true, Strings.ResourceManager, new object[]
			{
				configDC
			});
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000575DC File Offset: 0x000557DC
		public static LocalizedString LogConditionFailed(Type conditionType, bool expectedResult)
		{
			return new LocalizedString("LogConditionFailed", "ExCB9B43", false, true, Strings.ResourceManager, new object[]
			{
				conditionType,
				expectedResult
			});
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00057614 File Offset: 0x00055814
		public static LocalizedString LogFunctionExit(Type type, string methodName)
		{
			return new LocalizedString("LogFunctionExit", "Ex689125", false, true, Strings.ResourceManager, new object[]
			{
				type,
				methodName
			});
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00057648 File Offset: 0x00055848
		public static LocalizedString WrongTypeMailboxRecipient(string identity)
		{
			return new LocalizedString("WrongTypeMailboxRecipient", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00057678 File Offset: 0x00055878
		public static LocalizedString WrongTypeUser(string identity)
		{
			return new LocalizedString("WrongTypeUser", "Ex01488F", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000576A8 File Offset: 0x000558A8
		public static LocalizedString WillUninstallInstalledService(string name)
		{
			return new LocalizedString("WillUninstallInstalledService", "ExDADBB1", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000576D8 File Offset: 0x000558D8
		public static LocalizedString ExceptionMissingDetailSchemaFile(string masterSchemaFileName, string className)
		{
			return new LocalizedString("ExceptionMissingDetailSchemaFile", "ExC43184", false, true, Strings.ResourceManager, new object[]
			{
				masterSchemaFileName,
				className
			});
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0005770B File Offset: 0x0005590B
		public static LocalizedString ErrorInstanceObjectConatinsNullIdentity
		{
			get
			{
				return new LocalizedString("ErrorInstanceObjectConatinsNullIdentity", "ExDAF7E8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0005772C File Offset: 0x0005592C
		public static LocalizedString MonitoringPerfomanceCounterString(string perfObject, string counter, string instance, double value)
		{
			return new LocalizedString("MonitoringPerfomanceCounterString", "Ex293136", false, true, Strings.ResourceManager, new object[]
			{
				perfObject,
				counter,
				instance,
				value
			});
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0005776C File Offset: 0x0005596C
		public static LocalizedString VerboseAdminSessionSettingsUserDCs(string DCs)
		{
			return new LocalizedString("VerboseAdminSessionSettingsUserDCs", "Ex9D7640", false, true, Strings.ResourceManager, new object[]
			{
				DCs
			});
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0005779C File Offset: 0x0005599C
		public static LocalizedString WarningDefaultResultSizeReached(string resultSize)
		{
			return new LocalizedString("WarningDefaultResultSizeReached", "Ex2E94E0", false, true, Strings.ResourceManager, new object[]
			{
				resultSize
			});
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000577CB File Offset: 0x000559CB
		public static LocalizedString ErrorAdminLoginUsingAppPassword
		{
			get
			{
				return new LocalizedString("ErrorAdminLoginUsingAppPassword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000577E9 File Offset: 0x000559E9
		public static LocalizedString ErrorCannotFindMailboxToLogonPublicStore
		{
			get
			{
				return new LocalizedString("ErrorCannotFindMailboxToLogonPublicStore", "Ex6E586E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00057808 File Offset: 0x00055A08
		public static LocalizedString UnknownAuditManagerType(string auditorType)
		{
			return new LocalizedString("UnknownAuditManagerType", "ExD0AACE", false, true, Strings.ResourceManager, new object[]
			{
				auditorType
			});
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00057838 File Offset: 0x00055A38
		public static LocalizedString ExceptionRoleNotFoundObjects(string identities)
		{
			return new LocalizedString("ExceptionRoleNotFoundObjects", "Ex144F4E", false, true, Strings.ResourceManager, new object[]
			{
				identities
			});
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00057868 File Offset: 0x00055A68
		public static LocalizedString LogTaskCandidate(Type taskType)
		{
			return new LocalizedString("LogTaskCandidate", "ExA3168F", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00057897 File Offset: 0x00055A97
		public static LocalizedString WorkUnitStatusInProgress
		{
			get
			{
				return new LocalizedString("WorkUnitStatusInProgress", "Ex7DA896", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000578B8 File Offset: 0x00055AB8
		public static LocalizedString VerboseTaskProcessingObject(string id)
		{
			return new LocalizedString("VerboseTaskProcessingObject", "ExC96184", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000578E7 File Offset: 0x00055AE7
		public static LocalizedString ErrorObjectHasValidationErrors
		{
			get
			{
				return new LocalizedString("ErrorObjectHasValidationErrors", "ExABE500", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00057905 File Offset: 0x00055B05
		public static LocalizedString ErrorNoProvisioningHandlerAvailable
		{
			get
			{
				return new LocalizedString("ErrorNoProvisioningHandlerAvailable", "Ex4FB266", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00057924 File Offset: 0x00055B24
		public static LocalizedString VerboseRereadADObject(string id, string type, string root)
		{
			return new LocalizedString("VerboseRereadADObject", "Ex0EC40A", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type,
				root
			});
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005795C File Offset: 0x00055B5C
		public static LocalizedString InvocationExceptionDescriptionWithoutError(string commandText)
		{
			return new LocalizedString("InvocationExceptionDescriptionWithoutError", "Ex8C7580", false, true, Strings.ResourceManager, new object[]
			{
				commandText
			});
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005798C File Offset: 0x00055B8C
		public static LocalizedString CouldntFindClassFactoryInAssembly(string classFactoryName, string assembly)
		{
			return new LocalizedString("CouldntFindClassFactoryInAssembly", "Ex0BEA57", false, true, Strings.ResourceManager, new object[]
			{
				classFactoryName,
				assembly
			});
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x000579BF File Offset: 0x00055BBF
		public static LocalizedString WarningUnlicensedMailbox
		{
			get
			{
				return new LocalizedString("WarningUnlicensedMailbox", "Ex185717", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000579E0 File Offset: 0x00055BE0
		public static LocalizedString VerboseAdminSessionSettingsDefaultScope(string defaultScope)
		{
			return new LocalizedString("VerboseAdminSessionSettingsDefaultScope", "Ex0535F9", false, true, Strings.ResourceManager, new object[]
			{
				defaultScope
			});
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00057A10 File Offset: 0x00055C10
		public static LocalizedString WarningCannotWriteToEventLog(string reason)
		{
			return new LocalizedString("WarningCannotWriteToEventLog", "ExF96315", false, true, Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00057A40 File Offset: 0x00055C40
		public static LocalizedString InvalidAttribute(string attribute)
		{
			return new LocalizedString("InvalidAttribute", "Ex248A23", false, true, Strings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00057A70 File Offset: 0x00055C70
		public static LocalizedString ExceptionMismatchedConfigObjectType(Type configuredType, Type usedType)
		{
			return new LocalizedString("ExceptionMismatchedConfigObjectType", "Ex112866", false, true, Strings.ResourceManager, new object[]
			{
				configuredType,
				usedType
			});
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00057AA4 File Offset: 0x00055CA4
		public static LocalizedString VerboseSource(string source)
		{
			return new LocalizedString("VerboseSource", "Ex6BDE38", false, true, Strings.ResourceManager, new object[]
			{
				source
			});
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00057AD3 File Offset: 0x00055CD3
		public static LocalizedString WorkUnitStatusFailed
		{
			get
			{
				return new LocalizedString("WorkUnitStatusFailed", "Ex83221A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00057AF1 File Offset: 0x00055CF1
		public static LocalizedString VerboseInitializeRunspaceServerSettingsRemote
		{
			get
			{
				return new LocalizedString("VerboseInitializeRunspaceServerSettingsRemote", "ExF7B42B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00057B0F File Offset: 0x00055D0F
		public static LocalizedString ExceptionTaskInconsistent
		{
			get
			{
				return new LocalizedString("ExceptionTaskInconsistent", "Ex179672", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00057B2D File Offset: 0x00055D2D
		public static LocalizedString WarningForceMessage
		{
			get
			{
				return new LocalizedString("WarningForceMessage", "Ex2838CC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00057B4C File Offset: 0x00055D4C
		public static LocalizedString VerboseLbRetryableException(string errorMessage)
		{
			return new LocalizedString("VerboseLbRetryableException", "ExF7804A", false, true, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00057B7C File Offset: 0x00055D7C
		public static LocalizedString ErrorPartnerApplicationWithoutLinkedAccount(string pa)
		{
			return new LocalizedString("ErrorPartnerApplicationWithoutLinkedAccount", "", false, false, Strings.ResourceManager, new object[]
			{
				pa
			});
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00057BAC File Offset: 0x00055DAC
		public static LocalizedString ErrorNoAvailablePublicFolderDatabaseInDatacenter(string organizationName)
		{
			return new LocalizedString("ErrorNoAvailablePublicFolderDatabaseInDatacenter", "ExAC8771", false, true, Strings.ResourceManager, new object[]
			{
				organizationName
			});
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00057BDC File Offset: 0x00055DDC
		public static LocalizedString NoRequiredRole(string identity)
		{
			return new LocalizedString("NoRequiredRole", "Ex02B210", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00057C0B File Offset: 0x00055E0B
		public static LocalizedString WarningCannotSetPrimarySmtpAddressWhenEapEnabled
		{
			get
			{
				return new LocalizedString("WarningCannotSetPrimarySmtpAddressWhenEapEnabled", "Ex58C382", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00057C2C File Offset: 0x00055E2C
		public static LocalizedString ErrorOrganizationNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationNotUnique", "ExD2094F", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00057C5B File Offset: 0x00055E5B
		public static LocalizedString ExecutingUserNameIsMissing
		{
			get
			{
				return new LocalizedString("ExecutingUserNameIsMissing", "ExC429BF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00057C7C File Offset: 0x00055E7C
		public static LocalizedString WrongTypeUserContactComputer(string identity)
		{
			return new LocalizedString("WrongTypeUserContactComputer", "ExF099FD", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00057CAC File Offset: 0x00055EAC
		public static LocalizedString ErrorOperationTarpitting(int delaySeconds)
		{
			return new LocalizedString("ErrorOperationTarpitting", "", false, false, Strings.ResourceManager, new object[]
			{
				delaySeconds
			});
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00057CE0 File Offset: 0x00055EE0
		public static LocalizedString ConfigObjectAmbiguous(string identity, Type classType)
		{
			return new LocalizedString("ConfigObjectAmbiguous", "Ex3C06AB", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				classType
			});
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00057D14 File Offset: 0x00055F14
		public static LocalizedString VerboseLbDatabaseIsNotOnline(int status)
		{
			return new LocalizedString("VerboseLbDatabaseIsNotOnline", "Ex9B0FE9", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00057D48 File Offset: 0x00055F48
		public static LocalizedString InvalidNegativeValue(string name)
		{
			return new LocalizedString("InvalidNegativeValue", "Ex7DC091", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00057D78 File Offset: 0x00055F78
		public static LocalizedString ErrorNotUserMailboxCanLogonPFDatabase(string pfdb)
		{
			return new LocalizedString("ErrorNotUserMailboxCanLogonPFDatabase", "ExC4308A", false, true, Strings.ResourceManager, new object[]
			{
				pfdb
			});
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00057DA7 File Offset: 0x00055FA7
		public static LocalizedString ExceptionNoChangesSpecified
		{
			get
			{
				return new LocalizedString("ExceptionNoChangesSpecified", "Ex960200", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00057DC8 File Offset: 0x00055FC8
		public static LocalizedString VerboseLbRemoteSiteDatabaseReturned(string database, string serverName)
		{
			return new LocalizedString("VerboseLbRemoteSiteDatabaseReturned", "", false, false, Strings.ResourceManager, new object[]
			{
				database,
				serverName
			});
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00057DFC File Offset: 0x00055FFC
		public static LocalizedString RBACContextParserException(int lineNumber, int position, string reason)
		{
			return new LocalizedString("RBACContextParserException", "Ex25E296", false, true, Strings.ResourceManager, new object[]
			{
				lineNumber,
				position,
				reason
			});
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00057E40 File Offset: 0x00056040
		public static LocalizedString ErrorCannotOpenService(string name)
		{
			return new LocalizedString("ErrorCannotOpenService", "Ex9D56F7", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00057E70 File Offset: 0x00056070
		public static LocalizedString ExArgumentNullException(string paramName)
		{
			return new LocalizedString("ExArgumentNullException", "Ex56EB59", false, true, Strings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00057E9F File Offset: 0x0005609F
		public static LocalizedString ExceptionTaskAlreadyInitialized
		{
			get
			{
				return new LocalizedString("ExceptionTaskAlreadyInitialized", "Ex9224EA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00057EC0 File Offset: 0x000560C0
		public static LocalizedString WarningWindowsEmailAddressTooLong(string recipient)
		{
			return new LocalizedString("WarningWindowsEmailAddressTooLong", "Ex4EE4B5", false, true, Strings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00057EF0 File Offset: 0x000560F0
		public static LocalizedString PswsResponseElementNotExisingError(string documentXml, string xPath)
		{
			return new LocalizedString("PswsResponseElementNotExisingError", "", false, false, Strings.ResourceManager, new object[]
			{
				documentXml,
				xPath
			});
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00057F24 File Offset: 0x00056124
		public static LocalizedString NoRoleEntriesWithParametersFound(string exchangeCmdletName)
		{
			return new LocalizedString("NoRoleEntriesWithParametersFound", "Ex5E8EA4", false, true, Strings.ResourceManager, new object[]
			{
				exchangeCmdletName
			});
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00057F54 File Offset: 0x00056154
		public static LocalizedString VerbosePostConditions(string conditions)
		{
			return new LocalizedString("VerbosePostConditions", "Ex53712E", false, true, Strings.ResourceManager, new object[]
			{
				conditions
			});
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00057F84 File Offset: 0x00056184
		public static LocalizedString ErrorProvisioningValidation(string description, string agentName)
		{
			return new LocalizedString("ErrorProvisioningValidation", "ExE71684", false, true, Strings.ResourceManager, new object[]
			{
				description,
				agentName
			});
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00057FB7 File Offset: 0x000561B7
		public static LocalizedString ParameterValueTooLarge
		{
			get
			{
				return new LocalizedString("ParameterValueTooLarge", "Ex786531", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00057FD8 File Offset: 0x000561D8
		public static LocalizedString UnhandledErrorMessage(string error)
		{
			return new LocalizedString("UnhandledErrorMessage", "Ex3BBD8E", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00058007 File Offset: 0x00056207
		public static LocalizedString ErrorNotSupportSingletonWildcard
		{
			get
			{
				return new LocalizedString("ErrorNotSupportSingletonWildcard", "Ex047A07", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00058028 File Offset: 0x00056228
		public static LocalizedString WrongTypeMailContact(string identity)
		{
			return new LocalizedString("WrongTypeMailContact", "Ex72A78A", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00058058 File Offset: 0x00056258
		public static LocalizedString PiiRedactionInitializationFailed(string reason)
		{
			return new LocalizedString("PiiRedactionInitializationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00058088 File Offset: 0x00056288
		public static LocalizedString VerboseAdminSessionSettingsViewForest(string viewForest)
		{
			return new LocalizedString("VerboseAdminSessionSettingsViewForest", "Ex8656DA", false, true, Strings.ResourceManager, new object[]
			{
				viewForest
			});
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000580B8 File Offset: 0x000562B8
		public static LocalizedString VerboseRemovingRoleAssignment(string id)
		{
			return new LocalizedString("VerboseRemovingRoleAssignment", "Ex88CF02", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000580E8 File Offset: 0x000562E8
		public static LocalizedString CommandNotFoundError(string cmdlet)
		{
			return new LocalizedString("CommandNotFoundError", "Ex61D0B7", false, true, Strings.ResourceManager, new object[]
			{
				cmdlet
			});
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00058118 File Offset: 0x00056318
		public static LocalizedString WrongTypeMailPublicFolder(string identity)
		{
			return new LocalizedString("WrongTypeMailPublicFolder", "Ex150E3C", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00058147 File Offset: 0x00056347
		public static LocalizedString WorkUnitCollectionConfigurationSummary
		{
			get
			{
				return new LocalizedString("WorkUnitCollectionConfigurationSummary", "ExFCCA37", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00058165 File Offset: 0x00056365
		public static LocalizedString ExceptionMDACommandNotExecuting
		{
			get
			{
				return new LocalizedString("ExceptionMDACommandNotExecuting", "ExCAC498", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00058184 File Offset: 0x00056384
		public static LocalizedString OverallElapsedTimeDescription(int h, int mm, int ss)
		{
			return new LocalizedString("OverallElapsedTimeDescription", "Ex26F2ED", false, true, Strings.ResourceManager, new object[]
			{
				h,
				mm,
				ss
			});
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000581CC File Offset: 0x000563CC
		public static LocalizedString ExceptionMDACommandExcutionError(int innerErrorCode, string command)
		{
			return new LocalizedString("ExceptionMDACommandExcutionError", "Ex699F1A", false, true, Strings.ResourceManager, new object[]
			{
				innerErrorCode,
				command
			});
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00058204 File Offset: 0x00056404
		public static LocalizedString ErrorRecipientPropertyValueAlreadybeUsed(string property, string value)
		{
			return new LocalizedString("ErrorRecipientPropertyValueAlreadybeUsed", "", false, false, Strings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00058238 File Offset: 0x00056438
		public static LocalizedString ErrorManagementObjectNotFound(string id)
		{
			return new LocalizedString("ErrorManagementObjectNotFound", "Ex43C0AC", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00058268 File Offset: 0x00056468
		public static LocalizedString ErrorInvalidMailboxStoreObjectIdentity(string input)
		{
			return new LocalizedString("ErrorInvalidMailboxStoreObjectIdentity", "ExE2F600", false, true, Strings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00058298 File Offset: 0x00056498
		public static LocalizedString ErrorCmdletProxy(string command, string serverFqn, string serverVersion, string proxyMethod, string errorMessage)
		{
			return new LocalizedString("ErrorCmdletProxy", "", false, false, Strings.ResourceManager, new object[]
			{
				command,
				serverFqn,
				serverVersion,
				proxyMethod,
				errorMessage
			});
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000582D8 File Offset: 0x000564D8
		public static LocalizedString WarningCouldNotRemoveRoleAssignment(string id, string error)
		{
			return new LocalizedString("WarningCouldNotRemoveRoleAssignment", "Ex7E7F41", false, true, Strings.ResourceManager, new object[]
			{
				id,
				error
			});
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0005830C File Offset: 0x0005650C
		public static LocalizedString VerboseDeleteObject(string id, string type)
		{
			return new LocalizedString("VerboseDeleteObject", "Ex65B31E", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type
			});
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00058340 File Offset: 0x00056540
		public static LocalizedString VerboseCmdletProxiedToAnotherServer(string cmdlet, string server, string serverVersion, string proxyMethod)
		{
			return new LocalizedString("VerboseCmdletProxiedToAnotherServer", "", false, false, Strings.ResourceManager, new object[]
			{
				cmdlet,
				server,
				serverVersion,
				proxyMethod
			});
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005837C File Offset: 0x0005657C
		public static LocalizedString ExceptionInvalidDatabaseLegacyDnFormat(string legacyDn)
		{
			return new LocalizedString("ExceptionInvalidDatabaseLegacyDnFormat", "ExAAE39F", false, true, Strings.ResourceManager, new object[]
			{
				legacyDn
			});
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000583AC File Offset: 0x000565AC
		public static LocalizedString LogFunctionEnter(Type type, string methodName, string argumentList)
		{
			return new LocalizedString("LogFunctionEnter", "Ex37DFC4", false, true, Strings.ResourceManager, new object[]
			{
				type,
				methodName,
				argumentList
			});
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000583E4 File Offset: 0x000565E4
		public static LocalizedString VerboseAdminSessionSettingsUserGlobalCatalog(string globalCatalog)
		{
			return new LocalizedString("VerboseAdminSessionSettingsUserGlobalCatalog", "ExED5741", false, true, Strings.ResourceManager, new object[]
			{
				globalCatalog
			});
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00058414 File Offset: 0x00056614
		public static LocalizedString CouldNotDeterimineServiceInstanceException(string domainName)
		{
			return new LocalizedString("CouldNotDeterimineServiceInstanceException", "Ex3EA890", false, true, Strings.ResourceManager, new object[]
			{
				domainName
			});
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00058444 File Offset: 0x00056644
		public static LocalizedString ErrorInvalidParameterFormat(string parameterName)
		{
			return new LocalizedString("ErrorInvalidParameterFormat", "ExAB5EC3", false, true, Strings.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00058474 File Offset: 0x00056674
		public static LocalizedString VerboseAdminSessionSettingsUserAFGlobalCatalog(string globalCatalog)
		{
			return new LocalizedString("VerboseAdminSessionSettingsUserAFGlobalCatalog", "", false, false, Strings.ResourceManager, new object[]
			{
				globalCatalog
			});
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000584A4 File Offset: 0x000566A4
		public static LocalizedString WrongTypeSecurityPrincipal(string identity)
		{
			return new LocalizedString("WrongTypeSecurityPrincipal", "ExD4DC3B", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000584D3 File Offset: 0x000566D3
		public static LocalizedString ErrorRemotePowershellConnectionBlocked
		{
			get
			{
				return new LocalizedString("ErrorRemotePowershellConnectionBlocked", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x000584F1 File Offset: 0x000566F1
		public static LocalizedString LogExecutionFailed
		{
			get
			{
				return new LocalizedString("LogExecutionFailed", "ExCD1BDD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00058510 File Offset: 0x00056710
		public static LocalizedString ErrorInvalidGlobalAddressListIdentity(string idValue)
		{
			return new LocalizedString("ErrorInvalidGlobalAddressListIdentity", "ExCACD99", false, true, Strings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0005853F File Offset: 0x0005673F
		public static LocalizedString VerboseLbNoOabVDirReturned
		{
			get
			{
				return new LocalizedString("VerboseLbNoOabVDirReturned", "Ex5B5C0A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00058560 File Offset: 0x00056760
		public static LocalizedString WarningTaskRetried(string exception)
		{
			return new LocalizedString("WarningTaskRetried", "", false, false, Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0005858F File Offset: 0x0005678F
		public static LocalizedString VerboseLbEnterSiteMailboxEnterprise
		{
			get
			{
				return new LocalizedString("VerboseLbEnterSiteMailboxEnterprise", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000585B0 File Offset: 0x000567B0
		public static LocalizedString AssemblyFileNotFound(string fileName)
		{
			return new LocalizedString("AssemblyFileNotFound", "Ex015D0B", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000585DF File Offset: 0x000567DF
		public static LocalizedString HierarchicalIdentityNullOrEmpty
		{
			get
			{
				return new LocalizedString("HierarchicalIdentityNullOrEmpty", "Ex9A9CC7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00058600 File Offset: 0x00056800
		public static LocalizedString VerboseLbDatabaseContainer(string dbContainerDn)
		{
			return new LocalizedString("VerboseLbDatabaseContainer", "ExFF1AF9", false, true, Strings.ResourceManager, new object[]
			{
				dbContainerDn
			});
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0005862F File Offset: 0x0005682F
		public static LocalizedString ExceptionObjectAlreadyExists
		{
			get
			{
				return new LocalizedString("ExceptionObjectAlreadyExists", "ExC69E3F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00058650 File Offset: 0x00056850
		public static LocalizedString ErrorProxyAddressAlreadyExists(string address, string user)
		{
			return new LocalizedString("ErrorProxyAddressAlreadyExists", "Ex1472D1", false, true, Strings.ResourceManager, new object[]
			{
				address,
				user
			});
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00058684 File Offset: 0x00056884
		public static LocalizedString VerboseCannotGetRemoteServiceUriForUser(string id, string proxyAddress, string reason)
		{
			return new LocalizedString("VerboseCannotGetRemoteServiceUriForUser", "", false, false, Strings.ResourceManager, new object[]
			{
				id,
				proxyAddress,
				reason
			});
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000586BC File Offset: 0x000568BC
		public static LocalizedString VerboseAdminSessionSettingsUserAFConfigDC(string configDC)
		{
			return new LocalizedString("VerboseAdminSessionSettingsUserAFConfigDC", "", false, false, Strings.ResourceManager, new object[]
			{
				configDC
			});
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000586EB File Offset: 0x000568EB
		public static LocalizedString ErrorRemotePowerShellNotEnabled
		{
			get
			{
				return new LocalizedString("ErrorRemotePowerShellNotEnabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005870C File Offset: 0x0005690C
		public static LocalizedString VerboseLbCountOfOABRecordsOwnedByServer(string server, int count)
		{
			return new LocalizedString("VerboseLbCountOfOABRecordsOwnedByServer", "Ex5AE5F8", false, true, Strings.ResourceManager, new object[]
			{
				server,
				count
			});
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00058744 File Offset: 0x00056944
		public static LocalizedString PswsSerializationError(string errorMessage)
		{
			return new LocalizedString("PswsSerializationError", "", false, false, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00058774 File Offset: 0x00056974
		public static LocalizedString CouldNotStartService(string servicename)
		{
			return new LocalizedString("CouldNotStartService", "Ex87696D", false, true, Strings.ResourceManager, new object[]
			{
				servicename
			});
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000587A4 File Offset: 0x000569A4
		public static LocalizedString VerboseTaskGetDataObjects(string id, string type, string root)
		{
			return new LocalizedString("VerboseTaskGetDataObjects", "ExB38F79", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type,
				root
			});
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000587DC File Offset: 0x000569DC
		public static LocalizedString ErrorPublicFolderMailDisabled(string identity)
		{
			return new LocalizedString("ErrorPublicFolderMailDisabled", "Ex2BC3FC", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005880C File Offset: 0x00056A0C
		public static LocalizedString VerboseLbIsLocalSiteNotEnoughInformation(string database, string databaseSite, string localSite)
		{
			return new LocalizedString("VerboseLbIsLocalSiteNotEnoughInformation", "ExE42B4F", false, true, Strings.ResourceManager, new object[]
			{
				database,
				databaseSite,
				localSite
			});
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00058844 File Offset: 0x00056A44
		public static LocalizedString ErrorRecipientNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorRecipientNotFound", "Ex914C70", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00058874 File Offset: 0x00056A74
		public static LocalizedString VerboseAdminSessionSettingsRecipientViewRoot(string recipientViewRoot)
		{
			return new LocalizedString("VerboseAdminSessionSettingsRecipientViewRoot", "ExBD2683", false, true, Strings.ResourceManager, new object[]
			{
				recipientViewRoot
			});
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000588A4 File Offset: 0x00056AA4
		public static LocalizedString VerboseLbFoundOabVDir(string vdirDn, int count)
		{
			return new LocalizedString("VerboseLbFoundOabVDir", "Ex8C45CE", false, true, Strings.ResourceManager, new object[]
			{
				vdirDn,
				count
			});
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000588DC File Offset: 0x00056ADC
		public static LocalizedString ErrorManagementObjectNotFoundWithSource(string id, string source)
		{
			return new LocalizedString("ErrorManagementObjectNotFoundWithSource", "Ex6F9304", false, true, Strings.ResourceManager, new object[]
			{
				id,
				source
			});
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00058910 File Offset: 0x00056B10
		public static LocalizedString MicroDelayNotEnforcedMaxThreadsExceeded(int cappedDelay, bool required, int threadNum)
		{
			return new LocalizedString("MicroDelayNotEnforcedMaxThreadsExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				cappedDelay,
				required,
				threadNum
			});
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00058958 File Offset: 0x00056B58
		public static LocalizedString ServiceStopFailure(string name, string msg)
		{
			return new LocalizedString("ServiceStopFailure", "Ex28E845", false, true, Strings.ResourceManager, new object[]
			{
				name,
				msg
			});
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005898B File Offset: 0x00056B8B
		public static LocalizedString ErrorRbacConfigurationNotSupportedSharedConfiguration
		{
			get
			{
				return new LocalizedString("ErrorRbacConfigurationNotSupportedSharedConfiguration", "Ex041D05", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000589AC File Offset: 0x00056BAC
		public static LocalizedString ErrorRecipientPropertyValueAlreadyExists(string property, string value, string existingRecipientId)
		{
			return new LocalizedString("ErrorRecipientPropertyValueAlreadyExists", "Ex362523", false, true, Strings.ResourceManager, new object[]
			{
				property,
				value,
				existingRecipientId
			});
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x000589E3 File Offset: 0x00056BE3
		public static LocalizedString UnknownEnumValue
		{
			get
			{
				return new LocalizedString("UnknownEnumValue", "Ex813DAD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00058A04 File Offset: 0x00056C04
		public static LocalizedString VerboseTaskFindDataObjectsInAL(string type, string addressList)
		{
			return new LocalizedString("VerboseTaskFindDataObjectsInAL", "Ex4E9AE8", false, true, Strings.ResourceManager, new object[]
			{
				type,
				addressList
			});
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00058A38 File Offset: 0x00056C38
		public static LocalizedString MultipleDefaultMailboxPlansFound(string id, string dc)
		{
			return new LocalizedString("MultipleDefaultMailboxPlansFound", "Ex6CCBC5", false, true, Strings.ResourceManager, new object[]
			{
				id,
				dc
			});
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00058A6C File Offset: 0x00056C6C
		public static LocalizedString ElementMustNotHaveAttributes(string element)
		{
			return new LocalizedString("ElementMustNotHaveAttributes", "ExB007D2", false, true, Strings.ResourceManager, new object[]
			{
				element
			});
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00058A9B File Offset: 0x00056C9B
		public static LocalizedString ErrorOperationRequiresManager
		{
			get
			{
				return new LocalizedString("ErrorOperationRequiresManager", "ExB6FAB5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00058ABC File Offset: 0x00056CBC
		public static LocalizedString WarningTaskModuleSkipped(string methodName, string exception)
		{
			return new LocalizedString("WarningTaskModuleSkipped", "", false, false, Strings.ResourceManager, new object[]
			{
				methodName,
				exception
			});
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00058AF0 File Offset: 0x00056CF0
		public static LocalizedString ErrorIsOutofDatabaseScope(string id, string exceptionDetails)
		{
			return new LocalizedString("ErrorIsOutofDatabaseScope", "ExBD293F", false, true, Strings.ResourceManager, new object[]
			{
				id,
				exceptionDetails
			});
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00058B24 File Offset: 0x00056D24
		public static LocalizedString ExceptionInvalidTaskType(Type taskType)
		{
			return new LocalizedString("ExceptionInvalidTaskType", "Ex190A7A", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00058B54 File Offset: 0x00056D54
		public static LocalizedString WorkUnitCollectionStatus(int totalCount, int completedCount, int failedCount)
		{
			return new LocalizedString("WorkUnitCollectionStatus", "Ex23E40F", false, true, Strings.ResourceManager, new object[]
			{
				totalCount,
				completedCount,
				failedCount
			});
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00058B9A File Offset: 0x00056D9A
		public static LocalizedString ErrorOrganizationWildcard
		{
			get
			{
				return new LocalizedString("ErrorOrganizationWildcard", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00058BB8 File Offset: 0x00056DB8
		public static LocalizedString ErrorAddressListNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorAddressListNotUnique", "ExA6530D", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00058BE8 File Offset: 0x00056DE8
		public static LocalizedString VerboseTaskParameterLoggingFailed(string param, Exception e)
		{
			return new LocalizedString("VerboseTaskParameterLoggingFailed", "Ex6F5716", false, true, Strings.ResourceManager, new object[]
			{
				param,
				e
			});
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00058C1C File Offset: 0x00056E1C
		public static LocalizedString ErrorConversionFailed(string name)
		{
			return new LocalizedString("ErrorConversionFailed", "Ex9E1222", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00058C4C File Offset: 0x00056E4C
		public static LocalizedString ErrorRecipientNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorRecipientNotUnique", "Ex55FBCD", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00058C7C File Offset: 0x00056E7C
		public static LocalizedString ExceptionInvalidConfigObjectType(Type type)
		{
			return new LocalizedString("ExceptionInvalidConfigObjectType", "Ex90DF2B", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00058CAC File Offset: 0x00056EAC
		public static LocalizedString LogAutoResolving(Type taskType)
		{
			return new LocalizedString("LogAutoResolving", "Ex5E25A9", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00058CDB File Offset: 0x00056EDB
		public static LocalizedString ErrorDelegatedUserNotInOrg
		{
			get
			{
				return new LocalizedString("ErrorDelegatedUserNotInOrg", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00058CF9 File Offset: 0x00056EF9
		public static LocalizedString VerboseSerializationDataNotExist
		{
			get
			{
				return new LocalizedString("VerboseSerializationDataNotExist", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00058D18 File Offset: 0x00056F18
		public static LocalizedString VerboseInternalQueryFilterInGetTasks(string filter)
		{
			return new LocalizedString("VerboseInternalQueryFilterInGetTasks", "", false, false, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00058D48 File Offset: 0x00056F48
		public static LocalizedString PswsCmdletError(string errorMessage)
		{
			return new LocalizedString("PswsCmdletError", "", false, false, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00058D78 File Offset: 0x00056F78
		public static LocalizedString LogCheckpoint(object checkPoint)
		{
			return new LocalizedString("LogCheckpoint", "Ex359016", false, true, Strings.ResourceManager, new object[]
			{
				checkPoint
			});
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00058DA8 File Offset: 0x00056FA8
		public static LocalizedString ErrorOrganizationNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationNotFound", "Ex48C448", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00058DD8 File Offset: 0x00056FD8
		public static LocalizedString ErrorGlobalAddressListNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorGlobalAddressListNotFound", "Ex8DD552", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00058E08 File Offset: 0x00057008
		public static LocalizedString DependentArguments(string arg1, string arg2)
		{
			return new LocalizedString("DependentArguments", "Ex70684E", false, true, Strings.ResourceManager, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00058E3C File Offset: 0x0005703C
		public static LocalizedString ErrorParentNotFound(string identity, string parent)
		{
			return new LocalizedString("ErrorParentNotFound", "Ex574388", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				parent
			});
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00058E6F File Offset: 0x0005706F
		public static LocalizedString ErrorNoAvailablePublicFolderDatabase
		{
			get
			{
				return new LocalizedString("ErrorNoAvailablePublicFolderDatabase", "Ex7469AC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00058E90 File Offset: 0x00057090
		public static LocalizedString ErrorNotFoundWithReason(string notFound, string reason)
		{
			return new LocalizedString("ErrorNotFoundWithReason", "Ex04C125", false, true, Strings.ResourceManager, new object[]
			{
				notFound,
				reason
			});
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00058EC4 File Offset: 0x000570C4
		public static LocalizedString WrongTypeUserContactGroupIdParameter(string identity)
		{
			return new LocalizedString("WrongTypeUserContactGroupIdParameter", "ExC8FAF9", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00058EF3 File Offset: 0x000570F3
		public static LocalizedString SessionExpiredException
		{
			get
			{
				return new LocalizedString("SessionExpiredException", "Ex96CE50", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00058F14 File Offset: 0x00057114
		public static LocalizedString VerboseAdminSessionSettingsGlobalCatalog(string globalCatalog)
		{
			return new LocalizedString("VerboseAdminSessionSettingsGlobalCatalog", "Ex6BC584", false, true, Strings.ResourceManager, new object[]
			{
				globalCatalog
			});
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00058F44 File Offset: 0x00057144
		public static LocalizedString ExceptionParameterRange(string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionParameterRange", "Ex9F8581", false, true, Strings.ResourceManager, new object[]
			{
				invalidQuery,
				position
			});
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00058F7C File Offset: 0x0005717C
		public static LocalizedString ErrorUnsupportedValues(string unsupported, string allowed)
		{
			return new LocalizedString("ErrorUnsupportedValues", "Ex63FE45", false, true, Strings.ResourceManager, new object[]
			{
				unsupported,
				allowed
			});
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00058FAF File Offset: 0x000571AF
		public static LocalizedString HierarchicalIdentityStartsOrEndsWithBackslash
		{
			get
			{
				return new LocalizedString("HierarchicalIdentityStartsOrEndsWithBackslash", "Ex733D8B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00058FD0 File Offset: 0x000571D0
		public static LocalizedString LogServiceState(ServiceControllerStatus status)
		{
			return new LocalizedString("LogServiceState", "ExFD2C6C", false, true, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00059004 File Offset: 0x00057204
		public static LocalizedString ExceptionMDAInvalidConnectionString(string connectionString)
		{
			return new LocalizedString("ExceptionMDAInvalidConnectionString", "ExD98FF9", false, true, Strings.ResourceManager, new object[]
			{
				connectionString
			});
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00059034 File Offset: 0x00057234
		public static LocalizedString ErrorParentNotFoundOnDomainController(string identity, string domainController, string parent, string domain)
		{
			return new LocalizedString("ErrorParentNotFoundOnDomainController", "Ex80484B", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				domainController,
				parent,
				domain
			});
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00059070 File Offset: 0x00057270
		public static LocalizedString ClassFactoryDoesNotImplementIProvisioningAgent(string classFactoryName, string assembly)
		{
			return new LocalizedString("ClassFactoryDoesNotImplementIProvisioningAgent", "Ex1631A5", false, true, Strings.ResourceManager, new object[]
			{
				classFactoryName,
				assembly
			});
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000590A3 File Offset: 0x000572A3
		public static LocalizedString VerboseInitializeRunspaceServerSettingsLocal
		{
			get
			{
				return new LocalizedString("VerboseInitializeRunspaceServerSettingsLocal", "Ex323EEB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000590C4 File Offset: 0x000572C4
		public static LocalizedString ErrorInvalidOrganizationalUnitDNFormat(string input)
		{
			return new LocalizedString("ErrorInvalidOrganizationalUnitDNFormat", "ExF12D53", false, true, Strings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000590F4 File Offset: 0x000572F4
		public static LocalizedString VerboseDatabaseNotFound(string databaseId, string message)
		{
			return new LocalizedString("VerboseDatabaseNotFound", "Ex068D33", false, true, Strings.ResourceManager, new object[]
			{
				databaseId,
				message
			});
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00059128 File Offset: 0x00057328
		public static LocalizedString VerboseRecipientTaskHelperGetOrgnization(string ou)
		{
			return new LocalizedString("VerboseRecipientTaskHelperGetOrgnization", "ExA76E2B", false, true, Strings.ResourceManager, new object[]
			{
				ou
			});
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00059158 File Offset: 0x00057358
		public static LocalizedString NoRoleEntriesWithParameterFound(string exchangeCmdletName, string parameterName)
		{
			return new LocalizedString("NoRoleEntriesWithParameterFound", "ExB880D8", false, true, Strings.ResourceManager, new object[]
			{
				exchangeCmdletName,
				parameterName
			});
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005918C File Offset: 0x0005738C
		public static LocalizedString ErrorEmptyParameter(string parameterName)
		{
			return new LocalizedString("ErrorEmptyParameter", "ExA08212", false, true, Strings.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x000591BB File Offset: 0x000573BB
		public static LocalizedString ExceptionMDACommandStillExecuting
		{
			get
			{
				return new LocalizedString("ExceptionMDACommandStillExecuting", "Ex0AF4A8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000591DC File Offset: 0x000573DC
		public static LocalizedString ErrorManagementObjectAmbiguous(string id)
		{
			return new LocalizedString("ErrorManagementObjectAmbiguous", "Ex9E65A2", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0005920C File Offset: 0x0005740C
		public static LocalizedString MicroDelayInfo(int actualDelayed, bool enforced, int cappedDelay, bool required, string additionalInfo)
		{
			return new LocalizedString("MicroDelayInfo", "", false, false, Strings.ResourceManager, new object[]
			{
				actualDelayed,
				enforced,
				cappedDelay,
				required,
				additionalInfo
			});
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00059260 File Offset: 0x00057460
		public static LocalizedString WorkUnitError
		{
			get
			{
				return new LocalizedString("WorkUnitError", "Ex9FC98D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00059280 File Offset: 0x00057480
		public static LocalizedString WrongTypeRemoteMailbox(string identity)
		{
			return new LocalizedString("WrongTypeRemoteMailbox", "ExA34B5E", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000592B0 File Offset: 0x000574B0
		public static LocalizedString WrongTypeRoleGroup(string identity)
		{
			return new LocalizedString("WrongTypeRoleGroup", "Ex9E8A2B", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000592E0 File Offset: 0x000574E0
		public static LocalizedString LogServiceName(string serviceName)
		{
			return new LocalizedString("LogServiceName", "Ex74EF2A", false, true, Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00059310 File Offset: 0x00057510
		public static LocalizedString ErrorGlobalAddressListNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorGlobalAddressListNotUnique", "Ex3C3297", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00059340 File Offset: 0x00057540
		public static LocalizedString ErrorNoServerForPublicFolderDatabase(string databaseGuid)
		{
			return new LocalizedString("ErrorNoServerForPublicFolderDatabase", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00059370 File Offset: 0x00057570
		public static LocalizedString VerboseADObjectChangedProperties(string propertyList)
		{
			return new LocalizedString("VerboseADObjectChangedProperties", "Ex2C227C", false, true, Strings.ResourceManager, new object[]
			{
				propertyList
			});
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000593A0 File Offset: 0x000575A0
		public static LocalizedString PswsResponseIsnotXMLError(string response)
		{
			return new LocalizedString("PswsResponseIsnotXMLError", "", false, false, Strings.ResourceManager, new object[]
			{
				response
			});
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000593D0 File Offset: 0x000575D0
		public static LocalizedString CheckIfUserIsASID(string user)
		{
			return new LocalizedString("CheckIfUserIsASID", "Ex5DC055", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00059400 File Offset: 0x00057600
		public static LocalizedString ExchangeSetupCannotResumeLog(string logFile)
		{
			return new LocalizedString("ExchangeSetupCannotResumeLog", "Ex434130", false, true, Strings.ResourceManager, new object[]
			{
				logFile
			});
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0005942F File Offset: 0x0005762F
		public static LocalizedString VerboseNoSource
		{
			get
			{
				return new LocalizedString("VerboseNoSource", "ExAD3D60", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00059450 File Offset: 0x00057650
		public static LocalizedString VerboseAdminSessionSettingsAFConfigDC(string configDC)
		{
			return new LocalizedString("VerboseAdminSessionSettingsAFConfigDC", "", false, false, Strings.ResourceManager, new object[]
			{
				configDC
			});
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00059480 File Offset: 0x00057680
		public static LocalizedString VerboseTaskBeginProcessing(string name)
		{
			return new LocalizedString("VerboseTaskBeginProcessing", "Ex65EA52", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000594AF File Offset: 0x000576AF
		public static LocalizedString ErrorFilteringOnlyUserLogin
		{
			get
			{
				return new LocalizedString("ErrorFilteringOnlyUserLogin", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000594D0 File Offset: 0x000576D0
		public static LocalizedString CouldNotStopService(string servicename)
		{
			return new LocalizedString("CouldNotStopService", "Ex1BCA4F", false, true, Strings.ResourceManager, new object[]
			{
				servicename
			});
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000594FF File Offset: 0x000576FF
		public static LocalizedString ExceptionNullInstanceParameter
		{
			get
			{
				return new LocalizedString("ExceptionNullInstanceParameter", "Ex1B1F8D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00059520 File Offset: 0x00057720
		public static LocalizedString LoadingLogonUserErrorText(string account)
		{
			return new LocalizedString("LoadingLogonUserErrorText", "ExF2B709", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00059550 File Offset: 0x00057750
		public static LocalizedString CrossForestAccount(string identity)
		{
			return new LocalizedString("CrossForestAccount", "ExF55A9F", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005957F File Offset: 0x0005777F
		public static LocalizedString VerboseLbCreateNewExRpcAdmin
		{
			get
			{
				return new LocalizedString("VerboseLbCreateNewExRpcAdmin", "Ex7BF6A5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000595A0 File Offset: 0x000577A0
		public static LocalizedString WrongTypeDistributionGroup(string identity)
		{
			return new LocalizedString("WrongTypeDistributionGroup", "Ex856B6B", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000595D0 File Offset: 0x000577D0
		public static LocalizedString WrongTypeMailUser(string identity)
		{
			return new LocalizedString("WrongTypeMailUser", "ExC76758", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00059600 File Offset: 0x00057800
		public static LocalizedString ErrorRecipientNotInSameOrgWithDataObject(string doId, string doOrg, string rpId, string rpOrg)
		{
			return new LocalizedString("ErrorRecipientNotInSameOrgWithDataObject", "Ex1B4E93", false, true, Strings.ResourceManager, new object[]
			{
				doId,
				doOrg,
				rpId,
				rpOrg
			});
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005963C File Offset: 0x0005783C
		public static LocalizedString ErrorMustWriteToRidMaster(string dc)
		{
			return new LocalizedString("ErrorMustWriteToRidMaster", "", false, false, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005966C File Offset: 0x0005786C
		public static LocalizedString ServiceUninstallFailure(string servicename, string error)
		{
			return new LocalizedString("ServiceUninstallFailure", "ExA6A247", false, true, Strings.ResourceManager, new object[]
			{
				servicename,
				error
			});
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000596A0 File Offset: 0x000578A0
		public static LocalizedString ExceptionResolverConstructorMissing(Type taskType)
		{
			return new LocalizedString("ExceptionResolverConstructorMissing", "Ex3AC6F4", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000596CF File Offset: 0x000578CF
		public static LocalizedString ErrorCannotDiscoverDefaultOrganizationUnitForRecipient
		{
			get
			{
				return new LocalizedString("ErrorCannotDiscoverDefaultOrganizationUnitForRecipient", "Ex2685E5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000596F0 File Offset: 0x000578F0
		public static LocalizedString VerboseAdminSessionSettingsAFGlobalCatalog(string globalCatalog)
		{
			return new LocalizedString("VerboseAdminSessionSettingsAFGlobalCatalog", "", false, false, Strings.ResourceManager, new object[]
			{
				globalCatalog
			});
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00059720 File Offset: 0x00057920
		public static LocalizedString CannotResolveParentOrganization(string ou)
		{
			return new LocalizedString("CannotResolveParentOrganization", "Ex91333F", false, true, Strings.ResourceManager, new object[]
			{
				ou
			});
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00059750 File Offset: 0x00057950
		public static LocalizedString WrongTypeNonMailEnabledUser(string identity)
		{
			return new LocalizedString("WrongTypeNonMailEnabledUser", "Ex470030", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00059780 File Offset: 0x00057980
		public static LocalizedString WarningDuplicateOrganizationSpecified(string organizationOrg, string identityOrg)
		{
			return new LocalizedString("WarningDuplicateOrganizationSpecified", "Ex79DD00", false, true, Strings.ResourceManager, new object[]
			{
				organizationOrg,
				identityOrg
			});
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000597B4 File Offset: 0x000579B4
		public static LocalizedString ResourceLoadDelayInfo(int actualDelayed, bool enforced, int cappedDelay, bool required, string resource, string load, string additionalInfo)
		{
			return new LocalizedString("ResourceLoadDelayInfo", "", false, false, Strings.ResourceManager, new object[]
			{
				actualDelayed,
				enforced,
				cappedDelay,
				required,
				resource,
				load,
				additionalInfo
			});
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00059814 File Offset: 0x00057A14
		public static LocalizedString VerboseLbBestServerSoFar(string serverDn, int num)
		{
			return new LocalizedString("VerboseLbBestServerSoFar", "Ex1ABE89", false, true, Strings.ResourceManager, new object[]
			{
				serverDn,
				num
			});
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005984C File Offset: 0x00057A4C
		public static LocalizedString ErrorIsOutOfDatabaseScopeNoServerCheck(string id, string exceptionDetails)
		{
			return new LocalizedString("ErrorIsOutOfDatabaseScopeNoServerCheck", "Ex9B0142", false, true, Strings.ResourceManager, new object[]
			{
				id,
				exceptionDetails
			});
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00059880 File Offset: 0x00057A80
		public static LocalizedString ErrorIsAcceptedDomain(string domain)
		{
			return new LocalizedString("ErrorIsAcceptedDomain", "Ex4A146E", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000598B0 File Offset: 0x00057AB0
		public static LocalizedString ValidationRuleNotFound(string ruleName)
		{
			return new LocalizedString("ValidationRuleNotFound", "Ex8D937E", false, true, Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000598E0 File Offset: 0x00057AE0
		public static LocalizedString ErrorInvalidUMHuntGroupIdentity(string idValue)
		{
			return new LocalizedString("ErrorInvalidUMHuntGroupIdentity", "ExE7C655", false, true, Strings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00059910 File Offset: 0x00057B10
		public static LocalizedString ErrorNoServersForDatabase(string id)
		{
			return new LocalizedString("ErrorNoServersForDatabase", "Ex3A075D", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0005993F File Offset: 0x00057B3F
		public static LocalizedString CommandSucceeded
		{
			get
			{
				return new LocalizedString("CommandSucceeded", "Ex963939", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00059960 File Offset: 0x00057B60
		public static LocalizedString ProvisioningValidationErrors(int handlerIndex, string errors)
		{
			return new LocalizedString("ProvisioningValidationErrors", "Ex18E25A", false, true, Strings.ResourceManager, new object[]
			{
				handlerIndex,
				errors
			});
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00059998 File Offset: 0x00057B98
		public static LocalizedString ResubmitRequestDoesNotExist(long requestId)
		{
			return new LocalizedString("ResubmitRequestDoesNotExist", "", false, false, Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000599CC File Offset: 0x00057BCC
		public static LocalizedString UserQuotaDelayInfo(int actualDelayed, bool enforced, int cappedDelay, bool required, string part, string additionalInfo)
		{
			return new LocalizedString("UserQuotaDelayInfo", "", false, false, Strings.ResourceManager, new object[]
			{
				actualDelayed,
				enforced,
				cappedDelay,
				required,
				part,
				additionalInfo
			});
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00059A28 File Offset: 0x00057C28
		public static LocalizedString ErrorTaskWin32ExceptionVerbose(string error, string verbose)
		{
			return new LocalizedString("ErrorTaskWin32ExceptionVerbose", "Ex56C93E", false, true, Strings.ResourceManager, new object[]
			{
				error,
				verbose
			});
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00059A5B File Offset: 0x00057C5B
		public static LocalizedString ErrorUninitializedParameter
		{
			get
			{
				return new LocalizedString("ErrorUninitializedParameter", "Ex82F788", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00059A7C File Offset: 0x00057C7C
		public static LocalizedString ErrorSetServiceObjectSecurity(string name)
		{
			return new LocalizedString("ErrorSetServiceObjectSecurity", "Ex7AFF77", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00059AAC File Offset: 0x00057CAC
		public static LocalizedString ErrorQueryServiceObjectSecurity(string name)
		{
			return new LocalizedString("ErrorQueryServiceObjectSecurity", "Ex6F168C", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00059ADB File Offset: 0x00057CDB
		public static LocalizedString ErrorNotAllowedForPartnerAccess
		{
			get
			{
				return new LocalizedString("ErrorNotAllowedForPartnerAccess", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00059AFC File Offset: 0x00057CFC
		public static LocalizedString ErrorNoAvailablePublicFolderDatabaseOnServer(string server)
		{
			return new LocalizedString("ErrorNoAvailablePublicFolderDatabaseOnServer", "Ex1C5F52", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00059B2C File Offset: 0x00057D2C
		public static LocalizedString LogConditionMatchingTypeMismacth(Type conditionType1, Type conditionType2)
		{
			return new LocalizedString("LogConditionMatchingTypeMismacth", "ExC4FFC5", false, true, Strings.ResourceManager, new object[]
			{
				conditionType1,
				conditionType2
			});
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00059B5F File Offset: 0x00057D5F
		public static LocalizedString ExceptionTaskNotExecuted
		{
			get
			{
				return new LocalizedString("ExceptionTaskNotExecuted", "ExE6D900", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00059B80 File Offset: 0x00057D80
		public static LocalizedString ConfigObjectNotFound(string identity, Type classType)
		{
			return new LocalizedString("ConfigObjectNotFound", "Ex37E8E1", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				classType
			});
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00059BB4 File Offset: 0x00057DB4
		public static LocalizedString ExceptionLexError(string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionLexError", "Ex5432C4", false, true, Strings.ResourceManager, new object[]
			{
				invalidQuery,
				position
			});
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00059BEC File Offset: 0x00057DEC
		public static LocalizedString VerboseADObjectNoChangedPropertiesWithId(string identity)
		{
			return new LocalizedString("VerboseADObjectNoChangedPropertiesWithId", "ExFE1249", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00059C1C File Offset: 0x00057E1C
		public static LocalizedString VerboseLbCheckingDatabaseIsAllowedOnScope(string databaseDn)
		{
			return new LocalizedString("VerboseLbCheckingDatabaseIsAllowedOnScope", "Ex84B091", false, true, Strings.ResourceManager, new object[]
			{
				databaseDn
			});
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00059C4C File Offset: 0x00057E4C
		public static LocalizedString InvalidElementValue(string value, string element)
		{
			return new LocalizedString("InvalidElementValue", "Ex655322", false, true, Strings.ResourceManager, new object[]
			{
				value,
				element
			});
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00059C80 File Offset: 0x00057E80
		public static LocalizedString ErrorRoleAssignmentNotFound(string str)
		{
			return new LocalizedString("ErrorRoleAssignmentNotFound", "Ex6FF57D", false, true, Strings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00059CAF File Offset: 0x00057EAF
		public static LocalizedString ErrorInvalidResultSize
		{
			get
			{
				return new LocalizedString("ErrorInvalidResultSize", "Ex6FEEE0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00059CCD File Offset: 0x00057ECD
		public static LocalizedString VerboseLbDeletedServer
		{
			get
			{
				return new LocalizedString("VerboseLbDeletedServer", "Ex58EC74", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00059CEC File Offset: 0x00057EEC
		public static LocalizedString WrongTypeMailboxPlan(string identity)
		{
			return new LocalizedString("WrongTypeMailboxPlan", "Ex4AD36D", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00059D1B File Offset: 0x00057F1B
		public static LocalizedString TaskCompleted
		{
			get
			{
				return new LocalizedString("TaskCompleted", "ExDA9525", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00059D3C File Offset: 0x00057F3C
		public static LocalizedString WarningCmdletTarpittingByUserQuota(string policy, string delaySeconds, string computerName)
		{
			return new LocalizedString("WarningCmdletTarpittingByUserQuota", "", false, false, Strings.ResourceManager, new object[]
			{
				policy,
				delaySeconds,
				computerName
			});
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00059D73 File Offset: 0x00057F73
		public static LocalizedString ErrorMaxTenantPSConnectionLimitNotResolved
		{
			get
			{
				return new LocalizedString("ErrorMaxTenantPSConnectionLimitNotResolved", "Ex703EDE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00059D94 File Offset: 0x00057F94
		public static LocalizedString InvalidRBACContextType(string configType)
		{
			return new LocalizedString("InvalidRBACContextType", "Ex62E035", false, true, Strings.ResourceManager, new object[]
			{
				configType
			});
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00059DC3 File Offset: 0x00057FC3
		public static LocalizedString InvalidCharacterInComponentPartOfHierarchicalIdentity
		{
			get
			{
				return new LocalizedString("InvalidCharacterInComponentPartOfHierarchicalIdentity", "Ex2F63E0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00059DE4 File Offset: 0x00057FE4
		public static LocalizedString VerboseLbOabVDirSelected(string vdirDn)
		{
			return new LocalizedString("VerboseLbOabVDirSelected", "ExE4D73B", false, true, Strings.ResourceManager, new object[]
			{
				vdirDn
			});
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00059E14 File Offset: 0x00058014
		public static LocalizedString MonitoringEventStringWithInstanceName(string eventSource, int eventId, string eventType, string eventMessage, string eventInstanceName)
		{
			return new LocalizedString("MonitoringEventStringWithInstanceName", "Ex4D0114", false, true, Strings.ResourceManager, new object[]
			{
				eventSource,
				eventId,
				eventType,
				eventMessage,
				eventInstanceName
			});
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00059E5C File Offset: 0x0005805C
		public static LocalizedString NoRoleAssignmentsFound(string identity)
		{
			return new LocalizedString("NoRoleAssignmentsFound", "Ex34303A", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00059E8C File Offset: 0x0005808C
		public static LocalizedString VerboseSaveChange(string id, string type, string state)
		{
			return new LocalizedString("VerboseSaveChange", "ExCF5282", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type,
				state
			});
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00059EC4 File Offset: 0x000580C4
		public static LocalizedString WrongActiveSyncDeviceIdParameter(string identity)
		{
			return new LocalizedString("WrongActiveSyncDeviceIdParameter", "Ex0FBD0C", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00059EF3 File Offset: 0x000580F3
		public static LocalizedString ExceptionReadOnlyPropertyBag
		{
			get
			{
				return new LocalizedString("ExceptionReadOnlyPropertyBag", "Ex4ED62A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00059F11 File Offset: 0x00058111
		public static LocalizedString VerboseLbTryRetrieveDatabaseStatus
		{
			get
			{
				return new LocalizedString("VerboseLbTryRetrieveDatabaseStatus", "ExDDF00D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00059F30 File Offset: 0x00058130
		public static LocalizedString ErrorDuplicateManagementObjectFound(IIdentityParameter id1, IIdentityParameter id2, object entry)
		{
			return new LocalizedString("ErrorDuplicateManagementObjectFound", "Ex199CCD", false, true, Strings.ResourceManager, new object[]
			{
				id1,
				id2,
				entry
			});
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00059F68 File Offset: 0x00058168
		public static LocalizedString VerboseLbDatabaseNotFoundException(string errorMessage)
		{
			return new LocalizedString("VerboseLbDatabaseNotFoundException", "Ex329BD6", false, true, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00059F97 File Offset: 0x00058197
		public static LocalizedString ErrorIgnoreDefaultScopeAndDCSetTogether
		{
			get
			{
				return new LocalizedString("ErrorIgnoreDefaultScopeAndDCSetTogether", "Ex86A9F7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00059FB5 File Offset: 0x000581B5
		public static LocalizedString ExceptionGettingConditionObject
		{
			get
			{
				return new LocalizedString("ExceptionGettingConditionObject", "Ex467FFB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00059FD4 File Offset: 0x000581D4
		public static LocalizedString VerboseLbGetServerForActiveDatabaseCopy(string name)
		{
			return new LocalizedString("VerboseLbGetServerForActiveDatabaseCopy", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0005A004 File Offset: 0x00058204
		public static LocalizedString ErrorOpenKeyDeniedForWrite(string keyPath)
		{
			return new LocalizedString("ErrorOpenKeyDeniedForWrite", "ExC1627A", false, true, Strings.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0005A034 File Offset: 0x00058234
		public static LocalizedString WarningCmdletMicroDelay(string delayMSecs)
		{
			return new LocalizedString("WarningCmdletMicroDelay", "", false, false, Strings.ResourceManager, new object[]
			{
				delayMSecs
			});
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0005A064 File Offset: 0x00058264
		public static LocalizedString ErrorManagementObjectNotFoundWithSourceByType(string type, string source)
		{
			return new LocalizedString("ErrorManagementObjectNotFoundWithSourceByType", "Ex727424", false, true, Strings.ResourceManager, new object[]
			{
				type,
				source
			});
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0005A098 File Offset: 0x00058298
		public static LocalizedString ErrorInvalidParameterType(string parameterName, string parameterType)
		{
			return new LocalizedString("ErrorInvalidParameterType", "ExEDDBFE", false, true, Strings.ResourceManager, new object[]
			{
				parameterName,
				parameterType
			});
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0005A0CC File Offset: 0x000582CC
		public static LocalizedString ExceptionCondition(LocalizedString failureDescription, Condition faultingCondition)
		{
			return new LocalizedString("ExceptionCondition", "ExC76D39", false, true, Strings.ResourceManager, new object[]
			{
				failureDescription,
				faultingCondition
			});
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005A104 File Offset: 0x00058304
		public static LocalizedString EnabledString
		{
			get
			{
				return new LocalizedString("EnabledString", "Ex667CAC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0005A124 File Offset: 0x00058324
		public static LocalizedString ElapsedTimeDescription(int h, int mm, int ss)
		{
			return new LocalizedString("ElapsedTimeDescription", "ExB3E8C1", false, true, Strings.ResourceManager, new object[]
			{
				h,
				mm,
				ss
			});
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0005A16C File Offset: 0x0005836C
		public static LocalizedString ExceptionParseError(string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionParseError", "Ex5420B7", false, true, Strings.ResourceManager, new object[]
			{
				invalidQuery,
				position
			});
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0005A1A4 File Offset: 0x000583A4
		public static LocalizedString VerboseLbGeneralTrace(string message)
		{
			return new LocalizedString("VerboseLbGeneralTrace", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0005A1D4 File Offset: 0x000583D4
		public static LocalizedString ErrorPublicFolderDatabaseIsNotMounted(string database)
		{
			return new LocalizedString("ErrorPublicFolderDatabaseIsNotMounted", "ExE09C4F", false, true, Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0005A204 File Offset: 0x00058404
		public static LocalizedString WarningForceMessageWithId(string identity)
		{
			return new LocalizedString("WarningForceMessageWithId", "Ex2E55C2", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0005A233 File Offset: 0x00058433
		public static LocalizedString WorkUnitWarning
		{
			get
			{
				return new LocalizedString("WorkUnitWarning", "Ex6342E6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0005A254 File Offset: 0x00058454
		public static LocalizedString ErrorPublicFolderGeneratingProxy(string identity)
		{
			return new LocalizedString("ErrorPublicFolderGeneratingProxy", "ExE90F4A", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0005A284 File Offset: 0x00058484
		public static LocalizedString ErrorMaxRunspacesLimit(string maxConnection, string policyPart)
		{
			return new LocalizedString("ErrorMaxRunspacesLimit", "", false, false, Strings.ResourceManager, new object[]
			{
				maxConnection,
				policyPart
			});
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0005A2B7 File Offset: 0x000584B7
		public static LocalizedString VerboseLbServerDownSoMarkDatabaseDown
		{
			get
			{
				return new LocalizedString("VerboseLbServerDownSoMarkDatabaseDown", "Ex6B0DE4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0005A2D8 File Offset: 0x000584D8
		public static LocalizedString LogConditionMatchingPropertyMismatch(Type conditionType, string propertyName, object yourValue, object theirValue)
		{
			return new LocalizedString("LogConditionMatchingPropertyMismatch", "Ex96B080", false, true, Strings.ResourceManager, new object[]
			{
				conditionType,
				propertyName,
				yourValue,
				theirValue
			});
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0005A314 File Offset: 0x00058514
		public static LocalizedString HandlerThronwExceptionInOnComplete(int i, string exception)
		{
			return new LocalizedString("HandlerThronwExceptionInOnComplete", "Ex7BABB4", false, true, Strings.ResourceManager, new object[]
			{
				i,
				exception
			});
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0005A34C File Offset: 0x0005854C
		public static LocalizedString InvalidGuidParameter(string parameterValue)
		{
			return new LocalizedString("InvalidGuidParameter", "", false, false, Strings.ResourceManager, new object[]
			{
				parameterValue
			});
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0005A37C File Offset: 0x0005857C
		public static LocalizedString LogTaskExecutionPlan(int taskIndex, Task task)
		{
			return new LocalizedString("LogTaskExecutionPlan", "Ex63378F", false, true, Strings.ResourceManager, new object[]
			{
				taskIndex,
				task
			});
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0005A3B4 File Offset: 0x000585B4
		public static LocalizedString WarningCannotGetLocalServerFqdn(string msg)
		{
			return new LocalizedString("WarningCannotGetLocalServerFqdn", "", false, false, Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0005A3E3 File Offset: 0x000585E3
		public static LocalizedString BinaryDataStakeHodler
		{
			get
			{
				return new LocalizedString("BinaryDataStakeHodler", "Ex516277", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0005A404 File Offset: 0x00058604
		public static LocalizedString ExArgumentOutOfRangeException(string paramName, object actualValue)
		{
			return new LocalizedString("ExArgumentOutOfRangeException", "Ex5A5127", false, true, Strings.ResourceManager, new object[]
			{
				paramName,
				actualValue
			});
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0005A438 File Offset: 0x00058638
		public static LocalizedString ErrorRemoveNewerObject(string identity, string version)
		{
			return new LocalizedString("ErrorRemoveNewerObject", "ExE88A71", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				version
			});
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0005A46C File Offset: 0x0005866C
		public static LocalizedString DelegatedAdminAccount(string identity)
		{
			return new LocalizedString("DelegatedAdminAccount", "Ex9A9C5A", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005A49B File Offset: 0x0005869B
		public static LocalizedString ErrorWriteOpOnDehydratedTenant
		{
			get
			{
				return new LocalizedString("ErrorWriteOpOnDehydratedTenant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0005A4BC File Offset: 0x000586BC
		public static LocalizedString ErrorInvalidHierarchicalIdentity(string identity, string reason)
		{
			return new LocalizedString("ErrorInvalidHierarchicalIdentity", "ExC635FE", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				reason
			});
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0005A4F0 File Offset: 0x000586F0
		public static LocalizedString ExceptionRollbackFailed(Type taskType, Exception rollbackException)
		{
			return new LocalizedString("ExceptionRollbackFailed", "Ex749D4B", false, true, Strings.ResourceManager, new object[]
			{
				taskType,
				rollbackException
			});
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005A523 File Offset: 0x00058723
		public static LocalizedString ExceptionMDACommandAlreadyExecuting
		{
			get
			{
				return new LocalizedString("ExceptionMDACommandAlreadyExecuting", "Ex1848E9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0005A544 File Offset: 0x00058744
		public static LocalizedString VerboseTaskEndProcessing(string name)
		{
			return new LocalizedString("VerboseTaskEndProcessing", "ExD6F385", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0005A574 File Offset: 0x00058774
		public static LocalizedString ErrorInvalidMailboxFolderIdentity(string input)
		{
			return new LocalizedString("ErrorInvalidMailboxFolderIdentity", "Ex8944E7", false, true, Strings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0005A5A4 File Offset: 0x000587A4
		public static LocalizedString ErrorFoundMultipleRootRole(string roleType)
		{
			return new LocalizedString("ErrorFoundMultipleRootRole", "", false, false, Strings.ResourceManager, new object[]
			{
				roleType
			});
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0005A5D4 File Offset: 0x000587D4
		public static LocalizedString ImpersonationNotAllowed(string account, string impersonatedUser)
		{
			return new LocalizedString("ImpersonationNotAllowed", "Ex723AFD", false, true, Strings.ResourceManager, new object[]
			{
				account,
				impersonatedUser
			});
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0005A607 File Offset: 0x00058807
		public static LocalizedString SipCultureInfoArgumentCheckFailure
		{
			get
			{
				return new LocalizedString("SipCultureInfoArgumentCheckFailure", "Ex33719F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0005A628 File Offset: 0x00058828
		public static LocalizedString MonitoringEventString(string eventSource, int eventId, string eventType, string eventMessage)
		{
			return new LocalizedString("MonitoringEventString", "Ex89F6CA", false, true, Strings.ResourceManager, new object[]
			{
				eventSource,
				eventId,
				eventType,
				eventMessage
			});
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0005A668 File Offset: 0x00058868
		public static LocalizedString ErrorFailedToReadFromDC(string id, string dc)
		{
			return new LocalizedString("ErrorFailedToReadFromDC", "Ex4215D1", false, true, Strings.ResourceManager, new object[]
			{
				id,
				dc
			});
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0005A69C File Offset: 0x0005889C
		public static LocalizedString ErrorTenantMaxRunspacesTarpitting(string orgName, int delaySeconds)
		{
			return new LocalizedString("ErrorTenantMaxRunspacesTarpitting", "", false, false, Strings.ResourceManager, new object[]
			{
				orgName,
				delaySeconds
			});
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0005A6D4 File Offset: 0x000588D4
		public static LocalizedString ForeignForestTrustFailedException(string user)
		{
			return new LocalizedString("ForeignForestTrustFailedException", "Ex19B768", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0005A704 File Offset: 0x00058904
		public static LocalizedString WrongTypeUserContact(string identity)
		{
			return new LocalizedString("WrongTypeUserContact", "Ex086491", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0005A734 File Offset: 0x00058934
		public static LocalizedString ProvisioningUpdateAffectedObject(int handlerIndex, string detailes)
		{
			return new LocalizedString("ProvisioningUpdateAffectedObject", "ExD120E5", false, true, Strings.ResourceManager, new object[]
			{
				handlerIndex,
				detailes
			});
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0005A76C File Offset: 0x0005896C
		public static LocalizedString LookupUserAsDomainUser(string user)
		{
			return new LocalizedString("LookupUserAsDomainUser", "Ex81D909", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0005A79C File Offset: 0x0005899C
		public static LocalizedString MissingAttribute(string attribute, string element)
		{
			return new LocalizedString("MissingAttribute", "Ex02D59E", false, true, Strings.ResourceManager, new object[]
			{
				attribute,
				element
			});
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0005A7D0 File Offset: 0x000589D0
		public static LocalizedString ErrorCannotFormatRecipient(int recipientType)
		{
			return new LocalizedString("ErrorCannotFormatRecipient", "Ex869E77", false, true, Strings.ResourceManager, new object[]
			{
				recipientType
			});
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005A804 File Offset: 0x00058A04
		public static LocalizedString ConsecutiveWholeWildcardNamePartsInHierarchicalIdentity
		{
			get
			{
				return new LocalizedString("ConsecutiveWholeWildcardNamePartsInHierarchicalIdentity", "ExABED9B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0005A824 File Offset: 0x00058A24
		public static LocalizedString ErrorCannotGetPublicFolderDatabaseByLegacyDn(string legacyDn)
		{
			return new LocalizedString("ErrorCannotGetPublicFolderDatabaseByLegacyDn", "ExF245BB", false, true, Strings.ResourceManager, new object[]
			{
				legacyDn
			});
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005A853 File Offset: 0x00058A53
		public static LocalizedString ErrorMapiPublicFolderTreeNotUnique
		{
			get
			{
				return new LocalizedString("ErrorMapiPublicFolderTreeNotUnique", "ExCD8A8E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0005A874 File Offset: 0x00058A74
		public static LocalizedString VerboseRemovedRoleAssignment(string id)
		{
			return new LocalizedString("VerboseRemovedRoleAssignment", "Ex65326E", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0005A8A3 File Offset: 0x00058AA3
		public static LocalizedString WarningMoreResultsAvailable
		{
			get
			{
				return new LocalizedString("WarningMoreResultsAvailable", "ExE002C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0005A8C4 File Offset: 0x00058AC4
		public static LocalizedString LoadingLogonUser(string account)
		{
			return new LocalizedString("LoadingLogonUser", "Ex040F63", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		public static LocalizedString ErrorObjectVersionChanged(string identity)
		{
			return new LocalizedString("ErrorObjectVersionChanged", "ExCA77A4", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0005A924 File Offset: 0x00058B24
		public static LocalizedString WrongTypeMailboxOrMailUserOrMailContact(string identity)
		{
			return new LocalizedString("WrongTypeMailboxOrMailUserOrMailContact", "ExC91979", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005A953 File Offset: 0x00058B53
		public static LocalizedString ErrorOperationOnInvalidObject
		{
			get
			{
				return new LocalizedString("ErrorOperationOnInvalidObject", "Ex9D674A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0005A974 File Offset: 0x00058B74
		public static LocalizedString ErrorTaskWin32Exception(string error)
		{
			return new LocalizedString("ErrorTaskWin32Exception", "ExF23ED4", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0005A9A4 File Offset: 0x00058BA4
		public static LocalizedString ServiceNotInstalled(string servicename)
		{
			return new LocalizedString("ServiceNotInstalled", "Ex216ACC", false, true, Strings.ResourceManager, new object[]
			{
				servicename
			});
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005A9D4 File Offset: 0x00058BD4
		public static LocalizedString PooledConnectionOpenTimeoutError(string msg)
		{
			return new LocalizedString("PooledConnectionOpenTimeoutError", "Ex9E338C", false, true, Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0005AA04 File Offset: 0x00058C04
		public static LocalizedString VerboseLbAddingEligibleServer(string mailboxServer)
		{
			return new LocalizedString("VerboseLbAddingEligibleServer", "Ex501298", false, true, Strings.ResourceManager, new object[]
			{
				mailboxServer
			});
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0005AA33 File Offset: 0x00058C33
		public static LocalizedString VerboseInitializeRunspaceServerSettingsAdam
		{
			get
			{
				return new LocalizedString("VerboseInitializeRunspaceServerSettingsAdam", "Ex7E23DE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0005AA54 File Offset: 0x00058C54
		public static LocalizedString ErrorInvalidIdentity(string idValue)
		{
			return new LocalizedString("ErrorInvalidIdentity", "ExDDE39A", false, true, Strings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0005AA83 File Offset: 0x00058C83
		public static LocalizedString VerboseLbNoAvailableDatabase
		{
			get
			{
				return new LocalizedString("VerboseLbNoAvailableDatabase", "Ex56A0A4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0005AAA4 File Offset: 0x00058CA4
		public static LocalizedString NonMigratedUserException(string identity)
		{
			return new LocalizedString("NonMigratedUserException", "Ex52DC13", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0005AAD4 File Offset: 0x00058CD4
		public static LocalizedString InvalidPropertyName(string property)
		{
			return new LocalizedString("InvalidPropertyName", "Ex9E0328", false, true, Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0005AB04 File Offset: 0x00058D04
		public static LocalizedString WrongTypeDynamicGroup(string identity)
		{
			return new LocalizedString("WrongTypeDynamicGroup", "Ex16CB89", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005AB33 File Offset: 0x00058D33
		public static LocalizedString PswsManagementAutomationAssemblyLoadError
		{
			get
			{
				return new LocalizedString("PswsManagementAutomationAssemblyLoadError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0005AB54 File Offset: 0x00058D54
		public static LocalizedString LoadingRoleErrorText(string account)
		{
			return new LocalizedString("LoadingRoleErrorText", "Ex637EA9", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0005AB83 File Offset: 0x00058D83
		public static LocalizedString LogRollbackFailed
		{
			get
			{
				return new LocalizedString("LogRollbackFailed", "Ex837B90", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0005ABA4 File Offset: 0x00058DA4
		public static LocalizedString PowerShellTimeout(int timeout)
		{
			return new LocalizedString("PowerShellTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				timeout
			});
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0005ABD8 File Offset: 0x00058DD8
		public static LocalizedString VerboseLbDatabaseAndServerTry(string databaseDn, string serverFqdn)
		{
			return new LocalizedString("VerboseLbDatabaseAndServerTry", "ExA64F9C", false, true, Strings.ResourceManager, new object[]
			{
				databaseDn,
				serverFqdn
			});
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0005AC0C File Offset: 0x00058E0C
		public static LocalizedString ErrorCannotResolveCertificate(string certName)
		{
			return new LocalizedString("ErrorCannotResolveCertificate", "", false, false, Strings.ResourceManager, new object[]
			{
				certName
			});
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0005AC3C File Offset: 0x00058E3C
		public static LocalizedString WrongTypeMailEnabledContact(string identity)
		{
			return new LocalizedString("WrongTypeMailEnabledContact", "Ex14B1F2", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0005AC6C File Offset: 0x00058E6C
		public static LocalizedString ExchangeSetupCannotCopyWatson(string logFile, string watsonFile)
		{
			return new LocalizedString("ExchangeSetupCannotCopyWatson", "Ex6E8702", false, true, Strings.ResourceManager, new object[]
			{
				logFile,
				watsonFile
			});
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0005ACA0 File Offset: 0x00058EA0
		public static LocalizedString VerboseApplyRusPolicyForRecipient(string id, string homeDC)
		{
			return new LocalizedString("VerboseApplyRusPolicyForRecipient", "Ex3EAF98", false, true, Strings.ResourceManager, new object[]
			{
				id,
				homeDC
			});
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0005ACD4 File Offset: 0x00058ED4
		public static LocalizedString NotInSameOrg(string logonIdentity, string impersonatedIdentity)
		{
			return new LocalizedString("NotInSameOrg", "Ex7DBC70", false, true, Strings.ResourceManager, new object[]
			{
				logonIdentity,
				impersonatedIdentity
			});
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0005AD08 File Offset: 0x00058F08
		public static LocalizedString WrongTypeLogonableObjectIdParameter(string identity)
		{
			return new LocalizedString("WrongTypeLogonableObjectIdParameter", "ExEE7DA9", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0005AD38 File Offset: 0x00058F38
		public static LocalizedString ClashingIdentities(string Assembly, string ClassFactory)
		{
			return new LocalizedString("ClashingIdentities", "ExE4FDE6", false, true, Strings.ResourceManager, new object[]
			{
				Assembly,
				ClassFactory
			});
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0005AD6C File Offset: 0x00058F6C
		public static LocalizedString ErrorMaxRunspacesTarpitting(int delaySeconds)
		{
			return new LocalizedString("ErrorMaxRunspacesTarpitting", "", false, false, Strings.ResourceManager, new object[]
			{
				delaySeconds
			});
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005ADA0 File Offset: 0x00058FA0
		public static LocalizedString VerboseExecutingUserContext(string executingUserId, string executingUserOrganizationId, string currentOrganizationId, string isRbacEnabled)
		{
			return new LocalizedString("VerboseExecutingUserContext", "Ex0E7FA3", false, true, Strings.ResourceManager, new object[]
			{
				executingUserId,
				executingUserOrganizationId,
				currentOrganizationId,
				isRbacEnabled
			});
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005ADDC File Offset: 0x00058FDC
		public static LocalizedString ExceptionSetupFileNotFound(string fileName)
		{
			return new LocalizedString("ExceptionSetupFileNotFound", "Ex040A47", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0005AE0C File Offset: 0x0005900C
		public static LocalizedString VerboseFailedToGetProxyServer(int minServerVersion, string objectVersion)
		{
			return new LocalizedString("VerboseFailedToGetProxyServer", "", false, false, Strings.ResourceManager, new object[]
			{
				minServerVersion,
				objectVersion
			});
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0005AE44 File Offset: 0x00059044
		public static LocalizedString NullOrEmptyNamePartsInHierarchicalIdentity
		{
			get
			{
				return new LocalizedString("NullOrEmptyNamePartsInHierarchicalIdentity", "ExD28329", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0005AE62 File Offset: 0x00059062
		public static LocalizedString ErrorCloseServiceHandle
		{
			get
			{
				return new LocalizedString("ErrorCloseServiceHandle", "Ex9FC550", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0005AE80 File Offset: 0x00059080
		public static LocalizedString ExceptionTaskContextConsistencyViolation(Type taskType)
		{
			return new LocalizedString("ExceptionTaskContextConsistencyViolation", "Ex9F100E", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0005AEB0 File Offset: 0x000590B0
		public static LocalizedString LogPreconditionImmediate(Type conditionType)
		{
			return new LocalizedString("LogPreconditionImmediate", "ExEB4D08", false, true, Strings.ResourceManager, new object[]
			{
				conditionType
			});
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0005AEE0 File Offset: 0x000590E0
		public static LocalizedString ErrorInvalidType(string parameterType)
		{
			return new LocalizedString("ErrorInvalidType", "Ex4B61A0", false, true, Strings.ResourceManager, new object[]
			{
				parameterType
			});
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0005AF0F File Offset: 0x0005910F
		public static LocalizedString WorkUnitStatusCompleted
		{
			get
			{
				return new LocalizedString("WorkUnitStatusCompleted", "Ex8BFED9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0005AF30 File Offset: 0x00059130
		public static LocalizedString VerboseCannotGetRemoteServerForUser(string id, string proxyAddress, string reason)
		{
			return new LocalizedString("VerboseCannotGetRemoteServerForUser", "", false, false, Strings.ResourceManager, new object[]
			{
				id,
				proxyAddress,
				reason
			});
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0005AF68 File Offset: 0x00059168
		public static LocalizedString ErrorNoReplicaOnServer(string folder, string server)
		{
			return new LocalizedString("ErrorNoReplicaOnServer", "ExD77E9D", false, true, Strings.ResourceManager, new object[]
			{
				folder,
				server
			});
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0005AF9C File Offset: 0x0005919C
		public static LocalizedString ErrorUserNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorUserNotUnique", "Ex0F741B", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0005AFCC File Offset: 0x000591CC
		public static LocalizedString ErrorMaxConnectionLimit(string vdirPath)
		{
			return new LocalizedString("ErrorMaxConnectionLimit", "ExED1E53", false, true, Strings.ResourceManager, new object[]
			{
				vdirPath
			});
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005AFFB File Offset: 0x000591FB
		public static LocalizedString ErrorUrlInValid
		{
			get
			{
				return new LocalizedString("ErrorUrlInValid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0005B01C File Offset: 0x0005921C
		public static LocalizedString DuplicateExternalDirectoryObjectIdException(string objectName, string edoId)
		{
			return new LocalizedString("DuplicateExternalDirectoryObjectIdException", "", false, false, Strings.ResourceManager, new object[]
			{
				objectName,
				edoId
			});
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0005B050 File Offset: 0x00059250
		public static LocalizedString InvalidElement(string element)
		{
			return new LocalizedString("InvalidElement", "Ex19A663", false, true, Strings.ResourceManager, new object[]
			{
				element
			});
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0005B080 File Offset: 0x00059280
		public static LocalizedString VerbosePreConditions(string conditions)
		{
			return new LocalizedString("VerbosePreConditions", "Ex0A14CB", false, true, Strings.ResourceManager, new object[]
			{
				conditions
			});
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005B0AF File Offset: 0x000592AF
		public static LocalizedString ErrorNoMailboxUserInTheForest
		{
			get
			{
				return new LocalizedString("ErrorNoMailboxUserInTheForest", "Ex4DE0B7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0005B0D0 File Offset: 0x000592D0
		public static LocalizedString WrongTypeGroup(string identity)
		{
			return new LocalizedString("WrongTypeGroup", "Ex62C8AC", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005B0FF File Offset: 0x000592FF
		public static LocalizedString ServerNotAvailable
		{
			get
			{
				return new LocalizedString("ServerNotAvailable", "Ex1A17BA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0005B120 File Offset: 0x00059320
		public static LocalizedString VerboseSelectedRusServer(string server)
		{
			return new LocalizedString("VerboseSelectedRusServer", "ExAC18CC", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0005B150 File Offset: 0x00059350
		public static LocalizedString VerboseLbDisposeExRpcAdmin(string serverFqdn)
		{
			return new LocalizedString("VerboseLbDisposeExRpcAdmin", "Ex4804CF", false, true, Strings.ResourceManager, new object[]
			{
				serverFqdn
			});
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0005B180 File Offset: 0x00059380
		public static LocalizedString ErrorRelativeDn(string dn)
		{
			return new LocalizedString("ErrorRelativeDn", "ExFF31B9", false, true, Strings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005B1AF File Offset: 0x000593AF
		public static LocalizedString HelpUrlHeaderText
		{
			get
			{
				return new LocalizedString("HelpUrlHeaderText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0005B1D0 File Offset: 0x000593D0
		public static LocalizedString InvalidAttributeValue(string value, string attribute)
		{
			return new LocalizedString("InvalidAttributeValue", "Ex4644BC", false, true, Strings.ResourceManager, new object[]
			{
				value,
				attribute
			});
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0005B204 File Offset: 0x00059404
		public static LocalizedString PropertyIsAlreadyProvisioned(string propertyName, int i)
		{
			return new LocalizedString("PropertyIsAlreadyProvisioned", "Ex4F79CE", false, true, Strings.ResourceManager, new object[]
			{
				propertyName,
				i
			});
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0005B23C File Offset: 0x0005943C
		public static LocalizedString ErrorInvalidModerator(string moderator)
		{
			return new LocalizedString("ErrorInvalidModerator", "Ex33BBE9", false, true, Strings.ResourceManager, new object[]
			{
				moderator
			});
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0005B26C File Offset: 0x0005946C
		public static LocalizedString ErrorOrganizationalUnitNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationalUnitNotUnique", "Ex52FDB2", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0005B29C File Offset: 0x0005949C
		public static LocalizedString ExceptionMissingCreateInstance(Type type, string codeBase)
		{
			return new LocalizedString("ExceptionMissingCreateInstance", "Ex23D891", false, true, Strings.ResourceManager, new object[]
			{
				type,
				codeBase
			});
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0005B2D0 File Offset: 0x000594D0
		public static LocalizedString ExecutingUserPropertyNotFound(string propertyName)
		{
			return new LocalizedString("ExecutingUserPropertyNotFound", "Ex6B9416", false, true, Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0005B300 File Offset: 0x00059500
		public static LocalizedString CannotHaveLocalAccountException(string user)
		{
			return new LocalizedString("CannotHaveLocalAccountException", "ExE7A580", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005B32F File Offset: 0x0005952F
		public static LocalizedString VersionMismatchDuringCreateRemoteRunspace
		{
			get
			{
				return new LocalizedString("VersionMismatchDuringCreateRemoteRunspace", "ExB0FB5E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0005B350 File Offset: 0x00059550
		public static LocalizedString LoadingRoleAssignmentErrorText(string account)
		{
			return new LocalizedString("LoadingRoleAssignmentErrorText", "ExE848AF", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0005B380 File Offset: 0x00059580
		public static LocalizedString ExceptionMissingDataSourceManager(string codeBase)
		{
			return new LocalizedString("ExceptionMissingDataSourceManager", "ExCF7C58", false, true, Strings.ResourceManager, new object[]
			{
				codeBase
			});
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005B3AF File Offset: 0x000595AF
		public static LocalizedString ErrorCannotOpenServiceControllerManager
		{
			get
			{
				return new LocalizedString("ErrorCannotOpenServiceControllerManager", "ExFC4F1D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0005B3D0 File Offset: 0x000595D0
		public static LocalizedString ProvisioningExceptionMessage(string error)
		{
			return new LocalizedString("ProvisioningExceptionMessage", "ExA2F22D", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0005B400 File Offset: 0x00059600
		public static LocalizedString ErrorObjectHasValidationErrorsWithId(object identity)
		{
			return new LocalizedString("ErrorObjectHasValidationErrorsWithId", "Ex34751A", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0005B42F File Offset: 0x0005962F
		public static LocalizedString VerboseLbNoEligibleServers
		{
			get
			{
				return new LocalizedString("VerboseLbNoEligibleServers", "Ex1DA741", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0005B450 File Offset: 0x00059650
		public static LocalizedString ExceptionTaskExpansionTooDeep(Type taskType)
		{
			return new LocalizedString("ExceptionTaskExpansionTooDeep", "Ex20F3BB", false, true, Strings.ResourceManager, new object[]
			{
				taskType
			});
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0005B480 File Offset: 0x00059680
		public static LocalizedString VerboseSourceFromGC(string source)
		{
			return new LocalizedString("VerboseSourceFromGC", "Ex69861F", false, true, Strings.ResourceManager, new object[]
			{
				source
			});
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0005B4B0 File Offset: 0x000596B0
		public static LocalizedString VerboseCannotResolveSid(string sid, string msg)
		{
			return new LocalizedString("VerboseCannotResolveSid", "Ex68400D", false, true, Strings.ResourceManager, new object[]
			{
				sid,
				msg
			});
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0005B4E4 File Offset: 0x000596E4
		public static LocalizedString ErrorCannotSendMailToPublicFolderMailbox(string id)
		{
			return new LocalizedString("ErrorCannotSendMailToPublicFolderMailbox", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0005B514 File Offset: 0x00059714
		public static LocalizedString ErrorCorruptedPartition(string partitionName)
		{
			return new LocalizedString("ErrorCorruptedPartition", "", false, false, Strings.ResourceManager, new object[]
			{
				partitionName
			});
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0005B544 File Offset: 0x00059744
		public static LocalizedString VerboseADObjectChangedPropertiesWithId(string id, string propertyList)
		{
			return new LocalizedString("VerboseADObjectChangedPropertiesWithId", "Ex85EC0D", false, true, Strings.ResourceManager, new object[]
			{
				id,
				propertyList
			});
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0005B578 File Offset: 0x00059778
		public static LocalizedString VerboseLbFoundMailboxServer(string mailboxServer)
		{
			return new LocalizedString("VerboseLbFoundMailboxServer", "ExC8624C", false, true, Strings.ResourceManager, new object[]
			{
				mailboxServer
			});
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005B5A8 File Offset: 0x000597A8
		public static LocalizedString ErrorSetTaskChangeRecipientType(string id, string oldType, string newType)
		{
			return new LocalizedString("ErrorSetTaskChangeRecipientType", "ExA36246", false, true, Strings.ResourceManager, new object[]
			{
				id,
				oldType,
				newType
			});
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0005B5E0 File Offset: 0x000597E0
		public static LocalizedString ServiceStartFailure(string name, string msg)
		{
			return new LocalizedString("ServiceStartFailure", "ExA4C2D6", false, true, Strings.ResourceManager, new object[]
			{
				name,
				msg
			});
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0005B613 File Offset: 0x00059813
		public static LocalizedString VerboseLbDatabaseFound
		{
			get
			{
				return new LocalizedString("VerboseLbDatabaseFound", "Ex3260C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0005B631 File Offset: 0x00059831
		public static LocalizedString VerboseADObjectNoChangedProperties
		{
			get
			{
				return new LocalizedString("VerboseADObjectNoChangedProperties", "Ex13F412", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005B650 File Offset: 0x00059850
		public static LocalizedString ErrorCouldNotFindCorrespondingObject(string identity, Type type, string dc)
		{
			return new LocalizedString("ErrorCouldNotFindCorrespondingObject", "ExEC9D18", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				type,
				dc
			});
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005B688 File Offset: 0x00059888
		public static LocalizedString VerboseWriteResultSize(string resultSize)
		{
			return new LocalizedString("VerboseWriteResultSize", "Ex07FDA9", false, true, Strings.ResourceManager, new object[]
			{
				resultSize
			});
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005B6B7 File Offset: 0x000598B7
		public static LocalizedString VerbosePopulateScopeSet
		{
			get
			{
				return new LocalizedString("VerbosePopulateScopeSet", "Ex0A310D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0005B6D8 File Offset: 0x000598D8
		public static LocalizedString VerboseTaskSpecifiedParameters(string paramInfo)
		{
			return new LocalizedString("VerboseTaskSpecifiedParameters", "", false, false, Strings.ResourceManager, new object[]
			{
				paramInfo
			});
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0005B708 File Offset: 0x00059908
		public static LocalizedString VerboseLbDatabaseNotInUserScope(string databaseDn, string errorMessage)
		{
			return new LocalizedString("VerboseLbDatabaseNotInUserScope", "ExE29229", false, true, Strings.ResourceManager, new object[]
			{
				databaseDn,
				errorMessage
			});
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0005B73B File Offset: 0x0005993B
		public static LocalizedString ErrorCertificateDenied
		{
			get
			{
				return new LocalizedString("ErrorCertificateDenied", "ExCEFBB0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005B75C File Offset: 0x0005995C
		public static LocalizedString ErrorNoServersAndOutofServerScope(string databaseid, string serverid)
		{
			return new LocalizedString("ErrorNoServersAndOutofServerScope", "ExB07E6B", false, true, Strings.ResourceManager, new object[]
			{
				databaseid,
				serverid
			});
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0005B790 File Offset: 0x00059990
		public static LocalizedString ErrorInvalidAddressListIdentity(string idValue)
		{
			return new LocalizedString("ErrorInvalidAddressListIdentity", "ExB3AA2D", false, true, Strings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0005B7C0 File Offset: 0x000599C0
		public static LocalizedString ErrorOrganizationalUnitNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationalUnitNotFound", "ExF75CBB", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0005B7EF File Offset: 0x000599EF
		public static LocalizedString ErrorCannotResolvePUIDToWindowsIdentity
		{
			get
			{
				return new LocalizedString("ErrorCannotResolvePUIDToWindowsIdentity", "ExB19BD7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0005B810 File Offset: 0x00059A10
		public static LocalizedString VerboseAdminSessionSettingsConfigDC(string configDC)
		{
			return new LocalizedString("VerboseAdminSessionSettingsConfigDC", "Ex78E529", false, true, Strings.ResourceManager, new object[]
			{
				configDC
			});
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0005B83F File Offset: 0x00059A3F
		public static LocalizedString ErrorMissOrganization
		{
			get
			{
				return new LocalizedString("ErrorMissOrganization", "Ex55298E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0005B860 File Offset: 0x00059A60
		public static LocalizedString WrongTypeRecipientIdParamter(string identity)
		{
			return new LocalizedString("WrongTypeRecipientIdParamter", "Ex555066", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0005B890 File Offset: 0x00059A90
		public static LocalizedString ErrorOuOutOfOrganization(string ou)
		{
			return new LocalizedString("ErrorOuOutOfOrganization", "ExC44E57", false, true, Strings.ResourceManager, new object[]
			{
				ou
			});
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0005B8C0 File Offset: 0x00059AC0
		public static LocalizedString InvocationExceptionDescription(string error, string commandText)
		{
			return new LocalizedString("InvocationExceptionDescription", "Ex9078DF", false, true, Strings.ResourceManager, new object[]
			{
				error,
				commandText
			});
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0005B8F4 File Offset: 0x00059AF4
		public static LocalizedString VerboseLbNetworkError(string errorMessage)
		{
			return new LocalizedString("VerboseLbNetworkError", "ExDE3357", false, true, Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0005B923 File Offset: 0x00059B23
		public static LocalizedString ExceptionMDAConnectionAlreadyOpened
		{
			get
			{
				return new LocalizedString("ExceptionMDAConnectionAlreadyOpened", "Ex8C607C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0005B941 File Offset: 0x00059B41
		public static LocalizedString ExceptionObjectStillExists
		{
			get
			{
				return new LocalizedString("ExceptionObjectStillExists", "ExA36AA4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005B95F File Offset: 0x00059B5F
		public static LocalizedString ExceptionMDAConnectionMustBeOpened
		{
			get
			{
				return new LocalizedString("ExceptionMDAConnectionMustBeOpened", "ExAFC863", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0005B97D File Offset: 0x00059B7D
		public static LocalizedString WriteErrorMessage
		{
			get
			{
				return new LocalizedString("WriteErrorMessage", "ExCCF2A6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0005B99C File Offset: 0x00059B9C
		public static LocalizedString UserNotSAMAccount(string user)
		{
			return new LocalizedString("UserNotSAMAccount", "ExC8F580", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0005B9CC File Offset: 0x00059BCC
		public static LocalizedString VerboseADObjectChangedPropertiesWithDn(string id, string dn, string propertyList)
		{
			return new LocalizedString("VerboseADObjectChangedPropertiesWithDn", "ExB09D19", false, true, Strings.ResourceManager, new object[]
			{
				id,
				dn,
				propertyList
			});
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0005BA04 File Offset: 0x00059C04
		public static LocalizedString ErrorChangeServiceConfig2(string name)
		{
			return new LocalizedString("ErrorChangeServiceConfig2", "Ex326B1C", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0005BA34 File Offset: 0x00059C34
		public static LocalizedString InstantiatingHandlerForAgent(int i, string agentName)
		{
			return new LocalizedString("InstantiatingHandlerForAgent", "Ex93D03D", false, true, Strings.ResourceManager, new object[]
			{
				i,
				agentName
			});
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0005BA6C File Offset: 0x00059C6C
		public static LocalizedString GenericConditionFailure
		{
			get
			{
				return new LocalizedString("GenericConditionFailure", "ExA30D5C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0005BA8C File Offset: 0x00059C8C
		public static LocalizedString LoadingRoleAssignment(string account)
		{
			return new LocalizedString("LoadingRoleAssignment", "ExECD590", false, true, Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0005BABC File Offset: 0x00059CBC
		public static LocalizedString ErrorServerNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorServerNotFound", "Ex0290FB", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0005BAEC File Offset: 0x00059CEC
		public static LocalizedString ErrorServerNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorServerNotUnique", "ExD07479", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0005BB1C File Offset: 0x00059D1C
		public static LocalizedString PswsInvocationTimout(int timeoutMsec)
		{
			return new LocalizedString("PswsInvocationTimout", "", false, false, Strings.ResourceManager, new object[]
			{
				timeoutMsec
			});
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0005BB50 File Offset: 0x00059D50
		public static LocalizedString VerboseAdminSessionSettingsDCs(string DCs)
		{
			return new LocalizedString("VerboseAdminSessionSettingsDCs", "Ex9365DA", false, true, Strings.ResourceManager, new object[]
			{
				DCs
			});
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0005BB7F File Offset: 0x00059D7F
		public static LocalizedString VerboseLbExRpcAdminExists
		{
			get
			{
				return new LocalizedString("VerboseLbExRpcAdminExists", "ExA32E96", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0005BBA0 File Offset: 0x00059DA0
		public static LocalizedString ClashingPriorities(string Priority, string Name, string otherAgentName)
		{
			return new LocalizedString("ClashingPriorities", "Ex9F2F5E", false, true, Strings.ResourceManager, new object[]
			{
				Priority,
				Name,
				otherAgentName
			});
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		public static LocalizedString VerboseResolvedOrganization(string orgId)
		{
			return new LocalizedString("VerboseResolvedOrganization", "Ex6C34B5", false, true, Strings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0005BC08 File Offset: 0x00059E08
		public static LocalizedString WrongTypeComputer(string identity)
		{
			return new LocalizedString("WrongTypeComputer", "Ex828D5C", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0005BC38 File Offset: 0x00059E38
		public static LocalizedString VerboseTaskFindDataObjects(string type, string filter, string scope, string root)
		{
			return new LocalizedString("VerboseTaskFindDataObjects", "ExEA61FA", false, true, Strings.ResourceManager, new object[]
			{
				type,
				filter,
				scope,
				root
			});
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0005BC74 File Offset: 0x00059E74
		public static LocalizedString ExceptionTypeNotFound(string typeName)
		{
			return new LocalizedString("ExceptionTypeNotFound", "Ex873EFB", false, true, Strings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0005BCA4 File Offset: 0x00059EA4
		public static LocalizedString VerboseFailedToDeserializePSObject(string msg)
		{
			return new LocalizedString("VerboseFailedToDeserializePSObject", "", false, false, Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0005BCD4 File Offset: 0x00059ED4
		public static LocalizedString WrongTypeGeneralMailboxIdParameter(string identity)
		{
			return new LocalizedString("WrongTypeGeneralMailboxIdParameter", "Ex2E1EF6", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0005BD04 File Offset: 0x00059F04
		public static LocalizedString WrongTypeMailboxUserContact(string identity)
		{
			return new LocalizedString("WrongTypeMailboxUserContact", "Ex2B5F02", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0005BD34 File Offset: 0x00059F34
		public static LocalizedString AgentAssemblyWithoutPathFound(string agentName)
		{
			return new LocalizedString("AgentAssemblyWithoutPathFound", "ExD7B056", false, true, Strings.ResourceManager, new object[]
			{
				agentName
			});
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0005BD64 File Offset: 0x00059F64
		public static LocalizedString ConfirmSharedConfiguration(string id)
		{
			return new LocalizedString("ConfirmSharedConfiguration", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005BD93 File Offset: 0x00059F93
		public static LocalizedString DisabledString
		{
			get
			{
				return new LocalizedString("DisabledString", "Ex1527F0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005BDB1 File Offset: 0x00059FB1
		public static LocalizedString ExceptionRemoveNoneExistenceObject
		{
			get
			{
				return new LocalizedString("ExceptionRemoveNoneExistenceObject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0005BDD0 File Offset: 0x00059FD0
		public static LocalizedString UserQuotaDelayNotEnforcedMaxThreadsExceeded(int cappedDelay, bool required, string part, int threadNum)
		{
			return new LocalizedString("UserQuotaDelayNotEnforcedMaxThreadsExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				cappedDelay,
				required,
				part,
				threadNum
			});
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005BE1A File Offset: 0x0005A01A
		public static LocalizedString LogErrorPrefix
		{
			get
			{
				return new LocalizedString("LogErrorPrefix", "ExBFA257", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0005BE38 File Offset: 0x0005A038
		public static LocalizedString ErrorOrgNotFound(string identity, string org)
		{
			return new LocalizedString("ErrorOrgNotFound", "Ex1F3BD8", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				org
			});
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0005BE6C File Offset: 0x0005A06C
		public static LocalizedString MultipleHandlersForCmdlet(string cmdlet, string asm1, string asm2)
		{
			return new LocalizedString("MultipleHandlersForCmdlet", "Ex32A86B", false, true, Strings.ResourceManager, new object[]
			{
				cmdlet,
				asm1,
				asm2
			});
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0005BEA3 File Offset: 0x0005A0A3
		public static LocalizedString ErrorMapiPublicFolderTreeNotFound
		{
			get
			{
				return new LocalizedString("ErrorMapiPublicFolderTreeNotFound", "ExF42A65", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0005BEC4 File Offset: 0x0005A0C4
		public static LocalizedString ConditionNotInitialized(string uninitializedProperty, Condition owningCondition)
		{
			return new LocalizedString("ConditionNotInitialized", "Ex2B6554", false, true, Strings.ResourceManager, new object[]
			{
				uninitializedProperty,
				owningCondition
			});
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0005BEF8 File Offset: 0x0005A0F8
		public static LocalizedString VerboseLbOnlyOneEligibleServer(string onlyServer)
		{
			return new LocalizedString("VerboseLbOnlyOneEligibleServer", "ExACACED", false, true, Strings.ResourceManager, new object[]
			{
				onlyServer
			});
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0005BF28 File Offset: 0x0005A128
		public static LocalizedString ProvisioningBrokerInitializationFailed(string message)
		{
			return new LocalizedString("ProvisioningBrokerInitializationFailed", "Ex0CC47D", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0005BF58 File Offset: 0x0005A158
		public static LocalizedString LogPreconditionDeferred(Type conditionType)
		{
			return new LocalizedString("LogPreconditionDeferred", "Ex865596", false, true, Strings.ResourceManager, new object[]
			{
				conditionType
			});
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0005BF88 File Offset: 0x0005A188
		public static LocalizedString VerboseAdminSessionSettings(string cmdletName)
		{
			return new LocalizedString("VerboseAdminSessionSettings", "ExD53031", false, true, Strings.ResourceManager, new object[]
			{
				cmdletName
			});
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
		public static LocalizedString WrongTypeNonMailEnabledGroup(string identity)
		{
			return new LocalizedString("WrongTypeNonMailEnabledGroup", "Ex68F6BD", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0005BFE8 File Offset: 0x0005A1E8
		public static LocalizedString ErrorIsOutofConfigWriteScope(string type, string id)
		{
			return new LocalizedString("ErrorIsOutofConfigWriteScope", "Ex6404E2", false, true, Strings.ResourceManager, new object[]
			{
				type,
				id
			});
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0005C01B File Offset: 0x0005A21B
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000791 RID: 1937
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(110);

		// Token: 0x04000792 RID: 1938
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Configuration.Common.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200029B RID: 667
		public enum IDs : uint
		{
			// Token: 0x04000794 RID: 1940
			MissingImpersonatedUserSid = 3480090246U,
			// Token: 0x04000795 RID: 1941
			FalseString = 3445110312U,
			// Token: 0x04000796 RID: 1942
			VerboseFailedToGetServiceTopology = 2329139938U,
			// Token: 0x04000797 RID: 1943
			NotGrantCustomScriptRole = 831535795U,
			// Token: 0x04000798 RID: 1944
			ErrorChangeImmutableType = 3609756192U,
			// Token: 0x04000799 RID: 1945
			NotGrantAnyAdminRoles = 1212809041U,
			// Token: 0x0400079A RID: 1946
			ErrorOnlyDNSupportedWithIgnoreDefaultScope = 2716022819U,
			// Token: 0x0400079B RID: 1947
			ExceptionTaskNotInitialized = 2866126871U,
			// Token: 0x0400079C RID: 1948
			VerboseLbNoDatabaseFoundInAD = 311479382U,
			// Token: 0x0400079D RID: 1949
			WorkUnitStatusNotStarted = 1464829243U,
			// Token: 0x0400079E RID: 1950
			TrueString = 444666707U,
			// Token: 0x0400079F RID: 1951
			InvalidPswsDirectInvocationBlocked = 1953440811U,
			// Token: 0x040007A0 RID: 1952
			InvalidProperties = 2103372448U,
			// Token: 0x040007A1 RID: 1953
			ErrorInstanceObjectConatinsNullIdentity = 3344562832U,
			// Token: 0x040007A2 RID: 1954
			ErrorAdminLoginUsingAppPassword = 2895423986U,
			// Token: 0x040007A3 RID: 1955
			ErrorCannotFindMailboxToLogonPublicStore = 2403641406U,
			// Token: 0x040007A4 RID: 1956
			WorkUnitStatusInProgress = 3498243205U,
			// Token: 0x040007A5 RID: 1957
			ErrorObjectHasValidationErrors = 19923723U,
			// Token: 0x040007A6 RID: 1958
			ErrorNoProvisioningHandlerAvailable = 452500353U,
			// Token: 0x040007A7 RID: 1959
			WarningUnlicensedMailbox = 1811096880U,
			// Token: 0x040007A8 RID: 1960
			WorkUnitStatusFailed = 767622998U,
			// Token: 0x040007A9 RID: 1961
			VerboseInitializeRunspaceServerSettingsRemote = 1306006239U,
			// Token: 0x040007AA RID: 1962
			ExceptionTaskInconsistent = 585695277U,
			// Token: 0x040007AB RID: 1963
			WarningForceMessage = 1520502444U,
			// Token: 0x040007AC RID: 1964
			WarningCannotSetPrimarySmtpAddressWhenEapEnabled = 4140272390U,
			// Token: 0x040007AD RID: 1965
			ExecutingUserNameIsMissing = 3204373538U,
			// Token: 0x040007AE RID: 1966
			ExceptionNoChangesSpecified = 2188568225U,
			// Token: 0x040007AF RID: 1967
			ExceptionTaskAlreadyInitialized = 3805201920U,
			// Token: 0x040007B0 RID: 1968
			ParameterValueTooLarge = 2951618283U,
			// Token: 0x040007B1 RID: 1969
			ErrorNotSupportSingletonWildcard = 3718372071U,
			// Token: 0x040007B2 RID: 1970
			WorkUnitCollectionConfigurationSummary = 2784860353U,
			// Token: 0x040007B3 RID: 1971
			ExceptionMDACommandNotExecuting = 143513383U,
			// Token: 0x040007B4 RID: 1972
			ErrorRemotePowershellConnectionBlocked = 1958023215U,
			// Token: 0x040007B5 RID: 1973
			LogExecutionFailed = 2725322457U,
			// Token: 0x040007B6 RID: 1974
			VerboseLbNoOabVDirReturned = 3624978883U,
			// Token: 0x040007B7 RID: 1975
			VerboseLbEnterSiteMailboxEnterprise = 2158269158U,
			// Token: 0x040007B8 RID: 1976
			HierarchicalIdentityNullOrEmpty = 2890992798U,
			// Token: 0x040007B9 RID: 1977
			ExceptionObjectAlreadyExists = 1268762784U,
			// Token: 0x040007BA RID: 1978
			ErrorRemotePowerShellNotEnabled = 2576106929U,
			// Token: 0x040007BB RID: 1979
			ErrorRbacConfigurationNotSupportedSharedConfiguration = 298822364U,
			// Token: 0x040007BC RID: 1980
			UnknownEnumValue = 1356455742U,
			// Token: 0x040007BD RID: 1981
			ErrorOperationRequiresManager = 439815616U,
			// Token: 0x040007BE RID: 1982
			ErrorOrganizationWildcard = 2916171677U,
			// Token: 0x040007BF RID: 1983
			ErrorDelegatedUserNotInOrg = 687978330U,
			// Token: 0x040007C0 RID: 1984
			VerboseSerializationDataNotExist = 1837325848U,
			// Token: 0x040007C1 RID: 1985
			ErrorNoAvailablePublicFolderDatabase = 1983570122U,
			// Token: 0x040007C2 RID: 1986
			SessionExpiredException = 1655423524U,
			// Token: 0x040007C3 RID: 1987
			HierarchicalIdentityStartsOrEndsWithBackslash = 2661346553U,
			// Token: 0x040007C4 RID: 1988
			VerboseInitializeRunspaceServerSettingsLocal = 2859768776U,
			// Token: 0x040007C5 RID: 1989
			ExceptionMDACommandStillExecuting = 1551194332U,
			// Token: 0x040007C6 RID: 1990
			WorkUnitError = 1960180119U,
			// Token: 0x040007C7 RID: 1991
			VerboseNoSource = 4205209694U,
			// Token: 0x040007C8 RID: 1992
			ErrorFilteringOnlyUserLogin = 797535012U,
			// Token: 0x040007C9 RID: 1993
			ExceptionNullInstanceParameter = 1654901580U,
			// Token: 0x040007CA RID: 1994
			VerboseLbCreateNewExRpcAdmin = 498095919U,
			// Token: 0x040007CB RID: 1995
			ErrorCannotDiscoverDefaultOrganizationUnitForRecipient = 2137526570U,
			// Token: 0x040007CC RID: 1996
			CommandSucceeded = 3713958116U,
			// Token: 0x040007CD RID: 1997
			ErrorUninitializedParameter = 84680862U,
			// Token: 0x040007CE RID: 1998
			ErrorNotAllowedForPartnerAccess = 2037166858U,
			// Token: 0x040007CF RID: 1999
			ExceptionTaskNotExecuted = 1859189834U,
			// Token: 0x040007D0 RID: 2000
			ErrorInvalidResultSize = 3245318277U,
			// Token: 0x040007D1 RID: 2001
			VerboseLbDeletedServer = 201309884U,
			// Token: 0x040007D2 RID: 2002
			TaskCompleted = 4282198592U,
			// Token: 0x040007D3 RID: 2003
			ErrorMaxTenantPSConnectionLimitNotResolved = 1234513041U,
			// Token: 0x040007D4 RID: 2004
			InvalidCharacterInComponentPartOfHierarchicalIdentity = 1421372901U,
			// Token: 0x040007D5 RID: 2005
			ExceptionReadOnlyPropertyBag = 838517570U,
			// Token: 0x040007D6 RID: 2006
			VerboseLbTryRetrieveDatabaseStatus = 3781767156U,
			// Token: 0x040007D7 RID: 2007
			ErrorIgnoreDefaultScopeAndDCSetTogether = 3216817101U,
			// Token: 0x040007D8 RID: 2008
			ExceptionGettingConditionObject = 3519173187U,
			// Token: 0x040007D9 RID: 2009
			EnabledString = 4160063394U,
			// Token: 0x040007DA RID: 2010
			WorkUnitWarning = 3507110937U,
			// Token: 0x040007DB RID: 2011
			VerboseLbServerDownSoMarkDatabaseDown = 1170623981U,
			// Token: 0x040007DC RID: 2012
			BinaryDataStakeHodler = 3828427927U,
			// Token: 0x040007DD RID: 2013
			ErrorWriteOpOnDehydratedTenant = 1221524445U,
			// Token: 0x040007DE RID: 2014
			ExceptionMDACommandAlreadyExecuting = 3394388186U,
			// Token: 0x040007DF RID: 2015
			SipCultureInfoArgumentCheckFailure = 2174831997U,
			// Token: 0x040007E0 RID: 2016
			ConsecutiveWholeWildcardNamePartsInHierarchicalIdentity = 577240232U,
			// Token: 0x040007E1 RID: 2017
			ErrorMapiPublicFolderTreeNotUnique = 324189978U,
			// Token: 0x040007E2 RID: 2018
			WarningMoreResultsAvailable = 3002944702U,
			// Token: 0x040007E3 RID: 2019
			ErrorOperationOnInvalidObject = 3806422804U,
			// Token: 0x040007E4 RID: 2020
			VerboseInitializeRunspaceServerSettingsAdam = 4253079958U,
			// Token: 0x040007E5 RID: 2021
			VerboseLbNoAvailableDatabase = 3746749985U,
			// Token: 0x040007E6 RID: 2022
			PswsManagementAutomationAssemblyLoadError = 3026495307U,
			// Token: 0x040007E7 RID: 2023
			LogRollbackFailed = 2439453065U,
			// Token: 0x040007E8 RID: 2024
			NullOrEmptyNamePartsInHierarchicalIdentity = 2502725106U,
			// Token: 0x040007E9 RID: 2025
			ErrorCloseServiceHandle = 328156873U,
			// Token: 0x040007EA RID: 2026
			WorkUnitStatusCompleted = 3026665436U,
			// Token: 0x040007EB RID: 2027
			ErrorUrlInValid = 2267892936U,
			// Token: 0x040007EC RID: 2028
			ErrorNoMailboxUserInTheForest = 2717050851U,
			// Token: 0x040007ED RID: 2029
			ServerNotAvailable = 4074275529U,
			// Token: 0x040007EE RID: 2030
			HelpUrlHeaderText = 292337732U,
			// Token: 0x040007EF RID: 2031
			VersionMismatchDuringCreateRemoteRunspace = 2496835620U,
			// Token: 0x040007F0 RID: 2032
			ErrorCannotOpenServiceControllerManager = 2667845807U,
			// Token: 0x040007F1 RID: 2033
			VerboseLbNoEligibleServers = 2472100876U,
			// Token: 0x040007F2 RID: 2034
			VerboseLbDatabaseFound = 3699804131U,
			// Token: 0x040007F3 RID: 2035
			VerboseADObjectNoChangedProperties = 3296071226U,
			// Token: 0x040007F4 RID: 2036
			VerbosePopulateScopeSet = 2442074922U,
			// Token: 0x040007F5 RID: 2037
			ErrorCertificateDenied = 2293662344U,
			// Token: 0x040007F6 RID: 2038
			ErrorCannotResolvePUIDToWindowsIdentity = 4083645591U,
			// Token: 0x040007F7 RID: 2039
			ErrorMissOrganization = 2468805291U,
			// Token: 0x040007F8 RID: 2040
			ExceptionMDAConnectionAlreadyOpened = 1891802266U,
			// Token: 0x040007F9 RID: 2041
			ExceptionObjectStillExists = 982491582U,
			// Token: 0x040007FA RID: 2042
			ExceptionMDAConnectionMustBeOpened = 3844753652U,
			// Token: 0x040007FB RID: 2043
			WriteErrorMessage = 1602649260U,
			// Token: 0x040007FC RID: 2044
			GenericConditionFailure = 4100583810U,
			// Token: 0x040007FD RID: 2045
			VerboseLbExRpcAdminExists = 4285414215U,
			// Token: 0x040007FE RID: 2046
			DisabledString = 325596373U,
			// Token: 0x040007FF RID: 2047
			ExceptionRemoveNoneExistenceObject = 2781337548U,
			// Token: 0x04000800 RID: 2048
			LogErrorPrefix = 492587358U,
			// Token: 0x04000801 RID: 2049
			ErrorMapiPublicFolderTreeNotFound = 1558360907U
		}

		// Token: 0x0200029C RID: 668
		private enum ParamIDs
		{
			// Token: 0x04000803 RID: 2051
			LoadingRole,
			// Token: 0x04000804 RID: 2052
			LookupUserAsSAMAccount,
			// Token: 0x04000805 RID: 2053
			VerboseLbOABOwnedByServer,
			// Token: 0x04000806 RID: 2054
			VerboseLbNoServerForDatabaseException,
			// Token: 0x04000807 RID: 2055
			WrongTypeMailboxUser,
			// Token: 0x04000808 RID: 2056
			VerboseRequestFilterInGetTask,
			// Token: 0x04000809 RID: 2057
			CannotFindClassFactoryInAgentAssembly,
			// Token: 0x0400080A RID: 2058
			ErrorInvalidStatePartnerOrgNotNull,
			// Token: 0x0400080B RID: 2059
			LoadingScopeErrorText,
			// Token: 0x0400080C RID: 2060
			VerboseLbOABIsCurrentlyOnServer,
			// Token: 0x0400080D RID: 2061
			AgentAssemblyDuplicateFound,
			// Token: 0x0400080E RID: 2062
			LogHelpUrl,
			// Token: 0x0400080F RID: 2063
			NoRoleEntriesFound,
			// Token: 0x04000810 RID: 2064
			VerboseLbPermanentException,
			// Token: 0x04000811 RID: 2065
			ErrorIncompleteDCPublicFolderIdParameter,
			// Token: 0x04000812 RID: 2066
			ErrorInvalidServerName,
			// Token: 0x04000813 RID: 2067
			VerboseLbNoAvailableE15Database,
			// Token: 0x04000814 RID: 2068
			MutuallyExclusiveArguments,
			// Token: 0x04000815 RID: 2069
			LogResolverInstantiated,
			// Token: 0x04000816 RID: 2070
			ProvisionDefaultProperties,
			// Token: 0x04000817 RID: 2071
			VerboseReadADObject,
			// Token: 0x04000818 RID: 2072
			ResourceLoadDelayNotEnforcedMaxThreadsExceeded,
			// Token: 0x04000819 RID: 2073
			ErrorNotAcceptedDomain,
			// Token: 0x0400081A RID: 2074
			SortOrderFormatException,
			// Token: 0x0400081B RID: 2075
			LoadingScope,
			// Token: 0x0400081C RID: 2076
			ExceptionObjectNotFound,
			// Token: 0x0400081D RID: 2077
			WrongTypeMailboxOrMailUser,
			// Token: 0x0400081E RID: 2078
			ErrorPolicyUserOrSecurityGroupNotFound,
			// Token: 0x0400081F RID: 2079
			ExceptionSetupRegkeyMissing,
			// Token: 0x04000820 RID: 2080
			PswsDeserializationError,
			// Token: 0x04000821 RID: 2081
			OnbehalfOf,
			// Token: 0x04000822 RID: 2082
			ErrorIncompletePublicFolderIdParameter,
			// Token: 0x04000823 RID: 2083
			VerboseFailedToReadFromDC,
			// Token: 0x04000824 RID: 2084
			ErrorNotMailboxServer,
			// Token: 0x04000825 RID: 2085
			ExceptionNoConversion,
			// Token: 0x04000826 RID: 2086
			PropertyProvisioned,
			// Token: 0x04000827 RID: 2087
			VerboseTaskReadDataObject,
			// Token: 0x04000828 RID: 2088
			UnExpectedElement,
			// Token: 0x04000829 RID: 2089
			ExceptionLegacyObjects,
			// Token: 0x0400082A RID: 2090
			ExceptionObjectAmbiguous,
			// Token: 0x0400082B RID: 2091
			VerboseLbInitialProvisioningDatabaseExcluded,
			// Token: 0x0400082C RID: 2092
			ErrorMaxTenantPSConnectionLimit,
			// Token: 0x0400082D RID: 2093
			ProvisioningPreInternalProcessRecord,
			// Token: 0x0400082E RID: 2094
			VerboseLbDatabase,
			// Token: 0x0400082F RID: 2095
			VerboseSourceFromDC,
			// Token: 0x04000830 RID: 2096
			VerboseLbIsDatabaseLocal,
			// Token: 0x04000831 RID: 2097
			ErrorOrgOutOfPartnerScope,
			// Token: 0x04000832 RID: 2098
			ServiceAlreadyNotInstalled,
			// Token: 0x04000833 RID: 2099
			WarningCmdletTarpittingByResourceLoad,
			// Token: 0x04000834 RID: 2100
			ErrorManagementObjectNotFoundByType,
			// Token: 0x04000835 RID: 2101
			VerboseLbSitesAreNotBalanced,
			// Token: 0x04000836 RID: 2102
			ErrorUserNotFound,
			// Token: 0x04000837 RID: 2103
			PswsResponseChildElementNotExisingError,
			// Token: 0x04000838 RID: 2104
			VerboseLbReturningServer,
			// Token: 0x04000839 RID: 2105
			ErrorConfigurationUnitNotFound,
			// Token: 0x0400083A RID: 2106
			PswsRequestException,
			// Token: 0x0400083B RID: 2107
			ErrorAddressListNotFound,
			// Token: 0x0400083C RID: 2108
			ErrorNoPartnerScopes,
			// Token: 0x0400083D RID: 2109
			VerboseSkipObject,
			// Token: 0x0400083E RID: 2110
			ErrorPolicyUserOrSecurityGroupNotUnique,
			// Token: 0x0400083F RID: 2111
			ErrorParentHasNewerVersion,
			// Token: 0x04000840 RID: 2112
			VerboseAdminSessionSettingsUserConfigDC,
			// Token: 0x04000841 RID: 2113
			LogConditionFailed,
			// Token: 0x04000842 RID: 2114
			LogFunctionExit,
			// Token: 0x04000843 RID: 2115
			WrongTypeMailboxRecipient,
			// Token: 0x04000844 RID: 2116
			WrongTypeUser,
			// Token: 0x04000845 RID: 2117
			WillUninstallInstalledService,
			// Token: 0x04000846 RID: 2118
			ExceptionMissingDetailSchemaFile,
			// Token: 0x04000847 RID: 2119
			MonitoringPerfomanceCounterString,
			// Token: 0x04000848 RID: 2120
			VerboseAdminSessionSettingsUserDCs,
			// Token: 0x04000849 RID: 2121
			WarningDefaultResultSizeReached,
			// Token: 0x0400084A RID: 2122
			UnknownAuditManagerType,
			// Token: 0x0400084B RID: 2123
			ExceptionRoleNotFoundObjects,
			// Token: 0x0400084C RID: 2124
			LogTaskCandidate,
			// Token: 0x0400084D RID: 2125
			VerboseTaskProcessingObject,
			// Token: 0x0400084E RID: 2126
			VerboseRereadADObject,
			// Token: 0x0400084F RID: 2127
			InvocationExceptionDescriptionWithoutError,
			// Token: 0x04000850 RID: 2128
			CouldntFindClassFactoryInAssembly,
			// Token: 0x04000851 RID: 2129
			VerboseAdminSessionSettingsDefaultScope,
			// Token: 0x04000852 RID: 2130
			WarningCannotWriteToEventLog,
			// Token: 0x04000853 RID: 2131
			InvalidAttribute,
			// Token: 0x04000854 RID: 2132
			ExceptionMismatchedConfigObjectType,
			// Token: 0x04000855 RID: 2133
			VerboseSource,
			// Token: 0x04000856 RID: 2134
			VerboseLbRetryableException,
			// Token: 0x04000857 RID: 2135
			ErrorPartnerApplicationWithoutLinkedAccount,
			// Token: 0x04000858 RID: 2136
			ErrorNoAvailablePublicFolderDatabaseInDatacenter,
			// Token: 0x04000859 RID: 2137
			NoRequiredRole,
			// Token: 0x0400085A RID: 2138
			ErrorOrganizationNotUnique,
			// Token: 0x0400085B RID: 2139
			WrongTypeUserContactComputer,
			// Token: 0x0400085C RID: 2140
			ErrorOperationTarpitting,
			// Token: 0x0400085D RID: 2141
			ConfigObjectAmbiguous,
			// Token: 0x0400085E RID: 2142
			VerboseLbDatabaseIsNotOnline,
			// Token: 0x0400085F RID: 2143
			InvalidNegativeValue,
			// Token: 0x04000860 RID: 2144
			ErrorNotUserMailboxCanLogonPFDatabase,
			// Token: 0x04000861 RID: 2145
			VerboseLbRemoteSiteDatabaseReturned,
			// Token: 0x04000862 RID: 2146
			RBACContextParserException,
			// Token: 0x04000863 RID: 2147
			ErrorCannotOpenService,
			// Token: 0x04000864 RID: 2148
			ExArgumentNullException,
			// Token: 0x04000865 RID: 2149
			WarningWindowsEmailAddressTooLong,
			// Token: 0x04000866 RID: 2150
			PswsResponseElementNotExisingError,
			// Token: 0x04000867 RID: 2151
			NoRoleEntriesWithParametersFound,
			// Token: 0x04000868 RID: 2152
			VerbosePostConditions,
			// Token: 0x04000869 RID: 2153
			ErrorProvisioningValidation,
			// Token: 0x0400086A RID: 2154
			UnhandledErrorMessage,
			// Token: 0x0400086B RID: 2155
			WrongTypeMailContact,
			// Token: 0x0400086C RID: 2156
			PiiRedactionInitializationFailed,
			// Token: 0x0400086D RID: 2157
			VerboseAdminSessionSettingsViewForest,
			// Token: 0x0400086E RID: 2158
			VerboseRemovingRoleAssignment,
			// Token: 0x0400086F RID: 2159
			CommandNotFoundError,
			// Token: 0x04000870 RID: 2160
			WrongTypeMailPublicFolder,
			// Token: 0x04000871 RID: 2161
			OverallElapsedTimeDescription,
			// Token: 0x04000872 RID: 2162
			ExceptionMDACommandExcutionError,
			// Token: 0x04000873 RID: 2163
			ErrorRecipientPropertyValueAlreadybeUsed,
			// Token: 0x04000874 RID: 2164
			ErrorManagementObjectNotFound,
			// Token: 0x04000875 RID: 2165
			ErrorInvalidMailboxStoreObjectIdentity,
			// Token: 0x04000876 RID: 2166
			ErrorCmdletProxy,
			// Token: 0x04000877 RID: 2167
			WarningCouldNotRemoveRoleAssignment,
			// Token: 0x04000878 RID: 2168
			VerboseDeleteObject,
			// Token: 0x04000879 RID: 2169
			VerboseCmdletProxiedToAnotherServer,
			// Token: 0x0400087A RID: 2170
			ExceptionInvalidDatabaseLegacyDnFormat,
			// Token: 0x0400087B RID: 2171
			LogFunctionEnter,
			// Token: 0x0400087C RID: 2172
			VerboseAdminSessionSettingsUserGlobalCatalog,
			// Token: 0x0400087D RID: 2173
			CouldNotDeterimineServiceInstanceException,
			// Token: 0x0400087E RID: 2174
			ErrorInvalidParameterFormat,
			// Token: 0x0400087F RID: 2175
			VerboseAdminSessionSettingsUserAFGlobalCatalog,
			// Token: 0x04000880 RID: 2176
			WrongTypeSecurityPrincipal,
			// Token: 0x04000881 RID: 2177
			ErrorInvalidGlobalAddressListIdentity,
			// Token: 0x04000882 RID: 2178
			WarningTaskRetried,
			// Token: 0x04000883 RID: 2179
			AssemblyFileNotFound,
			// Token: 0x04000884 RID: 2180
			VerboseLbDatabaseContainer,
			// Token: 0x04000885 RID: 2181
			ErrorProxyAddressAlreadyExists,
			// Token: 0x04000886 RID: 2182
			VerboseCannotGetRemoteServiceUriForUser,
			// Token: 0x04000887 RID: 2183
			VerboseAdminSessionSettingsUserAFConfigDC,
			// Token: 0x04000888 RID: 2184
			VerboseLbCountOfOABRecordsOwnedByServer,
			// Token: 0x04000889 RID: 2185
			PswsSerializationError,
			// Token: 0x0400088A RID: 2186
			CouldNotStartService,
			// Token: 0x0400088B RID: 2187
			VerboseTaskGetDataObjects,
			// Token: 0x0400088C RID: 2188
			ErrorPublicFolderMailDisabled,
			// Token: 0x0400088D RID: 2189
			VerboseLbIsLocalSiteNotEnoughInformation,
			// Token: 0x0400088E RID: 2190
			ErrorRecipientNotFound,
			// Token: 0x0400088F RID: 2191
			VerboseAdminSessionSettingsRecipientViewRoot,
			// Token: 0x04000890 RID: 2192
			VerboseLbFoundOabVDir,
			// Token: 0x04000891 RID: 2193
			ErrorManagementObjectNotFoundWithSource,
			// Token: 0x04000892 RID: 2194
			MicroDelayNotEnforcedMaxThreadsExceeded,
			// Token: 0x04000893 RID: 2195
			ServiceStopFailure,
			// Token: 0x04000894 RID: 2196
			ErrorRecipientPropertyValueAlreadyExists,
			// Token: 0x04000895 RID: 2197
			VerboseTaskFindDataObjectsInAL,
			// Token: 0x04000896 RID: 2198
			MultipleDefaultMailboxPlansFound,
			// Token: 0x04000897 RID: 2199
			ElementMustNotHaveAttributes,
			// Token: 0x04000898 RID: 2200
			WarningTaskModuleSkipped,
			// Token: 0x04000899 RID: 2201
			ErrorIsOutofDatabaseScope,
			// Token: 0x0400089A RID: 2202
			ExceptionInvalidTaskType,
			// Token: 0x0400089B RID: 2203
			WorkUnitCollectionStatus,
			// Token: 0x0400089C RID: 2204
			ErrorAddressListNotUnique,
			// Token: 0x0400089D RID: 2205
			VerboseTaskParameterLoggingFailed,
			// Token: 0x0400089E RID: 2206
			ErrorConversionFailed,
			// Token: 0x0400089F RID: 2207
			ErrorRecipientNotUnique,
			// Token: 0x040008A0 RID: 2208
			ExceptionInvalidConfigObjectType,
			// Token: 0x040008A1 RID: 2209
			LogAutoResolving,
			// Token: 0x040008A2 RID: 2210
			VerboseInternalQueryFilterInGetTasks,
			// Token: 0x040008A3 RID: 2211
			PswsCmdletError,
			// Token: 0x040008A4 RID: 2212
			LogCheckpoint,
			// Token: 0x040008A5 RID: 2213
			ErrorOrganizationNotFound,
			// Token: 0x040008A6 RID: 2214
			ErrorGlobalAddressListNotFound,
			// Token: 0x040008A7 RID: 2215
			DependentArguments,
			// Token: 0x040008A8 RID: 2216
			ErrorParentNotFound,
			// Token: 0x040008A9 RID: 2217
			ErrorNotFoundWithReason,
			// Token: 0x040008AA RID: 2218
			WrongTypeUserContactGroupIdParameter,
			// Token: 0x040008AB RID: 2219
			VerboseAdminSessionSettingsGlobalCatalog,
			// Token: 0x040008AC RID: 2220
			ExceptionParameterRange,
			// Token: 0x040008AD RID: 2221
			ErrorUnsupportedValues,
			// Token: 0x040008AE RID: 2222
			LogServiceState,
			// Token: 0x040008AF RID: 2223
			ExceptionMDAInvalidConnectionString,
			// Token: 0x040008B0 RID: 2224
			ErrorParentNotFoundOnDomainController,
			// Token: 0x040008B1 RID: 2225
			ClassFactoryDoesNotImplementIProvisioningAgent,
			// Token: 0x040008B2 RID: 2226
			ErrorInvalidOrganizationalUnitDNFormat,
			// Token: 0x040008B3 RID: 2227
			VerboseDatabaseNotFound,
			// Token: 0x040008B4 RID: 2228
			VerboseRecipientTaskHelperGetOrgnization,
			// Token: 0x040008B5 RID: 2229
			NoRoleEntriesWithParameterFound,
			// Token: 0x040008B6 RID: 2230
			ErrorEmptyParameter,
			// Token: 0x040008B7 RID: 2231
			ErrorManagementObjectAmbiguous,
			// Token: 0x040008B8 RID: 2232
			MicroDelayInfo,
			// Token: 0x040008B9 RID: 2233
			WrongTypeRemoteMailbox,
			// Token: 0x040008BA RID: 2234
			WrongTypeRoleGroup,
			// Token: 0x040008BB RID: 2235
			LogServiceName,
			// Token: 0x040008BC RID: 2236
			ErrorGlobalAddressListNotUnique,
			// Token: 0x040008BD RID: 2237
			ErrorNoServerForPublicFolderDatabase,
			// Token: 0x040008BE RID: 2238
			VerboseADObjectChangedProperties,
			// Token: 0x040008BF RID: 2239
			PswsResponseIsnotXMLError,
			// Token: 0x040008C0 RID: 2240
			CheckIfUserIsASID,
			// Token: 0x040008C1 RID: 2241
			ExchangeSetupCannotResumeLog,
			// Token: 0x040008C2 RID: 2242
			VerboseAdminSessionSettingsAFConfigDC,
			// Token: 0x040008C3 RID: 2243
			VerboseTaskBeginProcessing,
			// Token: 0x040008C4 RID: 2244
			CouldNotStopService,
			// Token: 0x040008C5 RID: 2245
			LoadingLogonUserErrorText,
			// Token: 0x040008C6 RID: 2246
			CrossForestAccount,
			// Token: 0x040008C7 RID: 2247
			WrongTypeDistributionGroup,
			// Token: 0x040008C8 RID: 2248
			WrongTypeMailUser,
			// Token: 0x040008C9 RID: 2249
			ErrorRecipientNotInSameOrgWithDataObject,
			// Token: 0x040008CA RID: 2250
			ErrorMustWriteToRidMaster,
			// Token: 0x040008CB RID: 2251
			ServiceUninstallFailure,
			// Token: 0x040008CC RID: 2252
			ExceptionResolverConstructorMissing,
			// Token: 0x040008CD RID: 2253
			VerboseAdminSessionSettingsAFGlobalCatalog,
			// Token: 0x040008CE RID: 2254
			CannotResolveParentOrganization,
			// Token: 0x040008CF RID: 2255
			WrongTypeNonMailEnabledUser,
			// Token: 0x040008D0 RID: 2256
			WarningDuplicateOrganizationSpecified,
			// Token: 0x040008D1 RID: 2257
			ResourceLoadDelayInfo,
			// Token: 0x040008D2 RID: 2258
			VerboseLbBestServerSoFar,
			// Token: 0x040008D3 RID: 2259
			ErrorIsOutOfDatabaseScopeNoServerCheck,
			// Token: 0x040008D4 RID: 2260
			ErrorIsAcceptedDomain,
			// Token: 0x040008D5 RID: 2261
			ValidationRuleNotFound,
			// Token: 0x040008D6 RID: 2262
			ErrorInvalidUMHuntGroupIdentity,
			// Token: 0x040008D7 RID: 2263
			ErrorNoServersForDatabase,
			// Token: 0x040008D8 RID: 2264
			ProvisioningValidationErrors,
			// Token: 0x040008D9 RID: 2265
			ResubmitRequestDoesNotExist,
			// Token: 0x040008DA RID: 2266
			UserQuotaDelayInfo,
			// Token: 0x040008DB RID: 2267
			ErrorTaskWin32ExceptionVerbose,
			// Token: 0x040008DC RID: 2268
			ErrorSetServiceObjectSecurity,
			// Token: 0x040008DD RID: 2269
			ErrorQueryServiceObjectSecurity,
			// Token: 0x040008DE RID: 2270
			ErrorNoAvailablePublicFolderDatabaseOnServer,
			// Token: 0x040008DF RID: 2271
			LogConditionMatchingTypeMismacth,
			// Token: 0x040008E0 RID: 2272
			ConfigObjectNotFound,
			// Token: 0x040008E1 RID: 2273
			ExceptionLexError,
			// Token: 0x040008E2 RID: 2274
			VerboseADObjectNoChangedPropertiesWithId,
			// Token: 0x040008E3 RID: 2275
			VerboseLbCheckingDatabaseIsAllowedOnScope,
			// Token: 0x040008E4 RID: 2276
			InvalidElementValue,
			// Token: 0x040008E5 RID: 2277
			ErrorRoleAssignmentNotFound,
			// Token: 0x040008E6 RID: 2278
			WrongTypeMailboxPlan,
			// Token: 0x040008E7 RID: 2279
			WarningCmdletTarpittingByUserQuota,
			// Token: 0x040008E8 RID: 2280
			InvalidRBACContextType,
			// Token: 0x040008E9 RID: 2281
			VerboseLbOabVDirSelected,
			// Token: 0x040008EA RID: 2282
			MonitoringEventStringWithInstanceName,
			// Token: 0x040008EB RID: 2283
			NoRoleAssignmentsFound,
			// Token: 0x040008EC RID: 2284
			VerboseSaveChange,
			// Token: 0x040008ED RID: 2285
			WrongActiveSyncDeviceIdParameter,
			// Token: 0x040008EE RID: 2286
			ErrorDuplicateManagementObjectFound,
			// Token: 0x040008EF RID: 2287
			VerboseLbDatabaseNotFoundException,
			// Token: 0x040008F0 RID: 2288
			VerboseLbGetServerForActiveDatabaseCopy,
			// Token: 0x040008F1 RID: 2289
			ErrorOpenKeyDeniedForWrite,
			// Token: 0x040008F2 RID: 2290
			WarningCmdletMicroDelay,
			// Token: 0x040008F3 RID: 2291
			ErrorManagementObjectNotFoundWithSourceByType,
			// Token: 0x040008F4 RID: 2292
			ErrorInvalidParameterType,
			// Token: 0x040008F5 RID: 2293
			ExceptionCondition,
			// Token: 0x040008F6 RID: 2294
			ElapsedTimeDescription,
			// Token: 0x040008F7 RID: 2295
			ExceptionParseError,
			// Token: 0x040008F8 RID: 2296
			VerboseLbGeneralTrace,
			// Token: 0x040008F9 RID: 2297
			ErrorPublicFolderDatabaseIsNotMounted,
			// Token: 0x040008FA RID: 2298
			WarningForceMessageWithId,
			// Token: 0x040008FB RID: 2299
			ErrorPublicFolderGeneratingProxy,
			// Token: 0x040008FC RID: 2300
			ErrorMaxRunspacesLimit,
			// Token: 0x040008FD RID: 2301
			LogConditionMatchingPropertyMismatch,
			// Token: 0x040008FE RID: 2302
			HandlerThronwExceptionInOnComplete,
			// Token: 0x040008FF RID: 2303
			InvalidGuidParameter,
			// Token: 0x04000900 RID: 2304
			LogTaskExecutionPlan,
			// Token: 0x04000901 RID: 2305
			WarningCannotGetLocalServerFqdn,
			// Token: 0x04000902 RID: 2306
			ExArgumentOutOfRangeException,
			// Token: 0x04000903 RID: 2307
			ErrorRemoveNewerObject,
			// Token: 0x04000904 RID: 2308
			DelegatedAdminAccount,
			// Token: 0x04000905 RID: 2309
			ErrorInvalidHierarchicalIdentity,
			// Token: 0x04000906 RID: 2310
			ExceptionRollbackFailed,
			// Token: 0x04000907 RID: 2311
			VerboseTaskEndProcessing,
			// Token: 0x04000908 RID: 2312
			ErrorInvalidMailboxFolderIdentity,
			// Token: 0x04000909 RID: 2313
			ErrorFoundMultipleRootRole,
			// Token: 0x0400090A RID: 2314
			ImpersonationNotAllowed,
			// Token: 0x0400090B RID: 2315
			MonitoringEventString,
			// Token: 0x0400090C RID: 2316
			ErrorFailedToReadFromDC,
			// Token: 0x0400090D RID: 2317
			ErrorTenantMaxRunspacesTarpitting,
			// Token: 0x0400090E RID: 2318
			ForeignForestTrustFailedException,
			// Token: 0x0400090F RID: 2319
			WrongTypeUserContact,
			// Token: 0x04000910 RID: 2320
			ProvisioningUpdateAffectedObject,
			// Token: 0x04000911 RID: 2321
			LookupUserAsDomainUser,
			// Token: 0x04000912 RID: 2322
			MissingAttribute,
			// Token: 0x04000913 RID: 2323
			ErrorCannotFormatRecipient,
			// Token: 0x04000914 RID: 2324
			ErrorCannotGetPublicFolderDatabaseByLegacyDn,
			// Token: 0x04000915 RID: 2325
			VerboseRemovedRoleAssignment,
			// Token: 0x04000916 RID: 2326
			LoadingLogonUser,
			// Token: 0x04000917 RID: 2327
			ErrorObjectVersionChanged,
			// Token: 0x04000918 RID: 2328
			WrongTypeMailboxOrMailUserOrMailContact,
			// Token: 0x04000919 RID: 2329
			ErrorTaskWin32Exception,
			// Token: 0x0400091A RID: 2330
			ServiceNotInstalled,
			// Token: 0x0400091B RID: 2331
			PooledConnectionOpenTimeoutError,
			// Token: 0x0400091C RID: 2332
			VerboseLbAddingEligibleServer,
			// Token: 0x0400091D RID: 2333
			ErrorInvalidIdentity,
			// Token: 0x0400091E RID: 2334
			NonMigratedUserException,
			// Token: 0x0400091F RID: 2335
			InvalidPropertyName,
			// Token: 0x04000920 RID: 2336
			WrongTypeDynamicGroup,
			// Token: 0x04000921 RID: 2337
			LoadingRoleErrorText,
			// Token: 0x04000922 RID: 2338
			PowerShellTimeout,
			// Token: 0x04000923 RID: 2339
			VerboseLbDatabaseAndServerTry,
			// Token: 0x04000924 RID: 2340
			ErrorCannotResolveCertificate,
			// Token: 0x04000925 RID: 2341
			WrongTypeMailEnabledContact,
			// Token: 0x04000926 RID: 2342
			ExchangeSetupCannotCopyWatson,
			// Token: 0x04000927 RID: 2343
			VerboseApplyRusPolicyForRecipient,
			// Token: 0x04000928 RID: 2344
			NotInSameOrg,
			// Token: 0x04000929 RID: 2345
			WrongTypeLogonableObjectIdParameter,
			// Token: 0x0400092A RID: 2346
			ClashingIdentities,
			// Token: 0x0400092B RID: 2347
			ErrorMaxRunspacesTarpitting,
			// Token: 0x0400092C RID: 2348
			VerboseExecutingUserContext,
			// Token: 0x0400092D RID: 2349
			ExceptionSetupFileNotFound,
			// Token: 0x0400092E RID: 2350
			VerboseFailedToGetProxyServer,
			// Token: 0x0400092F RID: 2351
			ExceptionTaskContextConsistencyViolation,
			// Token: 0x04000930 RID: 2352
			LogPreconditionImmediate,
			// Token: 0x04000931 RID: 2353
			ErrorInvalidType,
			// Token: 0x04000932 RID: 2354
			VerboseCannotGetRemoteServerForUser,
			// Token: 0x04000933 RID: 2355
			ErrorNoReplicaOnServer,
			// Token: 0x04000934 RID: 2356
			ErrorUserNotUnique,
			// Token: 0x04000935 RID: 2357
			ErrorMaxConnectionLimit,
			// Token: 0x04000936 RID: 2358
			DuplicateExternalDirectoryObjectIdException,
			// Token: 0x04000937 RID: 2359
			InvalidElement,
			// Token: 0x04000938 RID: 2360
			VerbosePreConditions,
			// Token: 0x04000939 RID: 2361
			WrongTypeGroup,
			// Token: 0x0400093A RID: 2362
			VerboseSelectedRusServer,
			// Token: 0x0400093B RID: 2363
			VerboseLbDisposeExRpcAdmin,
			// Token: 0x0400093C RID: 2364
			ErrorRelativeDn,
			// Token: 0x0400093D RID: 2365
			InvalidAttributeValue,
			// Token: 0x0400093E RID: 2366
			PropertyIsAlreadyProvisioned,
			// Token: 0x0400093F RID: 2367
			ErrorInvalidModerator,
			// Token: 0x04000940 RID: 2368
			ErrorOrganizationalUnitNotUnique,
			// Token: 0x04000941 RID: 2369
			ExceptionMissingCreateInstance,
			// Token: 0x04000942 RID: 2370
			ExecutingUserPropertyNotFound,
			// Token: 0x04000943 RID: 2371
			CannotHaveLocalAccountException,
			// Token: 0x04000944 RID: 2372
			LoadingRoleAssignmentErrorText,
			// Token: 0x04000945 RID: 2373
			ExceptionMissingDataSourceManager,
			// Token: 0x04000946 RID: 2374
			ProvisioningExceptionMessage,
			// Token: 0x04000947 RID: 2375
			ErrorObjectHasValidationErrorsWithId,
			// Token: 0x04000948 RID: 2376
			ExceptionTaskExpansionTooDeep,
			// Token: 0x04000949 RID: 2377
			VerboseSourceFromGC,
			// Token: 0x0400094A RID: 2378
			VerboseCannotResolveSid,
			// Token: 0x0400094B RID: 2379
			ErrorCannotSendMailToPublicFolderMailbox,
			// Token: 0x0400094C RID: 2380
			ErrorCorruptedPartition,
			// Token: 0x0400094D RID: 2381
			VerboseADObjectChangedPropertiesWithId,
			// Token: 0x0400094E RID: 2382
			VerboseLbFoundMailboxServer,
			// Token: 0x0400094F RID: 2383
			ErrorSetTaskChangeRecipientType,
			// Token: 0x04000950 RID: 2384
			ServiceStartFailure,
			// Token: 0x04000951 RID: 2385
			ErrorCouldNotFindCorrespondingObject,
			// Token: 0x04000952 RID: 2386
			VerboseWriteResultSize,
			// Token: 0x04000953 RID: 2387
			VerboseTaskSpecifiedParameters,
			// Token: 0x04000954 RID: 2388
			VerboseLbDatabaseNotInUserScope,
			// Token: 0x04000955 RID: 2389
			ErrorNoServersAndOutofServerScope,
			// Token: 0x04000956 RID: 2390
			ErrorInvalidAddressListIdentity,
			// Token: 0x04000957 RID: 2391
			ErrorOrganizationalUnitNotFound,
			// Token: 0x04000958 RID: 2392
			VerboseAdminSessionSettingsConfigDC,
			// Token: 0x04000959 RID: 2393
			WrongTypeRecipientIdParamter,
			// Token: 0x0400095A RID: 2394
			ErrorOuOutOfOrganization,
			// Token: 0x0400095B RID: 2395
			InvocationExceptionDescription,
			// Token: 0x0400095C RID: 2396
			VerboseLbNetworkError,
			// Token: 0x0400095D RID: 2397
			UserNotSAMAccount,
			// Token: 0x0400095E RID: 2398
			VerboseADObjectChangedPropertiesWithDn,
			// Token: 0x0400095F RID: 2399
			ErrorChangeServiceConfig2,
			// Token: 0x04000960 RID: 2400
			InstantiatingHandlerForAgent,
			// Token: 0x04000961 RID: 2401
			LoadingRoleAssignment,
			// Token: 0x04000962 RID: 2402
			ErrorServerNotFound,
			// Token: 0x04000963 RID: 2403
			ErrorServerNotUnique,
			// Token: 0x04000964 RID: 2404
			PswsInvocationTimout,
			// Token: 0x04000965 RID: 2405
			VerboseAdminSessionSettingsDCs,
			// Token: 0x04000966 RID: 2406
			ClashingPriorities,
			// Token: 0x04000967 RID: 2407
			VerboseResolvedOrganization,
			// Token: 0x04000968 RID: 2408
			WrongTypeComputer,
			// Token: 0x04000969 RID: 2409
			VerboseTaskFindDataObjects,
			// Token: 0x0400096A RID: 2410
			ExceptionTypeNotFound,
			// Token: 0x0400096B RID: 2411
			VerboseFailedToDeserializePSObject,
			// Token: 0x0400096C RID: 2412
			WrongTypeGeneralMailboxIdParameter,
			// Token: 0x0400096D RID: 2413
			WrongTypeMailboxUserContact,
			// Token: 0x0400096E RID: 2414
			AgentAssemblyWithoutPathFound,
			// Token: 0x0400096F RID: 2415
			ConfirmSharedConfiguration,
			// Token: 0x04000970 RID: 2416
			UserQuotaDelayNotEnforcedMaxThreadsExceeded,
			// Token: 0x04000971 RID: 2417
			ErrorOrgNotFound,
			// Token: 0x04000972 RID: 2418
			MultipleHandlersForCmdlet,
			// Token: 0x04000973 RID: 2419
			ConditionNotInitialized,
			// Token: 0x04000974 RID: 2420
			VerboseLbOnlyOneEligibleServer,
			// Token: 0x04000975 RID: 2421
			ProvisioningBrokerInitializationFailed,
			// Token: 0x04000976 RID: 2422
			LogPreconditionDeferred,
			// Token: 0x04000977 RID: 2423
			VerboseAdminSessionSettings,
			// Token: 0x04000978 RID: 2424
			WrongTypeNonMailEnabledGroup,
			// Token: 0x04000979 RID: 2425
			ErrorIsOutofConfigWriteScope
		}
	}
}
