using System;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Compliance;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x02000017 RID: 23
	internal class ComplianceProtocolLog : DisposeTrackableBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003F38 File Offset: 0x00002138
		public ComplianceProtocolLog(string path) : this(path, ComplianceProtocolLog.DefaultLogFileMaxAge, ComplianceProtocolLog.DefaultMaxDirectorySize, ComplianceProtocolLog.DefaultMaxFileSize, ComplianceProtocolLog.DefaultMaxCacheSize, ComplianceProtocolLog.DefaultLogFlushInterval)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003F5C File Offset: 0x0000215C
		protected ComplianceProtocolLog()
		{
			this.logLock = new object();
			this.logFileMaxAge = TimeSpan.FromDays(30.0);
			this.maxDirectorySize = 100;
			this.maxFileSize = 100;
			this.maxCacheSize = 10;
			this.logFlushInterval = TimeSpan.FromSeconds(10.0);
			base..ctor();
			this.logSchema = new LogSchema(this.SoftwareName, Assembly.GetExecutingAssembly().GetName().Version.ToString(), this.LogTypeName, ComplianceProtocolLog.GetColumnArray(ComplianceProtocolLog.Fields));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003FF0 File Offset: 0x000021F0
		protected ComplianceProtocolLog(string path, int logFileMaxAge, int maxDirectorySize, int maxFileSize, int maxCacheSize, int logFlushInterval)
		{
			this.logLock = new object();
			this.logFileMaxAge = TimeSpan.FromDays(30.0);
			this.maxDirectorySize = 100;
			this.maxFileSize = 100;
			this.maxCacheSize = 10;
			this.logFlushInterval = TimeSpan.FromSeconds(10.0);
			base..ctor();
			this.logPath = path;
			this.logFileMaxAge = TimeSpan.FromDays((double)logFileMaxAge);
			this.maxDirectorySize = maxDirectorySize * 1024 * 1024;
			this.maxFileSize = maxFileSize * 1024 * 1024;
			this.maxCacheSize = maxCacheSize * 1024 * 1024;
			this.logFlushInterval = TimeSpan.FromSeconds((double)logFlushInterval);
			this.logSchema = new LogSchema(this.SoftwareName, Assembly.GetExecutingAssembly().GetName().Version.ToString(), this.LogTypeName, ComplianceProtocolLog.GetColumnArray(ComplianceProtocolLog.Fields));
			this.log = new Log(this.FileNamePrefixName, new LogHeaderFormatter(this.logSchema, true), this.LogComponentName);
			this.Configure();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00004109 File Offset: 0x00002309
		internal string SoftwareName
		{
			get
			{
				return "Microsoft Exchange";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004110 File Offset: 0x00002310
		internal string LogComponentName
		{
			get
			{
				return "ComplianceProtocol";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004117 File Offset: 0x00002317
		internal string LogTypeName
		{
			get
			{
				return "Compliance Protocol Log";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000411E File Offset: 0x0000231E
		internal string FileNamePrefixName
		{
			get
			{
				return "ComplianceProtocol_";
			}
		}

		// Token: 0x17000010 RID: 16
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00004125 File Offset: 0x00002325
		protected static ComplianceProtocolLog Instance
		{
			set
			{
				ComplianceProtocolLog.instance = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000412D File Offset: 0x0000232D
		public static void CreateInstance(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				ComplianceProtocolLog.instance = null;
				return;
			}
			ComplianceProtocolLog.instance = new ComplianceProtocolLog(path);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004149 File Offset: 0x00002349
		public static ComplianceProtocolLog GetInstance()
		{
			return ComplianceProtocolLog.instance;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004150 File Offset: 0x00002350
		public static void Trace(long id, TraceType traceType, object parameter1)
		{
			if (ComplianceProtocolLog.GetInstance() == null)
			{
				return;
			}
			if (ExTraceGlobals.TaskDistributionSystemTracer.IsTraceEnabled(traceType))
			{
				string formatString = "{0}";
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceDebug(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceWarning(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceError(id, formatString, new object[]
					{
						parameter1
					});
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TracePerformance(id, formatString, new object[]
					{
						parameter1
					});
					return;
				}
				ExTraceGlobals.TaskDistributionSystemTracer.Information(id, formatString, new object[]
				{
					parameter1
				});
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004224 File Offset: 0x00002424
		public static void Trace(long id, TraceType traceType, object parameter1, object parameter2)
		{
			if (ComplianceProtocolLog.GetInstance() == null)
			{
				return;
			}
			if (ExTraceGlobals.TaskDistributionSystemTracer.IsTraceEnabled(traceType))
			{
				string formatString = "{0} {1}";
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceDebug(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceWarning(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceError(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TracePerformance(id, formatString, new object[]
					{
						parameter1,
						parameter2
					});
					return;
				}
				ExTraceGlobals.TaskDistributionSystemTracer.Information(id, formatString, new object[]
				{
					parameter1,
					parameter2
				});
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004310 File Offset: 0x00002510
		public static void Trace(long id, TraceType traceType, params object[] parameters)
		{
			if (ComplianceProtocolLog.GetInstance() == null)
			{
				return;
			}
			if (parameters != null && parameters.Length > 0 && ExTraceGlobals.TaskDistributionSystemTracer.IsTraceEnabled(traceType))
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < parameters.Length; i++)
				{
					stringBuilder.AppendFormat("{{{0}}}", i);
				}
				string formatString = stringBuilder.ToString();
				switch (traceType)
				{
				case TraceType.DebugTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceDebug(id, formatString, parameters);
					return;
				case TraceType.WarningTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceWarning(id, formatString, parameters);
					return;
				case TraceType.ErrorTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TraceError(id, formatString, parameters);
					return;
				case TraceType.PerformanceTrace:
					ExTraceGlobals.TaskDistributionSystemTracer.TracePerformance(id, formatString, parameters);
					return;
				}
				ExTraceGlobals.TaskDistributionSystemTracer.Information(id, formatString, parameters);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000043DC File Offset: 0x000025DC
		public static void LogIncomingMessage(ComplianceMessage complianceMessage)
		{
			ComplianceProtocolLog complianceProtocolLog = ComplianceProtocolLog.GetInstance();
			if (complianceProtocolLog != null && complianceMessage != null)
			{
				complianceProtocolLog.Log(complianceMessage, true);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004400 File Offset: 0x00002600
		public static void LogOutgoingMessage(ComplianceMessage complianceMessage)
		{
			ComplianceProtocolLog complianceProtocolLog = ComplianceProtocolLog.GetInstance();
			if (complianceProtocolLog != null && complianceMessage != null)
			{
				complianceProtocolLog.Log(complianceMessage, false);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004424 File Offset: 0x00002624
		public void Log(ComplianceMessage complianceMessage, bool incoming)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[1] = (incoming ? "In" : "Out");
			logRowFormatter[2] = complianceMessage.CorrelationId.ToString();
			logRowFormatter[3] = ((complianceMessage.TenantId != null) ? this.ByteArrayToString(complianceMessage.TenantId) : "NA");
			logRowFormatter[4] = ((!string.IsNullOrEmpty(complianceMessage.MessageId)) ? complianceMessage.MessageId : "NA");
			logRowFormatter[5] = ((!string.IsNullOrEmpty(complianceMessage.MessageSourceId)) ? complianceMessage.MessageSourceId : "NA");
			logRowFormatter[6] = ((complianceMessage.MessageTarget != null) ? this.TargetToString(complianceMessage.MessageTarget) : "NA");
			logRowFormatter[7] = ((complianceMessage.MessageSource != null) ? this.TargetToString(complianceMessage.MessageSource) : "NA");
			logRowFormatter[8] = complianceMessage.ComplianceMessageType.ToString();
			logRowFormatter[9] = complianceMessage.WorkDefinitionType.ToString();
			logRowFormatter[10] = ((complianceMessage.Payload != null) ? complianceMessage.Payload.Length.ToString() : "NA");
			logRowFormatter[11] = "NA";
			logRowFormatter[12] = "NA";
			logRowFormatter[13] = "NA";
			logRowFormatter[14] = "NA";
			logRowFormatter[15] = "NA";
			this.AppendLogRow(logRowFormatter);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000045B4 File Offset: 0x000027B4
		public void Configure()
		{
			if (!base.IsDisposed)
			{
				lock (this.logLock)
				{
					this.log.Configure(this.logPath, this.logFileMaxAge, (long)this.maxDirectorySize, (long)this.maxFileSize, this.maxCacheSize, this.logFlushInterval, true);
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004628 File Offset: 0x00002828
		public void Close()
		{
			if (!base.IsDisposed && this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000464C File Offset: 0x0000284C
		protected static string[] GetColumnArray(ComplianceProtocolLog.FieldInfo[] fields)
		{
			string[] array = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array[i] = fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004685 File Offset: 0x00002885
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004690 File Offset: 0x00002890
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ComplianceProtocolLog>(this);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004698 File Offset: 0x00002898
		protected virtual void AppendLogRow(LogRowFormatter row)
		{
			if (!base.IsDisposed)
			{
				this.log.Append(row, 0);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000046B0 File Offset: 0x000028B0
		private string ByteArrayToString(byte[] bytes)
		{
			string text = BitConverter.ToString(bytes);
			return text.Replace("-", string.Empty);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000046D4 File Offset: 0x000028D4
		private string TargetToString(Target target)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			stringBuilder.Append(string.Format("T:{0} I:{1} S:{2} D:{3} M:{4} F:{5}", new object[]
			{
				target.TargetType.ToString(),
				string.IsNullOrEmpty(target.Identifier) ? string.Empty : target.Identifier,
				string.IsNullOrEmpty(target.Server) ? string.Empty : target.Server,
				(target.Database == Guid.Empty) ? string.Empty : target.Database.ToString(),
				(target.Mailbox == Guid.Empty) ? string.Empty : target.Mailbox.ToString(),
				string.IsNullOrEmpty(target.Folder) ? string.Empty : target.Folder
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x04000023 RID: 35
		private const string DefaultData = "NA";

		// Token: 0x04000024 RID: 36
		internal static readonly ComplianceProtocolLog.FieldInfo[] Fields = new ComplianceProtocolLog.FieldInfo[]
		{
			new ComplianceProtocolLog.FieldInfo(0, "date-time"),
			new ComplianceProtocolLog.FieldInfo(1, "incoming"),
			new ComplianceProtocolLog.FieldInfo(2, "correlationid"),
			new ComplianceProtocolLog.FieldInfo(3, "tenantid"),
			new ComplianceProtocolLog.FieldInfo(4, "messageid"),
			new ComplianceProtocolLog.FieldInfo(5, "messagesourceid"),
			new ComplianceProtocolLog.FieldInfo(6, "messagetarget"),
			new ComplianceProtocolLog.FieldInfo(7, "messagesource"),
			new ComplianceProtocolLog.FieldInfo(8, "compliancemessagetype"),
			new ComplianceProtocolLog.FieldInfo(9, "workdefinitiontype"),
			new ComplianceProtocolLog.FieldInfo(10, "payloadsize"),
			new ComplianceProtocolLog.FieldInfo(11, "receivedtime"),
			new ComplianceProtocolLog.FieldInfo(12, "processedtime"),
			new ComplianceProtocolLog.FieldInfo(13, "queuedtime"),
			new ComplianceProtocolLog.FieldInfo(14, "genericinfo"),
			new ComplianceProtocolLog.FieldInfo(15, "fault")
		};

		// Token: 0x04000025 RID: 37
		private static readonly int DefaultLogFileMaxAge = 30;

		// Token: 0x04000026 RID: 38
		private static readonly int DefaultMaxDirectorySize = 100;

		// Token: 0x04000027 RID: 39
		private static readonly int DefaultMaxFileSize = 100;

		// Token: 0x04000028 RID: 40
		private static readonly int DefaultMaxCacheSize = 10;

		// Token: 0x04000029 RID: 41
		private static readonly int DefaultLogFlushInterval = 10;

		// Token: 0x0400002A RID: 42
		private static ComplianceProtocolLog instance = null;

		// Token: 0x0400002B RID: 43
		private readonly LogSchema logSchema;

		// Token: 0x0400002C RID: 44
		private readonly object logLock;

		// Token: 0x0400002D RID: 45
		private readonly TimeSpan logFileMaxAge;

		// Token: 0x0400002E RID: 46
		private readonly int maxDirectorySize;

		// Token: 0x0400002F RID: 47
		private readonly int maxFileSize;

		// Token: 0x04000030 RID: 48
		private readonly int maxCacheSize;

		// Token: 0x04000031 RID: 49
		private readonly TimeSpan logFlushInterval;

		// Token: 0x04000032 RID: 50
		private readonly string logPath;

		// Token: 0x04000033 RID: 51
		private Log log;

		// Token: 0x02000018 RID: 24
		protected enum Field : byte
		{
			// Token: 0x04000035 RID: 53
			DateTime,
			// Token: 0x04000036 RID: 54
			Incoming,
			// Token: 0x04000037 RID: 55
			CorrelationId,
			// Token: 0x04000038 RID: 56
			TenantId,
			// Token: 0x04000039 RID: 57
			MessageId,
			// Token: 0x0400003A RID: 58
			MessageSourceId,
			// Token: 0x0400003B RID: 59
			MessageTarget,
			// Token: 0x0400003C RID: 60
			MessageSource,
			// Token: 0x0400003D RID: 61
			ComplianceMessageType,
			// Token: 0x0400003E RID: 62
			WorkDefinitionType,
			// Token: 0x0400003F RID: 63
			PayloadSize,
			// Token: 0x04000040 RID: 64
			ReceivedTime,
			// Token: 0x04000041 RID: 65
			ProcessedTime,
			// Token: 0x04000042 RID: 66
			QueuedTime,
			// Token: 0x04000043 RID: 67
			GenericInfo,
			// Token: 0x04000044 RID: 68
			Fault
		}

		// Token: 0x02000019 RID: 25
		internal struct FieldInfo
		{
			// Token: 0x06000059 RID: 89 RVA: 0x0000499A File Offset: 0x00002B9A
			public FieldInfo(byte field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x04000045 RID: 69
			internal readonly byte Field;

			// Token: 0x04000046 RID: 70
			internal readonly string ColumnName;
		}
	}
}
