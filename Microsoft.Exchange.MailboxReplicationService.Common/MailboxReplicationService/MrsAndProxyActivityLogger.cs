using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxReplicationService;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001BE RID: 446
	internal sealed class MrsAndProxyActivityLogger : ActivityContextLogger
	{
		// Token: 0x060010D1 RID: 4305 RVA: 0x000273E5 File Offset: 0x000255E5
		private MrsAndProxyActivityLogger()
		{
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000273F8 File Offset: 0x000255F8
		protected override string LogComponentName
		{
			get
			{
				return WorkloadType.MailboxReplicationService.ToString();
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00027405 File Offset: 0x00025605
		protected override string LogTypeName
		{
			get
			{
				return "Mailbox Replication Log";
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0002740C File Offset: 0x0002560C
		protected override string FileNamePrefix
		{
			get
			{
				return "MRS";
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00027413 File Offset: 0x00025613
		protected override int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00027416 File Offset: 0x00025616
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MailboxReplicationResourceHealthTracer;
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0002741D File Offset: 0x0002561D
		public static MrsAndProxyActivityLogger Start()
		{
			MrsAndProxyActivityLogger.instance = new MrsAndProxyActivityLogger();
			MrsAndProxyActivityLogger.InternalLogRow(MrsAndProxyActivityLogger.instance.id, "ServiceStart", null, null, null, null);
			return MrsAndProxyActivityLogger.instance;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00027446 File Offset: 0x00025646
		public static void Stop()
		{
			if (MrsAndProxyActivityLogger.instance != null)
			{
				MrsAndProxyActivityLogger.InternalLogRow(MrsAndProxyActivityLogger.instance.id, "ServiceStop", null, null, null, null);
				MrsAndProxyActivityLogger.instance.Dispose();
				MrsAndProxyActivityLogger.instance = null;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00027477 File Offset: 0x00025677
		public static void Flush()
		{
			if (MrsAndProxyActivityLogger.instance != null)
			{
				MrsAndProxyActivityLogger.instance.FlushLog();
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002748C File Offset: 0x0002568C
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
				MrsAndProxyActivityLogger.instance.SafeTraceDebug(0L, "Skip logging ActivityEvent '{0}'.", new object[]
				{
					eventType
				});
				return;
			}
			MrsAndProxyActivityLogger.InternalLogRow(scope.ActivityId, ActivityContextLogger.ActivityEventTypeDictionary[eventType], scope.Action, scope.UserId, scope.ClientInfo, customData);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002750B File Offset: 0x0002570B
		protected override string[] GetLogFields()
		{
			return Enum.GetNames(typeof(MrsAndProxyActivityContextLogFields));
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002751C File Offset: 0x0002571C
		protected override ActivityContextLogFileSettings GetLogFileSettings()
		{
			return MrsAndProxyLoggerSettings.Load();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00027524 File Offset: 0x00025724
		private static void InternalLogRow(Guid activityId, string eventType, string action, string mailboxId, string mrsServerName, List<KeyValuePair<string, object>> customData)
		{
			if (MrsAndProxyActivityLogger.instance == null)
			{
				return;
			}
			if (!MrsAndProxyActivityLogger.instance.Enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MrsAndProxyActivityLogger.instance.LogSchema);
			if (MrsAndProxyActivityLogger.instance.IsDebugTraceEnabled)
			{
				string text = string.Empty;
				if (customData != null)
				{
					bool flag;
					text = LogRowFormatter.FormatCollection(customData, out flag);
				}
				MrsAndProxyActivityLogger.instance.SafeTraceDebug(0L, "Adding row to MRS log: ServerName='{0}', ActivityId='{1}', Event='{2}', Action= '{3}', Mailbox='{4}', CustomData='{5}'", new object[]
				{
					MrsAndProxyActivityLogger.instance.ServerName,
					activityId,
					eventType,
					action,
					mailboxId,
					text
				});
			}
			logRowFormatter[2] = activityId.ToString("D");
			logRowFormatter[1] = mrsServerName;
			logRowFormatter[3] = eventType;
			logRowFormatter[4] = action;
			logRowFormatter[5] = mailboxId;
			logRowFormatter[6] = customData;
			MrsAndProxyActivityLogger.instance.AppendLog(logRowFormatter);
		}

		// Token: 0x0400097B RID: 2427
		internal const string MrsProxyClassName = "MailboxReplicationProxyService";

		// Token: 0x0400097C RID: 2428
		internal const string LoggerFileNamePrefix = "MRS";

		// Token: 0x0400097D RID: 2429
		internal const string LoggerTypeName = "Mailbox Replication Log";

		// Token: 0x0400097E RID: 2430
		private const string ServiceStart = "ServiceStart";

		// Token: 0x0400097F RID: 2431
		private const string ServiceStop = "ServiceStop";

		// Token: 0x04000980 RID: 2432
		private static MrsAndProxyActivityLogger instance;

		// Token: 0x04000981 RID: 2433
		private readonly Guid id = Guid.NewGuid();
	}
}
