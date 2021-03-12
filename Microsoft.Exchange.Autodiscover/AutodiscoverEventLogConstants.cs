using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x0200000A RID: 10
	internal static class AutodiscoverEventLogConstants
	{
		// Token: 0x0400003F RID: 63
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrWebException = new ExEventLog.EventTuple(3221225473U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000040 RID: 64
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrWebAnonymousRequest = new ExEventLog.EventTuple(3221225474U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000041 RID: 65
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrWebBasicAuthRequest = new ExEventLog.EventTuple(3221225475U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000042 RID: 66
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrCoreNoProvidersFound = new ExEventLog.EventTuple(3221225573U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000043 RID: 67
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrCoreInvalidRedirectionUrl = new ExEventLog.EventTuple(3221225574U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000044 RID: 68
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrProviderRedirectServerCertificate = new ExEventLog.EventTuple(3221225673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000045 RID: 69
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrProviderFormatException = new ExEventLog.EventTuple(3221225674U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000046 RID: 70
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrProviderRegistryMisconfiguration = new ExEventLog.EventTuple(3221225675U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000047 RID: 71
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrProviderOabMisconfiguration = new ExEventLog.EventTuple(3221225676U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000048 RID: 72
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrProviderOabNotExist = new ExEventLog.EventTuple(3221225677U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000049 RID: 73
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreElementIsEmpty = new ExEventLog.EventTuple(2147484749U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004A RID: 74
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreElementsAreEmpty = new ExEventLog.EventTuple(2147484750U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004B RID: 75
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreValidationError = new ExEventLog.EventTuple(2147484751U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004C RID: 76
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreValidationException = new ExEventLog.EventTuple(2147484752U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004D RID: 77
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderNotFound = new ExEventLog.EventTuple(2147484753U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004E RID: 78
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderLoadError = new ExEventLog.EventTuple(2147484754U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400004F RID: 79
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderFileLoadException = new ExEventLog.EventTuple(2147484756U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderReflectionTypeLoadException = new ExEventLog.EventTuple(2147484757U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderBadImageFormatException = new ExEventLog.EventTuple(2147484758U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderSecurityException = new ExEventLog.EventTuple(2147484759U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderFileNotFoundException = new ExEventLog.EventTuple(2147484760U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCoreProviderAttributeException = new ExEventLog.EventTuple(2147484761U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCorePerfCounterInitializationFailed = new ExEventLog.EventTuple(2147484762U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnCorePerfCounterIncrementFailed = new ExEventLog.EventTuple(2147484763U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarnProvErrorResponse = new ExEventLog.EventTuple(2147484849U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoWebApplicationStart = new ExEventLog.EventTuple(2001U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoWebApplicationStop = new ExEventLog.EventTuple(2002U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoWebSessionStart = new ExEventLog.EventTuple(2003U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoWebSessionSuccess = new ExEventLog.EventTuple(2004U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoWebSessionFailure = new ExEventLog.EventTuple(2005U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoCoreProvidersLoaded = new ExEventLog.EventTuple(2101U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoProvRedirectionResponse = new ExEventLog.EventTuple(2201U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoProvConfigurationResponse = new ExEventLog.EventTuple(2202U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StreamInsightsDataUploadFailed = new ExEventLog.EventTuple(2147485851U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoProvRedirectBypassConfigurationResponse = new ExEventLog.EventTuple(2204U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000B RID: 11
		private enum Category : short
		{
			// Token: 0x04000063 RID: 99
			Core = 1,
			// Token: 0x04000064 RID: 100
			Web,
			// Token: 0x04000065 RID: 101
			Provider
		}

		// Token: 0x0200000C RID: 12
		internal enum Message : uint
		{
			// Token: 0x04000067 RID: 103
			ErrWebException = 3221225473U,
			// Token: 0x04000068 RID: 104
			ErrWebAnonymousRequest,
			// Token: 0x04000069 RID: 105
			ErrWebBasicAuthRequest,
			// Token: 0x0400006A RID: 106
			ErrCoreNoProvidersFound = 3221225573U,
			// Token: 0x0400006B RID: 107
			ErrCoreInvalidRedirectionUrl,
			// Token: 0x0400006C RID: 108
			ErrProviderRedirectServerCertificate = 3221225673U,
			// Token: 0x0400006D RID: 109
			ErrProviderFormatException,
			// Token: 0x0400006E RID: 110
			ErrProviderRegistryMisconfiguration,
			// Token: 0x0400006F RID: 111
			ErrProviderOabMisconfiguration,
			// Token: 0x04000070 RID: 112
			ErrProviderOabNotExist,
			// Token: 0x04000071 RID: 113
			WarnCoreElementIsEmpty = 2147484749U,
			// Token: 0x04000072 RID: 114
			WarnCoreElementsAreEmpty,
			// Token: 0x04000073 RID: 115
			WarnCoreValidationError,
			// Token: 0x04000074 RID: 116
			WarnCoreValidationException,
			// Token: 0x04000075 RID: 117
			WarnCoreProviderNotFound,
			// Token: 0x04000076 RID: 118
			WarnCoreProviderLoadError,
			// Token: 0x04000077 RID: 119
			WarnCoreProviderFileLoadException = 2147484756U,
			// Token: 0x04000078 RID: 120
			WarnCoreProviderReflectionTypeLoadException,
			// Token: 0x04000079 RID: 121
			WarnCoreProviderBadImageFormatException,
			// Token: 0x0400007A RID: 122
			WarnCoreProviderSecurityException,
			// Token: 0x0400007B RID: 123
			WarnCoreProviderFileNotFoundException,
			// Token: 0x0400007C RID: 124
			WarnCoreProviderAttributeException,
			// Token: 0x0400007D RID: 125
			WarnCorePerfCounterInitializationFailed,
			// Token: 0x0400007E RID: 126
			WarnCorePerfCounterIncrementFailed,
			// Token: 0x0400007F RID: 127
			WarnProvErrorResponse = 2147484849U,
			// Token: 0x04000080 RID: 128
			InfoWebApplicationStart = 2001U,
			// Token: 0x04000081 RID: 129
			InfoWebApplicationStop,
			// Token: 0x04000082 RID: 130
			InfoWebSessionStart,
			// Token: 0x04000083 RID: 131
			InfoWebSessionSuccess,
			// Token: 0x04000084 RID: 132
			InfoWebSessionFailure,
			// Token: 0x04000085 RID: 133
			InfoCoreProvidersLoaded = 2101U,
			// Token: 0x04000086 RID: 134
			InfoProvRedirectionResponse = 2201U,
			// Token: 0x04000087 RID: 135
			InfoProvConfigurationResponse,
			// Token: 0x04000088 RID: 136
			StreamInsightsDataUploadFailed = 2147485851U,
			// Token: 0x04000089 RID: 137
			InfoProvRedirectBypassConfigurationResponse = 2204U
		}
	}
}
