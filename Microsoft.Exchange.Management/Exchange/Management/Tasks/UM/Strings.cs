using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011A3 RID: 4515
	internal static class Strings
	{
		// Token: 0x0600B71D RID: 46877 RVA: 0x002A0E34 File Offset: 0x0029F034
		static Strings()
		{
			Strings.stringIDs.Add(781067276U, "ConfirmationMessageStartUMPhoneSession");
			Strings.stringIDs.Add(3239495249U, "InstallUMWebServiceTask");
			Strings.stringIDs.Add(3779087329U, "InvalidMethodToDisableAA");
			Strings.stringIDs.Add(1689586553U, "EmptyCountryOrRegionCode");
			Strings.stringIDs.Add(1601426056U, "DefaultMailboxSettings");
			Strings.stringIDs.Add(530191348U, "ConfirmationMessageStopUMPhoneSession");
			Strings.stringIDs.Add(3015522797U, "InstallUmCallRouterTask");
			Strings.stringIDs.Add(1999730358U, "CannotCreateHuntGroupForHostedSipDialPlan");
			Strings.stringIDs.Add(3303439652U, "PasswordMailBody");
			Strings.stringIDs.Add(987112185U, "ExternalHostFqdnChanges");
			Strings.stringIDs.Add(3433711682U, "RemoteEndDisconnected");
			Strings.stringIDs.Add(887877668U, "SuccessfulLogonState");
			Strings.stringIDs.Add(1970930248U, "ValidCertRequiredForUM");
			Strings.stringIDs.Add(454845832U, "AttemptingToCreateHuntgroup");
			Strings.stringIDs.Add(2238046577U, "OperationSuccessful");
			Strings.stringIDs.Add(3572721584U, "CallRouterTransferFromTLStoTCPModeWarning");
			Strings.stringIDs.Add(505394326U, "UmServiceNotInstalled");
			Strings.stringIDs.Add(3094237923U, "UMStartupModeChanges");
			Strings.stringIDs.Add(2019001798U, "PromptProvisioningShareDescription");
			Strings.stringIDs.Add(3372841158U, "UmCallRouterName");
			Strings.stringIDs.Add(1223643872U, "UserProblem");
			Strings.stringIDs.Add(648648092U, "ConfigureGatewayToForwardCallsMsg");
			Strings.stringIDs.Add(939630945U, "GatewayAddressRequiresFqdn");
			Strings.stringIDs.Add(471142364U, "DNSEntryNotFound");
			Strings.stringIDs.Add(3757387627U, "ExceptionInvalidSipNameDomain");
			Strings.stringIDs.Add(4150015982U, "UninstallUmCallRouterTask");
			Strings.stringIDs.Add(1600651549U, "LogonError");
			Strings.stringIDs.Add(388296289U, "ConfirmationMessageDisableUMServerImmediately");
			Strings.stringIDs.Add(818779485U, "GatewayIPAddressFamilyInconsistentException");
			Strings.stringIDs.Add(2840915457U, "ConfirmationMessageDisableUMServer");
			Strings.stringIDs.Add(1625624441U, "NoMailboxServersFound");
			Strings.stringIDs.Add(1298209242U, "SrtpWithoutTls");
			Strings.stringIDs.Add(3114226637U, "ConfirmationMessageDisableUMIPGateway");
			Strings.stringIDs.Add(2388018184U, "UninstallUmServiceTask");
			Strings.stringIDs.Add(2187708795U, "UmServiceDescription");
			Strings.stringIDs.Add(3944137335U, "DefaultMailboxRequiredWhenForwardTrue");
			Strings.stringIDs.Add(4164812292U, "ConfirmationMessageTestUMConnectivityLocalLoop");
			Strings.stringIDs.Add(587456431U, "InvalidDefaultOutboundCallingLineId");
			Strings.stringIDs.Add(193099536U, "ErrorGeneratingDefaultPassword");
			Strings.stringIDs.Add(3703544840U, "InvalidDTMFSequenceReceived");
			Strings.stringIDs.Add(2970756378U, "UninstallUMWebServiceTask");
			Strings.stringIDs.Add(1263770381U, "ADError");
			Strings.stringIDs.Add(48881282U, "NotMailboxServer");
			Strings.stringIDs.Add(2410305798U, "LanguagesNotPassed");
			Strings.stringIDs.Add(1691564973U, "InstallUmServiceTask");
			Strings.stringIDs.Add(2718089772U, "WaitForFirstDiagnosticResponse");
			Strings.stringIDs.Add(940435338U, "InvalidTimeZoneParameters");
			Strings.stringIDs.Add(1355103499U, "CertNotFound");
			Strings.stringIDs.Add(2010047074U, "PilotNumberState");
			Strings.stringIDs.Add(1658738722U, "KeepProperties");
			Strings.stringIDs.Add(2919065030U, "WaitForDiagnosticResponse");
			Strings.stringIDs.Add(2675709228U, "UCMAPreReqException");
			Strings.stringIDs.Add(2660011992U, "DialPlanAssociatedWithPoliciesException");
			Strings.stringIDs.Add(1878527290U, "PinExpired");
			Strings.stringIDs.Add(1151155524U, "LockedOut");
			Strings.stringIDs.Add(100754582U, "GatewayFqdnNotInAcceptedDomain");
			Strings.stringIDs.Add(918425027U, "NoDTMFSwereReceived");
			Strings.stringIDs.Add(1885509224U, "PasswordMailSubject");
			Strings.stringIDs.Add(2380514321U, "InvalidIPAddressReceived");
			Strings.stringIDs.Add(943497584U, "InvalidALParameterException");
			Strings.stringIDs.Add(3527640475U, "MustSpecifyThumbprint");
			Strings.stringIDs.Add(3746187495U, "InvalidMailboxServerVersionForTUMCTask");
			Strings.stringIDs.Add(886805380U, "CannotCreateGatewayForHostedSipDialPlan");
			Strings.stringIDs.Add(2203312157U, "ConfirmationMessageDisableUMIPGatewayImmediately");
			Strings.stringIDs.Add(1602032641U, "PilotNumber");
			Strings.stringIDs.Add(1762496243U, "AttemptingToCreateIPGateway");
			Strings.stringIDs.Add(505861453U, "ExceptionUserNotAllowedForUMEnabled");
			Strings.stringIDs.Add(305424905U, "ExchangePrincipalError");
			Strings.stringIDs.Add(2568008289U, "InvalidExternalHostFqdn");
			Strings.stringIDs.Add(912893922U, "UCMAPreReqUpgradeException");
			Strings.stringIDs.Add(4252462372U, "AADisableConfirmationString");
			Strings.stringIDs.Add(3860731788U, "AAAlreadyDisabled");
			Strings.stringIDs.Add(3372908061U, "ConfirmationMessageTestUMConnectivityPinReset");
			Strings.stringIDs.Add(1942717475U, "CertWithoutTls");
			Strings.stringIDs.Add(1061463472U, "SendEmail");
			Strings.stringIDs.Add(3845881662U, "ExceptionSipResourceIdNotUnique");
			Strings.stringIDs.Add(55026498U, "PortChanges");
			Strings.stringIDs.Add(577419765U, "AANameTooLong");
			Strings.stringIDs.Add(1006009848U, "DefaultUMHuntGroupName");
			Strings.stringIDs.Add(1694996750U, "CouldnotRetreivePasswd");
			Strings.stringIDs.Add(1731989106U, "PINEnterState");
			Strings.stringIDs.Add(3858123826U, "BusinessHoursSettings");
			Strings.stringIDs.Add(2511055751U, "UmServiceStillInstalled");
			Strings.stringIDs.Add(1003963056U, "ConfirmationMessageSetUmCallRouterSettings");
			Strings.stringIDs.Add(160860353U, "ValidCertRequiredForUMCallRouter");
			Strings.stringIDs.Add(3650433099U, "DialPlanAssociatedWithUserException");
			Strings.stringIDs.Add(73835935U, "TransferFromTLStoTCPModeWarning");
			Strings.stringIDs.Add(4163764725U, "InvalidALParameter");
			Strings.stringIDs.Add(154856458U, "AfterHoursSettings");
			Strings.stringIDs.Add(3666462471U, "UmCallRouterDescription");
			Strings.stringIDs.Add(3969155231U, "InvalidAutoAttendantScopeSetting");
			Strings.stringIDs.Add(3358569313U, "TcpAndTlsPortsCannotBeSame");
			Strings.stringIDs.Add(3177175916U, "ConfirmationMessageTestUMConnectivityTUILocalLoop");
			Strings.stringIDs.Add(2342320894U, "CurrentTimeZoneIdNotFound");
			Strings.stringIDs.Add(975840932U, "AttemptingToStampFQDN");
			Strings.stringIDs.Add(339800695U, "Pin");
			Strings.stringIDs.Add(4175977607U, "NotifyEmail");
			Strings.stringIDs.Add(1074457952U, "UmServiceName");
			Strings.stringIDs.Add(2152868767U, "AAAlreadyEnabled");
			Strings.stringIDs.Add(1304023191U, "TransferFromTCPtoTLSModeWarning");
		}

		// Token: 0x0600B71E RID: 46878 RVA: 0x002A1640 File Offset: 0x0029F840
		public static LocalizedString DisabledLinkedAutoAttendant(string autoAttendant, string linkedAutoAttendant)
		{
			return new LocalizedString("DisabledLinkedAutoAttendant", "", false, false, Strings.ResourceManager, new object[]
			{
				autoAttendant,
				linkedAutoAttendant
			});
		}

		// Token: 0x0600B71F RID: 46879 RVA: 0x002A1674 File Offset: 0x0029F874
		public static LocalizedString MultipleAutoAttendantsWithSameId(string s)
		{
			return new LocalizedString("MultipleAutoAttendantsWithSameId", "Ex13CFAB", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039AE RID: 14766
		// (get) Token: 0x0600B720 RID: 46880 RVA: 0x002A16A3 File Offset: 0x0029F8A3
		public static LocalizedString ConfirmationMessageStartUMPhoneSession
		{
			get
			{
				return new LocalizedString("ConfirmationMessageStartUMPhoneSession", "ExEAF89A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B721 RID: 46881 RVA: 0x002A16C4 File Offset: 0x0029F8C4
		public static LocalizedString UMServiceDisabledException(string serviceName, string serverName)
		{
			return new LocalizedString("UMServiceDisabledException", "", false, false, Strings.ResourceManager, new object[]
			{
				serviceName,
				serverName
			});
		}

		// Token: 0x170039AF RID: 14767
		// (get) Token: 0x0600B722 RID: 46882 RVA: 0x002A16F7 File Offset: 0x0029F8F7
		public static LocalizedString InstallUMWebServiceTask
		{
			get
			{
				return new LocalizedString("InstallUMWebServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039B0 RID: 14768
		// (get) Token: 0x0600B723 RID: 46883 RVA: 0x002A1715 File Offset: 0x0029F915
		public static LocalizedString InvalidMethodToDisableAA
		{
			get
			{
				return new LocalizedString("InvalidMethodToDisableAA", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039B1 RID: 14769
		// (get) Token: 0x0600B724 RID: 46884 RVA: 0x002A1733 File Offset: 0x0029F933
		public static LocalizedString EmptyCountryOrRegionCode
		{
			get
			{
				return new LocalizedString("EmptyCountryOrRegionCode", "Ex3AF75C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B725 RID: 46885 RVA: 0x002A1754 File Offset: 0x0029F954
		public static LocalizedString SourceFileOpenException(string fileName)
		{
			return new LocalizedString("SourceFileOpenException", "", false, false, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x0600B726 RID: 46886 RVA: 0x002A1784 File Offset: 0x0029F984
		public static LocalizedString InvalidDtmfFallbackAutoAttendant(string s)
		{
			return new LocalizedString("InvalidDtmfFallbackAutoAttendant", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B727 RID: 46887 RVA: 0x002A17B4 File Offset: 0x0029F9B4
		public static LocalizedString MultipleDialplansWithSameId(object s)
		{
			return new LocalizedString("MultipleDialplansWithSameId", "Ex33C279", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039B2 RID: 14770
		// (get) Token: 0x0600B728 RID: 46888 RVA: 0x002A17E3 File Offset: 0x0029F9E3
		public static LocalizedString DefaultMailboxSettings
		{
			get
			{
				return new LocalizedString("DefaultMailboxSettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B729 RID: 46889 RVA: 0x002A1804 File Offset: 0x0029FA04
		public static LocalizedString ConfirmationMessageExportUMMailboxPrompt(string promptName)
		{
			return new LocalizedString("ConfirmationMessageExportUMMailboxPrompt", "Ex6462C2", false, true, Strings.ResourceManager, new object[]
			{
				promptName
			});
		}

		// Token: 0x170039B3 RID: 14771
		// (get) Token: 0x0600B72A RID: 46890 RVA: 0x002A1833 File Offset: 0x0029FA33
		public static LocalizedString ConfirmationMessageStopUMPhoneSession
		{
			get
			{
				return new LocalizedString("ConfirmationMessageStopUMPhoneSession", "Ex00E79A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B72B RID: 46891 RVA: 0x002A1854 File Offset: 0x0029FA54
		public static LocalizedString CannotDisableAutoAttendant_KeyMapping(string s)
		{
			return new LocalizedString("CannotDisableAutoAttendant_KeyMapping", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B72C RID: 46892 RVA: 0x002A1884 File Offset: 0x0029FA84
		public static LocalizedString ExceptionSIPResouceIdConflictWithExistingValue(string sipResId, string sipProxy)
		{
			return new LocalizedString("ExceptionSIPResouceIdConflictWithExistingValue", "", false, false, Strings.ResourceManager, new object[]
			{
				sipResId,
				sipProxy
			});
		}

		// Token: 0x0600B72D RID: 46893 RVA: 0x002A18B8 File Offset: 0x0029FAB8
		public static LocalizedString PINResetfailedToResetPin(string s)
		{
			return new LocalizedString("PINResetfailedToResetPin", "Ex4860CE", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B72E RID: 46894 RVA: 0x002A18E8 File Offset: 0x0029FAE8
		public static LocalizedString RpcNotRegistered(string server)
		{
			return new LocalizedString("RpcNotRegistered", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170039B4 RID: 14772
		// (get) Token: 0x0600B72F RID: 46895 RVA: 0x002A1917 File Offset: 0x0029FB17
		public static LocalizedString InstallUmCallRouterTask
		{
			get
			{
				return new LocalizedString("InstallUmCallRouterTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B730 RID: 46896 RVA: 0x002A1938 File Offset: 0x0029FB38
		public static LocalizedString InvalidUMServerStateOperationException(string s)
		{
			return new LocalizedString("InvalidUMServerStateOperationException", "Ex887580", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B731 RID: 46897 RVA: 0x002A1968 File Offset: 0x0029FB68
		public static LocalizedString ErrorUMInvalidExtensionFormat(string s)
		{
			return new LocalizedString("ErrorUMInvalidExtensionFormat", "Ex82291E", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B732 RID: 46898 RVA: 0x002A1998 File Offset: 0x0029FB98
		public static LocalizedString SavePINError(string user, LocalizedString reason)
		{
			return new LocalizedString("SavePINError", "Ex7152F3", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B733 RID: 46899 RVA: 0x002A19D0 File Offset: 0x0029FBD0
		public static LocalizedString AutoAttendantAlreadDisabledException(string s)
		{
			return new LocalizedString("AutoAttendantAlreadDisabledException", "ExB5F0B1", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B734 RID: 46900 RVA: 0x002A1A00 File Offset: 0x0029FC00
		public static LocalizedString UnableToSetMSSRegistryValue(string registryKey, string exceptionMessage)
		{
			return new LocalizedString("UnableToSetMSSRegistryValue", "ExA53D7C", false, true, Strings.ResourceManager, new object[]
			{
				registryKey,
				exceptionMessage
			});
		}

		// Token: 0x170039B5 RID: 14773
		// (get) Token: 0x0600B735 RID: 46901 RVA: 0x002A1A33 File Offset: 0x0029FC33
		public static LocalizedString CannotCreateHuntGroupForHostedSipDialPlan
		{
			get
			{
				return new LocalizedString("CannotCreateHuntGroupForHostedSipDialPlan", "ExF07F7A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039B6 RID: 14774
		// (get) Token: 0x0600B736 RID: 46902 RVA: 0x002A1A51 File Offset: 0x0029FC51
		public static LocalizedString PasswordMailBody
		{
			get
			{
				return new LocalizedString("PasswordMailBody", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B737 RID: 46903 RVA: 0x002A1A70 File Offset: 0x0029FC70
		public static LocalizedString ConfirmationMessageEnableUMAutoAttendant(string Identity)
		{
			return new LocalizedString("ConfirmationMessageEnableUMAutoAttendant", "ExB8396E", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B738 RID: 46904 RVA: 0x002A1AA0 File Offset: 0x0029FCA0
		public static LocalizedString DialPlanAssociatedWithIPGatewayException(string s)
		{
			return new LocalizedString("DialPlanAssociatedWithIPGatewayException", "ExA19A7E", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039B7 RID: 14775
		// (get) Token: 0x0600B739 RID: 46905 RVA: 0x002A1ACF File Offset: 0x0029FCCF
		public static LocalizedString ExternalHostFqdnChanges
		{
			get
			{
				return new LocalizedString("ExternalHostFqdnChanges", "Ex6260BA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039B8 RID: 14776
		// (get) Token: 0x0600B73A RID: 46906 RVA: 0x002A1AED File Offset: 0x0029FCED
		public static LocalizedString RemoteEndDisconnected
		{
			get
			{
				return new LocalizedString("RemoteEndDisconnected", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B73B RID: 46907 RVA: 0x002A1B0C File Offset: 0x0029FD0C
		public static LocalizedString TUILogonSuccessful(string s)
		{
			return new LocalizedString("TUILogonSuccessful", "Ex4A52B6", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B73C RID: 46908 RVA: 0x002A1B3C File Offset: 0x0029FD3C
		public static LocalizedString SIPFEServerConfigurationNotFound(string serverName)
		{
			return new LocalizedString("SIPFEServerConfigurationNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170039B9 RID: 14777
		// (get) Token: 0x0600B73D RID: 46909 RVA: 0x002A1B6B File Offset: 0x0029FD6B
		public static LocalizedString SuccessfulLogonState
		{
			get
			{
				return new LocalizedString("SuccessfulLogonState", "ExED26CE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039BA RID: 14778
		// (get) Token: 0x0600B73E RID: 46910 RVA: 0x002A1B89 File Offset: 0x0029FD89
		public static LocalizedString ValidCertRequiredForUM
		{
			get
			{
				return new LocalizedString("ValidCertRequiredForUM", "ExAD69E6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B73F RID: 46911 RVA: 0x002A1BA8 File Offset: 0x0029FDA8
		public static LocalizedString ConfirmationMessageCopyUMCustomPromptDownloadAutoAttendantPrompts(string TargetPath, string UMAutoAttendant)
		{
			return new LocalizedString("ConfirmationMessageCopyUMCustomPromptDownloadAutoAttendantPrompts", "ExBC9FB1", false, true, Strings.ResourceManager, new object[]
			{
				TargetPath,
				UMAutoAttendant
			});
		}

		// Token: 0x170039BB RID: 14779
		// (get) Token: 0x0600B740 RID: 46912 RVA: 0x002A1BDB File Offset: 0x0029FDDB
		public static LocalizedString AttemptingToCreateHuntgroup
		{
			get
			{
				return new LocalizedString("AttemptingToCreateHuntgroup", "ExACF72F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B741 RID: 46913 RVA: 0x002A1BFC File Offset: 0x0029FDFC
		public static LocalizedString AutoAttendantEnabledEvent(string s)
		{
			return new LocalizedString("AutoAttendantEnabledEvent", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B742 RID: 46914 RVA: 0x002A1C2C File Offset: 0x0029FE2C
		public static LocalizedString DPLinkedGwNotFQDN(string address, string gateway)
		{
			return new LocalizedString("DPLinkedGwNotFQDN", "Ex4E8609", false, true, Strings.ResourceManager, new object[]
			{
				address,
				gateway
			});
		}

		// Token: 0x0600B743 RID: 46915 RVA: 0x002A1C60 File Offset: 0x0029FE60
		public static LocalizedString ExceptionIPGatewayAlreadyExists(string ipaddress)
		{
			return new LocalizedString("ExceptionIPGatewayAlreadyExists", "", false, false, Strings.ResourceManager, new object[]
			{
				ipaddress
			});
		}

		// Token: 0x0600B744 RID: 46916 RVA: 0x002A1C90 File Offset: 0x0029FE90
		public static LocalizedString ConfirmationMessageExportUMPromptAutoAttendantPrompts(string Path, string UMAutoAttendant)
		{
			return new LocalizedString("ConfirmationMessageExportUMPromptAutoAttendantPrompts", "Ex236E06", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMAutoAttendant
			});
		}

		// Token: 0x0600B745 RID: 46917 RVA: 0x002A1CC4 File Offset: 0x0029FEC4
		public static LocalizedString InvalidLanguageIdException(string l)
		{
			return new LocalizedString("InvalidLanguageIdException", "ExB6F513", false, true, Strings.ResourceManager, new object[]
			{
				l
			});
		}

		// Token: 0x170039BC RID: 14780
		// (get) Token: 0x0600B746 RID: 46918 RVA: 0x002A1CF3 File Offset: 0x0029FEF3
		public static LocalizedString OperationSuccessful
		{
			get
			{
				return new LocalizedString("OperationSuccessful", "Ex469E17", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039BD RID: 14781
		// (get) Token: 0x0600B747 RID: 46919 RVA: 0x002A1D11 File Offset: 0x0029FF11
		public static LocalizedString CallRouterTransferFromTLStoTCPModeWarning
		{
			get
			{
				return new LocalizedString("CallRouterTransferFromTLStoTCPModeWarning", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B748 RID: 46920 RVA: 0x002A1D30 File Offset: 0x0029FF30
		public static LocalizedString InvalidAutoAttendant(string s)
		{
			return new LocalizedString("InvalidAutoAttendant", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B749 RID: 46921 RVA: 0x002A1D60 File Offset: 0x0029FF60
		public static LocalizedString RpcUnavailable(string server)
		{
			return new LocalizedString("RpcUnavailable", "", false, false, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170039BE RID: 14782
		// (get) Token: 0x0600B74A RID: 46922 RVA: 0x002A1D8F File Offset: 0x0029FF8F
		public static LocalizedString UmServiceNotInstalled
		{
			get
			{
				return new LocalizedString("UmServiceNotInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039BF RID: 14783
		// (get) Token: 0x0600B74B RID: 46923 RVA: 0x002A1DAD File Offset: 0x0029FFAD
		public static LocalizedString UMStartupModeChanges
		{
			get
			{
				return new LocalizedString("UMStartupModeChanges", "ExE8B015", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B74C RID: 46924 RVA: 0x002A1DCC File Offset: 0x0029FFCC
		public static LocalizedString FailedToEstablishMedia(int seconds)
		{
			return new LocalizedString("FailedToEstablishMedia", "ExE69203", false, true, Strings.ResourceManager, new object[]
			{
				seconds
			});
		}

		// Token: 0x0600B74D RID: 46925 RVA: 0x002A1E00 File Offset: 0x002A0000
		public static LocalizedString IPGatewayAlreadEnabledException(string s)
		{
			return new LocalizedString("IPGatewayAlreadEnabledException", "ExBA40F0", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B74E RID: 46926 RVA: 0x002A1E30 File Offset: 0x002A0030
		public static LocalizedString DefaultLanguageNotAvailable(string val)
		{
			return new LocalizedString("DefaultLanguageNotAvailable", "", false, false, Strings.ResourceManager, new object[]
			{
				val
			});
		}

		// Token: 0x0600B74F RID: 46927 RVA: 0x002A1E60 File Offset: 0x002A0060
		public static LocalizedString MismatchedOrgInDPAndGW(string dp, string gw)
		{
			return new LocalizedString("MismatchedOrgInDPAndGW", "", false, false, Strings.ResourceManager, new object[]
			{
				dp,
				gw
			});
		}

		// Token: 0x170039C0 RID: 14784
		// (get) Token: 0x0600B750 RID: 46928 RVA: 0x002A1E93 File Offset: 0x002A0093
		public static LocalizedString PromptProvisioningShareDescription
		{
			get
			{
				return new LocalizedString("PromptProvisioningShareDescription", "ExEE67A1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B751 RID: 46929 RVA: 0x002A1EB4 File Offset: 0x002A00B4
		public static LocalizedString ConfirmationMessageRemoveUMHuntGroup(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMHuntGroup", "ExFFB4D2", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B752 RID: 46930 RVA: 0x002A1EE4 File Offset: 0x002A00E4
		public static LocalizedString ConfirmationMessageNewUMDialPlan(string Name, string NumberOfDigitsInExtension)
		{
			return new LocalizedString("ConfirmationMessageNewUMDialPlan", "Ex61C8AF", false, true, Strings.ResourceManager, new object[]
			{
				Name,
				NumberOfDigitsInExtension
			});
		}

		// Token: 0x0600B753 RID: 46931 RVA: 0x002A1F18 File Offset: 0x002A0118
		public static LocalizedString MaxAsrPhraseLengthExceeded(string menu)
		{
			return new LocalizedString("MaxAsrPhraseLengthExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				menu
			});
		}

		// Token: 0x0600B754 RID: 46932 RVA: 0x002A1F48 File Offset: 0x002A0148
		public static LocalizedString ErrorOrganizationNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationNotUnique", "ExA3D209", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x170039C1 RID: 14785
		// (get) Token: 0x0600B755 RID: 46933 RVA: 0x002A1F77 File Offset: 0x002A0177
		public static LocalizedString UmCallRouterName
		{
			get
			{
				return new LocalizedString("UmCallRouterName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B756 RID: 46934 RVA: 0x002A1F98 File Offset: 0x002A0198
		public static LocalizedString OperationNotSupportedOnLegacMailboxException(string use)
		{
			return new LocalizedString("OperationNotSupportedOnLegacMailboxException", "ExCC7517", false, true, Strings.ResourceManager, new object[]
			{
				use
			});
		}

		// Token: 0x170039C2 RID: 14786
		// (get) Token: 0x0600B757 RID: 46935 RVA: 0x002A1FC7 File Offset: 0x002A01C7
		public static LocalizedString UserProblem
		{
			get
			{
				return new LocalizedString("UserProblem", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B758 RID: 46936 RVA: 0x002A1FE8 File Offset: 0x002A01E8
		public static LocalizedString SipOptionsError(string targetUri, string error)
		{
			return new LocalizedString("SipOptionsError", "", false, false, Strings.ResourceManager, new object[]
			{
				targetUri,
				error
			});
		}

		// Token: 0x170039C3 RID: 14787
		// (get) Token: 0x0600B759 RID: 46937 RVA: 0x002A201B File Offset: 0x002A021B
		public static LocalizedString ConfigureGatewayToForwardCallsMsg
		{
			get
			{
				return new LocalizedString("ConfigureGatewayToForwardCallsMsg", "Ex78168D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B75A RID: 46938 RVA: 0x002A203C File Offset: 0x002A023C
		public static LocalizedString ConfirmationMessageSetUmServer(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUmServer", "ExBAA6AF", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B75B RID: 46939 RVA: 0x002A206C File Offset: 0x002A026C
		public static LocalizedString NonExistantServer(string s)
		{
			return new LocalizedString("NonExistantServer", "Ex84E341", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039C4 RID: 14788
		// (get) Token: 0x0600B75C RID: 46940 RVA: 0x002A209B File Offset: 0x002A029B
		public static LocalizedString GatewayAddressRequiresFqdn
		{
			get
			{
				return new LocalizedString("GatewayAddressRequiresFqdn", "Ex78EEA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039C5 RID: 14789
		// (get) Token: 0x0600B75D RID: 46941 RVA: 0x002A20B9 File Offset: 0x002A02B9
		public static LocalizedString DNSEntryNotFound
		{
			get
			{
				return new LocalizedString("DNSEntryNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039C6 RID: 14790
		// (get) Token: 0x0600B75E RID: 46942 RVA: 0x002A20D7 File Offset: 0x002A02D7
		public static LocalizedString ExceptionInvalidSipNameDomain
		{
			get
			{
				return new LocalizedString("ExceptionInvalidSipNameDomain", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B75F RID: 46943 RVA: 0x002A20F8 File Offset: 0x002A02F8
		public static LocalizedString ErrorContactAddressListNotUnique(string cal)
		{
			return new LocalizedString("ErrorContactAddressListNotUnique", "ExA27D04", false, true, Strings.ResourceManager, new object[]
			{
				cal
			});
		}

		// Token: 0x170039C7 RID: 14791
		// (get) Token: 0x0600B760 RID: 46944 RVA: 0x002A2127 File Offset: 0x002A0327
		public static LocalizedString UninstallUmCallRouterTask
		{
			get
			{
				return new LocalizedString("UninstallUmCallRouterTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B761 RID: 46945 RVA: 0x002A2148 File Offset: 0x002A0348
		public static LocalizedString DefaultUMMailboxPolicyName(string dialPlan)
		{
			return new LocalizedString("DefaultUMMailboxPolicyName", "Ex23F363", false, true, Strings.ResourceManager, new object[]
			{
				dialPlan
			});
		}

		// Token: 0x0600B762 RID: 46946 RVA: 0x002A2178 File Offset: 0x002A0378
		public static LocalizedString DefaultPolicyCreationNameTooLong(string dialPlan)
		{
			return new LocalizedString("DefaultPolicyCreationNameTooLong", "ExDB32C1", false, true, Strings.ResourceManager, new object[]
			{
				dialPlan
			});
		}

		// Token: 0x0600B763 RID: 46947 RVA: 0x002A21A8 File Offset: 0x002A03A8
		public static LocalizedString CallAnsweringRuleNotFoundException(string identity)
		{
			return new LocalizedString("CallAnsweringRuleNotFoundException", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600B764 RID: 46948 RVA: 0x002A21D8 File Offset: 0x002A03D8
		public static LocalizedString EmptyASRPhrase(string menu)
		{
			return new LocalizedString("EmptyASRPhrase", "", false, false, Strings.ResourceManager, new object[]
			{
				menu
			});
		}

		// Token: 0x0600B765 RID: 46949 RVA: 0x002A2208 File Offset: 0x002A0408
		public static LocalizedString ConfirmationMessageNewUMIPGateway(string Name, string IPAddress)
		{
			return new LocalizedString("ConfirmationMessageNewUMIPGateway", "ExE8280F", false, true, Strings.ResourceManager, new object[]
			{
				Name,
				IPAddress
			});
		}

		// Token: 0x0600B766 RID: 46950 RVA: 0x002A223C File Offset: 0x002A043C
		public static LocalizedString NonExistantDialPlan(object s)
		{
			return new LocalizedString("NonExistantDialPlan", "ExF33045", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039C8 RID: 14792
		// (get) Token: 0x0600B767 RID: 46951 RVA: 0x002A226B File Offset: 0x002A046B
		public static LocalizedString LogonError
		{
			get
			{
				return new LocalizedString("LogonError", "ExE453E4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B768 RID: 46952 RVA: 0x002A228C File Offset: 0x002A048C
		public static LocalizedString ConfirmationMessageRemoveUMAutoAttendant(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMAutoAttendant", "ExF5A4B1", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B769 RID: 46953 RVA: 0x002A22BC File Offset: 0x002A04BC
		public static LocalizedString UMServerAlreadDisabledException(string s)
		{
			return new LocalizedString("UMServerAlreadDisabledException", "Ex1FA46E", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039C9 RID: 14793
		// (get) Token: 0x0600B76A RID: 46954 RVA: 0x002A22EB File Offset: 0x002A04EB
		public static LocalizedString ConfirmationMessageDisableUMServerImmediately
		{
			get
			{
				return new LocalizedString("ConfirmationMessageDisableUMServerImmediately", "Ex5524D9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039CA RID: 14794
		// (get) Token: 0x0600B76B RID: 46955 RVA: 0x002A2309 File Offset: 0x002A0509
		public static LocalizedString GatewayIPAddressFamilyInconsistentException
		{
			get
			{
				return new LocalizedString("GatewayIPAddressFamilyInconsistentException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039CB RID: 14795
		// (get) Token: 0x0600B76C RID: 46956 RVA: 0x002A2327 File Offset: 0x002A0527
		public static LocalizedString ConfirmationMessageDisableUMServer
		{
			get
			{
				return new LocalizedString("ConfirmationMessageDisableUMServer", "Ex3CC24E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B76D RID: 46957 RVA: 0x002A2348 File Offset: 0x002A0548
		public static LocalizedString AutoAttendantDisabledEvent(string s)
		{
			return new LocalizedString("AutoAttendantDisabledEvent", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039CC RID: 14796
		// (get) Token: 0x0600B76E RID: 46958 RVA: 0x002A2377 File Offset: 0x002A0577
		public static LocalizedString NoMailboxServersFound
		{
			get
			{
				return new LocalizedString("NoMailboxServersFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B76F RID: 46959 RVA: 0x002A2398 File Offset: 0x002A0598
		public static LocalizedString MakeCallError(string host, string error)
		{
			return new LocalizedString("MakeCallError", "", false, false, Strings.ResourceManager, new object[]
			{
				host,
				error
			});
		}

		// Token: 0x0600B770 RID: 46960 RVA: 0x002A23CC File Offset: 0x002A05CC
		public static LocalizedString ChangesTakeEffectAfterRestartingUmServer(string changedObject, string server, string extraData)
		{
			return new LocalizedString("ChangesTakeEffectAfterRestartingUmServer", "Ex2F1EC6", false, true, Strings.ResourceManager, new object[]
			{
				changedObject,
				server,
				extraData
			});
		}

		// Token: 0x170039CD RID: 14797
		// (get) Token: 0x0600B771 RID: 46961 RVA: 0x002A2403 File Offset: 0x002A0603
		public static LocalizedString SrtpWithoutTls
		{
			get
			{
				return new LocalizedString("SrtpWithoutTls", "Ex7E56C5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x002A2424 File Offset: 0x002A0624
		public static LocalizedString ServerIsPublishingPointException(string dialPlan)
		{
			return new LocalizedString("ServerIsPublishingPointException", "", false, false, Strings.ResourceManager, new object[]
			{
				dialPlan
			});
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x002A2454 File Offset: 0x002A0654
		public static LocalizedString AutoAttendantAlreadEnabledException(string s)
		{
			return new LocalizedString("AutoAttendantAlreadEnabledException", "ExA84098", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B774 RID: 46964 RVA: 0x002A2484 File Offset: 0x002A0684
		public static LocalizedString AutoAttendantAlreadyExistsException(string name, string dialplan)
		{
			return new LocalizedString("AutoAttendantAlreadyExistsException", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				dialplan
			});
		}

		// Token: 0x170039CE RID: 14798
		// (get) Token: 0x0600B775 RID: 46965 RVA: 0x002A24B7 File Offset: 0x002A06B7
		public static LocalizedString ConfirmationMessageDisableUMIPGateway
		{
			get
			{
				return new LocalizedString("ConfirmationMessageDisableUMIPGateway", "Ex46A84A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039CF RID: 14799
		// (get) Token: 0x0600B776 RID: 46966 RVA: 0x002A24D5 File Offset: 0x002A06D5
		public static LocalizedString UninstallUmServiceTask
		{
			get
			{
				return new LocalizedString("UninstallUmServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B777 RID: 46967 RVA: 0x002A24F4 File Offset: 0x002A06F4
		public static LocalizedString ConfirmationMessageRemoveUMDialPlan(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMDialPlan", "Ex1539C6", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x170039D0 RID: 14800
		// (get) Token: 0x0600B778 RID: 46968 RVA: 0x002A2523 File Offset: 0x002A0723
		public static LocalizedString UmServiceDescription
		{
			get
			{
				return new LocalizedString("UmServiceDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B779 RID: 46969 RVA: 0x002A2544 File Offset: 0x002A0744
		public static LocalizedString DiagnosticSequence(string dtmf)
		{
			return new LocalizedString("DiagnosticSequence", "Ex5BAAEA", false, true, Strings.ResourceManager, new object[]
			{
				dtmf
			});
		}

		// Token: 0x0600B77A RID: 46970 RVA: 0x002A2574 File Offset: 0x002A0774
		public static LocalizedString TUILogonfailedToMakeCall(string s)
		{
			return new LocalizedString("TUILogonfailedToMakeCall", "Ex67DE1D", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039D1 RID: 14801
		// (get) Token: 0x0600B77B RID: 46971 RVA: 0x002A25A3 File Offset: 0x002A07A3
		public static LocalizedString DefaultMailboxRequiredWhenForwardTrue
		{
			get
			{
				return new LocalizedString("DefaultMailboxRequiredWhenForwardTrue", "Ex9F9BB7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B77C RID: 46972 RVA: 0x002A25C4 File Offset: 0x002A07C4
		public static LocalizedString MultipleUMMailboxPolicyWithSameId(string s)
		{
			return new LocalizedString("MultipleUMMailboxPolicyWithSameId", "Ex95B89D", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039D2 RID: 14802
		// (get) Token: 0x0600B77D RID: 46973 RVA: 0x002A25F3 File Offset: 0x002A07F3
		public static LocalizedString ConfirmationMessageTestUMConnectivityLocalLoop
		{
			get
			{
				return new LocalizedString("ConfirmationMessageTestUMConnectivityLocalLoop", "Ex140AFE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B77E RID: 46974 RVA: 0x002A2614 File Offset: 0x002A0814
		public static LocalizedString ConfirmationMessageRemoveUMIPGateway(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMIPGateway", "ExE8CFB2", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B77F RID: 46975 RVA: 0x002A2644 File Offset: 0x002A0844
		public static LocalizedString ErrorUMInvalidSipNameAddressFormat(string s)
		{
			return new LocalizedString("ErrorUMInvalidSipNameAddressFormat", "ExFEA28B", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B780 RID: 46976 RVA: 0x002A2674 File Offset: 0x002A0874
		public static LocalizedString NonExistantIPGateway(string s)
		{
			return new LocalizedString("NonExistantIPGateway", "Ex9184CF", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039D3 RID: 14803
		// (get) Token: 0x0600B781 RID: 46977 RVA: 0x002A26A3 File Offset: 0x002A08A3
		public static LocalizedString InvalidDefaultOutboundCallingLineId
		{
			get
			{
				return new LocalizedString("InvalidDefaultOutboundCallingLineId", "Ex983F08", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B782 RID: 46978 RVA: 0x002A26C4 File Offset: 0x002A08C4
		public static LocalizedString ErrorContactAddressListNotFound(string cal)
		{
			return new LocalizedString("ErrorContactAddressListNotFound", "Ex79E0BB", false, true, Strings.ResourceManager, new object[]
			{
				cal
			});
		}

		// Token: 0x0600B783 RID: 46979 RVA: 0x002A26F4 File Offset: 0x002A08F4
		public static LocalizedString DnsResolutionError(string hostName, string message)
		{
			return new LocalizedString("DnsResolutionError", "", false, false, Strings.ResourceManager, new object[]
			{
				hostName,
				message
			});
		}

		// Token: 0x170039D4 RID: 14804
		// (get) Token: 0x0600B784 RID: 46980 RVA: 0x002A2727 File Offset: 0x002A0927
		public static LocalizedString ErrorGeneratingDefaultPassword
		{
			get
			{
				return new LocalizedString("ErrorGeneratingDefaultPassword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B785 RID: 46981 RVA: 0x002A2748 File Offset: 0x002A0948
		public static LocalizedString ErrorObjectNotFound(string s)
		{
			return new LocalizedString("ErrorObjectNotFound", "ExF2BD41", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B786 RID: 46982 RVA: 0x002A2778 File Offset: 0x002A0978
		public static LocalizedString ErrorWeakPasswordHistorySingular(int minLength)
		{
			return new LocalizedString("ErrorWeakPasswordHistorySingular", "", false, false, Strings.ResourceManager, new object[]
			{
				minLength
			});
		}

		// Token: 0x170039D5 RID: 14805
		// (get) Token: 0x0600B787 RID: 46983 RVA: 0x002A27AC File Offset: 0x002A09AC
		public static LocalizedString InvalidDTMFSequenceReceived
		{
			get
			{
				return new LocalizedString("InvalidDTMFSequenceReceived", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B788 RID: 46984 RVA: 0x002A27CC File Offset: 0x002A09CC
		public static LocalizedString ConfirmationMessageNewUMHuntGroup(string Name, string PilotIdentifier, string UMDialPlan, string IPGateway)
		{
			return new LocalizedString("ConfirmationMessageNewUMHuntGroup", "Ex89A6FA", false, true, Strings.ResourceManager, new object[]
			{
				Name,
				PilotIdentifier,
				UMDialPlan,
				IPGateway
			});
		}

		// Token: 0x170039D6 RID: 14806
		// (get) Token: 0x0600B789 RID: 46985 RVA: 0x002A2807 File Offset: 0x002A0A07
		public static LocalizedString UninstallUMWebServiceTask
		{
			get
			{
				return new LocalizedString("UninstallUMWebServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B78A RID: 46986 RVA: 0x002A2828 File Offset: 0x002A0A28
		public static LocalizedString ExceptionUserAlreadyUmEnabled(string s)
		{
			return new LocalizedString("ExceptionUserAlreadyUmEnabled", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B78B RID: 46987 RVA: 0x002A2858 File Offset: 0x002A0A58
		public static LocalizedString ResetUMMailboxError(string user, LocalizedString reason)
		{
			return new LocalizedString("ResetUMMailboxError", "ExE75932", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B78C RID: 46988 RVA: 0x002A2890 File Offset: 0x002A0A90
		public static LocalizedString InvalidAutoAttendantInDialPlan(string s, string d)
		{
			return new LocalizedString("InvalidAutoAttendantInDialPlan", "", false, false, Strings.ResourceManager, new object[]
			{
				s,
				d
			});
		}

		// Token: 0x0600B78D RID: 46989 RVA: 0x002A28C4 File Offset: 0x002A0AC4
		public static LocalizedString ErrorUMInvalidE164AddressFormat(string s)
		{
			return new LocalizedString("ErrorUMInvalidE164AddressFormat", "Ex49FCC2", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039D7 RID: 14807
		// (get) Token: 0x0600B78E RID: 46990 RVA: 0x002A28F3 File Offset: 0x002A0AF3
		public static LocalizedString ADError
		{
			get
			{
				return new LocalizedString("ADError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B78F RID: 46991 RVA: 0x002A2914 File Offset: 0x002A0B14
		public static LocalizedString SendPINResetMailError(string user, LocalizedString reason)
		{
			return new LocalizedString("SendPINResetMailError", "Ex83904B", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B790 RID: 46992 RVA: 0x002A294C File Offset: 0x002A0B4C
		public static LocalizedString Confirm(string userId)
		{
			return new LocalizedString("Confirm", "Ex395C75", false, true, Strings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x0600B791 RID: 46993 RVA: 0x002A297C File Offset: 0x002A0B7C
		public static LocalizedString PINResetfailedToResetPasswd(string s)
		{
			return new LocalizedString("PINResetfailedToResetPasswd", "Ex3F7D35", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B792 RID: 46994 RVA: 0x002A29AC File Offset: 0x002A0BAC
		public static LocalizedString ErrorWeakPasswordNoHistory(int minLength)
		{
			return new LocalizedString("ErrorWeakPasswordNoHistory", "", false, false, Strings.ResourceManager, new object[]
			{
				minLength
			});
		}

		// Token: 0x170039D8 RID: 14808
		// (get) Token: 0x0600B793 RID: 46995 RVA: 0x002A29E0 File Offset: 0x002A0BE0
		public static LocalizedString NotMailboxServer
		{
			get
			{
				return new LocalizedString("NotMailboxServer", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039D9 RID: 14809
		// (get) Token: 0x0600B794 RID: 46996 RVA: 0x002A29FE File Offset: 0x002A0BFE
		public static LocalizedString LanguagesNotPassed
		{
			get
			{
				return new LocalizedString("LanguagesNotPassed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B795 RID: 46997 RVA: 0x002A2A1C File Offset: 0x002A0C1C
		public static LocalizedString InvalidUMUser(string phone, string dialplan)
		{
			return new LocalizedString("InvalidUMUser", "ExE5F49C", false, true, Strings.ResourceManager, new object[]
			{
				phone,
				dialplan
			});
		}

		// Token: 0x0600B796 RID: 46998 RVA: 0x002A2A50 File Offset: 0x002A0C50
		public static LocalizedString ErrorUMInvalidSipNameDomain(string s)
		{
			return new LocalizedString("ErrorUMInvalidSipNameDomain", "Ex64C4F6", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039DA RID: 14810
		// (get) Token: 0x0600B797 RID: 46999 RVA: 0x002A2A7F File Offset: 0x002A0C7F
		public static LocalizedString InstallUmServiceTask
		{
			get
			{
				return new LocalizedString("InstallUmServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B798 RID: 47000 RVA: 0x002A2AA0 File Offset: 0x002A0CA0
		public static LocalizedString InvalidServerVersionForUMRpcTask(string server)
		{
			return new LocalizedString("InvalidServerVersionForUMRpcTask", "Ex0916DC", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170039DB RID: 14811
		// (get) Token: 0x0600B799 RID: 47001 RVA: 0x002A2ACF File Offset: 0x002A0CCF
		public static LocalizedString WaitForFirstDiagnosticResponse
		{
			get
			{
				return new LocalizedString("WaitForFirstDiagnosticResponse", "Ex9D6E1C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B79A RID: 47002 RVA: 0x002A2AF0 File Offset: 0x002A0CF0
		public static LocalizedString MultipleIPGatewaysWithSameId(string s)
		{
			return new LocalizedString("MultipleIPGatewaysWithSameId", "Ex449BE0", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039DC RID: 14812
		// (get) Token: 0x0600B79B RID: 47003 RVA: 0x002A2B1F File Offset: 0x002A0D1F
		public static LocalizedString InvalidTimeZoneParameters
		{
			get
			{
				return new LocalizedString("InvalidTimeZoneParameters", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039DD RID: 14813
		// (get) Token: 0x0600B79C RID: 47004 RVA: 0x002A2B3D File Offset: 0x002A0D3D
		public static LocalizedString CertNotFound
		{
			get
			{
				return new LocalizedString("CertNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039DE RID: 14814
		// (get) Token: 0x0600B79D RID: 47005 RVA: 0x002A2B5B File Offset: 0x002A0D5B
		public static LocalizedString PilotNumberState
		{
			get
			{
				return new LocalizedString("PilotNumberState", "Ex034A59", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039DF RID: 14815
		// (get) Token: 0x0600B79E RID: 47006 RVA: 0x002A2B79 File Offset: 0x002A0D79
		public static LocalizedString KeepProperties
		{
			get
			{
				return new LocalizedString("KeepProperties", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B79F RID: 47007 RVA: 0x002A2B98 File Offset: 0x002A0D98
		public static LocalizedString DuplicateMenuName(string menu)
		{
			return new LocalizedString("DuplicateMenuName", "", false, false, Strings.ResourceManager, new object[]
			{
				menu
			});
		}

		// Token: 0x170039E0 RID: 14816
		// (get) Token: 0x0600B7A0 RID: 47008 RVA: 0x002A2BC7 File Offset: 0x002A0DC7
		public static LocalizedString WaitForDiagnosticResponse
		{
			get
			{
				return new LocalizedString("WaitForDiagnosticResponse", "ExB5BEB1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7A1 RID: 47009 RVA: 0x002A2BE8 File Offset: 0x002A0DE8
		public static LocalizedString FirewallCorrectlyConfigured(string server)
		{
			return new LocalizedString("FirewallCorrectlyConfigured", "ExF0465F", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170039E1 RID: 14817
		// (get) Token: 0x0600B7A2 RID: 47010 RVA: 0x002A2C17 File Offset: 0x002A0E17
		public static LocalizedString UCMAPreReqException
		{
			get
			{
				return new LocalizedString("UCMAPreReqException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7A3 RID: 47011 RVA: 0x002A2C38 File Offset: 0x002A0E38
		public static LocalizedString ChangesTakeEffectAfterRestartingUmCallRouterService(string changedObject, string server, string extraData)
		{
			return new LocalizedString("ChangesTakeEffectAfterRestartingUmCallRouterService", "", false, false, Strings.ResourceManager, new object[]
			{
				changedObject,
				server,
				extraData
			});
		}

		// Token: 0x170039E2 RID: 14818
		// (get) Token: 0x0600B7A4 RID: 47012 RVA: 0x002A2C6F File Offset: 0x002A0E6F
		public static LocalizedString DialPlanAssociatedWithPoliciesException
		{
			get
			{
				return new LocalizedString("DialPlanAssociatedWithPoliciesException", "ExF5AEBA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039E3 RID: 14819
		// (get) Token: 0x0600B7A5 RID: 47013 RVA: 0x002A2C8D File Offset: 0x002A0E8D
		public static LocalizedString PinExpired
		{
			get
			{
				return new LocalizedString("PinExpired", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039E4 RID: 14820
		// (get) Token: 0x0600B7A6 RID: 47014 RVA: 0x002A2CAB File Offset: 0x002A0EAB
		public static LocalizedString LockedOut
		{
			get
			{
				return new LocalizedString("LockedOut", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039E5 RID: 14821
		// (get) Token: 0x0600B7A7 RID: 47015 RVA: 0x002A2CC9 File Offset: 0x002A0EC9
		public static LocalizedString GatewayFqdnNotInAcceptedDomain
		{
			get
			{
				return new LocalizedString("GatewayFqdnNotInAcceptedDomain", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7A8 RID: 47016 RVA: 0x002A2CE8 File Offset: 0x002A0EE8
		public static LocalizedString ScopeErrorOnAutoAttendant(string aa, string reason)
		{
			return new LocalizedString("ScopeErrorOnAutoAttendant", "Ex95FA7F", false, true, Strings.ResourceManager, new object[]
			{
				aa,
				reason
			});
		}

		// Token: 0x0600B7A9 RID: 47017 RVA: 0x002A2D1C File Offset: 0x002A0F1C
		public static LocalizedString DefaultPolicyCreation(string moreInfo)
		{
			return new LocalizedString("DefaultPolicyCreation", "Ex1513EC", false, true, Strings.ResourceManager, new object[]
			{
				moreInfo
			});
		}

		// Token: 0x0600B7AA RID: 47018 RVA: 0x002A2D4C File Offset: 0x002A0F4C
		public static LocalizedString ConfirmationMessageCopyUMCustomPromptDownloadDialPlanPrompts(string TargetPath, string UMDialPlan)
		{
			return new LocalizedString("ConfirmationMessageCopyUMCustomPromptDownloadDialPlanPrompts", "Ex01C3E9", false, true, Strings.ResourceManager, new object[]
			{
				TargetPath,
				UMDialPlan
			});
		}

		// Token: 0x170039E6 RID: 14822
		// (get) Token: 0x0600B7AB RID: 47019 RVA: 0x002A2D7F File Offset: 0x002A0F7F
		public static LocalizedString NoDTMFSwereReceived
		{
			get
			{
				return new LocalizedString("NoDTMFSwereReceived", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7AC RID: 47020 RVA: 0x002A2DA0 File Offset: 0x002A0FA0
		public static LocalizedString DialPlanAssociatedWithAutoAttendantException(string s)
		{
			return new LocalizedString("DialPlanAssociatedWithAutoAttendantException", "ExCD9DAC", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7AD RID: 47021 RVA: 0x002A2DD0 File Offset: 0x002A0FD0
		public static LocalizedString ExceptionUserAlreadyUmDisabled(string s)
		{
			return new LocalizedString("ExceptionUserAlreadyUmDisabled", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7AE RID: 47022 RVA: 0x002A2E00 File Offset: 0x002A1000
		public static LocalizedString AADisableWhatifString(string s)
		{
			return new LocalizedString("AADisableWhatifString", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7AF RID: 47023 RVA: 0x002A2E30 File Offset: 0x002A1030
		public static LocalizedString InitUMMailboxError(string user, LocalizedString reason)
		{
			return new LocalizedString("InitUMMailboxError", "ExBC88DA", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B7B0 RID: 47024 RVA: 0x002A2E68 File Offset: 0x002A1068
		public static LocalizedString SipUriError(string field)
		{
			return new LocalizedString("SipUriError", "", false, false, Strings.ResourceManager, new object[]
			{
				field
			});
		}

		// Token: 0x170039E7 RID: 14823
		// (get) Token: 0x0600B7B1 RID: 47025 RVA: 0x002A2E97 File Offset: 0x002A1097
		public static LocalizedString PasswordMailSubject
		{
			get
			{
				return new LocalizedString("PasswordMailSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039E8 RID: 14824
		// (get) Token: 0x0600B7B2 RID: 47026 RVA: 0x002A2EB5 File Offset: 0x002A10B5
		public static LocalizedString InvalidIPAddressReceived
		{
			get
			{
				return new LocalizedString("InvalidIPAddressReceived", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039E9 RID: 14825
		// (get) Token: 0x0600B7B3 RID: 47027 RVA: 0x002A2ED3 File Offset: 0x002A10D3
		public static LocalizedString InvalidALParameterException
		{
			get
			{
				return new LocalizedString("InvalidALParameterException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7B4 RID: 47028 RVA: 0x002A2EF4 File Offset: 0x002A10F4
		public static LocalizedString ConfirmationMessageCopyUMCustomPromptUploadAutoAttendantPrompts(string Path, string UMAutoAttendant)
		{
			return new LocalizedString("ConfirmationMessageCopyUMCustomPromptUploadAutoAttendantPrompts", "Ex9F7491", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMAutoAttendant
			});
		}

		// Token: 0x170039EA RID: 14826
		// (get) Token: 0x0600B7B5 RID: 47029 RVA: 0x002A2F27 File Offset: 0x002A1127
		public static LocalizedString MustSpecifyThumbprint
		{
			get
			{
				return new LocalizedString("MustSpecifyThumbprint", "Ex8B33C9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039EB RID: 14827
		// (get) Token: 0x0600B7B6 RID: 47030 RVA: 0x002A2F45 File Offset: 0x002A1145
		public static LocalizedString InvalidMailboxServerVersionForTUMCTask
		{
			get
			{
				return new LocalizedString("InvalidMailboxServerVersionForTUMCTask", "Ex4B2908", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039EC RID: 14828
		// (get) Token: 0x0600B7B7 RID: 47031 RVA: 0x002A2F63 File Offset: 0x002A1163
		public static LocalizedString CannotCreateGatewayForHostedSipDialPlan
		{
			get
			{
				return new LocalizedString("CannotCreateGatewayForHostedSipDialPlan", "Ex27F54C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7B8 RID: 47032 RVA: 0x002A2F84 File Offset: 0x002A1184
		public static LocalizedString InvalidMailbox(string mailbox, string setting)
		{
			return new LocalizedString("InvalidMailbox", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox,
				setting
			});
		}

		// Token: 0x0600B7B9 RID: 47033 RVA: 0x002A2FB8 File Offset: 0x002A11B8
		public static LocalizedString ConfirmationMessageExportUMCallDataRecord(string date)
		{
			return new LocalizedString("ConfirmationMessageExportUMCallDataRecord", "ExE568C5", false, true, Strings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x0600B7BA RID: 47034 RVA: 0x002A2FE8 File Offset: 0x002A11E8
		public static LocalizedString UMMailboxPolicyNotPresent(string user)
		{
			return new LocalizedString("UMMailboxPolicyNotPresent", "ExA64168", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x0600B7BB RID: 47035 RVA: 0x002A3018 File Offset: 0x002A1218
		public static LocalizedString InvalidDtmfChar(char c)
		{
			return new LocalizedString("InvalidDtmfChar", "Ex3255FC", false, true, Strings.ResourceManager, new object[]
			{
				c
			});
		}

		// Token: 0x170039ED RID: 14829
		// (get) Token: 0x0600B7BC RID: 47036 RVA: 0x002A304C File Offset: 0x002A124C
		public static LocalizedString ConfirmationMessageDisableUMIPGatewayImmediately
		{
			get
			{
				return new LocalizedString("ConfirmationMessageDisableUMIPGatewayImmediately", "Ex856DF0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7BD RID: 47037 RVA: 0x002A306C File Offset: 0x002A126C
		public static LocalizedString ErrorWeakPasswordHistoryPlural(int minLength, int history)
		{
			return new LocalizedString("ErrorWeakPasswordHistoryPlural", "", false, false, Strings.ResourceManager, new object[]
			{
				minLength,
				history
			});
		}

		// Token: 0x170039EE RID: 14830
		// (get) Token: 0x0600B7BE RID: 47038 RVA: 0x002A30A9 File Offset: 0x002A12A9
		public static LocalizedString PilotNumber
		{
			get
			{
				return new LocalizedString("PilotNumber", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039EF RID: 14831
		// (get) Token: 0x0600B7BF RID: 47039 RVA: 0x002A30C7 File Offset: 0x002A12C7
		public static LocalizedString AttemptingToCreateIPGateway
		{
			get
			{
				return new LocalizedString("AttemptingToCreateIPGateway", "Ex92EE52", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039F0 RID: 14832
		// (get) Token: 0x0600B7C0 RID: 47040 RVA: 0x002A30E5 File Offset: 0x002A12E5
		public static LocalizedString ExceptionUserNotAllowedForUMEnabled
		{
			get
			{
				return new LocalizedString("ExceptionUserNotAllowedForUMEnabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7C1 RID: 47041 RVA: 0x002A3104 File Offset: 0x002A1304
		public static LocalizedString ExceptionIPGatewayIPAddressAlreadyExists(string ipaddress)
		{
			return new LocalizedString("ExceptionIPGatewayIPAddressAlreadyExists", "", false, false, Strings.ResourceManager, new object[]
			{
				ipaddress
			});
		}

		// Token: 0x0600B7C2 RID: 47042 RVA: 0x002A3134 File Offset: 0x002A1334
		public static LocalizedString IPGatewayAlreadDisabledException(string s)
		{
			return new LocalizedString("IPGatewayAlreadDisabledException", "ExC437B0", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039F1 RID: 14833
		// (get) Token: 0x0600B7C3 RID: 47043 RVA: 0x002A3163 File Offset: 0x002A1363
		public static LocalizedString ExchangePrincipalError
		{
			get
			{
				return new LocalizedString("ExchangePrincipalError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7C4 RID: 47044 RVA: 0x002A3184 File Offset: 0x002A1384
		public static LocalizedString ErrorWeakPasswordWithNoCommonPatterns(LocalizedString baseText)
		{
			return new LocalizedString("ErrorWeakPasswordWithNoCommonPatterns", "", false, false, Strings.ResourceManager, new object[]
			{
				baseText
			});
		}

		// Token: 0x170039F2 RID: 14834
		// (get) Token: 0x0600B7C5 RID: 47045 RVA: 0x002A31B8 File Offset: 0x002A13B8
		public static LocalizedString InvalidExternalHostFqdn
		{
			get
			{
				return new LocalizedString("InvalidExternalHostFqdn", "Ex63572E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039F3 RID: 14835
		// (get) Token: 0x0600B7C6 RID: 47046 RVA: 0x002A31D6 File Offset: 0x002A13D6
		public static LocalizedString UCMAPreReqUpgradeException
		{
			get
			{
				return new LocalizedString("UCMAPreReqUpgradeException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039F4 RID: 14836
		// (get) Token: 0x0600B7C7 RID: 47047 RVA: 0x002A31F4 File Offset: 0x002A13F4
		public static LocalizedString AADisableConfirmationString
		{
			get
			{
				return new LocalizedString("AADisableConfirmationString", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039F5 RID: 14837
		// (get) Token: 0x0600B7C8 RID: 47048 RVA: 0x002A3212 File Offset: 0x002A1412
		public static LocalizedString AAAlreadyDisabled
		{
			get
			{
				return new LocalizedString("AAAlreadyDisabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7C9 RID: 47049 RVA: 0x002A3230 File Offset: 0x002A1430
		public static LocalizedString MaxAsrPhraseCountExceeded(string menu)
		{
			return new LocalizedString("MaxAsrPhraseCountExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				menu
			});
		}

		// Token: 0x0600B7CA RID: 47050 RVA: 0x002A3260 File Offset: 0x002A1460
		public static LocalizedString InvalidAAFileExtension(string s)
		{
			return new LocalizedString("InvalidAAFileExtension", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039F6 RID: 14838
		// (get) Token: 0x0600B7CB RID: 47051 RVA: 0x002A328F File Offset: 0x002A148F
		public static LocalizedString ConfirmationMessageTestUMConnectivityPinReset
		{
			get
			{
				return new LocalizedString("ConfirmationMessageTestUMConnectivityPinReset", "Ex152E23", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7CC RID: 47052 RVA: 0x002A32B0 File Offset: 0x002A14B0
		public static LocalizedString SendSequenceError(string error)
		{
			return new LocalizedString("SendSequenceError", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x002A32E0 File Offset: 0x002A14E0
		public static LocalizedString ConfirmationMessageImportUMPromptAutoAttendantPrompts(string Path, string UMAutoAttendant)
		{
			return new LocalizedString("ConfirmationMessageImportUMPromptAutoAttendantPrompts", "Ex406BE4", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMAutoAttendant
			});
		}

		// Token: 0x0600B7CE RID: 47054 RVA: 0x002A3314 File Offset: 0x002A1514
		public static LocalizedString MailboxNotLocal(string userName, string mailboxServer)
		{
			return new LocalizedString("MailboxNotLocal", "", false, false, Strings.ResourceManager, new object[]
			{
				userName,
				mailboxServer
			});
		}

		// Token: 0x0600B7CF RID: 47055 RVA: 0x002A3348 File Offset: 0x002A1548
		public static LocalizedString DuplicateASRPhrase(string phrase)
		{
			return new LocalizedString("DuplicateASRPhrase", "", false, false, Strings.ResourceManager, new object[]
			{
				phrase
			});
		}

		// Token: 0x0600B7D0 RID: 47056 RVA: 0x002A3378 File Offset: 0x002A1578
		public static LocalizedString ErrorServerNotFound(object idStringValue)
		{
			return new LocalizedString("ErrorServerNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600B7D1 RID: 47057 RVA: 0x002A33A8 File Offset: 0x002A15A8
		public static LocalizedString ErrorWeakPassword(string details)
		{
			return new LocalizedString("ErrorWeakPassword", "", false, false, Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x170039F7 RID: 14839
		// (get) Token: 0x0600B7D2 RID: 47058 RVA: 0x002A33D7 File Offset: 0x002A15D7
		public static LocalizedString CertWithoutTls
		{
			get
			{
				return new LocalizedString("CertWithoutTls", "Ex76FB5C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039F8 RID: 14840
		// (get) Token: 0x0600B7D3 RID: 47059 RVA: 0x002A33F5 File Offset: 0x002A15F5
		public static LocalizedString SendEmail
		{
			get
			{
				return new LocalizedString("SendEmail", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7D4 RID: 47060 RVA: 0x002A3414 File Offset: 0x002A1614
		public static LocalizedString ConfirmationMessageSetUMMailboxPIN(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUMMailboxPIN", "Ex14B69B", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B7D5 RID: 47061 RVA: 0x002A3444 File Offset: 0x002A1644
		public static LocalizedString InvalidIPGatewayStateOperationException(string s)
		{
			return new LocalizedString("InvalidIPGatewayStateOperationException", "ExDF0034", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7D6 RID: 47062 RVA: 0x002A3474 File Offset: 0x002A1674
		public static LocalizedString TUILogonfailedToLogon(string s)
		{
			return new LocalizedString("TUILogonfailedToLogon", "Ex9FCD6D", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7D7 RID: 47063 RVA: 0x002A34A4 File Offset: 0x002A16A4
		public static LocalizedString DesktopExperienceRequiredException(string serverFqdn)
		{
			return new LocalizedString("DesktopExperienceRequiredException", "Ex61B65E", false, true, Strings.ResourceManager, new object[]
			{
				serverFqdn
			});
		}

		// Token: 0x0600B7D8 RID: 47064 RVA: 0x002A34D4 File Offset: 0x002A16D4
		public static LocalizedString NewPublishingPointException(string shareName, string moreInfo)
		{
			return new LocalizedString("NewPublishingPointException", "", false, false, Strings.ResourceManager, new object[]
			{
				shareName,
				moreInfo
			});
		}

		// Token: 0x0600B7D9 RID: 47065 RVA: 0x002A3508 File Offset: 0x002A1708
		public static LocalizedString ExceptionIPGatewayInvalid(string ipaddress)
		{
			return new LocalizedString("ExceptionIPGatewayInvalid", "", false, false, Strings.ResourceManager, new object[]
			{
				ipaddress
			});
		}

		// Token: 0x0600B7DA RID: 47066 RVA: 0x002A3538 File Offset: 0x002A1738
		public static LocalizedString ConfirmationMessageSetUMAutoAttendant(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUMAutoAttendant", "ExC46D25", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x170039F9 RID: 14841
		// (get) Token: 0x0600B7DB RID: 47067 RVA: 0x002A3567 File Offset: 0x002A1767
		public static LocalizedString ExceptionSipResourceIdNotUnique
		{
			get
			{
				return new LocalizedString("ExceptionSipResourceIdNotUnique", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7DC RID: 47068 RVA: 0x002A3588 File Offset: 0x002A1788
		public static LocalizedString DuplicateE164PilotIdentifierListEntry(string objectName)
		{
			return new LocalizedString("DuplicateE164PilotIdentifierListEntry", "Ex1893DE", false, true, Strings.ResourceManager, new object[]
			{
				objectName
			});
		}

		// Token: 0x170039FA RID: 14842
		// (get) Token: 0x0600B7DD RID: 47069 RVA: 0x002A35B7 File Offset: 0x002A17B7
		public static LocalizedString PortChanges
		{
			get
			{
				return new LocalizedString("PortChanges", "Ex49211B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170039FB RID: 14843
		// (get) Token: 0x0600B7DE RID: 47070 RVA: 0x002A35D5 File Offset: 0x002A17D5
		public static LocalizedString AANameTooLong
		{
			get
			{
				return new LocalizedString("AANameTooLong", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7DF RID: 47071 RVA: 0x002A35F4 File Offset: 0x002A17F4
		public static LocalizedString ErrorOrganizationalUnitNotUnique(string ou)
		{
			return new LocalizedString("ErrorOrganizationalUnitNotUnique", "ExDA52FD", false, true, Strings.ResourceManager, new object[]
			{
				ou
			});
		}

		// Token: 0x170039FC RID: 14844
		// (get) Token: 0x0600B7E0 RID: 47072 RVA: 0x002A3623 File Offset: 0x002A1823
		public static LocalizedString DefaultUMHuntGroupName
		{
			get
			{
				return new LocalizedString("DefaultUMHuntGroupName", "Ex28450E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7E1 RID: 47073 RVA: 0x002A3644 File Offset: 0x002A1844
		public static LocalizedString ErrorUMPilotIdentifierInUse(string pilotIdentifier, string aa, string dp)
		{
			return new LocalizedString("ErrorUMPilotIdentifierInUse", "Ex33BB1F", false, true, Strings.ResourceManager, new object[]
			{
				pilotIdentifier,
				aa,
				dp
			});
		}

		// Token: 0x0600B7E2 RID: 47074 RVA: 0x002A367C File Offset: 0x002A187C
		public static LocalizedString ConfirmationMessageEnableUMIPGateway(string Identity)
		{
			return new LocalizedString("ConfirmationMessageEnableUMIPGateway", "Ex016190", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B7E3 RID: 47075 RVA: 0x002A36AC File Offset: 0x002A18AC
		public static LocalizedString ChangingMSSMaxDiskCacheSize(string maxDiskCacheSize)
		{
			return new LocalizedString("ChangingMSSMaxDiskCacheSize", "Ex0B61A3", false, true, Strings.ResourceManager, new object[]
			{
				maxDiskCacheSize
			});
		}

		// Token: 0x0600B7E4 RID: 47076 RVA: 0x002A36DC File Offset: 0x002A18DC
		public static LocalizedString ConfirmationMessageImportUMPromptDialPlanPrompts(string Path, string UMDialPlan)
		{
			return new LocalizedString("ConfirmationMessageImportUMPromptDialPlanPrompts", "ExCFE423", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMDialPlan
			});
		}

		// Token: 0x0600B7E5 RID: 47077 RVA: 0x002A3710 File Offset: 0x002A1910
		public static LocalizedString DuplicateKeys(string key)
		{
			return new LocalizedString("DuplicateKeys", "", false, false, Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x0600B7E6 RID: 47078 RVA: 0x002A3740 File Offset: 0x002A1940
		public static LocalizedString DefaultAutoAttendantInDialPlanException(string s)
		{
			return new LocalizedString("DefaultAutoAttendantInDialPlanException", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7E7 RID: 47079 RVA: 0x002A3770 File Offset: 0x002A1970
		public static LocalizedString GenericRPCError(string msg, string server)
		{
			return new LocalizedString("GenericRPCError", "", false, false, Strings.ResourceManager, new object[]
			{
				msg,
				server
			});
		}

		// Token: 0x170039FD RID: 14845
		// (get) Token: 0x0600B7E8 RID: 47080 RVA: 0x002A37A3 File Offset: 0x002A19A3
		public static LocalizedString CouldnotRetreivePasswd
		{
			get
			{
				return new LocalizedString("CouldnotRetreivePasswd", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7E9 RID: 47081 RVA: 0x002A37C4 File Offset: 0x002A19C4
		public static LocalizedString TopologyDiscoveryProblem(string s)
		{
			return new LocalizedString("TopologyDiscoveryProblem", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7EA RID: 47082 RVA: 0x002A37F4 File Offset: 0x002A19F4
		public static LocalizedString NonExistantAutoAttendant(string s)
		{
			return new LocalizedString("NonExistantAutoAttendant", "ExD0D0CA", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7EB RID: 47083 RVA: 0x002A3824 File Offset: 0x002A1A24
		public static LocalizedString MailboxMustBeSpecifiedException(string parameter)
		{
			return new LocalizedString("MailboxMustBeSpecifiedException", "", false, false, Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x170039FE RID: 14846
		// (get) Token: 0x0600B7EC RID: 47084 RVA: 0x002A3853 File Offset: 0x002A1A53
		public static LocalizedString PINEnterState
		{
			get
			{
				return new LocalizedString("PINEnterState", "ExA22288", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7ED RID: 47085 RVA: 0x002A3874 File Offset: 0x002A1A74
		public static LocalizedString InvalidSpeechEnabledAutoAttendant(string s)
		{
			return new LocalizedString("InvalidSpeechEnabledAutoAttendant", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170039FF RID: 14847
		// (get) Token: 0x0600B7EE RID: 47086 RVA: 0x002A38A3 File Offset: 0x002A1AA3
		public static LocalizedString BusinessHoursSettings
		{
			get
			{
				return new LocalizedString("BusinessHoursSettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A00 RID: 14848
		// (get) Token: 0x0600B7EF RID: 47087 RVA: 0x002A38C1 File Offset: 0x002A1AC1
		public static LocalizedString UmServiceStillInstalled
		{
			get
			{
				return new LocalizedString("UmServiceStillInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7F0 RID: 47088 RVA: 0x002A38E0 File Offset: 0x002A1AE0
		public static LocalizedString InitializeError(string error)
		{
			return new LocalizedString("InitializeError", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600B7F1 RID: 47089 RVA: 0x002A3910 File Offset: 0x002A1B10
		public static LocalizedString OperationFailed(string operation)
		{
			return new LocalizedString("OperationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x17003A01 RID: 14849
		// (get) Token: 0x0600B7F2 RID: 47090 RVA: 0x002A393F File Offset: 0x002A1B3F
		public static LocalizedString ConfirmationMessageSetUmCallRouterSettings
		{
			get
			{
				return new LocalizedString("ConfirmationMessageSetUmCallRouterSettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7F3 RID: 47091 RVA: 0x002A3960 File Offset: 0x002A1B60
		public static LocalizedString ValidateGeneratePINError(string user, LocalizedString reason)
		{
			return new LocalizedString("ValidateGeneratePINError", "ExEFEEDF", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B7F4 RID: 47092 RVA: 0x002A3998 File Offset: 0x002A1B98
		public static LocalizedString ErrorOrganizationNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorOrganizationNotFound", "Ex5948C4", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600B7F5 RID: 47093 RVA: 0x002A39C8 File Offset: 0x002A1BC8
		public static LocalizedString TUILogonfailedToGetPin(string s)
		{
			return new LocalizedString("TUILogonfailedToGetPin", "Ex9BC15A", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7F6 RID: 47094 RVA: 0x002A39F8 File Offset: 0x002A1BF8
		public static LocalizedString ErrorOrganizationalUnitNotFound(string ou)
		{
			return new LocalizedString("ErrorOrganizationalUnitNotFound", "Ex05F75C", false, true, Strings.ResourceManager, new object[]
			{
				ou
			});
		}

		// Token: 0x0600B7F7 RID: 47095 RVA: 0x002A3A28 File Offset: 0x002A1C28
		public static LocalizedString NotifyEmailPilotNumberField(string s)
		{
			return new LocalizedString("NotifyEmailPilotNumberField", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7F8 RID: 47096 RVA: 0x002A3A58 File Offset: 0x002A1C58
		public static LocalizedString ConfirmationMessageSetUMDialPlan(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUMDialPlan", "Ex175B50", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A02 RID: 14850
		// (get) Token: 0x0600B7F9 RID: 47097 RVA: 0x002A3A87 File Offset: 0x002A1C87
		public static LocalizedString ValidCertRequiredForUMCallRouter
		{
			get
			{
				return new LocalizedString("ValidCertRequiredForUMCallRouter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7FA RID: 47098 RVA: 0x002A3AA8 File Offset: 0x002A1CA8
		public static LocalizedString InvalidDtmfFallbackAutoAttendantDialPlan(string s)
		{
			return new LocalizedString("InvalidDtmfFallbackAutoAttendantDialPlan", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B7FB RID: 47099 RVA: 0x002A3AD8 File Offset: 0x002A1CD8
		public static LocalizedString ConfirmationMessageTestUMConnectivityEndToEnd(string IPGateway, string Phone)
		{
			return new LocalizedString("ConfirmationMessageTestUMConnectivityEndToEnd", "Ex887C22", false, true, Strings.ResourceManager, new object[]
			{
				IPGateway,
				Phone
			});
		}

		// Token: 0x17003A03 RID: 14851
		// (get) Token: 0x0600B7FC RID: 47100 RVA: 0x002A3B0B File Offset: 0x002A1D0B
		public static LocalizedString DialPlanAssociatedWithUserException
		{
			get
			{
				return new LocalizedString("DialPlanAssociatedWithUserException", "ExFEC83F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A04 RID: 14852
		// (get) Token: 0x0600B7FD RID: 47101 RVA: 0x002A3B29 File Offset: 0x002A1D29
		public static LocalizedString TransferFromTLStoTCPModeWarning
		{
			get
			{
				return new LocalizedString("TransferFromTLStoTCPModeWarning", "Ex79C6E3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A05 RID: 14853
		// (get) Token: 0x0600B7FE RID: 47102 RVA: 0x002A3B47 File Offset: 0x002A1D47
		public static LocalizedString InvalidALParameter
		{
			get
			{
				return new LocalizedString("InvalidALParameter", "Ex13D141", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B7FF RID: 47103 RVA: 0x002A3B68 File Offset: 0x002A1D68
		public static LocalizedString ConfirmationMessageSetUMMailboxConfiguration(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUMMailboxConfiguration", "Ex92BDFC", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B800 RID: 47104 RVA: 0x002A3B98 File Offset: 0x002A1D98
		public static LocalizedString InvalidUMUserName(string email)
		{
			return new LocalizedString("InvalidUMUserName", "ExE9089C", false, true, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x0600B801 RID: 47105 RVA: 0x002A3BC8 File Offset: 0x002A1DC8
		public static LocalizedString ExceptionDialPlanNotFound(string s)
		{
			return new LocalizedString("ExceptionDialPlanNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B802 RID: 47106 RVA: 0x002A3BF8 File Offset: 0x002A1DF8
		public static LocalizedString InvalidDtmfFallbackAutoAttendant_Disabled(string s)
		{
			return new LocalizedString("InvalidDtmfFallbackAutoAttendant_Disabled", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B803 RID: 47107 RVA: 0x002A3C28 File Offset: 0x002A1E28
		public static LocalizedString OperationTimedOutInTestUMConnectivityTask(string operation, string timeout)
		{
			return new LocalizedString("OperationTimedOutInTestUMConnectivityTask", "", false, false, Strings.ResourceManager, new object[]
			{
				operation,
				timeout
			});
		}

		// Token: 0x0600B804 RID: 47108 RVA: 0x002A3C5C File Offset: 0x002A1E5C
		public static LocalizedString UMServerAlreadEnabledException(string s)
		{
			return new LocalizedString("UMServerAlreadEnabledException", "Ex212695", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17003A06 RID: 14854
		// (get) Token: 0x0600B805 RID: 47109 RVA: 0x002A3C8B File Offset: 0x002A1E8B
		public static LocalizedString AfterHoursSettings
		{
			get
			{
				return new LocalizedString("AfterHoursSettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B806 RID: 47110 RVA: 0x002A3CAC File Offset: 0x002A1EAC
		public static LocalizedString ConfirmationMessageDisableUMAutoAttendant(string Identity)
		{
			return new LocalizedString("ConfirmationMessageDisableUMAutoAttendant", "Ex9FFD26", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B807 RID: 47111 RVA: 0x002A3CDC File Offset: 0x002A1EDC
		public static LocalizedString ConfirmationMessageCopyUMCustomPromptUploadDialPlanPrompts(string Path, string UMDialPlan)
		{
			return new LocalizedString("ConfirmationMessageCopyUMCustomPromptUploadDialPlanPrompts", "ExE1969C", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMDialPlan
			});
		}

		// Token: 0x17003A07 RID: 14855
		// (get) Token: 0x0600B808 RID: 47112 RVA: 0x002A3D0F File Offset: 0x002A1F0F
		public static LocalizedString UmCallRouterDescription
		{
			get
			{
				return new LocalizedString("UmCallRouterDescription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A08 RID: 14856
		// (get) Token: 0x0600B809 RID: 47113 RVA: 0x002A3D2D File Offset: 0x002A1F2D
		public static LocalizedString InvalidAutoAttendantScopeSetting
		{
			get
			{
				return new LocalizedString("InvalidAutoAttendantScopeSetting", "ExFD440E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A09 RID: 14857
		// (get) Token: 0x0600B80A RID: 47114 RVA: 0x002A3D4B File Offset: 0x002A1F4B
		public static LocalizedString TcpAndTlsPortsCannotBeSame
		{
			get
			{
				return new LocalizedString("TcpAndTlsPortsCannotBeSame", "Ex174B88", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B80B RID: 47115 RVA: 0x002A3D6C File Offset: 0x002A1F6C
		public static LocalizedString DialPlanAssociatedWithServerException(string s)
		{
			return new LocalizedString("DialPlanAssociatedWithServerException", "Ex3A40FB", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17003A0A RID: 14858
		// (get) Token: 0x0600B80C RID: 47116 RVA: 0x002A3D9B File Offset: 0x002A1F9B
		public static LocalizedString ConfirmationMessageTestUMConnectivityTUILocalLoop
		{
			get
			{
				return new LocalizedString("ConfirmationMessageTestUMConnectivityTUILocalLoop", "Ex4E3109", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A0B RID: 14859
		// (get) Token: 0x0600B80D RID: 47117 RVA: 0x002A3DB9 File Offset: 0x002A1FB9
		public static LocalizedString CurrentTimeZoneIdNotFound
		{
			get
			{
				return new LocalizedString("CurrentTimeZoneIdNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B80E RID: 47118 RVA: 0x002A3DD8 File Offset: 0x002A1FD8
		public static LocalizedString ConfirmationMessageRemoveUMPublishingPoint(string shareName, string hostName)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMPublishingPoint", "Ex8E8159", false, true, Strings.ResourceManager, new object[]
			{
				shareName,
				hostName
			});
		}

		// Token: 0x0600B80F RID: 47119 RVA: 0x002A3E0C File Offset: 0x002A200C
		public static LocalizedString ConfirmationMessageExportUMPromptDialPlanPrompts(string Path, string UMDialPlan)
		{
			return new LocalizedString("ConfirmationMessageExportUMPromptDialPlanPrompts", "ExEDDCE6", false, true, Strings.ResourceManager, new object[]
			{
				Path,
				UMDialPlan
			});
		}

		// Token: 0x0600B810 RID: 47120 RVA: 0x002A3E40 File Offset: 0x002A2040
		public static LocalizedString MultipleServersWithSameId(string s)
		{
			return new LocalizedString("MultipleServersWithSameId", "ExDF8F7B", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B811 RID: 47121 RVA: 0x002A3E70 File Offset: 0x002A2070
		public static LocalizedString RemovePublishingPointException(string shareName, string moreInfo)
		{
			return new LocalizedString("RemovePublishingPointException", "", false, false, Strings.ResourceManager, new object[]
			{
				shareName,
				moreInfo
			});
		}

		// Token: 0x0600B812 RID: 47122 RVA: 0x002A3EA4 File Offset: 0x002A20A4
		public static LocalizedString ConfirmationMessageRemoveUMMailboxPrompt(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveUMMailboxPrompt", "Ex6C326C", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B813 RID: 47123 RVA: 0x002A3ED4 File Offset: 0x002A20D4
		public static LocalizedString UnableToCreateGatewayObjectException(string msg)
		{
			return new LocalizedString("UnableToCreateGatewayObjectException", "ExCD819E", false, true, Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600B814 RID: 47124 RVA: 0x002A3F04 File Offset: 0x002A2104
		public static LocalizedString ConfirmationMessageEnableUMServer(string Identity)
		{
			return new LocalizedString("ConfirmationMessageEnableUMServer", "ExFBC3B9", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B815 RID: 47125 RVA: 0x002A3F34 File Offset: 0x002A2134
		public static LocalizedString DialPlanChangeException(string s)
		{
			return new LocalizedString("DialPlanChangeException", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B816 RID: 47126 RVA: 0x002A3F64 File Offset: 0x002A2164
		public static LocalizedString InvalidServerVersionInDialPlan(string dialplan)
		{
			return new LocalizedString("InvalidServerVersionInDialPlan", "ExAFF016", false, true, Strings.ResourceManager, new object[]
			{
				dialplan
			});
		}

		// Token: 0x0600B817 RID: 47127 RVA: 0x002A3F94 File Offset: 0x002A2194
		public static LocalizedString ErrorServerNotUnique(object idStringValue)
		{
			return new LocalizedString("ErrorServerNotUnique", "", false, false, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x0600B818 RID: 47128 RVA: 0x002A3FC4 File Offset: 0x002A21C4
		public static LocalizedString GetPINInfoError(string user, LocalizedString reason)
		{
			return new LocalizedString("GetPINInfoError", "Ex2AA5F2", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x0600B819 RID: 47129 RVA: 0x002A3FFC File Offset: 0x002A21FC
		public static LocalizedString ConfirmationMessageNewUMAutoAttendant(string Name, string PilotIdentifierList, string UMDialPlan)
		{
			return new LocalizedString("ConfirmationMessageNewUMAutoAttendant", "ExC3F82B", false, true, Strings.ResourceManager, new object[]
			{
				Name,
				PilotIdentifierList,
				UMDialPlan
			});
		}

		// Token: 0x0600B81A RID: 47130 RVA: 0x002A4034 File Offset: 0x002A2234
		public static LocalizedString InvalidDtmfFallbackAutoAttendantSelf(string s)
		{
			return new LocalizedString("InvalidDtmfFallbackAutoAttendantSelf", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B81B RID: 47131 RVA: 0x002A4064 File Offset: 0x002A2264
		public static LocalizedString ExceptionNumericArgumentLengthInvalid(string value, string argument, int maxSize)
		{
			return new LocalizedString("ExceptionNumericArgumentLengthInvalid", "", false, false, Strings.ResourceManager, new object[]
			{
				value,
				argument,
				maxSize
			});
		}

		// Token: 0x17003A0C RID: 14860
		// (get) Token: 0x0600B81C RID: 47132 RVA: 0x002A40A0 File Offset: 0x002A22A0
		public static LocalizedString AttemptingToStampFQDN
		{
			get
			{
				return new LocalizedString("AttemptingToStampFQDN", "Ex789BB0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B81D RID: 47133 RVA: 0x002A40C0 File Offset: 0x002A22C0
		public static LocalizedString PINResetSuccessful(string s)
		{
			return new LocalizedString("PINResetSuccessful", "Ex246F83", false, true, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B81E RID: 47134 RVA: 0x002A40F0 File Offset: 0x002A22F0
		public static LocalizedString InvalidTimeZone(string tzKeyName)
		{
			return new LocalizedString("InvalidTimeZone", "Ex095AE6", false, true, Strings.ResourceManager, new object[]
			{
				tzKeyName
			});
		}

		// Token: 0x17003A0D RID: 14861
		// (get) Token: 0x0600B81F RID: 47135 RVA: 0x002A411F File Offset: 0x002A231F
		public static LocalizedString Pin
		{
			get
			{
				return new LocalizedString("Pin", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B820 RID: 47136 RVA: 0x002A4140 File Offset: 0x002A2340
		public static LocalizedString CannotDisableAutoAttendant(string s)
		{
			return new LocalizedString("CannotDisableAutoAttendant", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B821 RID: 47137 RVA: 0x002A4170 File Offset: 0x002A2370
		public static LocalizedString SubmitWelcomeMailError(string user, LocalizedString reason)
		{
			return new LocalizedString("SubmitWelcomeMailError", "Ex7A9A91", false, true, Strings.ResourceManager, new object[]
			{
				user,
				reason
			});
		}

		// Token: 0x17003A0E RID: 14862
		// (get) Token: 0x0600B822 RID: 47138 RVA: 0x002A41A8 File Offset: 0x002A23A8
		public static LocalizedString NotifyEmail
		{
			get
			{
				return new LocalizedString("NotifyEmail", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B823 RID: 47139 RVA: 0x002A41C8 File Offset: 0x002A23C8
		public static LocalizedString ConfirmationMessageSetUMIPGateway(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetUMIPGateway", "Ex0A1D7E", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B824 RID: 47140 RVA: 0x002A41F8 File Offset: 0x002A23F8
		public static LocalizedString ExceptionHuntGroupAlreadyExists(string ipGateway, string pilotIdentifier)
		{
			return new LocalizedString("ExceptionHuntGroupAlreadyExists", "", false, false, Strings.ResourceManager, new object[]
			{
				ipGateway,
				pilotIdentifier
			});
		}

		// Token: 0x0600B825 RID: 47141 RVA: 0x002A422C File Offset: 0x002A242C
		public static LocalizedString CannotAddNonSipNameDialplanToCallRouter(string s)
		{
			return new LocalizedString("CannotAddNonSipNameDialplanToCallRouter", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B826 RID: 47142 RVA: 0x002A425C File Offset: 0x002A245C
		public static LocalizedString CannotAddNonSipNameDialplan(string s)
		{
			return new LocalizedString("CannotAddNonSipNameDialplan", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B827 RID: 47143 RVA: 0x002A428C File Offset: 0x002A248C
		public static LocalizedString ConfirmationMessageDisableUMMailbox(string Identity)
		{
			return new LocalizedString("ConfirmationMessageDisableUMMailbox", "Ex372C0B", false, true, Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B828 RID: 47144 RVA: 0x002A42BC File Offset: 0x002A24BC
		public static LocalizedString ConfirmationMessageEnableUMMailbox(string Identity, string UMMailboxPolicy)
		{
			return new LocalizedString("ConfirmationMessageEnableUMMailbox", "Ex5A93BD", false, true, Strings.ResourceManager, new object[]
			{
				Identity,
				UMMailboxPolicy
			});
		}

		// Token: 0x17003A0F RID: 14863
		// (get) Token: 0x0600B829 RID: 47145 RVA: 0x002A42EF File Offset: 0x002A24EF
		public static LocalizedString UmServiceName
		{
			get
			{
				return new LocalizedString("UmServiceName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A10 RID: 14864
		// (get) Token: 0x0600B82A RID: 47146 RVA: 0x002A430D File Offset: 0x002A250D
		public static LocalizedString AAAlreadyEnabled
		{
			get
			{
				return new LocalizedString("AAAlreadyEnabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B82B RID: 47147 RVA: 0x002A432C File Offset: 0x002A252C
		public static LocalizedString StatusChangeException(string s)
		{
			return new LocalizedString("StatusChangeException", "", false, false, Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600B82C RID: 47148 RVA: 0x002A435C File Offset: 0x002A255C
		public static LocalizedString UMMailboxPolicyIdNotFound(string id)
		{
			return new LocalizedString("UMMailboxPolicyIdNotFound", "Ex6386DA", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600B82D RID: 47149 RVA: 0x002A438C File Offset: 0x002A258C
		public static LocalizedString ShouldUpgradeObjectVersion(string objtype)
		{
			return new LocalizedString("ShouldUpgradeObjectVersion", "ExD7A795", false, true, Strings.ResourceManager, new object[]
			{
				objtype
			});
		}

		// Token: 0x0600B82E RID: 47150 RVA: 0x002A43BC File Offset: 0x002A25BC
		public static LocalizedString InvalidServerVersionInGateway(string gateway)
		{
			return new LocalizedString("InvalidServerVersionInGateway", "Ex5C141E", false, true, Strings.ResourceManager, new object[]
			{
				gateway
			});
		}

		// Token: 0x0600B82F RID: 47151 RVA: 0x002A43EC File Offset: 0x002A25EC
		public static LocalizedString CallNotAnsweredInTestUMConnectivityTask(string timeout)
		{
			return new LocalizedString("CallNotAnsweredInTestUMConnectivityTask", "", false, false, Strings.ResourceManager, new object[]
			{
				timeout
			});
		}

		// Token: 0x0600B830 RID: 47152 RVA: 0x002A441C File Offset: 0x002A261C
		public static LocalizedString PropertyNotSupportedOnLegacyObjectException(string user, string propname)
		{
			return new LocalizedString("PropertyNotSupportedOnLegacyObjectException", "Ex87FD02", false, true, Strings.ResourceManager, new object[]
			{
				user,
				propname
			});
		}

		// Token: 0x17003A11 RID: 14865
		// (get) Token: 0x0600B831 RID: 47153 RVA: 0x002A444F File Offset: 0x002A264F
		public static LocalizedString TransferFromTCPtoTLSModeWarning
		{
			get
			{
				return new LocalizedString("TransferFromTCPtoTLSModeWarning", "Ex48BB9D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B832 RID: 47154 RVA: 0x002A4470 File Offset: 0x002A2670
		public static LocalizedString ServiceNotStarted(string serviceName)
		{
			return new LocalizedString("ServiceNotStarted", "", false, false, Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x0600B833 RID: 47155 RVA: 0x002A449F File Offset: 0x002A269F
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04006314 RID: 25364
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(100);

		// Token: 0x04006315 RID: 25365
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Tasks.UM.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020011A4 RID: 4516
		public enum IDs : uint
		{
			// Token: 0x04006317 RID: 25367
			ConfirmationMessageStartUMPhoneSession = 781067276U,
			// Token: 0x04006318 RID: 25368
			InstallUMWebServiceTask = 3239495249U,
			// Token: 0x04006319 RID: 25369
			InvalidMethodToDisableAA = 3779087329U,
			// Token: 0x0400631A RID: 25370
			EmptyCountryOrRegionCode = 1689586553U,
			// Token: 0x0400631B RID: 25371
			DefaultMailboxSettings = 1601426056U,
			// Token: 0x0400631C RID: 25372
			ConfirmationMessageStopUMPhoneSession = 530191348U,
			// Token: 0x0400631D RID: 25373
			InstallUmCallRouterTask = 3015522797U,
			// Token: 0x0400631E RID: 25374
			CannotCreateHuntGroupForHostedSipDialPlan = 1999730358U,
			// Token: 0x0400631F RID: 25375
			PasswordMailBody = 3303439652U,
			// Token: 0x04006320 RID: 25376
			ExternalHostFqdnChanges = 987112185U,
			// Token: 0x04006321 RID: 25377
			RemoteEndDisconnected = 3433711682U,
			// Token: 0x04006322 RID: 25378
			SuccessfulLogonState = 887877668U,
			// Token: 0x04006323 RID: 25379
			ValidCertRequiredForUM = 1970930248U,
			// Token: 0x04006324 RID: 25380
			AttemptingToCreateHuntgroup = 454845832U,
			// Token: 0x04006325 RID: 25381
			OperationSuccessful = 2238046577U,
			// Token: 0x04006326 RID: 25382
			CallRouterTransferFromTLStoTCPModeWarning = 3572721584U,
			// Token: 0x04006327 RID: 25383
			UmServiceNotInstalled = 505394326U,
			// Token: 0x04006328 RID: 25384
			UMStartupModeChanges = 3094237923U,
			// Token: 0x04006329 RID: 25385
			PromptProvisioningShareDescription = 2019001798U,
			// Token: 0x0400632A RID: 25386
			UmCallRouterName = 3372841158U,
			// Token: 0x0400632B RID: 25387
			UserProblem = 1223643872U,
			// Token: 0x0400632C RID: 25388
			ConfigureGatewayToForwardCallsMsg = 648648092U,
			// Token: 0x0400632D RID: 25389
			GatewayAddressRequiresFqdn = 939630945U,
			// Token: 0x0400632E RID: 25390
			DNSEntryNotFound = 471142364U,
			// Token: 0x0400632F RID: 25391
			ExceptionInvalidSipNameDomain = 3757387627U,
			// Token: 0x04006330 RID: 25392
			UninstallUmCallRouterTask = 4150015982U,
			// Token: 0x04006331 RID: 25393
			LogonError = 1600651549U,
			// Token: 0x04006332 RID: 25394
			ConfirmationMessageDisableUMServerImmediately = 388296289U,
			// Token: 0x04006333 RID: 25395
			GatewayIPAddressFamilyInconsistentException = 818779485U,
			// Token: 0x04006334 RID: 25396
			ConfirmationMessageDisableUMServer = 2840915457U,
			// Token: 0x04006335 RID: 25397
			NoMailboxServersFound = 1625624441U,
			// Token: 0x04006336 RID: 25398
			SrtpWithoutTls = 1298209242U,
			// Token: 0x04006337 RID: 25399
			ConfirmationMessageDisableUMIPGateway = 3114226637U,
			// Token: 0x04006338 RID: 25400
			UninstallUmServiceTask = 2388018184U,
			// Token: 0x04006339 RID: 25401
			UmServiceDescription = 2187708795U,
			// Token: 0x0400633A RID: 25402
			DefaultMailboxRequiredWhenForwardTrue = 3944137335U,
			// Token: 0x0400633B RID: 25403
			ConfirmationMessageTestUMConnectivityLocalLoop = 4164812292U,
			// Token: 0x0400633C RID: 25404
			InvalidDefaultOutboundCallingLineId = 587456431U,
			// Token: 0x0400633D RID: 25405
			ErrorGeneratingDefaultPassword = 193099536U,
			// Token: 0x0400633E RID: 25406
			InvalidDTMFSequenceReceived = 3703544840U,
			// Token: 0x0400633F RID: 25407
			UninstallUMWebServiceTask = 2970756378U,
			// Token: 0x04006340 RID: 25408
			ADError = 1263770381U,
			// Token: 0x04006341 RID: 25409
			NotMailboxServer = 48881282U,
			// Token: 0x04006342 RID: 25410
			LanguagesNotPassed = 2410305798U,
			// Token: 0x04006343 RID: 25411
			InstallUmServiceTask = 1691564973U,
			// Token: 0x04006344 RID: 25412
			WaitForFirstDiagnosticResponse = 2718089772U,
			// Token: 0x04006345 RID: 25413
			InvalidTimeZoneParameters = 940435338U,
			// Token: 0x04006346 RID: 25414
			CertNotFound = 1355103499U,
			// Token: 0x04006347 RID: 25415
			PilotNumberState = 2010047074U,
			// Token: 0x04006348 RID: 25416
			KeepProperties = 1658738722U,
			// Token: 0x04006349 RID: 25417
			WaitForDiagnosticResponse = 2919065030U,
			// Token: 0x0400634A RID: 25418
			UCMAPreReqException = 2675709228U,
			// Token: 0x0400634B RID: 25419
			DialPlanAssociatedWithPoliciesException = 2660011992U,
			// Token: 0x0400634C RID: 25420
			PinExpired = 1878527290U,
			// Token: 0x0400634D RID: 25421
			LockedOut = 1151155524U,
			// Token: 0x0400634E RID: 25422
			GatewayFqdnNotInAcceptedDomain = 100754582U,
			// Token: 0x0400634F RID: 25423
			NoDTMFSwereReceived = 918425027U,
			// Token: 0x04006350 RID: 25424
			PasswordMailSubject = 1885509224U,
			// Token: 0x04006351 RID: 25425
			InvalidIPAddressReceived = 2380514321U,
			// Token: 0x04006352 RID: 25426
			InvalidALParameterException = 943497584U,
			// Token: 0x04006353 RID: 25427
			MustSpecifyThumbprint = 3527640475U,
			// Token: 0x04006354 RID: 25428
			InvalidMailboxServerVersionForTUMCTask = 3746187495U,
			// Token: 0x04006355 RID: 25429
			CannotCreateGatewayForHostedSipDialPlan = 886805380U,
			// Token: 0x04006356 RID: 25430
			ConfirmationMessageDisableUMIPGatewayImmediately = 2203312157U,
			// Token: 0x04006357 RID: 25431
			PilotNumber = 1602032641U,
			// Token: 0x04006358 RID: 25432
			AttemptingToCreateIPGateway = 1762496243U,
			// Token: 0x04006359 RID: 25433
			ExceptionUserNotAllowedForUMEnabled = 505861453U,
			// Token: 0x0400635A RID: 25434
			ExchangePrincipalError = 305424905U,
			// Token: 0x0400635B RID: 25435
			InvalidExternalHostFqdn = 2568008289U,
			// Token: 0x0400635C RID: 25436
			UCMAPreReqUpgradeException = 912893922U,
			// Token: 0x0400635D RID: 25437
			AADisableConfirmationString = 4252462372U,
			// Token: 0x0400635E RID: 25438
			AAAlreadyDisabled = 3860731788U,
			// Token: 0x0400635F RID: 25439
			ConfirmationMessageTestUMConnectivityPinReset = 3372908061U,
			// Token: 0x04006360 RID: 25440
			CertWithoutTls = 1942717475U,
			// Token: 0x04006361 RID: 25441
			SendEmail = 1061463472U,
			// Token: 0x04006362 RID: 25442
			ExceptionSipResourceIdNotUnique = 3845881662U,
			// Token: 0x04006363 RID: 25443
			PortChanges = 55026498U,
			// Token: 0x04006364 RID: 25444
			AANameTooLong = 577419765U,
			// Token: 0x04006365 RID: 25445
			DefaultUMHuntGroupName = 1006009848U,
			// Token: 0x04006366 RID: 25446
			CouldnotRetreivePasswd = 1694996750U,
			// Token: 0x04006367 RID: 25447
			PINEnterState = 1731989106U,
			// Token: 0x04006368 RID: 25448
			BusinessHoursSettings = 3858123826U,
			// Token: 0x04006369 RID: 25449
			UmServiceStillInstalled = 2511055751U,
			// Token: 0x0400636A RID: 25450
			ConfirmationMessageSetUmCallRouterSettings = 1003963056U,
			// Token: 0x0400636B RID: 25451
			ValidCertRequiredForUMCallRouter = 160860353U,
			// Token: 0x0400636C RID: 25452
			DialPlanAssociatedWithUserException = 3650433099U,
			// Token: 0x0400636D RID: 25453
			TransferFromTLStoTCPModeWarning = 73835935U,
			// Token: 0x0400636E RID: 25454
			InvalidALParameter = 4163764725U,
			// Token: 0x0400636F RID: 25455
			AfterHoursSettings = 154856458U,
			// Token: 0x04006370 RID: 25456
			UmCallRouterDescription = 3666462471U,
			// Token: 0x04006371 RID: 25457
			InvalidAutoAttendantScopeSetting = 3969155231U,
			// Token: 0x04006372 RID: 25458
			TcpAndTlsPortsCannotBeSame = 3358569313U,
			// Token: 0x04006373 RID: 25459
			ConfirmationMessageTestUMConnectivityTUILocalLoop = 3177175916U,
			// Token: 0x04006374 RID: 25460
			CurrentTimeZoneIdNotFound = 2342320894U,
			// Token: 0x04006375 RID: 25461
			AttemptingToStampFQDN = 975840932U,
			// Token: 0x04006376 RID: 25462
			Pin = 339800695U,
			// Token: 0x04006377 RID: 25463
			NotifyEmail = 4175977607U,
			// Token: 0x04006378 RID: 25464
			UmServiceName = 1074457952U,
			// Token: 0x04006379 RID: 25465
			AAAlreadyEnabled = 2152868767U,
			// Token: 0x0400637A RID: 25466
			TransferFromTCPtoTLSModeWarning = 1304023191U
		}

		// Token: 0x020011A5 RID: 4517
		private enum ParamIDs
		{
			// Token: 0x0400637C RID: 25468
			DisabledLinkedAutoAttendant,
			// Token: 0x0400637D RID: 25469
			MultipleAutoAttendantsWithSameId,
			// Token: 0x0400637E RID: 25470
			UMServiceDisabledException,
			// Token: 0x0400637F RID: 25471
			SourceFileOpenException,
			// Token: 0x04006380 RID: 25472
			InvalidDtmfFallbackAutoAttendant,
			// Token: 0x04006381 RID: 25473
			MultipleDialplansWithSameId,
			// Token: 0x04006382 RID: 25474
			ConfirmationMessageExportUMMailboxPrompt,
			// Token: 0x04006383 RID: 25475
			CannotDisableAutoAttendant_KeyMapping,
			// Token: 0x04006384 RID: 25476
			ExceptionSIPResouceIdConflictWithExistingValue,
			// Token: 0x04006385 RID: 25477
			PINResetfailedToResetPin,
			// Token: 0x04006386 RID: 25478
			RpcNotRegistered,
			// Token: 0x04006387 RID: 25479
			InvalidUMServerStateOperationException,
			// Token: 0x04006388 RID: 25480
			ErrorUMInvalidExtensionFormat,
			// Token: 0x04006389 RID: 25481
			SavePINError,
			// Token: 0x0400638A RID: 25482
			AutoAttendantAlreadDisabledException,
			// Token: 0x0400638B RID: 25483
			UnableToSetMSSRegistryValue,
			// Token: 0x0400638C RID: 25484
			ConfirmationMessageEnableUMAutoAttendant,
			// Token: 0x0400638D RID: 25485
			DialPlanAssociatedWithIPGatewayException,
			// Token: 0x0400638E RID: 25486
			TUILogonSuccessful,
			// Token: 0x0400638F RID: 25487
			SIPFEServerConfigurationNotFound,
			// Token: 0x04006390 RID: 25488
			ConfirmationMessageCopyUMCustomPromptDownloadAutoAttendantPrompts,
			// Token: 0x04006391 RID: 25489
			AutoAttendantEnabledEvent,
			// Token: 0x04006392 RID: 25490
			DPLinkedGwNotFQDN,
			// Token: 0x04006393 RID: 25491
			ExceptionIPGatewayAlreadyExists,
			// Token: 0x04006394 RID: 25492
			ConfirmationMessageExportUMPromptAutoAttendantPrompts,
			// Token: 0x04006395 RID: 25493
			InvalidLanguageIdException,
			// Token: 0x04006396 RID: 25494
			InvalidAutoAttendant,
			// Token: 0x04006397 RID: 25495
			RpcUnavailable,
			// Token: 0x04006398 RID: 25496
			FailedToEstablishMedia,
			// Token: 0x04006399 RID: 25497
			IPGatewayAlreadEnabledException,
			// Token: 0x0400639A RID: 25498
			DefaultLanguageNotAvailable,
			// Token: 0x0400639B RID: 25499
			MismatchedOrgInDPAndGW,
			// Token: 0x0400639C RID: 25500
			ConfirmationMessageRemoveUMHuntGroup,
			// Token: 0x0400639D RID: 25501
			ConfirmationMessageNewUMDialPlan,
			// Token: 0x0400639E RID: 25502
			MaxAsrPhraseLengthExceeded,
			// Token: 0x0400639F RID: 25503
			ErrorOrganizationNotUnique,
			// Token: 0x040063A0 RID: 25504
			OperationNotSupportedOnLegacMailboxException,
			// Token: 0x040063A1 RID: 25505
			SipOptionsError,
			// Token: 0x040063A2 RID: 25506
			ConfirmationMessageSetUmServer,
			// Token: 0x040063A3 RID: 25507
			NonExistantServer,
			// Token: 0x040063A4 RID: 25508
			ErrorContactAddressListNotUnique,
			// Token: 0x040063A5 RID: 25509
			DefaultUMMailboxPolicyName,
			// Token: 0x040063A6 RID: 25510
			DefaultPolicyCreationNameTooLong,
			// Token: 0x040063A7 RID: 25511
			CallAnsweringRuleNotFoundException,
			// Token: 0x040063A8 RID: 25512
			EmptyASRPhrase,
			// Token: 0x040063A9 RID: 25513
			ConfirmationMessageNewUMIPGateway,
			// Token: 0x040063AA RID: 25514
			NonExistantDialPlan,
			// Token: 0x040063AB RID: 25515
			ConfirmationMessageRemoveUMAutoAttendant,
			// Token: 0x040063AC RID: 25516
			UMServerAlreadDisabledException,
			// Token: 0x040063AD RID: 25517
			AutoAttendantDisabledEvent,
			// Token: 0x040063AE RID: 25518
			MakeCallError,
			// Token: 0x040063AF RID: 25519
			ChangesTakeEffectAfterRestartingUmServer,
			// Token: 0x040063B0 RID: 25520
			ServerIsPublishingPointException,
			// Token: 0x040063B1 RID: 25521
			AutoAttendantAlreadEnabledException,
			// Token: 0x040063B2 RID: 25522
			AutoAttendantAlreadyExistsException,
			// Token: 0x040063B3 RID: 25523
			ConfirmationMessageRemoveUMDialPlan,
			// Token: 0x040063B4 RID: 25524
			DiagnosticSequence,
			// Token: 0x040063B5 RID: 25525
			TUILogonfailedToMakeCall,
			// Token: 0x040063B6 RID: 25526
			MultipleUMMailboxPolicyWithSameId,
			// Token: 0x040063B7 RID: 25527
			ConfirmationMessageRemoveUMIPGateway,
			// Token: 0x040063B8 RID: 25528
			ErrorUMInvalidSipNameAddressFormat,
			// Token: 0x040063B9 RID: 25529
			NonExistantIPGateway,
			// Token: 0x040063BA RID: 25530
			ErrorContactAddressListNotFound,
			// Token: 0x040063BB RID: 25531
			DnsResolutionError,
			// Token: 0x040063BC RID: 25532
			ErrorObjectNotFound,
			// Token: 0x040063BD RID: 25533
			ErrorWeakPasswordHistorySingular,
			// Token: 0x040063BE RID: 25534
			ConfirmationMessageNewUMHuntGroup,
			// Token: 0x040063BF RID: 25535
			ExceptionUserAlreadyUmEnabled,
			// Token: 0x040063C0 RID: 25536
			ResetUMMailboxError,
			// Token: 0x040063C1 RID: 25537
			InvalidAutoAttendantInDialPlan,
			// Token: 0x040063C2 RID: 25538
			ErrorUMInvalidE164AddressFormat,
			// Token: 0x040063C3 RID: 25539
			SendPINResetMailError,
			// Token: 0x040063C4 RID: 25540
			Confirm,
			// Token: 0x040063C5 RID: 25541
			PINResetfailedToResetPasswd,
			// Token: 0x040063C6 RID: 25542
			ErrorWeakPasswordNoHistory,
			// Token: 0x040063C7 RID: 25543
			InvalidUMUser,
			// Token: 0x040063C8 RID: 25544
			ErrorUMInvalidSipNameDomain,
			// Token: 0x040063C9 RID: 25545
			InvalidServerVersionForUMRpcTask,
			// Token: 0x040063CA RID: 25546
			MultipleIPGatewaysWithSameId,
			// Token: 0x040063CB RID: 25547
			DuplicateMenuName,
			// Token: 0x040063CC RID: 25548
			FirewallCorrectlyConfigured,
			// Token: 0x040063CD RID: 25549
			ChangesTakeEffectAfterRestartingUmCallRouterService,
			// Token: 0x040063CE RID: 25550
			ScopeErrorOnAutoAttendant,
			// Token: 0x040063CF RID: 25551
			DefaultPolicyCreation,
			// Token: 0x040063D0 RID: 25552
			ConfirmationMessageCopyUMCustomPromptDownloadDialPlanPrompts,
			// Token: 0x040063D1 RID: 25553
			DialPlanAssociatedWithAutoAttendantException,
			// Token: 0x040063D2 RID: 25554
			ExceptionUserAlreadyUmDisabled,
			// Token: 0x040063D3 RID: 25555
			AADisableWhatifString,
			// Token: 0x040063D4 RID: 25556
			InitUMMailboxError,
			// Token: 0x040063D5 RID: 25557
			SipUriError,
			// Token: 0x040063D6 RID: 25558
			ConfirmationMessageCopyUMCustomPromptUploadAutoAttendantPrompts,
			// Token: 0x040063D7 RID: 25559
			InvalidMailbox,
			// Token: 0x040063D8 RID: 25560
			ConfirmationMessageExportUMCallDataRecord,
			// Token: 0x040063D9 RID: 25561
			UMMailboxPolicyNotPresent,
			// Token: 0x040063DA RID: 25562
			InvalidDtmfChar,
			// Token: 0x040063DB RID: 25563
			ErrorWeakPasswordHistoryPlural,
			// Token: 0x040063DC RID: 25564
			ExceptionIPGatewayIPAddressAlreadyExists,
			// Token: 0x040063DD RID: 25565
			IPGatewayAlreadDisabledException,
			// Token: 0x040063DE RID: 25566
			ErrorWeakPasswordWithNoCommonPatterns,
			// Token: 0x040063DF RID: 25567
			MaxAsrPhraseCountExceeded,
			// Token: 0x040063E0 RID: 25568
			InvalidAAFileExtension,
			// Token: 0x040063E1 RID: 25569
			SendSequenceError,
			// Token: 0x040063E2 RID: 25570
			ConfirmationMessageImportUMPromptAutoAttendantPrompts,
			// Token: 0x040063E3 RID: 25571
			MailboxNotLocal,
			// Token: 0x040063E4 RID: 25572
			DuplicateASRPhrase,
			// Token: 0x040063E5 RID: 25573
			ErrorServerNotFound,
			// Token: 0x040063E6 RID: 25574
			ErrorWeakPassword,
			// Token: 0x040063E7 RID: 25575
			ConfirmationMessageSetUMMailboxPIN,
			// Token: 0x040063E8 RID: 25576
			InvalidIPGatewayStateOperationException,
			// Token: 0x040063E9 RID: 25577
			TUILogonfailedToLogon,
			// Token: 0x040063EA RID: 25578
			DesktopExperienceRequiredException,
			// Token: 0x040063EB RID: 25579
			NewPublishingPointException,
			// Token: 0x040063EC RID: 25580
			ExceptionIPGatewayInvalid,
			// Token: 0x040063ED RID: 25581
			ConfirmationMessageSetUMAutoAttendant,
			// Token: 0x040063EE RID: 25582
			DuplicateE164PilotIdentifierListEntry,
			// Token: 0x040063EF RID: 25583
			ErrorOrganizationalUnitNotUnique,
			// Token: 0x040063F0 RID: 25584
			ErrorUMPilotIdentifierInUse,
			// Token: 0x040063F1 RID: 25585
			ConfirmationMessageEnableUMIPGateway,
			// Token: 0x040063F2 RID: 25586
			ChangingMSSMaxDiskCacheSize,
			// Token: 0x040063F3 RID: 25587
			ConfirmationMessageImportUMPromptDialPlanPrompts,
			// Token: 0x040063F4 RID: 25588
			DuplicateKeys,
			// Token: 0x040063F5 RID: 25589
			DefaultAutoAttendantInDialPlanException,
			// Token: 0x040063F6 RID: 25590
			GenericRPCError,
			// Token: 0x040063F7 RID: 25591
			TopologyDiscoveryProblem,
			// Token: 0x040063F8 RID: 25592
			NonExistantAutoAttendant,
			// Token: 0x040063F9 RID: 25593
			MailboxMustBeSpecifiedException,
			// Token: 0x040063FA RID: 25594
			InvalidSpeechEnabledAutoAttendant,
			// Token: 0x040063FB RID: 25595
			InitializeError,
			// Token: 0x040063FC RID: 25596
			OperationFailed,
			// Token: 0x040063FD RID: 25597
			ValidateGeneratePINError,
			// Token: 0x040063FE RID: 25598
			ErrorOrganizationNotFound,
			// Token: 0x040063FF RID: 25599
			TUILogonfailedToGetPin,
			// Token: 0x04006400 RID: 25600
			ErrorOrganizationalUnitNotFound,
			// Token: 0x04006401 RID: 25601
			NotifyEmailPilotNumberField,
			// Token: 0x04006402 RID: 25602
			ConfirmationMessageSetUMDialPlan,
			// Token: 0x04006403 RID: 25603
			InvalidDtmfFallbackAutoAttendantDialPlan,
			// Token: 0x04006404 RID: 25604
			ConfirmationMessageTestUMConnectivityEndToEnd,
			// Token: 0x04006405 RID: 25605
			ConfirmationMessageSetUMMailboxConfiguration,
			// Token: 0x04006406 RID: 25606
			InvalidUMUserName,
			// Token: 0x04006407 RID: 25607
			ExceptionDialPlanNotFound,
			// Token: 0x04006408 RID: 25608
			InvalidDtmfFallbackAutoAttendant_Disabled,
			// Token: 0x04006409 RID: 25609
			OperationTimedOutInTestUMConnectivityTask,
			// Token: 0x0400640A RID: 25610
			UMServerAlreadEnabledException,
			// Token: 0x0400640B RID: 25611
			ConfirmationMessageDisableUMAutoAttendant,
			// Token: 0x0400640C RID: 25612
			ConfirmationMessageCopyUMCustomPromptUploadDialPlanPrompts,
			// Token: 0x0400640D RID: 25613
			DialPlanAssociatedWithServerException,
			// Token: 0x0400640E RID: 25614
			ConfirmationMessageRemoveUMPublishingPoint,
			// Token: 0x0400640F RID: 25615
			ConfirmationMessageExportUMPromptDialPlanPrompts,
			// Token: 0x04006410 RID: 25616
			MultipleServersWithSameId,
			// Token: 0x04006411 RID: 25617
			RemovePublishingPointException,
			// Token: 0x04006412 RID: 25618
			ConfirmationMessageRemoveUMMailboxPrompt,
			// Token: 0x04006413 RID: 25619
			UnableToCreateGatewayObjectException,
			// Token: 0x04006414 RID: 25620
			ConfirmationMessageEnableUMServer,
			// Token: 0x04006415 RID: 25621
			DialPlanChangeException,
			// Token: 0x04006416 RID: 25622
			InvalidServerVersionInDialPlan,
			// Token: 0x04006417 RID: 25623
			ErrorServerNotUnique,
			// Token: 0x04006418 RID: 25624
			GetPINInfoError,
			// Token: 0x04006419 RID: 25625
			ConfirmationMessageNewUMAutoAttendant,
			// Token: 0x0400641A RID: 25626
			InvalidDtmfFallbackAutoAttendantSelf,
			// Token: 0x0400641B RID: 25627
			ExceptionNumericArgumentLengthInvalid,
			// Token: 0x0400641C RID: 25628
			PINResetSuccessful,
			// Token: 0x0400641D RID: 25629
			InvalidTimeZone,
			// Token: 0x0400641E RID: 25630
			CannotDisableAutoAttendant,
			// Token: 0x0400641F RID: 25631
			SubmitWelcomeMailError,
			// Token: 0x04006420 RID: 25632
			ConfirmationMessageSetUMIPGateway,
			// Token: 0x04006421 RID: 25633
			ExceptionHuntGroupAlreadyExists,
			// Token: 0x04006422 RID: 25634
			CannotAddNonSipNameDialplanToCallRouter,
			// Token: 0x04006423 RID: 25635
			CannotAddNonSipNameDialplan,
			// Token: 0x04006424 RID: 25636
			ConfirmationMessageDisableUMMailbox,
			// Token: 0x04006425 RID: 25637
			ConfirmationMessageEnableUMMailbox,
			// Token: 0x04006426 RID: 25638
			StatusChangeException,
			// Token: 0x04006427 RID: 25639
			UMMailboxPolicyIdNotFound,
			// Token: 0x04006428 RID: 25640
			ShouldUpgradeObjectVersion,
			// Token: 0x04006429 RID: 25641
			InvalidServerVersionInGateway,
			// Token: 0x0400642A RID: 25642
			CallNotAnsweredInTestUMConnectivityTask,
			// Token: 0x0400642B RID: 25643
			PropertyNotSupportedOnLegacyObjectException,
			// Token: 0x0400642C RID: 25644
			ServiceNotStarted
		}
	}
}
