using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001CD RID: 461
	internal class SimpleObjectLogConfiguration : ObjectLogConfiguration
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0002B2A8 File Offset: 0x000294A8
		public SimpleObjectLogConfiguration(string logName, string logEnabledKey, string maxDirSizeKey, string maxFileSizeKey)
		{
			this.LogName = logName;
			this.LogEnabledKey = logEnabledKey;
			this.MaxDirSizeKey = maxDirSizeKey;
			this.MaxFileSizeKey = maxFileSizeKey;
			this.DirectoryName = string.Format("{0}s", this.LogName);
			this.logComponentName = string.Format("{0}{1}Log", this.ComponentName, this.LogName);
			this.filenamePrefix = string.Format("{0}_{1}_", this.ComponentName, this.LogName);
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0002B326 File Offset: 0x00029526
		public override bool IsEnabled
		{
			get
			{
				return string.IsNullOrEmpty(this.LogEnabledKey) || ConfigBase<MRSConfigSchema>.GetConfig<bool>(this.LogEnabledKey);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0002B342 File Offset: 0x00029542
		public override TimeSpan MaxLogAge
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("MaxLogAge");
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0002B34E File Offset: 0x0002954E
		public override bool FlushToDisk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0002B351 File Offset: 0x00029551
		public override TimeSpan StreamFlushInterval
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0002B364 File Offset: 0x00029564
		public override string LoggingFolder
		{
			get
			{
				string text = ConfigBase<MRSConfigSchema>.GetConfig<string>("LoggingPath");
				if (string.IsNullOrEmpty(text))
				{
					text = MRSConfigSchema.DefaultLoggingPath;
				}
				return Path.Combine(text, this.DirectoryName);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0002B396 File Offset: 0x00029596
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x0002B39E File Offset: 0x0002959E
		public string DirectoryName { get; private set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0002B3A7 File Offset: 0x000295A7
		public override string LogComponentName
		{
			get
			{
				return this.logComponentName;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0002B3AF File Offset: 0x000295AF
		public override string FilenamePrefix
		{
			get
			{
				return this.filenamePrefix;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0002B3B7 File Offset: 0x000295B7
		public override long MaxLogDirSize
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<long>(this.MaxDirSizeKey);
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0002B3C4 File Offset: 0x000295C4
		public override long MaxLogFileSize
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<long>(this.MaxFileSizeKey);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0002B3D1 File Offset: 0x000295D1
		protected virtual string ComponentName
		{
			get
			{
				return "MRS";
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0002B3D8 File Offset: 0x000295D8
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x0002B3E0 File Offset: 0x000295E0
		private protected string LogName { protected get; private set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0002B3E9 File Offset: 0x000295E9
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0002B3F1 File Offset: 0x000295F1
		private protected string LogEnabledKey { protected get; private set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0002B3FA File Offset: 0x000295FA
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x0002B402 File Offset: 0x00029602
		private protected string MaxDirSizeKey { protected get; private set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0002B40B File Offset: 0x0002960B
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x0002B413 File Offset: 0x00029613
		private protected string MaxFileSizeKey { protected get; private set; }

		// Token: 0x040009BE RID: 2494
		private const string FilenamePrefixFormat = "{0}_{1}_";

		// Token: 0x040009BF RID: 2495
		private const string LogComponentFormat = "{0}{1}Log";

		// Token: 0x040009C0 RID: 2496
		private const string DirectoryNameFormat = "{0}s";

		// Token: 0x040009C1 RID: 2497
		private readonly string logComponentName;

		// Token: 0x040009C2 RID: 2498
		private readonly string filenamePrefix;
	}
}
