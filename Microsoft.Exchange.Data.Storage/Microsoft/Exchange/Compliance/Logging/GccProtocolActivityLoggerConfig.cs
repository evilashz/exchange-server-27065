using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Win32;

namespace Microsoft.Exchange.Compliance.Logging
{
	// Token: 0x020007CF RID: 1999
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GccProtocolActivityLoggerConfig
	{
		// Token: 0x06004AF6 RID: 19190 RVA: 0x00139B1C File Offset: 0x00137D1C
		public GccProtocolActivityLoggerConfig() : this(GccProtocolActivityLoggerConfig.ParseConfigValue(ConfigurationManager.AppSettings, "GccDisabled", true), null)
		{
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00139B38 File Offset: 0x00137D38
		internal GccProtocolActivityLoggerConfig(bool disabled, string logRoot)
		{
			NameValueCollection appSettings = ConfigurationManager.AppSettings;
			this.disabled = disabled;
			this.maxAge = GccProtocolActivityLoggerConfig.ParseConfigValue(appSettings, "GccLogMaxAge", GccProtocolActivityLoggerConfig.DefaultMaxAge);
			this.maxDirectorySize = GccProtocolActivityLoggerConfig.ParseConfigValue(appSettings, "GccLogMaxDirectorySize", long.MaxValue);
			this.maxLogfileSize = GccProtocolActivityLoggerConfig.ParseConfigValue(appSettings, "GccLogMaxLogfileSize", long.MaxValue);
			this.reportIntervalMilliseconds = GccProtocolActivityLoggerConfig.ParseConfigValue(appSettings, "GccReportIntervalMilliseconds", 60000.0);
			if (logRoot == null)
			{
				try
				{
					this.logRoot = (string)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15", "GccLogRoot", null) ?? string.Empty);
					goto IL_B5;
				}
				catch
				{
					this.logRoot = string.Empty;
					goto IL_B5;
				}
			}
			this.logRoot = logRoot;
			IL_B5:
			if (!this.disabled && this.logRoot.Length == 0)
			{
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "No GccLogRoot regkey was found - Criminal Compliance logging disabled");
				this.disabled = true;
			}
			try
			{
				if (!this.disabled && !Path.IsPathRooted(this.logRoot))
				{
					ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "GccLogRoot contains a relative path, must be absolute - Criminal Compliance logging disabled");
					this.disabled = true;
				}
			}
			catch (ArgumentException)
			{
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "GccLogRoot contains invalid path characters - Criminal Compliance logging disabled");
				this.disabled = true;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x00139C9C File Offset: 0x00137E9C
		public string LogRoot
		{
			get
			{
				return this.logRoot;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x00139CA4 File Offset: 0x00137EA4
		public TimeSpan MaxAge
		{
			get
			{
				return this.maxAge;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x00139CAC File Offset: 0x00137EAC
		public long MaxDirectorySize
		{
			get
			{
				return this.maxDirectorySize;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x00139CB4 File Offset: 0x00137EB4
		public long MaxLogfileSize
		{
			get
			{
				return this.maxLogfileSize;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x00139CBC File Offset: 0x00137EBC
		public double ReportIntervalMilliseconds
		{
			get
			{
				return this.reportIntervalMilliseconds;
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x00139CC4 File Offset: 0x00137EC4
		public bool Disabled
		{
			get
			{
				return this.disabled;
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00139CCC File Offset: 0x00137ECC
		private static bool ParseConfigValue(NameValueCollection values, string name, bool defaultValue)
		{
			string value = values[name];
			bool result = defaultValue;
			if (!string.IsNullOrEmpty(value))
			{
				bool.TryParse(value, out result);
			}
			return result;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00139CF8 File Offset: 0x00137EF8
		private static long ParseConfigValue(NameValueCollection values, string name, long defaultValue)
		{
			string text = values[name];
			long result = defaultValue;
			if (!string.IsNullOrEmpty(text))
			{
				long.TryParse(text, out result);
			}
			return result;
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00139D24 File Offset: 0x00137F24
		private static double ParseConfigValue(NameValueCollection values, string name, double defaultValue)
		{
			string text = values[name];
			double result = defaultValue;
			if (!string.IsNullOrEmpty(text))
			{
				double.TryParse(text, out result);
			}
			return result;
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x00139D50 File Offset: 0x00137F50
		private static TimeSpan ParseConfigValue(NameValueCollection values, string name, TimeSpan defaultValue)
		{
			string text = values[name];
			TimeSpan result = defaultValue;
			if (!string.IsNullOrEmpty(text))
			{
				TimeSpan.TryParse(text, out result);
			}
			return result;
		}

		// Token: 0x040028CA RID: 10442
		private const string Exchange14RootRegKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040028CB RID: 10443
		private const string GccLogRootRegValue = "GccLogRoot";

		// Token: 0x040028CC RID: 10444
		private const string GccDisabledSetting = "GccDisabled";

		// Token: 0x040028CD RID: 10445
		private const string GccMaxAgeSetting = "GccLogMaxAge";

		// Token: 0x040028CE RID: 10446
		private const string MaxDirectorySizeSetting = "GccLogMaxDirectorySize";

		// Token: 0x040028CF RID: 10447
		private const string MaxLogfileSizeSetting = "GccLogMaxLogfileSize";

		// Token: 0x040028D0 RID: 10448
		private const string ReportIntervalMillisecondsSetting = "GccReportIntervalMilliseconds";

		// Token: 0x040028D1 RID: 10449
		private const bool DefaultDisabled = true;

		// Token: 0x040028D2 RID: 10450
		private const long DefaultMaxDirectorySize = 9223372036854775807L;

		// Token: 0x040028D3 RID: 10451
		private const long DefaultMaxLogfileSize = 9223372036854775807L;

		// Token: 0x040028D4 RID: 10452
		private const double DefaultReportIntervalMilliseconds = 60000.0;

		// Token: 0x040028D5 RID: 10453
		private static readonly TimeSpan DefaultMaxAge = TimeSpan.MaxValue;

		// Token: 0x040028D6 RID: 10454
		private bool disabled;

		// Token: 0x040028D7 RID: 10455
		private string logRoot;

		// Token: 0x040028D8 RID: 10456
		private TimeSpan maxAge;

		// Token: 0x040028D9 RID: 10457
		private long maxDirectorySize;

		// Token: 0x040028DA RID: 10458
		private long maxLogfileSize;

		// Token: 0x040028DB RID: 10459
		private double reportIntervalMilliseconds;
	}
}
