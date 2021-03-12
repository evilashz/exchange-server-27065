using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000017 RID: 23
	internal sealed class DCAdminActionsLogger : ActivityContextLogger
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005292 File Offset: 0x00003492
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AdminAuditLogTracer;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005299 File Offset: 0x00003499
		internal static DCAdminActionsLogger Instance
		{
			get
			{
				if (DCAdminActionsLogger.instance == null)
				{
					DCAdminActionsLogger.instance = new DCAdminActionsLogger();
				}
				return DCAdminActionsLogger.instance;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000052B1 File Offset: 0x000034B1
		private DCAdminActionsLogger()
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000052B9 File Offset: 0x000034B9
		protected override string LogComponentName
		{
			get
			{
				return "DCAdminActions";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000052C0 File Offset: 0x000034C0
		protected override string LogTypeName
		{
			get
			{
				return "Admin Audit Logs for DC Admin Actions";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000052C7 File Offset: 0x000034C7
		protected override string FileNamePrefix
		{
			get
			{
				return "DCAdminActions";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000052CE File Offset: 0x000034CE
		protected override int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000052D1 File Offset: 0x000034D1
		public static void Start()
		{
			if (DCAdminActionsLogger.instance == null)
			{
				DCAdminActionsLogger.instance = new DCAdminActionsLogger();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000052E4 File Offset: 0x000034E4
		public static void Stop()
		{
			if (DCAdminActionsLogger.instance != null)
			{
				DCAdminActionsLogger.instance.Dispose();
				DCAdminActionsLogger.instance = null;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000052FD File Offset: 0x000034FD
		public static void Flush()
		{
			if (DCAdminActionsLogger.instance != null)
			{
				DCAdminActionsLogger.instance.FlushLog();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005310 File Offset: 0x00003510
		internal static void LogDCAdminAction(Guid activityId, List<KeyValuePair<string, object>> customData)
		{
			DCAdminActionsLogger.InternalLogRow(activityId, customData);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000531C File Offset: 0x0000351C
		protected override void InternalLogActivityEvent(IActivityScope scope, ActivityEventType eventType)
		{
			List<KeyValuePair<string, object>> customData = null;
			switch (eventType)
			{
			case ActivityEventType.StartActivity:
			case ActivityEventType.ResumeActivity:
				break;
			case ActivityEventType.SuspendActivity:
			case ActivityEventType.EndActivity:
				customData = WorkloadManagementLogger.FormatWlmActivity(scope, true);
				break;
			default:
				DCAdminActionsLogger.instance.SafeTraceDebug(0L, "Skip logging ActivityEvent '{0}'.", new object[]
				{
					eventType
				});
				return;
			}
			DCAdminActionsLogger.InternalLogRow(scope.ActivityId, customData);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000537E File Offset: 0x0000357E
		protected override string[] GetLogFields()
		{
			return Enum.GetNames(typeof(DCAdminActionsLogFields));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000538F File Offset: 0x0000358F
		protected override ActivityContextLogFileSettings GetLogFileSettings()
		{
			return DCAdminActionsLoggerSettings.Load();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005398 File Offset: 0x00003598
		private static void InternalLogRow(Guid activityId, List<KeyValuePair<string, object>> customData)
		{
			if (DCAdminActionsLogger.instance == null)
			{
				return;
			}
			if (!DCAdminActionsLogger.instance.Enabled)
			{
				DCAdminActionsLogger.instance.SafeTraceDebug(0L, "DCAdminAtionsLogger log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(DCAdminActionsLogger.instance.LogSchema);
			if (DCAdminActionsLogger.instance.IsDebugTraceEnabled)
			{
				string text = string.Empty;
				if (customData != null)
				{
					bool flag;
					text = LogRowFormatter.FormatCollection(customData, out flag);
				}
				DCAdminActionsLogger.instance.SafeTraceDebug(0L, "Start writing row to MRS log: ServerName='{0}', ActivityId='{1}', CustomData='{2}'", new object[]
				{
					DCAdminActionsLogger.instance.ServerName,
					activityId,
					text
				});
			}
			logRowFormatter[1] = activityId.ToString("D");
			logRowFormatter[2] = customData;
			DCAdminActionsLogger.instance.AppendLog(logRowFormatter);
			DCAdminActionsLogger.instance.SafeTraceDebug(0L, "The above row is written to MRS log successfully.", new object[0]);
		}

		// Token: 0x04000065 RID: 101
		internal const string LoggerFileNamePrefix = "DCAdminActions";

		// Token: 0x04000066 RID: 102
		internal const string LoggerComponentName = "DCAdminActions";

		// Token: 0x04000067 RID: 103
		internal const string LoggerTypeName = "Admin Audit Logs for DC Admin Actions";

		// Token: 0x04000068 RID: 104
		private static DCAdminActionsLogger instance;
	}
}
