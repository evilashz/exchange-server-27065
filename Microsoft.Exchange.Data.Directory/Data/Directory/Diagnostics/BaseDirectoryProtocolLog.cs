using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000B3 RID: 179
	internal abstract class BaseDirectoryProtocolLog
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002B42B File Offset: 0x0002962B
		protected static bool LoggingEnabled
		{
			get
			{
				return BaseDirectoryProtocolLog.loggingEnabled.Value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002B437 File Offset: 0x00029637
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0002B43F File Offset: 0x0002963F
		private protected bool Initialized { protected get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009A1 RID: 2465
		protected abstract LogSchema Schema { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002B448 File Offset: 0x00029648
		protected Log Logger
		{
			get
			{
				return this.log;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002B450 File Offset: 0x00029650
		protected void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriond, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision, string logComponent)
		{
			if (this.Initialized)
			{
				throw new NotSupportedException("Protocol Log is already initialized");
			}
			BaseDirectoryProtocolLog.InitializeGlobalConfigIfRequired();
			if (this.log == null)
			{
				this.log = new Log(BaseDirectoryProtocolLog.logFilePrefix, new LogHeaderFormatter(this.Schema, LogHeaderCsvOption.CsvCompatible), logComponent);
				AppDomain.CurrentDomain.ProcessExit += this.CurrentDomainProcessExit;
			}
			if (BaseDirectoryProtocolLog.loggingEnabled.Value)
			{
				this.log.Configure(logFilePath, maxRetentionPeriond, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, BaseDirectoryProtocolLog.bufferSize, BaseDirectoryProtocolLog.DefaultFlushInterval, LogFileRollOver.Hourly);
			}
			BaseDirectoryProtocolLog.callsBacks = (TimerCallback)Delegate.Combine(BaseDirectoryProtocolLog.callsBacks, new TimerCallback(this.UpdateConfigIfChanged));
			this.Initialized = true;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002B50D File Offset: 0x0002970D
		protected virtual void UpdateConfigIfChanged(object state)
		{
			if ((bool)state)
			{
				this.Initialized = false;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002B520 File Offset: 0x00029720
		protected int GetNextSequenceNumber()
		{
			int result = Interlocked.Increment(ref this.sequenceNumber);
			if (0 > this.sequenceNumber)
			{
				this.sequenceNumber = 0;
				result = 0;
			}
			return result;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002B54C File Offset: 0x0002974C
		protected static bool GetRegistryBool(RegistryKey regkey, string key, bool defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(num.Value);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002B58D File Offset: 0x0002978D
		private void CurrentDomainProcessExit(object sender, EventArgs e)
		{
			this.Initialized = false;
			this.Shutdown();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002B59C File Offset: 0x0002979C
		private void Shutdown()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002B5B4 File Offset: 0x000297B4
		protected static string[] GetColumnArray(BaseDirectoryProtocolLog.FieldInfo[] fields)
		{
			string[] array = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array[i] = fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002B5F0 File Offset: 0x000297F0
		protected static string GetExchangeInstallPath()
		{
			string result = string.Empty;
			try
			{
				result = ExchangeSetupContext.InstallPath;
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002B640 File Offset: 0x00029840
		private static void InitializeGlobalConfigIfRequired()
		{
			lock (BaseDirectoryProtocolLog.globalLock)
			{
				if (BaseDirectoryProtocolLog.bufferSize == 0 || BaseDirectoryProtocolLog.loggingEnabled == null)
				{
					BaseDirectoryProtocolLog.ReadGlobalConfig();
					AppDomain.CurrentDomain.ProcessExit += delegate(object x, EventArgs y)
					{
						BaseDirectoryProtocolLog.loggingEnabled = new bool?(false);
						if (BaseDirectoryProtocolLog.timer != null)
						{
							BaseDirectoryProtocolLog.timer.Dispose();
						}
					};
				}
				if (BaseDirectoryProtocolLog.registryWatcher == null)
				{
					BaseDirectoryProtocolLog.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", false);
				}
				if (BaseDirectoryProtocolLog.timer == null)
				{
					BaseDirectoryProtocolLog.timer = new Timer(new TimerCallback(BaseDirectoryProtocolLog.GlobalUpdateConfigIfChanged), null, 0, 300000);
				}
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002B6F4 File Offset: 0x000298F4
		private static void GlobalUpdateConfigIfChanged(object state)
		{
			if (BaseDirectoryProtocolLog.registryWatcher.IsChanged())
			{
				bool flag = false;
				using (Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
				{
					bool value = BaseDirectoryProtocolLog.loggingEnabled.Value;
					BaseDirectoryProtocolLog.ReadGlobalConfig();
					flag = (value != BaseDirectoryProtocolLog.loggingEnabled.Value);
				}
				TimerCallback timerCallback = BaseDirectoryProtocolLog.callsBacks;
				if (timerCallback != null)
				{
					timerCallback(flag);
				}
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002B774 File Offset: 0x00029974
		private static void ReadGlobalConfig()
		{
			BaseDirectoryProtocolLog.bufferSize = Globals.GetIntValueFromRegistry("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", "LogBufferSize", 1048576, 0);
			int intValueFromRegistry = Globals.GetIntValueFromRegistry("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", "FlushIntervalInMinutes", 15, 0);
			if (intValueFromRegistry > 0)
			{
				BaseDirectoryProtocolLog.DefaultFlushInterval = TimeSpan.FromMinutes((double)intValueFromRegistry);
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				BaseDirectoryProtocolLog.loggingEnabled = new bool?(BaseDirectoryProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true));
			}
		}

		// Token: 0x0400033D RID: 829
		private const int DefaultLogBufferSize = 1048576;

		// Token: 0x0400033E RID: 830
		protected const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters";

		// Token: 0x0400033F RID: 831
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x04000340 RID: 832
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x04000341 RID: 833
		private const string LogFlushIntervalRegKeyName = "FlushIntervalInMinutes";

		// Token: 0x04000342 RID: 834
		protected static readonly TimeSpan DefaultMaxRetentionPeriod = TimeSpan.FromHours(8.0);

		// Token: 0x04000343 RID: 835
		protected static readonly ByteQuantifiedSize DefaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x04000344 RID: 836
		protected static readonly ByteQuantifiedSize DefaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x04000345 RID: 837
		protected static TimeSpan DefaultFlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000346 RID: 838
		private static readonly string logFilePrefix = Globals.ProcessName + "_" + Globals.ProcessAppName + "_";

		// Token: 0x04000347 RID: 839
		private static object globalLock = new object();

		// Token: 0x04000348 RID: 840
		private static Timer timer;

		// Token: 0x04000349 RID: 841
		private static int bufferSize;

		// Token: 0x0400034A RID: 842
		private static bool? loggingEnabled;

		// Token: 0x0400034B RID: 843
		private static RegistryWatcher registryWatcher;

		// Token: 0x0400034C RID: 844
		private static TimerCallback callsBacks;

		// Token: 0x0400034D RID: 845
		private int sequenceNumber;

		// Token: 0x0400034E RID: 846
		private Log log;

		// Token: 0x020000B4 RID: 180
		internal struct FieldInfo
		{
			// Token: 0x060009B1 RID: 2481 RVA: 0x0002B881 File Offset: 0x00029A81
			public FieldInfo(byte field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x04000351 RID: 849
			internal readonly byte Field;

			// Token: 0x04000352 RID: 850
			internal readonly string ColumnName;
		}
	}
}
