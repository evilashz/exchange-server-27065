using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000003 RID: 3
	internal class DiagnosticsConfiguration
	{
		// Token: 0x04000006 RID: 6
		internal static readonly StringAppSettingsEntry LogFolderPath = new StringAppSettingsEntry("RequestLogger.LogFolderPath", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\HttpProxy", HttpProxyGlobals.ProtocolType.ToString()), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000007 RID: 7
		internal static readonly StringAppSettingsEntry LogType = new StringAppSettingsEntry("RequestLogger.LogType", "HttpProxy Logs", ExTraceGlobals.VerboseTracer);

		// Token: 0x04000008 RID: 8
		internal static readonly StringAppSettingsEntry LogFileNamePrefix = new StringAppSettingsEntry("RequestLogger.LogFileNamePrefix", "HttpProxy2_", ExTraceGlobals.VerboseTracer);

		// Token: 0x04000009 RID: 9
		internal static readonly IntAppSettingsEntry MaxLogRetentionInDays = new IntAppSettingsEntry("RequestLogger.MaxLogRetentionInDays", 30, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400000A RID: 10
		internal static readonly IntAppSettingsEntry MaxLogDirectorySizeInGB = new IntAppSettingsEntry("RequestLogger.MaxLogDirectorySizeInGB", 1, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400000B RID: 11
		internal static readonly IntAppSettingsEntry MaxLogFileSizeInMB = new IntAppSettingsEntry("RequestLogger.MaxLogFileSizeInMB", 10, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400000C RID: 12
		internal static readonly BoolAppSettingsEntry AddDebugHeadersToTheResponse = new BoolAppSettingsEntry("RequestLogger.AddDebugHeadersToTheResponse", true, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400000D RID: 13
		internal static readonly BoolAppSettingsEntry DetailedLatencyTracingEnabled = new BoolAppSettingsEntry("LatencyTracker.DetailedLatencyTracing", false, ExTraceGlobals.VerboseTracer);
	}
}
