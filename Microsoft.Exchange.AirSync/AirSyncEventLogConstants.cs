using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000002 RID: 2
	public static class AirSyncEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BadItemReportConversionException = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AirSyncException = new ExEventLog.EventTuple(2147746800U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidHbiLimits = new ExEventLog.EventTuple(2147746801U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MaxHbiTooHigh = new ExEventLog.EventTuple(2147746802U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MinHbiTooLow = new ExEventLog.EventTuple(2147746803U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AirSyncLoaded = new ExEventLog.EventTuple(2147746805U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AirSyncUnloaded = new ExEventLog.EventTuple(2147746806U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveDirectoryTransientError = new ExEventLog.EventTuple(3221488631U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ContentIndexingNotEnabled = new ExEventLog.EventTuple(2147746810U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledDeviceIdTryingToSync = new ExEventLog.EventTuple(2147746811U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeviceIdAndDeviceTypeMustBePresent = new ExEventLog.EventTuple(2147746812U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NonconformingDeviceError = new ExEventLog.EventTuple(2147746813U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConnectionFailed = new ExEventLog.EventTuple(2147746814U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxOffline = new ExEventLog.EventTuple(2147746815U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxAccessDenied = new ExEventLog.EventTuple(2147746816U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccountDisabled = new ExEventLog.EventTuple(2147746817U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxQuotaExceeded = new ExEventLog.EventTuple(2147746818U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BadProxyServerVersion = new ExEventLog.EventTuple(2147746820U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserOnNewMailboxCannotSync = new ExEventLog.EventTuple(2147746821U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserOnLegacyMailboxCannotSync = new ExEventLog.EventTuple(2147746822U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserHasBeenDisabled = new ExEventLog.EventTuple(2147746823U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxConnectionFailed = new ExEventLog.EventTuple(2147746824U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueHasBeenDefaulted = new ExEventLog.EventTuple(2147746825U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyResultTimedOut = new ExEventLog.EventTuple(2147746826U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SecondCasFailureSSL = new ExEventLog.EventTuple(2147746827U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SecondCasFailureNTLM = new ExEventLog.EventTuple(2147746828U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ClientDisabledFromSyncEvent = new ExEventLog.EventTuple(2147746829U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SetPrivilegesFailure = new ExEventLog.EventTuple(3221488654U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MultipleDefaultMobileMailboxPoliciesDetected = new ExEventLog.EventTuple(2147746831U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AverageHbiTooLow = new ExEventLog.EventTuple(2147746832U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HeartbeatSampleSizeTooHigh = new ExEventLog.EventTuple(2147746833U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HeartbeatAlertThresholdTooHigh = new ExEventLog.EventTuple(2147746834U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryInfoMissingError = new ExEventLog.EventTuple(2147746836U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueHasInvalidCharacters = new ExEventLog.EventTuple(2147746838U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueHasBeenDefaultedAndIgnored = new ExEventLog.EventTuple(2147746839U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomTypeDoesNotStartWithIPM = new ExEventLog.EventTuple(2147746840U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RunAsLocalSystem = new ExEventLog.EventTuple(3221488665U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpLevelProxyNotSupported = new ExEventLog.EventTuple(2147746844U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateADDevicesContainer = new ExEventLog.EventTuple(3221488669U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateADDevice = new ExEventLog.EventTuple(3221488670U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserHasBeenBlocked = new ExEventLog.EventTuple(2147746847U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidFireWallConfiguration = new ExEventLog.EventTuple(3221488716U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRegisterADNotification = new ExEventLog.EventTuple(2147746893U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLoadADSettings = new ExEventLog.EventTuple(2147746894U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveDirectoryExternalError = new ExEventLog.EventTuple(3221488719U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveDirectoryOperationError = new ExEventLog.EventTuple(3221488720U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AirSyncFatalException = new ExEventLog.EventTuple(3221488721U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AirSyncUnhandledException = new ExEventLog.EventTuple(3221488722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SyncStateExisted = new ExEventLog.EventTuple(2147746899U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidSyncStateVersion = new ExEventLog.EventTuple(2147746900U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerExternalUrlConfigurationError = new ExEventLog.EventTuple(3221488725U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoAdminMailRecipientsError = new ExEventLog.EventTuple(3221488726U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoGccStoredSecretKey = new ExEventLog.EventTuple(3221488727U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DirectoryAccessDenied = new ExEventLog.EventTuple(3221488728U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoMailboxRights = new ExEventLog.EventTuple(3221488729U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoOrgSettings = new ExEventLog.EventTuple(3221488730U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoPerfCounterTimer = new ExEventLog.EventTuple(2147746907U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoDeviceClassContainer = new ExEventLog.EventTuple(3221488732U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyDeviceClassNodes = new ExEventLog.EventTuple(3221488733U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueOutOfRange = new ExEventLog.EventTuple(3221488734U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueNotParsable = new ExEventLog.EventTuple(3221488735U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueRegistryReadFailure = new ExEventLog.EventTuple(3221488736U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GlobalValueRegistryValueMissing = new ExEventLog.EventTuple(3221488737U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x04000041 RID: 65
			Requests = 1,
			// Token: 0x04000042 RID: 66
			Configuration,
			// Token: 0x04000043 RID: 67
			Server
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x04000045 RID: 69
			BadItemReportConversionException = 3221488623U,
			// Token: 0x04000046 RID: 70
			AirSyncException = 2147746800U,
			// Token: 0x04000047 RID: 71
			InvalidHbiLimits,
			// Token: 0x04000048 RID: 72
			MaxHbiTooHigh,
			// Token: 0x04000049 RID: 73
			MinHbiTooLow,
			// Token: 0x0400004A RID: 74
			AirSyncLoaded = 2147746805U,
			// Token: 0x0400004B RID: 75
			AirSyncUnloaded,
			// Token: 0x0400004C RID: 76
			ActiveDirectoryTransientError = 3221488631U,
			// Token: 0x0400004D RID: 77
			ContentIndexingNotEnabled = 2147746810U,
			// Token: 0x0400004E RID: 78
			DisabledDeviceIdTryingToSync,
			// Token: 0x0400004F RID: 79
			DeviceIdAndDeviceTypeMustBePresent,
			// Token: 0x04000050 RID: 80
			NonconformingDeviceError,
			// Token: 0x04000051 RID: 81
			ConnectionFailed,
			// Token: 0x04000052 RID: 82
			MailboxOffline,
			// Token: 0x04000053 RID: 83
			MailboxAccessDenied,
			// Token: 0x04000054 RID: 84
			AccountDisabled,
			// Token: 0x04000055 RID: 85
			MailboxQuotaExceeded,
			// Token: 0x04000056 RID: 86
			BadProxyServerVersion = 2147746820U,
			// Token: 0x04000057 RID: 87
			UserOnNewMailboxCannotSync,
			// Token: 0x04000058 RID: 88
			UserOnLegacyMailboxCannotSync,
			// Token: 0x04000059 RID: 89
			UserHasBeenDisabled,
			// Token: 0x0400005A RID: 90
			MailboxConnectionFailed,
			// Token: 0x0400005B RID: 91
			GlobalValueHasBeenDefaulted,
			// Token: 0x0400005C RID: 92
			ProxyResultTimedOut,
			// Token: 0x0400005D RID: 93
			SecondCasFailureSSL,
			// Token: 0x0400005E RID: 94
			SecondCasFailureNTLM,
			// Token: 0x0400005F RID: 95
			ClientDisabledFromSyncEvent,
			// Token: 0x04000060 RID: 96
			SetPrivilegesFailure = 3221488654U,
			// Token: 0x04000061 RID: 97
			MultipleDefaultMobileMailboxPoliciesDetected = 2147746831U,
			// Token: 0x04000062 RID: 98
			AverageHbiTooLow,
			// Token: 0x04000063 RID: 99
			HeartbeatSampleSizeTooHigh,
			// Token: 0x04000064 RID: 100
			HeartbeatAlertThresholdTooHigh,
			// Token: 0x04000065 RID: 101
			DiscoveryInfoMissingError = 2147746836U,
			// Token: 0x04000066 RID: 102
			GlobalValueHasInvalidCharacters = 2147746838U,
			// Token: 0x04000067 RID: 103
			GlobalValueHasBeenDefaultedAndIgnored,
			// Token: 0x04000068 RID: 104
			CustomTypeDoesNotStartWithIPM,
			// Token: 0x04000069 RID: 105
			RunAsLocalSystem = 3221488665U,
			// Token: 0x0400006A RID: 106
			UpLevelProxyNotSupported = 2147746844U,
			// Token: 0x0400006B RID: 107
			UnableToCreateADDevicesContainer = 3221488669U,
			// Token: 0x0400006C RID: 108
			UnableToCreateADDevice,
			// Token: 0x0400006D RID: 109
			UserHasBeenBlocked = 2147746847U,
			// Token: 0x0400006E RID: 110
			InvalidFireWallConfiguration = 3221488716U,
			// Token: 0x0400006F RID: 111
			FailedToRegisterADNotification = 2147746893U,
			// Token: 0x04000070 RID: 112
			FailedToLoadADSettings,
			// Token: 0x04000071 RID: 113
			ActiveDirectoryExternalError = 3221488719U,
			// Token: 0x04000072 RID: 114
			ActiveDirectoryOperationError,
			// Token: 0x04000073 RID: 115
			AirSyncFatalException,
			// Token: 0x04000074 RID: 116
			AirSyncUnhandledException,
			// Token: 0x04000075 RID: 117
			SyncStateExisted = 2147746899U,
			// Token: 0x04000076 RID: 118
			InvalidSyncStateVersion,
			// Token: 0x04000077 RID: 119
			ServerExternalUrlConfigurationError = 3221488725U,
			// Token: 0x04000078 RID: 120
			NoAdminMailRecipientsError,
			// Token: 0x04000079 RID: 121
			NoGccStoredSecretKey,
			// Token: 0x0400007A RID: 122
			DirectoryAccessDenied,
			// Token: 0x0400007B RID: 123
			NoMailboxRights,
			// Token: 0x0400007C RID: 124
			NoOrgSettings,
			// Token: 0x0400007D RID: 125
			NoPerfCounterTimer = 2147746907U,
			// Token: 0x0400007E RID: 126
			NoDeviceClassContainer = 3221488732U,
			// Token: 0x0400007F RID: 127
			TooManyDeviceClassNodes,
			// Token: 0x04000080 RID: 128
			GlobalValueOutOfRange,
			// Token: 0x04000081 RID: 129
			GlobalValueNotParsable,
			// Token: 0x04000082 RID: 130
			GlobalValueRegistryReadFailure,
			// Token: 0x04000083 RID: 131
			GlobalValueRegistryValueMissing
		}
	}
}
