using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200002B RID: 43
	internal sealed class CommonDiagnosticsLog
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000078C4 File Offset: 0x00005AC4
		private void Configure(HostId hostId)
		{
			if (!string.IsNullOrEmpty(CommonDiagnosticsLog.LogPath))
			{
				string fileNamePrefix = Names<HostId>.Map[(int)hostId] + "." + "COMMONDIAG";
				this.log = new Log(fileNamePrefix, new LogHeaderFormatter(CommonDiagnosticsLog.commonDiagnosticsLogSchema), "CommonDiagnostics");
				this.log.Configure(CommonDiagnosticsLog.LogPath, TimeSpan.FromDays(30.0), 10485760L, 1048576L);
				this.initialized = true;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00007940 File Offset: 0x00005B40
		public static CommonDiagnosticsLog Instance
		{
			get
			{
				if (CommonDiagnosticsLog.instance == null)
				{
					lock (CommonDiagnosticsLog.initLock)
					{
						if (CommonDiagnosticsLog.instance == null)
						{
							if (CommonDiagnosticsLog.hostId == HostId.NotInitialized)
							{
								throw new InvalidOperationException("CommonDiagnosticsLog is not yet initialized");
							}
							CommonDiagnosticsLog commonDiagnosticsLog = new CommonDiagnosticsLog();
							commonDiagnosticsLog.Configure(CommonDiagnosticsLog.hostId);
							CommonDiagnosticsLog.instance = commonDiagnosticsLog;
						}
					}
				}
				return CommonDiagnosticsLog.instance;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000079B8 File Offset: 0x00005BB8
		public static string LogPath
		{
			get
			{
				string path = null;
				try
				{
					path = ConfigurationContext.Setup.InstallPath;
				}
				catch (SecurityException)
				{
					return null;
				}
				catch (IOException)
				{
					return null;
				}
				string result;
				try
				{
					result = Path.Combine(path, "TransportRoles\\Logs\\CommonDiagnosticsLog");
				}
				catch (ArgumentException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007A18 File Offset: 0x00005C18
		public static void Initialize(HostId hostId)
		{
			CommonDiagnosticsLog.hostId = hostId;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007A20 File Offset: 0x00005C20
		public void LogEvent(CommonDiagnosticsLog.Source source, IEnumerable<KeyValuePair<string, object>> eventData)
		{
			if (!this.initialized)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(CommonDiagnosticsLog.commonDiagnosticsLogSchema);
			logRowFormatter[1] = Names<CommonDiagnosticsLog.Source>.Map[(int)source];
			logRowFormatter[2] = eventData;
			this.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0400008B RID: 139
		public const string LogName = "COMMONDIAG";

		// Token: 0x0400008C RID: 140
		public const string LogType = "Common Diagnostics Log";

		// Token: 0x0400008D RID: 141
		public const string LogComponent = "CommonDiagnostics";

		// Token: 0x0400008E RID: 142
		private static string[] fields = new string[]
		{
			"TimeStamp",
			"Source",
			"EventData"
		};

		// Token: 0x0400008F RID: 143
		private static object initLock = new object();

		// Token: 0x04000090 RID: 144
		private static HostId hostId;

		// Token: 0x04000091 RID: 145
		private static LogSchema commonDiagnosticsLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Common Diagnostics Log", CommonDiagnosticsLog.fields);

		// Token: 0x04000092 RID: 146
		public static CsvTable CommonDiagnosticsLogTable = new CsvTable(new CsvField[]
		{
			new CsvField(CommonDiagnosticsLog.fields[0], typeof(DateTime)),
			new CsvField(CommonDiagnosticsLog.fields[1], typeof(string)),
			new CsvField(CommonDiagnosticsLog.fields[2], typeof(KeyValuePair<string, object>[]))
		});

		// Token: 0x04000093 RID: 147
		private static CommonDiagnosticsLog instance;

		// Token: 0x04000094 RID: 148
		private bool initialized;

		// Token: 0x04000095 RID: 149
		private Log log;

		// Token: 0x0200002C RID: 44
		internal enum Source
		{
			// Token: 0x04000097 RID: 151
			DeliveryReports,
			// Token: 0x04000098 RID: 152
			Trace,
			// Token: 0x04000099 RID: 153
			DeliveryReportsCache,
			// Token: 0x0400009A RID: 154
			MailSubmissionService,
			// Token: 0x0400009B RID: 155
			MailboxDeliveryService,
			// Token: 0x0400009C RID: 156
			MailboxTransportSubmissionService
		}
	}
}
