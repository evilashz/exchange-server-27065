using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;

namespace Microsoft.Exchange.Migration.Logging
{
	// Token: 0x02000074 RID: 116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationLog : ADConfigurationLoader<Server, Server>
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001E24A File Offset: 0x0001C44A
		public MigrationLog()
		{
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 120, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationLog.cs");
			base.ReadConfiguration();
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001E27C File Offset: 0x0001C47C
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		public static string DefaultLogPath
		{
			get
			{
				if (MigrationLog.defaultLogPath == null)
				{
					string text = ExchangeSetupContext.InstallPath;
					if (text == null)
					{
						text = Assembly.GetExecutingAssembly().Location;
						text = Path.GetDirectoryName(text);
					}
					MigrationLog.defaultLogPath = Path.Combine(text, "logging\\MigrationLogs");
				}
				return MigrationLog.defaultLogPath;
			}
			internal set
			{
				MigrationLog.defaultLogPath = value;
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		public void Close()
		{
			lock (MigrationLog.lockObj)
			{
				if (this.log != null)
				{
					this.log.Close();
					this.log = null;
				}
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001E31C File Offset: 0x0001C51C
		public void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			foreach (KeyValuePair<MigrationEventType, MigrationLog.Tracer> keyValuePair in MigrationLog.Tracers)
			{
				if (keyValuePair.Key == eventType)
				{
					keyValuePair.Value((long)context.GetHashCode(), string.Join(",", new string[]
					{
						source,
						context.ToString(),
						format
					}), args);
					break;
				}
			}
			if (this.log == null || !this.IsLoggingLevelEnabled(eventType))
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(MigrationLog.Schema);
			logRowFormatter[1] = Thread.CurrentThread.ManagedThreadId;
			logRowFormatter[3] = eventType;
			logRowFormatter[2] = source;
			logRowFormatter[4] = context.ToString();
			if (args != null)
			{
				logRowFormatter[5] = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				logRowFormatter[5] = format;
			}
			lock (MigrationLog.lockObj)
			{
				this.log.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001E450 File Offset: 0x0001C650
		public void Flush()
		{
			lock (MigrationLog.lockObj)
			{
				this.log.Flush();
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001E494 File Offset: 0x0001C694
		internal bool IsLoggingLevelEnabled(MigrationEventType level)
		{
			return this.loggingLevel >= level;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
		protected override void LogFailure(ADConfigurationLoader<Server, Server>.FailureLocation failureLocation, Exception exception)
		{
			this.Log("ConfigurationLoader", MigrationEventType.Error, MigrationLogContext.Current, "Failed to perform {0} due to exception {1}", new object[]
			{
				failureLocation,
				exception
			});
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		protected override void PreAdOperation(ref Server server)
		{
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001E4DE File Offset: 0x0001C6DE
		protected override void AdOperation(ref Server server)
		{
			server = this.configurationSession.FindLocalServer();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		protected override void PostAdOperation(Server server, bool wasSuccessful)
		{
			if (wasSuccessful)
			{
				lock (MigrationLog.lockObj)
				{
					this.loggingLevel = server.MigrationLogLoggingLevel;
					string b = (null == server.MigrationLogFilePath) ? string.Empty : server.MigrationLogFilePath.ToString();
					if (this.log == null)
					{
						this.log = new Log("Migration_", new LogHeaderFormatter(MigrationLog.Schema), "Migration");
					}
					else if (string.Equals(this.logFilePath, b, StringComparison.OrdinalIgnoreCase) && this.maxAge == server.MigrationLogMaxAge && this.maxDirectorySize == server.MigrationLogMaxDirectorySize && this.maxFileSize == server.MigrationLogMaxFileSize)
					{
						return;
					}
					this.logFilePath = b;
					this.maxAge = server.MigrationLogMaxAge;
					this.maxDirectorySize = server.MigrationLogMaxDirectorySize;
					this.maxFileSize = server.MigrationLogMaxFileSize;
					this.log.Configure(string.IsNullOrEmpty(this.logFilePath) ? MigrationLog.DefaultLogPath : this.logFilePath, this.maxAge, (long)((double)this.maxDirectorySize), (long)((double)this.maxFileSize), false);
				}
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001E654 File Offset: 0x0001C854
		protected override void OnServerChangeCallback(ADNotificationEventArgs args)
		{
			base.ReadConfiguration();
		}

		// Token: 0x040002B8 RID: 696
		private static readonly string[] Fields = new string[]
		{
			"timestamp",
			"thread-id",
			"source",
			"event-type",
			"context",
			"data"
		};

		// Token: 0x040002B9 RID: 697
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "MigrationLog", MigrationLog.Fields);

		// Token: 0x040002BA RID: 698
		private static readonly KeyValuePair<MigrationEventType, MigrationLog.Tracer>[] Tracers = new KeyValuePair<MigrationEventType, MigrationLog.Tracer>[]
		{
			new KeyValuePair<MigrationEventType, MigrationLog.Tracer>(MigrationEventType.Verbose, new MigrationLog.Tracer(ExTraceGlobals.ServiceletTracer.TraceDebug)),
			new KeyValuePair<MigrationEventType, MigrationLog.Tracer>(MigrationEventType.Information, new MigrationLog.Tracer(ExTraceGlobals.ServiceletTracer.Information)),
			new KeyValuePair<MigrationEventType, MigrationLog.Tracer>(MigrationEventType.Warning, new MigrationLog.Tracer(ExTraceGlobals.ServiceletTracer.TraceWarning)),
			new KeyValuePair<MigrationEventType, MigrationLog.Tracer>(MigrationEventType.Error, new MigrationLog.Tracer(ExTraceGlobals.ServiceletTracer.TraceError))
		};

		// Token: 0x040002BB RID: 699
		private static string defaultLogPath;

		// Token: 0x040002BC RID: 700
		private static object lockObj = new object();

		// Token: 0x040002BD RID: 701
		private readonly ITopologyConfigurationSession configurationSession;

		// Token: 0x040002BE RID: 702
		private Log log;

		// Token: 0x040002BF RID: 703
		private string logFilePath;

		// Token: 0x040002C0 RID: 704
		private TimeSpan maxAge;

		// Token: 0x040002C1 RID: 705
		private ByteQuantifiedSize maxDirectorySize;

		// Token: 0x040002C2 RID: 706
		private ByteQuantifiedSize maxFileSize;

		// Token: 0x040002C3 RID: 707
		private MigrationEventType loggingLevel;

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060006B0 RID: 1712
		private delegate void Tracer(long id, string formatString, params object[] args);

		// Token: 0x02000076 RID: 118
		private enum Field
		{
			// Token: 0x040002C5 RID: 709
			TimeStamp,
			// Token: 0x040002C6 RID: 710
			ThreadID,
			// Token: 0x040002C7 RID: 711
			Source,
			// Token: 0x040002C8 RID: 712
			EventType,
			// Token: 0x040002C9 RID: 713
			Context,
			// Token: 0x040002CA RID: 714
			Data
		}
	}
}
