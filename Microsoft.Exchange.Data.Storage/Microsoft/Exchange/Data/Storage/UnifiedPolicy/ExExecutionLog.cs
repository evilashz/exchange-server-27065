using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E81 RID: 3713
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExExecutionLog : ExecutionLog
	{
		// Token: 0x060080AF RID: 32943 RVA: 0x00233190 File Offset: 0x00231390
		private ExExecutionLog(string path)
		{
			string[] array = new string[ExExecutionLog.CommonFields.Length + CustomDataLogger.CustomFields.Length];
			Array.Copy(ExExecutionLog.CommonFields, array, ExExecutionLog.CommonFields.Length);
			Array.Copy(CustomDataLogger.CustomFields, 0, array, ExExecutionLog.CommonFields.Length, CustomDataLogger.CustomFields.Length);
			this.logSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Unified Policy Log", array);
			this.logInstance = new Log(ExExecutionLog.GetLogFileName(), new LogHeaderFormatter(this.logSchema), "UnifiedPolicyLog");
			this.logInstance.Configure(Path.Combine(ExchangeSetupContext.InstallPath, path), ExExecutionLog.LogMaxAge, 262144000L, 10485760L);
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x0023325A File Offset: 0x0023145A
		public static ExExecutionLog CreateForServicelet()
		{
			return new ExExecutionLog("Logging\\UnifiedPolicy\\SyncAgent\\");
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x00233266 File Offset: 0x00231466
		public static ExExecutionLog CreateForCmdlet()
		{
			return new ExExecutionLog("Logging\\UnifiedPolicy\\Cmdlet\\");
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x00233274 File Offset: 0x00231474
		public override void LogInformation(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			this.LogOneEntry(client, tenantId, correlationId, ExecutionLog.EventType.Information, string.Empty, contextData, null, customData);
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x00233298 File Offset: 0x00231498
		public override void LogVerbose(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			this.LogOneEntry(client, tenantId, correlationId, ExecutionLog.EventType.Verbose, string.Empty, contextData, null, customData);
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x002332BC File Offset: 0x002314BC
		public override void LogWarnining(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			this.LogOneEntry(client, tenantId, correlationId, ExecutionLog.EventType.Warning, string.Empty, contextData, null, customData);
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x002332E0 File Offset: 0x002314E0
		public override void LogError(string client, string tenantId, string correlationId, Exception exception, string contextData, params KeyValuePair<string, object>[] customData)
		{
			this.LogOneEntry(client, tenantId, correlationId, ExecutionLog.EventType.Error, string.Empty, contextData, exception, customData);
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x00233304 File Offset: 0x00231504
		public override void LogOneEntry(string client, string correlationId, ExecutionLog.EventType eventType, string contextData, Exception exception)
		{
			this.LogOneEntry(client, null, correlationId, eventType, string.Empty, contextData, exception, new KeyValuePair<string, object>[0]);
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x0023332C File Offset: 0x0023152C
		public override void LogOneEntry(string client, string tenantId, string correlationId, ExecutionLog.EventType eventType, string tag, string contextData, Exception exception, params KeyValuePair<string, object>[] customData)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			Stream stream = null;
			logRowFormatter[1] = client;
			logRowFormatter[2] = tenantId;
			logRowFormatter[3] = correlationId;
			logRowFormatter[4] = eventType;
			logRowFormatter[5] = tag;
			logRowFormatter[6] = contextData;
			CustomDataLogger.Log(customData, logRowFormatter, out stream);
			if (exception != null)
			{
				List<string> list = null;
				List<string> list2 = null;
				string value = null;
				ExecutionLog.GetExceptionTypeAndDetails(exception, out list, out list2, out value, false);
				logRowFormatter[7] = list[0];
				logRowFormatter[8] = list2[0];
				if (list.Count > 1)
				{
					logRowFormatter[9] = list[list.Count - 1];
					logRowFormatter[10] = list2[list2.Count - 1];
				}
				if (!ExExecutionLog.ShouldSkipExceptionChainLogging(list))
				{
					logRowFormatter[11] = value;
				}
				logRowFormatter[12] = exception.GetHashCode().ToString();
			}
			this.logInstance.Append(logRowFormatter, 0);
			if (stream != null)
			{
				try
				{
					logRowFormatter.Write(stream);
				}
				catch (StorageTransientException)
				{
				}
				catch (StoragePermanentException)
				{
				}
			}
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x0023345C File Offset: 0x0023165C
		private static string GetLogFileName()
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
				{
					currentProcess.ProcessName,
					"UnifiedPolicyLog"
				});
			}
			return result;
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x002334B8 File Offset: 0x002316B8
		private static bool ShouldSkipExceptionChainLogging(List<string> types)
		{
			if (types == null || types.Count == 0)
			{
				return true;
			}
			foreach (string text in types)
			{
				if (text.IndexOf(typeof(TenantAccessBlockedException).FullName, StringComparison.OrdinalIgnoreCase) != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040056D4 RID: 22228
		private const string DefaultLogPathForServicelet = "Logging\\UnifiedPolicy\\SyncAgent\\";

		// Token: 0x040056D5 RID: 22229
		private const string DefaultLogPathForCmdlet = "Logging\\UnifiedPolicy\\Cmdlet\\";

		// Token: 0x040056D6 RID: 22230
		private const string LogType = "Unified Policy Log";

		// Token: 0x040056D7 RID: 22231
		private const string LogComponent = "UnifiedPolicyLog";

		// Token: 0x040056D8 RID: 22232
		private const string LogSuffix = "UnifiedPolicyLog";

		// Token: 0x040056D9 RID: 22233
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x040056DA RID: 22234
		private const int MaxLogFileSize = 10485760;

		// Token: 0x040056DB RID: 22235
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x040056DC RID: 22236
		private static readonly string[] CommonFields = (from ExExecutionLog.CommonField x in Enum.GetValues(typeof(ExExecutionLog.CommonField))
		select x.ToString()).ToArray<string>();

		// Token: 0x040056DD RID: 22237
		private LogSchema logSchema;

		// Token: 0x040056DE RID: 22238
		private Log logInstance;

		// Token: 0x02000E82 RID: 3714
		private enum CommonField
		{
			// Token: 0x040056E1 RID: 22241
			Time,
			// Token: 0x040056E2 RID: 22242
			Client,
			// Token: 0x040056E3 RID: 22243
			TenantId,
			// Token: 0x040056E4 RID: 22244
			CorrelationId,
			// Token: 0x040056E5 RID: 22245
			EventType,
			// Token: 0x040056E6 RID: 22246
			Tag,
			// Token: 0x040056E7 RID: 22247
			ContextData,
			// Token: 0x040056E8 RID: 22248
			OuterExceptionType,
			// Token: 0x040056E9 RID: 22249
			OuterExceptionMessage,
			// Token: 0x040056EA RID: 22250
			InnerExceptionType,
			// Token: 0x040056EB RID: 22251
			InnerExceptionMessage,
			// Token: 0x040056EC RID: 22252
			ExceptionChain,
			// Token: 0x040056ED RID: 22253
			ExceptionTag
		}
	}
}
