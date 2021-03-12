using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RequestDetailsLogger : RequestDetailsLoggerBase<RequestDetailsLogger>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002ACA File Offset: 0x00000CCA
		protected override void InitializeLogger()
		{
			ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
			ActivityContext.RegisterMetadata(typeof(RoutingUpdateModuleMetadata));
			base.InitializeLogger();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AF0 File Offset: 0x00000CF0
		protected override RequestLoggerConfig GetRequestLoggerConfig()
		{
			List<KeyValuePair<string, Enum>> columns = new List<KeyValuePair<string, Enum>>
			{
				new KeyValuePair<string, Enum>("DateTime", ActivityReadonlyMetadata.EndTime),
				new KeyValuePair<string, Enum>("RequestId", ActivityReadonlyMetadata.ActivityId),
				new KeyValuePair<string, Enum>("MajorVersion", ServiceCommonMetadata.ServerVersionMajor),
				new KeyValuePair<string, Enum>("MinorVersion", ServiceCommonMetadata.ServerVersionMinor),
				new KeyValuePair<string, Enum>("BuildVersion", ServiceCommonMetadata.ServerVersionBuild),
				new KeyValuePair<string, Enum>("RevisionVersion", ServiceCommonMetadata.ServerVersionRevision),
				new KeyValuePair<string, Enum>("Protocol", RoutingUpdateModuleMetadata.Protocol),
				new KeyValuePair<string, Enum>("ServerLocatorLatency", RoutingUpdateModuleMetadata.ServerLocatorLatency),
				new KeyValuePair<string, Enum>("GlsLatencyBreakup", RoutingUpdateModuleMetadata.GlsLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalGlsLatency", RoutingUpdateModuleMetadata.TotalGlsLatency),
				new KeyValuePair<string, Enum>("AccountForestLatencyBreakup", RoutingUpdateModuleMetadata.AccountForestLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalAccountForestLatency", RoutingUpdateModuleMetadata.TotalAccountForestLatency),
				new KeyValuePair<string, Enum>("ResourceForestLatencyBreakup", RoutingUpdateModuleMetadata.ResourceForestLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalResourceForestLatency", RoutingUpdateModuleMetadata.TotalResourceForestLatency),
				new KeyValuePair<string, Enum>("ActiveManagerLatencyBreakup", RoutingUpdateModuleMetadata.ActiveManagerLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalActiveManagerLatency", RoutingUpdateModuleMetadata.TotalActiveManagerLatency),
				new KeyValuePair<string, Enum>("GenericInfo", ServiceCommonMetadata.GenericInfo),
				new KeyValuePair<string, Enum>("GenericErrors", ServiceCommonMetadata.GenericErrors)
			};
			return new RequestLoggerConfig("Routing Update Module Protocol Logs", "RoutingUpdateModule_", "RoutingUpdateModuleProtocolLogs", "RequestDetailsLogger.RoutingUpdateModuleProtocolLogFolder", RequestDetailsLogger.DefaultLogFolderPath, TimeSpan.FromDays((double)RequestDetailsLogger.MaxLogRetentionInDays.Value), (long)(RequestDetailsLogger.MaxLogDirectorySizeInGB.Value * 1024 * 1024 * 1024), (long)(RequestDetailsLogger.MaxLogFileSizeInMB.Value * 1024 * 1024), ServiceCommonMetadata.GenericInfo, columns, Enum.GetValues(typeof(ServiceLatencyMetadata)).Length);
		}

		// Token: 0x0400001A RID: 26
		private const string LogType = "Routing Update Module Protocol Logs";

		// Token: 0x0400001B RID: 27
		private const string LogFilePrefix = "RoutingUpdateModule_";

		// Token: 0x0400001C RID: 28
		private const string LogComponent = "RoutingUpdateModuleProtocolLogs";

		// Token: 0x0400001D RID: 29
		private const string CustomLogFolderPathAppSettingsKey = "RequestDetailsLogger.RoutingUpdateModuleProtocolLogFolder";

		// Token: 0x0400001E RID: 30
		internal static readonly StringAppSettingsEntry ProtocolType = new StringAppSettingsEntry("RoutingUpdateModule.ProtocolType", string.Empty, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x0400001F RID: 31
		private static readonly IntAppSettingsEntry MaxLogRetentionInDays = new IntAppSettingsEntry("RoutingUpdateModule.MaxLogRetentionInDays", 30, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x04000020 RID: 32
		private static readonly IntAppSettingsEntry MaxLogDirectorySizeInGB = new IntAppSettingsEntry("RoutingUpdateModule.MaxLogDirectorySizeInGB", 5, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x04000021 RID: 33
		private static readonly IntAppSettingsEntry MaxLogFileSizeInMB = new IntAppSettingsEntry("RoutingUpdateModule.MaxLogFileSizeInMB", 10, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x04000022 RID: 34
		private static readonly string DefaultLogFolderPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\RoutingUpdateModule\\" + RequestDetailsLogger.ProtocolType.Value);
	}
}
