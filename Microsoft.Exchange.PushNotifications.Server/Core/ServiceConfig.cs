using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x0200000A RID: 10
	internal static class ServiceConfig
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029B0 File Offset: 0x00000BB0
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000029B7 File Offset: 0x00000BB7
		public static bool IsWriteStackTraceOnResponseEnabled { get; private set; } = AppConfigLoader.GetConfigBoolValue("WriteStackTraceOnResponse", false);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000029C6 File Offset: 0x00000BC6
		public static int ConfigurationRefreshRateInMinutes { get; private set; } = AppConfigLoader.GetConfigIntValue("ConfigurationRefreshRateInMinutes", 0, int.MaxValue, 15);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000029CE File Offset: 0x00000BCE
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000029D5 File Offset: 0x00000BD5
		public static bool IgnoreCertificateErrors { get; private set; } = false;

		// Token: 0x04000024 RID: 36
		public const string WriteStackTraceOnResponseKey = "WriteStackTraceOnResponse";

		// Token: 0x04000025 RID: 37
		public const string ConfigurationRefreshRateInMinutesKey = "ConfigurationRefreshRateInMinutes";

		// Token: 0x04000026 RID: 38
		public const string IgnoreCertificateErrorsKey = "IgnoreCertificateErrors";

		// Token: 0x04000027 RID: 39
		public const string UseDebugTenantIdKey = "UseDebugTenantId";

		// Token: 0x04000028 RID: 40
		public const bool WriteStackTraceOnResponseDefaultValue = false;

		// Token: 0x04000029 RID: 41
		public const int ConfigurationRefreshRateInMinutesDefaultValue = 15;

		// Token: 0x0400002A RID: 42
		public const bool IgnoreCertificateErrorsDefaultValue = false;
	}
}
