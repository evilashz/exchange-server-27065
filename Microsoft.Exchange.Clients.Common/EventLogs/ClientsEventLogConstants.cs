using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.EventLogs
{
	// Token: 0x02000002 RID: 2
	public static class ClientsEventLogConstants
	{
		// Token: 0x04000001 RID: 1
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsFolderNotFound = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000002 RID: 2
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryNotFound = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000003 RID: 3
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryMissingBaseExperience = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000004 RID: 4
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryReDefinition = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000005 RID: 5
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryExpectedElement = new ExEventLog.EventTuple(3221225477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000006 RID: 6
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryExpectedAttribute = new ExEventLog.EventTuple(3221225478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000007 RID: 7
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidUserOfBaseExperience = new ExEventLog.EventTuple(3221225479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryExpectedBaseExperienceOrInheritsFrom = new ExEventLog.EventTuple(3221225480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryParseError = new ExEventLog.EventTuple(3221225481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidMinimumVersion = new ExEventLog.EventTuple(3221225482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidApplicationElement = new ExEventLog.EventTuple(3221225483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidClientControl = new ExEventLog.EventTuple(3221225484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryExpectedClientOrApplicationElement = new ExEventLog.EventTuple(3221225485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaStartedSuccessfully = new ExEventLog.EventTuple(14U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaAttachmentFileTypeInvalidCharacter = new ExEventLog.EventTuple(3221225487U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaConfigurationNotFound = new ExEventLog.EventTuple(3221225488U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoThemesFolder = new ExEventLog.EventTuple(3221225490U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoExpectedElement = new ExEventLog.EventTuple(3221225491U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoDuplicatedAttribute = new ExEventLog.EventTuple(3221225492U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoAttributeExceededMaximumLength = new ExEventLog.EventTuple(3221225494U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoEmptyAttribute = new ExEventLog.EventTuple(3221225493U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoMissingAttribute = new ExEventLog.EventTuple(3221225496U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ThemeInfoErrorParsingXml = new ExEventLog.EventTuple(3221225497U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoBaseTheme = new ExEventLog.EventTuple(3221225498U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidRequiredFeatures = new ExEventLog.EventTuple(3221225499U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmallIconsFileNotFound = new ExEventLog.EventTuple(3221225500U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebConfigAuthenticationIncorrect = new ExEventLog.EventTuple(3221225501U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_VDirAnonymous = new ExEventLog.EventTuple(3221225502U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomPropertiesRootElementNotFound = new ExEventLog.EventTuple(3221225503U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCustomPropertiesAttributeCount = new ExEventLog.EventTuple(3221225504U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001F RID: 31
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomPropertiesAttributeNotFound = new ExEventLog.EventTuple(3221225505U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidElementInCustomPropertiesFile = new ExEventLog.EventTuple(3221225506U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomPropertiesParseError = new ExEventLog.EventTuple(3221225507U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomPropertiesInvalidAttibuteValue = new ExEventLog.EventTuple(3221225508U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PrimarySmtpAddressUnavailable = new ExEventLog.EventTuple(3221225509U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorWrongUriFormat = new ExEventLog.EventTuple(3221225510U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorCASFailoverTryNextOne = new ExEventLog.EventTuple(3221225511U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorCASFailoverAllAttemptsFailed = new ExEventLog.EventTuple(3221225512U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorCouldNotFindCAS = new ExEventLog.EventTuple(3221225513U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorSslConnection = new ExEventLog.EventTuple(3221225514U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorSslTrustFailure = new ExEventLog.EventTuple(3221225515U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorTooManySidsInContext = new ExEventLog.EventTuple(3221225516U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorAccessCheck = new ExEventLog.EventTuple(3221225517U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorCASCompatibility = new ExEventLog.EventTuple(3221225518U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingCacheFolderCreationFailed = new ExEventLog.EventTuple(3221225519U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingWorkerApplicationNotRegistered = new ExEventLog.EventTuple(3221225520U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingWorkerApplicationNotFound = new ExEventLog.EventTuple(3221225521U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADNotificationsRegistration = new ExEventLog.EventTuple(3221225522U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADMessageClassificationRegistration = new ExEventLog.EventTuple(3221225523U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadSettingsFromAD = new ExEventLog.EventTuple(3221225524U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadMessageClassificationFromAD = new ExEventLog.EventTuple(3221225525U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ADSystemConfigurationSession = new ExEventLog.EventTuple(3221225526U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FormsRegistryInvalidUserOfIsRichClient = new ExEventLog.EventTuple(3221225527U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingManagerInitializationFailed = new ExEventLog.EventTuple(1073741880U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorServiceDiscovery = new ExEventLog.EventTuple(3221225529U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaRestartingAfterFailedLoad = new ExEventLog.EventTuple(58U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptCalendarConfiguration = new ExEventLog.EventTuple(3221225531U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SmallIconsAltReferenceInvalid = new ExEventLog.EventTuple(3221225532U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxServerVersionConfiguration = new ExEventLog.EventTuple(3221225533U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003C RID: 60
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingWorkerProcessFails = new ExEventLog.EventTuple(1073741886U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003D RID: 61
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationSettingsUpdated = new ExEventLog.EventTuple(63U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003E RID: 62
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateConfigurationSettings = new ExEventLog.EventTuple(3221225536U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GenericConfigurationUpdateError = new ExEventLog.EventTuple(3221225537U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnregisterADNotifications = new ExEventLog.EventTuple(1073741890U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnregisterADMessageClassification = new ExEventLog.EventTuple(1073741891U, 8, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingWorkerInitializationFailed = new ExEventLog.EventTuple(1073741892U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingCacheReachedQuota = new ExEventLog.EventTuple(1073741893U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000044 RID: 68
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingCacheFolderACLSettingAccessDenied = new ExEventLog.EventTuple(3221225542U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorAuthenticationToCas2Failure = new ExEventLog.EventTuple(3221225543U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomizationUIExtensionParseError = new ExEventLog.EventTuple(3221225544U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomizationFormsRegistryLoadSuccessfully = new ExEventLog.EventTuple(73U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PayloadNotBeingPickedup = new ExEventLog.EventTuple(3221225546U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToEstablishMTLSConnection = new ExEventLog.EventTuple(3221225547U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorSipEndpointTerminate = new ExEventLog.EventTuple(3221225548U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCollaborationManagerTerminate = new ExEventLog.EventTuple(3221225549U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToEstablishIMConnection = new ExEventLog.EventTuple(3221225550U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncompatibleTimeoutSetting = new ExEventLog.EventTuple(3221225551U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoCASFoundForInSiteMailbox = new ExEventLog.EventTuple(3221225552U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoCASFoundForCrossSiteMailboxToRedirect = new ExEventLog.EventTuple(3221225553U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorMSNSignOut = new ExEventLog.EventTuple(3221225554U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMProviderNoRegistrySetting = new ExEventLog.EventTuple(3221225555U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMProviderFileDoesNotExist = new ExEventLog.EventTuple(3221225556U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMProviderMultipleClasses = new ExEventLog.EventTuple(3221225557U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMProviderNoValidConstructor = new ExEventLog.EventTuple(3221225558U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMProviderExceptionDuringLoad = new ExEventLog.EventTuple(3221225559U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCreateEndpointFailure = new ExEventLog.EventTuple(3221225560U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoLegacyCasToRedirect = new ExEventLog.EventTuple(3221225562U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoSslE2003CasToRedirect = new ExEventLog.EventTuple(3221225563U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoSslE2007CasToRedirect = new ExEventLog.EventTuple(3221225564U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveHeaderConfigurationError = new ExEventLog.EventTuple(3221225565U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorMailboxServerTooBusy = new ExEventLog.EventTuple(3221225566U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingCacheFolderDeletingAccessDenied = new ExEventLog.EventTuple(1073741919U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TranscodingStartSuccessfully = new ExEventLog.EventTuple(96U, 7, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCreatingClientContext = new ExEventLog.EventTuple(3221225569U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyOWAReInitializationRequests = new ExEventLog.EventTuple(1073741922U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MessagingPayloadNotBeingPickedup = new ExEventLog.EventTuple(1073741923U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMServerNameInvalid = new ExEventLog.EventTuple(3221225572U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateThumbprintInvalid = new ExEventLog.EventTuple(3221225573U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateExpired = new ExEventLog.EventTuple(3221225574U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateNotFound = new ExEventLog.EventTuple(3221225575U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateInvalidDate = new ExEventLog.EventTuple(3221225576U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateNoPrivateKey = new ExEventLog.EventTuple(3221225577U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorIMCertificateWillExpireSoon = new ExEventLog.EventTuple(1073741930U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoThemeResources = new ExEventLog.EventTuple(3221225579U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaEwsConnectionError = new ExEventLog.EventTuple(3221225580U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006A RID: 106
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BposHeaderConfigurationError = new ExEventLog.EventTuple(3221225581U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MdbConcurrencySettingsInvalid = new ExEventLog.EventTuple(3221225582U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorResourceUnhealthy = new ExEventLog.EventTuple(3221225583U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IMEndpointManagerInitializedSuccessfully = new ExEventLog.EventTuple(112U, 9, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StorageTransientExceptionWarning = new ExEventLog.EventTuple(2147483761U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveMailboxAccessFailedWarning = new ExEventLog.EventTuple(1073741939U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidThemeFolder = new ExEventLog.EventTuple(2147483764U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSNProviderInitializationError = new ExEventLog.EventTuple(3221225589U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSNProviderInitializationSucceeded = new ExEventLog.EventTuple(118U, 9, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaStartPageInitializationError = new ExEventLog.EventTuple(3221225591U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaStartPageInitializationWarning = new ExEventLog.EventTuple(1073741944U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_O365SetUserThemeError = new ExEventLog.EventTuple(3221225593U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveAssetReaderInitResourceConsumerStarted = new ExEventLog.EventTuple(1073741955U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveAssetReaderInitResourceConsumerSucceeded = new ExEventLog.EventTuple(1073741956U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LiveAssetReaderInitResourceConsumerError = new ExEventLog.EventTuple(1073741957U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaConfigurationWebSiteUnavailable = new ExEventLog.EventTuple(3221225606U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNoSslE2010CasToRedirect = new ExEventLog.EventTuple(3221225607U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorNo2007OrAboveCasToRedirect = new ExEventLog.EventTuple(3221225608U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UserContextTerminationError = new ExEventLog.EventTuple(3221225609U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WacConfigurationSetupError = new ExEventLog.EventTuple(3221225611U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WacConfigurationSetupSuccessful = new ExEventLog.EventTuple(140U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WacDiscoveryDataRetrievalFailure = new ExEventLog.EventTuple(3221225613U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WacDiscoveryDataRetrievedSuccessfully = new ExEventLog.EventTuple(142U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsTemplateLoadFailure = new ExEventLog.EventTuple(3221225615U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000082 RID: 130
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaManifestInvalid = new ExEventLog.EventTuple(3221225616U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DfpOwaStartedSuccessfully = new ExEventLog.EventTuple(145U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000084 RID: 132
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OwaFailedToCreateExchangePrincipal = new ExEventLog.EventTuple(3221225618U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationCachePerfCountersLoadFailure = new ExEventLog.EventTuple(3221225619U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000086 RID: 134
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FblFailedToConnectToSmtpServer = new ExEventLog.EventTuple(3221225620U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000087 RID: 135
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FblSmtpServerResponse = new ExEventLog.EventTuple(3221225621U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000088 RID: 136
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FblErrorSendingMessage = new ExEventLog.EventTuple(3221225622U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000089 RID: 137
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientFblErrorUpdatingMServ = new ExEventLog.EventTuple(3221225623U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008A RID: 138
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentFblErrorUpdatingMServ = new ExEventLog.EventTuple(3221225624U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008B RID: 139
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientFblErrorReadingMServ = new ExEventLog.EventTuple(3221225625U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008C RID: 140
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentFblErrorReadingMServ = new ExEventLog.EventTuple(3221225626U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008D RID: 141
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FblUnableToProcessRequest = new ExEventLog.EventTuple(3221225627U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000003 RID: 3
		private enum Category : short
		{
			// Token: 0x0400008F RID: 143
			FormsRegistry = 1,
			// Token: 0x04000090 RID: 144
			Core,
			// Token: 0x04000091 RID: 145
			Configuration,
			// Token: 0x04000092 RID: 146
			Themes,
			// Token: 0x04000093 RID: 147
			SmallIcons,
			// Token: 0x04000094 RID: 148
			Proxy,
			// Token: 0x04000095 RID: 149
			Transcoding,
			// Token: 0x04000096 RID: 150
			ADNotifications,
			// Token: 0x04000097 RID: 151
			InstantMessage,
			// Token: 0x04000098 RID: 152
			Wac,
			// Token: 0x04000099 RID: 153
			EOP
		}

		// Token: 0x02000004 RID: 4
		internal enum Message : uint
		{
			// Token: 0x0400009B RID: 155
			FormsFolderNotFound = 3221225473U,
			// Token: 0x0400009C RID: 156
			FormsRegistryNotFound,
			// Token: 0x0400009D RID: 157
			FormsRegistryMissingBaseExperience,
			// Token: 0x0400009E RID: 158
			FormsRegistryReDefinition,
			// Token: 0x0400009F RID: 159
			FormsRegistryExpectedElement,
			// Token: 0x040000A0 RID: 160
			FormsRegistryExpectedAttribute,
			// Token: 0x040000A1 RID: 161
			FormsRegistryInvalidUserOfBaseExperience,
			// Token: 0x040000A2 RID: 162
			FormsRegistryExpectedBaseExperienceOrInheritsFrom,
			// Token: 0x040000A3 RID: 163
			FormsRegistryParseError,
			// Token: 0x040000A4 RID: 164
			FormsRegistryInvalidMinimumVersion,
			// Token: 0x040000A5 RID: 165
			FormsRegistryInvalidApplicationElement,
			// Token: 0x040000A6 RID: 166
			FormsRegistryInvalidClientControl,
			// Token: 0x040000A7 RID: 167
			FormsRegistryExpectedClientOrApplicationElement,
			// Token: 0x040000A8 RID: 168
			OwaStartedSuccessfully = 14U,
			// Token: 0x040000A9 RID: 169
			OwaAttachmentFileTypeInvalidCharacter = 3221225487U,
			// Token: 0x040000AA RID: 170
			OwaConfigurationNotFound,
			// Token: 0x040000AB RID: 171
			NoThemesFolder = 3221225490U,
			// Token: 0x040000AC RID: 172
			ThemeInfoExpectedElement,
			// Token: 0x040000AD RID: 173
			ThemeInfoDuplicatedAttribute,
			// Token: 0x040000AE RID: 174
			ThemeInfoAttributeExceededMaximumLength = 3221225494U,
			// Token: 0x040000AF RID: 175
			ThemeInfoEmptyAttribute = 3221225493U,
			// Token: 0x040000B0 RID: 176
			ThemeInfoMissingAttribute = 3221225496U,
			// Token: 0x040000B1 RID: 177
			ThemeInfoErrorParsingXml,
			// Token: 0x040000B2 RID: 178
			NoBaseTheme,
			// Token: 0x040000B3 RID: 179
			FormsRegistryInvalidRequiredFeatures,
			// Token: 0x040000B4 RID: 180
			SmallIconsFileNotFound,
			// Token: 0x040000B5 RID: 181
			WebConfigAuthenticationIncorrect,
			// Token: 0x040000B6 RID: 182
			VDirAnonymous,
			// Token: 0x040000B7 RID: 183
			CustomPropertiesRootElementNotFound,
			// Token: 0x040000B8 RID: 184
			InvalidCustomPropertiesAttributeCount,
			// Token: 0x040000B9 RID: 185
			CustomPropertiesAttributeNotFound,
			// Token: 0x040000BA RID: 186
			InvalidElementInCustomPropertiesFile,
			// Token: 0x040000BB RID: 187
			CustomPropertiesParseError,
			// Token: 0x040000BC RID: 188
			CustomPropertiesInvalidAttibuteValue,
			// Token: 0x040000BD RID: 189
			PrimarySmtpAddressUnavailable,
			// Token: 0x040000BE RID: 190
			ProxyErrorWrongUriFormat,
			// Token: 0x040000BF RID: 191
			ProxyErrorCASFailoverTryNextOne,
			// Token: 0x040000C0 RID: 192
			ProxyErrorCASFailoverAllAttemptsFailed,
			// Token: 0x040000C1 RID: 193
			ProxyErrorCouldNotFindCAS,
			// Token: 0x040000C2 RID: 194
			ProxyErrorSslConnection,
			// Token: 0x040000C3 RID: 195
			ProxyErrorSslTrustFailure,
			// Token: 0x040000C4 RID: 196
			ProxyErrorTooManySidsInContext,
			// Token: 0x040000C5 RID: 197
			ProxyErrorAccessCheck,
			// Token: 0x040000C6 RID: 198
			ProxyErrorCASCompatibility,
			// Token: 0x040000C7 RID: 199
			TranscodingCacheFolderCreationFailed,
			// Token: 0x040000C8 RID: 200
			TranscodingWorkerApplicationNotRegistered,
			// Token: 0x040000C9 RID: 201
			TranscodingWorkerApplicationNotFound,
			// Token: 0x040000CA RID: 202
			ADNotificationsRegistration,
			// Token: 0x040000CB RID: 203
			ADMessageClassificationRegistration,
			// Token: 0x040000CC RID: 204
			ReadSettingsFromAD,
			// Token: 0x040000CD RID: 205
			ReadMessageClassificationFromAD,
			// Token: 0x040000CE RID: 206
			ADSystemConfigurationSession,
			// Token: 0x040000CF RID: 207
			FormsRegistryInvalidUserOfIsRichClient,
			// Token: 0x040000D0 RID: 208
			TranscodingManagerInitializationFailed = 1073741880U,
			// Token: 0x040000D1 RID: 209
			ProxyErrorServiceDiscovery = 3221225529U,
			// Token: 0x040000D2 RID: 210
			OwaRestartingAfterFailedLoad = 58U,
			// Token: 0x040000D3 RID: 211
			CorruptCalendarConfiguration = 3221225531U,
			// Token: 0x040000D4 RID: 212
			SmallIconsAltReferenceInvalid,
			// Token: 0x040000D5 RID: 213
			MailboxServerVersionConfiguration,
			// Token: 0x040000D6 RID: 214
			TranscodingWorkerProcessFails = 1073741886U,
			// Token: 0x040000D7 RID: 215
			ConfigurationSettingsUpdated = 63U,
			// Token: 0x040000D8 RID: 216
			FailedToUpdateConfigurationSettings = 3221225536U,
			// Token: 0x040000D9 RID: 217
			GenericConfigurationUpdateError,
			// Token: 0x040000DA RID: 218
			UnregisterADNotifications = 1073741890U,
			// Token: 0x040000DB RID: 219
			UnregisterADMessageClassification,
			// Token: 0x040000DC RID: 220
			TranscodingWorkerInitializationFailed,
			// Token: 0x040000DD RID: 221
			TranscodingCacheReachedQuota,
			// Token: 0x040000DE RID: 222
			TranscodingCacheFolderACLSettingAccessDenied = 3221225542U,
			// Token: 0x040000DF RID: 223
			ProxyErrorAuthenticationToCas2Failure,
			// Token: 0x040000E0 RID: 224
			CustomizationUIExtensionParseError,
			// Token: 0x040000E1 RID: 225
			CustomizationFormsRegistryLoadSuccessfully = 73U,
			// Token: 0x040000E2 RID: 226
			PayloadNotBeingPickedup = 3221225546U,
			// Token: 0x040000E3 RID: 227
			FailedToEstablishMTLSConnection,
			// Token: 0x040000E4 RID: 228
			ErrorSipEndpointTerminate,
			// Token: 0x040000E5 RID: 229
			ErrorCollaborationManagerTerminate,
			// Token: 0x040000E6 RID: 230
			FailedToEstablishIMConnection,
			// Token: 0x040000E7 RID: 231
			IncompatibleTimeoutSetting,
			// Token: 0x040000E8 RID: 232
			ProxyErrorNoCASFoundForInSiteMailbox,
			// Token: 0x040000E9 RID: 233
			ProxyErrorNoCASFoundForCrossSiteMailboxToRedirect,
			// Token: 0x040000EA RID: 234
			ErrorMSNSignOut,
			// Token: 0x040000EB RID: 235
			ErrorIMProviderNoRegistrySetting,
			// Token: 0x040000EC RID: 236
			ErrorIMProviderFileDoesNotExist,
			// Token: 0x040000ED RID: 237
			ErrorIMProviderMultipleClasses,
			// Token: 0x040000EE RID: 238
			ErrorIMProviderNoValidConstructor,
			// Token: 0x040000EF RID: 239
			ErrorIMProviderExceptionDuringLoad,
			// Token: 0x040000F0 RID: 240
			ErrorIMCreateEndpointFailure,
			// Token: 0x040000F1 RID: 241
			ProxyErrorNoLegacyCasToRedirect = 3221225562U,
			// Token: 0x040000F2 RID: 242
			ProxyErrorNoSslE2003CasToRedirect,
			// Token: 0x040000F3 RID: 243
			ProxyErrorNoSslE2007CasToRedirect,
			// Token: 0x040000F4 RID: 244
			LiveHeaderConfigurationError,
			// Token: 0x040000F5 RID: 245
			ErrorMailboxServerTooBusy,
			// Token: 0x040000F6 RID: 246
			TranscodingCacheFolderDeletingAccessDenied = 1073741919U,
			// Token: 0x040000F7 RID: 247
			TranscodingStartSuccessfully = 96U,
			// Token: 0x040000F8 RID: 248
			ErrorCreatingClientContext = 3221225569U,
			// Token: 0x040000F9 RID: 249
			TooManyOWAReInitializationRequests = 1073741922U,
			// Token: 0x040000FA RID: 250
			MessagingPayloadNotBeingPickedup,
			// Token: 0x040000FB RID: 251
			ErrorIMServerNameInvalid = 3221225572U,
			// Token: 0x040000FC RID: 252
			ErrorIMCertificateThumbprintInvalid,
			// Token: 0x040000FD RID: 253
			ErrorIMCertificateExpired,
			// Token: 0x040000FE RID: 254
			ErrorIMCertificateNotFound,
			// Token: 0x040000FF RID: 255
			ErrorIMCertificateInvalidDate,
			// Token: 0x04000100 RID: 256
			ErrorIMCertificateNoPrivateKey,
			// Token: 0x04000101 RID: 257
			ErrorIMCertificateWillExpireSoon = 1073741930U,
			// Token: 0x04000102 RID: 258
			NoThemeResources = 3221225579U,
			// Token: 0x04000103 RID: 259
			OwaEwsConnectionError,
			// Token: 0x04000104 RID: 260
			BposHeaderConfigurationError,
			// Token: 0x04000105 RID: 261
			MdbConcurrencySettingsInvalid,
			// Token: 0x04000106 RID: 262
			ErrorResourceUnhealthy,
			// Token: 0x04000107 RID: 263
			IMEndpointManagerInitializedSuccessfully = 112U,
			// Token: 0x04000108 RID: 264
			StorageTransientExceptionWarning = 2147483761U,
			// Token: 0x04000109 RID: 265
			ArchiveMailboxAccessFailedWarning = 1073741939U,
			// Token: 0x0400010A RID: 266
			InvalidThemeFolder = 2147483764U,
			// Token: 0x0400010B RID: 267
			MSNProviderInitializationError = 3221225589U,
			// Token: 0x0400010C RID: 268
			MSNProviderInitializationSucceeded = 118U,
			// Token: 0x0400010D RID: 269
			OwaStartPageInitializationError = 3221225591U,
			// Token: 0x0400010E RID: 270
			OwaStartPageInitializationWarning = 1073741944U,
			// Token: 0x0400010F RID: 271
			O365SetUserThemeError = 3221225593U,
			// Token: 0x04000110 RID: 272
			LiveAssetReaderInitResourceConsumerStarted = 1073741955U,
			// Token: 0x04000111 RID: 273
			LiveAssetReaderInitResourceConsumerSucceeded,
			// Token: 0x04000112 RID: 274
			LiveAssetReaderInitResourceConsumerError,
			// Token: 0x04000113 RID: 275
			OwaConfigurationWebSiteUnavailable = 3221225606U,
			// Token: 0x04000114 RID: 276
			ProxyErrorNoSslE2010CasToRedirect,
			// Token: 0x04000115 RID: 277
			ProxyErrorNo2007OrAboveCasToRedirect,
			// Token: 0x04000116 RID: 278
			UserContextTerminationError,
			// Token: 0x04000117 RID: 279
			WacConfigurationSetupError = 3221225611U,
			// Token: 0x04000118 RID: 280
			WacConfigurationSetupSuccessful = 140U,
			// Token: 0x04000119 RID: 281
			WacDiscoveryDataRetrievalFailure = 3221225613U,
			// Token: 0x0400011A RID: 282
			WacDiscoveryDataRetrievedSuccessfully = 142U,
			// Token: 0x0400011B RID: 283
			RmsTemplateLoadFailure = 3221225615U,
			// Token: 0x0400011C RID: 284
			OwaManifestInvalid,
			// Token: 0x0400011D RID: 285
			DfpOwaStartedSuccessfully = 145U,
			// Token: 0x0400011E RID: 286
			OwaFailedToCreateExchangePrincipal = 3221225618U,
			// Token: 0x0400011F RID: 287
			ConfigurationCachePerfCountersLoadFailure,
			// Token: 0x04000120 RID: 288
			FblFailedToConnectToSmtpServer,
			// Token: 0x04000121 RID: 289
			FblSmtpServerResponse,
			// Token: 0x04000122 RID: 290
			FblErrorSendingMessage,
			// Token: 0x04000123 RID: 291
			TransientFblErrorUpdatingMServ,
			// Token: 0x04000124 RID: 292
			PermanentFblErrorUpdatingMServ,
			// Token: 0x04000125 RID: 293
			TransientFblErrorReadingMServ,
			// Token: 0x04000126 RID: 294
			PermanentFblErrorReadingMServ,
			// Token: 0x04000127 RID: 295
			FblUnableToProcessRequest
		}
	}
}
