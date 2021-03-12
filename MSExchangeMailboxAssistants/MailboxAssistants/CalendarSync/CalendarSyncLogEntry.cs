using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.Sharing;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C8 RID: 200
	internal class CalendarSyncLogEntry
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0003AA14 File Offset: 0x00038C14
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0003AA1C File Offset: 0x00038C1C
		public DateTime ProcessingStartTime { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0003AA25 File Offset: 0x00038C25
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0003AA2D File Offset: 0x00038C2D
		public DateTime ProcessingEndTime { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0003AA36 File Offset: 0x00038C36
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0003AA3E File Offset: 0x00038C3E
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0003AA47 File Offset: 0x00038C47
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0003AA4F File Offset: 0x00038C4F
		public bool IsArchive { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0003AA58 File Offset: 0x00038C58
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0003AA60 File Offset: 0x00038C60
		public bool IsOnDemandJob { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0003AA69 File Offset: 0x00038C69
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0003AA71 File Offset: 0x00038C71
		public long TotalProcessingTime { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0003AA7A File Offset: 0x00038C7A
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0003AA82 File Offset: 0x00038C82
		public long MaxTimeoutValue { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0003AA8B File Offset: 0x00038C8B
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0003AA93 File Offset: 0x00038C93
		public string ExceptionType { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0003AA9C File Offset: 0x00038C9C
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0003AAA4 File Offset: 0x00038CA4
		public int NumOfFoldersToSync { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0003AAAD File Offset: 0x00038CAD
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0003AAB5 File Offset: 0x00038CB5
		public int NumOfFoldersActuallySynced { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0003AABE File Offset: 0x00038CBE
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0003AAC6 File Offset: 0x00038CC6
		public Guid TenantGuid { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0003AACF File Offset: 0x00038CCF
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0003AAD7 File Offset: 0x00038CD7
		public Exception Exception { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0003AAE8 File Offset: 0x00038CE8
		public bool IsDeadlineExpired { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0003AAF1 File Offset: 0x00038CF1
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0003AAF9 File Offset: 0x00038CF9
		public object MailboxType { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0003AB02 File Offset: 0x00038D02
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0003AB0A File Offset: 0x00038D0A
		public List<CalendarSyncFolderOperationLogEntry> FolderDetailLoglines { get; set; }

		// Token: 0x0600086D RID: 2157 RVA: 0x0003AB14 File Offset: 0x00038D14
		public List<KeyValuePair<string, object>> LogAllData(MailboxSession mailboxSession, string parameters)
		{
			if (this.Exception != null)
			{
				SharingSyncAssistantLog.LogEntry(mailboxSession, "CalendarSyncAssistantHelper.ProcessFolder with parameter {0} hit the following exception: {1}", new object[]
				{
					parameters,
					this.Exception
				});
			}
			if (this.IsDeadlineExpired)
			{
				SharingSyncAssistantLog.LogEntry(mailboxSession, "Deadline expired before processing all folders. Processed {0}/{1} folders. MaxSyncTime: {2}", new object[]
				{
					this.NumOfFoldersActuallySynced,
					this.NumOfFoldersToSync,
					this.MaxTimeoutValue
				});
			}
			if (this.FolderDetailLoglines != null)
			{
				foreach (CalendarSyncFolderOperationLogEntry calendarSyncFolderOperationLogEntry in this.FolderDetailLoglines)
				{
					List<KeyValuePair<string, object>> list = calendarSyncFolderOperationLogEntry.FormatCustomData();
					AssistantsLog.LogFolderSyncOperationEvent(calendarSyncFolderOperationLogEntry.ActivityId, "CalendarSyncAssistant", list);
					this.AddExtendedProperty("FolderFullyProcessed", calendarSyncFolderOperationLogEntry.FolderType, calendarSyncFolderOperationLogEntry.FolderId, calendarSyncFolderOperationLogEntry.IsSyncSuccess);
					this.PostFolderDetailsToQuickLogs(mailboxSession, calendarSyncFolderOperationLogEntry, string.Join<KeyValuePair<string, object>>(";", list.ToArray()));
					if (calendarSyncFolderOperationLogEntry.SanitizedStackTraces != null && calendarSyncFolderOperationLogEntry.SanitizedStackTraces.Count > 0)
					{
						foreach (List<KeyValuePair<string, object>> customData in calendarSyncFolderOperationLogEntry.SanitizedStackTraces)
						{
							AssistantsLog.LogFolderSyncExceptionEvent(calendarSyncFolderOperationLogEntry.ActivityId, "CalendarSyncAssistant", customData);
						}
					}
				}
			}
			return this.FormatCustomData();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0003AC9C File Offset: 0x00038E9C
		public void AddFolderDetailLogline(CalendarSyncFolderOperationLogEntry folderDetail)
		{
			if (this.FolderDetailLoglines == null)
			{
				this.FolderDetailLoglines = new List<CalendarSyncFolderOperationLogEntry>();
			}
			this.FolderDetailLoglines.Add(folderDetail);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003ACC0 File Offset: 0x00038EC0
		private void PostFolderDetailsToQuickLogs(MailboxSession mailboxSession, CalendarSyncFolderOperationLogEntry folderOpLogEntry, string folderDetailsToLog)
		{
			if (folderOpLogEntry.FolderType == "UnknownFolderType")
			{
				InternetCalendarLog.LogEntry(mailboxSession, string.Format("CalendarSyncAssistantHelper.ProcessFolder Couldn't find a corresponding SynchronizableFolderType object for folder id {0}.", folderOpLogEntry.FolderId));
				return;
			}
			if (folderOpLogEntry.FolderType == "InternetCalendar")
			{
				if (folderOpLogEntry.SubscriptionData == null)
				{
					string entry = string.Format("InternetCalendarType.Synchronize couldn't find subscription data for folder {0} with id {1} for mailbox {2}.", folderOpLogEntry.DisplayName, folderOpLogEntry.FolderId, mailboxSession.DisplayName);
					InternetCalendarLog.LogEntry(mailboxSession, entry);
				}
				InternetCalendarLog.LogEntry(mailboxSession, string.Format("Sync Details for InternetCalendar subscription {0}, folder {1} with id {2} are {3}.", new object[]
				{
					folderOpLogEntry.SubscriptionData,
					folderOpLogEntry.DisplayName,
					folderOpLogEntry.FolderId,
					folderDetailsToLog
				}));
			}
			if (folderOpLogEntry.IsDeadlineExpired)
			{
				SharingSyncAssistantLog.LogEntry(mailboxSession, "Deadline expired before processing folder {0}. MaxSyncTime: {1}", new object[]
				{
					folderOpLogEntry.DisplayName,
					SynchronizableFolderType.MaxSyncTimePerFolder
				});
			}
			SharingSyncAssistantLog.LogEntry(mailboxSession, "Sync Details for folder {0} are {1}", new object[]
			{
				folderOpLogEntry.DisplayName,
				folderDetailsToLog
			});
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003ADBC File Offset: 0x00038FBC
		private List<KeyValuePair<string, object>> FormatCustomData()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			list.Add(new KeyValuePair<string, object>("ProcessingStartTime", string.Format("{0}", this.ProcessingStartTime)));
			list.Add(new KeyValuePair<string, object>("ProcessingEndTime", string.Format("{0}", this.ProcessingEndTime)));
			list.Add(new KeyValuePair<string, object>("MailboxGuid", string.Format("{0}", this.MailboxGuid)));
			list.Add(new KeyValuePair<string, object>("IsArchive", string.Format("{0}", this.IsArchive)));
			list.Add(new KeyValuePair<string, object>("IsOnDemandJob", string.Format("{0}", this.IsOnDemandJob)));
			list.Add(new KeyValuePair<string, object>("TotalProcessingTime", string.Format("{0}", this.TotalProcessingTime)));
			list.Add(new KeyValuePair<string, object>("MaxTimeoutValue", string.Format("{0}", this.MaxTimeoutValue)));
			list.Add(new KeyValuePair<string, object>("ExceptionType", string.Format("{0}", this.ExceptionType)));
			list.Add(new KeyValuePair<string, object>("NumOfFoldersToSync", string.Format("{0}", this.NumOfFoldersToSync)));
			list.Add(new KeyValuePair<string, object>("NumOfFoldersActuallySynced", string.Format("{0}", this.NumOfFoldersActuallySynced)));
			list.Add(new KeyValuePair<string, object>("TenantGuid", string.Format("{0}", this.TenantGuid)));
			list.Add(new KeyValuePair<string, object>("MailboxType", string.Format("{0}", this.MailboxType)));
			foreach (KeyValuePair<string, object> keyValuePair in this.extendedPropertiesMap)
			{
				list.Add(new KeyValuePair<string, object>(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0003AFDC File Offset: 0x000391DC
		private void AddExtendedProperty(string extendedTypeLabel, string folderType, string folderId, bool isSuccess)
		{
			string key;
			if (folderId != null)
			{
				key = string.Format("{0}.{1}[{2}]", extendedTypeLabel, folderType, SpecialCharacters.SanitizeForLogging(folderId));
			}
			else
			{
				key = string.Format("{0}.{1}[{2}]", extendedTypeLabel, "UnknownFolderId", SpecialCharacters.SanitizeForLogging(Guid.NewGuid().ToString()));
				isSuccess = false;
			}
			if (!this.extendedPropertiesMap.ContainsKey(key))
			{
				this.extendedPropertiesMap.Add(key, isSuccess.ToString());
			}
		}

		// Token: 0x040005D8 RID: 1496
		private const string folderFullyProcessed = "FolderFullyProcessed";

		// Token: 0x040005D9 RID: 1497
		private const string InternetCalendarType = "InternetCalendar";

		// Token: 0x040005DA RID: 1498
		private Dictionary<string, object> extendedPropertiesMap = new Dictionary<string, object>();
	}
}
