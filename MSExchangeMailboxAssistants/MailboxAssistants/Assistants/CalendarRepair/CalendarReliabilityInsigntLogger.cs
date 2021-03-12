using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000156 RID: 342
	internal class CalendarReliabilityInsigntLogger : DisposeTrackableBase
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0005434C File Offset: 0x0005254C
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00054354 File Offset: 0x00052554
		internal bool IsLoggerDisposed { get; private set; }

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000544EC File Offset: 0x000526EC
		internal void Log(CalendarRepairAssistantLogEntry logEntry, List<MeetingValidationResult> validationResults)
		{
			foreach (MeetingValidationResult meetingValidationResult in validationResults)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
				logRowFormatter[1] = ((LocalServerCache.LocalServer == null) ? string.Empty : LocalServerCache.LocalServer.Name);
				logRowFormatter[2] = ((logEntry.MailboxGuid == Guid.Empty) ? string.Empty : logEntry.MailboxGuid.ToString("D"));
				logRowFormatter[3] = ((logEntry.TenantGuid == Guid.Empty) ? string.Empty : logEntry.TenantGuid.ToString("D"));
				logRowFormatter[4] = (meetingValidationResult.GlobalObjectId.ToString() ?? string.Empty);
				logRowFormatter[12] = meetingValidationResult.MeetingData.CalendarItemType.ToString();
				logRowFormatter[5] = (meetingValidationResult.MailboxUserPrimarySmtpAddress ?? string.Empty);
				logRowFormatter[6] = (meetingValidationResult.Subject ?? string.Empty);
				logRowFormatter[7] = meetingValidationResult.CreationTime;
				logRowFormatter[8] = meetingValidationResult.StartTime;
				logRowFormatter[9] = meetingValidationResult.EndTime;
				logRowFormatter[10] = meetingValidationResult.NumberOfDelegates;
				logRowFormatter[11] = meetingValidationResult.WasValidationSuccessful;
				logRowFormatter[13] = meetingValidationResult.IsConsistent;
				logRowFormatter[14] = meetingValidationResult.IsDuplicate;
				logRowFormatter[15] = meetingValidationResult.DuplicatesDetected;
				logRowFormatter[16] = meetingValidationResult.IsDuplicateRemoved;
				logRowFormatter[17] = meetingValidationResult.IsOrganizer;
				logRowFormatter[18] = ((meetingValidationResult.ResultsPerAttendee == null) ? 0 : meetingValidationResult.ResultsPerAttendee.Count);
				string value = this.FormatDictionaryToString((from x in meetingValidationResult.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => mcr.CheckResultList.SelectMany((ConsistencyCheckResult cr) => from i in cr.Inconsistencies
				where i.ShouldFix
				select i))
				group x by new
				{
					x.Flag,
					x.Owner
				} into y
				select new
				{
					Item = y.Key.Flag.ToString(),
					Owner = y.Key.Owner.ToString(),
					Count = y.Count<Inconsistency>()
				}).ToDictionary(z => z.Item + "-" + z.Owner, z => z.Count));
				string value2 = this.FormatDictionaryToString((from x in meetingValidationResult.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => mcr.CheckResultList.SelectMany((ConsistencyCheckResult cr) => from i in cr.Inconsistencies
				where !i.ShouldFix
				select i))
				group x by new
				{
					x.Flag,
					x.Owner
				} into y
				select new
				{
					Item = y.Key.Flag.ToString(),
					Owner = y.Key.Owner.ToString(),
					Count = y.Count<Inconsistency>()
				}).ToDictionary(z => z.Item + "-" + z.Owner, z => z.Count));
				logRowFormatter[19] = value;
				logRowFormatter[20] = value2;
				lock (this.lockObject)
				{
					if (this.IsLoggerDisposed)
					{
						break;
					}
					this.log.Append(logRowFormatter, this.TimestampField);
				}
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000548F4 File Offset: 0x00052AF4
		internal void FlushAndDispose()
		{
			lock (this.lockObject)
			{
				if (!this.IsLoggerDisposed)
				{
					this.log.Flush();
					this.Dispose();
					this.IsLoggerDisposed = true;
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00054950 File Offset: 0x00052B50
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00054958 File Offset: 0x00052B58
		private protected int TimestampField { protected get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00054961 File Offset: 0x00052B61
		protected Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AssistantBaseTracer;
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00054968 File Offset: 0x00052B68
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarReliabilityInsigntLogger>(this);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00054970 File Offset: 0x00052B70
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.SafeTraceDebug(0L, "Closing {0} logger.", new object[]
				{
					"CalendarRepair"
				});
				if (this.log != null)
				{
					this.log.Close();
					this.log = null;
				}
				this.SafeTraceDebug(0L, "{0} logger is closed.", new object[]
				{
					"CalendarRepair"
				});
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000549D4 File Offset: 0x00052BD4
		protected void SafeTraceDebug(long id, string message, params object[] args)
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug(id, message, args);
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000549EC File Offset: 0x00052BEC
		private CalendarReliabilityInsigntLogger()
		{
			string[] names = Enum.GetNames(typeof(CraReliabilityLogFields));
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			ActivityContextLogFileSettings activityContextLogFileSettings = new CalendarReliabilityInsigntLogSettings();
			this.logSchema = new LogSchema("Microsoft Exchange", version.ToString(), "CalendarRepair", names);
			this.log = new Log("CalendarRepair", new LogHeaderFormatter(this.logSchema), "CalendarRepair");
			this.log.Configure(activityContextLogFileSettings.DirectoryPath, activityContextLogFileSettings.MaxAge, (long)activityContextLogFileSettings.MaxDirectorySize.ToBytes(), (long)activityContextLogFileSettings.MaxFileSize.ToBytes(), (int)activityContextLogFileSettings.CacheSize.ToBytes(), activityContextLogFileSettings.FlushInterval, activityContextLogFileSettings.FlushToDisk);
			this.IsLoggerDisposed = false;
			this.TimestampField = 0;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00054ACC File Offset: 0x00052CCC
		private string FormatDictionaryToString(Dictionary<string, int> dictionary)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				stringBuilder.Append(string.Format("{0}_{1}|", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'|'
			});
		}

		// Token: 0x040008E8 RID: 2280
		internal const string AssistantName = "CalendarRepair";

		// Token: 0x040008E9 RID: 2281
		internal static readonly CalendarReliabilityInsigntLogger Instance = new CalendarReliabilityInsigntLogger();

		// Token: 0x040008EA RID: 2282
		private Log log;

		// Token: 0x040008EB RID: 2283
		private LogSchema logSchema;

		// Token: 0x040008EC RID: 2284
		private object lockObject = new object();
	}
}
