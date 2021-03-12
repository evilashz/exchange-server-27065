using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200015A RID: 346
	internal abstract class StatisticsLogger : DisposableBase
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B14 RID: 2836
		protected abstract StatisticsLogger.StatisticsLogSchema LogSchema { get; }

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002994E File Offset: 0x00027B4E
		public void Init()
		{
			this.Init(AppConfig.Instance.Service.StatisticsLoggingEnabled, "UnifiedMessaging\\log");
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002996C File Offset: 0x00027B6C
		public void Init(bool statisticsLoggingEnabled, string logDirectoryPath)
		{
			this.Init(VariantConfiguration.InvariantNoFlightingSnapshot.UM.AnonymizeLogging.Enabled, statisticsLoggingEnabled, Path.Combine(Utils.GetExchangeDirectory(), logDirectoryPath), AppConfig.Instance.Service.StatisticsLoggingMaxDirectorySize, AppConfig.Instance.Service.StatisticsLoggingMaxFileSize);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000299C0 File Offset: 0x00027BC0
		public void Init(bool anonymise, bool statisticsLoggingEnabled, string logDirectoryPath, int maxDirectorySize, int maxFileSize)
		{
			this.anonymise = anonymise;
			this.statisticsLoggingEnabled = statisticsLoggingEnabled;
			if (this.statisticsLoggingEnabled)
			{
				this.log = new Log(this.LogSchema.LogType + this.LogSchema.Version, new LogHeaderFormatter(this.LogSchema, true), "UnifiedMessaging");
				TimeSpan timeSpan = TimeSpan.FromDays(30.0);
				long num = (long)ByteQuantifiedSize.FromGB((ulong)((long)maxDirectorySize)).ToBytes();
				long num2 = (long)ByteQuantifiedSize.FromMB((ulong)((long)maxFileSize)).ToBytes();
				this.log.Configure(logDirectoryPath, timeSpan, num, num2);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Initialized log file. Path: '{0}'; Max age: '{1}'; Max dir size: '{2}'; Max file size: '{3}';", new object[]
				{
					logDirectoryPath,
					timeSpan,
					num,
					num2
				});
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Statistics logging is disabled", new object[0]);
			}
			this.initialized = true;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00029ABC File Offset: 0x00027CBC
		public void Append(StatisticsLogger.StatisticsLogRow row)
		{
			base.CheckDisposed();
			ExAssert.RetailAssert(this.initialized, "Statistics logger has not been initialized yet");
			if (!this.statisticsLoggingEnabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Statistics logging is disabled", new object[0]);
				return;
			}
			row.PopulateFields();
			using (StatisticsLogger.StatisticsLogRowFormatterGenerator statisticsLogRowFormatterGenerator = StatisticsLogger.StatisticsLogRowFormatterGenerator.Create(this.anonymise))
			{
				LogRowFormatter row2 = statisticsLogRowFormatterGenerator.CreateLogRowFormatter(row);
				try
				{
					this.log.Append(row2, -1);
				}
				catch (ObjectDisposedException ex)
				{
					string message = CallIdTracer.FormatMessage("Trying to log a row after disposing the logger. Exception: {0}", new object[]
					{
						ex
					});
					message = CallIdTracer.FormatMessageWithContextAndCallId(this, message);
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Log file is not accessible because it has been closed, logging will be skipped. Exception: {0}.", new object[]
					{
						ex
					});
				}
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00029B94 File Offset: 0x00027D94
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Disposing statistics logger...", new object[0]);
				if (this.log != null)
				{
					this.log.Close();
					this.log = null;
				}
			}
		}

		// Token: 0x040005E7 RID: 1511
		private const string LogComponent = "UnifiedMessaging";

		// Token: 0x040005E8 RID: 1512
		private const int LogMaxAgeInDays = 30;

		// Token: 0x040005E9 RID: 1513
		private Log log;

		// Token: 0x040005EA RID: 1514
		private bool initialized;

		// Token: 0x040005EB RID: 1515
		private bool anonymise;

		// Token: 0x040005EC RID: 1516
		private bool statisticsLoggingEnabled;

		// Token: 0x0200015B RID: 347
		public class StatisticsLogColumn
		{
			// Token: 0x06000B1B RID: 2843 RVA: 0x00029BD1 File Offset: 0x00027DD1
			public StatisticsLogColumn(string name, bool anonymise)
			{
				this.name = name;
				this.anonymise = anonymise;
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00029BE7 File Offset: 0x00027DE7
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00029BEF File Offset: 0x00027DEF
			public bool Anonymise
			{
				get
				{
					return this.anonymise;
				}
			}

			// Token: 0x040005ED RID: 1517
			private readonly string name;

			// Token: 0x040005EE RID: 1518
			private readonly bool anonymise;
		}

		// Token: 0x0200015C RID: 348
		public abstract class StatisticsLogSchema : LogSchema
		{
			// Token: 0x06000B1E RID: 2846 RVA: 0x00029BFF File Offset: 0x00027DFF
			public StatisticsLogSchema(string version, string logType, StatisticsLogger.StatisticsLogColumn[] columns) : base("Microsoft Exchange Server", version, logType, columns.ConvertAll((StatisticsLogger.StatisticsLogColumn column) => column.Name).ToArray())
			{
				this.columns = columns;
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x00029C3D File Offset: 0x00027E3D
			public bool ShouldAnonymise(int index)
			{
				return this.columns[index].Anonymise;
			}

			// Token: 0x040005EF RID: 1519
			private const string StatisticsLogSoftware = "Microsoft Exchange Server";

			// Token: 0x040005F0 RID: 1520
			private readonly StatisticsLogger.StatisticsLogColumn[] columns;
		}

		// Token: 0x0200015D RID: 349
		public abstract class StatisticsLogRow
		{
			// Token: 0x06000B21 RID: 2849 RVA: 0x00029C4C File Offset: 0x00027E4C
			public StatisticsLogRow(StatisticsLogger.StatisticsLogSchema logSchema)
			{
				this.logSchema = logSchema;
				this.fields = new string[this.LogSchema.Fields.Length];
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00029C73 File Offset: 0x00027E73
			public StatisticsLogger.StatisticsLogSchema LogSchema
			{
				get
				{
					return this.logSchema;
				}
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00029C7B File Offset: 0x00027E7B
			public string[] Fields
			{
				get
				{
					return this.fields;
				}
			}

			// Token: 0x06000B24 RID: 2852
			public abstract void PopulateFields();

			// Token: 0x040005F2 RID: 1522
			private readonly StatisticsLogger.StatisticsLogSchema logSchema;

			// Token: 0x040005F3 RID: 1523
			private readonly string[] fields;
		}

		// Token: 0x0200015E RID: 350
		private abstract class StatisticsLogRowFormatterGenerator : DisposableBase
		{
			// Token: 0x06000B25 RID: 2853 RVA: 0x00029C83 File Offset: 0x00027E83
			public static StatisticsLogger.StatisticsLogRowFormatterGenerator Create(bool isDatacenter)
			{
				if (isDatacenter)
				{
					return new StatisticsLogger.DatacenterStatisticsLogRowFormatterGenerator();
				}
				return new StatisticsLogger.EnterpriseStatisticsLogRowFormatterGenerator();
			}

			// Token: 0x06000B26 RID: 2854 RVA: 0x00029C94 File Offset: 0x00027E94
			public LogRowFormatter CreateLogRowFormatter(StatisticsLogger.StatisticsLogRow row)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(row.LogSchema);
				for (int i = 0; i < row.Fields.Length; i++)
				{
					string text = row.Fields[i];
					logRowFormatter[i] = (row.LogSchema.ShouldAnonymise(i) ? this.Anonymise(text) : text);
				}
				return logRowFormatter;
			}

			// Token: 0x06000B27 RID: 2855
			protected abstract string Anonymise(string value);
		}

		// Token: 0x0200015F RID: 351
		private class EnterpriseStatisticsLogRowFormatterGenerator : StatisticsLogger.StatisticsLogRowFormatterGenerator
		{
			// Token: 0x06000B29 RID: 2857 RVA: 0x00029CF1 File Offset: 0x00027EF1
			protected override string Anonymise(string value)
			{
				return value;
			}

			// Token: 0x06000B2A RID: 2858 RVA: 0x00029CF4 File Offset: 0x00027EF4
			protected override void InternalDispose(bool disposing)
			{
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x00029CF6 File Offset: 0x00027EF6
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<StatisticsLogger.EnterpriseStatisticsLogRowFormatterGenerator>(this);
			}
		}

		// Token: 0x02000160 RID: 352
		private class DatacenterStatisticsLogRowFormatterGenerator : StatisticsLogger.StatisticsLogRowFormatterGenerator
		{
			// Token: 0x06000B2D RID: 2861 RVA: 0x00029D06 File Offset: 0x00027F06
			public DatacenterStatisticsLogRowFormatterGenerator()
			{
				this.sha256 = new SHA256Cng();
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x00029D1C File Offset: 0x00027F1C
			protected override string Anonymise(string value)
			{
				if (string.IsNullOrEmpty(value))
				{
					return value;
				}
				byte[] inArray = this.sha256.ComputeHash(Encoding.Default.GetBytes(value));
				return Convert.ToBase64String(inArray);
			}

			// Token: 0x06000B2F RID: 2863 RVA: 0x00029D50 File Offset: 0x00027F50
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.sha256 != null)
				{
					this.sha256.Clear();
					this.sha256 = null;
				}
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x00029D6F File Offset: 0x00027F6F
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<StatisticsLogger.DatacenterStatisticsLogRowFormatterGenerator>(this);
			}

			// Token: 0x040005F4 RID: 1524
			private SHA256Cng sha256;
		}
	}
}
