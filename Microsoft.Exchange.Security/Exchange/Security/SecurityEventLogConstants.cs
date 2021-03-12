using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000002 RID: 2
	public static class SecurityEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdServerUnreachable = new ExEventLog.EventTuple(3221488616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpnMismatch = new ExEventLog.EventTuple(2147746793U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NativeApiFailed = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIDAmbiguous = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotConnectToAD = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotConnectToAuthService = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceConfigured = new ExEventLog.EventTuple(1007U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdServerError = new ExEventLog.EventTuple(2147746800U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceStarting = new ExEventLog.EventTuple(1074004977U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceStarted = new ExEventLog.EventTuple(1074004978U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceStopped = new ExEventLog.EventTuple(1074004979U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceFailedToRegisterEndpoint = new ExEventLog.EventTuple(3221488628U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotConnectToHomeRealmDiscovery = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederatedSTSUnreachable = new ExEventLog.EventTuple(3221488630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GeneralException = new ExEventLog.EventTuple(3221488631U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AuthServiceFailedToInitRPS = new ExEventLog.EventTuple(3221488632U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WSManCookieCreationException = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdServerUnreachableKHI = new ExEventLog.EventTuple(3221488634U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotConnectToHomeRealmDiscoveryKHI = new ExEventLog.EventTuple(3221488635U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ShibbolethSTSUnreachable = new ExEventLog.EventTuple(3221488636U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LiveIdLoginPostError = new ExEventLog.EventTuple(2147746813U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyTimedOutRequests_TenantAlert = new ExEventLog.EventTuple(2147746814U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyBadCreds_TenantAlert = new ExEventLog.EventTuple(2147746815U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyFailedResponses_TenantAlert = new ExEventLog.EventTuple(2147746816U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTokensTooLarge_TenantAlert = new ExEventLog.EventTuple(2147746817U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyTimedOutRequests_TenantAlert = new ExEventLog.EventTuple(2147746818U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyBadCreds_TenantAlert = new ExEventLog.EventTuple(2147746819U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyFailedResponses_TenantAlert = new ExEventLog.EventTuple(2147746820U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningTokensTooLarge_TenantAlert = new ExEventLog.EventTuple(2147746821U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigReadError = new ExEventLog.EventTuple(3221488646U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RealmDiscoveryReadError = new ExEventLog.EventTuple(3221488647U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederatedSTSUrlNotSecure_TenantAlert = new ExEventLog.EventTuple(3221488648U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GeneralClientException = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyTimedOutRequests_Forensic = new ExEventLog.EventTuple(2147746826U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyBadCreds_Forensic = new ExEventLog.EventTuple(2147746827U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTooManyFailedResponses_Forensic = new ExEventLog.EventTuple(2147746828U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgBlacklistedTokensTooLarge_Forensic = new ExEventLog.EventTuple(2147746829U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyTimedOutRequests_Forensic = new ExEventLog.EventTuple(2147746830U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyBadCreds_Forensic = new ExEventLog.EventTuple(2147746831U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningManyFailedResponses_Forensic = new ExEventLog.EventTuple(2147746832U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgWarningTokensTooLarge_Forensic = new ExEventLog.EventTuple(2147746833U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederatedSTSUrlNotSecure_Forensic = new ExEventLog.EventTuple(3221488658U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceIsCalledWhenShuttingDown = new ExEventLog.EventTuple(2147746835U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailToIssueTokenForOAB = new ExEventLog.EventTuple(3221488660U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailToAuthenticateTokenForOAB = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OfflineAuthFailed = new ExEventLog.EventTuple(3221488662U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadOfflineAuthProvisioningFlagsFailed = new ExEventLog.EventTuple(3221488663U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessOfflineHRDFailed = new ExEventLog.EventTuple(3221488664U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADHRDCorrupted = new ExEventLog.EventTuple(3221488665U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ReadMsoEndpointTypeFailed = new ExEventLog.EventTuple(3221488666U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateLastLogonTimeFailed = new ExEventLog.EventTuple(3221488667U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RequestTimeout = new ExEventLog.EventTuple(2147746844U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailToLoadLocalConfiguration = new ExEventLog.EventTuple(3221489617U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthPartnerApplicationMissingCertificates = new ExEventLog.EventTuple(2147747794U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthAuthServerMissingCertificates = new ExEventLog.EventTuple(2147747795U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthSigningCertificateNotFoundOrMissingPrivateKey = new ExEventLog.EventTuple(2147747796U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailToReadSigningCertificates = new ExEventLog.EventTuple(3221489621U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailToAuthenticateToken = new ExEventLog.EventTuple(2147747798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthFailedWhileReadingMetadata = new ExEventLog.EventTuple(2147747799U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthNewCertificatesFromMetadataUrl = new ExEventLog.EventTuple(2147747800U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OAuthMoreThanOneAuthServerWithAuthorizationEndpoint = new ExEventLog.EventTuple(2147747801U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackendRehydrationRehydrated = new ExEventLog.EventTuple(265145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackendRehydrationError = new ExEventLog.EventTuple(3221490618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BackendRehydrationNoTokenSerializationPermission = new ExEventLog.EventTuple(3221490619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MSARPSServiceUnhandledException = new ExEventLog.EventTuple(3221491617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MSARPSLoadException = new ExEventLog.EventTuple(3221491618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MSARPSTicketParsingException = new ExEventLog.EventTuple(3221491619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x04000045 RID: 69
			Requests = 1,
			// Token: 0x04000046 RID: 70
			Configuration,
			// Token: 0x04000047 RID: 71
			Server
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x04000049 RID: 73
			LiveIdServerUnreachable = 3221488616U,
			// Token: 0x0400004A RID: 74
			UpnMismatch = 2147746793U,
			// Token: 0x0400004B RID: 75
			NativeApiFailed = 3221488618U,
			// Token: 0x0400004C RID: 76
			LiveIDAmbiguous = 3221488620U,
			// Token: 0x0400004D RID: 77
			CannotConnectToAD,
			// Token: 0x0400004E RID: 78
			CannotConnectToAuthService,
			// Token: 0x0400004F RID: 79
			AuthServiceConfigured = 1007U,
			// Token: 0x04000050 RID: 80
			LiveIdServerError = 2147746800U,
			// Token: 0x04000051 RID: 81
			AuthServiceStarting = 1074004977U,
			// Token: 0x04000052 RID: 82
			AuthServiceStarted,
			// Token: 0x04000053 RID: 83
			AuthServiceStopped,
			// Token: 0x04000054 RID: 84
			AuthServiceFailedToRegisterEndpoint = 3221488628U,
			// Token: 0x04000055 RID: 85
			CannotConnectToHomeRealmDiscovery,
			// Token: 0x04000056 RID: 86
			FederatedSTSUnreachable,
			// Token: 0x04000057 RID: 87
			GeneralException,
			// Token: 0x04000058 RID: 88
			AuthServiceFailedToInitRPS,
			// Token: 0x04000059 RID: 89
			WSManCookieCreationException,
			// Token: 0x0400005A RID: 90
			LiveIdServerUnreachableKHI,
			// Token: 0x0400005B RID: 91
			CannotConnectToHomeRealmDiscoveryKHI,
			// Token: 0x0400005C RID: 92
			ShibbolethSTSUnreachable,
			// Token: 0x0400005D RID: 93
			LiveIdLoginPostError = 2147746813U,
			// Token: 0x0400005E RID: 94
			OrgBlacklistedTooManyTimedOutRequests_TenantAlert,
			// Token: 0x0400005F RID: 95
			OrgBlacklistedTooManyBadCreds_TenantAlert,
			// Token: 0x04000060 RID: 96
			OrgBlacklistedTooManyFailedResponses_TenantAlert,
			// Token: 0x04000061 RID: 97
			OrgBlacklistedTokensTooLarge_TenantAlert,
			// Token: 0x04000062 RID: 98
			OrgWarningManyTimedOutRequests_TenantAlert,
			// Token: 0x04000063 RID: 99
			OrgWarningManyBadCreds_TenantAlert,
			// Token: 0x04000064 RID: 100
			OrgWarningManyFailedResponses_TenantAlert,
			// Token: 0x04000065 RID: 101
			OrgWarningTokensTooLarge_TenantAlert,
			// Token: 0x04000066 RID: 102
			ConfigReadError = 3221488646U,
			// Token: 0x04000067 RID: 103
			RealmDiscoveryReadError,
			// Token: 0x04000068 RID: 104
			FederatedSTSUrlNotSecure_TenantAlert,
			// Token: 0x04000069 RID: 105
			GeneralClientException,
			// Token: 0x0400006A RID: 106
			OrgBlacklistedTooManyTimedOutRequests_Forensic = 2147746826U,
			// Token: 0x0400006B RID: 107
			OrgBlacklistedTooManyBadCreds_Forensic,
			// Token: 0x0400006C RID: 108
			OrgBlacklistedTooManyFailedResponses_Forensic,
			// Token: 0x0400006D RID: 109
			OrgBlacklistedTokensTooLarge_Forensic,
			// Token: 0x0400006E RID: 110
			OrgWarningManyTimedOutRequests_Forensic,
			// Token: 0x0400006F RID: 111
			OrgWarningManyBadCreds_Forensic,
			// Token: 0x04000070 RID: 112
			OrgWarningManyFailedResponses_Forensic,
			// Token: 0x04000071 RID: 113
			OrgWarningTokensTooLarge_Forensic,
			// Token: 0x04000072 RID: 114
			FederatedSTSUrlNotSecure_Forensic = 3221488658U,
			// Token: 0x04000073 RID: 115
			ServiceIsCalledWhenShuttingDown = 2147746835U,
			// Token: 0x04000074 RID: 116
			OAuthFailToIssueTokenForOAB = 3221488660U,
			// Token: 0x04000075 RID: 117
			OAuthFailToAuthenticateTokenForOAB,
			// Token: 0x04000076 RID: 118
			OfflineAuthFailed,
			// Token: 0x04000077 RID: 119
			ReadOfflineAuthProvisioningFlagsFailed,
			// Token: 0x04000078 RID: 120
			AccessOfflineHRDFailed,
			// Token: 0x04000079 RID: 121
			ADHRDCorrupted,
			// Token: 0x0400007A RID: 122
			ReadMsoEndpointTypeFailed,
			// Token: 0x0400007B RID: 123
			UpdateLastLogonTimeFailed,
			// Token: 0x0400007C RID: 124
			RequestTimeout = 2147746844U,
			// Token: 0x0400007D RID: 125
			OAuthFailToLoadLocalConfiguration = 3221489617U,
			// Token: 0x0400007E RID: 126
			OAuthPartnerApplicationMissingCertificates = 2147747794U,
			// Token: 0x0400007F RID: 127
			OAuthAuthServerMissingCertificates,
			// Token: 0x04000080 RID: 128
			OAuthSigningCertificateNotFoundOrMissingPrivateKey,
			// Token: 0x04000081 RID: 129
			OAuthFailToReadSigningCertificates = 3221489621U,
			// Token: 0x04000082 RID: 130
			OAuthFailToAuthenticateToken = 2147747798U,
			// Token: 0x04000083 RID: 131
			OAuthFailedWhileReadingMetadata,
			// Token: 0x04000084 RID: 132
			OAuthNewCertificatesFromMetadataUrl,
			// Token: 0x04000085 RID: 133
			OAuthMoreThanOneAuthServerWithAuthorizationEndpoint,
			// Token: 0x04000086 RID: 134
			BackendRehydrationRehydrated = 265145U,
			// Token: 0x04000087 RID: 135
			BackendRehydrationError = 3221490618U,
			// Token: 0x04000088 RID: 136
			BackendRehydrationNoTokenSerializationPermission,
			// Token: 0x04000089 RID: 137
			MSARPSServiceUnhandledException = 3221491617U,
			// Token: 0x0400008A RID: 138
			MSARPSLoadException,
			// Token: 0x0400008B RID: 139
			MSARPSTicketParsingException
		}
	}
}
