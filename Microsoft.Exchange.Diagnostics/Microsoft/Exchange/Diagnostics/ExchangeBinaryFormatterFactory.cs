using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001CD RID: 461
	public static class ExchangeBinaryFormatterFactory
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x00036698 File Offset: 0x00034898
		public static BinaryFormatter CreateBinaryFormatter(SerializationBinder binder = null)
		{
			return new BinaryFormatter
			{
				Binder = new ChainedSerializationBinder(binder, ExchangeBinaryFormatterFactory.typeGatherer.Value, new string[0])
			};
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000366C8 File Offset: 0x000348C8
		public static BinaryFormatter CreateBinaryFormatter(SerializationBinder binder, params string[] allowList)
		{
			return new BinaryFormatter
			{
				Binder = new ChainedSerializationBinder(binder, ExchangeBinaryFormatterFactory.typeGatherer.Value, allowList)
			};
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000366F3 File Offset: 0x000348F3
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x000366FA File Offset: 0x000348FA
		public static bool LoggingEnabled
		{
			get
			{
				return ExchangeBinaryFormatterFactory.loggingEnabled;
			}
			private set
			{
				ExchangeBinaryFormatterFactory.loggingEnabled = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00036702 File Offset: 0x00034902
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00036709 File Offset: 0x00034909
		public static TimeSpan LogDumpInterval
		{
			get
			{
				return ExchangeBinaryFormatterFactory.logDumpInterval;
			}
			set
			{
				if (value != TimeSpan.Zero)
				{
					ExchangeBinaryFormatterFactory.logDumpInterval = value;
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0003671E File Offset: 0x0003491E
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00036725 File Offset: 0x00034925
		public static bool ClearAfterSave { get; private set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0003672D File Offset: 0x0003492D
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00036734 File Offset: 0x00034934
		public static bool IncludeStackTrace { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003673C File Offset: 0x0003493C
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00036743 File Offset: 0x00034943
		public static string ConfigError { get; private set; }

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0003674C File Offset: 0x0003494C
		public static string GetLogDirectory()
		{
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
					{
						if (registryKey2 != null)
						{
							object value = registryKey2.GetValue("MsiInstallPath");
							return (value != null) ? Path.Combine(value.ToString(), "Logging", "TypeDeserialization", ExchangeBinaryFormatterFactory.GetProtocolName()) : null;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000367F0 File Offset: 0x000349F0
		public static void RefreshConfig(string config)
		{
			if (string.IsNullOrEmpty(config))
			{
				return;
			}
			ExchangeBinaryFormatterFactory.ConfigureDefaultSettings();
			if (config.StartsWith("LoggingDisabled", StringComparison.OrdinalIgnoreCase))
			{
				ExchangeBinaryFormatterFactory.LoggingEnabled = false;
				return;
			}
			if (config.StartsWith("LoggingEnabled", StringComparison.OrdinalIgnoreCase))
			{
				ExchangeBinaryFormatterFactory.LoggingEnabled = true;
				if (config.IndexOf(';') > -1)
				{
					string[] array = config.Split(new char[]
					{
						';'
					});
					if (array.Length > 1)
					{
						for (int i = 1; i < array.Length; i++)
						{
							if (array[i].Equals("IncludeStackTrace", StringComparison.OrdinalIgnoreCase))
							{
								ExchangeBinaryFormatterFactory.IncludeStackTrace = true;
							}
							if (array[i].Equals("ClearAfterSave", StringComparison.OrdinalIgnoreCase))
							{
								ExchangeBinaryFormatterFactory.ClearAfterSave = true;
							}
							if (array[i].StartsWith("LogDumpInterval", StringComparison.OrdinalIgnoreCase))
							{
								ExchangeBinaryFormatterFactory.LogDumpInterval = ExchangeBinaryFormatterFactory.GetTimespanForLogDumpInterval(array[i]);
							}
						}
						return;
					}
				}
			}
			else
			{
				ExchangeBinaryFormatterFactory.ConfigureDefaultSettings();
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000368BC File Offset: 0x00034ABC
		private static IDeserializedTypesGatherer BuildTypeGatherer()
		{
			try
			{
				string logDirectory = ExchangeBinaryFormatterFactory.GetLogDirectory();
				if (logDirectory != null)
				{
					return new FileBasedDeserializedTypeGatherer(logDirectory, ExchangeBinaryFormatterFactory.LogDumpInterval)
					{
						AddStackTrace = ExchangeBinaryFormatterFactory.IncludeStackTrace,
						ClearAfterSave = ExchangeBinaryFormatterFactory.ClearAfterSave
					};
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00036910 File Offset: 0x00034B10
		private static string GetProtocolName()
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = (string.Equals(currentProcess.ProcessName, "w3wp", StringComparison.OrdinalIgnoreCase) ? ExchangeBinaryFormatterFactory.GetAppPoolId() : currentProcess.ProcessName);
			}
			return result;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00036964 File Offset: 0x00034B64
		private static string GetAppPoolId()
		{
			string text = Environment.GetEnvironmentVariable("APP_POOL_ID", EnvironmentVariableTarget.Process);
			if (string.IsNullOrWhiteSpace(text))
			{
				string input = Environment.CommandLine ?? string.Empty;
				Match match = Regex.Match(input, "-ap\\s+\"(?<appPool>\\w+)\"");
				if (match.Success)
				{
					text = match.Groups["appPool"].Value;
				}
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000369CD File Offset: 0x00034BCD
		private static void HandleConfigReadError(string error)
		{
			ExchangeBinaryFormatterFactory.ConfigError = DateTime.UtcNow + "-" + error;
			ExchangeBinaryFormatterFactory.ConfigureDefaultSettings();
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000369EE File Offset: 0x00034BEE
		private static void ConfigureDefaultSettings()
		{
			ExchangeBinaryFormatterFactory.LoggingEnabled = true;
			ExchangeBinaryFormatterFactory.LogDumpInterval = ExchangeBinaryFormatterFactory.defaultLogDumpInterval;
			ExchangeBinaryFormatterFactory.ClearAfterSave = false;
			ExchangeBinaryFormatterFactory.IncludeStackTrace = false;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00036A0C File Offset: 0x00034C0C
		private static TimeSpan GetTimespanForLogDumpInterval(string logDumpInterval)
		{
			TimeSpan timeSpan;
			if (logDumpInterval.IndexOf('=') <= -1 || !TimeSpan.TryParse(logDumpInterval.Substring(logDumpInterval.IndexOf('=') + 1), out timeSpan))
			{
				return ExchangeBinaryFormatterFactory.defaultLogDumpInterval;
			}
			if (!(timeSpan < ExchangeBinaryFormatterFactory.defaultLogDumpInterval))
			{
				return timeSpan;
			}
			return ExchangeBinaryFormatterFactory.defaultLogDumpInterval;
		}

		// Token: 0x04000989 RID: 2441
		public const string LOGREGKEY = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x0400098A RID: 2442
		public const string LOGREGVALUE = "ExchangeBinaryFormatterFactory";

		// Token: 0x0400098B RID: 2443
		private static readonly RegistryKeyChangeWatcher RegistryKeyChangeWatcher = new RegistryKeyChangeWatcher("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics", "ExchangeBinaryFormatterFactory", new Action<string>(ExchangeBinaryFormatterFactory.RefreshConfig), new Action<string>(ExchangeBinaryFormatterFactory.HandleConfigReadError));

		// Token: 0x0400098C RID: 2444
		private static readonly Lazy<IDeserializedTypesGatherer> typeGatherer = new Lazy<IDeserializedTypesGatherer>(() => ExchangeBinaryFormatterFactory.BuildTypeGatherer(), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x0400098D RID: 2445
		private static readonly TimeSpan defaultLogDumpInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400098E RID: 2446
		private static TimeSpan logDumpInterval = ExchangeBinaryFormatterFactory.defaultLogDumpInterval;

		// Token: 0x0400098F RID: 2447
		private static bool loggingEnabled = true;
	}
}
