using System;
using System.Configuration;
using System.Web.Configuration;

namespace Microsoft.Exchange.Hygiene.Data.RawFileLogger
{
	// Token: 0x020001BA RID: 442
	internal class RawFileLoggerConfiguration : ConfigurationSection
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00038AE8 File Offset: 0x00036CE8
		public static string SectionName
		{
			get
			{
				return "RawFileLogger";
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00038AEF File Offset: 0x00036CEF
		public static RawFileLoggerConfiguration Instance
		{
			get
			{
				if (RawFileLoggerConfiguration.instance == null)
				{
					RawFileLoggerConfiguration.instance = (((RawFileLoggerConfiguration)WebConfigurationManager.GetSection(RawFileLoggerConfiguration.SectionName)) ?? new RawFileLoggerConfiguration());
				}
				return RawFileLoggerConfiguration.instance;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00038B1A File Offset: 0x00036D1A
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x00038B2C File Offset: 0x00036D2C
		[ConfigurationProperty("maximumLogAge", IsRequired = false, DefaultValue = "10.00:00:00")]
		public TimeSpan MaximumLogAge
		{
			get
			{
				return (TimeSpan)base["maximumLogAge"];
			}
			set
			{
				base["maximumLogAge"] = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00038B3F File Offset: 0x00036D3F
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00038B51 File Offset: 0x00036D51
		[ConfigurationProperty("maximumLogDirectorySize", IsRequired = false, DefaultValue = 107374182400L)]
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 214748364800L)]
		public long MaximumLogDirectorySize
		{
			get
			{
				return (long)base["maximumLogDirectorySize"];
			}
			set
			{
				base["maximumLogDirectorySize"] = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00038B64 File Offset: 0x00036D64
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x00038B76 File Offset: 0x00036D76
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 1073741824L)]
		[ConfigurationProperty("maximumLogFileSize", IsRequired = false, DefaultValue = 31457280L)]
		public long MaximumLogFileSize
		{
			get
			{
				return (long)base["maximumLogFileSize"];
			}
			set
			{
				base["maximumLogFileSize"] = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00038B89 File Offset: 0x00036D89
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00038B9B File Offset: 0x00036D9B
		[ConfigurationProperty("logBufferSize", IsRequired = false, DefaultValue = 1024)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0)]
		public int LogBufferSize
		{
			get
			{
				return (int)base["logBufferSize"];
			}
			set
			{
				base["logBufferSize"] = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00038BAE File Offset: 0x00036DAE
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x00038BC0 File Offset: 0x00036DC0
		[ConfigurationProperty("bufferFlushInterval", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan LogBufferFlushInterval
		{
			get
			{
				return (TimeSpan)base["bufferFlushInterval"];
			}
			set
			{
				base["bufferFlushInterval"] = value;
			}
		}

		// Token: 0x040008DF RID: 2271
		private const string MaximumLogAgeKey = "maximumLogAge";

		// Token: 0x040008E0 RID: 2272
		private const string MaximumLogDirectorySizeKey = "maximumLogDirectorySize";

		// Token: 0x040008E1 RID: 2273
		private const string MaximumLogFileSizeKey = "maximumLogFileSize";

		// Token: 0x040008E2 RID: 2274
		private const string LogBufferSizeKey = "logBufferSize";

		// Token: 0x040008E3 RID: 2275
		private const string LogBufferFlushIntervalKey = "bufferFlushInterval";

		// Token: 0x040008E4 RID: 2276
		private static RawFileLoggerConfiguration instance;
	}
}
