using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DatacenterStrings
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(3442334529U, "BecErrorThrottling");
			Strings.stringIDs.Add(2921179735U, "ErrorSPFInvalidMemberName");
			Strings.stringIDs.Add(3165600047U, "BecAccessDenied");
			Strings.stringIDs.Add(534972034U, "BecIncorrectPassword");
			Strings.stringIDs.Add(2713690330U, "BecErrorNotFouond");
			Strings.stringIDs.Add(2548455642U, "ErrorManagedParameterSetPassedInForFederatedTenant");
			Strings.stringIDs.Add(457807320U, "IDSErrorMissingNamespaceIdNode");
			Strings.stringIDs.Add(3569539930U, "ErrorParameterIsIncorrect");
			Strings.stringIDs.Add(3398198740U, "ErrorCodeProfileDoesNotExists");
			Strings.stringIDs.Add(71190514U, "ErrorMemberLockedOutBecauseOfPasswordAttempts");
			Strings.stringIDs.Add(507263250U, "IDSErrorEmptyCredFlagsNode");
			Strings.stringIDs.Add(2457590018U, "ErrorPasswordMatchesAccountWithSameMemberName");
			Strings.stringIDs.Add(240550867U, "BecErrorDomainValidation");
			Strings.stringIDs.Add(2539075827U, "ErrorDomainDoesNotExist");
			Strings.stringIDs.Add(1628902957U, "ErrorSignInNameInCompleteOrInvalid");
			Strings.stringIDs.Add(746302685U, "ErrorUserBlocked");
			Strings.stringIDs.Add(2001068460U, "BecTransientError");
			Strings.stringIDs.Add(2511795258U, "ErrorFederatedParameterSetPassedInForManagedTenant");
			Strings.stringIDs.Add(2085485339U, "IDSErrorEmptyPuidNode");
			Strings.stringIDs.Add(1384815350U, "BecRedirection");
			Strings.stringIDs.Add(2259851215U, "InvalidNetId");
			Strings.stringIDs.Add(1908027994U, "ErrorNameContainsBlockedWord");
			Strings.stringIDs.Add(153681091U, "ErrorServiceUnavailableDueToInternalError");
			Strings.stringIDs.Add(1374323893U, "IDSErrorEmptyNetIdNode");
			Strings.stringIDs.Add(1890206562U, "ErrorSPFPasswordTooLong");
			Strings.stringIDs.Add(3875381480U, "BecErrorInvalidHeader");
			Strings.stringIDs.Add(2343187933U, "BecErrorUserExists");
			Strings.stringIDs.Add(1260215560U, "ErrorUserNameReserved");
			Strings.stringIDs.Add(286588848U, "ErrorFieldContainsInvalidCharacters");
			Strings.stringIDs.Add(2252511124U, "ErrorPasswordRequired");
			Strings.stringIDs.Add(3038881494U, "ErrorInputContainsForbiddenWord");
			Strings.stringIDs.Add(1170008863U, "ErrorFederatedAccountAlreadyExists");
			Strings.stringIDs.Add(2883915563U, "ErrorEmailNameContainsInvalidCharacters");
			Strings.stringIDs.Add(116822391U, "ErrorPasswordContainedInSQ");
			Strings.stringIDs.Add(3327865544U, "BecErrorInvalidContext");
			Strings.stringIDs.Add(2629176178U, "ErrorsRemovedMailboxHaveNoNetID");
			Strings.stringIDs.Add(3821098863U, "ErrorPasswordContainsInvalidCharacters");
			Strings.stringIDs.Add(24718424U, "ErrorInvalidPassportId");
			Strings.stringIDs.Add(3848807642U, "BecErrorQuota");
			Strings.stringIDs.Add(159495284U, "BecErrorInvalidLicense");
			Strings.stringIDs.Add(1626889520U, "ErrorSPFPasswordIsBlank");
			Strings.stringIDs.Add(4122120961U, "ErrorCannotRecoverLiveIdMismatchInstanceType");
			Strings.stringIDs.Add(2570238007U, "BecErrorSubscription");
			Strings.stringIDs.Add(2854433926U, "ErrorSecretAnswerContainsPassword");
			Strings.stringIDs.Add(344542475U, "ErrorWLCDInternal");
			Strings.stringIDs.Add(1112425574U, "ErrorManagedMemberExistsSPF");
			Strings.stringIDs.Add(2178303307U, "BecErrorUniquenessValidation");
			Strings.stringIDs.Add(3710614060U, "ErrorPasswordIsInvalid");
			Strings.stringIDs.Add(3791085944U, "BecErrorInvalidWeakPassword");
			Strings.stringIDs.Add(1520187672U, "ErrorIncompleteEmailAddress");
			Strings.stringIDs.Add(1810820280U, "ErrorSecretQuestionContainsPassword");
			Strings.stringIDs.Add(561051570U, "BecUserInRecycleState");
			Strings.stringIDs.Add(854137693U, "ErrorFederatedParameterSetPassedInForManagedNamespace");
			Strings.stringIDs.Add(3194642979U, "InternalError");
			Strings.stringIDs.Add(764417362U, "ErrorMemberNameAndFederatedIdentityNotMatch");
			Strings.stringIDs.Add(1746286164U, "ErrorArchiveOnly");
			Strings.stringIDs.Add(2776399702U, "BecErrorSyntaxValidation");
			Strings.stringIDs.Add(2366837181U, "IDSErrorEmptyNamespaceIdNode");
			Strings.stringIDs.Add(2439548284U, "ErrorMemberIsLocked");
			Strings.stringIDs.Add(1968969488U, "ErrorSecretQuestionContainsMemberName");
			Strings.stringIDs.Add(370795085U, "BecErrorAccountDisabled");
			Strings.stringIDs.Add(1787101328U, "BecErrorInvalidPassword");
			Strings.stringIDs.Add(2743094604U, "ErrorCannotRenameCredentialToSameName");
			Strings.stringIDs.Add(2646378639U, "ErrorEnableRoomMailboxAccountParameterFalseInDatacenter");
			Strings.stringIDs.Add(3978792718U, "ErrorAccountIsNotEASI");
			Strings.stringIDs.Add(1953245018U, "NotAuthorized");
			Strings.stringIDs.Add(888348176U, "WindowsLiveIdProvisioningHandlerException");
			Strings.stringIDs.Add(4169169063U, "OrganizationIsReadOnly");
			Strings.stringIDs.Add(1190886676U, "ErrorEmailNameStartAfterDot");
			Strings.stringIDs.Add(3074197328U, "ErrorNamespaceNotFound");
			Strings.stringIDs.Add(3693389389U, "BecIdentityInternalError");
			Strings.stringIDs.Add(2927230868U, "BecCompanyNotFound");
			Strings.stringIDs.Add(2733812604U, "ErrorNoRecordInDatabase");
			Strings.stringIDs.Add(2692387272U, "BecErrorPropertyNotSettable");
			Strings.stringIDs.Add(2602850872U, "ErrorEmailAddressTooLong");
			Strings.stringIDs.Add(1479442559U, "BecErrorServiceUnavailable");
			Strings.stringIDs.Add(2501723808U, "IDSErrorEmptySignInNamesForNetId");
			Strings.stringIDs.Add(4277226160U, "ErrorUserAlreadyInactive");
			Strings.stringIDs.Add(3142877869U, "BecErrorTooManyMappedTenants");
			Strings.stringIDs.Add(1684749577U, "BecErrorStringLength");
			Strings.stringIDs.Add(509547158U, "ErrorPartnerNotAuthorized");
			Strings.stringIDs.Add(1770558589U, "ErrorManagedParameterSetPassedInForFederatedNamespace");
			Strings.stringIDs.Add(312656184U, "BecDirectoryInternalError");
			Strings.stringIDs.Add(364820960U, "BecUnknownError");
			Strings.stringIDs.Add(829279753U, "ErrorPasswordContainsLastName");
			Strings.stringIDs.Add(3970665479U, "BecInternalError");
			Strings.stringIDs.Add(938622763U, "ErrorDomainIsManaged");
			Strings.stringIDs.Add(3197989195U, "ErrorPasswordContainsMemberName");
			Strings.stringIDs.Add(706972491U, "ErrorPartnerCannotModifyProtectedField");
			Strings.stringIDs.Add(1463063580U, "ErrorMissingNetIDWhenBypassWLID");
			Strings.stringIDs.Add(3151258897U, "ErrorEmailStartsAndEndsWithDot");
			Strings.stringIDs.Add(524928998U, "ErrorCodeInvalidNetId");
			Strings.stringIDs.Add(146893062U, "ErrorMemberExists");
			Strings.stringIDs.Add(1316852426U, "BecErrorRequiredProperty");
			Strings.stringIDs.Add(2872232368U, "ErrorInvalidBrandData");
			Strings.stringIDs.Add(3566184850U, "ErrorCanNotSetPasswordOrResetOnFederatedAccount");
			Strings.stringIDs.Add(840675049U, "ErrorSignInNameTooLong");
			Strings.stringIDs.Add(2454442314U, "ErrorNamespaceDoesNotExist");
			Strings.stringIDs.Add(2655490445U, "IDSErrorMissingCredFlagsNode");
			Strings.stringIDs.Add(3197291445U, "ErrorNonEASIMemberExists");
			Strings.stringIDs.Add(1384046745U, "ErrorPasswordContainsFirstName");
			Strings.stringIDs.Add(1475855582U, "ErrorDomainNameIsNull");
			Strings.stringIDs.Add(1895268805U, "BecErrorQuotaExceeded");
			Strings.stringIDs.Add(3873716657U, "ErrorDatabaseOnMaintenance");
			Strings.stringIDs.Add(3937658635U, "ErrorLiveIdWithFederatedIdentityExists");
			Strings.stringIDs.Add(2857272973U, "ErrorSignInNameTooShort");
			Strings.stringIDs.Add(173165206U, "ErrorSPFPasswordTooShort");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002968 File Offset: 0x00000B68
		public static LocalizedString BecErrorThrottling
		{
			get
			{
				return new LocalizedString("BecErrorThrottling", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002986 File Offset: 0x00000B86
		public static LocalizedString ErrorSPFInvalidMemberName
		{
			get
			{
				return new LocalizedString("ErrorSPFInvalidMemberName", "Ex0D2954", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000029A4 File Offset: 0x00000BA4
		public static LocalizedString ErrorCannotChangeMemberFederationState(string act, string liveId, string oldDomain, string newDomain)
		{
			return new LocalizedString("ErrorCannotChangeMemberFederationState", "ExB66C30", false, true, Strings.ResourceManager, new object[]
			{
				act,
				liveId,
				oldDomain,
				newDomain
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static LocalizedString ErrorManagedMemberExists(string memberName)
		{
			return new LocalizedString("ErrorManagedMemberExists", "ExBD5DC4", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002A0F File Offset: 0x00000C0F
		public static LocalizedString BecAccessDenied
		{
			get
			{
				return new LocalizedString("BecAccessDenied", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002A2D File Offset: 0x00000C2D
		public static LocalizedString BecIncorrectPassword
		{
			get
			{
				return new LocalizedString("BecIncorrectPassword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002A4C File Offset: 0x00000C4C
		public static LocalizedString ErrorEvictLiveIdMemberExists(string memberName)
		{
			return new LocalizedString("ErrorEvictLiveIdMemberExists", "Ex55F706", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002A7B File Offset: 0x00000C7B
		public static LocalizedString BecErrorNotFouond
		{
			get
			{
				return new LocalizedString("BecErrorNotFouond", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002A99 File Offset: 0x00000C99
		public static LocalizedString ErrorManagedParameterSetPassedInForFederatedTenant
		{
			get
			{
				return new LocalizedString("ErrorManagedParameterSetPassedInForFederatedTenant", "Ex62E9A2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static LocalizedString ErrorPasswordIncludesMemberName(string memberName)
		{
			return new LocalizedString("ErrorPasswordIncludesMemberName", "ExEEF0F9", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static LocalizedString ErrorEvictLiveIdMemberNotExists(string memberName)
		{
			return new LocalizedString("ErrorEvictLiveIdMemberNotExists", "Ex47077E", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B18 File Offset: 0x00000D18
		public static LocalizedString IDSErrorUnexpectedResultsForGetProfileByAttributes(string memberName, string length)
		{
			return new LocalizedString("IDSErrorUnexpectedResultsForGetProfileByAttributes", "Ex60C917", false, true, Strings.ResourceManager, new object[]
			{
				memberName,
				length
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002B4B File Offset: 0x00000D4B
		public static LocalizedString IDSErrorMissingNamespaceIdNode
		{
			get
			{
				return new LocalizedString("IDSErrorMissingNamespaceIdNode", "Ex922F62", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002B69 File Offset: 0x00000D69
		public static LocalizedString ErrorParameterIsIncorrect
		{
			get
			{
				return new LocalizedString("ErrorParameterIsIncorrect", "Ex3FD29F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002B87 File Offset: 0x00000D87
		public static LocalizedString ErrorCodeProfileDoesNotExists
		{
			get
			{
				return new LocalizedString("ErrorCodeProfileDoesNotExists", "ExE5AB42", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public static LocalizedString ErrorMemberLockedOutBecauseOfPasswordAttempts
		{
			get
			{
				return new LocalizedString("ErrorMemberLockedOutBecauseOfPasswordAttempts", "ExC7BD17", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public static LocalizedString IDSErrorEmptyCredFlagsNode
		{
			get
			{
				return new LocalizedString("IDSErrorEmptyCredFlagsNode", "Ex8AFBBB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public static LocalizedString ErrorMemberNameUnavailableUsedForEASI(string memberName)
		{
			return new LocalizedString("ErrorMemberNameUnavailableUsedForEASI", "Ex8F202B", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002C13 File Offset: 0x00000E13
		public static LocalizedString ErrorPasswordMatchesAccountWithSameMemberName
		{
			get
			{
				return new LocalizedString("ErrorPasswordMatchesAccountWithSameMemberName", "Ex7B4616", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002C31 File Offset: 0x00000E31
		public static LocalizedString BecErrorDomainValidation
		{
			get
			{
				return new LocalizedString("BecErrorDomainValidation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002C4F File Offset: 0x00000E4F
		public static LocalizedString ErrorDomainDoesNotExist
		{
			get
			{
				return new LocalizedString("ErrorDomainDoesNotExist", "Ex8C4E74", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C70 File Offset: 0x00000E70
		public static LocalizedString ErrorPasswordIncludesInvalidChars(string memberName)
		{
			return new LocalizedString("ErrorPasswordIncludesInvalidChars", "ExAEADDF", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002C9F File Offset: 0x00000E9F
		public static LocalizedString ErrorSignInNameInCompleteOrInvalid
		{
			get
			{
				return new LocalizedString("ErrorSignInNameInCompleteOrInvalid", "Ex36606C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002CBD File Offset: 0x00000EBD
		public static LocalizedString ErrorUserBlocked
		{
			get
			{
				return new LocalizedString("ErrorUserBlocked", "ExD8AEE7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002CDB File Offset: 0x00000EDB
		public static LocalizedString BecTransientError
		{
			get
			{
				return new LocalizedString("BecTransientError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002CFC File Offset: 0x00000EFC
		public static LocalizedString VerboseEvictMember(string memberName)
		{
			return new LocalizedString("VerboseEvictMember", "Ex6058C9", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static LocalizedString ErrorUnknown(string memberName)
		{
			return new LocalizedString("ErrorUnknown", "ExD7EB39", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002D5B File Offset: 0x00000F5B
		public static LocalizedString ErrorFederatedParameterSetPassedInForManagedTenant
		{
			get
			{
				return new LocalizedString("ErrorFederatedParameterSetPassedInForManagedTenant", "Ex9C2EB5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002D79 File Offset: 0x00000F79
		public static LocalizedString IDSErrorEmptyPuidNode
		{
			get
			{
				return new LocalizedString("IDSErrorEmptyPuidNode", "Ex71D3CC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D98 File Offset: 0x00000F98
		public static LocalizedString ErrorCannotUseReservedLiveId(string windowsLiveId)
		{
			return new LocalizedString("ErrorCannotUseReservedLiveId", "Ex8B95DC", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public static LocalizedString BecRedirection
		{
			get
			{
				return new LocalizedString("BecRedirection", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002DE5 File Offset: 0x00000FE5
		public static LocalizedString InvalidNetId
		{
			get
			{
				return new LocalizedString("InvalidNetId", "Ex76CC69", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002E03 File Offset: 0x00001003
		public static LocalizedString ErrorNameContainsBlockedWord
		{
			get
			{
				return new LocalizedString("ErrorNameContainsBlockedWord", "Ex3056EF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002E21 File Offset: 0x00001021
		public static LocalizedString ErrorServiceUnavailableDueToInternalError
		{
			get
			{
				return new LocalizedString("ErrorServiceUnavailableDueToInternalError", "Ex276B63", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002E3F File Offset: 0x0000103F
		public static LocalizedString IDSErrorEmptyNetIdNode
		{
			get
			{
				return new LocalizedString("IDSErrorEmptyNetIdNode", "ExF49CA6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002E5D File Offset: 0x0000105D
		public static LocalizedString ErrorSPFPasswordTooLong
		{
			get
			{
				return new LocalizedString("ErrorSPFPasswordTooLong", "Ex9CFCE7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E7B File Offset: 0x0000107B
		public static LocalizedString BecErrorInvalidHeader
		{
			get
			{
				return new LocalizedString("BecErrorInvalidHeader", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E9C File Offset: 0x0000109C
		public static LocalizedString ErrorWindowsLiveIdRequired(string user)
		{
			return new LocalizedString("ErrorWindowsLiveIdRequired", "Ex63E9B9", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002ECB File Offset: 0x000010CB
		public static LocalizedString BecErrorUserExists
		{
			get
			{
				return new LocalizedString("BecErrorUserExists", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002EE9 File Offset: 0x000010E9
		public static LocalizedString ErrorUserNameReserved
		{
			get
			{
				return new LocalizedString("ErrorUserNameReserved", "Ex1F6050", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002F07 File Offset: 0x00001107
		public static LocalizedString ErrorFieldContainsInvalidCharacters
		{
			get
			{
				return new LocalizedString("ErrorFieldContainsInvalidCharacters", "Ex60F178", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F28 File Offset: 0x00001128
		public static LocalizedString ErrorRedirectionEntryManagerException(string message)
		{
			return new LocalizedString("ErrorRedirectionEntryManagerException", "Ex0DD6C6", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002F57 File Offset: 0x00001157
		public static LocalizedString ErrorPasswordRequired
		{
			get
			{
				return new LocalizedString("ErrorPasswordRequired", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002F75 File Offset: 0x00001175
		public static LocalizedString ErrorInputContainsForbiddenWord
		{
			get
			{
				return new LocalizedString("ErrorInputContainsForbiddenWord", "Ex357ED3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002F93 File Offset: 0x00001193
		public static LocalizedString ErrorFederatedAccountAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorFederatedAccountAlreadyExists", "Ex24CDBC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002FB1 File Offset: 0x000011B1
		public static LocalizedString ErrorEmailNameContainsInvalidCharacters
		{
			get
			{
				return new LocalizedString("ErrorEmailNameContainsInvalidCharacters", "Ex7DBC42", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002FD0 File Offset: 0x000011D0
		public static LocalizedString ErrorCannotImportForNamespaceType(string memberName)
		{
			return new LocalizedString("ErrorCannotImportForNamespaceType", "Ex1FFDD4", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002FFF File Offset: 0x000011FF
		public static LocalizedString ErrorPasswordContainedInSQ
		{
			get
			{
				return new LocalizedString("ErrorPasswordContainedInSQ", "ExEE8BF8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003020 File Offset: 0x00001220
		public static LocalizedString ErrorUnmanagedMemberNotExists(string memberName)
		{
			return new LocalizedString("ErrorUnmanagedMemberNotExists", "ExE08325", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003050 File Offset: 0x00001250
		public static LocalizedString ErrorMemberNameUnavailableUsedForDL(string memberName)
		{
			return new LocalizedString("ErrorMemberNameUnavailableUsedForDL", "Ex28E0C9", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000307F File Offset: 0x0000127F
		public static LocalizedString BecErrorInvalidContext
		{
			get
			{
				return new LocalizedString("BecErrorInvalidContext", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000309D File Offset: 0x0000129D
		public static LocalizedString ErrorsRemovedMailboxHaveNoNetID
		{
			get
			{
				return new LocalizedString("ErrorsRemovedMailboxHaveNoNetID", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030BC File Offset: 0x000012BC
		public static LocalizedString ErrorLiveIdDoesNotExist(string windowsLiveId)
		{
			return new LocalizedString("ErrorLiveIdDoesNotExist", "Ex20CA8B", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000030EB File Offset: 0x000012EB
		public static LocalizedString ErrorPasswordContainsInvalidCharacters
		{
			get
			{
				return new LocalizedString("ErrorPasswordContainsInvalidCharacters", "Ex46338B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000310C File Offset: 0x0000130C
		public static LocalizedString IDSErrorUnexpectedResultsForCreatePassports(string memberName, string length)
		{
			return new LocalizedString("IDSErrorUnexpectedResultsForCreatePassports", "ExF34284", false, true, Strings.ResourceManager, new object[]
			{
				memberName,
				length
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003140 File Offset: 0x00001340
		public static LocalizedString ErrorConfigurationUnitNotFound(string configunit)
		{
			return new LocalizedString("ErrorConfigurationUnitNotFound", "Ex2F6EA5", false, true, Strings.ResourceManager, new object[]
			{
				configunit
			});
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000316F File Offset: 0x0000136F
		public static LocalizedString ErrorInvalidPassportId
		{
			get
			{
				return new LocalizedString("ErrorInvalidPassportId", "Ex679B02", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000318D File Offset: 0x0000138D
		public static LocalizedString BecErrorQuota
		{
			get
			{
				return new LocalizedString("BecErrorQuota", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000031AC File Offset: 0x000013AC
		public static LocalizedString ErrorPasswordTooShort(string memberName)
		{
			return new LocalizedString("ErrorPasswordTooShort", "ExDC236B", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000031DB File Offset: 0x000013DB
		public static LocalizedString BecErrorInvalidLicense
		{
			get
			{
				return new LocalizedString("BecErrorInvalidLicense", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000031F9 File Offset: 0x000013F9
		public static LocalizedString ErrorSPFPasswordIsBlank
		{
			get
			{
				return new LocalizedString("ErrorSPFPasswordIsBlank", "Ex8A7A81", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003218 File Offset: 0x00001418
		public static LocalizedString ErrorCannotRemoveWindowsLiveIDFromProxyAddresses(string windowsLiveId)
		{
			return new LocalizedString("ErrorCannotRemoveWindowsLiveIDFromProxyAddresses", "ExBE4789", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003247 File Offset: 0x00001447
		public static LocalizedString ErrorCannotRecoverLiveIdMismatchInstanceType
		{
			get
			{
				return new LocalizedString("ErrorCannotRecoverLiveIdMismatchInstanceType", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003268 File Offset: 0x00001468
		public static LocalizedString ErrorGuidNotParsable(string propertyName)
		{
			return new LocalizedString("ErrorGuidNotParsable", "", false, false, Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003297 File Offset: 0x00001497
		public static LocalizedString BecErrorSubscription
		{
			get
			{
				return new LocalizedString("BecErrorSubscription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000032B5 File Offset: 0x000014B5
		public static LocalizedString ErrorSecretAnswerContainsPassword
		{
			get
			{
				return new LocalizedString("ErrorSecretAnswerContainsPassword", "Ex73E2A4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032D4 File Offset: 0x000014D4
		public static LocalizedString ErrorImportLiveIdManagedMemberExists(string memberName)
		{
			return new LocalizedString("ErrorImportLiveIdManagedMemberExists", "ExED7DB1", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003303 File Offset: 0x00001503
		public static LocalizedString ErrorWLCDInternal
		{
			get
			{
				return new LocalizedString("ErrorWLCDInternal", "Ex01378C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003324 File Offset: 0x00001524
		public static LocalizedString ErrorManagedMemberDoesNotExistForByolid(string memberName)
		{
			return new LocalizedString("ErrorManagedMemberDoesNotExistForByolid", "Ex22C8A9", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003354 File Offset: 0x00001554
		public static LocalizedString ErrorCannotRenameAccrossNamespaceTypes(string domain1, string domain2)
		{
			return new LocalizedString("ErrorCannotRenameAccrossNamespaceTypes", "Ex71D6DB", false, true, Strings.ResourceManager, new object[]
			{
				domain1,
				domain2
			});
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003387 File Offset: 0x00001587
		public static LocalizedString ErrorManagedMemberExistsSPF
		{
			get
			{
				return new LocalizedString("ErrorManagedMemberExistsSPF", "ExC0D31D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000033A5 File Offset: 0x000015A5
		public static LocalizedString BecErrorUniquenessValidation
		{
			get
			{
				return new LocalizedString("BecErrorUniquenessValidation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000033C3 File Offset: 0x000015C3
		public static LocalizedString ErrorPasswordIsInvalid
		{
			get
			{
				return new LocalizedString("ErrorPasswordIsInvalid", "Ex33980C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000033E4 File Offset: 0x000015E4
		public static LocalizedString OrganizationIsImmutable(string cu)
		{
			return new LocalizedString("OrganizationIsImmutable", "", false, false, Strings.ResourceManager, new object[]
			{
				cu
			});
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003413 File Offset: 0x00001613
		public static LocalizedString BecErrorInvalidWeakPassword
		{
			get
			{
				return new LocalizedString("BecErrorInvalidWeakPassword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003434 File Offset: 0x00001634
		public static LocalizedString ErrorCannotDetermineLiveInstance(string identity)
		{
			return new LocalizedString("ErrorCannotDetermineLiveInstance", "Ex756F8E", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003463 File Offset: 0x00001663
		public static LocalizedString ErrorIncompleteEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorIncompleteEmailAddress", "Ex9AA701", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003481 File Offset: 0x00001681
		public static LocalizedString ErrorSecretQuestionContainsPassword
		{
			get
			{
				return new LocalizedString("ErrorSecretQuestionContainsPassword", "ExCBA529", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000034A0 File Offset: 0x000016A0
		public static LocalizedString ErrorFailedToEvictMember(string memberName, string message)
		{
			return new LocalizedString("ErrorFailedToEvictMember", "Ex4E574D", false, true, Strings.ResourceManager, new object[]
			{
				memberName,
				message
			});
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000034D3 File Offset: 0x000016D3
		public static LocalizedString BecUserInRecycleState
		{
			get
			{
				return new LocalizedString("BecUserInRecycleState", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000034F4 File Offset: 0x000016F4
		public static LocalizedString ErrorParameterRequired(string parameterName)
		{
			return new LocalizedString("ErrorParameterRequired", "Ex7A03BB", false, true, Strings.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003524 File Offset: 0x00001724
		public static LocalizedString ErrorOnGetProfile(string netId, string errorCode)
		{
			return new LocalizedString("ErrorOnGetProfile", "ExB02F66", false, true, Strings.ResourceManager, new object[]
			{
				netId,
				errorCode
			});
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003557 File Offset: 0x00001757
		public static LocalizedString ErrorFederatedParameterSetPassedInForManagedNamespace
		{
			get
			{
				return new LocalizedString("ErrorFederatedParameterSetPassedInForManagedNamespace", "Ex821DDF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003575 File Offset: 0x00001775
		public static LocalizedString InternalError
		{
			get
			{
				return new LocalizedString("InternalError", "Ex3E461F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003593 File Offset: 0x00001793
		public static LocalizedString ErrorMemberNameAndFederatedIdentityNotMatch
		{
			get
			{
				return new LocalizedString("ErrorMemberNameAndFederatedIdentityNotMatch", "ExE6878D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000035B1 File Offset: 0x000017B1
		public static LocalizedString ErrorArchiveOnly
		{
			get
			{
				return new LocalizedString("ErrorArchiveOnly", "Ex9EC78C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000035CF File Offset: 0x000017CF
		public static LocalizedString BecErrorSyntaxValidation
		{
			get
			{
				return new LocalizedString("BecErrorSyntaxValidation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000035ED File Offset: 0x000017ED
		public static LocalizedString IDSErrorEmptyNamespaceIdNode
		{
			get
			{
				return new LocalizedString("IDSErrorEmptyNamespaceIdNode", "Ex0728C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000360B File Offset: 0x0000180B
		public static LocalizedString ErrorMemberIsLocked
		{
			get
			{
				return new LocalizedString("ErrorMemberIsLocked", "Ex776892", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003629 File Offset: 0x00001829
		public static LocalizedString ErrorSecretQuestionContainsMemberName
		{
			get
			{
				return new LocalizedString("ErrorSecretQuestionContainsMemberName", "Ex24C70E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003647 File Offset: 0x00001847
		public static LocalizedString BecErrorAccountDisabled
		{
			get
			{
				return new LocalizedString("BecErrorAccountDisabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003668 File Offset: 0x00001868
		public static LocalizedString ErrorMemberNameBlocked(string memberName)
		{
			return new LocalizedString("ErrorMemberNameBlocked", "Ex6221CE", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003697 File Offset: 0x00001897
		public static LocalizedString BecErrorInvalidPassword
		{
			get
			{
				return new LocalizedString("BecErrorInvalidPassword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000036B8 File Offset: 0x000018B8
		public static LocalizedString SPFInternalError(string additionalMessage)
		{
			return new LocalizedString("SPFInternalError", "Ex026526", false, true, Strings.ResourceManager, new object[]
			{
				additionalMessage
			});
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000036E7 File Offset: 0x000018E7
		public static LocalizedString ErrorCannotRenameCredentialToSameName
		{
			get
			{
				return new LocalizedString("ErrorCannotRenameCredentialToSameName", "ExFEC65C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003705 File Offset: 0x00001905
		public static LocalizedString ErrorEnableRoomMailboxAccountParameterFalseInDatacenter
		{
			get
			{
				return new LocalizedString("ErrorEnableRoomMailboxAccountParameterFalseInDatacenter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003723 File Offset: 0x00001923
		public static LocalizedString ErrorAccountIsNotEASI
		{
			get
			{
				return new LocalizedString("ErrorAccountIsNotEASI", "Ex5CD86F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003741 File Offset: 0x00001941
		public static LocalizedString NotAuthorized
		{
			get
			{
				return new LocalizedString("NotAuthorized", "Ex4036C4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000375F File Offset: 0x0000195F
		public static LocalizedString WindowsLiveIdProvisioningHandlerException
		{
			get
			{
				return new LocalizedString("WindowsLiveIdProvisioningHandlerException", "ExEDB8A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000377D File Offset: 0x0000197D
		public static LocalizedString OrganizationIsReadOnly
		{
			get
			{
				return new LocalizedString("OrganizationIsReadOnly", "ExF12C78", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000379B File Offset: 0x0000199B
		public static LocalizedString ErrorEmailNameStartAfterDot
		{
			get
			{
				return new LocalizedString("ErrorEmailNameStartAfterDot", "Ex79E561", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000037BC File Offset: 0x000019BC
		public static LocalizedString ErrorMemberNameUnavailable(string memberName)
		{
			return new LocalizedString("ErrorMemberNameUnavailable", "Ex4070F8", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000037EB File Offset: 0x000019EB
		public static LocalizedString ErrorNamespaceNotFound
		{
			get
			{
				return new LocalizedString("ErrorNamespaceNotFound", "ExF61989", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003809 File Offset: 0x00001A09
		public static LocalizedString BecIdentityInternalError
		{
			get
			{
				return new LocalizedString("BecIdentityInternalError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003828 File Offset: 0x00001A28
		public static LocalizedString ErrorUnmanagedMemberExists(string memberName)
		{
			return new LocalizedString("ErrorUnmanagedMemberExists", "Ex312482", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003857 File Offset: 0x00001A57
		public static LocalizedString BecCompanyNotFound
		{
			get
			{
				return new LocalizedString("BecCompanyNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003878 File Offset: 0x00001A78
		public static LocalizedString AdditionalDebugInfo(string request, string response, string info)
		{
			return new LocalizedString("AdditionalDebugInfo", "ExB01114", false, true, Strings.ResourceManager, new object[]
			{
				request,
				response,
				info
			});
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000038AF File Offset: 0x00001AAF
		public static LocalizedString ErrorNoRecordInDatabase
		{
			get
			{
				return new LocalizedString("ErrorNoRecordInDatabase", "ExCFCC64", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000038CD File Offset: 0x00001ACD
		public static LocalizedString BecErrorPropertyNotSettable
		{
			get
			{
				return new LocalizedString("BecErrorPropertyNotSettable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000038EC File Offset: 0x00001AEC
		public static LocalizedString ErrorMaxMembershipLimit(string memberName)
		{
			return new LocalizedString("ErrorMaxMembershipLimit", "ExCC8AE5", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000391C File Offset: 0x00001B1C
		public static LocalizedString ErrorCannotFindAcceptedDomain(string domainName)
		{
			return new LocalizedString("ErrorCannotFindAcceptedDomain", "Ex550E91", false, true, Strings.ResourceManager, new object[]
			{
				domainName
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000394C File Offset: 0x00001B4C
		public static LocalizedString ErrorCannotGetNamespaceId(string domain)
		{
			return new LocalizedString("ErrorCannotGetNamespaceId", "Ex5FF1C3", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000397C File Offset: 0x00001B7C
		public static LocalizedString ErrorUserOrganizationIsNull(string adminName)
		{
			return new LocalizedString("ErrorUserOrganizationIsNull", "Ex308959", false, true, Strings.ResourceManager, new object[]
			{
				adminName
			});
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000039AB File Offset: 0x00001BAB
		public static LocalizedString ErrorEmailAddressTooLong
		{
			get
			{
				return new LocalizedString("ErrorEmailAddressTooLong", "Ex8EFBF5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000039CC File Offset: 0x00001BCC
		public static LocalizedString ErrorManagedMemberDoesNotExist(string memberName)
		{
			return new LocalizedString("ErrorManagedMemberDoesNotExist", "ExFE76C4", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000039FB File Offset: 0x00001BFB
		public static LocalizedString BecErrorServiceUnavailable
		{
			get
			{
				return new LocalizedString("BecErrorServiceUnavailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A1C File Offset: 0x00001C1C
		public static LocalizedString IDSErrorUnexpectedXmlForCreatePassports(string memberName, string result)
		{
			return new LocalizedString("IDSErrorUnexpectedXmlForCreatePassports", "ExC2254F", false, true, Strings.ResourceManager, new object[]
			{
				memberName,
				result
			});
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003A4F File Offset: 0x00001C4F
		public static LocalizedString IDSErrorEmptySignInNamesForNetId
		{
			get
			{
				return new LocalizedString("IDSErrorEmptySignInNamesForNetId", "ExDB6EC4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003A70 File Offset: 0x00001C70
		public static LocalizedString ErrorWLCDPartnerAccessException(string message)
		{
			return new LocalizedString("ErrorWLCDPartnerAccessException", "Ex44F3EF", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003A9F File Offset: 0x00001C9F
		public static LocalizedString ErrorUserAlreadyInactive
		{
			get
			{
				return new LocalizedString("ErrorUserAlreadyInactive", "Ex52119D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003ABD File Offset: 0x00001CBD
		public static LocalizedString BecErrorTooManyMappedTenants
		{
			get
			{
				return new LocalizedString("BecErrorTooManyMappedTenants", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static LocalizedString ErrorIDSReturnedNullNetID(string memberName, string federatedIdentity)
		{
			return new LocalizedString("ErrorIDSReturnedNullNetID", "Ex68D71B", false, true, Strings.ResourceManager, new object[]
			{
				memberName,
				federatedIdentity
			});
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003B0F File Offset: 0x00001D0F
		public static LocalizedString BecErrorStringLength
		{
			get
			{
				return new LocalizedString("BecErrorStringLength", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003B2D File Offset: 0x00001D2D
		public static LocalizedString ErrorPartnerNotAuthorized
		{
			get
			{
				return new LocalizedString("ErrorPartnerNotAuthorized", "Ex440094", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003B4B File Offset: 0x00001D4B
		public static LocalizedString ErrorManagedParameterSetPassedInForFederatedNamespace
		{
			get
			{
				return new LocalizedString("ErrorManagedParameterSetPassedInForFederatedNamespace", "Ex275CAB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003B69 File Offset: 0x00001D69
		public static LocalizedString BecDirectoryInternalError
		{
			get
			{
				return new LocalizedString("BecDirectoryInternalError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003B87 File Offset: 0x00001D87
		public static LocalizedString BecUnknownError
		{
			get
			{
				return new LocalizedString("BecUnknownError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static LocalizedString ErrorRedirectionEntryExists(string windowsLiveId)
		{
			return new LocalizedString("ErrorRedirectionEntryExists", "Ex689A9A", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003BD7 File Offset: 0x00001DD7
		public static LocalizedString ErrorPasswordContainsLastName
		{
			get
			{
				return new LocalizedString("ErrorPasswordContainsLastName", "Ex63E272", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public static LocalizedString ErrorInvalidNetId(string memberName)
		{
			return new LocalizedString("ErrorInvalidNetId", "Ex664B35", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003C27 File Offset: 0x00001E27
		public static LocalizedString BecInternalError
		{
			get
			{
				return new LocalizedString("BecInternalError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003C45 File Offset: 0x00001E45
		public static LocalizedString ErrorDomainIsManaged
		{
			get
			{
				return new LocalizedString("ErrorDomainIsManaged", "Ex38C789", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003C63 File Offset: 0x00001E63
		public static LocalizedString ErrorPasswordContainsMemberName
		{
			get
			{
				return new LocalizedString("ErrorPasswordContainsMemberName", "Ex95C592", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003C84 File Offset: 0x00001E84
		public static LocalizedString ErrorLiveIdAlreadyExistsAsManaged(string windowsLiveId)
		{
			return new LocalizedString("ErrorLiveIdAlreadyExistsAsManaged", "ExB10BE9", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public static LocalizedString ErrorPartnerCannotModifyProtectedField
		{
			get
			{
				return new LocalizedString("ErrorPartnerCannotModifyProtectedField", "ExA65C1B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003CD1 File Offset: 0x00001ED1
		public static LocalizedString ErrorMissingNetIDWhenBypassWLID
		{
			get
			{
				return new LocalizedString("ErrorMissingNetIDWhenBypassWLID", "Ex81D36A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003CEF File Offset: 0x00001EEF
		public static LocalizedString ErrorEmailStartsAndEndsWithDot
		{
			get
			{
				return new LocalizedString("ErrorEmailStartsAndEndsWithDot", "ExD5B19B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D10 File Offset: 0x00001F10
		public static LocalizedString ErrorLiveIdAlreadyExistsAsEASI(string windowsLiveId)
		{
			return new LocalizedString("ErrorLiveIdAlreadyExistsAsEASI", "Ex280342", false, true, Strings.ResourceManager, new object[]
			{
				windowsLiveId
			});
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003D3F File Offset: 0x00001F3F
		public static LocalizedString ErrorCodeInvalidNetId
		{
			get
			{
				return new LocalizedString("ErrorCodeInvalidNetId", "Ex94410E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003D5D File Offset: 0x00001F5D
		public static LocalizedString ErrorMemberExists
		{
			get
			{
				return new LocalizedString("ErrorMemberExists", "ExB09BC6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003D7B File Offset: 0x00001F7B
		public static LocalizedString BecErrorRequiredProperty
		{
			get
			{
				return new LocalizedString("BecErrorRequiredProperty", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D9C File Offset: 0x00001F9C
		public static LocalizedString IDSErrorBlob(string errorBlob)
		{
			return new LocalizedString("IDSErrorBlob", "Ex43D0F9", false, true, Strings.ResourceManager, new object[]
			{
				errorBlob
			});
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003DCC File Offset: 0x00001FCC
		public static LocalizedString ErrorPasswordBlank(string memberName)
		{
			return new LocalizedString("ErrorPasswordBlank", "Ex090AAB", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003DFB File Offset: 0x00001FFB
		public static LocalizedString ErrorInvalidBrandData
		{
			get
			{
				return new LocalizedString("ErrorInvalidBrandData", "Ex5952D7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E1C File Offset: 0x0000201C
		public static LocalizedString IDSInternalError(string additionalMessage)
		{
			return new LocalizedString("IDSInternalError", "Ex7BFB37", false, true, Strings.ResourceManager, new object[]
			{
				additionalMessage
			});
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003E4B File Offset: 0x0000204B
		public static LocalizedString ErrorCanNotSetPasswordOrResetOnFederatedAccount
		{
			get
			{
				return new LocalizedString("ErrorCanNotSetPasswordOrResetOnFederatedAccount", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003E69 File Offset: 0x00002069
		public static LocalizedString ErrorSignInNameTooLong
		{
			get
			{
				return new LocalizedString("ErrorSignInNameTooLong", "Ex5C1AC7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003E88 File Offset: 0x00002088
		public static LocalizedString ErrorInvalidMemberName(string memberName)
		{
			return new LocalizedString("ErrorInvalidMemberName", "ExD68B92", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003EB7 File Offset: 0x000020B7
		public static LocalizedString ErrorNamespaceDoesNotExist
		{
			get
			{
				return new LocalizedString("ErrorNamespaceDoesNotExist", "Ex11F39D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static LocalizedString ErrorMemberNameInUse(string memberName)
		{
			return new LocalizedString("ErrorMemberNameInUse", "Ex9957D0", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003F07 File Offset: 0x00002107
		public static LocalizedString IDSErrorMissingCredFlagsNode
		{
			get
			{
				return new LocalizedString("IDSErrorMissingCredFlagsNode", "Ex40C1B4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003F28 File Offset: 0x00002128
		public static LocalizedString ErrorPasswordInvalid(string memberName)
		{
			return new LocalizedString("ErrorPasswordInvalid", "Ex816E42", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003F57 File Offset: 0x00002157
		public static LocalizedString ErrorNonEASIMemberExists
		{
			get
			{
				return new LocalizedString("ErrorNonEASIMemberExists", "ExE53E32", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F78 File Offset: 0x00002178
		public static LocalizedString ErrorPrefix(string identity)
		{
			return new LocalizedString("ErrorPrefix", "Ex589ADA", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003FA8 File Offset: 0x000021A8
		public static LocalizedString ErrorOnGetNamespaceId(string domain)
		{
			return new LocalizedString("ErrorOnGetNamespaceId", "Ex42CD0A", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static LocalizedString ErrorPasswordTooLong(string memberName)
		{
			return new LocalizedString("ErrorPasswordTooLong", "Ex52FA3C", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004007 File Offset: 0x00002207
		public static LocalizedString ErrorPasswordContainsFirstName
		{
			get
			{
				return new LocalizedString("ErrorPasswordContainsFirstName", "Ex8845A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004025 File Offset: 0x00002225
		public static LocalizedString ErrorDomainNameIsNull
		{
			get
			{
				return new LocalizedString("ErrorDomainNameIsNull", "ExD819C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004043 File Offset: 0x00002243
		public static LocalizedString BecErrorQuotaExceeded
		{
			get
			{
				return new LocalizedString("BecErrorQuotaExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004064 File Offset: 0x00002264
		public static LocalizedString ErrorManagedMemberNotExists(string memberName)
		{
			return new LocalizedString("ErrorManagedMemberNotExists", "Ex0CE124", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004093 File Offset: 0x00002293
		public static LocalizedString ErrorDatabaseOnMaintenance
		{
			get
			{
				return new LocalizedString("ErrorDatabaseOnMaintenance", "ExC74032", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000040B4 File Offset: 0x000022B4
		public static LocalizedString ErrorMemberNameUnavailableUsedAlternateAlias(string memberName)
		{
			return new LocalizedString("ErrorMemberNameUnavailableUsedAlternateAlias", "Ex309929", false, true, Strings.ResourceManager, new object[]
			{
				memberName
			});
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000040E3 File Offset: 0x000022E3
		public static LocalizedString ErrorLiveIdWithFederatedIdentityExists
		{
			get
			{
				return new LocalizedString("ErrorLiveIdWithFederatedIdentityExists", "Ex48C427", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004101 File Offset: 0x00002301
		public static LocalizedString ErrorSignInNameTooShort
		{
			get
			{
				return new LocalizedString("ErrorSignInNameTooShort", "ExBDC882", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004120 File Offset: 0x00002320
		public static LocalizedString ErrorLiveServicesPartnerAccessException(string message, string info)
		{
			return new LocalizedString("ErrorLiveServicesPartnerAccessException", "ExF735F7", false, true, Strings.ResourceManager, new object[]
			{
				message,
				info
			});
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004153 File Offset: 0x00002353
		public static LocalizedString ErrorSPFPasswordTooShort
		{
			get
			{
				return new LocalizedString("ErrorSPFPasswordTooShort", "ExFEBDFD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004171 File Offset: 0x00002371
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(107);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.DatacenterStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			BecErrorThrottling = 3442334529U,
			// Token: 0x04000005 RID: 5
			ErrorSPFInvalidMemberName = 2921179735U,
			// Token: 0x04000006 RID: 6
			BecAccessDenied = 3165600047U,
			// Token: 0x04000007 RID: 7
			BecIncorrectPassword = 534972034U,
			// Token: 0x04000008 RID: 8
			BecErrorNotFouond = 2713690330U,
			// Token: 0x04000009 RID: 9
			ErrorManagedParameterSetPassedInForFederatedTenant = 2548455642U,
			// Token: 0x0400000A RID: 10
			IDSErrorMissingNamespaceIdNode = 457807320U,
			// Token: 0x0400000B RID: 11
			ErrorParameterIsIncorrect = 3569539930U,
			// Token: 0x0400000C RID: 12
			ErrorCodeProfileDoesNotExists = 3398198740U,
			// Token: 0x0400000D RID: 13
			ErrorMemberLockedOutBecauseOfPasswordAttempts = 71190514U,
			// Token: 0x0400000E RID: 14
			IDSErrorEmptyCredFlagsNode = 507263250U,
			// Token: 0x0400000F RID: 15
			ErrorPasswordMatchesAccountWithSameMemberName = 2457590018U,
			// Token: 0x04000010 RID: 16
			BecErrorDomainValidation = 240550867U,
			// Token: 0x04000011 RID: 17
			ErrorDomainDoesNotExist = 2539075827U,
			// Token: 0x04000012 RID: 18
			ErrorSignInNameInCompleteOrInvalid = 1628902957U,
			// Token: 0x04000013 RID: 19
			ErrorUserBlocked = 746302685U,
			// Token: 0x04000014 RID: 20
			BecTransientError = 2001068460U,
			// Token: 0x04000015 RID: 21
			ErrorFederatedParameterSetPassedInForManagedTenant = 2511795258U,
			// Token: 0x04000016 RID: 22
			IDSErrorEmptyPuidNode = 2085485339U,
			// Token: 0x04000017 RID: 23
			BecRedirection = 1384815350U,
			// Token: 0x04000018 RID: 24
			InvalidNetId = 2259851215U,
			// Token: 0x04000019 RID: 25
			ErrorNameContainsBlockedWord = 1908027994U,
			// Token: 0x0400001A RID: 26
			ErrorServiceUnavailableDueToInternalError = 153681091U,
			// Token: 0x0400001B RID: 27
			IDSErrorEmptyNetIdNode = 1374323893U,
			// Token: 0x0400001C RID: 28
			ErrorSPFPasswordTooLong = 1890206562U,
			// Token: 0x0400001D RID: 29
			BecErrorInvalidHeader = 3875381480U,
			// Token: 0x0400001E RID: 30
			BecErrorUserExists = 2343187933U,
			// Token: 0x0400001F RID: 31
			ErrorUserNameReserved = 1260215560U,
			// Token: 0x04000020 RID: 32
			ErrorFieldContainsInvalidCharacters = 286588848U,
			// Token: 0x04000021 RID: 33
			ErrorPasswordRequired = 2252511124U,
			// Token: 0x04000022 RID: 34
			ErrorInputContainsForbiddenWord = 3038881494U,
			// Token: 0x04000023 RID: 35
			ErrorFederatedAccountAlreadyExists = 1170008863U,
			// Token: 0x04000024 RID: 36
			ErrorEmailNameContainsInvalidCharacters = 2883915563U,
			// Token: 0x04000025 RID: 37
			ErrorPasswordContainedInSQ = 116822391U,
			// Token: 0x04000026 RID: 38
			BecErrorInvalidContext = 3327865544U,
			// Token: 0x04000027 RID: 39
			ErrorsRemovedMailboxHaveNoNetID = 2629176178U,
			// Token: 0x04000028 RID: 40
			ErrorPasswordContainsInvalidCharacters = 3821098863U,
			// Token: 0x04000029 RID: 41
			ErrorInvalidPassportId = 24718424U,
			// Token: 0x0400002A RID: 42
			BecErrorQuota = 3848807642U,
			// Token: 0x0400002B RID: 43
			BecErrorInvalidLicense = 159495284U,
			// Token: 0x0400002C RID: 44
			ErrorSPFPasswordIsBlank = 1626889520U,
			// Token: 0x0400002D RID: 45
			ErrorCannotRecoverLiveIdMismatchInstanceType = 4122120961U,
			// Token: 0x0400002E RID: 46
			BecErrorSubscription = 2570238007U,
			// Token: 0x0400002F RID: 47
			ErrorSecretAnswerContainsPassword = 2854433926U,
			// Token: 0x04000030 RID: 48
			ErrorWLCDInternal = 344542475U,
			// Token: 0x04000031 RID: 49
			ErrorManagedMemberExistsSPF = 1112425574U,
			// Token: 0x04000032 RID: 50
			BecErrorUniquenessValidation = 2178303307U,
			// Token: 0x04000033 RID: 51
			ErrorPasswordIsInvalid = 3710614060U,
			// Token: 0x04000034 RID: 52
			BecErrorInvalidWeakPassword = 3791085944U,
			// Token: 0x04000035 RID: 53
			ErrorIncompleteEmailAddress = 1520187672U,
			// Token: 0x04000036 RID: 54
			ErrorSecretQuestionContainsPassword = 1810820280U,
			// Token: 0x04000037 RID: 55
			BecUserInRecycleState = 561051570U,
			// Token: 0x04000038 RID: 56
			ErrorFederatedParameterSetPassedInForManagedNamespace = 854137693U,
			// Token: 0x04000039 RID: 57
			InternalError = 3194642979U,
			// Token: 0x0400003A RID: 58
			ErrorMemberNameAndFederatedIdentityNotMatch = 764417362U,
			// Token: 0x0400003B RID: 59
			ErrorArchiveOnly = 1746286164U,
			// Token: 0x0400003C RID: 60
			BecErrorSyntaxValidation = 2776399702U,
			// Token: 0x0400003D RID: 61
			IDSErrorEmptyNamespaceIdNode = 2366837181U,
			// Token: 0x0400003E RID: 62
			ErrorMemberIsLocked = 2439548284U,
			// Token: 0x0400003F RID: 63
			ErrorSecretQuestionContainsMemberName = 1968969488U,
			// Token: 0x04000040 RID: 64
			BecErrorAccountDisabled = 370795085U,
			// Token: 0x04000041 RID: 65
			BecErrorInvalidPassword = 1787101328U,
			// Token: 0x04000042 RID: 66
			ErrorCannotRenameCredentialToSameName = 2743094604U,
			// Token: 0x04000043 RID: 67
			ErrorEnableRoomMailboxAccountParameterFalseInDatacenter = 2646378639U,
			// Token: 0x04000044 RID: 68
			ErrorAccountIsNotEASI = 3978792718U,
			// Token: 0x04000045 RID: 69
			NotAuthorized = 1953245018U,
			// Token: 0x04000046 RID: 70
			WindowsLiveIdProvisioningHandlerException = 888348176U,
			// Token: 0x04000047 RID: 71
			OrganizationIsReadOnly = 4169169063U,
			// Token: 0x04000048 RID: 72
			ErrorEmailNameStartAfterDot = 1190886676U,
			// Token: 0x04000049 RID: 73
			ErrorNamespaceNotFound = 3074197328U,
			// Token: 0x0400004A RID: 74
			BecIdentityInternalError = 3693389389U,
			// Token: 0x0400004B RID: 75
			BecCompanyNotFound = 2927230868U,
			// Token: 0x0400004C RID: 76
			ErrorNoRecordInDatabase = 2733812604U,
			// Token: 0x0400004D RID: 77
			BecErrorPropertyNotSettable = 2692387272U,
			// Token: 0x0400004E RID: 78
			ErrorEmailAddressTooLong = 2602850872U,
			// Token: 0x0400004F RID: 79
			BecErrorServiceUnavailable = 1479442559U,
			// Token: 0x04000050 RID: 80
			IDSErrorEmptySignInNamesForNetId = 2501723808U,
			// Token: 0x04000051 RID: 81
			ErrorUserAlreadyInactive = 4277226160U,
			// Token: 0x04000052 RID: 82
			BecErrorTooManyMappedTenants = 3142877869U,
			// Token: 0x04000053 RID: 83
			BecErrorStringLength = 1684749577U,
			// Token: 0x04000054 RID: 84
			ErrorPartnerNotAuthorized = 509547158U,
			// Token: 0x04000055 RID: 85
			ErrorManagedParameterSetPassedInForFederatedNamespace = 1770558589U,
			// Token: 0x04000056 RID: 86
			BecDirectoryInternalError = 312656184U,
			// Token: 0x04000057 RID: 87
			BecUnknownError = 364820960U,
			// Token: 0x04000058 RID: 88
			ErrorPasswordContainsLastName = 829279753U,
			// Token: 0x04000059 RID: 89
			BecInternalError = 3970665479U,
			// Token: 0x0400005A RID: 90
			ErrorDomainIsManaged = 938622763U,
			// Token: 0x0400005B RID: 91
			ErrorPasswordContainsMemberName = 3197989195U,
			// Token: 0x0400005C RID: 92
			ErrorPartnerCannotModifyProtectedField = 706972491U,
			// Token: 0x0400005D RID: 93
			ErrorMissingNetIDWhenBypassWLID = 1463063580U,
			// Token: 0x0400005E RID: 94
			ErrorEmailStartsAndEndsWithDot = 3151258897U,
			// Token: 0x0400005F RID: 95
			ErrorCodeInvalidNetId = 524928998U,
			// Token: 0x04000060 RID: 96
			ErrorMemberExists = 146893062U,
			// Token: 0x04000061 RID: 97
			BecErrorRequiredProperty = 1316852426U,
			// Token: 0x04000062 RID: 98
			ErrorInvalidBrandData = 2872232368U,
			// Token: 0x04000063 RID: 99
			ErrorCanNotSetPasswordOrResetOnFederatedAccount = 3566184850U,
			// Token: 0x04000064 RID: 100
			ErrorSignInNameTooLong = 840675049U,
			// Token: 0x04000065 RID: 101
			ErrorNamespaceDoesNotExist = 2454442314U,
			// Token: 0x04000066 RID: 102
			IDSErrorMissingCredFlagsNode = 2655490445U,
			// Token: 0x04000067 RID: 103
			ErrorNonEASIMemberExists = 3197291445U,
			// Token: 0x04000068 RID: 104
			ErrorPasswordContainsFirstName = 1384046745U,
			// Token: 0x04000069 RID: 105
			ErrorDomainNameIsNull = 1475855582U,
			// Token: 0x0400006A RID: 106
			BecErrorQuotaExceeded = 1895268805U,
			// Token: 0x0400006B RID: 107
			ErrorDatabaseOnMaintenance = 3873716657U,
			// Token: 0x0400006C RID: 108
			ErrorLiveIdWithFederatedIdentityExists = 3937658635U,
			// Token: 0x0400006D RID: 109
			ErrorSignInNameTooShort = 2857272973U,
			// Token: 0x0400006E RID: 110
			ErrorSPFPasswordTooShort = 173165206U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000070 RID: 112
			ErrorCannotChangeMemberFederationState,
			// Token: 0x04000071 RID: 113
			ErrorManagedMemberExists,
			// Token: 0x04000072 RID: 114
			ErrorEvictLiveIdMemberExists,
			// Token: 0x04000073 RID: 115
			ErrorPasswordIncludesMemberName,
			// Token: 0x04000074 RID: 116
			ErrorEvictLiveIdMemberNotExists,
			// Token: 0x04000075 RID: 117
			IDSErrorUnexpectedResultsForGetProfileByAttributes,
			// Token: 0x04000076 RID: 118
			ErrorMemberNameUnavailableUsedForEASI,
			// Token: 0x04000077 RID: 119
			ErrorPasswordIncludesInvalidChars,
			// Token: 0x04000078 RID: 120
			VerboseEvictMember,
			// Token: 0x04000079 RID: 121
			ErrorUnknown,
			// Token: 0x0400007A RID: 122
			ErrorCannotUseReservedLiveId,
			// Token: 0x0400007B RID: 123
			ErrorWindowsLiveIdRequired,
			// Token: 0x0400007C RID: 124
			ErrorRedirectionEntryManagerException,
			// Token: 0x0400007D RID: 125
			ErrorCannotImportForNamespaceType,
			// Token: 0x0400007E RID: 126
			ErrorUnmanagedMemberNotExists,
			// Token: 0x0400007F RID: 127
			ErrorMemberNameUnavailableUsedForDL,
			// Token: 0x04000080 RID: 128
			ErrorLiveIdDoesNotExist,
			// Token: 0x04000081 RID: 129
			IDSErrorUnexpectedResultsForCreatePassports,
			// Token: 0x04000082 RID: 130
			ErrorConfigurationUnitNotFound,
			// Token: 0x04000083 RID: 131
			ErrorPasswordTooShort,
			// Token: 0x04000084 RID: 132
			ErrorCannotRemoveWindowsLiveIDFromProxyAddresses,
			// Token: 0x04000085 RID: 133
			ErrorGuidNotParsable,
			// Token: 0x04000086 RID: 134
			ErrorImportLiveIdManagedMemberExists,
			// Token: 0x04000087 RID: 135
			ErrorManagedMemberDoesNotExistForByolid,
			// Token: 0x04000088 RID: 136
			ErrorCannotRenameAccrossNamespaceTypes,
			// Token: 0x04000089 RID: 137
			OrganizationIsImmutable,
			// Token: 0x0400008A RID: 138
			ErrorCannotDetermineLiveInstance,
			// Token: 0x0400008B RID: 139
			ErrorFailedToEvictMember,
			// Token: 0x0400008C RID: 140
			ErrorParameterRequired,
			// Token: 0x0400008D RID: 141
			ErrorOnGetProfile,
			// Token: 0x0400008E RID: 142
			ErrorMemberNameBlocked,
			// Token: 0x0400008F RID: 143
			SPFInternalError,
			// Token: 0x04000090 RID: 144
			ErrorMemberNameUnavailable,
			// Token: 0x04000091 RID: 145
			ErrorUnmanagedMemberExists,
			// Token: 0x04000092 RID: 146
			AdditionalDebugInfo,
			// Token: 0x04000093 RID: 147
			ErrorMaxMembershipLimit,
			// Token: 0x04000094 RID: 148
			ErrorCannotFindAcceptedDomain,
			// Token: 0x04000095 RID: 149
			ErrorCannotGetNamespaceId,
			// Token: 0x04000096 RID: 150
			ErrorUserOrganizationIsNull,
			// Token: 0x04000097 RID: 151
			ErrorManagedMemberDoesNotExist,
			// Token: 0x04000098 RID: 152
			IDSErrorUnexpectedXmlForCreatePassports,
			// Token: 0x04000099 RID: 153
			ErrorWLCDPartnerAccessException,
			// Token: 0x0400009A RID: 154
			ErrorIDSReturnedNullNetID,
			// Token: 0x0400009B RID: 155
			ErrorRedirectionEntryExists,
			// Token: 0x0400009C RID: 156
			ErrorInvalidNetId,
			// Token: 0x0400009D RID: 157
			ErrorLiveIdAlreadyExistsAsManaged,
			// Token: 0x0400009E RID: 158
			ErrorLiveIdAlreadyExistsAsEASI,
			// Token: 0x0400009F RID: 159
			IDSErrorBlob,
			// Token: 0x040000A0 RID: 160
			ErrorPasswordBlank,
			// Token: 0x040000A1 RID: 161
			IDSInternalError,
			// Token: 0x040000A2 RID: 162
			ErrorInvalidMemberName,
			// Token: 0x040000A3 RID: 163
			ErrorMemberNameInUse,
			// Token: 0x040000A4 RID: 164
			ErrorPasswordInvalid,
			// Token: 0x040000A5 RID: 165
			ErrorPrefix,
			// Token: 0x040000A6 RID: 166
			ErrorOnGetNamespaceId,
			// Token: 0x040000A7 RID: 167
			ErrorPasswordTooLong,
			// Token: 0x040000A8 RID: 168
			ErrorManagedMemberNotExists,
			// Token: 0x040000A9 RID: 169
			ErrorMemberNameUnavailableUsedAlternateAlias,
			// Token: 0x040000AA RID: 170
			ErrorLiveServicesPartnerAccessException
		}
	}
}
