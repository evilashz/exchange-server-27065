using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon.Exceptions
{
	// Token: 0x02000193 RID: 403
	internal static class Strings
	{
		// Token: 0x06000D85 RID: 3461 RVA: 0x000330D0 File Offset: 0x000312D0
		static Strings()
		{
			Strings.stringIDs.Add(1713941734U, "ValidateOrGeneratePINRequest");
			Strings.stringIDs.Add(2723036908U, "ExceptionInvalidSipNameResourceId");
			Strings.stringIDs.Add(50529680U, "InvalidADRecipientException");
			Strings.stringIDs.Add(437391379U, "GetCallInfoRequest");
			Strings.stringIDs.Add(456952916U, "PromptPreviewRpcRequest");
			Strings.stringIDs.Add(3129803840U, "DeleteFailed");
			Strings.stringIDs.Add(3755207777U, "InvalidCallIdException");
			Strings.stringIDs.Add(1007247767U, "ContentIndexingNotEnabled");
			Strings.stringIDs.Add(2554681784U, "ClientAccessException");
			Strings.stringIDs.Add(3560634689U, "UMRPCIncompatibleVersionException");
			Strings.stringIDs.Add(3827073469U, "InitializeUMMailboxRequest");
			Strings.stringIDs.Add(1575129900U, "UnSecured");
			Strings.stringIDs.Add(1171028813U, "InvalidPAA");
			Strings.stringIDs.Add(1404156949U, "PlayOnPhoneGreetingRequest");
			Strings.stringIDs.Add(3153985174U, "SubmitWelcomeMessageRequest");
			Strings.stringIDs.Add(1559723317U, "DisableUMMailboxRequest");
			Strings.stringIDs.Add(2262233287U, "ExceptionInvalidE164ResourceId");
			Strings.stringIDs.Add(2807255575U, "OverPlayOnPhoneCallLimitException");
			Strings.stringIDs.Add(2987232564U, "ExceptionSipResourceIdNotNeeded");
			Strings.stringIDs.Add(954056551U, "ProcessPartnerMessageRequest");
			Strings.stringIDs.Add(913692915U, "AutoAttendantPromptRequest");
			Strings.stringIDs.Add(2355518126U, "GetPINInfoRequest");
			Strings.stringIDs.Add(1929284865U, "DisconnectRequest");
			Strings.stringIDs.Add(4142400168U, "DialingRulesException");
			Strings.stringIDs.Add(1333967066U, "AutoAttendantBusinessHoursPromptRequest");
			Strings.stringIDs.Add(3615365293U, "ExceptionE164ResourceIdNeeded");
			Strings.stringIDs.Add(634056442U, "AutoAttendantBusinessLocationPromptRequest");
			Strings.stringIDs.Add(3099272000U, "PasswordDerivedBytesNeedNonNegNum");
			Strings.stringIDs.Add(1057562681U, "PlayOnPhoneAAGreetingRequest");
			Strings.stringIDs.Add(1298000829U, "UnsupportedCustomGreetingWaveFormat");
			Strings.stringIDs.Add(1479848360U, "ExceptionInvalidSipUri");
			Strings.stringIDs.Add(2382391216U, "ADAccessFailed");
			Strings.stringIDs.Add(2474709144U, "AutoAttendantCustomPromptRequest");
			Strings.stringIDs.Add(2965802745U, "AutoAttendantWelcomePromptRequest");
			Strings.stringIDs.Add(49929337U, "TamperedPin");
			Strings.stringIDs.Add(3060719176U, "SubmitPINResetMessageRequest");
			Strings.stringIDs.Add(3132314599U, "PlayOnPhoneMessageRequest");
			Strings.stringIDs.Add(2380102598U, "InvalidPrincipalException");
			Strings.stringIDs.Add(4275120716U, "RpcUMServerNotFoundException");
			Strings.stringIDs.Add(1158496135U, "SipResourceIdAndExtensionsNeeded");
			Strings.stringIDs.Add(1496370889U, "PlayOnPhonePAAGreetingRequest");
			Strings.stringIDs.Add(229602390U, "NoCallerIdToUseException");
			Strings.stringIDs.Add(2125881321U, "UndeleteFailed");
			Strings.stringIDs.Add(1651664428U, "UMDataStorageMailboxNotFound");
			Strings.stringIDs.Add(1738969940U, "InvalidObjectIdException");
			Strings.stringIDs.Add(1136924537U, "UMMailboxPromptRequest");
			Strings.stringIDs.Add(3151943388U, "EWSNoResponseReceived");
			Strings.stringIDs.Add(4186469259U, "CorruptedPAAStore");
			Strings.stringIDs.Add(2584780874U, "CallIdNull");
			Strings.stringIDs.Add(2625829937U, "ExceptionInvalidPhoneNumber");
			Strings.stringIDs.Add(1241597555U, "Secured");
			Strings.stringIDs.Add(267464401U, "IPGatewayNotFoundException");
			Strings.stringIDs.Add(1196599U, "UnsupportedCustomGreetingWmaFormat");
			Strings.stringIDs.Add(143404757U, "ExceptionCouldNotGenerateExtension");
			Strings.stringIDs.Add(396119749U, "UndeleteNotFound");
			Strings.stringIDs.Add(3947493446U, "InvalidUMAutoAttendantException");
			Strings.stringIDs.Add(2003854661U, "UMGray");
			Strings.stringIDs.Add(4078673759U, "SavePINRequest");
			Strings.stringIDs.Add(1064401785U, "ResetPINException");
			Strings.stringIDs.Add(4195133953U, "PasswordDerivedBytesTooManyBytes");
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000335BC File Offset: 0x000317BC
		public static LocalizedString UMMbxPolicyNotFound(string policy, string user)
		{
			return new LocalizedString("UMMbxPolicyNotFound", Strings.ResourceManager, new object[]
			{
				policy,
				user
			});
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x000335E8 File Offset: 0x000317E8
		public static LocalizedString ValidateOrGeneratePINRequest
		{
			get
			{
				return new LocalizedString("ValidateOrGeneratePINRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000335FF File Offset: 0x000317FF
		public static LocalizedString ExceptionInvalidSipNameResourceId
		{
			get
			{
				return new LocalizedString("ExceptionInvalidSipNameResourceId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00033618 File Offset: 0x00031818
		public static LocalizedString InvalidTenantGuidException(Guid tenantGuid)
		{
			return new LocalizedString("InvalidTenantGuidException", Strings.ResourceManager, new object[]
			{
				tenantGuid
			});
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00033648 File Offset: 0x00031848
		public static LocalizedString AcmFailure(string failureMessage)
		{
			return new LocalizedString("AcmFailure", Strings.ResourceManager, new object[]
			{
				failureMessage
			});
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00033670 File Offset: 0x00031870
		public static LocalizedString InvalidFileNameException(int fileNameMaximumLength)
		{
			return new LocalizedString("InvalidFileNameException", Strings.ResourceManager, new object[]
			{
				fileNameMaximumLength
			});
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003369D File Offset: 0x0003189D
		public static LocalizedString InvalidADRecipientException
		{
			get
			{
				return new LocalizedString("InvalidADRecipientException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000336B4 File Offset: 0x000318B4
		public static LocalizedString ExceptionExchangeServerNotValid(string name)
		{
			return new LocalizedString("ExceptionExchangeServerNotValid", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000336DC File Offset: 0x000318DC
		public static LocalizedString ExceptionExchangeServerNotFound(string s)
		{
			return new LocalizedString("ExceptionExchangeServerNotFound", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00033704 File Offset: 0x00031904
		public static LocalizedString TlsTlsNegotiationFailure(int i, string t)
		{
			return new LocalizedString("TlsTlsNegotiationFailure", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00033735 File Offset: 0x00031935
		public static LocalizedString GetCallInfoRequest
		{
			get
			{
				return new LocalizedString("GetCallInfoRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003374C File Offset: 0x0003194C
		public static LocalizedString UMRpcTransientError(string user, string server)
		{
			return new LocalizedString("UMRpcTransientError", Strings.ResourceManager, new object[]
			{
				user,
				server
			});
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00033778 File Offset: 0x00031978
		public static LocalizedString OpenRestrictedContentException(string reason)
		{
			return new LocalizedString("OpenRestrictedContentException", Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000337A0 File Offset: 0x000319A0
		public static LocalizedString PromptPreviewRpcRequest
		{
			get
			{
				return new LocalizedString("PromptPreviewRpcRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000337B8 File Offset: 0x000319B8
		public static LocalizedString DialPlanNotFoundForServer(string s)
		{
			return new LocalizedString("DialPlanNotFoundForServer", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000337E0 File Offset: 0x000319E0
		public static LocalizedString PasswordDerivedBytesValuesFixed(string name)
		{
			return new LocalizedString("PasswordDerivedBytesValuesFixed", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00033808 File Offset: 0x00031A08
		public static LocalizedString DeleteFailed
		{
			get
			{
				return new LocalizedString("DeleteFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00033820 File Offset: 0x00031A20
		public static LocalizedString CorruptedConfigurationCouldNotBeDeleted(string userAddress)
		{
			return new LocalizedString("CorruptedConfigurationCouldNotBeDeleted", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00033848 File Offset: 0x00031A48
		public static LocalizedString ExclusionListException(string msg)
		{
			return new LocalizedString("ExclusionListException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00033870 File Offset: 0x00031A70
		public static LocalizedString PermanentlyUnableToAccessUserConfiguration(string userAddress)
		{
			return new LocalizedString("PermanentlyUnableToAccessUserConfiguration", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00033898 File Offset: 0x00031A98
		public static LocalizedString GrammarDirectoryNotFoundError(string s, string p1, string p2)
		{
			return new LocalizedString("GrammarDirectoryNotFoundError", Strings.ResourceManager, new object[]
			{
				s,
				p1,
				p2
			});
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x000338C8 File Offset: 0x00031AC8
		public static LocalizedString InvalidCallIdException
		{
			get
			{
				return new LocalizedString("InvalidCallIdException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000338E0 File Offset: 0x00031AE0
		public static LocalizedString FileNotFound(string path)
		{
			return new LocalizedString("FileNotFound", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00033908 File Offset: 0x00031B08
		public static LocalizedString UMRpcAccessDeniedError(string server)
		{
			return new LocalizedString("UMRpcAccessDeniedError", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00033930 File Offset: 0x00031B30
		public static LocalizedString InvalidResponseException(string channel, string error)
		{
			return new LocalizedString("InvalidResponseException", Strings.ResourceManager, new object[]
			{
				channel,
				error
			});
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003395C File Offset: 0x00031B5C
		public static LocalizedString FsmConfigurationException(string exceptionText)
		{
			return new LocalizedString("FsmConfigurationException", Strings.ResourceManager, new object[]
			{
				exceptionText
			});
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00033984 File Offset: 0x00031B84
		public static LocalizedString NoIPAddress(string hostName)
		{
			return new LocalizedString("NoIPAddress", Strings.ResourceManager, new object[]
			{
				hostName
			});
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000339AC File Offset: 0x00031BAC
		public static LocalizedString ContentIndexingNotEnabled
		{
			get
			{
				return new LocalizedString("ContentIndexingNotEnabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000339C4 File Offset: 0x00031BC4
		public static LocalizedString UMMailboxOperationQuotaExceededError(string message)
		{
			return new LocalizedString("UMMailboxOperationQuotaExceededError", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000339EC File Offset: 0x00031BEC
		public static LocalizedString UMMailboxNotFound(string user)
		{
			return new LocalizedString("UMMailboxNotFound", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00033A14 File Offset: 0x00031C14
		public static LocalizedString ClientAccessException
		{
			get
			{
				return new LocalizedString("ClientAccessException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00033A2C File Offset: 0x00031C2C
		public static LocalizedString UnableToFindUMReportData(string mailboxOwner)
		{
			return new LocalizedString("UnableToFindUMReportData", Strings.ResourceManager, new object[]
			{
				mailboxOwner
			});
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00033A54 File Offset: 0x00031C54
		public static LocalizedString UMRPCIncompatibleVersionException
		{
			get
			{
				return new LocalizedString("UMRPCIncompatibleVersionException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00033A6C File Offset: 0x00031C6C
		public static LocalizedString InvalidAcceptedDomainException(string organizationId)
		{
			return new LocalizedString("InvalidAcceptedDomainException", Strings.ResourceManager, new object[]
			{
				organizationId
			});
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00033A94 File Offset: 0x00031C94
		public static LocalizedString TlsCertificateExpired(int i, string t)
		{
			return new LocalizedString("TlsCertificateExpired", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00033AC8 File Offset: 0x00031CC8
		public static LocalizedString ADOperationRetriesExceeded(string s)
		{
			return new LocalizedString("ADOperationRetriesExceeded", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00033AF0 File Offset: 0x00031CF0
		public static LocalizedString InitializeUMMailboxRequest
		{
			get
			{
				return new LocalizedString("InitializeUMMailboxRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00033B07 File Offset: 0x00031D07
		public static LocalizedString UnSecured
		{
			get
			{
				return new LocalizedString("UnSecured", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00033B1E File Offset: 0x00031D1E
		public static LocalizedString InvalidPAA
		{
			get
			{
				return new LocalizedString("InvalidPAA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00033B35 File Offset: 0x00031D35
		public static LocalizedString PlayOnPhoneGreetingRequest
		{
			get
			{
				return new LocalizedString("PlayOnPhoneGreetingRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00033B4C File Offset: 0x00031D4C
		public static LocalizedString PersonalAutoAttendantNotFound(string identity)
		{
			return new LocalizedString("PersonalAutoAttendantNotFound", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00033B74 File Offset: 0x00031D74
		public static LocalizedString DuplicateReplacementStringError(string s)
		{
			return new LocalizedString("DuplicateReplacementStringError", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00033B9C File Offset: 0x00031D9C
		public static LocalizedString UnsupportedCustomGreetingSizeFormat(string minutes)
		{
			return new LocalizedString("UnsupportedCustomGreetingSizeFormat", Strings.ResourceManager, new object[]
			{
				minutes
			});
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00033BC4 File Offset: 0x00031DC4
		public static LocalizedString SubmitWelcomeMessageRequest
		{
			get
			{
				return new LocalizedString("SubmitWelcomeMessageRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00033BDB File Offset: 0x00031DDB
		public static LocalizedString DisableUMMailboxRequest
		{
			get
			{
				return new LocalizedString("DisableUMMailboxRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00033BF4 File Offset: 0x00031DF4
		public static LocalizedString ADFatalError(string s)
		{
			return new LocalizedString("ADFatalError", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00033C1C File Offset: 0x00031E1C
		public static LocalizedString ExceptionInvalidE164ResourceId
		{
			get
			{
				return new LocalizedString("ExceptionInvalidE164ResourceId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00033C34 File Offset: 0x00031E34
		public static LocalizedString CorruptedPasswordField(string user)
		{
			return new LocalizedString("CorruptedPasswordField", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00033C5C File Offset: 0x00031E5C
		public static LocalizedString UserConfigurationException(string exceptionText)
		{
			return new LocalizedString("UserConfigurationException", Strings.ResourceManager, new object[]
			{
				exceptionText
			});
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00033C84 File Offset: 0x00031E84
		public static LocalizedString OverPlayOnPhoneCallLimitException
		{
			get
			{
				return new LocalizedString("OverPlayOnPhoneCallLimitException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00033C9C File Offset: 0x00031E9C
		public static LocalizedString MowaGrammarException(string exceptionText)
		{
			return new LocalizedString("MowaGrammarException", Strings.ResourceManager, new object[]
			{
				exceptionText
			});
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00033CC4 File Offset: 0x00031EC4
		public static LocalizedString ADOperationFailure(string s)
		{
			return new LocalizedString("ADOperationFailure", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00033CEC File Offset: 0x00031EEC
		public static LocalizedString UMMailboxOperationTransientError(string message)
		{
			return new LocalizedString("UMMailboxOperationTransientError", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00033D14 File Offset: 0x00031F14
		public static LocalizedString AudioDataIsOversizeException(int maxAudioDataMegabytes, long maxGreetingSizeMinutes)
		{
			return new LocalizedString("AudioDataIsOversizeException", Strings.ResourceManager, new object[]
			{
				maxAudioDataMegabytes,
				maxGreetingSizeMinutes
			});
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00033D4C File Offset: 0x00031F4C
		public static LocalizedString PromptSynthesisException(string info)
		{
			return new LocalizedString("PromptSynthesisException", Strings.ResourceManager, new object[]
			{
				info
			});
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00033D74 File Offset: 0x00031F74
		public static LocalizedString ExceptionSipResourceIdNotNeeded
		{
			get
			{
				return new LocalizedString("ExceptionSipResourceIdNotNeeded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00033D8C File Offset: 0x00031F8C
		public static LocalizedString CDROperationTransientError(string message)
		{
			return new LocalizedString("CDROperationTransientError", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00033DB4 File Offset: 0x00031FB4
		public static LocalizedString TransportException(string msg)
		{
			return new LocalizedString("TransportException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00033DDC File Offset: 0x00031FDC
		public static LocalizedString ProcessPartnerMessageRequest
		{
			get
			{
				return new LocalizedString("ProcessPartnerMessageRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00033DF3 File Offset: 0x00031FF3
		public static LocalizedString AutoAttendantPromptRequest
		{
			get
			{
				return new LocalizedString("AutoAttendantPromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00033E0A File Offset: 0x0003200A
		public static LocalizedString GetPINInfoRequest
		{
			get
			{
				return new LocalizedString("GetPINInfoRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00033E24 File Offset: 0x00032024
		public static LocalizedString XMLError(int line, int position, string message)
		{
			return new LocalizedString("XMLError", Strings.ResourceManager, new object[]
			{
				line,
				position,
				message
			});
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00033E5E File Offset: 0x0003205E
		public static LocalizedString DisconnectRequest
		{
			get
			{
				return new LocalizedString("DisconnectRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00033E78 File Offset: 0x00032078
		public static LocalizedString UnsupportedCustomGreetingLegacyFormat(string fileName)
		{
			return new LocalizedString("UnsupportedCustomGreetingLegacyFormat", Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00033EA0 File Offset: 0x000320A0
		public static LocalizedString TransientlyUnableToAccessUserConfiguration(string userAddress)
		{
			return new LocalizedString("TransientlyUnableToAccessUserConfiguration", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00033EC8 File Offset: 0x000320C8
		public static LocalizedString DialingRulesException
		{
			get
			{
				return new LocalizedString("DialingRulesException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00033EE0 File Offset: 0x000320E0
		public static LocalizedString ErrorMaxPAACountReached(int count)
		{
			return new LocalizedString("ErrorMaxPAACountReached", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00033F0D File Offset: 0x0003210D
		public static LocalizedString AutoAttendantBusinessHoursPromptRequest
		{
			get
			{
				return new LocalizedString("AutoAttendantBusinessHoursPromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00033F24 File Offset: 0x00032124
		public static LocalizedString ExceptionE164ResourceIdNeeded
		{
			get
			{
				return new LocalizedString("ExceptionE164ResourceIdNeeded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00033F3C File Offset: 0x0003213C
		public static LocalizedString TlsRemoteCertificateRevoked(int i, string t)
		{
			return new LocalizedString("TlsRemoteCertificateRevoked", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00033F70 File Offset: 0x00032170
		public static LocalizedString EWSUrlDiscoveryFailed(string user)
		{
			return new LocalizedString("EWSUrlDiscoveryFailed", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00033F98 File Offset: 0x00032198
		public static LocalizedString AutoAttendantBusinessLocationPromptRequest
		{
			get
			{
				return new LocalizedString("AutoAttendantBusinessLocationPromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00033FB0 File Offset: 0x000321B0
		public static LocalizedString ErrorPerformingCDROperation(string moreInfo)
		{
			return new LocalizedString("ErrorPerformingCDROperation", Strings.ResourceManager, new object[]
			{
				moreInfo
			});
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00033FD8 File Offset: 0x000321D8
		public static LocalizedString UMRecipientValidation(string recipient, string fieldName)
		{
			return new LocalizedString("UMRecipientValidation", Strings.ResourceManager, new object[]
			{
				recipient,
				fieldName
			});
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00034004 File Offset: 0x00032204
		public static LocalizedString PasswordDerivedBytesNeedNonNegNum
		{
			get
			{
				return new LocalizedString("PasswordDerivedBytesNeedNonNegNum", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003401C File Offset: 0x0003221C
		public static LocalizedString ExceptionUserNotUmEnabled(string user)
		{
			return new LocalizedString("ExceptionUserNotUmEnabled", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00034044 File Offset: 0x00032244
		public static LocalizedString TlsUntrustedRemoteCertificate(int i, string t)
		{
			return new LocalizedString("TlsUntrustedRemoteCertificate", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00034075 File Offset: 0x00032275
		public static LocalizedString PlayOnPhoneAAGreetingRequest
		{
			get
			{
				return new LocalizedString("PlayOnPhoneAAGreetingRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0003408C File Offset: 0x0003228C
		public static LocalizedString UnsupportedCustomGreetingWaveFormat
		{
			get
			{
				return new LocalizedString("UnsupportedCustomGreetingWaveFormat", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000340A4 File Offset: 0x000322A4
		public static LocalizedString DialPlanNotFound(string s)
		{
			return new LocalizedString("DialPlanNotFound", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000340CC File Offset: 0x000322CC
		public static LocalizedString ExceptionInvalidSipUri
		{
			get
			{
				return new LocalizedString("ExceptionInvalidSipUri", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000340E4 File Offset: 0x000322E4
		public static LocalizedString UmAuthorizationException(string user, string activity)
		{
			return new LocalizedString("UmAuthorizationException", Strings.ResourceManager, new object[]
			{
				user,
				activity
			});
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00034110 File Offset: 0x00032310
		public static LocalizedString ADRetry(int i)
		{
			return new LocalizedString("ADRetry", Strings.ResourceManager, new object[]
			{
				i
			});
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00034140 File Offset: 0x00032340
		public static LocalizedString PublishingException(LocalizedString msg)
		{
			return new LocalizedString("PublishingException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003416D File Offset: 0x0003236D
		public static LocalizedString ADAccessFailed
		{
			get
			{
				return new LocalizedString("ADAccessFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00034184 File Offset: 0x00032384
		public static LocalizedString SerializationError(string mailboxOwner)
		{
			return new LocalizedString("SerializationError", Strings.ResourceManager, new object[]
			{
				mailboxOwner
			});
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x000341AC File Offset: 0x000323AC
		public static LocalizedString AutoAttendantCustomPromptRequest
		{
			get
			{
				return new LocalizedString("AutoAttendantCustomPromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000341C4 File Offset: 0x000323C4
		public static LocalizedString UMRPCIncompatibleVersionError(string server)
		{
			return new LocalizedString("UMRPCIncompatibleVersionError", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000341EC File Offset: 0x000323EC
		public static LocalizedString TlsRemoteCertificateInvalidUsage(int i, string t)
		{
			return new LocalizedString("TlsRemoteCertificateInvalidUsage", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003421D File Offset: 0x0003241D
		public static LocalizedString AutoAttendantWelcomePromptRequest
		{
			get
			{
				return new LocalizedString("AutoAttendantWelcomePromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00034234 File Offset: 0x00032434
		public static LocalizedString TlsIncorrectNameInRemoteCertificate(int i, string t)
		{
			return new LocalizedString("TlsIncorrectNameInRemoteCertificate", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00034268 File Offset: 0x00032468
		public static LocalizedString CDROperationQuotaExceededError(string message)
		{
			return new LocalizedString("CDROperationQuotaExceededError", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00034290 File Offset: 0x00032490
		public static LocalizedString DeleteContentException(string moreInfo)
		{
			return new LocalizedString("DeleteContentException", Strings.ResourceManager, new object[]
			{
				moreInfo
			});
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000342B8 File Offset: 0x000324B8
		public static LocalizedString TamperedPin
		{
			get
			{
				return new LocalizedString("TamperedPin", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000342CF File Offset: 0x000324CF
		public static LocalizedString SubmitPINResetMessageRequest
		{
			get
			{
				return new LocalizedString("SubmitPINResetMessageRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000342E8 File Offset: 0x000324E8
		public static LocalizedString PromptDirectoryNotFoundError(string s, string p1, string p2)
		{
			return new LocalizedString("PromptDirectoryNotFoundError", Strings.ResourceManager, new object[]
			{
				s,
				p1,
				p2
			});
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00034318 File Offset: 0x00032518
		public static LocalizedString CorruptedGreetingCouldNotBeDeleted(string userAddress)
		{
			return new LocalizedString("CorruptedGreetingCouldNotBeDeleted", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00034340 File Offset: 0x00032540
		public static LocalizedString UMRpcError(string targetName, int responseCode, string responseText)
		{
			return new LocalizedString("UMRpcError", Strings.ResourceManager, new object[]
			{
				targetName,
				responseCode,
				responseText
			});
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00034375 File Offset: 0x00032575
		public static LocalizedString PlayOnPhoneMessageRequest
		{
			get
			{
				return new LocalizedString("PlayOnPhoneMessageRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003438C File Offset: 0x0003258C
		public static LocalizedString InvalidPrincipalException
		{
			get
			{
				return new LocalizedString("InvalidPrincipalException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000343A4 File Offset: 0x000325A4
		public static LocalizedString descMwiMessageExpiredError(string userName)
		{
			return new LocalizedString("descMwiMessageExpiredError", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000343CC File Offset: 0x000325CC
		public static LocalizedString MissingUserAttribute(string attribute, string extension)
		{
			return new LocalizedString("MissingUserAttribute", Strings.ResourceManager, new object[]
			{
				attribute,
				extension
			});
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x000343F8 File Offset: 0x000325F8
		public static LocalizedString RpcUMServerNotFoundException
		{
			get
			{
				return new LocalizedString("RpcUMServerNotFoundException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0003440F File Offset: 0x0003260F
		public static LocalizedString SipResourceIdAndExtensionsNeeded
		{
			get
			{
				return new LocalizedString("SipResourceIdAndExtensionsNeeded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00034426 File Offset: 0x00032626
		public static LocalizedString PlayOnPhonePAAGreetingRequest
		{
			get
			{
				return new LocalizedString("PlayOnPhonePAAGreetingRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00034440 File Offset: 0x00032640
		public static LocalizedString EWSOperationFailed(string response, string details)
		{
			return new LocalizedString("EWSOperationFailed", Strings.ResourceManager, new object[]
			{
				response,
				details
			});
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003446C File Offset: 0x0003266C
		public static LocalizedString NoCallerIdToUseException
		{
			get
			{
				return new LocalizedString("NoCallerIdToUseException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00034484 File Offset: 0x00032684
		public static LocalizedString DestinationAlreadyExists(string path)
		{
			return new LocalizedString("DestinationAlreadyExists", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000344AC File Offset: 0x000326AC
		public static LocalizedString InvalidUMProxyAddressException(string proxyAddress)
		{
			return new LocalizedString("InvalidUMProxyAddressException", Strings.ResourceManager, new object[]
			{
				proxyAddress
			});
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000344D4 File Offset: 0x000326D4
		public static LocalizedString UMServerNotFoundDialPlanException(string dialPlan)
		{
			return new LocalizedString("UMServerNotFoundDialPlanException", Strings.ResourceManager, new object[]
			{
				dialPlan
			});
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x000344FC File Offset: 0x000326FC
		public static LocalizedString UndeleteFailed
		{
			get
			{
				return new LocalizedString("UndeleteFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00034514 File Offset: 0x00032714
		public static LocalizedString TlsOther(int i, string t)
		{
			return new LocalizedString("TlsOther", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00034545 File Offset: 0x00032745
		public static LocalizedString UMDataStorageMailboxNotFound
		{
			get
			{
				return new LocalizedString("UMDataStorageMailboxNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003455C File Offset: 0x0003275C
		public static LocalizedString InvalidObjectIdException
		{
			get
			{
				return new LocalizedString("InvalidObjectIdException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00034574 File Offset: 0x00032774
		public static LocalizedString EWSUMMailboxAccessException(string reason)
		{
			return new LocalizedString("EWSUMMailboxAccessException", Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003459C File Offset: 0x0003279C
		public static LocalizedString UMMailboxPromptRequest
		{
			get
			{
				return new LocalizedString("UMMailboxPromptRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000345B3 File Offset: 0x000327B3
		public static LocalizedString EWSNoResponseReceived
		{
			get
			{
				return new LocalizedString("EWSNoResponseReceived", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000345CC File Offset: 0x000327CC
		public static LocalizedString WmaToWavConversion(string wma, string wav)
		{
			return new LocalizedString("WmaToWavConversion", Strings.ResourceManager, new object[]
			{
				wma,
				wav
			});
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000345F8 File Offset: 0x000327F8
		public static LocalizedString TlsRemoteDisconnected(int i, string t)
		{
			return new LocalizedString("TlsRemoteDisconnected", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003462C File Offset: 0x0003282C
		public static LocalizedString UmUserException(string exceptionText)
		{
			return new LocalizedString("UmUserException", Strings.ResourceManager, new object[]
			{
				exceptionText
			});
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00034654 File Offset: 0x00032854
		public static LocalizedString CorruptedPAAStore
		{
			get
			{
				return new LocalizedString("CorruptedPAAStore", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003466C File Offset: 0x0003286C
		public static LocalizedString CDROperationObjectNotFound(string message)
		{
			return new LocalizedString("CDROperationObjectNotFound", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00034694 File Offset: 0x00032894
		public static LocalizedString UMRpcGenericError(string user, int hResult, string server)
		{
			return new LocalizedString("UMRpcGenericError", Strings.ResourceManager, new object[]
			{
				user,
				hResult,
				server
			});
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000346CC File Offset: 0x000328CC
		public static LocalizedString MoreThanOneSearchFolder(int searchFolderCount, string searchFolderName)
		{
			return new LocalizedString("MoreThanOneSearchFolder", Strings.ResourceManager, new object[]
			{
				searchFolderCount,
				searchFolderName
			});
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000346FD File Offset: 0x000328FD
		public static LocalizedString CallIdNull
		{
			get
			{
				return new LocalizedString("CallIdNull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00034714 File Offset: 0x00032914
		public static LocalizedString ExceptionInvalidPhoneNumber
		{
			get
			{
				return new LocalizedString("ExceptionInvalidPhoneNumber", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003472C File Offset: 0x0003292C
		public static LocalizedString InvalidArgumentException(string s)
		{
			return new LocalizedString("InvalidArgumentException", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00034754 File Offset: 0x00032954
		public static LocalizedString UMMailboxOperationSendEmailError(string message)
		{
			return new LocalizedString("UMMailboxOperationSendEmailError", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003477C File Offset: 0x0003297C
		public static LocalizedString ErrorPerformingUMMailboxOperation(string moreInfo)
		{
			return new LocalizedString("ErrorPerformingUMMailboxOperation", Strings.ResourceManager, new object[]
			{
				moreInfo
			});
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000347A4 File Offset: 0x000329A4
		public static LocalizedString descMwiNoTargetsAvailableError(string userName)
		{
			return new LocalizedString("descMwiNoTargetsAvailableError", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000347CC File Offset: 0x000329CC
		public static LocalizedString SourceFileNotFound(string path)
		{
			return new LocalizedString("SourceFileNotFound", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000347F4 File Offset: 0x000329F4
		public static LocalizedString CallIdNotNull(string currentlySetCallId, string newCallId)
		{
			return new LocalizedString("CallIdNotNull", Strings.ResourceManager, new object[]
			{
				currentlySetCallId,
				newCallId
			});
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00034820 File Offset: 0x00032A20
		public static LocalizedString Secured
		{
			get
			{
				return new LocalizedString("Secured", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00034838 File Offset: 0x00032A38
		public static LocalizedString InsufficientSendQuotaForUMEnablement(string user)
		{
			return new LocalizedString("InsufficientSendQuotaForUMEnablement", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00034860 File Offset: 0x00032A60
		public static LocalizedString IPGatewayNotFoundException
		{
			get
			{
				return new LocalizedString("IPGatewayNotFoundException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00034877 File Offset: 0x00032A77
		public static LocalizedString UnsupportedCustomGreetingWmaFormat
		{
			get
			{
				return new LocalizedString("UnsupportedCustomGreetingWmaFormat", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00034890 File Offset: 0x00032A90
		public static LocalizedString UMInvalidPartnerMessageException(string fieldName)
		{
			return new LocalizedString("UMInvalidPartnerMessageException", Strings.ResourceManager, new object[]
			{
				fieldName
			});
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000348B8 File Offset: 0x00032AB8
		public static LocalizedString InvalidRequestException(string server)
		{
			return new LocalizedString("InvalidRequestException", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000348E0 File Offset: 0x00032AE0
		public static LocalizedString TlsLocalCertificateNotFound(int i, string t)
		{
			return new LocalizedString("TlsLocalCertificateNotFound", Strings.ResourceManager, new object[]
			{
				i,
				t
			});
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00034914 File Offset: 0x00032B14
		public static LocalizedString CorruptedPIN(string userAddress)
		{
			return new LocalizedString("CorruptedPIN", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003493C File Offset: 0x00032B3C
		public static LocalizedString descTooManyOutstandingMwiRequestsError(string userName)
		{
			return new LocalizedString("descTooManyOutstandingMwiRequestsError", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00034964 File Offset: 0x00032B64
		public static LocalizedString MultupleUMDataStorageMailboxFound(string id1, string id2)
		{
			return new LocalizedString("MultupleUMDataStorageMailboxFound", Strings.ResourceManager, new object[]
			{
				id1,
				id2
			});
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00034990 File Offset: 0x00032B90
		public static LocalizedString ExceptionCouldNotGenerateExtension
		{
			get
			{
				return new LocalizedString("ExceptionCouldNotGenerateExtension", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000349A7 File Offset: 0x00032BA7
		public static LocalizedString UndeleteNotFound
		{
			get
			{
				return new LocalizedString("UndeleteNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000349C0 File Offset: 0x00032BC0
		public static LocalizedString UnableToRemoveCustomGreeting(string userAddress)
		{
			return new LocalizedString("UnableToRemoveCustomGreeting", Strings.ResourceManager, new object[]
			{
				userAddress
			});
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000349E8 File Offset: 0x00032BE8
		public static LocalizedString InvalidUMAutoAttendantException
		{
			get
			{
				return new LocalizedString("InvalidUMAutoAttendantException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000349FF File Offset: 0x00032BFF
		public static LocalizedString UMGray
		{
			get
			{
				return new LocalizedString("UMGray", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00034A16 File Offset: 0x00032C16
		public static LocalizedString SavePINRequest
		{
			get
			{
				return new LocalizedString("SavePINRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00034A30 File Offset: 0x00032C30
		public static LocalizedString NoIPv4Address(string hostName)
		{
			return new LocalizedString("NoIPv4Address", Strings.ResourceManager, new object[]
			{
				hostName
			});
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00034A58 File Offset: 0x00032C58
		public static LocalizedString DuplicateClassNameError(string s)
		{
			return new LocalizedString("DuplicateClassNameError", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00034A80 File Offset: 0x00032C80
		public static LocalizedString ResetPINException
		{
			get
			{
				return new LocalizedString("ResetPINException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00034A98 File Offset: 0x00032C98
		public static LocalizedString LegacyUmUser(string legacyDN)
		{
			return new LocalizedString("LegacyUmUser", Strings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00034AC0 File Offset: 0x00032CC0
		public static LocalizedString PasswordDerivedBytesTooManyBytes
		{
			get
			{
				return new LocalizedString("PasswordDerivedBytesTooManyBytes", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00034AD8 File Offset: 0x00032CD8
		public static LocalizedString WavToWmaConversion(string wav, string wma)
		{
			return new LocalizedString("WavToWmaConversion", Strings.ResourceManager, new object[]
			{
				wav,
				wma
			});
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00034B04 File Offset: 0x00032D04
		public static LocalizedString ExceptionNoMailboxForUser(string user)
		{
			return new LocalizedString("ExceptionNoMailboxForUser", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00034B2C File Offset: 0x00032D2C
		public static LocalizedString ErrorAccessingPublishingPoint(string moreInfo)
		{
			return new LocalizedString("ErrorAccessingPublishingPoint", Strings.ResourceManager, new object[]
			{
				moreInfo
			});
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00034B54 File Offset: 0x00032D54
		public static LocalizedString OutboundCallFailure(string s)
		{
			return new LocalizedString("OutboundCallFailure", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00034B7C File Offset: 0x00032D7C
		public static LocalizedString ADUMUserInvalidUMMailboxPolicyException(string useraddress)
		{
			return new LocalizedString("ADUMUserInvalidUMMailboxPolicyException", Strings.ResourceManager, new object[]
			{
				useraddress
			});
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00034BA4 File Offset: 0x00032DA4
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040006DB RID: 1755
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(60);

		// Token: 0x040006DC RID: 1756
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMCommon.Exceptions.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000194 RID: 404
		public enum IDs : uint
		{
			// Token: 0x040006DE RID: 1758
			ValidateOrGeneratePINRequest = 1713941734U,
			// Token: 0x040006DF RID: 1759
			ExceptionInvalidSipNameResourceId = 2723036908U,
			// Token: 0x040006E0 RID: 1760
			InvalidADRecipientException = 50529680U,
			// Token: 0x040006E1 RID: 1761
			GetCallInfoRequest = 437391379U,
			// Token: 0x040006E2 RID: 1762
			PromptPreviewRpcRequest = 456952916U,
			// Token: 0x040006E3 RID: 1763
			DeleteFailed = 3129803840U,
			// Token: 0x040006E4 RID: 1764
			InvalidCallIdException = 3755207777U,
			// Token: 0x040006E5 RID: 1765
			ContentIndexingNotEnabled = 1007247767U,
			// Token: 0x040006E6 RID: 1766
			ClientAccessException = 2554681784U,
			// Token: 0x040006E7 RID: 1767
			UMRPCIncompatibleVersionException = 3560634689U,
			// Token: 0x040006E8 RID: 1768
			InitializeUMMailboxRequest = 3827073469U,
			// Token: 0x040006E9 RID: 1769
			UnSecured = 1575129900U,
			// Token: 0x040006EA RID: 1770
			InvalidPAA = 1171028813U,
			// Token: 0x040006EB RID: 1771
			PlayOnPhoneGreetingRequest = 1404156949U,
			// Token: 0x040006EC RID: 1772
			SubmitWelcomeMessageRequest = 3153985174U,
			// Token: 0x040006ED RID: 1773
			DisableUMMailboxRequest = 1559723317U,
			// Token: 0x040006EE RID: 1774
			ExceptionInvalidE164ResourceId = 2262233287U,
			// Token: 0x040006EF RID: 1775
			OverPlayOnPhoneCallLimitException = 2807255575U,
			// Token: 0x040006F0 RID: 1776
			ExceptionSipResourceIdNotNeeded = 2987232564U,
			// Token: 0x040006F1 RID: 1777
			ProcessPartnerMessageRequest = 954056551U,
			// Token: 0x040006F2 RID: 1778
			AutoAttendantPromptRequest = 913692915U,
			// Token: 0x040006F3 RID: 1779
			GetPINInfoRequest = 2355518126U,
			// Token: 0x040006F4 RID: 1780
			DisconnectRequest = 1929284865U,
			// Token: 0x040006F5 RID: 1781
			DialingRulesException = 4142400168U,
			// Token: 0x040006F6 RID: 1782
			AutoAttendantBusinessHoursPromptRequest = 1333967066U,
			// Token: 0x040006F7 RID: 1783
			ExceptionE164ResourceIdNeeded = 3615365293U,
			// Token: 0x040006F8 RID: 1784
			AutoAttendantBusinessLocationPromptRequest = 634056442U,
			// Token: 0x040006F9 RID: 1785
			PasswordDerivedBytesNeedNonNegNum = 3099272000U,
			// Token: 0x040006FA RID: 1786
			PlayOnPhoneAAGreetingRequest = 1057562681U,
			// Token: 0x040006FB RID: 1787
			UnsupportedCustomGreetingWaveFormat = 1298000829U,
			// Token: 0x040006FC RID: 1788
			ExceptionInvalidSipUri = 1479848360U,
			// Token: 0x040006FD RID: 1789
			ADAccessFailed = 2382391216U,
			// Token: 0x040006FE RID: 1790
			AutoAttendantCustomPromptRequest = 2474709144U,
			// Token: 0x040006FF RID: 1791
			AutoAttendantWelcomePromptRequest = 2965802745U,
			// Token: 0x04000700 RID: 1792
			TamperedPin = 49929337U,
			// Token: 0x04000701 RID: 1793
			SubmitPINResetMessageRequest = 3060719176U,
			// Token: 0x04000702 RID: 1794
			PlayOnPhoneMessageRequest = 3132314599U,
			// Token: 0x04000703 RID: 1795
			InvalidPrincipalException = 2380102598U,
			// Token: 0x04000704 RID: 1796
			RpcUMServerNotFoundException = 4275120716U,
			// Token: 0x04000705 RID: 1797
			SipResourceIdAndExtensionsNeeded = 1158496135U,
			// Token: 0x04000706 RID: 1798
			PlayOnPhonePAAGreetingRequest = 1496370889U,
			// Token: 0x04000707 RID: 1799
			NoCallerIdToUseException = 229602390U,
			// Token: 0x04000708 RID: 1800
			UndeleteFailed = 2125881321U,
			// Token: 0x04000709 RID: 1801
			UMDataStorageMailboxNotFound = 1651664428U,
			// Token: 0x0400070A RID: 1802
			InvalidObjectIdException = 1738969940U,
			// Token: 0x0400070B RID: 1803
			UMMailboxPromptRequest = 1136924537U,
			// Token: 0x0400070C RID: 1804
			EWSNoResponseReceived = 3151943388U,
			// Token: 0x0400070D RID: 1805
			CorruptedPAAStore = 4186469259U,
			// Token: 0x0400070E RID: 1806
			CallIdNull = 2584780874U,
			// Token: 0x0400070F RID: 1807
			ExceptionInvalidPhoneNumber = 2625829937U,
			// Token: 0x04000710 RID: 1808
			Secured = 1241597555U,
			// Token: 0x04000711 RID: 1809
			IPGatewayNotFoundException = 267464401U,
			// Token: 0x04000712 RID: 1810
			UnsupportedCustomGreetingWmaFormat = 1196599U,
			// Token: 0x04000713 RID: 1811
			ExceptionCouldNotGenerateExtension = 143404757U,
			// Token: 0x04000714 RID: 1812
			UndeleteNotFound = 396119749U,
			// Token: 0x04000715 RID: 1813
			InvalidUMAutoAttendantException = 3947493446U,
			// Token: 0x04000716 RID: 1814
			UMGray = 2003854661U,
			// Token: 0x04000717 RID: 1815
			SavePINRequest = 4078673759U,
			// Token: 0x04000718 RID: 1816
			ResetPINException = 1064401785U,
			// Token: 0x04000719 RID: 1817
			PasswordDerivedBytesTooManyBytes = 4195133953U
		}

		// Token: 0x02000195 RID: 405
		private enum ParamIDs
		{
			// Token: 0x0400071B RID: 1819
			UMMbxPolicyNotFound,
			// Token: 0x0400071C RID: 1820
			InvalidTenantGuidException,
			// Token: 0x0400071D RID: 1821
			AcmFailure,
			// Token: 0x0400071E RID: 1822
			InvalidFileNameException,
			// Token: 0x0400071F RID: 1823
			ExceptionExchangeServerNotValid,
			// Token: 0x04000720 RID: 1824
			ExceptionExchangeServerNotFound,
			// Token: 0x04000721 RID: 1825
			TlsTlsNegotiationFailure,
			// Token: 0x04000722 RID: 1826
			UMRpcTransientError,
			// Token: 0x04000723 RID: 1827
			OpenRestrictedContentException,
			// Token: 0x04000724 RID: 1828
			DialPlanNotFoundForServer,
			// Token: 0x04000725 RID: 1829
			PasswordDerivedBytesValuesFixed,
			// Token: 0x04000726 RID: 1830
			CorruptedConfigurationCouldNotBeDeleted,
			// Token: 0x04000727 RID: 1831
			ExclusionListException,
			// Token: 0x04000728 RID: 1832
			PermanentlyUnableToAccessUserConfiguration,
			// Token: 0x04000729 RID: 1833
			GrammarDirectoryNotFoundError,
			// Token: 0x0400072A RID: 1834
			FileNotFound,
			// Token: 0x0400072B RID: 1835
			UMRpcAccessDeniedError,
			// Token: 0x0400072C RID: 1836
			InvalidResponseException,
			// Token: 0x0400072D RID: 1837
			FsmConfigurationException,
			// Token: 0x0400072E RID: 1838
			NoIPAddress,
			// Token: 0x0400072F RID: 1839
			UMMailboxOperationQuotaExceededError,
			// Token: 0x04000730 RID: 1840
			UMMailboxNotFound,
			// Token: 0x04000731 RID: 1841
			UnableToFindUMReportData,
			// Token: 0x04000732 RID: 1842
			InvalidAcceptedDomainException,
			// Token: 0x04000733 RID: 1843
			TlsCertificateExpired,
			// Token: 0x04000734 RID: 1844
			ADOperationRetriesExceeded,
			// Token: 0x04000735 RID: 1845
			PersonalAutoAttendantNotFound,
			// Token: 0x04000736 RID: 1846
			DuplicateReplacementStringError,
			// Token: 0x04000737 RID: 1847
			UnsupportedCustomGreetingSizeFormat,
			// Token: 0x04000738 RID: 1848
			ADFatalError,
			// Token: 0x04000739 RID: 1849
			CorruptedPasswordField,
			// Token: 0x0400073A RID: 1850
			UserConfigurationException,
			// Token: 0x0400073B RID: 1851
			MowaGrammarException,
			// Token: 0x0400073C RID: 1852
			ADOperationFailure,
			// Token: 0x0400073D RID: 1853
			UMMailboxOperationTransientError,
			// Token: 0x0400073E RID: 1854
			AudioDataIsOversizeException,
			// Token: 0x0400073F RID: 1855
			PromptSynthesisException,
			// Token: 0x04000740 RID: 1856
			CDROperationTransientError,
			// Token: 0x04000741 RID: 1857
			TransportException,
			// Token: 0x04000742 RID: 1858
			XMLError,
			// Token: 0x04000743 RID: 1859
			UnsupportedCustomGreetingLegacyFormat,
			// Token: 0x04000744 RID: 1860
			TransientlyUnableToAccessUserConfiguration,
			// Token: 0x04000745 RID: 1861
			ErrorMaxPAACountReached,
			// Token: 0x04000746 RID: 1862
			TlsRemoteCertificateRevoked,
			// Token: 0x04000747 RID: 1863
			EWSUrlDiscoveryFailed,
			// Token: 0x04000748 RID: 1864
			ErrorPerformingCDROperation,
			// Token: 0x04000749 RID: 1865
			UMRecipientValidation,
			// Token: 0x0400074A RID: 1866
			ExceptionUserNotUmEnabled,
			// Token: 0x0400074B RID: 1867
			TlsUntrustedRemoteCertificate,
			// Token: 0x0400074C RID: 1868
			DialPlanNotFound,
			// Token: 0x0400074D RID: 1869
			UmAuthorizationException,
			// Token: 0x0400074E RID: 1870
			ADRetry,
			// Token: 0x0400074F RID: 1871
			PublishingException,
			// Token: 0x04000750 RID: 1872
			SerializationError,
			// Token: 0x04000751 RID: 1873
			UMRPCIncompatibleVersionError,
			// Token: 0x04000752 RID: 1874
			TlsRemoteCertificateInvalidUsage,
			// Token: 0x04000753 RID: 1875
			TlsIncorrectNameInRemoteCertificate,
			// Token: 0x04000754 RID: 1876
			CDROperationQuotaExceededError,
			// Token: 0x04000755 RID: 1877
			DeleteContentException,
			// Token: 0x04000756 RID: 1878
			PromptDirectoryNotFoundError,
			// Token: 0x04000757 RID: 1879
			CorruptedGreetingCouldNotBeDeleted,
			// Token: 0x04000758 RID: 1880
			UMRpcError,
			// Token: 0x04000759 RID: 1881
			descMwiMessageExpiredError,
			// Token: 0x0400075A RID: 1882
			MissingUserAttribute,
			// Token: 0x0400075B RID: 1883
			EWSOperationFailed,
			// Token: 0x0400075C RID: 1884
			DestinationAlreadyExists,
			// Token: 0x0400075D RID: 1885
			InvalidUMProxyAddressException,
			// Token: 0x0400075E RID: 1886
			UMServerNotFoundDialPlanException,
			// Token: 0x0400075F RID: 1887
			TlsOther,
			// Token: 0x04000760 RID: 1888
			EWSUMMailboxAccessException,
			// Token: 0x04000761 RID: 1889
			WmaToWavConversion,
			// Token: 0x04000762 RID: 1890
			TlsRemoteDisconnected,
			// Token: 0x04000763 RID: 1891
			UmUserException,
			// Token: 0x04000764 RID: 1892
			CDROperationObjectNotFound,
			// Token: 0x04000765 RID: 1893
			UMRpcGenericError,
			// Token: 0x04000766 RID: 1894
			MoreThanOneSearchFolder,
			// Token: 0x04000767 RID: 1895
			InvalidArgumentException,
			// Token: 0x04000768 RID: 1896
			UMMailboxOperationSendEmailError,
			// Token: 0x04000769 RID: 1897
			ErrorPerformingUMMailboxOperation,
			// Token: 0x0400076A RID: 1898
			descMwiNoTargetsAvailableError,
			// Token: 0x0400076B RID: 1899
			SourceFileNotFound,
			// Token: 0x0400076C RID: 1900
			CallIdNotNull,
			// Token: 0x0400076D RID: 1901
			InsufficientSendQuotaForUMEnablement,
			// Token: 0x0400076E RID: 1902
			UMInvalidPartnerMessageException,
			// Token: 0x0400076F RID: 1903
			InvalidRequestException,
			// Token: 0x04000770 RID: 1904
			TlsLocalCertificateNotFound,
			// Token: 0x04000771 RID: 1905
			CorruptedPIN,
			// Token: 0x04000772 RID: 1906
			descTooManyOutstandingMwiRequestsError,
			// Token: 0x04000773 RID: 1907
			MultupleUMDataStorageMailboxFound,
			// Token: 0x04000774 RID: 1908
			UnableToRemoveCustomGreeting,
			// Token: 0x04000775 RID: 1909
			NoIPv4Address,
			// Token: 0x04000776 RID: 1910
			DuplicateClassNameError,
			// Token: 0x04000777 RID: 1911
			LegacyUmUser,
			// Token: 0x04000778 RID: 1912
			WavToWmaConversion,
			// Token: 0x04000779 RID: 1913
			ExceptionNoMailboxForUser,
			// Token: 0x0400077A RID: 1914
			ErrorAccessingPublishingPoint,
			// Token: 0x0400077B RID: 1915
			OutboundCallFailure,
			// Token: 0x0400077C RID: 1916
			ADUMUserInvalidUMMailboxPolicyException
		}
	}
}
