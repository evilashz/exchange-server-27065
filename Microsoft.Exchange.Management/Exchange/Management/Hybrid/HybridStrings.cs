using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001230 RID: 4656
	internal static class HybridStrings
	{
		// Token: 0x0600BB4E RID: 47950 RVA: 0x002AA0D4 File Offset: 0x002A82D4
		static HybridStrings()
		{
			HybridStrings.stringIDs.Add(2425718147U, "ErrorHybridUpgradedTo2013");
			HybridStrings.stringIDs.Add(270912202U, "ErrorMultipleFederationTrusts");
			HybridStrings.stringIDs.Add(820521406U, "ErrorHybridMustBeUpgraded");
			HybridStrings.stringIDs.Add(4267730599U, "ErrorNoFederatedDomainsOnTenant");
			HybridStrings.stringIDs.Add(1424719599U, "HybridErrorBothTransportServersNotSet");
			HybridStrings.stringIDs.Add(2925976073U, "ErrorNoHybridDomain");
			HybridStrings.stringIDs.Add(3187185748U, "HybridInfoObjectNotFound");
			HybridStrings.stringIDs.Add(2511569494U, "ErrorMultipleMatchingOrgRelationships");
			HybridStrings.stringIDs.Add(3431283964U, "ErrorOnPremUsingConsumerLiveID");
			HybridStrings.stringIDs.Add(4267028265U, "ErrorFederationIDNotProvisioned");
			HybridStrings.stringIDs.Add(4281839122U, "ErrorHybridClientAccessServersNotCleared");
			HybridStrings.stringIDs.Add(1430057223U, "ErrorNoTenantAcceptedDomains");
			HybridStrings.stringIDs.Add(2996915408U, "MRSProxyTaskName");
			HybridStrings.stringIDs.Add(458024348U, "InvalidOrganizationRelationship");
			HybridStrings.stringIDs.Add(786567629U, "HybridInfoValidateUnusedConfigurationAttributesAreCleared");
			HybridStrings.stringIDs.Add(423230028U, "HybridErrorOnlyOneAutoDiscoverDomainMayBeSet");
			HybridStrings.stringIDs.Add(2908580272U, "GlobalPrereqTaskName");
			HybridStrings.stringIDs.Add(127541045U, "HybridInfoExecutionComplete");
			HybridStrings.stringIDs.Add(3035608407U, "WarningUnableToCommunicateWithAutoDiscoveryEP");
			HybridStrings.stringIDs.Add(2863677737U, "ConfirmationMessageUpdateHybridConfiguration");
			HybridStrings.stringIDs.Add(2717467465U, "ErrorOrgRelNotFoundOnPrem");
			HybridStrings.stringIDs.Add(838311222U, "HybridInfoBasePrereqsFailed");
			HybridStrings.stringIDs.Add(876046568U, "ErrorNoOrganizationalUnitsFound");
			HybridStrings.stringIDs.Add(512075U, "IOCConfigurationTaskName");
			HybridStrings.stringIDs.Add(2570457594U, "ConfirmationMessageSetHybridConfiguration");
			HybridStrings.stringIDs.Add(2441367287U, "ReturnResultForHybridDetectionWasFalse");
			HybridStrings.stringIDs.Add(3815718049U, "ErrorIncompatibleServersDetected");
			HybridStrings.stringIDs.Add(2541891350U, "HybridErrorNoTlsCertificateNameSet");
			HybridStrings.stringIDs.Add(3364263621U, "ErrorHybridUpgradeNotAllTransportServersProperVersion");
			HybridStrings.stringIDs.Add(3426408323U, "HybridInfoVerifyTenantHasBeenUpgraded");
			HybridStrings.stringIDs.Add(3661239469U, "RecipientTaskName");
			HybridStrings.stringIDs.Add(3953578261U, "OrganizationRelationshipTaskName");
			HybridStrings.stringIDs.Add(573859146U, "HybridInfoUpdatingHybridConfigurationVersion");
			HybridStrings.stringIDs.Add(3251785458U, "ErrorAccountNamespace");
			HybridStrings.stringIDs.Add(2320637488U, "ErrorNoOnPremAcceptedDomains");
			HybridStrings.stringIDs.Add(2413435486U, "ErrorNoHybridDomains");
			HybridStrings.stringIDs.Add(888551196U, "HybridInfoPurePSObjectsNotSupported");
			HybridStrings.stringIDs.Add(2147167682U, "HybridInfoClearingUnusedHybridConfigurationProperties");
			HybridStrings.stringIDs.Add(1453693463U, "HybridFedInfoFallbackInfo");
			HybridStrings.stringIDs.Add(46137829U, "HybridUpgradeFrom14TaskName");
			HybridStrings.stringIDs.Add(2918546762U, "HybridInfoConnectedToTenant");
			HybridStrings.stringIDs.Add(3422685683U, "ErrorOrgRelNotFoundOnTenant");
			HybridStrings.stringIDs.Add(345297155U, "HybridEngineCheckingForUpgradeTenant");
			HybridStrings.stringIDs.Add(2844731914U, "HybridInfoRemovingUnnecessaryRemoteDomains");
			HybridStrings.stringIDs.Add(1876236381U, "TenantDetectionTaskName");
			HybridStrings.stringIDs.Add(144627946U, "HybridInfoVerifyTransportServers");
			HybridStrings.stringIDs.Add(430320558U, "ErrorNoOutboundConnector");
			HybridStrings.stringIDs.Add(3561336298U, "HybridErrorNoTransportServersSet");
			HybridStrings.stringIDs.Add(4039070452U, "HybridActivityEstablish");
			HybridStrings.stringIDs.Add(2473550644U, "HybridErrorNoSmartHostSet");
			HybridStrings.stringIDs.Add(2276570094U, "HybridInfoValidatingUnnecessaryRemoteDomainsAreRemoved");
			HybridStrings.stringIDs.Add(3952178692U, "WarningRedirectCU10HybridStandaloneConfiguration");
			HybridStrings.stringIDs.Add(2384567192U, "ErrorHybridNoCASWithEWSURL");
			HybridStrings.stringIDs.Add(383363188U, "ConfirmationMessageNewHybridConfiguration");
			HybridStrings.stringIDs.Add(1366871238U, "HybridErrorMixedTransportServersSet");
			HybridStrings.stringIDs.Add(1823864370U, "HybridConnectingToOnPrem");
			HybridStrings.stringIDs.Add(3524037511U, "RemoveHybidConfigurationConfirmation");
			HybridStrings.stringIDs.Add(3455711695U, "ErrorTenantUsingConsumerLiveID");
			HybridStrings.stringIDs.Add(856118049U, "HybridActivityConfigure");
			HybridStrings.stringIDs.Add(3567836490U, "HybridInfoNoNeedToUpgrade");
			HybridStrings.stringIDs.Add(1109246636U, "ErrorNoFederationTrustFound");
			HybridStrings.stringIDs.Add(2371375820U, "ErrorHybridConfigurationVersionNotUpdated");
			HybridStrings.stringIDs.Add(3910362397U, "ErrorHybridExternalIPAddressesNotCleared");
			HybridStrings.stringIDs.Add(1896684180U, "ErrorCASExternalUrlNotSet");
			HybridStrings.stringIDs.Add(2525757763U, "HybridInfoCheckForPermissionToUpgrade");
			HybridStrings.stringIDs.Add(1105710278U, "ErrorHybridOnPremisesOrganizationWasNotCreatedWithUpgradedConnectors");
			HybridStrings.stringIDs.Add(4077665459U, "ErrorNoInboundConnector");
			HybridStrings.stringIDs.Add(4171349911U, "ErrorHybridConfigurationAlreadyDefined");
			HybridStrings.stringIDs.Add(2951975549U, "MailFlowTaskName");
			HybridStrings.stringIDs.Add(969801930U, "ErrorHybridExternalIPAddressesRangeAddressesNotSupported");
			HybridStrings.stringIDs.Add(1250087182U, "HybridActivityCompleted");
			HybridStrings.stringIDs.Add(951022927U, "ErrorFederatedIdentifierDisabled");
			HybridStrings.stringIDs.Add(1869337625U, "HybridInfoConnectedToOnPrem");
			HybridStrings.stringIDs.Add(4016244689U, "HybridConnectingToTenant");
			HybridStrings.stringIDs.Add(2902086538U, "HybridUpgradePrompt");
			HybridStrings.stringIDs.Add(2366359160U, "ErrorHybridTenenatUpgradeRequired");
			HybridStrings.stringIDs.Add(3418341869U, "ErrorHybridExternalIPAddressesExceeded40Entries");
			HybridStrings.stringIDs.Add(3323479649U, "OnOffSettingsTaskName");
		}

		// Token: 0x17003AEE RID: 15086
		// (get) Token: 0x0600BB4F RID: 47951 RVA: 0x002AA728 File Offset: 0x002A8928
		public static LocalizedString ErrorHybridUpgradedTo2013
		{
			get
			{
				return new LocalizedString("ErrorHybridUpgradedTo2013", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB50 RID: 47952 RVA: 0x002AA740 File Offset: 0x002A8940
		public static LocalizedString ErrorSecureMailCertificateNotFound(string subject, string issuer, string server)
		{
			return new LocalizedString("ErrorSecureMailCertificateNotFound", HybridStrings.ResourceManager, new object[]
			{
				subject,
				issuer,
				server
			});
		}

		// Token: 0x17003AEF RID: 15087
		// (get) Token: 0x0600BB51 RID: 47953 RVA: 0x002AA770 File Offset: 0x002A8970
		public static LocalizedString ErrorMultipleFederationTrusts
		{
			get
			{
				return new LocalizedString("ErrorMultipleFederationTrusts", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF0 RID: 15088
		// (get) Token: 0x0600BB52 RID: 47954 RVA: 0x002AA787 File Offset: 0x002A8987
		public static LocalizedString ErrorHybridMustBeUpgraded
		{
			get
			{
				return new LocalizedString("ErrorHybridMustBeUpgraded", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF1 RID: 15089
		// (get) Token: 0x0600BB53 RID: 47955 RVA: 0x002AA79E File Offset: 0x002A899E
		public static LocalizedString ErrorNoFederatedDomainsOnTenant
		{
			get
			{
				return new LocalizedString("ErrorNoFederatedDomainsOnTenant", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF2 RID: 15090
		// (get) Token: 0x0600BB54 RID: 47956 RVA: 0x002AA7B5 File Offset: 0x002A89B5
		public static LocalizedString HybridErrorBothTransportServersNotSet
		{
			get
			{
				return new LocalizedString("HybridErrorBothTransportServersNotSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF3 RID: 15091
		// (get) Token: 0x0600BB55 RID: 47957 RVA: 0x002AA7CC File Offset: 0x002A89CC
		public static LocalizedString ErrorNoHybridDomain
		{
			get
			{
				return new LocalizedString("ErrorNoHybridDomain", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF4 RID: 15092
		// (get) Token: 0x0600BB56 RID: 47958 RVA: 0x002AA7E3 File Offset: 0x002A89E3
		public static LocalizedString HybridInfoObjectNotFound
		{
			get
			{
				return new LocalizedString("HybridInfoObjectNotFound", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB57 RID: 47959 RVA: 0x002AA7FC File Offset: 0x002A89FC
		public static LocalizedString ErrorCASExternalUrlMatchNotFound(string serverName, string subjectName)
		{
			return new LocalizedString("ErrorCASExternalUrlMatchNotFound", HybridStrings.ResourceManager, new object[]
			{
				serverName,
				subjectName
			});
		}

		// Token: 0x17003AF5 RID: 15093
		// (get) Token: 0x0600BB58 RID: 47960 RVA: 0x002AA828 File Offset: 0x002A8A28
		public static LocalizedString ErrorMultipleMatchingOrgRelationships
		{
			get
			{
				return new LocalizedString("ErrorMultipleMatchingOrgRelationships", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB59 RID: 47961 RVA: 0x002AA840 File Offset: 0x002A8A40
		public static LocalizedString ErrorTargetAutodiscoverEprNotFound(string domain)
		{
			return new LocalizedString("ErrorTargetAutodiscoverEprNotFound", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17003AF6 RID: 15094
		// (get) Token: 0x0600BB5A RID: 47962 RVA: 0x002AA868 File Offset: 0x002A8A68
		public static LocalizedString ErrorOnPremUsingConsumerLiveID
		{
			get
			{
				return new LocalizedString("ErrorOnPremUsingConsumerLiveID", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF7 RID: 15095
		// (get) Token: 0x0600BB5B RID: 47963 RVA: 0x002AA87F File Offset: 0x002A8A7F
		public static LocalizedString ErrorFederationIDNotProvisioned
		{
			get
			{
				return new LocalizedString("ErrorFederationIDNotProvisioned", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AF8 RID: 15096
		// (get) Token: 0x0600BB5C RID: 47964 RVA: 0x002AA896 File Offset: 0x002A8A96
		public static LocalizedString ErrorHybridClientAccessServersNotCleared
		{
			get
			{
				return new LocalizedString("ErrorHybridClientAccessServersNotCleared", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB5D RID: 47965 RVA: 0x002AA8B0 File Offset: 0x002A8AB0
		public static LocalizedString ErrorHybridConfigurationTooNew(string objectVersion, string currentVersion)
		{
			return new LocalizedString("ErrorHybridConfigurationTooNew", HybridStrings.ResourceManager, new object[]
			{
				objectVersion,
				currentVersion
			});
		}

		// Token: 0x17003AF9 RID: 15097
		// (get) Token: 0x0600BB5E RID: 47966 RVA: 0x002AA8DC File Offset: 0x002A8ADC
		public static LocalizedString ErrorNoTenantAcceptedDomains
		{
			get
			{
				return new LocalizedString("ErrorNoTenantAcceptedDomains", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AFA RID: 15098
		// (get) Token: 0x0600BB5F RID: 47967 RVA: 0x002AA8F3 File Offset: 0x002A8AF3
		public static LocalizedString MRSProxyTaskName
		{
			get
			{
				return new LocalizedString("MRSProxyTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AFB RID: 15099
		// (get) Token: 0x0600BB60 RID: 47968 RVA: 0x002AA90A File Offset: 0x002A8B0A
		public static LocalizedString InvalidOrganizationRelationship
		{
			get
			{
				return new LocalizedString("InvalidOrganizationRelationship", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB61 RID: 47969 RVA: 0x002AA924 File Offset: 0x002A8B24
		public static LocalizedString HybridErrorServerNotE14Edge(string server)
		{
			return new LocalizedString("HybridErrorServerNotE14Edge", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600BB62 RID: 47970 RVA: 0x002AA94C File Offset: 0x002A8B4C
		public static LocalizedString HybridErrorServerNotE14CAS(string server)
		{
			return new LocalizedString("HybridErrorServerNotE14CAS", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003AFC RID: 15100
		// (get) Token: 0x0600BB63 RID: 47971 RVA: 0x002AA974 File Offset: 0x002A8B74
		public static LocalizedString HybridInfoValidateUnusedConfigurationAttributesAreCleared
		{
			get
			{
				return new LocalizedString("HybridInfoValidateUnusedConfigurationAttributesAreCleared", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB64 RID: 47972 RVA: 0x002AA98C File Offset: 0x002A8B8C
		public static LocalizedString HybridCouldNotResolveServerException(string server, Exception e)
		{
			return new LocalizedString("HybridCouldNotResolveServerException", HybridStrings.ResourceManager, new object[]
			{
				server,
				e
			});
		}

		// Token: 0x17003AFD RID: 15101
		// (get) Token: 0x0600BB65 RID: 47973 RVA: 0x002AA9B8 File Offset: 0x002A8BB8
		public static LocalizedString HybridErrorOnlyOneAutoDiscoverDomainMayBeSet
		{
			get
			{
				return new LocalizedString("HybridErrorOnlyOneAutoDiscoverDomainMayBeSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB66 RID: 47974 RVA: 0x002AA9D0 File Offset: 0x002A8BD0
		public static LocalizedString HybridInfoOpeningRunspace(string uri)
		{
			return new LocalizedString("HybridInfoOpeningRunspace", HybridStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x0600BB67 RID: 47975 RVA: 0x002AA9F8 File Offset: 0x002A8BF8
		public static LocalizedString HybridErrorSendingTransportServerNotE15Hub(string server)
		{
			return new LocalizedString("HybridErrorSendingTransportServerNotE15Hub", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600BB68 RID: 47976 RVA: 0x002AAA20 File Offset: 0x002A8C20
		public static LocalizedString ErrorHybridTenantRemoteDomainNotRemoved(string remoteDomainName)
		{
			return new LocalizedString("ErrorHybridTenantRemoteDomainNotRemoved", HybridStrings.ResourceManager, new object[]
			{
				remoteDomainName
			});
		}

		// Token: 0x0600BB69 RID: 47977 RVA: 0x002AAA48 File Offset: 0x002A8C48
		public static LocalizedString WarningEdgeReceiveConnector(string server, string identity, string fqdn)
		{
			return new LocalizedString("WarningEdgeReceiveConnector", HybridStrings.ResourceManager, new object[]
			{
				server,
				identity,
				fqdn
			});
		}

		// Token: 0x17003AFE RID: 15102
		// (get) Token: 0x0600BB6A RID: 47978 RVA: 0x002AAA78 File Offset: 0x002A8C78
		public static LocalizedString GlobalPrereqTaskName
		{
			get
			{
				return new LocalizedString("GlobalPrereqTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003AFF RID: 15103
		// (get) Token: 0x0600BB6B RID: 47979 RVA: 0x002AAA8F File Offset: 0x002A8C8F
		public static LocalizedString HybridInfoExecutionComplete
		{
			get
			{
				return new LocalizedString("HybridInfoExecutionComplete", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB6C RID: 47980 RVA: 0x002AAAA8 File Offset: 0x002A8CA8
		public static LocalizedString HybridInfoTaskSubStepFinish(string taskName, string subStepName, bool result, double time)
		{
			return new LocalizedString("HybridInfoTaskSubStepFinish", HybridStrings.ResourceManager, new object[]
			{
				taskName,
				subStepName,
				result,
				time
			});
		}

		// Token: 0x17003B00 RID: 15104
		// (get) Token: 0x0600BB6D RID: 47981 RVA: 0x002AAAE6 File Offset: 0x002A8CE6
		public static LocalizedString WarningUnableToCommunicateWithAutoDiscoveryEP
		{
			get
			{
				return new LocalizedString("WarningUnableToCommunicateWithAutoDiscoveryEP", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB6E RID: 47982 RVA: 0x002AAB00 File Offset: 0x002A8D00
		public static LocalizedString ErrorHybridOnPremRemoteDomainNotRemoved(string remoteDomainName)
		{
			return new LocalizedString("ErrorHybridOnPremRemoteDomainNotRemoved", HybridStrings.ResourceManager, new object[]
			{
				remoteDomainName
			});
		}

		// Token: 0x17003B01 RID: 15105
		// (get) Token: 0x0600BB6F RID: 47983 RVA: 0x002AAB28 File Offset: 0x002A8D28
		public static LocalizedString ConfirmationMessageUpdateHybridConfiguration
		{
			get
			{
				return new LocalizedString("ConfirmationMessageUpdateHybridConfiguration", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B02 RID: 15106
		// (get) Token: 0x0600BB70 RID: 47984 RVA: 0x002AAB3F File Offset: 0x002A8D3F
		public static LocalizedString ErrorOrgRelNotFoundOnPrem
		{
			get
			{
				return new LocalizedString("ErrorOrgRelNotFoundOnPrem", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB71 RID: 47985 RVA: 0x002AAB58 File Offset: 0x002A8D58
		public static LocalizedString ErrorCASCertInvalidDate(string serverName, string thumbprint)
		{
			return new LocalizedString("ErrorCASCertInvalidDate", HybridStrings.ResourceManager, new object[]
			{
				serverName,
				thumbprint
			});
		}

		// Token: 0x0600BB72 RID: 47986 RVA: 0x002AAB84 File Offset: 0x002A8D84
		public static LocalizedString HybridCouldNotOpenRunspaceException(Exception e)
		{
			return new LocalizedString("HybridCouldNotOpenRunspaceException", HybridStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x17003B03 RID: 15107
		// (get) Token: 0x0600BB73 RID: 47987 RVA: 0x002AABAC File Offset: 0x002A8DAC
		public static LocalizedString HybridInfoBasePrereqsFailed
		{
			get
			{
				return new LocalizedString("HybridInfoBasePrereqsFailed", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB74 RID: 47988 RVA: 0x002AABC4 File Offset: 0x002A8DC4
		public static LocalizedString HybridFedInfoFallbackError(string domain)
		{
			return new LocalizedString("HybridFedInfoFallbackError", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BB75 RID: 47989 RVA: 0x002AABEC File Offset: 0x002A8DEC
		public static LocalizedString ErrorCASCertSelfSigned(string serverName, string thumbprint)
		{
			return new LocalizedString("ErrorCASCertSelfSigned", HybridStrings.ResourceManager, new object[]
			{
				serverName,
				thumbprint
			});
		}

		// Token: 0x0600BB76 RID: 47990 RVA: 0x002AAC18 File Offset: 0x002A8E18
		public static LocalizedString ErrorTargetApplicationUriNotFound(string domain)
		{
			return new LocalizedString("ErrorTargetApplicationUriNotFound", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17003B04 RID: 15108
		// (get) Token: 0x0600BB77 RID: 47991 RVA: 0x002AAC40 File Offset: 0x002A8E40
		public static LocalizedString ErrorNoOrganizationalUnitsFound
		{
			get
			{
				return new LocalizedString("ErrorNoOrganizationalUnitsFound", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB78 RID: 47992 RVA: 0x002AAC58 File Offset: 0x002A8E58
		public static LocalizedString ErrorInvalidSignupDomain(string domain, string tenantDomain)
		{
			return new LocalizedString("ErrorInvalidSignupDomain", HybridStrings.ResourceManager, new object[]
			{
				domain,
				tenantDomain
			});
		}

		// Token: 0x0600BB79 RID: 47993 RVA: 0x002AAC84 File Offset: 0x002A8E84
		public static LocalizedString ErrorHybridDomainNotAcceptedOnPrem(string domain)
		{
			return new LocalizedString("ErrorHybridDomainNotAcceptedOnPrem", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17003B05 RID: 15109
		// (get) Token: 0x0600BB7A RID: 47994 RVA: 0x002AACAC File Offset: 0x002A8EAC
		public static LocalizedString IOCConfigurationTaskName
		{
			get
			{
				return new LocalizedString("IOCConfigurationTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B06 RID: 15110
		// (get) Token: 0x0600BB7B RID: 47995 RVA: 0x002AACC3 File Offset: 0x002A8EC3
		public static LocalizedString ConfirmationMessageSetHybridConfiguration
		{
			get
			{
				return new LocalizedString("ConfirmationMessageSetHybridConfiguration", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB7C RID: 47996 RVA: 0x002AACDC File Offset: 0x002A8EDC
		public static LocalizedString ErrorCASRoleInvalid(string serverName)
		{
			return new LocalizedString("ErrorCASRoleInvalid", HybridStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17003B07 RID: 15111
		// (get) Token: 0x0600BB7D RID: 47997 RVA: 0x002AAD04 File Offset: 0x002A8F04
		public static LocalizedString ReturnResultForHybridDetectionWasFalse
		{
			get
			{
				return new LocalizedString("ReturnResultForHybridDetectionWasFalse", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B08 RID: 15112
		// (get) Token: 0x0600BB7E RID: 47998 RVA: 0x002AAD1B File Offset: 0x002A8F1B
		public static LocalizedString ErrorIncompatibleServersDetected
		{
			get
			{
				return new LocalizedString("ErrorIncompatibleServersDetected", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B09 RID: 15113
		// (get) Token: 0x0600BB7F RID: 47999 RVA: 0x002AAD32 File Offset: 0x002A8F32
		public static LocalizedString HybridErrorNoTlsCertificateNameSet
		{
			get
			{
				return new LocalizedString("HybridErrorNoTlsCertificateNameSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B0A RID: 15114
		// (get) Token: 0x0600BB80 RID: 48000 RVA: 0x002AAD49 File Offset: 0x002A8F49
		public static LocalizedString ErrorHybridUpgradeNotAllTransportServersProperVersion
		{
			get
			{
				return new LocalizedString("ErrorHybridUpgradeNotAllTransportServersProperVersion", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B0B RID: 15115
		// (get) Token: 0x0600BB81 RID: 48001 RVA: 0x002AAD60 File Offset: 0x002A8F60
		public static LocalizedString HybridInfoVerifyTenantHasBeenUpgraded
		{
			get
			{
				return new LocalizedString("HybridInfoVerifyTenantHasBeenUpgraded", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B0C RID: 15116
		// (get) Token: 0x0600BB82 RID: 48002 RVA: 0x002AAD77 File Offset: 0x002A8F77
		public static LocalizedString RecipientTaskName
		{
			get
			{
				return new LocalizedString("RecipientTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB83 RID: 48003 RVA: 0x002AAD90 File Offset: 0x002A8F90
		public static LocalizedString ErrorCmdletException(string cmdletName)
		{
			return new LocalizedString("ErrorCmdletException", HybridStrings.ResourceManager, new object[]
			{
				cmdletName
			});
		}

		// Token: 0x0600BB84 RID: 48004 RVA: 0x002AADB8 File Offset: 0x002A8FB8
		public static LocalizedString HybridCouldNotCreateTenantSessionException(Exception e)
		{
			return new LocalizedString("HybridCouldNotCreateTenantSessionException", HybridStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x17003B0D RID: 15117
		// (get) Token: 0x0600BB85 RID: 48005 RVA: 0x002AADE0 File Offset: 0x002A8FE0
		public static LocalizedString OrganizationRelationshipTaskName
		{
			get
			{
				return new LocalizedString("OrganizationRelationshipTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B0E RID: 15118
		// (get) Token: 0x0600BB86 RID: 48006 RVA: 0x002AADF7 File Offset: 0x002A8FF7
		public static LocalizedString HybridInfoUpdatingHybridConfigurationVersion
		{
			get
			{
				return new LocalizedString("HybridInfoUpdatingHybridConfigurationVersion", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B0F RID: 15119
		// (get) Token: 0x0600BB87 RID: 48007 RVA: 0x002AAE0E File Offset: 0x002A900E
		public static LocalizedString ErrorAccountNamespace
		{
			get
			{
				return new LocalizedString("ErrorAccountNamespace", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB88 RID: 48008 RVA: 0x002AAE28 File Offset: 0x002A9028
		public static LocalizedString ErrorOrgRelProvisionFailed(string domain)
		{
			return new LocalizedString("ErrorOrgRelProvisionFailed", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BB89 RID: 48009 RVA: 0x002AAE50 File Offset: 0x002A9050
		public static LocalizedString ErrorSignupDomain(string domain)
		{
			return new LocalizedString("ErrorSignupDomain", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17003B10 RID: 15120
		// (get) Token: 0x0600BB8A RID: 48010 RVA: 0x002AAE78 File Offset: 0x002A9078
		public static LocalizedString ErrorNoOnPremAcceptedDomains
		{
			get
			{
				return new LocalizedString("ErrorNoOnPremAcceptedDomains", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB8B RID: 48011 RVA: 0x002AAE90 File Offset: 0x002A9090
		public static LocalizedString HybridCouldNotCreateOnPremisesSessionException(Exception e)
		{
			return new LocalizedString("HybridCouldNotCreateOnPremisesSessionException", HybridStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x0600BB8C RID: 48012 RVA: 0x002AAEB8 File Offset: 0x002A90B8
		public static LocalizedString HybridErrorReceivingTransportServerNotE15FrontEnd(string server)
		{
			return new LocalizedString("HybridErrorReceivingTransportServerNotE15FrontEnd", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B11 RID: 15121
		// (get) Token: 0x0600BB8D RID: 48013 RVA: 0x002AAEE0 File Offset: 0x002A90E0
		public static LocalizedString ErrorNoHybridDomains
		{
			get
			{
				return new LocalizedString("ErrorNoHybridDomains", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B12 RID: 15122
		// (get) Token: 0x0600BB8E RID: 48014 RVA: 0x002AAEF7 File Offset: 0x002A90F7
		public static LocalizedString HybridInfoPurePSObjectsNotSupported
		{
			get
			{
				return new LocalizedString("HybridInfoPurePSObjectsNotSupported", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B13 RID: 15123
		// (get) Token: 0x0600BB8F RID: 48015 RVA: 0x002AAF0E File Offset: 0x002A910E
		public static LocalizedString HybridInfoClearingUnusedHybridConfigurationProperties
		{
			get
			{
				return new LocalizedString("HybridInfoClearingUnusedHybridConfigurationProperties", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB90 RID: 48016 RVA: 0x002AAF28 File Offset: 0x002A9128
		public static LocalizedString ErrorSecureMailCertificateNoSmtp(string server)
		{
			return new LocalizedString("ErrorSecureMailCertificateNoSmtp", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B14 RID: 15124
		// (get) Token: 0x0600BB91 RID: 48017 RVA: 0x002AAF50 File Offset: 0x002A9150
		public static LocalizedString HybridFedInfoFallbackInfo
		{
			get
			{
				return new LocalizedString("HybridFedInfoFallbackInfo", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B15 RID: 15125
		// (get) Token: 0x0600BB92 RID: 48018 RVA: 0x002AAF67 File Offset: 0x002A9167
		public static LocalizedString HybridUpgradeFrom14TaskName
		{
			get
			{
				return new LocalizedString("HybridUpgradeFrom14TaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB93 RID: 48019 RVA: 0x002AAF80 File Offset: 0x002A9180
		public static LocalizedString HybridInfoTaskLogTemplate(string taskName, string message)
		{
			return new LocalizedString("HybridInfoTaskLogTemplate", HybridStrings.ResourceManager, new object[]
			{
				taskName,
				message
			});
		}

		// Token: 0x17003B16 RID: 15126
		// (get) Token: 0x0600BB94 RID: 48020 RVA: 0x002AAFAC File Offset: 0x002A91AC
		public static LocalizedString HybridInfoConnectedToTenant
		{
			get
			{
				return new LocalizedString("HybridInfoConnectedToTenant", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B17 RID: 15127
		// (get) Token: 0x0600BB95 RID: 48021 RVA: 0x002AAFC3 File Offset: 0x002A91C3
		public static LocalizedString ErrorOrgRelNotFoundOnTenant
		{
			get
			{
				return new LocalizedString("ErrorOrgRelNotFoundOnTenant", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB96 RID: 48022 RVA: 0x002AAFDC File Offset: 0x002A91DC
		public static LocalizedString WarningTenantGetFedInfoFailed(string targetAutodiscoverEpr)
		{
			return new LocalizedString("WarningTenantGetFedInfoFailed", HybridStrings.ResourceManager, new object[]
			{
				targetAutodiscoverEpr
			});
		}

		// Token: 0x17003B18 RID: 15128
		// (get) Token: 0x0600BB97 RID: 48023 RVA: 0x002AB004 File Offset: 0x002A9204
		public static LocalizedString HybridEngineCheckingForUpgradeTenant
		{
			get
			{
				return new LocalizedString("HybridEngineCheckingForUpgradeTenant", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B19 RID: 15129
		// (get) Token: 0x0600BB98 RID: 48024 RVA: 0x002AB01B File Offset: 0x002A921B
		public static LocalizedString HybridInfoRemovingUnnecessaryRemoteDomains
		{
			get
			{
				return new LocalizedString("HybridInfoRemovingUnnecessaryRemoteDomains", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B1A RID: 15130
		// (get) Token: 0x0600BB99 RID: 48025 RVA: 0x002AB032 File Offset: 0x002A9232
		public static LocalizedString TenantDetectionTaskName
		{
			get
			{
				return new LocalizedString("TenantDetectionTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB9A RID: 48026 RVA: 0x002AB04C File Offset: 0x002A924C
		public static LocalizedString ErrorDefaultReceieveConnectorNotFound(string server)
		{
			return new LocalizedString("ErrorDefaultReceieveConnectorNotFound", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B1B RID: 15131
		// (get) Token: 0x0600BB9B RID: 48027 RVA: 0x002AB074 File Offset: 0x002A9274
		public static LocalizedString HybridInfoVerifyTransportServers
		{
			get
			{
				return new LocalizedString("HybridInfoVerifyTransportServers", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B1C RID: 15132
		// (get) Token: 0x0600BB9C RID: 48028 RVA: 0x002AB08B File Offset: 0x002A928B
		public static LocalizedString ErrorNoOutboundConnector
		{
			get
			{
				return new LocalizedString("ErrorNoOutboundConnector", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BB9D RID: 48029 RVA: 0x002AB0A4 File Offset: 0x002A92A4
		public static LocalizedString ErrorTaskExceptionTemplate(string subtask, string message)
		{
			return new LocalizedString("ErrorTaskExceptionTemplate", HybridStrings.ResourceManager, new object[]
			{
				subtask,
				message
			});
		}

		// Token: 0x17003B1D RID: 15133
		// (get) Token: 0x0600BB9E RID: 48030 RVA: 0x002AB0D0 File Offset: 0x002A92D0
		public static LocalizedString HybridErrorNoTransportServersSet
		{
			get
			{
				return new LocalizedString("HybridErrorNoTransportServersSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B1E RID: 15134
		// (get) Token: 0x0600BB9F RID: 48031 RVA: 0x002AB0E7 File Offset: 0x002A92E7
		public static LocalizedString HybridActivityEstablish
		{
			get
			{
				return new LocalizedString("HybridActivityEstablish", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B1F RID: 15135
		// (get) Token: 0x0600BBA0 RID: 48032 RVA: 0x002AB0FE File Offset: 0x002A92FE
		public static LocalizedString HybridErrorNoSmartHostSet
		{
			get
			{
				return new LocalizedString("HybridErrorNoSmartHostSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBA1 RID: 48033 RVA: 0x002AB118 File Offset: 0x002A9318
		public static LocalizedString ErrorSecureMailCertificateSelfSigned(string server)
		{
			return new LocalizedString("ErrorSecureMailCertificateSelfSigned", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600BBA2 RID: 48034 RVA: 0x002AB140 File Offset: 0x002A9340
		public static LocalizedString ErrorCoexistenceDomainNotAcceptedOnTenant(string domain)
		{
			return new LocalizedString("ErrorCoexistenceDomainNotAcceptedOnTenant", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BBA3 RID: 48035 RVA: 0x002AB168 File Offset: 0x002A9368
		public static LocalizedString ErrorFederationInfoNotFound(string hybridDomain)
		{
			return new LocalizedString("ErrorFederationInfoNotFound", HybridStrings.ResourceManager, new object[]
			{
				hybridDomain
			});
		}

		// Token: 0x17003B20 RID: 15136
		// (get) Token: 0x0600BBA4 RID: 48036 RVA: 0x002AB190 File Offset: 0x002A9390
		public static LocalizedString HybridInfoValidatingUnnecessaryRemoteDomainsAreRemoved
		{
			get
			{
				return new LocalizedString("HybridInfoValidatingUnnecessaryRemoteDomainsAreRemoved", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B21 RID: 15137
		// (get) Token: 0x0600BBA5 RID: 48037 RVA: 0x002AB1A7 File Offset: 0x002A93A7
		public static LocalizedString WarningRedirectCU10HybridStandaloneConfiguration
		{
			get
			{
				return new LocalizedString("WarningRedirectCU10HybridStandaloneConfiguration", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B22 RID: 15138
		// (get) Token: 0x0600BBA6 RID: 48038 RVA: 0x002AB1BE File Offset: 0x002A93BE
		public static LocalizedString ErrorHybridNoCASWithEWSURL
		{
			get
			{
				return new LocalizedString("ErrorHybridNoCASWithEWSURL", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B23 RID: 15139
		// (get) Token: 0x0600BBA7 RID: 48039 RVA: 0x002AB1D5 File Offset: 0x002A93D5
		public static LocalizedString ConfirmationMessageNewHybridConfiguration
		{
			get
			{
				return new LocalizedString("ConfirmationMessageNewHybridConfiguration", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B24 RID: 15140
		// (get) Token: 0x0600BBA8 RID: 48040 RVA: 0x002AB1EC File Offset: 0x002A93EC
		public static LocalizedString HybridErrorMixedTransportServersSet
		{
			get
			{
				return new LocalizedString("HybridErrorMixedTransportServersSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B25 RID: 15141
		// (get) Token: 0x0600BBA9 RID: 48041 RVA: 0x002AB203 File Offset: 0x002A9403
		public static LocalizedString HybridConnectingToOnPrem
		{
			get
			{
				return new LocalizedString("HybridConnectingToOnPrem", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBAA RID: 48042 RVA: 0x002AB21C File Offset: 0x002A941C
		public static LocalizedString HybridInfoGetObjectValue(string value, string identity, string command)
		{
			return new LocalizedString("HybridInfoGetObjectValue", HybridStrings.ResourceManager, new object[]
			{
				value,
				identity,
				command
			});
		}

		// Token: 0x0600BBAB RID: 48043 RVA: 0x002AB24C File Offset: 0x002A944C
		public static LocalizedString ErrorCASCertNotTrusted(string serverName, string thumbprint)
		{
			return new LocalizedString("ErrorCASCertNotTrusted", HybridStrings.ResourceManager, new object[]
			{
				serverName,
				thumbprint
			});
		}

		// Token: 0x0600BBAC RID: 48044 RVA: 0x002AB278 File Offset: 0x002A9478
		public static LocalizedString ExceptionUpdateHybridConfigurationFailedWithLog(string errMsg, string machineLogFileLocation, string localPathLogFileLocation)
		{
			return new LocalizedString("ExceptionUpdateHybridConfigurationFailedWithLog", HybridStrings.ResourceManager, new object[]
			{
				errMsg,
				machineLogFileLocation,
				localPathLogFileLocation
			});
		}

		// Token: 0x17003B26 RID: 15142
		// (get) Token: 0x0600BBAD RID: 48045 RVA: 0x002AB2A8 File Offset: 0x002A94A8
		public static LocalizedString RemoveHybidConfigurationConfirmation
		{
			get
			{
				return new LocalizedString("RemoveHybidConfigurationConfirmation", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBAE RID: 48046 RVA: 0x002AB2C0 File Offset: 0x002A94C0
		public static LocalizedString HybridInfoTotalCmdletTime(string session, double timeSeconds)
		{
			return new LocalizedString("HybridInfoTotalCmdletTime", HybridStrings.ResourceManager, new object[]
			{
				session,
				timeSeconds
			});
		}

		// Token: 0x17003B27 RID: 15143
		// (get) Token: 0x0600BBAF RID: 48047 RVA: 0x002AB2F1 File Offset: 0x002A94F1
		public static LocalizedString ErrorTenantUsingConsumerLiveID
		{
			get
			{
				return new LocalizedString("ErrorTenantUsingConsumerLiveID", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B28 RID: 15144
		// (get) Token: 0x0600BBB0 RID: 48048 RVA: 0x002AB308 File Offset: 0x002A9508
		public static LocalizedString HybridActivityConfigure
		{
			get
			{
				return new LocalizedString("HybridActivityConfigure", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBB1 RID: 48049 RVA: 0x002AB320 File Offset: 0x002A9520
		public static LocalizedString ErrorTooManyMatchingResults(string identity)
		{
			return new LocalizedString("ErrorTooManyMatchingResults", HybridStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003B29 RID: 15145
		// (get) Token: 0x0600BBB2 RID: 48050 RVA: 0x002AB348 File Offset: 0x002A9548
		public static LocalizedString HybridInfoNoNeedToUpgrade
		{
			get
			{
				return new LocalizedString("HybridInfoNoNeedToUpgrade", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBB3 RID: 48051 RVA: 0x002AB360 File Offset: 0x002A9560
		public static LocalizedString HybridErrorSendingTransportServerNotHub(string server)
		{
			return new LocalizedString("HybridErrorSendingTransportServerNotHub", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B2A RID: 15146
		// (get) Token: 0x0600BBB4 RID: 48052 RVA: 0x002AB388 File Offset: 0x002A9588
		public static LocalizedString ErrorNoFederationTrustFound
		{
			get
			{
				return new LocalizedString("ErrorNoFederationTrustFound", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B2B RID: 15147
		// (get) Token: 0x0600BBB5 RID: 48053 RVA: 0x002AB39F File Offset: 0x002A959F
		public static LocalizedString ErrorHybridConfigurationVersionNotUpdated
		{
			get
			{
				return new LocalizedString("ErrorHybridConfigurationVersionNotUpdated", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBB6 RID: 48054 RVA: 0x002AB3B8 File Offset: 0x002A95B8
		public static LocalizedString HybridInfoCmdletStart(string session, string cmdlet, string parameters)
		{
			return new LocalizedString("HybridInfoCmdletStart", HybridStrings.ResourceManager, new object[]
			{
				session,
				cmdlet,
				parameters
			});
		}

		// Token: 0x0600BBB7 RID: 48055 RVA: 0x002AB3E8 File Offset: 0x002A95E8
		public static LocalizedString ErrorHybridRegistryInvalidUri(string name)
		{
			return new LocalizedString("ErrorHybridRegistryInvalidUri", HybridStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17003B2C RID: 15148
		// (get) Token: 0x0600BBB8 RID: 48056 RVA: 0x002AB410 File Offset: 0x002A9610
		public static LocalizedString ErrorHybridExternalIPAddressesNotCleared
		{
			get
			{
				return new LocalizedString("ErrorHybridExternalIPAddressesNotCleared", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBB9 RID: 48057 RVA: 0x002AB428 File Offset: 0x002A9628
		public static LocalizedString ErrorDuplicateSendConnectorAddressSpace(string addressSpace)
		{
			return new LocalizedString("ErrorDuplicateSendConnectorAddressSpace", HybridStrings.ResourceManager, new object[]
			{
				addressSpace
			});
		}

		// Token: 0x17003B2D RID: 15149
		// (get) Token: 0x0600BBBA RID: 48058 RVA: 0x002AB450 File Offset: 0x002A9650
		public static LocalizedString ErrorCASExternalUrlNotSet
		{
			get
			{
				return new LocalizedString("ErrorCASExternalUrlNotSet", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B2E RID: 15150
		// (get) Token: 0x0600BBBB RID: 48059 RVA: 0x002AB467 File Offset: 0x002A9667
		public static LocalizedString HybridInfoCheckForPermissionToUpgrade
		{
			get
			{
				return new LocalizedString("HybridInfoCheckForPermissionToUpgrade", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBBC RID: 48060 RVA: 0x002AB480 File Offset: 0x002A9680
		public static LocalizedString ErrorHybridServerAlreadyAssigned(string server)
		{
			return new LocalizedString("ErrorHybridServerAlreadyAssigned", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B2F RID: 15151
		// (get) Token: 0x0600BBBD RID: 48061 RVA: 0x002AB4A8 File Offset: 0x002A96A8
		public static LocalizedString ErrorHybridOnPremisesOrganizationWasNotCreatedWithUpgradedConnectors
		{
			get
			{
				return new LocalizedString("ErrorHybridOnPremisesOrganizationWasNotCreatedWithUpgradedConnectors", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B30 RID: 15152
		// (get) Token: 0x0600BBBE RID: 48062 RVA: 0x002AB4BF File Offset: 0x002A96BF
		public static LocalizedString ErrorNoInboundConnector
		{
			get
			{
				return new LocalizedString("ErrorNoInboundConnector", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBBF RID: 48063 RVA: 0x002AB4D8 File Offset: 0x002A96D8
		public static LocalizedString HybridErrorReceivingTransportServerNotFrontEnd(string server)
		{
			return new LocalizedString("HybridErrorReceivingTransportServerNotFrontEnd", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600BBC0 RID: 48064 RVA: 0x002AB500 File Offset: 0x002A9700
		public static LocalizedString ErrorHybridDomainNotAcceptedOnTenant(string domain)
		{
			return new LocalizedString("ErrorHybridDomainNotAcceptedOnTenant", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BBC1 RID: 48065 RVA: 0x002AB528 File Offset: 0x002A9728
		public static LocalizedString HybridFedInfoFallbackWarning(string domain)
		{
			return new LocalizedString("HybridFedInfoFallbackWarning", HybridStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BBC2 RID: 48066 RVA: 0x002AB550 File Offset: 0x002A9750
		public static LocalizedString WarningHybridLegacyEmailAddressPolicyNotUpgraded(string policyName)
		{
			return new LocalizedString("WarningHybridLegacyEmailAddressPolicyNotUpgraded", HybridStrings.ResourceManager, new object[]
			{
				policyName
			});
		}

		// Token: 0x17003B31 RID: 15153
		// (get) Token: 0x0600BBC3 RID: 48067 RVA: 0x002AB578 File Offset: 0x002A9778
		public static LocalizedString ErrorHybridConfigurationAlreadyDefined
		{
			get
			{
				return new LocalizedString("ErrorHybridConfigurationAlreadyDefined", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBC4 RID: 48068 RVA: 0x002AB590 File Offset: 0x002A9790
		public static LocalizedString HybridErrorServerNotEdge(string server)
		{
			return new LocalizedString("HybridErrorServerNotEdge", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B32 RID: 15154
		// (get) Token: 0x0600BBC5 RID: 48069 RVA: 0x002AB5B8 File Offset: 0x002A97B8
		public static LocalizedString MailFlowTaskName
		{
			get
			{
				return new LocalizedString("MailFlowTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBC6 RID: 48070 RVA: 0x002AB5D0 File Offset: 0x002A97D0
		public static LocalizedString HybridInfoHybridConfigurationObjectVersion(ExchangeObjectVersion version)
		{
			return new LocalizedString("HybridInfoHybridConfigurationObjectVersion", HybridStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x17003B33 RID: 15155
		// (get) Token: 0x0600BBC7 RID: 48071 RVA: 0x002AB5F8 File Offset: 0x002A97F8
		public static LocalizedString ErrorHybridExternalIPAddressesRangeAddressesNotSupported
		{
			get
			{
				return new LocalizedString("ErrorHybridExternalIPAddressesRangeAddressesNotSupported", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBC8 RID: 48072 RVA: 0x002AB610 File Offset: 0x002A9810
		public static LocalizedString HybridInfoTaskSubStepStart(string taskName, string subStepName)
		{
			return new LocalizedString("HybridInfoTaskSubStepStart", HybridStrings.ResourceManager, new object[]
			{
				taskName,
				subStepName
			});
		}

		// Token: 0x17003B34 RID: 15156
		// (get) Token: 0x0600BBC9 RID: 48073 RVA: 0x002AB63C File Offset: 0x002A983C
		public static LocalizedString HybridActivityCompleted
		{
			get
			{
				return new LocalizedString("HybridActivityCompleted", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBCA RID: 48074 RVA: 0x002AB654 File Offset: 0x002A9854
		public static LocalizedString ErrorSendConnectorAddressSpaceNotExclusive(string addressSpace)
		{
			return new LocalizedString("ErrorSendConnectorAddressSpaceNotExclusive", HybridStrings.ResourceManager, new object[]
			{
				addressSpace
			});
		}

		// Token: 0x17003B35 RID: 15157
		// (get) Token: 0x0600BBCB RID: 48075 RVA: 0x002AB67C File Offset: 0x002A987C
		public static LocalizedString ErrorFederatedIdentifierDisabled
		{
			get
			{
				return new LocalizedString("ErrorFederatedIdentifierDisabled", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B36 RID: 15158
		// (get) Token: 0x0600BBCC RID: 48076 RVA: 0x002AB693 File Offset: 0x002A9893
		public static LocalizedString HybridInfoConnectedToOnPrem
		{
			get
			{
				return new LocalizedString("HybridInfoConnectedToOnPrem", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBCD RID: 48077 RVA: 0x002AB6AC File Offset: 0x002A98AC
		public static LocalizedString WarningOAuthNeedsConfiguration(string url)
		{
			return new LocalizedString("WarningOAuthNeedsConfiguration", HybridStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17003B37 RID: 15159
		// (get) Token: 0x0600BBCE RID: 48078 RVA: 0x002AB6D4 File Offset: 0x002A98D4
		public static LocalizedString HybridConnectingToTenant
		{
			get
			{
				return new LocalizedString("HybridConnectingToTenant", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBCF RID: 48079 RVA: 0x002AB6EC File Offset: 0x002A98EC
		public static LocalizedString ErrorSecureMailCertificateInvalidDate(string server)
		{
			return new LocalizedString("ErrorSecureMailCertificateInvalidDate", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600BBD0 RID: 48080 RVA: 0x002AB714 File Offset: 0x002A9914
		public static LocalizedString HybridErrorServerNotCAS(string server)
		{
			return new LocalizedString("HybridErrorServerNotCAS", HybridStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17003B38 RID: 15160
		// (get) Token: 0x0600BBD1 RID: 48081 RVA: 0x002AB73C File Offset: 0x002A993C
		public static LocalizedString HybridUpgradePrompt
		{
			get
			{
				return new LocalizedString("HybridUpgradePrompt", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003B39 RID: 15161
		// (get) Token: 0x0600BBD2 RID: 48082 RVA: 0x002AB753 File Offset: 0x002A9953
		public static LocalizedString ErrorHybridTenenatUpgradeRequired
		{
			get
			{
				return new LocalizedString("ErrorHybridTenenatUpgradeRequired", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBD3 RID: 48083 RVA: 0x002AB76C File Offset: 0x002A996C
		public static LocalizedString HybridInfoCmdletEnd(string session, string cmdlet, string elapsed)
		{
			return new LocalizedString("HybridInfoCmdletEnd", HybridStrings.ResourceManager, new object[]
			{
				session,
				cmdlet,
				elapsed
			});
		}

		// Token: 0x17003B3A RID: 15162
		// (get) Token: 0x0600BBD4 RID: 48084 RVA: 0x002AB79C File Offset: 0x002A999C
		public static LocalizedString ErrorHybridExternalIPAddressesExceeded40Entries
		{
			get
			{
				return new LocalizedString("ErrorHybridExternalIPAddressesExceeded40Entries", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBD5 RID: 48085 RVA: 0x002AB7B4 File Offset: 0x002A99B4
		public static LocalizedString HybridInfoHybridConfigurationEngineVersion(ExchangeObjectVersion version)
		{
			return new LocalizedString("HybridInfoHybridConfigurationEngineVersion", HybridStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x17003B3B RID: 15163
		// (get) Token: 0x0600BBD6 RID: 48086 RVA: 0x002AB7DC File Offset: 0x002A99DC
		public static LocalizedString OnOffSettingsTaskName
		{
			get
			{
				return new LocalizedString("OnOffSettingsTaskName", HybridStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BBD7 RID: 48087 RVA: 0x002AB7F3 File Offset: 0x002A99F3
		public static LocalizedString GetLocalizedString(HybridStrings.IDs key)
		{
			return new LocalizedString(HybridStrings.stringIDs[(uint)key], HybridStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04006576 RID: 25974
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(78);

		// Token: 0x04006577 RID: 25975
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Hybrid.Strings", typeof(HybridStrings).GetTypeInfo().Assembly);

		// Token: 0x02001231 RID: 4657
		public enum IDs : uint
		{
			// Token: 0x04006579 RID: 25977
			ErrorHybridUpgradedTo2013 = 2425718147U,
			// Token: 0x0400657A RID: 25978
			ErrorMultipleFederationTrusts = 270912202U,
			// Token: 0x0400657B RID: 25979
			ErrorHybridMustBeUpgraded = 820521406U,
			// Token: 0x0400657C RID: 25980
			ErrorNoFederatedDomainsOnTenant = 4267730599U,
			// Token: 0x0400657D RID: 25981
			HybridErrorBothTransportServersNotSet = 1424719599U,
			// Token: 0x0400657E RID: 25982
			ErrorNoHybridDomain = 2925976073U,
			// Token: 0x0400657F RID: 25983
			HybridInfoObjectNotFound = 3187185748U,
			// Token: 0x04006580 RID: 25984
			ErrorMultipleMatchingOrgRelationships = 2511569494U,
			// Token: 0x04006581 RID: 25985
			ErrorOnPremUsingConsumerLiveID = 3431283964U,
			// Token: 0x04006582 RID: 25986
			ErrorFederationIDNotProvisioned = 4267028265U,
			// Token: 0x04006583 RID: 25987
			ErrorHybridClientAccessServersNotCleared = 4281839122U,
			// Token: 0x04006584 RID: 25988
			ErrorNoTenantAcceptedDomains = 1430057223U,
			// Token: 0x04006585 RID: 25989
			MRSProxyTaskName = 2996915408U,
			// Token: 0x04006586 RID: 25990
			InvalidOrganizationRelationship = 458024348U,
			// Token: 0x04006587 RID: 25991
			HybridInfoValidateUnusedConfigurationAttributesAreCleared = 786567629U,
			// Token: 0x04006588 RID: 25992
			HybridErrorOnlyOneAutoDiscoverDomainMayBeSet = 423230028U,
			// Token: 0x04006589 RID: 25993
			GlobalPrereqTaskName = 2908580272U,
			// Token: 0x0400658A RID: 25994
			HybridInfoExecutionComplete = 127541045U,
			// Token: 0x0400658B RID: 25995
			WarningUnableToCommunicateWithAutoDiscoveryEP = 3035608407U,
			// Token: 0x0400658C RID: 25996
			ConfirmationMessageUpdateHybridConfiguration = 2863677737U,
			// Token: 0x0400658D RID: 25997
			ErrorOrgRelNotFoundOnPrem = 2717467465U,
			// Token: 0x0400658E RID: 25998
			HybridInfoBasePrereqsFailed = 838311222U,
			// Token: 0x0400658F RID: 25999
			ErrorNoOrganizationalUnitsFound = 876046568U,
			// Token: 0x04006590 RID: 26000
			IOCConfigurationTaskName = 512075U,
			// Token: 0x04006591 RID: 26001
			ConfirmationMessageSetHybridConfiguration = 2570457594U,
			// Token: 0x04006592 RID: 26002
			ReturnResultForHybridDetectionWasFalse = 2441367287U,
			// Token: 0x04006593 RID: 26003
			ErrorIncompatibleServersDetected = 3815718049U,
			// Token: 0x04006594 RID: 26004
			HybridErrorNoTlsCertificateNameSet = 2541891350U,
			// Token: 0x04006595 RID: 26005
			ErrorHybridUpgradeNotAllTransportServersProperVersion = 3364263621U,
			// Token: 0x04006596 RID: 26006
			HybridInfoVerifyTenantHasBeenUpgraded = 3426408323U,
			// Token: 0x04006597 RID: 26007
			RecipientTaskName = 3661239469U,
			// Token: 0x04006598 RID: 26008
			OrganizationRelationshipTaskName = 3953578261U,
			// Token: 0x04006599 RID: 26009
			HybridInfoUpdatingHybridConfigurationVersion = 573859146U,
			// Token: 0x0400659A RID: 26010
			ErrorAccountNamespace = 3251785458U,
			// Token: 0x0400659B RID: 26011
			ErrorNoOnPremAcceptedDomains = 2320637488U,
			// Token: 0x0400659C RID: 26012
			ErrorNoHybridDomains = 2413435486U,
			// Token: 0x0400659D RID: 26013
			HybridInfoPurePSObjectsNotSupported = 888551196U,
			// Token: 0x0400659E RID: 26014
			HybridInfoClearingUnusedHybridConfigurationProperties = 2147167682U,
			// Token: 0x0400659F RID: 26015
			HybridFedInfoFallbackInfo = 1453693463U,
			// Token: 0x040065A0 RID: 26016
			HybridUpgradeFrom14TaskName = 46137829U,
			// Token: 0x040065A1 RID: 26017
			HybridInfoConnectedToTenant = 2918546762U,
			// Token: 0x040065A2 RID: 26018
			ErrorOrgRelNotFoundOnTenant = 3422685683U,
			// Token: 0x040065A3 RID: 26019
			HybridEngineCheckingForUpgradeTenant = 345297155U,
			// Token: 0x040065A4 RID: 26020
			HybridInfoRemovingUnnecessaryRemoteDomains = 2844731914U,
			// Token: 0x040065A5 RID: 26021
			TenantDetectionTaskName = 1876236381U,
			// Token: 0x040065A6 RID: 26022
			HybridInfoVerifyTransportServers = 144627946U,
			// Token: 0x040065A7 RID: 26023
			ErrorNoOutboundConnector = 430320558U,
			// Token: 0x040065A8 RID: 26024
			HybridErrorNoTransportServersSet = 3561336298U,
			// Token: 0x040065A9 RID: 26025
			HybridActivityEstablish = 4039070452U,
			// Token: 0x040065AA RID: 26026
			HybridErrorNoSmartHostSet = 2473550644U,
			// Token: 0x040065AB RID: 26027
			HybridInfoValidatingUnnecessaryRemoteDomainsAreRemoved = 2276570094U,
			// Token: 0x040065AC RID: 26028
			WarningRedirectCU10HybridStandaloneConfiguration = 3952178692U,
			// Token: 0x040065AD RID: 26029
			ErrorHybridNoCASWithEWSURL = 2384567192U,
			// Token: 0x040065AE RID: 26030
			ConfirmationMessageNewHybridConfiguration = 383363188U,
			// Token: 0x040065AF RID: 26031
			HybridErrorMixedTransportServersSet = 1366871238U,
			// Token: 0x040065B0 RID: 26032
			HybridConnectingToOnPrem = 1823864370U,
			// Token: 0x040065B1 RID: 26033
			RemoveHybidConfigurationConfirmation = 3524037511U,
			// Token: 0x040065B2 RID: 26034
			ErrorTenantUsingConsumerLiveID = 3455711695U,
			// Token: 0x040065B3 RID: 26035
			HybridActivityConfigure = 856118049U,
			// Token: 0x040065B4 RID: 26036
			HybridInfoNoNeedToUpgrade = 3567836490U,
			// Token: 0x040065B5 RID: 26037
			ErrorNoFederationTrustFound = 1109246636U,
			// Token: 0x040065B6 RID: 26038
			ErrorHybridConfigurationVersionNotUpdated = 2371375820U,
			// Token: 0x040065B7 RID: 26039
			ErrorHybridExternalIPAddressesNotCleared = 3910362397U,
			// Token: 0x040065B8 RID: 26040
			ErrorCASExternalUrlNotSet = 1896684180U,
			// Token: 0x040065B9 RID: 26041
			HybridInfoCheckForPermissionToUpgrade = 2525757763U,
			// Token: 0x040065BA RID: 26042
			ErrorHybridOnPremisesOrganizationWasNotCreatedWithUpgradedConnectors = 1105710278U,
			// Token: 0x040065BB RID: 26043
			ErrorNoInboundConnector = 4077665459U,
			// Token: 0x040065BC RID: 26044
			ErrorHybridConfigurationAlreadyDefined = 4171349911U,
			// Token: 0x040065BD RID: 26045
			MailFlowTaskName = 2951975549U,
			// Token: 0x040065BE RID: 26046
			ErrorHybridExternalIPAddressesRangeAddressesNotSupported = 969801930U,
			// Token: 0x040065BF RID: 26047
			HybridActivityCompleted = 1250087182U,
			// Token: 0x040065C0 RID: 26048
			ErrorFederatedIdentifierDisabled = 951022927U,
			// Token: 0x040065C1 RID: 26049
			HybridInfoConnectedToOnPrem = 1869337625U,
			// Token: 0x040065C2 RID: 26050
			HybridConnectingToTenant = 4016244689U,
			// Token: 0x040065C3 RID: 26051
			HybridUpgradePrompt = 2902086538U,
			// Token: 0x040065C4 RID: 26052
			ErrorHybridTenenatUpgradeRequired = 2366359160U,
			// Token: 0x040065C5 RID: 26053
			ErrorHybridExternalIPAddressesExceeded40Entries = 3418341869U,
			// Token: 0x040065C6 RID: 26054
			OnOffSettingsTaskName = 3323479649U
		}

		// Token: 0x02001232 RID: 4658
		private enum ParamIDs
		{
			// Token: 0x040065C8 RID: 26056
			ErrorSecureMailCertificateNotFound,
			// Token: 0x040065C9 RID: 26057
			ErrorCASExternalUrlMatchNotFound,
			// Token: 0x040065CA RID: 26058
			ErrorTargetAutodiscoverEprNotFound,
			// Token: 0x040065CB RID: 26059
			ErrorHybridConfigurationTooNew,
			// Token: 0x040065CC RID: 26060
			HybridErrorServerNotE14Edge,
			// Token: 0x040065CD RID: 26061
			HybridErrorServerNotE14CAS,
			// Token: 0x040065CE RID: 26062
			HybridCouldNotResolveServerException,
			// Token: 0x040065CF RID: 26063
			HybridInfoOpeningRunspace,
			// Token: 0x040065D0 RID: 26064
			HybridErrorSendingTransportServerNotE15Hub,
			// Token: 0x040065D1 RID: 26065
			ErrorHybridTenantRemoteDomainNotRemoved,
			// Token: 0x040065D2 RID: 26066
			WarningEdgeReceiveConnector,
			// Token: 0x040065D3 RID: 26067
			HybridInfoTaskSubStepFinish,
			// Token: 0x040065D4 RID: 26068
			ErrorHybridOnPremRemoteDomainNotRemoved,
			// Token: 0x040065D5 RID: 26069
			ErrorCASCertInvalidDate,
			// Token: 0x040065D6 RID: 26070
			HybridCouldNotOpenRunspaceException,
			// Token: 0x040065D7 RID: 26071
			HybridFedInfoFallbackError,
			// Token: 0x040065D8 RID: 26072
			ErrorCASCertSelfSigned,
			// Token: 0x040065D9 RID: 26073
			ErrorTargetApplicationUriNotFound,
			// Token: 0x040065DA RID: 26074
			ErrorInvalidSignupDomain,
			// Token: 0x040065DB RID: 26075
			ErrorHybridDomainNotAcceptedOnPrem,
			// Token: 0x040065DC RID: 26076
			ErrorCASRoleInvalid,
			// Token: 0x040065DD RID: 26077
			ErrorCmdletException,
			// Token: 0x040065DE RID: 26078
			HybridCouldNotCreateTenantSessionException,
			// Token: 0x040065DF RID: 26079
			ErrorOrgRelProvisionFailed,
			// Token: 0x040065E0 RID: 26080
			ErrorSignupDomain,
			// Token: 0x040065E1 RID: 26081
			HybridCouldNotCreateOnPremisesSessionException,
			// Token: 0x040065E2 RID: 26082
			HybridErrorReceivingTransportServerNotE15FrontEnd,
			// Token: 0x040065E3 RID: 26083
			ErrorSecureMailCertificateNoSmtp,
			// Token: 0x040065E4 RID: 26084
			HybridInfoTaskLogTemplate,
			// Token: 0x040065E5 RID: 26085
			WarningTenantGetFedInfoFailed,
			// Token: 0x040065E6 RID: 26086
			ErrorDefaultReceieveConnectorNotFound,
			// Token: 0x040065E7 RID: 26087
			ErrorTaskExceptionTemplate,
			// Token: 0x040065E8 RID: 26088
			ErrorSecureMailCertificateSelfSigned,
			// Token: 0x040065E9 RID: 26089
			ErrorCoexistenceDomainNotAcceptedOnTenant,
			// Token: 0x040065EA RID: 26090
			ErrorFederationInfoNotFound,
			// Token: 0x040065EB RID: 26091
			HybridInfoGetObjectValue,
			// Token: 0x040065EC RID: 26092
			ErrorCASCertNotTrusted,
			// Token: 0x040065ED RID: 26093
			ExceptionUpdateHybridConfigurationFailedWithLog,
			// Token: 0x040065EE RID: 26094
			HybridInfoTotalCmdletTime,
			// Token: 0x040065EF RID: 26095
			ErrorTooManyMatchingResults,
			// Token: 0x040065F0 RID: 26096
			HybridErrorSendingTransportServerNotHub,
			// Token: 0x040065F1 RID: 26097
			HybridInfoCmdletStart,
			// Token: 0x040065F2 RID: 26098
			ErrorHybridRegistryInvalidUri,
			// Token: 0x040065F3 RID: 26099
			ErrorDuplicateSendConnectorAddressSpace,
			// Token: 0x040065F4 RID: 26100
			ErrorHybridServerAlreadyAssigned,
			// Token: 0x040065F5 RID: 26101
			HybridErrorReceivingTransportServerNotFrontEnd,
			// Token: 0x040065F6 RID: 26102
			ErrorHybridDomainNotAcceptedOnTenant,
			// Token: 0x040065F7 RID: 26103
			HybridFedInfoFallbackWarning,
			// Token: 0x040065F8 RID: 26104
			WarningHybridLegacyEmailAddressPolicyNotUpgraded,
			// Token: 0x040065F9 RID: 26105
			HybridErrorServerNotEdge,
			// Token: 0x040065FA RID: 26106
			HybridInfoHybridConfigurationObjectVersion,
			// Token: 0x040065FB RID: 26107
			HybridInfoTaskSubStepStart,
			// Token: 0x040065FC RID: 26108
			ErrorSendConnectorAddressSpaceNotExclusive,
			// Token: 0x040065FD RID: 26109
			WarningOAuthNeedsConfiguration,
			// Token: 0x040065FE RID: 26110
			ErrorSecureMailCertificateInvalidDate,
			// Token: 0x040065FF RID: 26111
			HybridErrorServerNotCAS,
			// Token: 0x04006600 RID: 26112
			HybridInfoCmdletEnd,
			// Token: 0x04006601 RID: 26113
			HybridInfoHybridConfigurationEngineVersion
		}
	}
}
