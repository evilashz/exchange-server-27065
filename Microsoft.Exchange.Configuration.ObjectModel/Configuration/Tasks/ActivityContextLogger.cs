using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200002B RID: 43
	internal abstract class ActivityContextLogger : DisposeTrackableBase, IWorkloadLogger
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006DF9 File Offset: 0x00004FF9
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00006E01 File Offset: 0x00005001
		private protected Log Log { protected get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006E0A File Offset: 0x0000500A
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006E12 File Offset: 0x00005012
		private protected bool Enabled { protected get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006E1B File Offset: 0x0000501B
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00006E23 File Offset: 0x00005023
		private protected LogSchema LogSchema { protected get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A1 RID: 417
		protected abstract string LogTypeName { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A2 RID: 418
		protected abstract string FileNamePrefix { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A3 RID: 419
		protected abstract string LogComponentName { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A4 RID: 420
		protected abstract Trace Tracer { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A5 RID: 421
		protected abstract int TimestampField { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006E2C File Offset: 0x0000502C
		protected virtual string ServerName
		{
			get
			{
				return ActivityContextLogger.serverName;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006E34 File Offset: 0x00005034
		private static Dictionary<ActivityEventType, string> CreateActivityEventTypeDictionary()
		{
			Dictionary<ActivityEventType, string> dictionary = new Dictionary<ActivityEventType, string>();
			foreach (object obj in Enum.GetValues(typeof(ActivityEventType)))
			{
				ActivityEventType activityEventType = (ActivityEventType)obj;
				dictionary.Add(activityEventType, activityEventType.ToString());
			}
			return dictionary;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006EA8 File Offset: 0x000050A8
		protected ActivityContextLogger() : this(string.Empty)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006EB8 File Offset: 0x000050B8
		protected ActivityContextLogger(string fileNamePrefix)
		{
			this.SafeTraceDebug(0L, "Start creating {0}.", new object[]
			{
				this.LogTypeName
			});
			string version = "0.0.0.0";
			Version version2 = Assembly.GetExecutingAssembly().GetName().Version;
			if (version2 != null)
			{
				version = version2.ToString();
			}
			this.LogSchema = new LogSchema("Microsoft Exchange", version, this.LogTypeName, this.GetLogFields());
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.LogSchema);
			string fileNamePrefix2 = string.IsNullOrWhiteSpace(fileNamePrefix) ? this.FileNamePrefix : fileNamePrefix;
			this.Log = new Log(fileNamePrefix2, headerFormatter, this.LogComponentName);
			ActivityContextLogFileSettings logFileSettings = this.GetLogFileSettings();
			if (logFileSettings.Enabled)
			{
				this.Enabled = true;
				this.Log.Configure(logFileSettings.DirectoryPath, logFileSettings.MaxAge, (long)logFileSettings.MaxDirectorySize.ToBytes(), (long)logFileSettings.MaxFileSize.ToBytes(), (int)logFileSettings.CacheSize.ToBytes(), logFileSettings.FlushInterval, logFileSettings.FlushToDisk);
				this.InternalConfigure(logFileSettings);
				this.SafeTraceDebug(0L, "{0} is configured.", new object[]
				{
					this.LogTypeName
				});
			}
			else
			{
				this.Enabled = false;
				this.SafeTraceDebug(0L, "{0} is disabled.", new object[]
				{
					this.LogTypeName
				});
			}
			this.SafeTraceDebug(0L, "{0} on server {1} is created and ready for use.", new object[]
			{
				this.LogTypeName,
				this.ServerName
			});
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007051 File Offset: 0x00005251
		public void LogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			if (activityScope == null)
			{
				throw new ArgumentNullException("activityScope");
			}
			this.InternalLogActivityEvent(activityScope, eventType);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007069 File Offset: 0x00005269
		protected void SafeTraceDebug(long id, string message, params object[] args)
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug(id, message, args);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007084 File Offset: 0x00005284
		protected void FlushLog()
		{
			this.SafeTraceDebug(0L, "Start flushing {0}.", new object[]
			{
				this.LogTypeName
			});
			this.Log.Flush();
			this.SafeTraceDebug(0L, "{0} is flushed.", new object[]
			{
				this.LogTypeName
			});
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000070D8 File Offset: 0x000052D8
		protected void AppendLog(LogRowFormatter row)
		{
			this.Log.Append(row, this.TimestampField);
			this.SafeTraceDebug(0L, "Row is appended for {0}.", new object[]
			{
				this.LogTypeName
			});
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007115 File Offset: 0x00005315
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityContextLogger>(this);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007120 File Offset: 0x00005320
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.SafeTraceDebug(0L, "Closing {0}.", new object[]
				{
					this.LogTypeName
				});
				if (this.Log != null)
				{
					this.Log.Close();
					this.Log = null;
				}
				this.SafeTraceDebug(0L, "{0} is closed.", new object[]
				{
					this.LogTypeName
				});
			}
		}

		// Token: 0x060001B0 RID: 432
		protected abstract void InternalLogActivityEvent(IActivityScope activityScope, ActivityEventType eventType);

		// Token: 0x060001B1 RID: 433
		protected abstract string[] GetLogFields();

		// Token: 0x060001B2 RID: 434
		protected abstract ActivityContextLogFileSettings GetLogFileSettings();

		// Token: 0x060001B3 RID: 435 RVA: 0x00007186 File Offset: 0x00005386
		protected virtual void InternalConfigure(ActivityContextLogFileSettings settings)
		{
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007188 File Offset: 0x00005388
		protected bool IsDebugTraceEnabled
		{
			get
			{
				return this.Tracer != null && this.Tracer.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x04000085 RID: 133
		private const string DefaultAssemblyVersion = "0.0.0.0";

		// Token: 0x04000086 RID: 134
		private const string SoftwareName = "Microsoft Exchange";

		// Token: 0x04000087 RID: 135
		private static readonly string serverName = Environment.MachineName;

		// Token: 0x04000088 RID: 136
		protected static readonly Dictionary<ActivityEventType, string> ActivityEventTypeDictionary = ActivityContextLogger.CreateActivityEventTypeDictionary();
	}
}
