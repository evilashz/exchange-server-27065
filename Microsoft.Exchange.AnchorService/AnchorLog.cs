using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorLog
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003560 File Offset: 0x00001760
		public AnchorLog(string applicationName, AnchorConfig config) : this(applicationName, config, ExTraceGlobals.ServiceletTracer)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003570 File Offset: 0x00001770
		public AnchorLog(string applicationName, AnchorConfig config, Trace tracer)
		{
			this.tracers = new KeyValuePair<MigrationEventType, AnchorLog.Tracer>[]
			{
				new KeyValuePair<MigrationEventType, AnchorLog.Tracer>(MigrationEventType.Verbose, new AnchorLog.Tracer(tracer.TraceDebug)),
				new KeyValuePair<MigrationEventType, AnchorLog.Tracer>(MigrationEventType.Information, new AnchorLog.Tracer(tracer.Information)),
				new KeyValuePair<MigrationEventType, AnchorLog.Tracer>(MigrationEventType.Warning, new AnchorLog.Tracer(tracer.TraceWarning)),
				new KeyValuePair<MigrationEventType, AnchorLog.Tracer>(MigrationEventType.Error, new AnchorLog.Tracer(tracer.TraceError))
			};
			this.ApplicationName = applicationName;
			this.Config = config;
			this.Update();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003628 File Offset: 0x00001828
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003630 File Offset: 0x00001830
		private MigrationEventType LoggingLevel { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003639 File Offset: 0x00001839
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003641 File Offset: 0x00001841
		private string ApplicationName { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000364A File Offset: 0x0000184A
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003652 File Offset: 0x00001852
		private AnchorConfig Config { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000365C File Offset: 0x0000185C
		private string DefaultLogPath
		{
			get
			{
				if (this.defaultLogPath == null)
				{
					string text = ExchangeSetupContext.InstallPath;
					if (text == null)
					{
						text = Assembly.GetExecutingAssembly().Location;
						text = Path.GetDirectoryName(text);
					}
					this.defaultLogPath = Path.Combine(text, string.Format("logging\\{0}Logs", this.ApplicationName));
				}
				return this.defaultLogPath;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000036AE File Offset: 0x000018AE
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000036B6 File Offset: 0x000018B6
		private string LogFilePath { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000036BF File Offset: 0x000018BF
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000036C7 File Offset: 0x000018C7
		private TimeSpan MaxAge { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000036D0 File Offset: 0x000018D0
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000036D8 File Offset: 0x000018D8
		private ByteQuantifiedSize MaxDirectorySize { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000036E1 File Offset: 0x000018E1
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000036E9 File Offset: 0x000018E9
		private ByteQuantifiedSize MaxFileSize { get; set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x000036F4 File Offset: 0x000018F4
		public void Close()
		{
			lock (this.lockObj)
			{
				if (this.log != null)
				{
					this.log.Close();
					this.log = null;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003748 File Offset: 0x00001948
		public void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			foreach (KeyValuePair<MigrationEventType, AnchorLog.Tracer> keyValuePair in this.tracers)
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
			if (this.log == null || this.LoggingLevel < eventType)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(AnchorLog.Schema);
			logRowFormatter[1] = Environment.CurrentManagedThreadId;
			logRowFormatter[3] = eventType;
			logRowFormatter[2] = source;
			logRowFormatter[4] = context.ToString();
			if (args != null && args.Length > 0)
			{
				logRowFormatter[5] = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				logRowFormatter[5] = format;
			}
			lock (this.lockObj)
			{
				this.log.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003880 File Offset: 0x00001A80
		protected virtual void Update()
		{
			MigrationEventType config = this.Config.GetConfig<MigrationEventType>("LoggingLevel");
			string config2 = this.Config.GetConfig<string>("LogFilePath");
			TimeSpan config3 = this.Config.GetConfig<TimeSpan>("LogMaxAge");
			ByteQuantifiedSize maxDirectorySize = new ByteQuantifiedSize(this.Config.GetConfig<ulong>("LogMaxDirectorySize"));
			ByteQuantifiedSize maxFileSize = new ByteQuantifiedSize(this.Config.GetConfig<ulong>("LogMaxFileSize"));
			this.Update(config, config2, config3, maxDirectorySize, maxFileSize);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038FC File Offset: 0x00001AFC
		protected void Update(MigrationEventType loggingLevel, string logFilePath, TimeSpan maxAge, ByteQuantifiedSize maxDirectorySize, ByteQuantifiedSize maxFileSize)
		{
			lock (this.lockObj)
			{
				this.LoggingLevel = loggingLevel;
				if (this.log == null)
				{
					this.log = new Log(string.Format("{0}_", this.ApplicationName), new LogHeaderFormatter(AnchorLog.Schema), this.ApplicationName);
				}
				else if (string.Equals(this.LogFilePath, logFilePath, StringComparison.OrdinalIgnoreCase) && this.MaxAge == maxAge && this.MaxDirectorySize == maxDirectorySize && this.MaxFileSize == maxFileSize)
				{
					return;
				}
				this.LogFilePath = logFilePath;
				this.MaxAge = maxAge;
				this.MaxDirectorySize = maxDirectorySize;
				this.MaxFileSize = maxFileSize;
				this.log.Configure(string.IsNullOrEmpty(this.LogFilePath) ? this.DefaultLogPath : this.LogFilePath, this.MaxAge, (long)((double)this.MaxDirectorySize), (long)((double)this.MaxFileSize), false);
			}
		}

		// Token: 0x04000037 RID: 55
		internal const string DefaultSeparator = ",";

		// Token: 0x04000038 RID: 56
		private static readonly string[] Fields = new string[]
		{
			"timestamp",
			"thread-id",
			"source",
			"event-type",
			"context",
			"data"
		};

		// Token: 0x04000039 RID: 57
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "AnchorLog", AnchorLog.Fields);

		// Token: 0x0400003A RID: 58
		private readonly KeyValuePair<MigrationEventType, AnchorLog.Tracer>[] tracers;

		// Token: 0x0400003B RID: 59
		private string defaultLogPath;

		// Token: 0x0400003C RID: 60
		private object lockObj = new object();

		// Token: 0x0400003D RID: 61
		private Log log;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x060000BA RID: 186
		private delegate void Tracer(long id, string formatString, params object[] args);

		// Token: 0x02000010 RID: 16
		private enum Field
		{
			// Token: 0x04000046 RID: 70
			TimeStamp,
			// Token: 0x04000047 RID: 71
			ThreadID,
			// Token: 0x04000048 RID: 72
			Source,
			// Token: 0x04000049 RID: 73
			EventType,
			// Token: 0x0400004A RID: 74
			Context,
			// Token: 0x0400004B RID: 75
			Data
		}
	}
}
