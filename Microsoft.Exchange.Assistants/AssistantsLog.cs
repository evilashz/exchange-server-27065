using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants.Logging;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200001B RID: 27
	internal sealed class AssistantsLog : ActivityContextLogger
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000460B File Offset: 0x0000280B
		private AssistantsLog()
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004613 File Offset: 0x00002813
		internal static AssistantsLog Instance
		{
			get
			{
				if (AssistantsLog.instance == null)
				{
					AssistantsLog.instance = new AssistantsLog();
				}
				return AssistantsLog.instance;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000462B File Offset: 0x0000282B
		internal static HashSet<string> LogDisabledAssistants
		{
			get
			{
				return AssistantsLog.Instance.logDisabledAssistants;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004637 File Offset: 0x00002837
		protected override string LogComponentName
		{
			get
			{
				return "MailboxAssistants";
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000463E File Offset: 0x0000283E
		protected override string LogTypeName
		{
			get
			{
				return "Mailbox Assistants Log";
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004645 File Offset: 0x00002845
		protected override string FileNamePrefix
		{
			get
			{
				return "MailboxAssistants";
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000464C File Offset: 0x0000284C
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AssistantBaseTracer;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004654 File Offset: 0x00002854
		protected override string ServerName
		{
			get
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer == null)
				{
					return string.Empty;
				}
				return localServer.Name;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004676 File Offset: 0x00002876
		protected override int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004679 File Offset: 0x00002879
		public static void Stop()
		{
			if (AssistantsLog.instance != null)
			{
				AssistantsLog.Instance.Dispose();
				AssistantsLog.instance = null;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004692 File Offset: 0x00002892
		public static void Flush()
		{
			if (AssistantsLog.instance != null)
			{
				AssistantsLog.Instance.FlushLog();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000046A5 File Offset: 0x000028A5
		internal static void LogServiceStartEvent(Guid activityId)
		{
			AssistantsLog.InternalLogRow(activityId, "AssistantsService", null, AssistantsEventType.ServiceStarted, null, Guid.Empty);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046BA File Offset: 0x000028BA
		internal static void LogServiceStopEvent(Guid activityId)
		{
			AssistantsLog.InternalLogRow(activityId, "AssistantsService", null, AssistantsEventType.ServiceStopped, null, Guid.Empty);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000046CF File Offset: 0x000028CF
		internal static void LogStartProcessingMailboxEvent(Guid activityId, AssistantBase assistant, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, TimeBasedDatabaseJob job)
		{
			ArgumentValidator.ThrowIfNull("job", job);
			AssistantsLog.InternalLogAssistantEvent(activityId, assistant, AssistantsEventType.StartProcessingMailbox, null, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, job, MailboxSlaEventType.StartProcessingMailbox, MailboxSlaFilterReasonType.None, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000046F4 File Offset: 0x000028F4
		internal static void LogStartProcessingMailboxEvent(Guid activityId, AssistantBase assistant, MapiEvent mapiEvent, Guid mailboxGuid)
		{
			ArgumentValidator.ThrowIfNull("mapiEvent", mapiEvent);
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MapiEvent", AssistantsLog.FormatMapiEvent(mapiEvent))
			};
			AssistantsLog.InternalLogAssistantEvent(activityId, assistant, AssistantsEventType.StartProcessingMailbox, customData, mailboxGuid);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004734 File Offset: 0x00002934
		internal static string GetDiagnosticContext(Exception exception)
		{
			string result = string.Empty;
			while (exception != null)
			{
				MapiPermanentException ex = exception.InnerException as MapiPermanentException;
				if (ex != null)
				{
					result = ex.DiagCtx.ToCompactString();
					break;
				}
				MapiRetryableException ex2 = exception.InnerException as MapiRetryableException;
				if (ex2 != null)
				{
					result = ex2.DiagCtx.ToCompactString();
					break;
				}
				exception = exception.InnerException;
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004790 File Offset: 0x00002990
		internal static void LogErrorProcessingMailboxEvent(string assistantName, MailboxData mailbox, Exception e, string databaseName = "", string jobId = "", MailboxSlaRequestType requestType = MailboxSlaRequestType.Unknown)
		{
			string value = string.Empty;
			string value2 = "unknown";
			string value3 = "unknown";
			Guid guid = Guid.Empty;
			string value4 = string.Empty;
			string value5 = (e.InnerException != null) ? e.InnerException.GetType().ToString() : "null";
			string diagnosticContext = AssistantsLog.GetDiagnosticContext(e);
			Guid activityId = (ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty;
			if (mailbox != null)
			{
				value3 = mailbox.DatabaseGuid.ToString();
				StoreMailboxData storeMailboxData = mailbox as StoreMailboxData;
				if (storeMailboxData != null)
				{
					value2 = "Store";
					guid = storeMailboxData.Guid;
					if (storeMailboxData.OrganizationId != null)
					{
						value = storeMailboxData.OrganizationId.ToString();
					}
				}
				else
				{
					AdminRpcMailboxData adminRpcMailboxData = mailbox as AdminRpcMailboxData;
					if (adminRpcMailboxData != null)
					{
						value2 = "AdminRpc";
						value4 = adminRpcMailboxData.MailboxNumber.ToString(CultureInfo.InvariantCulture);
					}
				}
			}
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MailboxType", value2),
				new KeyValuePair<string, object>("MailboxGuid", guid),
				new KeyValuePair<string, object>("MailboxId", value4),
				new KeyValuePair<string, object>("TenantId", value),
				new KeyValuePair<string, object>("Database", value3),
				new KeyValuePair<string, object>("ExceptionType", e.GetType().ToString()),
				new KeyValuePair<string, object>("InnerExceptionType", value5),
				new KeyValuePair<string, object>("DiagnosticContext", diagnosticContext)
			};
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.ErrorProcessingMailbox, customData, guid);
			if (!string.IsNullOrEmpty(assistantName))
			{
				MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog logInstance = MailboxAssistantsSlaReportLogFactory.GetLogInstance(assistantName, SlaLogType.MailboxSlaLog);
				if (logInstance != null)
				{
					logInstance.LogMailboxEvent(assistantName, databaseName, jobId, requestType, guid, (mailbox == null) ? string.Empty : mailbox.DisplayName, MailboxSlaEventType.ErrorProcessingMailbox, MailboxSlaFilterReasonType.None, e);
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004976 File Offset: 0x00002B76
		internal static void LogEndProcessingMailboxEvent(Guid activityId, AssistantBase assistant, List<KeyValuePair<string, object>> customData, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, TimeBasedDatabaseJob job = null)
		{
			AssistantsLog.InternalLogAssistantEvent(activityId, assistant, AssistantsEventType.EndProcessingMailbox, customData, mailboxGuid);
			if (job != null)
			{
				AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, job, MailboxSlaEventType.EndProcessingMailbox, MailboxSlaFilterReasonType.None, null);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004994 File Offset: 0x00002B94
		internal static void LogMailboxInterestingEvent(Guid activityId, string assistantName, AssistantBase assistant, List<KeyValuePair<string, object>> customData, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, assistant, AssistantsEventType.MailboxInteresting, customData, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, null, MailboxSlaEventType.MailboxInteresting, MailboxSlaFilterReasonType.None, null);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000049B1 File Offset: 0x00002BB1
		internal static void LogMailboxInterestingEvent(Guid activityId, string assistantName, List<KeyValuePair<string, object>> customData, Guid mailboxGuid)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.MailboxInteresting, customData, mailboxGuid);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000049BF File Offset: 0x00002BBF
		internal static void LogMailboxNotInterestingEvent(Guid activityId, string assistantName, AssistantBase assistant, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, assistant, AssistantsEventType.MailboxNotInteresting, null, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, null, MailboxSlaEventType.MailboxNotInteresting, MailboxSlaFilterReasonType.None, null);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000049DC File Offset: 0x00002BDC
		internal static void LogMailboxFilteredEvent(Guid activityId, string assistantName, AssistantBase assistant, string reason, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, MailboxSlaFilterReasonType filterReason)
		{
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("Reason", reason)
			};
			AssistantsLog.InternalLogRow(activityId, assistantName, assistant, AssistantsEventType.FilterMailbox, customData, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, null, MailboxSlaEventType.FilterMailbox, filterReason, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004A1D File Offset: 0x00002C1D
		internal static void LogMailboxSucceedToOpenStoreSessionEvent(Guid activityId, string assistantName, AssistantBase assistant, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, TimeBasedDatabaseJob job)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, assistant, AssistantsEventType.SucceedOpenMailboxStoreSession, null, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, job, MailboxSlaEventType.SucceedOpenMailboxStoreSession, MailboxSlaFilterReasonType.None, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A38 File Offset: 0x00002C38
		internal static void LogMailboxFailedToOpenStoreSessionEvent(Guid activityId, string assistantName, AssistantBase assistant, Exception storeSessionException, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, TimeBasedDatabaseJob job)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			if (storeSessionException != null)
			{
				list.Add(new KeyValuePair<string, object>("ExceptionType", storeSessionException.GetType().ToString()));
				list.Add(new KeyValuePair<string, object>("ExceptionMessage", storeSessionException.Message));
				if (storeSessionException.InnerException != null)
				{
					list.Add(new KeyValuePair<string, object>("InnerExceptionType", storeSessionException.InnerException.GetType().ToString()));
					list.Add(new KeyValuePair<string, object>("InnerExceptionMessage", storeSessionException.InnerException.Message));
				}
			}
			AssistantsLog.InternalLogRow(activityId, assistantName, assistant, AssistantsEventType.FailedOpenMailboxStoreSession, list, mailboxGuid);
			AssistantsLog.LogMailboxSlaEvent(assistant, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, job, MailboxSlaEventType.FailedOpenMailboxStoreSession, MailboxSlaFilterReasonType.None, storeSessionException);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004AE0 File Offset: 0x00002CE0
		internal static void LogBeginJob(string assistantName, string databaseName, int startingPendingQueueCount)
		{
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("DatabaseName", databaseName),
				new KeyValuePair<string, object>("PendingQueueCount", startingPendingQueueCount)
			};
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.BeginJob, customData, Guid.Empty);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B30 File Offset: 0x00002D30
		internal static void LogEndJobEvent(string assistantName, List<KeyValuePair<string, object>> customData)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.EndJob, customData, Guid.Empty);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B46 File Offset: 0x00002D46
		internal static void LogDatabaseStartEvent(AssistantBase assistant)
		{
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.StartDatabase, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B50 File Offset: 0x00002D50
		internal static void LogDatabaseStopEvent(AssistantBase assistant)
		{
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.StopDatabase, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B5C File Offset: 0x00002D5C
		internal static void LogGetMailboxesQueryEvent(Guid activityId, string assistantName, int numberOfMailboxesInQuery, AssistantBase assistant)
		{
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MailboxesToProcess", numberOfMailboxesInQuery)
			};
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.ReceivedQueriedMailboxes, customData, Guid.Empty);
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.StartMailboxTableQuery, null);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BA0 File Offset: 0x00002DA0
		internal static void LogEndGetMailboxesEvent(Guid activityId, string assistantName, int numberOfMailboxesToProcess, AssistantBase assistant)
		{
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MailboxesToProcess", numberOfMailboxesToProcess)
			};
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.EndGetMailboxes, customData, Guid.Empty);
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.EndMailboxTableQuery, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004BE3 File Offset: 0x00002DE3
		internal static void LogNoJobsEvent(string assistantName)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.NoJobs, null, Guid.Empty);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void LogNotStartedEvent(string assistantName, AssistantBase assistant)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.NotStarted, null, Guid.Empty);
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.DatabaseIsStopped, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004C17 File Offset: 0x00002E17
		internal static void LogDriverNotStartedEvent(string assistantName, AssistantBase assistant)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.DriverNotStarted, null, Guid.Empty);
			AssistantsLog.LogDatabaseSlaEvent(assistant, DatabaseSlaEventType.DatabaseIsStopped, null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C35 File Offset: 0x00002E35
		internal static void LogJobAlreadyRunningEvent(string assistantName)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.JobAlreadyRunning, null, Guid.Empty);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C4B File Offset: 0x00002E4B
		internal static void LogNoMailboxesPendingEvent(string assistantName)
		{
			AssistantsLog.InternalLogRow(Guid.Empty, assistantName, null, AssistantsEventType.NoMailboxes, null, Guid.Empty);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C64 File Offset: 0x00002E64
		internal static void LogErrorEnumeratingMailboxes(ITimeBasedAssistant assistant, Guid mailboxGuid, Exception exception, bool isExceptionHandled)
		{
			string value = (exception.InnerException != null) ? exception.InnerException.GetType().ToString() : string.Empty;
			List<KeyValuePair<string, object>> customData = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MailboxGuid", mailboxGuid.ToString()),
				new KeyValuePair<string, object>("ExceptionType", exception.GetType().ToString()),
				new KeyValuePair<string, object>("InnerExceptionType", value),
				new KeyValuePair<string, object>("IsExceptionHandled", isExceptionHandled.ToString()),
				new KeyValuePair<string, object>("ExceptionDetail", exception.ToString())
			};
			AssistantsLog.InternalLogRow(Guid.Empty, (assistant != null) ? assistant.NonLocalizedName : string.Empty, null, AssistantsEventType.ErrorEnumeratingMailbox, customData, Guid.Empty);
			AssistantsLog.LogDatabaseSlaEvent(assistant as AssistantBase, DatabaseSlaEventType.ErrorMailboxTableQuery, exception);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004D3F File Offset: 0x00002F3F
		internal static void LogFolderSyncOperationEvent(Guid activityId, string assistantName, List<KeyValuePair<string, object>> customData)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.FolderSyncOperation, customData, Guid.Empty);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004D51 File Offset: 0x00002F51
		internal static void LogFolderSyncExceptionEvent(Guid activityId, string assistantName, List<KeyValuePair<string, object>> customData)
		{
			AssistantsLog.InternalLogRow(activityId, assistantName, null, AssistantsEventType.FolderSyncException, customData, Guid.Empty);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004D64 File Offset: 0x00002F64
		protected override void InternalConfigure(ActivityContextLogFileSettings settings)
		{
			base.InternalConfigure(settings);
			this.logDisabledAssistants = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			AssistantsLogFileSettings assistantsLogFileSettings = settings as AssistantsLogFileSettings;
			if (assistantsLogFileSettings == null)
			{
				return;
			}
			foreach (string text in assistantsLogFileSettings.LogDisabledAssistants)
			{
				string text2 = text.Trim();
				if (text2 != string.Empty)
				{
					this.logDisabledAssistants.Add(text2);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004DD4 File Offset: 0x00002FD4
		protected override void InternalLogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			object userState = activityScope.UserState;
			AssistantBase assistantBase = null;
			string assistantShortName;
			if (activityScope.ActivityType == ActivityType.Global)
			{
				assistantShortName = "GlobalActivity";
			}
			else
			{
				SystemTaskBase systemTaskBase = userState as SystemTaskBase;
				if (systemTaskBase != null)
				{
					assistantShortName = systemTaskBase.Workload.Id;
				}
				else
				{
					AssistantBase assistantBase2 = userState as AssistantBase;
					if (assistantBase2 == null)
					{
						return;
					}
					assistantBase = assistantBase2;
					assistantShortName = assistantBase.NonLocalizedName;
				}
			}
			AssistantsEventType eventType2;
			switch (eventType)
			{
			case ActivityEventType.SuspendActivity:
				eventType2 = AssistantsEventType.SuspendActivity;
				goto IL_92;
			case ActivityEventType.EndActivity:
				eventType2 = AssistantsEventType.EndActivity;
				goto IL_92;
			}
			base.SafeTraceDebug(0L, "Skip logging ActivityEvent '{0}'.", new object[]
			{
				eventType
			});
			return;
			IL_92:
			List<KeyValuePair<string, object>> customData = WorkloadManagementLogger.FormatWlmActivity(activityScope, true);
			AssistantsLog.InternalLogRow(activityScope.ActivityId, assistantShortName, assistantBase, eventType2, customData, Guid.Empty);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004E91 File Offset: 0x00003091
		protected override string[] GetLogFields()
		{
			return Enum.GetNames(typeof(AssistantsLogField));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004EA2 File Offset: 0x000030A2
		protected override ActivityContextLogFileSettings GetLogFileSettings()
		{
			return AssistantsLogFileSettings.Load();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004EAC File Offset: 0x000030AC
		private static void InternalLogAssistantEvent(Guid activityId, AssistantBase assistant, AssistantsEventType eventType, List<KeyValuePair<string, object>> customData, Guid mailboxGuid)
		{
			string assistantShortName = (assistant == null) ? "Unknown" : assistant.NonLocalizedName;
			AssistantsLog.InternalLogRow(activityId, assistantShortName, assistant, eventType, customData, mailboxGuid);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004ED8 File Offset: 0x000030D8
		private static void InternalLogRow(Guid activityId, string assistantShortName, AssistantBase assistant, AssistantsEventType eventType, List<KeyValuePair<string, object>> customData, Guid mailboxGuid)
		{
			if (!AssistantsLog.Instance.Enabled)
			{
				AssistantsLog.Instance.SafeTraceDebug(0L, "Mailbox assistant log is disabled, skip writing to the log file.", new object[0]);
				return;
			}
			if (AssistantsLog.LogDisabledAssistants.Contains(assistantShortName))
			{
				AssistantsLog.Instance.SafeTraceDebug(0L, "Mailbox assistant '{0}' is disabled for logging, skip writing to the log file.", new object[]
				{
					assistantShortName
				});
				return;
			}
			string text = string.Empty;
			if (assistant != null && assistant.DatabaseInfo != null)
			{
				text = assistant.DatabaseInfo.DatabaseName;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(AssistantsLog.Instance.LogSchema);
			if (AssistantsLog.Instance.IsDebugTraceEnabled)
			{
				string text2 = string.Empty;
				if (customData != null)
				{
					bool flag;
					text2 = LogRowFormatter.FormatCollection(customData, out flag);
				}
				AssistantsLog.Instance.SafeTraceDebug(0L, "Start writing row to mailbox assistant log: ServerName='{0}', Location='{1}', AssistantName='{2}', ActivityId='{3}', TargetObject='{4}', Event='{5}', CustomData='{6}'", new object[]
				{
					AssistantsLog.Instance.ServerName,
					text,
					assistantShortName,
					activityId,
					mailboxGuid,
					AssistantsLog.stringDictionary[eventType],
					text2
				});
			}
			logRowFormatter[1] = AssistantsLog.Instance.ServerName;
			logRowFormatter[3] = assistantShortName;
			logRowFormatter[6] = AssistantsLog.stringDictionary[eventType];
			logRowFormatter[2] = text;
			logRowFormatter[7] = customData;
			logRowFormatter[5] = ((mailboxGuid == Guid.Empty) ? string.Empty : mailboxGuid.ToString("D"));
			logRowFormatter[4] = ((activityId == Guid.Empty) ? string.Empty : activityId.ToString("D"));
			AssistantsLog.Append(logRowFormatter);
			AssistantsLog.Instance.SafeTraceDebug(0L, "The above row is written to mailbox assistant log successfully.", new object[0]);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005088 File Offset: 0x00003288
		private static void Append(LogRowFormatter row)
		{
			AssistantsLog.Instance.AppendLog(row);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005098 File Offset: 0x00003298
		private static Dictionary<AssistantsEventType, string> CreateTypeDictionary()
		{
			Dictionary<AssistantsEventType, string> dictionary = new Dictionary<AssistantsEventType, string>();
			string[] names = Enum.GetNames(typeof(AssistantsEventType));
			Array values = Enum.GetValues(typeof(AssistantsEventType));
			int num = names.Length;
			for (int i = 0; i < num; i++)
			{
				dictionary.Add((AssistantsEventType)values.GetValue(i), names[i]);
			}
			return dictionary;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000050F8 File Offset: 0x000032F8
		private static string FormatMapiEvent(MapiEvent mapiEvent)
		{
			string text = string.Format("Counter: 0x{0,0:X}, MailboxGUID: {1}, Mask: {2}, Flags: {3}, ExtendedFlags: {4}, Object Class: {5}, Created Time: {6}, Item Type: {7}, Item EntryId: {8}, Parent entryId: {9}, Old Item entryId: {10}, Old parent entryId: {11}, SID: {12}, Client Type: {13}, Document ID: {14}", new object[]
			{
				mapiEvent.EventCounter,
				mapiEvent.MailboxGuid,
				mapiEvent.EventMask,
				mapiEvent.EventFlags,
				mapiEvent.ExtendedEventFlags,
				mapiEvent.ObjectClass,
				mapiEvent.CreateTime,
				mapiEvent.ItemType,
				AssistantsLog.FormatEntryId(mapiEvent.ItemEntryId),
				AssistantsLog.FormatEntryId(mapiEvent.ParentEntryId),
				AssistantsLog.FormatEntryId(mapiEvent.OldItemEntryId),
				AssistantsLog.FormatEntryId(mapiEvent.OldParentEntryId),
				(null != mapiEvent.Sid) ? mapiEvent.Sid.ToString() : "<null>",
				mapiEvent.ClientType,
				mapiEvent.DocumentId
			});
			if (ObjectType.MAPI_FOLDER == mapiEvent.ItemType)
			{
				text += string.Format(", Item Count: {0}, Unread Item Count: {1}", mapiEvent.ItemCount, mapiEvent.UnreadItemCount);
			}
			return text;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005232 File Offset: 0x00003432
		private static string FormatEntryId(byte[] entryId)
		{
			if (entryId != null)
			{
				return BitConverter.ToString(entryId);
			}
			return "<null>";
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005244 File Offset: 0x00003444
		private static void LogMailboxSlaEvent(AssistantBase assistant, Guid mailboxGuid, string mailboxDisplayNameTracingOnlyUsage, TimeBasedDatabaseJob job, MailboxSlaEventType eventType, MailboxSlaFilterReasonType reason = MailboxSlaFilterReasonType.None, Exception exception = null)
		{
			string text = "Unknown";
			string databaseName = "Unknown";
			string jobId = string.Empty;
			MailboxSlaRequestType requestType = MailboxSlaRequestType.Unknown;
			if (assistant != null)
			{
				databaseName = ((assistant.DatabaseInfo == null) ? "Unknown" : assistant.DatabaseInfo.DatabaseName);
				text = assistant.NonLocalizedName;
			}
			if (job != null)
			{
				jobId = job.StartTime.ToString("O");
				requestType = ((job is TimeBasedDatabaseWindowJob) ? MailboxSlaRequestType.Scheduled : MailboxSlaRequestType.OnDemand);
			}
			MailboxAssistantsSlaReportLogFactory.MailboxAssistantsSlaReportLog logInstance = MailboxAssistantsSlaReportLogFactory.GetLogInstance(text, SlaLogType.MailboxSlaLog);
			if (logInstance != null)
			{
				logInstance.LogMailboxEvent(text, databaseName, jobId, requestType, mailboxGuid, mailboxDisplayNameTracingOnlyUsage, eventType, reason, exception);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000052D0 File Offset: 0x000034D0
		private static void LogDatabaseSlaEvent(AssistantBase assistant, DatabaseSlaEventType eventType, Exception exception = null)
		{
			string text = "Unknown";
			string databaseName = "Unknown";
			if (assistant != null)
			{
				databaseName = ((assistant.DatabaseInfo == null) ? "Unknown" : assistant.DatabaseInfo.DatabaseName);
				text = assistant.NonLocalizedName;
			}
			MailboxAssistantsSlaReportLogFactory.MailboxAssistantsDatabaseSlaLog mailboxAssistantsDatabaseSlaLog = MailboxAssistantsSlaReportLogFactory.GetLogInstance(text, SlaLogType.DatabaseSlaLog) as MailboxAssistantsSlaReportLogFactory.MailboxAssistantsDatabaseSlaLog;
			if (mailboxAssistantsDatabaseSlaLog != null)
			{
				mailboxAssistantsDatabaseSlaLog.LogDatabaseEvent(text, databaseName, eventType, exception);
			}
		}

		// Token: 0x040000DF RID: 223
		internal const string AssistantsServiceName = "AssistantsService";

		// Token: 0x040000E0 RID: 224
		internal const string AssistantsLogFileNamePrefix = "MailboxAssistants";

		// Token: 0x040000E1 RID: 225
		internal const string AssistantsLogComponentName = "MailboxAssistants";

		// Token: 0x040000E2 RID: 226
		internal const string AssistantsLogTypeName = "Mailbox Assistants Log";

		// Token: 0x040000E3 RID: 227
		internal const string Unknown = "Unknown";

		// Token: 0x040000E4 RID: 228
		private static readonly Dictionary<AssistantsEventType, string> stringDictionary = AssistantsLog.CreateTypeDictionary();

		// Token: 0x040000E5 RID: 229
		private static AssistantsLog instance;

		// Token: 0x040000E6 RID: 230
		private HashSet<string> logDisabledAssistants;
	}
}
