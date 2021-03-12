using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F0C RID: 3852
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActivityFileLogger
	{
		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x0600849D RID: 33949 RVA: 0x0024330A File Offset: 0x0024150A
		public static ActivityFileLogger Instance
		{
			get
			{
				return ActivityFileLogger.lazyInstanceWrapper.Value;
			}
		}

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x0600849E RID: 33950 RVA: 0x00243316 File Offset: 0x00241516
		public string FilePrefix
		{
			get
			{
				return this.filePrefix;
			}
		}

		// Token: 0x0600849F RID: 33951 RVA: 0x00243320 File Offset: 0x00241520
		internal ActivityFileLogger()
		{
			int num = 0;
			string text = string.Empty;
			string version = "15.00.1497.010";
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				num = currentProcess.Id;
				text = currentProcess.ProcessName;
			}
			this.logSchema = new LogSchema(text, version, "ActivityLogging", ActivityFileLogger.LogSchemaStrings);
			this.filePrefix = string.Format("{0}_{1}_{2}_", "ActivityLogging", text, num.ToString());
			this.logger = new Log(this.filePrefix, new LogHeaderFormatter(this.logSchema), "ActivityLogging");
			this.logger.Configure(ActivityFileLogger.LogPath, ActivityLoggingConfig.Instance.MaxLogFileAge, (long)ActivityLoggingConfig.Instance.MaxLogDirectorySize.ToBytes(), (long)ActivityLoggingConfig.Instance.MaxLogFileSize.ToBytes());
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x00243410 File Offset: 0x00241610
		internal static void ResetInstance()
		{
			ActivityFileLogger.lazyInstanceWrapper.Value.logger.Flush();
			ActivityFileLogger.lazyInstanceWrapper.Value.logger.Close();
			ActivityFileLogger.lazyInstanceWrapper = new Lazy<ActivityFileLogger>(() => new ActivityFileLogger());
		}

		// Token: 0x060084A1 RID: 33953 RVA: 0x0024346C File Offset: 0x0024166C
		internal LogRowFormatter GetFileLogRow(Activity activity)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[0] = activity.Id;
			logRowFormatter[1] = activity.TimeStamp.ToUtc().ToISOString();
			logRowFormatter[2] = activity.ClientId;
			logRowFormatter[3] = activity.MailboxGuid;
			logRowFormatter[4] = activity.ClientSessionId;
			logRowFormatter[5] = activity.SequenceNumber;
			StoreObjectId storeObjectId = activity.ItemId;
			logRowFormatter[6] = ((storeObjectId == null) ? null : storeObjectId.ToBase64ProviderLevelItemId());
			logRowFormatter[7] = activity.ClientVersion;
			logRowFormatter[8] = activity.TenantName;
			logRowFormatter[9] = activity.LocaleId;
			logRowFormatter[10] = activity.CustomPropertiesString;
			storeObjectId = activity.PreviousItemId;
			logRowFormatter[11] = ((storeObjectId == null) ? null : storeObjectId.ToBase64ProviderLevelItemId());
			logRowFormatter[12] = activity.ActivityCreationTime.ToUtc().ToISOString();
			logRowFormatter[13] = ((activity.MailboxType == null) ? null : string.Format("0x{0:X16}", activity.MailboxType));
			logRowFormatter[14] = ((activity.NetId == null) ? null : activity.NetId.ToString());
			return logRowFormatter;
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x002435F8 File Offset: 0x002417F8
		public void Log(IEnumerable<Activity> activities)
		{
			if (activities == null)
			{
				throw new ArgumentNullException("activities");
			}
			int num = 0;
			bool flag = false;
			using (IEnumerator<Activity> enumerator = activities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Activity activity = enumerator.Current;
					flag = ActivityLogHelper.CatchNonFatalExceptions(delegate
					{
						this.Log(activity);
					}, null);
					if (flag)
					{
						break;
					}
					num++;
				}
			}
			if (flag)
			{
				ActivityFileLogger.activityPerfCounters.ActivityLogsFileWriteExceptions.Increment();
			}
			ActivityFileLogger.activityPerfCounters.ActivityLogsFileWriteCount.IncrementBy((long)num);
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x002436A8 File Offset: 0x002418A8
		public void Flush()
		{
			this.logger.Flush();
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x002436B8 File Offset: 0x002418B8
		private void Log(Activity activity)
		{
			LogRowFormatter fileLogRow = this.GetFileLogRow(activity);
			this.logger.Append(fileLogRow, -1);
		}

		// Token: 0x040058D4 RID: 22740
		internal const string ComponentName = "ActivityLogging";

		// Token: 0x040058D5 RID: 22741
		internal static readonly string LogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\ActivityLogging\\ActivityLogs");

		// Token: 0x040058D6 RID: 22742
		private static readonly string[] LogSchemaStrings = new string[]
		{
			"ActivityId",
			"Timestamp",
			"ClientId",
			"MailboxGuid",
			"ClientSession",
			"SequenceNumber",
			"ItemId",
			"ClientVersion",
			"TenantName",
			"LocaleId",
			"CustomProperties",
			"PreviousItemId",
			"ActivityCreationTime",
			"MailboxType",
			"NetId"
		};

		// Token: 0x040058D7 RID: 22743
		private static Lazy<ActivityFileLogger> lazyInstanceWrapper = new Lazy<ActivityFileLogger>(() => new ActivityFileLogger());

		// Token: 0x040058D8 RID: 22744
		private static MiddleTierStoragePerformanceCountersInstance activityPerfCounters = NamedPropMap.GetPerfCounters();

		// Token: 0x040058D9 RID: 22745
		private readonly LogSchema logSchema;

		// Token: 0x040058DA RID: 22746
		private readonly Log logger;

		// Token: 0x040058DB RID: 22747
		private readonly string filePrefix;
	}
}
